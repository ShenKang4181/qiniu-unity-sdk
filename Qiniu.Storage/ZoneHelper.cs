using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
	public class ZoneHelper
	{
		private static Dictionary<string, Zone> zoneCache = new Dictionary<string, Zone>();

		private static object rwLock = new object();

		public static async Cysharp.Threading.Tasks.UniTask<Zone> QueryZone(string accessKey, string bucket)
		{
			Zone zone = null;
			string key = string.Format("{0}:{1}", accessKey, bucket);
			lock (rwLock)
			{
				if (zoneCache.ContainsKey(key))
				{
					zone = zoneCache[key];
				}
			}
			if (zone != null)
			{
				return zone;
			}
			HttpResult httpResult = null;
			try
			{
				string url = string.Format("https://uc.qbox.me/v2/query?ak={0}&bucket={1}", accessKey, bucket);
				HttpManager httpManager = new HttpManager(false);
				httpResult = await httpManager.Get(url, null);
				if (httpResult.Code != 200)
				{
					throw new Exception("code: " + httpResult.Code + ", text: " + httpResult.Text + ", ref-text:" + httpResult.RefText);
				}
				ZoneInfo zoneInfo = JsonConvert.DeserializeObject<ZoneInfo>(httpResult.Text);
				if (zoneInfo == null)
				{
					throw new Exception("JSON Deserialize failed: " + httpResult.Text);
				}
				zone = new Zone();
				zone.SrcUpHosts = zoneInfo.Up.Src.Main;
				zone.CdnUpHosts = zoneInfo.Up.Acc.Main;
				zone.IovipHost = zoneInfo.Io.Src.Main[0];
				if (zone.IovipHost.Contains("z1"))
				{
					zone.ApiHost = "api-z1.qiniu.com";
					zone.RsHost = "rs-z1.qiniu.com";
					zone.RsfHost = "rsf-z1.qiniu.com";
				}
				else if (zone.IovipHost.Contains("z2"))
				{
					zone.ApiHost = "api-z2.qiniu.com";
					zone.RsHost = "rs-z2.qiniu.com";
					zone.RsfHost = "rsf-z2.qiniu.com";
				}
				else if (zone.IovipHost.Contains("na0"))
				{
					zone.ApiHost = "api-na0.qiniu.com";
					zone.RsHost = "rs-na0.qiniu.com";
					zone.RsfHost = "rsf-na0.qiniu.com";
				}
				else if (zone.IovipHost.Contains("as0"))
				{
					zone.ApiHost = "api-as0.qiniu.com";
					zone.RsHost = "rs-as0.qiniu.com";
					zone.RsfHost = "rsf-as0.qiniu.com";
				}
				else
				{
					zone.ApiHost = "api.qiniu.com";
					zone.RsHost = "rs.qiniu.com";
					zone.RsfHost = "rsf.qiniu.com";
				}
				lock (rwLock)
				{
					zoneCache[key] = zone;
				}
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] QueryZone Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				throw new QiniuException(httpResult, stringBuilder.ToString());
			}
			return zone;
		}
	}
}
