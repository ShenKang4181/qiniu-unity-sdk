using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class PutPolicy
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CScope_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CisPrefixalScope_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CDeadline_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CInsertOnly_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CSaveKey_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CEndUser_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CReturnUrl_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CReturnBody_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CCallbackUrl_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CCallbackBody_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CCallbackBodyType_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CCallbackHost_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CCallbackFetchKey_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CPersistentOps_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CPersistentNotifyUrl_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CPersistentPipeline_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CFsizeMin_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CFsizeLimit_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CDetectMime_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CMimeLimit_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CDeleteAfterDays_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Nullable<int> _003CFileType_003Ek__BackingField;

		[JsonProperty("scope")]
		public string Scope
		{
			[CompilerGenerated]
			get
			{
				return _003CScope_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CScope_003Ek__BackingField = value;
			}
		}

		[JsonProperty("isPrefixalScope", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> isPrefixalScope
		{
			[CompilerGenerated]
			get
			{
				return _003CisPrefixalScope_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CisPrefixalScope_003Ek__BackingField = value;
			}
		}

		[JsonProperty("deadline")]
		public int Deadline
		{
			[CompilerGenerated]
			get
			{
				return _003CDeadline_003Ek__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				_003CDeadline_003Ek__BackingField = value;
			}
		}

		[JsonProperty("insertOnly", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> InsertOnly
		{
			[CompilerGenerated]
			get
			{
				return _003CInsertOnly_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CInsertOnly_003Ek__BackingField = value;
			}
		}

		[JsonProperty("saveKey", NullValueHandling = NullValueHandling.Ignore)]
		public string SaveKey
		{
			[CompilerGenerated]
			get
			{
				return _003CSaveKey_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CSaveKey_003Ek__BackingField = value;
			}
		}

		[JsonProperty("endUser", NullValueHandling = NullValueHandling.Ignore)]
		public string EndUser
		{
			[CompilerGenerated]
			get
			{
				return _003CEndUser_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CEndUser_003Ek__BackingField = value;
			}
		}

		[JsonProperty("returnUrl", NullValueHandling = NullValueHandling.Ignore)]
		public string ReturnUrl
		{
			[CompilerGenerated]
			get
			{
				return _003CReturnUrl_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CReturnUrl_003Ek__BackingField = value;
			}
		}

		[JsonProperty("returnBody", NullValueHandling = NullValueHandling.Ignore)]
		public string ReturnBody
		{
			[CompilerGenerated]
			get
			{
				return _003CReturnBody_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CReturnBody_003Ek__BackingField = value;
			}
		}

		[JsonProperty("callbackUrl", NullValueHandling = NullValueHandling.Ignore)]
		public string CallbackUrl
		{
			[CompilerGenerated]
			get
			{
				return _003CCallbackUrl_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCallbackUrl_003Ek__BackingField = value;
			}
		}

		[JsonProperty("callbackBody", NullValueHandling = NullValueHandling.Ignore)]
		public string CallbackBody
		{
			[CompilerGenerated]
			get
			{
				return _003CCallbackBody_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCallbackBody_003Ek__BackingField = value;
			}
		}

		[JsonProperty("callbackBodyType", NullValueHandling = NullValueHandling.Ignore)]
		public string CallbackBodyType
		{
			[CompilerGenerated]
			get
			{
				return _003CCallbackBodyType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCallbackBodyType_003Ek__BackingField = value;
			}
		}

		[JsonProperty("callbackHost", NullValueHandling = NullValueHandling.Ignore)]
		public string CallbackHost
		{
			[CompilerGenerated]
			get
			{
				return _003CCallbackHost_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCallbackHost_003Ek__BackingField = value;
			}
		}

		[JsonProperty("callbackFetchKey", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> CallbackFetchKey
		{
			[CompilerGenerated]
			get
			{
				return _003CCallbackFetchKey_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCallbackFetchKey_003Ek__BackingField = value;
			}
		}

		[JsonProperty("persistentOps", NullValueHandling = NullValueHandling.Ignore)]
		public string PersistentOps
		{
			[CompilerGenerated]
			get
			{
				return _003CPersistentOps_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPersistentOps_003Ek__BackingField = value;
			}
		}

		[JsonProperty("persistentNotifyUrl", NullValueHandling = NullValueHandling.Ignore)]
		public string PersistentNotifyUrl
		{
			[CompilerGenerated]
			get
			{
				return _003CPersistentNotifyUrl_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPersistentNotifyUrl_003Ek__BackingField = value;
			}
		}

		[JsonProperty("persistentPipeline", NullValueHandling = NullValueHandling.Ignore)]
		public string PersistentPipeline
		{
			[CompilerGenerated]
			get
			{
				return _003CPersistentPipeline_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPersistentPipeline_003Ek__BackingField = value;
			}
		}

		[JsonProperty("fsizeMin", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> FsizeMin
		{
			[CompilerGenerated]
			get
			{
				return _003CFsizeMin_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFsizeMin_003Ek__BackingField = value;
			}
		}

		[JsonProperty("fsizeLimit", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> FsizeLimit
		{
			[CompilerGenerated]
			get
			{
				return _003CFsizeLimit_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFsizeLimit_003Ek__BackingField = value;
			}
		}

		[JsonProperty("detectMime", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> DetectMime
		{
			[CompilerGenerated]
			get
			{
				return _003CDetectMime_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDetectMime_003Ek__BackingField = value;
			}
		}

		[JsonProperty("mimeLimit", NullValueHandling = NullValueHandling.Ignore)]
		public string MimeLimit
		{
			[CompilerGenerated]
			get
			{
				return _003CMimeLimit_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMimeLimit_003Ek__BackingField = value;
			}
		}

		[JsonProperty("deleteAfterDays", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> DeleteAfterDays
		{
			[CompilerGenerated]
			get
			{
				return _003CDeleteAfterDays_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDeleteAfterDays_003Ek__BackingField = value;
			}
		}

		[JsonProperty("fileType", NullValueHandling = NullValueHandling.Ignore)]
		public Nullable<int> FileType
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

		public void SetExpires(int expireInSeconds)
		{
			Deadline = (int)UnixTimestamp.GetUnixTimestamp(expireInSeconds);
		}

		public string ToJsonString()
		{
			if (Deadline == 0)
			{
				SetExpires(3600);
			}
			return JsonConvert.SerializeObject(this);
		}
	}
}
