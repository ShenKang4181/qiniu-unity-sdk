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

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> UploadData(byte[] data, string key, string token, PutExtra extra)
		{
			FormUploader formUploader = new FormUploader(config);
			return await formUploader.UploadData(data, key, token, extra);
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> UploadFile(string localFile, string key, string token, PutExtra extra)
		{
			HttpResult httpResult = new HttpResult();
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(localFile);
			if (fileInfo.Length > config.PutThreshold)
			{
				ResumableUploader resumableUploader = new ResumableUploader(config);
				return await resumableUploader.UploadFile(localFile, key, token, extra);
			}
			FormUploader formUploader = new FormUploader(config);
			return await formUploader.UploadFile(localFile, key, token, extra);
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> UploadStream(Stream stream, string key, string token, PutExtra extra)
		{
			HttpResult httpResult = new HttpResult();
			if (stream.Length > config.PutThreshold)
			{
				ResumableUploader resumableUploader = new ResumableUploader(config);
				return await resumableUploader.UploadStream(stream, key, token, extra);
			}
			FormUploader formUploader = new FormUploader(config);
			return await formUploader.UploadStream(stream, key, token, extra);
		}
	}
}
