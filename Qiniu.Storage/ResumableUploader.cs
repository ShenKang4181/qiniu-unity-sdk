using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class ResumableUploader
	{
		private Config config;

		private const int BLOCK_SIZE = 4194304;

		private HttpManager httpManager;

		public ResumableUploader(Config config)
		{
			if (config == null)
			{
				this.config = new Config();
			}
			else
			{
				this.config = config;
			}
			httpManager = new HttpManager(false);
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> UploadFile(string localFile, string key, string token, PutExtra putExtra)
		{
			try
			{
				FileStream stream = new FileStream(localFile, FileMode.Open);
				return await UploadStream(stream, key, token, putExtra);
			}
			catch (Exception ex)
			{
				HttpResult invalidFile = HttpResult.InvalidFile;
				invalidFile.RefText = ex.Message;
				return invalidFile;
			}
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> UploadStream(Stream stream, string key, string upToken, PutExtra putExtra)
		{
			HttpResult httpResult = new HttpResult();
			if (putExtra == null)
			{
				putExtra = new PutExtra();
			}
			if (putExtra.ProgressHandler == null)
			{
				putExtra.ProgressHandler = new UploadProgressHandler(DefaultUploadProgressHandler);
			}
			if (putExtra.UploadController == null)
			{
				putExtra.UploadController = new UploadController(DefaultUploadController);
			}
			if (putExtra.BlockUploadThreads <= 0 || putExtra.BlockUploadThreads > 64)
			{
				putExtra.BlockUploadThreads = 1;
			}
			using (stream)
			{
				try
				{
					long num = 0L;
					long length = stream.Length;
					long num2 = (length + 4194304 - 1) / 4194304;
					ResumeInfo resumeInfo = null;
					if (File.Exists(putExtra.ResumeRecordFile))
					{
						resumeInfo = ResumeHelper.Load(putExtra.ResumeRecordFile);
						if (resumeInfo != null && length == resumeInfo.FileSize && UnixTimestamp.IsContextExpired(resumeInfo.ExpiredAt))
						{
							resumeInfo = null;
						}
					}
					if (resumeInfo == null)
					{
						ResumeInfo resumeInfo2 = new ResumeInfo();
						resumeInfo2.FileSize = length;
						resumeInfo2.BlockCount = num2;
						resumeInfo2.Contexts = new string[num2];
						resumeInfo2.ExpiredAt = 0L;
						resumeInfo = resumeInfo2;
					}
					for (long num3 = 0L; num3 < num2; num3++)
					{
						string value = resumeInfo.Contexts[num3];
						if (!string.IsNullOrEmpty(value))
						{
							num += 4194304;
						}
					}
					putExtra.ProgressHandler(num, length);
					UploadControllerAction uploadControllerAction = putExtra.UploadController();
					ManualResetEvent manualResetEvent = new ManualResetEvent(false);
					Dictionary<long, byte[]> dictionary = new Dictionary<long, byte[]>();
					Dictionary<long, HttpResult> dictionary2 = new Dictionary<long, HttpResult>();
					Dictionary<string, long> dictionary3 = new Dictionary<string, long>();
					dictionary3.Add("UploadProgress", num);
					byte[] array = new byte[4194304];
					for (long num4 = 0L; num4 < num2; num4++)
					{
						string value2 = resumeInfo.Contexts[num4];
						if (!string.IsNullOrEmpty(value2))
						{
							continue;
						}
						while (true)
						{
							uploadControllerAction = putExtra.UploadController();
							switch (uploadControllerAction)
							{
							case UploadControllerAction.Aborted:
								httpResult.Code = -2;
								httpResult.RefCode = -2;
								httpResult.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is aborted\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
								manualResetEvent.Set();
								return httpResult;
							case UploadControllerAction.Suspended:
								goto IL_028c;
							default:
								continue;
							case UploadControllerAction.Activated:
								break;
							}
							break;
							IL_028c:
							httpResult.RefCode = 1;
							httpResult.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is paused\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
							manualResetEvent.WaitOne(1000);
						}
						long offset = num4 * 4194304;
						stream.Seek(offset, SeekOrigin.Begin);
						int num5 = stream.Read(array, 0, 4194304);
						byte[] array2 = new byte[num5];
						Array.Copy(array, array2, num5);
						dictionary.Add(num4, array2);
						if (dictionary.Count != putExtra.BlockUploadThreads)
						{
							continue;
						}
						processMakeBlocks(dictionary, upToken, putExtra, resumeInfo, dictionary2, dictionary3, length);
						foreach (long key2 in dictionary2.Keys)
						{
							int num6 = (int)key2;
							HttpResult httpResult2 = dictionary2[num6];
							if (httpResult2.Code != 200)
							{
								httpResult = httpResult2;
								manualResetEvent.Set();
								return httpResult;
							}
						}
						dictionary.Clear();
						dictionary2.Clear();
						if (!string.IsNullOrEmpty(putExtra.ResumeRecordFile))
						{
							ResumeHelper.Save(resumeInfo, putExtra.ResumeRecordFile);
						}
					}
					if (dictionary.Count > 0)
					{
						processMakeBlocks(dictionary, upToken, putExtra, resumeInfo, dictionary2, dictionary3, length);
						foreach (long key3 in dictionary2.Keys)
						{
							int num7 = (int)key3;
							HttpResult httpResult3 = dictionary2[num7];
							if (httpResult3.Code != 200)
							{
								httpResult = httpResult3;
								manualResetEvent.Set();
								return httpResult;
							}
						}
						dictionary.Clear();
						dictionary2.Clear();
						if (!string.IsNullOrEmpty(putExtra.ResumeRecordFile))
						{
							ResumeHelper.Save(resumeInfo, putExtra.ResumeRecordFile);
						}
					}
					if (uploadControllerAction == UploadControllerAction.Activated)
					{
						HttpResult httpResult4 = await MakeFile(key, length, key, upToken, putExtra, resumeInfo.Contexts);
						if (httpResult4.Code != 200)
						{
							httpResult.Shadow(httpResult4);
							httpResult.RefText += string.Format("[{0}] [ResumableUpload] Error: mkfile: code = {1}, text = {2}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), httpResult4.Code, httpResult4.Text);
						}
						if (File.Exists(putExtra.ResumeRecordFile))
						{
							File.Delete(putExtra.ResumeRecordFile);
						}
						httpResult.Shadow(httpResult4);
						httpResult.RefText += string.Format("[{0}] [ResumableUpload] Uploaded: \"{1}\" ==> \"{2}\"\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), putExtra.ResumeRecordFile, key);
					}
					else
					{
						httpResult.Code = -2;
						httpResult.RefCode = -2;
						httpResult.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is aborted, mkfile\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
					}
					manualResetEvent.Set();
					return httpResult;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("[{0}] [ResumableUpload] Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
					for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
					{
						stringBuilder.Append(ex2.Message + " ");
					}
					stringBuilder.AppendLine();
					httpResult.RefCode = 0;
					httpResult.RefText += stringBuilder.ToString();
				}
			}
			return httpResult;
		}

		private void processMakeBlocks(Dictionary<long, byte[]> blockDataDict, string upToken, PutExtra putExtra, ResumeInfo resumeInfo, Dictionary<long, HttpResult> blockMakeResults, Dictionary<string, long> uploadedBytesDict, long fileSize)
		{
			int count = blockDataDict.Count;
			ManualResetEvent[] array = new ManualResetEvent[count];
			int num = 0;
			object progressLock = new object();
			foreach (long key in blockDataDict.Keys)
			{
				ManualResetEvent doneEvent = (array[num] = new ManualResetEvent(false));
				num++;
				byte[] blockBuffer = blockDataDict[key];
				ResumeBlocker state = new ResumeBlocker(doneEvent, blockBuffer, key, upToken, putExtra, resumeInfo, blockMakeResults, progressLock, uploadedBytesDict, fileSize);
				ThreadPool.QueueUserWorkItem(new WaitCallback(MakeBlock), state);
			}
			try
			{
				WaitHandle.WaitAll(array);
			}
			catch (Exception ex)
			{
				Console.WriteLine("wait all exceptions:" + ex.StackTrace);
			}
		}

		private async void MakeBlock(object resumeBlockerObj)
		{
			ResumeBlocker resumeBlocker = (ResumeBlocker)resumeBlockerObj;
			ManualResetEvent doneEvent = resumeBlocker.DoneEvent;
			Dictionary<long, HttpResult> blockMakeResults = resumeBlocker.BlockMakeResults;
			PutExtra putExtra = resumeBlocker.PutExtra;
			long blockIndex = resumeBlocker.BlockIndex;
			HttpResult httpResult = new HttpResult();
			while (true)
			{
				switch (resumeBlocker.PutExtra.UploadController())
				{
				case UploadControllerAction.Suspended:
					break;
				case UploadControllerAction.Aborted:
					doneEvent.Set();
					httpResult.Code = -2;
					httpResult.RefCode = -2;
					httpResult.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is aborted, mkblk {1}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), blockIndex);
					blockMakeResults.Add(blockIndex, httpResult);
					return;
				default:
				{
					byte[] blockBuffer = resumeBlocker.BlockBuffer;
					int num = blockBuffer.Length;
					string uploadToken = resumeBlocker.UploadToken;
					Dictionary<string, long> uploadedBytesDict = resumeBlocker.UploadedBytesDict;
					long fileSize = resumeBlocker.FileSize;
					object progressLock = resumeBlocker.ProgressLock;
					ResumeInfo resumeInfo = resumeBlocker.ResumeInfo;
					try
					{
						string accessKeyFromUpToken = UpToken.GetAccessKeyFromUpToken(uploadToken);
						string bucketFromUpToken = UpToken.GetBucketFromUpToken(uploadToken);
						if (accessKeyFromUpToken == null || bucketFromUpToken == null)
						{
							httpResult = HttpResult.InvalidToken;
							doneEvent.Set();
							return;
						}
						string arg = await config.UpHost(accessKeyFromUpToken, bucketFromUpToken);
						string url = string.Format("{0}/mkblk/{1}", arg, num);
						string token = string.Format("UpToken {0}", uploadToken);
						using (MemoryStream memoryStream = new MemoryStream(blockBuffer, 0, num))
						{
							byte[] data = memoryStream.ToArray();
							httpResult = await httpManager.PostData(url, data, token);
							if (httpResult.Code == 200)
							{
								ResumeContext resumeContext = JsonConvert.DeserializeObject<ResumeContext>(httpResult.Text);
								if (resumeContext.Crc32 != 0)
								{
									uint crc = resumeContext.Crc32;
									uint num2 = CRC32.CheckSumSlice(blockBuffer, 0, num);
									if (crc != num2)
									{
										httpResult.RefCode = 3;
										httpResult.RefText += string.Format(" CRC32: remote={0}, local={1}\n", crc, num2);
									}
									else
									{
										resumeInfo.Contexts[blockIndex] = resumeContext.Ctx;
										resumeInfo.ExpiredAt = resumeContext.ExpiredAt;
										lock (progressLock)
										{
											uploadedBytesDict["UploadProgress"] += num;
										}
										putExtra.ProgressHandler(uploadedBytesDict["UploadProgress"], fileSize);
									}
								}
								else
								{
									httpResult.RefText += string.Format("[{0}] JSON Decode Error: text = {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), httpResult.Text);
									httpResult.RefCode = 3;
								}
							}
							else
							{
								httpResult.RefCode = 3;
							}
						}
					}
					catch (Exception ex)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("[{0}] mkblk Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
						for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
						{
							stringBuilder.Append(ex2.Message + " ");
						}
						stringBuilder.AppendLine();
						if (ex is QiniuException)
						{
							QiniuException ex3 = (QiniuException)ex;
							httpResult.Code = ex3.HttpResult.Code;
							httpResult.RefCode = ex3.HttpResult.Code;
							httpResult.Text = ex3.HttpResult.Text;
							httpResult.RefText += stringBuilder.ToString();
						}
						else
						{
							httpResult.RefCode = 0;
							httpResult.RefText += stringBuilder.ToString();
						}
					}
					blockMakeResults.Add(blockIndex, httpResult);
					doneEvent.Set();
					return;
				}
				}
				doneEvent.WaitOne(1000);
			}
		}

		private async Cysharp.Threading.Tasks.UniTask<HttpResult> MakeFile(string fileName, long size, string key, string upToken, PutExtra putExtra, string[] contexts)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string text = "fname";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				if (!string.IsNullOrEmpty(fileName))
				{
					text = string.Format("/fname/{0}", Base64.UrlSafeBase64Encode(fileName));
				}
				if (!string.IsNullOrEmpty(putExtra.MimeType))
				{
					text2 = string.Format("/mimeType/{0}", Base64.UrlSafeBase64Encode(putExtra.MimeType));
				}
				if (!string.IsNullOrEmpty(key))
				{
					text3 = string.Format("/key/{0}", Base64.UrlSafeBase64Encode(key));
				}
				if (putExtra.Params != null && putExtra.Params.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (KeyValuePair<string, string> item in putExtra.Params)
					{
						string key2 = item.Key;
						string value = item.Value;
						if (key2.StartsWith("x:") && !string.IsNullOrEmpty(value))
						{
							stringBuilder.AppendFormat("/{0}/{1}", key2, value);
						}
					}
					text4 = stringBuilder.ToString();
				}
				string accessKeyFromUpToken = UpToken.GetAccessKeyFromUpToken(upToken);
				string bucketFromUpToken = UpToken.GetBucketFromUpToken(upToken);
				if (accessKeyFromUpToken == null || bucketFromUpToken == null)
				{
					return HttpResult.InvalidToken;
				}
				string text5 = await config.UpHost(accessKeyFromUpToken, bucketFromUpToken);
				string url = string.Format("{0}/mkfile/{1}{2}{3}{4}{5}", text5, size, text2, text, text3, text4);
				string data = string.Join(",", contexts);
				string token = string.Format("UpToken {0}", upToken);
				httpResult = await httpManager.PostText(url, data, token);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("[{0}] mkfile Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder2.Append(ex2.Message + " ");
				}
				stringBuilder2.AppendLine();
				if (ex is QiniuException)
				{
					QiniuException ex3 = (QiniuException)ex;
					httpResult.Code = ex3.HttpResult.Code;
					httpResult.RefCode = ex3.HttpResult.Code;
					httpResult.Text = ex3.HttpResult.Text;
					httpResult.RefText += stringBuilder2.ToString();
				}
				else
				{
					httpResult.RefCode = 0;
					httpResult.RefText += stringBuilder2.ToString();
				}
			}
			return httpResult;
		}

		public static void DefaultUploadProgressHandler(long uploadedBytes, long totalBytes)
		{
			if (uploadedBytes < totalBytes)
			{
				Console.WriteLine("[{0}] [ResumableUpload] Progress: {1,7:0.000}%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0 * (double)uploadedBytes / (double)totalBytes);
			}
			else
			{
				Console.WriteLine("[{0}] [ResumableUpload] Progress: {1,7:0.000}%\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0);
			}
		}

		public static UploadControllerAction DefaultUploadController()
		{
			return UploadControllerAction.Activated;
		}
	}
}
