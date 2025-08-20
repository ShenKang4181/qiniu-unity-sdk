namespace Qiniu.Storage
{
	public class Config
	{
		public static string DefaultRsHost = "rs.qiniu.com";

		public static string DefaultApiHost = "api.qiniu.com";

		public Zone Zone = null;

		public bool UseHttps = false;

		public bool UseCdnDomains = false;

		public ChunkUnit ChunkSize = ChunkUnit.U4096K;

		public int PutThreshold = ResumeChunk.GetChunkSize(ChunkUnit.U1024K) * 10;

		public int MaxRetryTimes = 3;

		public async Cysharp.Threading.Tasks.UniTask<string> RsHost(string ak, string bucket)
		{
			string arg = (UseHttps ? "https://" : "http://");
			Zone zone = Zone;
			if (zone == null)
			{
				zone = await ZoneHelper.QueryZone(ak, bucket);
			}
			return string.Format("{0}{1}", arg, zone.RsHost);
		}

		public async Cysharp.Threading.Tasks.UniTask<string> RsfHost(string ak, string bucket)
		{
			string arg = (UseHttps ? "https://" : "http://");
			Zone zone = Zone;
			if (zone == null)
			{
				zone = await ZoneHelper.QueryZone(ak, bucket);
			}
			return string.Format("{0}{1}", arg, zone.RsfHost);
		}

		public async Cysharp.Threading.Tasks.UniTask<string> ApiHost(string ak, string bucket)
		{
			string arg = (UseHttps ? "https://" : "http://");
			Zone zone = Zone;
			if (zone == null)
			{
				zone = await ZoneHelper.QueryZone(ak, bucket);
			}
			return string.Format("{0}{1}", arg, zone.ApiHost);
		}

		public async Cysharp.Threading.Tasks.UniTask<string> IovipHost(string ak, string bucket)
		{
			string arg = (UseHttps ? "https://" : "http://");
			Zone zone = Zone;
			if (zone == null)
			{
				zone = await ZoneHelper.QueryZone(ak, bucket);
			}
			return string.Format("{0}{1}", arg, zone.IovipHost);
		}

		public async Cysharp.Threading.Tasks.UniTask<string> UpHost(string ak, string bucket)
		{
			string arg = (UseHttps ? "https://" : "http://");
			Zone zone = Zone;
			if (zone == null)
			{
				zone = await ZoneHelper.QueryZone(ak, bucket);
			}
			string arg2 = zone.SrcUpHosts[0];
			if (UseCdnDomains)
			{
				arg2 = zone.CdnUpHosts[0];
			}
			return string.Format("{0}{1}", arg, arg2);
		}
	}
}
