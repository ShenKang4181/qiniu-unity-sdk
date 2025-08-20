using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.CDN
{
	public class LogListRequest
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CDay_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CDomains_003Ek__BackingField;

		[JsonProperty("day")]
		public string Day
		{
			[CompilerGenerated]
			get
			{
				return _003CDay_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDay_003Ek__BackingField = value;
			}
		}

		[JsonProperty("domains")]
		public string Domains
		{
			[CompilerGenerated]
			get
			{
				return _003CDomains_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDomains_003Ek__BackingField = value;
			}
		}

		public LogListRequest()
		{
			Day = "";
			Domains = "";
		}

		public LogListRequest(string day, string domains)
		{
			Day = day;
			Domains = domains;
		}

		public LogListRequest(string day, IList<string> domains)
		{
			if (string.IsNullOrEmpty(day))
			{
				Day = "";
			}
			else
			{
				Day = day;
			}
			if (domains == null)
			{
				Domains = "";
				return;
			}
			List<string> list = new List<string>();
			foreach (string domain in domains)
			{
				if (!list.Contains(domain))
				{
					list.Add(domain);
				}
			}
			if (list.Count > 0)
			{
				Domains = string.Join(";", list);
			}
			else
			{
				Domains = "";
			}
		}

		public string ToJsonStr()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
