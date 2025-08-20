using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class BatchInfo
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CCode_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BatchData _003CData_003Ek__BackingField;

		[JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
		public int Code
		{
			[CompilerGenerated]
			get
			{
				return _003CCode_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCode_003Ek__BackingField = value;
			}
		}

		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public BatchData Data
		{
			[CompilerGenerated]
			get
			{
				return _003CData_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CData_003Ek__BackingField = value;
			}
		}
	}
}
