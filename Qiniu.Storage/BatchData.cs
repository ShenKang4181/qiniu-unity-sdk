using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class BatchData
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CError_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CHash_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CFsize_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CMimeType_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CPutTime_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CFileType_003Ek__BackingField;

		[JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
		public string Error
		{
			[CompilerGenerated]
			get
			{
				return _003CError_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CError_003Ek__BackingField = value;
			}
		}

		[JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
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

		[JsonProperty("fsize", NullValueHandling = NullValueHandling.Ignore)]
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

		[JsonProperty("mimeType", NullValueHandling = NullValueHandling.Ignore)]
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

		[JsonProperty("putTime", NullValueHandling = NullValueHandling.Ignore)]
		public long PutTime
		{
			[CompilerGenerated]
			get
			{
				return _003CPutTime_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPutTime_003Ek__BackingField = value;
			}
		}

		[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
		public int FileType
		{
			[CompilerGenerated]
			get
			{
				return _003CFileType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileType_003Ek__BackingField = value;
			}
		}
	}
}
