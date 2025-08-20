using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Qiniu.Storage
{
	public class Zone
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CRsHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CRsfHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CApiHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CIovipHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string[] _003CSrcUpHosts_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string[] _003CCdnUpHosts_003Ek__BackingField;

		public static Zone ZONE_CN_East;

		public static Zone ZONE_CN_North;

		public static Zone ZONE_CN_South;

		public static Zone ZONE_US_North;

		public static Zone ZONE_AS_Singapore;

		public string RsHost
		{
			[CompilerGenerated]
			get
			{
				return _003CRsHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRsHost_003Ek__BackingField = value;
			}
		}

		public string RsfHost
		{
			[CompilerGenerated]
			get
			{
				return _003CRsfHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRsfHost_003Ek__BackingField = value;
			}
		}

		public string ApiHost
		{
			[CompilerGenerated]
			get
			{
				return _003CApiHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CApiHost_003Ek__BackingField = value;
			}
		}

		public string IovipHost
		{
			[CompilerGenerated]
			get
			{
				return _003CIovipHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIovipHost_003Ek__BackingField = value;
			}
		}

		public string[] SrcUpHosts
		{
			[CompilerGenerated]
			get
			{
				return _003CSrcUpHosts_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CSrcUpHosts_003Ek__BackingField = value;
			}
		}

		public string[] CdnUpHosts
		{
			[CompilerGenerated]
			get
			{
				return _003CCdnUpHosts_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCdnUpHosts_003Ek__BackingField = value;
			}
		}

		static Zone()
		{
			Zone zone = new Zone();
			zone.RsHost = "rs.qiniu.com";
			zone.RsfHost = "rsf.qiniu.com";
			zone.ApiHost = "api.qiniu.com";
			zone.IovipHost = "iovip.qbox.me";
			zone.SrcUpHosts = new string[3] { "up.qiniup.com", "up-nb.qiniup.com", "up-xs.qiniup.com" };
			zone.CdnUpHosts = new string[3] { "upload.qiniup.com", "upload-nb.qiniup.com", "upload-xs.qiniup.com" };
			ZONE_CN_East = zone;
			zone = new Zone();
			zone.RsHost = "rs-z1.qiniu.com";
			zone.RsfHost = "rsf-z1.qiniu.com";
			zone.ApiHost = "api-z1.qiniu.com";
			zone.IovipHost = "iovip-z1.qbox.me";
			zone.SrcUpHosts = new string[1] { "up-z1.qiniup.com" };
			zone.CdnUpHosts = new string[1] { "upload-z1.qiniup.com" };
			ZONE_CN_North = zone;
			zone = new Zone();
			zone.RsHost = "rs-z2.qiniu.com";
			zone.RsfHost = "rsf-z2.qiniu.com";
			zone.ApiHost = "api-z2.qiniu.com";
			zone.IovipHost = "iovip-z2.qbox.me";
			zone.SrcUpHosts = new string[3] { "up-z2.qiniup.com", "up-gz.qiniup.com", "up-fs.qiniup.com" };
			zone.CdnUpHosts = new string[3] { "upload-z2.qiniup.com", "upload-gz.qiniup.com", "upload-fs.qiniup.com" };
			ZONE_CN_South = zone;
			zone = new Zone();
			zone.RsHost = "rs-na0.qiniu.com";
			zone.RsfHost = "rsf-na0.qiniu.com";
			zone.ApiHost = "api-na0.qiniu.com";
			zone.IovipHost = "iovip-na0.qbox.me";
			zone.SrcUpHosts = new string[1] { "up-na0.qiniup.com" };
			zone.CdnUpHosts = new string[1] { "upload-na0.qiniup.com" };
			ZONE_US_North = zone;
			zone = new Zone();
			zone.RsHost = "rs-as0.qiniu.com";
			zone.RsfHost = "rsf-as0.qiniu.com";
			zone.ApiHost = "api-as0.qiniu.com";
			zone.IovipHost = "iovip-as0.qbox.me";
			zone.SrcUpHosts = new string[1] { "up-as0.qiniup.com" };
			zone.CdnUpHosts = new string[1] { "upload-as0.qiniup.com" };
			ZONE_AS_Singapore = zone;
		}
	}
}
