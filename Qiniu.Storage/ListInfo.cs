using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
	public class ListInfo
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CMarker_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<ListItem> _003CItems_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CCommonPrefixes_003Ek__BackingField;

		[JsonProperty("marker", NullValueHandling = NullValueHandling.Ignore)]
		public string Marker
		{
			[CompilerGenerated]
			get
			{
				return _003CMarker_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMarker_003Ek__BackingField = value;
			}
		}

		[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
		public List<ListItem> Items
		{
			[CompilerGenerated]
			get
			{
				return _003CItems_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CItems_003Ek__BackingField = value;
			}
		}

		[JsonProperty("commonPrefixes", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> CommonPrefixes
		{
			[CompilerGenerated]
			get
			{
				return _003CCommonPrefixes_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCommonPrefixes_003Ek__BackingField = value;
			}
		}
	}
}
