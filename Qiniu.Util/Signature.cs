using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Qiniu.Util
{
	public class Signature
	{
		private Mac mac;

		public Signature(Mac mac)
		{
			this.mac = mac;
		}

		private string encodedSign(byte[] data)
		{
			HMACSHA1 hMACSHA = new HMACSHA1(Encoding.UTF8.GetBytes(mac.SecretKey));
			byte[] data2 = hMACSHA.ComputeHash(data);
			return Base64.UrlSafeBase64Encode(data2);
		}

		private string encodedSign(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return encodedSign(bytes);
		}

		public string Sign(byte[] data)
		{
			return string.Format("{0}:{1}", mac.AccessKey, encodedSign(data));
		}

		public string Sign(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return Sign(bytes);
		}

		public string SignWithData(byte[] data)
		{
			string text = Base64.UrlSafeBase64Encode(data);
			return string.Format("{0}:{1}:{2}", mac.AccessKey, encodedSign(text), text);
		}

		public string SignWithData(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return SignWithData(bytes);
		}

		public string SignRequest(string url, byte[] body)
		{
			Uri uri = new Uri(url);
			string pathAndQuery = uri.PathAndQuery;
			byte[] bytes = Encoding.UTF8.GetBytes(pathAndQuery);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.WriteByte(10);
				if (body != null && body.Length != 0)
				{
					memoryStream.Write(body, 0, body.Length);
				}
				HMACSHA1 hMACSHA = new HMACSHA1(Encoding.UTF8.GetBytes(mac.SecretKey));
				byte[] data = hMACSHA.ComputeHash(memoryStream.ToArray());
				string arg = Base64.UrlSafeBase64Encode(data);
				return string.Format("{0}:{1}", mac.AccessKey, arg);
			}
		}

		public string SignRequest(string url, string body)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(body);
			return SignRequest(url, bytes);
		}
	}
}
