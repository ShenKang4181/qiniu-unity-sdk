using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class FormUploader
	{
		private Config config;

		private HttpManager httpManager;

		public FormUploader(Config config)
		{
			this.config = config;
			httpManager = new HttpManager(false);
		}

		public HttpResult UploadFile(string localFile, string key, string token, PutExtra extra)
		{
			try
			{
				FileStream stream = new FileStream(localFile, FileMode.Open);
				return UploadStream(stream, key, token, extra);
			}
			catch (Exception ex)
			{
				HttpResult invalidFile = HttpResult.InvalidFile;
				invalidFile.RefText = ex.Message;
				return invalidFile;
			}
		}

		public HttpResult UploadData(byte[] data, string key, string token, PutExtra extra)
		{
			MemoryStream stream = new MemoryStream(data);
			return UploadStream(stream, key, token, extra);
		}

		public HttpResult UploadStream(Stream stream, string key, string token, PutExtra putExtra)
		{
			if (putExtra == null)
			{
				putExtra = new PutExtra();
			}
			if (string.IsNullOrEmpty(putExtra.MimeType))
			{
				putExtra.MimeType = "application/octet-stream";
			}
			if (putExtra.ProgressHandler == null)
			{
				putExtra.ProgressHandler = new UploadProgressHandler(DefaultUploadProgressHandler);
			}
			if (putExtra.UploadController == null)
			{
				putExtra.UploadController = new UploadController(DefaultUploadController);
			}
			string arg = key;
			if (string.IsNullOrEmpty(key))
			{
				arg = "fname_temp";
			}
			HttpResult httpResult = new HttpResult();
			using (stream)
			{
				try
				{
					string text = HttpManager.CreateFormDataBoundary();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("--" + text);
					if (key != null)
					{
						stringBuilder.AppendLine("Content-Disposition: form-data; name=\"key\"");
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(key);
						stringBuilder.AppendLine("--" + text);
					}
					stringBuilder.AppendLine("Content-Disposition: form-data; name=\"token\"");
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(token);
					stringBuilder.AppendLine("--" + text);
					if (putExtra.Params != null && putExtra.Params.Count > 0)
					{
						foreach (KeyValuePair<string, string> item in putExtra.Params)
						{
							if (item.Key.StartsWith("x:"))
							{
								stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", item.Key);
								stringBuilder.AppendLine();
								stringBuilder.AppendLine();
								stringBuilder.AppendLine(item.Value);
								stringBuilder.AppendLine("--" + text);
							}
						}
					}
					int num = 1048576;
					byte[] buffer = new byte[num];
					int num2 = 0;
					putExtra.ProgressHandler(0L, stream.Length);
					MemoryStream memoryStream = new MemoryStream();
					while ((num2 = stream.Read(buffer, 0, num)) != 0)
					{
						memoryStream.Write(buffer, 0, num2);
					}
					uint num3 = CRC32.CheckSumBytes(memoryStream.ToArray());
					stringBuilder.AppendLine("Content-Disposition: form-data; name=\"crc32\"");
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(num3.ToString());
					stringBuilder.AppendLine("--" + text);
					stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"", arg);
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("Content-Type: {0}", putExtra.MimeType);
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.AppendLine();
					stringBuilder2.AppendLine("--" + text + "--");
					byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
					byte[] array = memoryStream.ToArray();
					byte[] bytes2 = Encoding.UTF8.GetBytes(stringBuilder2.ToString());
					MemoryStream memoryStream2 = new MemoryStream();
					memoryStream2.Write(bytes, 0, bytes.Length);
					memoryStream2.Write(array, 0, array.Length);
					memoryStream2.Write(bytes2, 0, bytes2.Length);
					string accessKeyFromUpToken = UpToken.GetAccessKeyFromUpToken(token);
					string bucketFromUpToken = UpToken.GetBucketFromUpToken(token);
					if (accessKeyFromUpToken == null || bucketFromUpToken == null)
					{
						return HttpResult.InvalidToken;
					}
					string url = config.UpHost(accessKeyFromUpToken, bucketFromUpToken);
					putExtra.ProgressHandler(stream.Length / 5, stream.Length);
					httpResult = httpManager.PostMultipart(url, memoryStream2.ToArray(), text, null);
					putExtra.ProgressHandler(stream.Length, stream.Length);
					if (httpResult.Code == 200)
					{
						httpResult.RefText += string.Format("[{0}] [FormUpload] Uploaded: #STREAM# ==> \"{1}\"\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), key);
					}
					else
					{
						httpResult.RefText += string.Format("[{0}] [FormUpload] Failed: code = {1}, text = {2}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), httpResult.Code, httpResult.Text);
					}
					memoryStream2.Close();
					memoryStream.Close();
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder3 = new StringBuilder();
					stringBuilder3.AppendFormat("[{0}] [FormUpload] Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
					for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
					{
						stringBuilder3.Append(ex2.Message + " ");
					}
					stringBuilder3.AppendLine();
					if (ex is QiniuException)
					{
						QiniuException ex3 = (QiniuException)ex;
						httpResult.Code = ex3.HttpResult.Code;
						httpResult.RefCode = ex3.HttpResult.Code;
						httpResult.Text = ex3.HttpResult.Text;
						httpResult.RefText += stringBuilder3.ToString();
					}
					else
					{
						httpResult.RefCode = 0;
						httpResult.RefText += stringBuilder3.ToString();
					}
				}
			}
			return httpResult;
		}

		public static void DefaultUploadProgressHandler(long uploadedBytes, long totalBytes)
		{
			if (uploadedBytes < totalBytes)
			{
				Console.WriteLine("[{0}] [FormUpload] Progress: {1,7:0.000}%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0 * (double)uploadedBytes / (double)totalBytes);
			}
			else
			{
				Console.WriteLine("[{0}] [FormUpload] Progress: {1,7:0.000}%\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0);
			}
		}

		public static UploadControllerAction DefaultUploadController()
		{
			return UploadControllerAction.Activated;
		}
	}
}
