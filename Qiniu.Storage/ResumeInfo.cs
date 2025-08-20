using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class ResumeInfo
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CFileSize_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CBlockCount_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string[] _003CContexts_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CExpiredAt_003Ek__BackingField;

		[JsonProperty("fileSize")]
		public long FileSize
		{
			[CompilerGenerated]
			get
			{
				return _003CFileSize_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileSize_003Ek__BackingField = value;
			}
		}

		[JsonProperty("blockCount")]
		public long BlockCount
		{
			[CompilerGenerated]
			get
			{
				return _003CBlockCount_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBlockCount_003Ek__BackingField = value;
			}
		}

		[JsonProperty("contexts")]
		public string[] Contexts
		{
			[CompilerGenerated]
			get
			{
				return _003CContexts_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CContexts_003Ek__BackingField = value;
			}
		}

		[JsonProperty("expiredAt")]
		public long ExpiredAt
		{
			[CompilerGenerated]
			get
			{
				return _003CExpiredAt_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CExpiredAt_003Ek__BackingField = value;
			}
		}

		public string ToJsonStr()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
