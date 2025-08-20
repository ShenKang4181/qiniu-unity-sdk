using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class ResumeContext
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CCtx_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CChecksum_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint _003CCrc32_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003COffset_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CExpiredAt_003Ek__BackingField;

		[JsonProperty("ctx")]
		public string Ctx
		{
			[CompilerGenerated]
			get
			{
				return _003CCtx_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCtx_003Ek__BackingField = value;
			}
		}

		[JsonProperty("checksum")]
		public string Checksum
		{
			[CompilerGenerated]
			get
			{
				return _003CChecksum_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CChecksum_003Ek__BackingField = value;
			}
		}

		[JsonProperty("crc32")]
		public uint Crc32
		{
			[CompilerGenerated]
			get
			{
				return _003CCrc32_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCrc32_003Ek__BackingField = value;
			}
		}

		[JsonProperty("offset")]
		public long Offset
		{
			[CompilerGenerated]
			get
			{
				return _003COffset_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003COffset_003Ek__BackingField = value;
			}
		}

		[JsonProperty("host")]
		public string Host
		{
			[CompilerGenerated]
			get
			{
				return _003CHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CHost_003Ek__BackingField = value;
			}
		}

		[JsonProperty("expired_at")]
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
	}
}
