using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.CDN
{
	public class FluxRequest
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CStartDate_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CEndDate_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CGranularity_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CDomains_003Ek__BackingField;

		[JsonProperty("startDate")]
		public string StartDate
		{
			[CompilerGenerated]
			get
			{
				return _003CStartDate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CStartDate_003Ek__BackingField = value;
			}
		}

		[JsonProperty("endDate")]
		public string EndDate
		{
			[CompilerGenerated]
			get
			{
				return _003CEndDate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CEndDate_003Ek__BackingField = value;
			}
		}

		[JsonProperty("granularity")]
		public string Granularity
		{
			[CompilerGenerated]
			get
			{
				return _003CGranularity_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CGranularity_003Ek__BackingField = value;
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

		public FluxRequest()
		{
			StartDate = "";
			EndDate = "";
			Granularity = "";
			Domains = "";
		}

		public FluxRequest(string startDate, string endDate, string granularity, string domains)
		{
			StartDate = startDate;
			EndDate = endDate;
			Granularity = granularity;
			Domains = domains;
		}

		public string ToJsonStr()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
