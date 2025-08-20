using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Qiniu.Storage
{
	public class PutExtra
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CResumeRecordFile_003Ek__BackingField;

		public Dictionary<string, string> Params;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CMimeType_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UploadProgressHandler _003CProgressHandler_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UploadController _003CUploadController_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CMaxRetryTimes_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CBlockUploadThreads_003Ek__BackingField;

		public string ResumeRecordFile
		{
			[CompilerGenerated]
			get
			{
				return _003CResumeRecordFile_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CResumeRecordFile_003Ek__BackingField = value;
			}
		}

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

		public UploadProgressHandler ProgressHandler
		{
			[CompilerGenerated]
			get
			{
				return _003CProgressHandler_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CProgressHandler_003Ek__BackingField = value;
			}
		}

		public UploadController UploadController
		{
			[CompilerGenerated]
			get
			{
				return _003CUploadController_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUploadController_003Ek__BackingField = value;
			}
		}

		public int MaxRetryTimes
		{
			[CompilerGenerated]
			get
			{
				return _003CMaxRetryTimes_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMaxRetryTimes_003Ek__BackingField = value;
			}
		}

		public int BlockUploadThreads
		{
			[CompilerGenerated]
			get
			{
				return _003CBlockUploadThreads_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBlockUploadThreads_003Ek__BackingField = value;
			}
		}
	}
}
