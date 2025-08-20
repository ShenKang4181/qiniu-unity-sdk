using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
	public class ListResult : HttpResult
	{
		public ListInfo Result
		{
			get
			{
				ListInfo result = null;
				if (base.Code == 200 && !string.IsNullOrEmpty(base.Text))
				{
					result = JsonConvert.DeserializeObject<ListInfo>(base.Text);
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
				if (Result.CommonPrefixes != null)
				{
					stringBuilder.Append("commonPrefixes:");
					foreach (string commonPrefix in Result.CommonPrefixes)
					{
						stringBuilder.AppendFormat("{0} ", commonPrefix);
					}
					stringBuilder.AppendLine();
				}
				if (!string.IsNullOrEmpty(Result.Marker))
				{
					stringBuilder.AppendFormat("marker: {0}\n", Result.Marker);
				}
				if (Result.Items != null)
				{
					stringBuilder.AppendLine("items:");
					int num = 0;
					int count = Result.Items.Count;
					foreach (ListItem item in Result.Items)
					{
						stringBuilder.AppendFormat("#{0}/{1}:Key={2}, Size={3}, Mime={4}, Hash={5}, Time={6}, Type={7}\n", ++num, count, item.Key, item.Fsize, item.MimeType, item.Hash, item.PutTime, item.FileType);
					}
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
