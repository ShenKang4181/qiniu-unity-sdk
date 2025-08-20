using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
	public class BucketsResult : HttpResult
	{
		public List<string> Result
		{
			get
			{
				List<string> result = null;
				if (base.Code == 200 && !string.IsNullOrEmpty(base.Text))
				{
					result = JsonConvert.DeserializeObject<List<string>>(base.Text);
				}
				return result;
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("code: {0}\n", base.Code);
			if (Result != null)
			{
				stringBuilder.AppendLine("bucket(s):");
				foreach (string item in Result)
				{
					stringBuilder.AppendLine(item);
				}
			}
			else if (!string.IsNullOrEmpty(base.Text))
			{
				stringBuilder.AppendLine("text:");
				stringBuilder.AppendLine(base.Text);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("ref-code: {0}\n", base.RefCode);
			if (!string.IsNullOrEmpty(base.RefText))
			{
				stringBuilder.AppendLine("ref-text:");
				stringBuilder.AppendLine(base.RefText);
			}
			if (base.RefInfo != null)
			{
				stringBuilder.AppendFormat("ref-info:\n");
				foreach (KeyValuePair<string, string> item2 in base.RefInfo)
				{
					stringBuilder.AppendLine(string.Format("{0}: {1}", item2.Key, item2.Value));
				}
			}
			return stringBuilder.ToString();
		}
	}
}
