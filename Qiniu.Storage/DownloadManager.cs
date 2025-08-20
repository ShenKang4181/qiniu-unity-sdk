using System;
using System.IO;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class DownloadManager
	{
		public static string CreatePrivateUrl(Mac mac, string domain, string fileName, int expireInSeconds = 3600)
		{
			long unixTimestamp = UnixTimestamp.GetUnixTimestamp(expireInSeconds);
			string text = CreatePublishUrl(domain, fileName);
			StringBuilder stringBuilder = new StringBuilder(text);
			if (text.Contains("?"))
			{
				stringBuilder.AppendFormat("&e={0}", unixTimestamp);
			}
			else
			{
				stringBuilder.AppendFormat("?e={0}", unixTimestamp);
			}
			string arg = Auth.CreateDownloadToken(mac, stringBuilder.ToString());
			stringBuilder.AppendFormat("&token={0}", arg);
			return stringBuilder.ToString();
		}

		public static string CreatePublishUrl(string domain, string fileName)
		{
			return string.Format("{0}/{1}", domain, Uri.EscapeUriString(fileName));
		}

		public static HttpResult Download(string url, string saveasFile)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				HttpManager httpManager = new HttpManager(false);
				httpResult = httpManager.Get(url, null, true);
				if (httpResult.Code == 200)
				{
					using (FileStream fileStream = File.Create(saveasFile, httpResult.Data.Length))
					{
						fileStream.Write(httpResult.Data, 0, httpResult.Data.Length);
						fileStream.Flush();
					}
					httpResult.RefText += string.Format("[{0}] [Download] Success: (Remote file) ==> \"{1}\"\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), saveasFile);
				}
				else
				{
					httpResult.RefText += string.Format("[{0}] [Download] Error: code = {1}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), httpResult.Code);
				}
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [Download] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				httpResult.RefCode = 0;
				httpResult.RefText += stringBuilder.ToString();
			}
			return httpResult;
		}
	}
}
