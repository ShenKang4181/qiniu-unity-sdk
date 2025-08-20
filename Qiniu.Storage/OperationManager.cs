using System;
using System.IO;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class OperationManager
	{
		private Auth auth;

		private Mac mac;

		private Config config;

		private HttpManager httpManager;

		public OperationManager(Mac mac, Config config)
		{
			this.mac = mac;
			auth = new Auth(mac);
			this.config = config;
			httpManager = new HttpManager(false);
		}

		public PfopResult Pfop(string bucket, string key, string fops, string pipeline, string notifyUrl, bool force)
		{
			PfopResult pfopResult = new PfopResult();
			try
			{
				string url = string.Format("{0}/pfop/", config.ApiHost(mac.AccessKey, bucket));
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("bucket={0}&key={1}&fops={2}", StringHelper.UrlEncode(bucket), StringHelper.UrlEncode(key), StringHelper.UrlEncode(fops));
				if (!string.IsNullOrEmpty(notifyUrl))
				{
					stringBuilder.AppendFormat("&notifyURL={0}", StringHelper.UrlEncode(notifyUrl));
				}
				if (force)
				{
					stringBuilder.Append("&force=1");
				}
				if (!string.IsNullOrEmpty(pipeline))
				{
					stringBuilder.AppendFormat("&pipeline={0}", pipeline);
				}
				byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
				string token = auth.CreateManageToken(url, bytes);
				HttpResult hr = httpManager.PostForm(url, bytes, token);
				pfopResult.Shadow(hr);
			}
			catch (QiniuException ex)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("[{0}] [pfop] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder2.Append(ex2.Message + " ");
				}
				stringBuilder2.AppendLine();
				pfopResult.Code = ex.HttpResult.Code;
				pfopResult.RefCode = ex.HttpResult.Code;
				pfopResult.Text = ex.HttpResult.Text;
				pfopResult.RefText += stringBuilder2.ToString();
			}
			return pfopResult;
		}

		public PfopResult Pfop(string bucket, string key, string[] fops, string pipeline, string notifyUrl, bool force)
		{
			string fops2 = string.Join(";", fops);
			return Pfop(bucket, key, fops2, pipeline, notifyUrl, force);
		}

		public PrefopResult Prefop(string persistentId)
		{
			PrefopResult prefopResult = new PrefopResult();
			string arg = (config.UseHttps ? "https://" : "http://");
			string url = string.Format("{0}{1}/status/get/prefop?id={2}", arg, Config.DefaultApiHost, persistentId);
			HttpManager httpManager = new HttpManager(false);
			HttpResult hr = httpManager.Get(url, null);
			prefopResult.Shadow(hr);
			return prefopResult;
		}

		public HttpResult Dfop(string fop, string uri)
		{
			if (UrlHelper.IsValidUrl(uri))
			{
				return DfopUrl(fop, uri);
			}
			return DfopData(fop, uri);
		}

		public HttpResult DfopText(string fop, string text)
		{
			HttpResult httpResult = new HttpResult();
			string arg = (config.UseHttps ? "https://" : "http://");
			string url = string.Format("{0}{1}/dfop?fop={2}", arg, Config.DefaultApiHost, fop);
			string token = auth.CreateManageToken(url);
			string text2 = HttpManager.CreateFormDataBoundary();
			string text3 = "--" + text2;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(text3);
			stringBuilder.AppendFormat("Content-Type: {0}", ContentType.TEXT_PLAIN);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Content-Disposition: form-data; name=data; filename=text");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(text);
			stringBuilder.AppendLine(text3 + "--");
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			return httpManager.PostMultipart(url, bytes, text2, token, true);
		}

		public HttpResult DfopTextFile(string fop, string textFile)
		{
			HttpResult httpResult = new HttpResult();
			if (File.Exists(textFile))
			{
				httpResult = DfopText(fop, File.ReadAllText(textFile));
			}
			else
			{
				httpResult.RefCode = -3;
				httpResult.RefText = "[dfop-error] File not found: " + textFile;
			}
			return httpResult;
		}

		public HttpResult DfopUrl(string fop, string url)
		{
			HttpResult httpResult = new HttpResult();
			string text = (config.UseHttps ? "https://" : "http://");
			string text2 = StringHelper.UrlEncode(url);
			string url2 = string.Format("{0}{1}/dfop?fop={2}&url={3}", text, Config.DefaultApiHost, fop, text2);
			string token = auth.CreateManageToken(url2);
			return httpManager.Post(url2, token, true);
		}

		public HttpResult DfopData(string fop, string localFile)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				string arg = (config.UseHttps ? "https://" : "http://");
				string url = string.Format("{0}{1}/dfop?fop={2}", arg, Config.DefaultApiHost, fop);
				string token = auth.CreateManageToken(url);
				string text = HttpManager.CreateFormDataBoundary();
				string text2 = "--" + text;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(text2);
				string fileName = Path.GetFileName(localFile);
				stringBuilder.AppendFormat("Content-Type: {0}", ContentType.APPLICATION_OCTET_STREAM);
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"data\"; filename={0}", fileName);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendLine();
				stringBuilder2.AppendLine(text2 + "--");
				byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
				byte[] array = File.ReadAllBytes(localFile);
				byte[] bytes2 = Encoding.UTF8.GetBytes(stringBuilder2.ToString());
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Write(array, 0, array.Length);
				memoryStream.Write(bytes2, 0, bytes2.Length);
				httpResult = httpManager.PostMultipart(url, memoryStream.ToArray(), text, token, true);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				stringBuilder3.AppendFormat("[{0}] [dfop] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder3.Append(ex2.Message + " ");
				}
				stringBuilder3.AppendLine();
				httpResult.RefCode = 0;
				httpResult.RefText += stringBuilder3.ToString();
			}
			return httpResult;
		}
	}
}
