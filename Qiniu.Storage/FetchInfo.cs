using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class FetchInfo
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CKey_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CFsize_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CHash_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CMimeType_003Ek__BackingField;

		[JsonProperty("key")]
		public string Key
		{
			[CompilerGenerated]
			get
			{
				return _003CKey_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CKey_003Ek__BackingField = value;
			}
		}

		[JsonProperty("fsize")]
		public long Fsize
		{
			[CompilerGenerated]
			get
			{
				return _003CFsize_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFsize_003Ek__BackingField = value;
			}
		}

		[JsonProperty("hash")]
		public string Hash
		{
			[CompilerGenerated]
			get
			{
				return _003CHash_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CHash_003Ek__BackingField = value;
			}
		}

		[JsonProperty("mimeType")]
		public string MimeType
		{
			[CompilerGenerated]
			get
			{
				return _003CMimeType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMimeType_003Ek__BackingField = value;
			}
		}
	}
}
