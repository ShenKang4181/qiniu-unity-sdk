using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.CDN
{
	public class RefreshRequest
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CUrls_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CDirs_003Ek__BackingField;

		[JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Urls
		{
			[CompilerGenerated]
			get
			{
				return _003CUrls_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUrls_003Ek__BackingField = value;
			}
		}

		[JsonProperty("dirs", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Dirs
		{
			[CompilerGenerated]
			get
			{
				return _003CDirs_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDirs_003Ek__BackingField = value;
			}
		}

		public RefreshRequest()
		{
			Urls = new List<string>();
			Dirs = new List<string>();
		}

		public RefreshRequest(IList<string> urls, IList<string> dirs)
		{
			Urls = new List<string>();
			Dirs = new List<string>();
			if (urls != null)
			{
				AddUrls(urls);
			}
			if (dirs != null)
			{
				AddDirs(dirs);
			}
		}

		public void AddUrls(IList<string> urls)
		{
			if (urls == null)
			{
				return;
			}
			foreach (string url in urls)
			{
				if (!Urls.Contains(url))
				{
					Urls.Add(url);
				}
			}
		}

		public void AddDirs(IList<string> dirs)
		{
			if (dirs == null)
			{
				return;
			}
			foreach (string dir in dirs)
			{
				if (!Dirs.Contains(dir))
				{
					Dirs.Add(dir);
				}
			}
		}

		public string ToJsonStr()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
