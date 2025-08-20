using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Qiniu.Http;

namespace Qiniu.Storage
{
	internal class ResumeBlocker
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ManualResetEvent _003CDoneEvent_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte[] _003CBlockBuffer_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CBlockIndex_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CUploadToken_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PutExtra _003CPutExtra_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ResumeInfo _003CResumeInfo_003Ek__BackingField;

		public Dictionary<long, HttpResult> BlockMakeResults;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object _003CProgressLock_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, long> _003CUploadedBytesDict_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long _003CFileSize_003Ek__BackingField;

		public ManualResetEvent DoneEvent
		{
			[CompilerGenerated]
			get
			{
				return _003CDoneEvent_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDoneEvent_003Ek__BackingField = value;
			}
		}

		public byte[] BlockBuffer
		{
			[CompilerGenerated]
			get
			{
				return _003CBlockBuffer_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBlockBuffer_003Ek__BackingField = value;
			}
		}

		public long BlockIndex
		{
			[CompilerGenerated]
			get
			{
				return _003CBlockIndex_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBlockIndex_003Ek__BackingField = value;
			}
		}

		public string UploadToken
		{
			[CompilerGenerated]
			get
			{
				return _003CUploadToken_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUploadToken_003Ek__BackingField = value;
			}
		}

		public PutExtra PutExtra
		{
			[CompilerGenerated]
			get
			{
				return _003CPutExtra_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPutExtra_003Ek__BackingField = value;
			}
		}

		public ResumeInfo ResumeInfo
		{
			[CompilerGenerated]
			get
			{
				return _003CResumeInfo_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CResumeInfo_003Ek__BackingField = value;
			}
		}

		public object ProgressLock
		{
			[CompilerGenerated]
			get
			{
				return _003CProgressLock_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CProgressLock_003Ek__BackingField = value;
			}
		}

		public Dictionary<string, long> UploadedBytesDict
		{
			[CompilerGenerated]
			get
			{
				return _003CUploadedBytesDict_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUploadedBytesDict_003Ek__BackingField = value;
			}
		}

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

		public ResumeBlocker(ManualResetEvent doneEvent, byte[] blockBuffer, long blockIndex, string uploadToken, PutExtra putExtra, ResumeInfo resumeInfo, Dictionary<long, HttpResult> blockMakeResults, object progressLock, Dictionary<string, long> uploadedBytesDict, long fileSize)
		{
			DoneEvent = doneEvent;
			BlockBuffer = blockBuffer;
			BlockIndex = blockIndex;
			UploadToken = uploadToken;
			PutExtra = putExtra;
			ResumeInfo = resumeInfo;
			BlockMakeResults = blockMakeResults;
			ProgressLock = progressLock;
			UploadedBytesDict = uploadedBytesDict;
			FileSize = fileSize;
		}
	}
}
