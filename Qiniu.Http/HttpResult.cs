using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Qiniu.Http
{
	public class HttpResult
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CCode_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CText_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte[] _003CData_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _003CRefCode_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _003CRefText_003Ek__BackingField;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, string> _003CRefInfo_003Ek__BackingField;

		public static HttpResult InvalidToken;

		public static HttpResult InvalidFile;

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

		public string Text
		{
			[CompilerGenerated]
			get
			{
				return _003CText_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CText_003Ek__BackingField = value;
			}
		}

		public byte[] Data
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

		public int RefCode
		{
			[CompilerGenerated]
			get
			{
				return _003CRefCode_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRefCode_003Ek__BackingField = value;
			}
		}

		public string RefText
		{
			[CompilerGenerated]
			get
			{
				return _003CRefText_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRefText_003Ek__BackingField = value;
			}
		}

		public Dictionary<string, string> RefInfo
		{
			[CompilerGenerated]
			get
			{
				return _003CRefInfo_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRefInfo_003Ek__BackingField = value;
			}
		}

		public HttpResult()
		{
			Code = 0;
			Text = null;
			Data = null;
			RefCode = 0;
			RefInfo = null;
		}

		public void Shadow(HttpResult hr)
		{
			Code = hr.Code;
			Text = hr.Text;
			Data = hr.Data;
			RefCode = hr.RefCode;
			RefText += hr.RefText;
			RefInfo = hr.RefInfo;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("code:{0}", Code);
			stringBuilder.AppendLine();
			if (!string.IsNullOrEmpty(Text))
			{
				stringBuilder.AppendLine("text:");
				stringBuilder.AppendLine(Text);
			}
			if (Data != null)
			{
				stringBuilder.AppendLine("data:");
				int num = 1024;
				if (Data.Length <= num)
				{
					stringBuilder.AppendLine(Encoding.UTF8.GetString(Data));
				}
				else
				{
					stringBuilder.AppendLine(Encoding.UTF8.GetString(Data, 0, num));
					stringBuilder.AppendFormat("<--- TOO-LARGE-TO-DISPLAY --- TOTAL {0} BYTES --->", Data.Length);
					stringBuilder.AppendLine();
				}
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("ref-code:{0}", RefCode);
			stringBuilder.AppendLine();
			if (!string.IsNullOrEmpty(RefText))
			{
				stringBuilder.AppendLine("ref-text:");
				stringBuilder.AppendLine(RefText);
			}
			if (RefInfo != null)
			{
				stringBuilder.AppendLine("ref-info:");
				foreach (KeyValuePair<string, string> item in RefInfo)
				{
					stringBuilder.AppendLine(string.Format("{0}:{1}", item.Key, item.Value));
				}
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		static HttpResult()
		{
			HttpResult httpResult = new HttpResult();
			httpResult.Code = -5;
			httpResult.Text = "invalid uptoken";
			InvalidToken = httpResult;
			HttpResult httpResult2 = new HttpResult();
			httpResult2.Code = -3;
			httpResult2.Text = "invalid file";
			InvalidFile = httpResult2;
		}
	}
}
