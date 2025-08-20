using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.CDN
{
	public class PrefetchRequest
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CUrls_003Ek__BackingField;

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

		public PrefetchRequest()
		{
			Urls = new List<string>();
		}

		public PrefetchRequest(IList<string> urls)
		{
			if (urls != null)
			{
				Urls = new List<string>(urls);
			}
			else
			{
				Urls = new List<string>();
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

		public string ToJsonStr()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
