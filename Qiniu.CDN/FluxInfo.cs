using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Qiniu.CDN
{
	public class FluxInfo
	{
		public class FluxData
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

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CCode_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CError_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> _003CTime_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, FluxData> _003CData_003Ek__BackingField;

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

		public List<string> Time
		{
			[CompilerGenerated]
			get
			{
				return _003CTime_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CTime_003Ek__BackingField = value;
			}
		}

		public Dictionary<string, FluxData> Data
		{
			[CompilerGenerated]
			get
			{
				return _003CData_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CData_003Ek__BackingField = value;
			}
		}
	}
}
