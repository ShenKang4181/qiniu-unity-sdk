using System;
using System.Text;
using Newtonsoft.Json;
using Qiniu.Storage;

namespace Qiniu.Util
{
	public class UpToken
	{
		public static string GetAccessKeyFromUpToken(string upToken)
		{
			string result = null;
			string[] array = upToken.Split(':');
			if (array.Length == 3)
			{
				result = array[0];
			}
			return result;
		}

		public static string GetBucketFromUpToken(string upToken)
		{
			string result = null;
			string[] array = upToken.Split(':');
			if (array.Length == 3)
			{
				string text = array[2];
				try
				{
					string value = Encoding.UTF8.GetString(Base64.UrlsafeBase64Decode(text));
					PutPolicy putPolicy = JsonConvert.DeserializeObject<PutPolicy>(value);
					string scope = putPolicy.Scope;
					string[] array2 = scope.Split(':');
					if (array2.Length >= 1)
					{
						result = array2[0];
					}
				}
				catch (Exception)
				{
				}
			}
			return result;
		}
	}
}
