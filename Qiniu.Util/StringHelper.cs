using System;
using System.Collections.Generic;
using System.Text;

namespace Qiniu.Util
{
	public class StringHelper
	{
		public static string UrlEncode(string text)
		{
			return Uri.EscapeDataString(text);
		}

		public static string UrlFormEncode(Dictionary<string, string> values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> value in values)
			{
				stringBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(value.Key), Uri.EscapeDataString(value.Value));
			}
			string text = stringBuilder.ToString();
			return text.Substring(0, text.Length - 1);
		}
	}
}
