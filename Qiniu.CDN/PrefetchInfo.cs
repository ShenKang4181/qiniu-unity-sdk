using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Qiniu.CDN
{
	public class PrefetchInfo
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CCode_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CError_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CRequestId_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CInvalidUrls_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CQuotaDay_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CSurplusDay_003Ek__BackingField;

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

		public string RequestId
		{
			[CompilerGenerated]
			get
			{
				return _003CRequestId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRequestId_003Ek__BackingField = value;
			}
		}

		public List<string> InvalidUrls
		{
			[CompilerGenerated]
			get
			{
				return _003CInvalidUrls_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CInvalidUrls_003Ek__BackingField = value;
			}
		}

		public int QuotaDay
		{
			[CompilerGenerated]
			get
			{
				return _003CQuotaDay_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CQuotaDay_003Ek__BackingField = value;
			}
		}

		public int SurplusDay
		{
			[CompilerGenerated]
			get
			{
				return _003CSurplusDay_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CSurplusDay_003Ek__BackingField = value;
			}
		}
	}
}
