using System;
using System.Collections.Generic;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class BucketManager
	{
		private Mac mac;

		private Auth auth;

		private HttpManager httpManager;

		private Config config;

		public BucketManager(Mac mac, Config config)
		{
			this.mac = mac;
			auth = new Auth(mac);
			httpManager = new HttpManager(false);
			this.config = config;
		}

		public async Cysharp.Threading.Tasks.UniTask<StatResult> Stat(string bucket, string key)
		{
			StatResult statResult = new StatResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, bucket), StatOp(bucket, key));
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.Get(url, token);
				statResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [stat] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				statResult.Code = ex.HttpResult.Code;
				statResult.RefCode = ex.HttpResult.Code;
				statResult.Text = ex.HttpResult.Text;
				statResult.RefText += stringBuilder.ToString();
			}
			return statResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<BucketsResult> Buckets(bool shared)
		{
			BucketsResult bucketsResult = new BucketsResult();
			try
			{
				string arg = (config.UseHttps ? "https://" : "http://");
				string arg2 = string.Format("{0}{1}", arg, Config.DefaultRsHost);
				string arg3 = "false";
				if (shared)
				{
					arg3 = "true";
				}
				string url = string.Format("{0}/buckets?shared={1}", arg2, arg3);
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.Get(url, token);
				bucketsResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [buckets] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				bucketsResult.Code = ex.HttpResult.Code;
				bucketsResult.RefCode = ex.HttpResult.Code;
				bucketsResult.Text = ex.HttpResult.Text;
				bucketsResult.RefText += stringBuilder.ToString();
			}
			return bucketsResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Delete(string bucket, string key)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, bucket), DeleteOp(bucket, key));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [delete] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Copy(string srcBucket, string srcKey, string dstBucket, string dstKey)
		{
			return await Copy(srcBucket, srcKey, dstBucket, dstKey, false);
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Copy(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, srcBucket), CopyOp(srcBucket, srcKey, dstBucket, dstKey, force));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [copy] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Move(string srcBucket, string srcKey, string dstBucket, string dstKey)
		{
			return await Move(srcBucket, srcKey, dstBucket, dstKey, false);
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Move(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, srcBucket), MoveOp(srcBucket, srcKey, dstBucket, dstKey, force));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [move] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> ChangeMime(string bucket, string key, string mimeType)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, bucket), ChangeMimeOp(bucket, key, mimeType));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [chgm] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> ChangeType(string bucket, string key, int fileType)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, bucket), ChangeTypeOp(bucket, key, fileType));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [chtype] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		private async Cysharp.Threading.Tasks.UniTask<BatchResult> Batch(string batchOps)
		{
			BatchResult batchResult = new BatchResult();
			try
			{
				string arg = (config.UseHttps ? "https://" : "http://");
				string text = string.Format("{0}{1}", arg, Config.DefaultRsHost);
				string url = text + "/batch";
				byte[] bytes = Encoding.UTF8.GetBytes(batchOps);
				string token = auth.CreateManageToken(url, bytes);
				HttpResult hr = await httpManager.PostForm(url, bytes, token);
				batchResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [batch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				batchResult.Code = ex.HttpResult.Code;
				batchResult.RefCode = ex.HttpResult.Code;
				batchResult.Text = ex.HttpResult.Text;
				batchResult.RefText += stringBuilder.ToString();
			}
			return batchResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<BatchResult> Batch(IList<string> ops)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("op={0}", ops[0]);
			for (int i = 1; i < ops.Count; i++)
			{
				stringBuilder.AppendFormat("&op={0}", ops[i]);
			}
			return await Batch(stringBuilder.ToString());
		}

		public async Cysharp.Threading.Tasks.UniTask<FetchResult> Fetch(string resUrl, string bucket, string key)
		{
			FetchResult fetchResult = new FetchResult();
			try
			{
				string url = string.Format("{0}{1}", await config.IovipHost(mac.AccessKey, bucket), FetchOp(resUrl, bucket, key));
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.Post(url, token);
				fetchResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [fetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				fetchResult.Code = ex.HttpResult.Code;
				fetchResult.RefCode = ex.HttpResult.Code;
				fetchResult.Text = ex.HttpResult.Text;
				fetchResult.RefText += stringBuilder.ToString();
			}
			return fetchResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Prefetch(string bucket, string key)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = await config.IovipHost(mac.AccessKey, bucket) + PrefetchOp(bucket, key);
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [prefetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<DomainsResult> Domains(string bucket)
		{
			DomainsResult domainsResult = new DomainsResult();
			try
			{
				string arg = (config.UseHttps ? "https://" : "http://");
				string arg2 = string.Format("{0}{1}", arg, Config.DefaultApiHost);
				string url = string.Format("{0}{1}", arg2, "/v6/domain/list");
				string s = string.Format("tbl={0}", bucket);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				string token = auth.CreateManageToken(url, bytes);
				HttpResult hr = await httpManager.PostForm(url, bytes, token);
				domainsResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [domains] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				domainsResult.Code = ex.HttpResult.Code;
				domainsResult.RefCode = ex.HttpResult.Code;
				domainsResult.Text = ex.HttpResult.Text;
				domainsResult.RefText += stringBuilder.ToString();
			}
			return domainsResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<ListResult> ListFiles(string bucket, string prefix, string marker, int limit, string delimiter)
		{
			ListResult listResult = new ListResult();
			try
			{
				StringBuilder stringBuilder = new StringBuilder("/list?bucket=" + bucket);
				if (!string.IsNullOrEmpty(marker))
				{
					stringBuilder.Append("&marker=" + marker);
				}
				if (!string.IsNullOrEmpty(prefix))
				{
					stringBuilder.Append("&prefix=" + prefix);
				}
				if (!string.IsNullOrEmpty(delimiter))
				{
					stringBuilder.Append("&delimiter=" + delimiter);
				}
				if (limit > 1000 || limit < 1)
				{
					stringBuilder.Append("&limit=1000");
				}
				else
				{
					stringBuilder.Append("&limit=" + limit);
				}
				string url = string.Format("{0}{1}", await config.RsfHost(mac.AccessKey, bucket), stringBuilder.ToString());
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.Post(url, token);
				listResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("[{0}] [listFiles] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder2.Append(ex2.Message + " ");
				}
				stringBuilder2.AppendLine();
				listResult.Code = ex.HttpResult.Code;
				listResult.RefCode = ex.HttpResult.Code;
				listResult.Text = ex.HttpResult.Text;
				listResult.RefText += stringBuilder2.ToString();
			}
			return listResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> DeleteAfterDays(string bucket, string key, int deleteAfterDays)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string url = string.Format("{0}{1}", await config.RsHost(mac.AccessKey, bucket), DeleteAfterDaysOp(bucket, key, deleteAfterDays));
				string token = auth.CreateManageToken(url);
				httpResult = await httpManager.Post(url, token);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [deleteAfterDays] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.Code = ex.HttpResult.Code;
				httpResult.RefCode = ex.HttpResult.Code;
				httpResult.Text = ex.HttpResult.Text;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}

		public string StatOp(string bucket, string key)
		{
			return string.Format("/stat/{0}", Base64.UrlSafeBase64Encode(bucket, key));
		}

		public string DeleteOp(string bucket, string key)
		{
			return string.Format("/delete/{0}", Base64.UrlSafeBase64Encode(bucket, key));
		}

		public string CopyOp(string srcBucket, string srcKey, string dstBucket, string dstKey)
		{
			return CopyOp(srcBucket, srcKey, dstBucket, dstKey, false);
		}

		public string CopyOp(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
		{
			string arg = (force ? "force/true" : "force/false");
			return string.Format("/copy/{0}/{1}/{2}", Base64.UrlSafeBase64Encode(srcBucket, srcKey), Base64.UrlSafeBase64Encode(dstBucket, dstKey), arg);
		}

		public string MoveOp(string srcBucket, string srcKey, string dstBucket, string dstKey)
		{
			return MoveOp(srcBucket, srcKey, dstBucket, dstKey, false);
		}

		public string MoveOp(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
		{
			string arg = (force ? "force/true" : "force/false");
			return string.Format("/move/{0}/{1}/{2}", Base64.UrlSafeBase64Encode(srcBucket, srcKey), Base64.UrlSafeBase64Encode(dstBucket, dstKey), arg);
		}

		public string ChangeMimeOp(string bucket, string key, string mimeType)
		{
			return string.Format("/chgm/{0}/mime/{1}", Base64.UrlSafeBase64Encode(bucket, key), Base64.UrlSafeBase64Encode(mimeType));
		}

		public string ChangeTypeOp(string bucket, string key, int fileType)
		{
			return string.Format("/chtype/{0}/type/{1}", Base64.UrlSafeBase64Encode(bucket, key), fileType);
		}

		public string FetchOp(string url, string bucket, string key)
		{
			string text = null;
			text = ((key != null) ? Base64.UrlSafeBase64Encode(bucket, key) : Base64.UrlSafeBase64Encode(bucket));
			return string.Format("/fetch/{0}/to/{1}", Base64.UrlSafeBase64Encode(url), text);
		}

		public string PrefetchOp(string bucket, string key)
		{
			return string.Format("/prefetch/{0}", Base64.UrlSafeBase64Encode(bucket, key));
		}

		public string DeleteAfterDaysOp(string bucket, string key, int deleteAfterDays)
		{
			return string.Format("/deleteAfterDays/{0}/{1}", Base64.UrlSafeBase64Encode(bucket, key), deleteAfterDays);
		}
	}
}
