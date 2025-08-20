using System.IO;
using Qiniu.Http;

namespace Qiniu.Storage
{
	public class UploadManager
	{
		private Config config;

		public UploadManager(Config config)
		{
			this.config = config;
		}

		public HttpResult UploadData(byte[] data, string key, string token, PutExtra extra)
		{
			FormUploader formUploader = new FormUploader(config);
			return formUploader.UploadData(data, key, token, extra);
		}

		public HttpResult UploadFile(string localFile, string key, string token, PutExtra extra)
		{
			HttpResult httpResult = new HttpResult();
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(localFile);
			if (fileInfo.Length > config.PutThreshold)
			{
				ResumableUploader resumableUploader = new ResumableUploader(config);
				return resumableUploader.UploadFile(localFile, key, token, extra);
			}
			FormUploader formUploader = new FormUploader(config);
			return formUploader.UploadFile(localFile, key, token, extra);
		}

		public HttpResult UploadStream(Stream stream, string key, string token, PutExtra extra)
		{
			HttpResult httpResult = new HttpResult();
			if (stream.Length > config.PutThreshold)
			{
				ResumableUploader resumableUploader = new ResumableUploader(config);
				return resumableUploader.UploadStream(stream, key, token, extra);
			}
			FormUploader formUploader = new FormUploader(config);
			return formUploader.UploadStream(stream, key, token, extra);
		}
	}
}
