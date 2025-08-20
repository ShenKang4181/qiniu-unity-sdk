using System;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.CDN
{
	public class CdnManager
	{
		private const string FUSION_API_HOST = "http://fusion.qiniuapi.com";

		private Auth auth;

		private HttpManager httpManager;

		public CdnManager(Mac mac)
		{
			auth = new Auth(mac);
			httpManager = new HttpManager(false);
		}

		private string refreshEntry()
		{
			return string.Format("{0}/v2/tune/refresh", "http://fusion.qiniuapi.com");
		}

		private string prefetchEntry()
		{
			return string.Format("{0}/v2/tune/prefetch", "http://fusion.qiniuapi.com");
		}

		private string bandwidthEntry()
		{
			return string.Format("{0}/v2/tune/bandwidth", "http://fusion.qiniuapi.com");
		}

		private string fluxEntry()
		{
			return string.Format("{0}/v2/tune/flux", "http://fusion.qiniuapi.com");
		}

		private string logListEntry()
		{
			return string.Format("{0}/v2/tune/log/list", "http://fusion.qiniuapi.com");
		}

		public async Cysharp.Threading.Tasks.UniTask<RefreshResult> RefreshUrlsAndDirs(string[] urls, string[] dirs)
		{
			RefreshRequest refreshRequest = new RefreshRequest(urls, dirs);
			RefreshResult refreshResult = new RefreshResult();
			try
			{
				string url = refreshEntry();
				string data = refreshRequest.ToJsonStr();
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.PostJson(url, data, token);
				refreshResult.Shadow(hr);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [refresh] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				refreshResult.RefCode = -4;
				refreshResult.RefText += stringBuilder.ToString();
			}
			return refreshResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<RefreshResult> RefreshUrls(string[] urls)
		{
			return await RefreshUrlsAndDirs(urls, null);
		}

		public async Cysharp.Threading.Tasks.UniTask<RefreshResult> RefreshDirs(string[] dirs)
		{
			return await RefreshUrlsAndDirs(null, dirs);
		}

		public async Cysharp.Threading.Tasks.UniTask<PrefetchResult> PrefetchUrls(string[] urls)
		{
			PrefetchRequest prefetchRequest = new PrefetchRequest();
			prefetchRequest.AddUrls(urls);
			PrefetchResult prefetchResult = new PrefetchResult();
			try
			{
				string url = prefetchEntry();
				string data = prefetchRequest.ToJsonStr();
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.PostJson(url, data, token);
				prefetchResult.Shadow(hr);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [prefetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				prefetchResult.RefCode = -4;
				prefetchResult.RefText += stringBuilder.ToString();
			}
			return prefetchResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<BandwidthResult> GetBandwidthData(string[] domains, string startDate, string endDate, string granularity)
		{
			BandwidthRequest bandwidthRequest = new BandwidthRequest();
			bandwidthRequest.Domains = string.Join(";", domains);
			bandwidthRequest.StartDate = startDate;
			bandwidthRequest.EndDate = endDate;
			bandwidthRequest.Granularity = granularity;
			BandwidthResult bandwidthResult = new BandwidthResult();
			try
			{
				string url = bandwidthEntry();
				string data = bandwidthRequest.ToJsonStr();
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.PostJson(url, data, token);
				bandwidthResult.Shadow(hr);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [bandwidth] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				bandwidthResult.RefCode = -4;
				bandwidthResult.RefText += stringBuilder.ToString();
			}
			return bandwidthResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<FluxResult> GetFluxData(string[] domains, string startDate, string endDate, string granularity)
		{
			FluxRequest fluxRequest = new FluxRequest();
			fluxRequest.Domains = string.Join(";", domains);
			fluxRequest.StartDate = startDate;
			fluxRequest.EndDate = endDate;
			fluxRequest.Granularity = granularity;
			FluxResult fluxResult = new FluxResult();
			try
			{
				string url = fluxEntry();
				string data = fluxRequest.ToJsonStr();
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.PostJson(url, data, token);
				fluxResult.Shadow(hr);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [flux] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				fluxResult.RefCode = -4;
				fluxResult.RefText += stringBuilder.ToString();
			}
			return fluxResult;
		}

		public async Cysharp.Threading.Tasks.UniTask<LogListResult> GetCdnLogList(string[] domains, string day)
		{
			LogListRequest logListRequest = new LogListRequest();
			logListRequest.Domains = string.Join(";", domains);
			logListRequest.Day = day;
			LogListResult logListResult = new LogListResult();
			try
			{
				string url = logListEntry();
				string data = logListRequest.ToJsonStr();
				string token = auth.CreateManageToken(url);
				HttpResult hr = await httpManager.PostJson(url, data, token);
				logListResult.Shadow(hr);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [loglist] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
				for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
				{
					stringBuilder.Append(ex2.Message + " ");
				}
				stringBuilder.AppendLine();
				logListResult.RefCode = -4;
				logListResult.RefText += stringBuilder.ToString();
			}
			return logListResult;
		}

		public static string CreateTimestampAntiLeechUrl(string host, string fileName, string query, string encryptKey, int expireInSeconds)
		{
			string text = UnixTimestamp.GetUnixTimestamp(expireInSeconds).ToString("x");
			string text2 = string.Format("/{0}", Uri.EscapeUriString(fileName));
			string str = string.Format("{0}{1}{2}", encryptKey, text2, text);
			string text3 = Hashing.CalcMD5X(str);
			//string text4 = null;
			if (!string.IsNullOrEmpty(query))
			{
				return string.Format("{0}{1}?{2}&sign={3}&t={4}", host, text2, query, text3, text);
			}
			return string.Format("{0}{1}?sign={2}&t={3}", host, text2, text3, text);
		}
	}
}
