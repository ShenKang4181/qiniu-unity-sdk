using System;
using System.IO;
using Newtonsoft.Json;
using Qiniu.Util;

namespace Qiniu.Storage
{
	public class ResumeHelper
	{
		public static string GetDefaultRecordKey(string localFile, string key)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(localFile);
			string str = string.Format("{0}:{1}:{2}", localFile, key, fileInfo.LastWriteTime.ToFileTime());
			return string.Format("{0}\\{1}", environmentVariable, "QiniuResume_" + Hashing.CalcMD5X(str));
		}

		public static ResumeInfo Load(string recordFile)
		{
			ResumeInfo result = null;
			try
			{
				using (FileStream stream = new FileStream(recordFile, FileMode.Open))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						string value = streamReader.ReadToEnd();
						result = JsonConvert.DeserializeObject<ResumeInfo>(value);
					}
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static void Save(ResumeInfo resumeInfo, string recordFile)
		{
			string value = resumeInfo.ToJsonStr();
			using (FileStream stream = new FileStream(recordFile, FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.Write(value);
				}
			}
		}
	}
}
