using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Qiniu.CDN
{
	public class BandWidthData
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<ulong> _003CChina_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<ulong> _003COversea_003Ek__BackingField;

		public List<ulong> China
		{
			[CompilerGenerated]
			get
			{
				return _003CChina_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CChina_003Ek__BackingField = value;
			}
		}

		public List<ulong> Oversea
		{
			[CompilerGenerated]
			get
			{
				return _003COversea_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003COversea_003Ek__BackingField = value;
			}
		}
	}
}
