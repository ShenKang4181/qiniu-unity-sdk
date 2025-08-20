using System;
using Qiniu.Http;

namespace Qiniu.Storage
{
	internal class QiniuException : Exception
	{
		public string message;

		public HttpResult HttpResult;

		public override string Message
		{
			get
			{
				return message;
			}
		}

		public QiniuException(HttpResult httpResult, string message)
		{
			HttpResult = ((httpResult == null) ? new HttpResult() : httpResult);
			this.message = message;
		}
	}
}
