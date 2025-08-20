using Cysharp.Threading.Tasks;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace Qiniu.Http
{
	public class HttpManager
	{
		private bool allowAutoRedirect;

		private string userAgent;

		public HttpManager(bool allowAutoRedirect = false)
		{
			this.allowAutoRedirect = allowAutoRedirect;
			userAgent = GetUserAgent();
		}

		public static string GetUserAgent()
		{
			string text = string.Concat(Environment.OSVersion.Platform, "; ", Environment.OSVersion.Version);
			return string.Format("{0}/{1} ({2}; {3})", "QiniuCSharpSDK", "7.3.0", "NET40", text);
		}

		public void SetUserAgent(string userAgent)
		{
			if (!string.IsNullOrEmpty(userAgent))
			{
				this.userAgent = userAgent;
			}
		}

		public static string CreateFormDataBoundary()
		{
			string str = DateTime.UtcNow.Ticks.ToString();
			return string.Format("-------{0}Boundary{1}", "QiniuCSharpSDK", Hashing.CalcMD5X(str));
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Get(string url, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "GET";
                request = UnityWebRequest.Get( url );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request .result == UnityWebRequest.Result.Success )
				{
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
						//int num = (int)httpWebResponse.ContentLength;
						//hr.Data = new byte[num];
						//int num2 = num;
						//int num3 = 0;
						//using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
						//{
						//	while (num2 > 0)
						//	{
						//		num3 = binaryReader.Read(hr.Data, num - num2, num2);
						//		num2 -= num3;
						//	}
						//}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
						//using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
						//{
						//	hr.Text = streamReader.ReadToEnd();
						//}
						hr.Text = request.downloadHandler.text;
					}
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-GET] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
				//if (httpWebRequest != null)
				//{
				//	httpWebRequest.Abort();
				//}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> Post(string url, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
				//httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				//httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostData(string url, byte[] data, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST"  );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
				//httpWebRequest.ContentType = ContentType.APPLICATION_OCTET_STREAM;
                request.SetRequestHeader( "Content-Type" , ContentType.APPLICATION_OCTET_STREAM );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
				//if (data != null)
				//{
				//	httpWebRequest.AllowWriteStreamBuffering = true;
				//	using (Stream stream = httpWebRequest.GetRequestStream())
				//	{
				//		stream.Write(data, 0, data.Length);
				//		stream.Flush();
				//	}
				//}
                request.uploadHandler = new UploadHandlerRaw( data );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-BIN] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostData(string url, byte[] data, string mimeType, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = mimeType;
                request.SetRequestHeader( "Content-Type" , mimeType );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
				//if (data != null)
				//{
				//	httpWebRequest.AllowWriteStreamBuffering = true;
				//	using (Stream stream = httpWebRequest.GetRequestStream())
				//	{
				//		stream.Write(data, 0, data.Length);
				//		stream.Flush();
				//	}
				//}
                request.uploadHandler = new UploadHandlerRaw( data );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-BIN] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostJson(string url, string data, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = ContentType.APPLICATION_JSON;
                request.SetRequestHeader( "Content-Type" , ContentType.APPLICATION_JSON );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
				//if (data != null)
				//{
				//	httpWebRequest.AllowWriteStreamBuffering = true;
				//	using (Stream stream = httpWebRequest.GetRequestStream())
				//	{
				//		stream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
				//		stream.Flush();
				//	}
				//}
                request.uploadHandler = new UploadHandlerRaw( Encoding.UTF8.GetBytes( data ) );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-JSON] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostText(string url, string data, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = ContentType.TEXT_PLAIN;
                request.SetRequestHeader( "Content-Type" , ContentType.TEXT_PLAIN );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
                //if (data != null)
                //{
                //	httpWebRequest.AllowWriteStreamBuffering = true;
                //	using (Stream stream = httpWebRequest.GetRequestStream())
                //	{
                //		stream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                //		stream.Flush();
                //	}
                //}
                request.uploadHandler = new UploadHandlerRaw( Encoding.UTF8.GetBytes( data ) );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-TEXT] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostForm(string url, Dictionary<string, string> kvData, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                List<IMultipartFormSection> formData = new List<IMultipartFormSection>( );
                if ( kvData != null )
                {
                    foreach ( var kvDatum in kvData )
                    {
                        formData.Add( new MultipartFormDataSection( kvDatum.Key , kvDatum.Value ) );
                    }
                }
                request = UnityWebRequest.Post( url , formData );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
				//httpWebRequest.ContentType = ContentType.WWW_FORM_URLENC;
                request.SetRequestHeader( "Content-Type" , ContentType.WWW_FORM_URLENC );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
                //if (kvData != null)
                //{
                //	StringBuilder stringBuilder = new StringBuilder();
                //	foreach (KeyValuePair<string, string> kvDatum in kvData)
                //	{
                //		stringBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(kvDatum.Key), Uri.EscapeDataString(kvDatum.Value));
                //	}
                //	httpWebRequest.AllowWriteStreamBuffering = true;
                //	using (Stream stream = httpWebRequest.GetRequestStream())
                //	{
                //		stream.Write(Encoding.UTF8.GetBytes(stringBuilder.ToString()), 0, stringBuilder.Length - 1);
                //		stream.Flush();
                //	}
                //}
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder2.Append(ex3.Message + " ");
				}
				stringBuilder2.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder2.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostForm(string url, string data, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = ContentType.WWW_FORM_URLENC;
                request.SetRequestHeader( "Content-Type" , ContentType.WWW_FORM_URLENC );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
				if (!string.IsNullOrEmpty(data))
				{
					//httpWebRequest.AllowWriteStreamBuffering = true;
					//using (Stream stream = httpWebRequest.GetRequestStream())
					//{
					//	stream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
					//	stream.Flush();
					//}
                    request.uploadHandler = new UploadHandlerRaw( Encoding.UTF8.GetBytes( data ) );
                }
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostForm(string url, byte[] data, string token, bool binaryMode = false)
		{
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = ContentType.WWW_FORM_URLENC;
                request.SetRequestHeader( "Content-Type" , ContentType.WWW_FORM_URLENC );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
				//if (data != null)
				//{
				//	httpWebRequest.AllowWriteStreamBuffering = true;
				//	using (Stream stream = httpWebRequest.GetRequestStream())
				//	{
				//		stream.Write(data, 0, data.Length);
				//		stream.Flush();
				//	}
				//}
                request.uploadHandler = new UploadHandlerRaw( data );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                await request.SendWebRequest( );
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		public async Cysharp.Threading.Tasks.UniTask<HttpResult> PostMultipart(string url, 
                                                                               byte[] data,
                                                                               string boundary,
                                                                               string token,
                                                                               bool binaryMode = false , 
                                                                               System.IProgress<float> progress = null )
        {
			HttpResult hr = new HttpResult();
            //HttpWebRequest httpWebRequest = null;
            UnityWebRequest request = null;
            try
			{
                //httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                //httpWebRequest.Method = "POST";
                request = new UnityWebRequest( url , "POST" );
                if (!string.IsNullOrEmpty(token))
				{
                    //httpWebRequest.Headers.Add("Authorization", token);
                    request.SetRequestHeader( "Authorization" , token );
                }
                //httpWebRequest.ContentType = string.Format("{0}; boundary={1}", ContentType.MULTIPART_FORM_DATA, boundary);
                request.SetRequestHeader( "Content-Type" , string.Format( "{0}; boundary={1}" , ContentType.MULTIPART_FORM_DATA , boundary ) );
                //httpWebRequest.UserAgent = userAgent;
                request.SetRequestHeader( "User-Agent" , userAgent );
                //httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                request.redirectLimit = allowAutoRedirect ? 32 : 0;
                //httpWebRequest.ServicePoint.Expect100Continue = false;
                //httpWebRequest.AllowWriteStreamBuffering = true;
                //using (Stream stream = httpWebRequest.GetRequestStream())
                //{
                //	stream.Write(data, 0, data.Length);
                //	stream.Flush();
                //}
                request.uploadHandler = new UploadHandlerRaw( data );
                request.downloadHandler = new DownloadHandlerBuffer( );
                //HttpWebResponse httpWebResponse = (await httpWebRequest.GetResponseAsync()) as HttpWebResponse;
                var  operation =  request.SendWebRequest( );
                while (!operation .isDone)
                {
                    progress?.Report( request.uploadProgress );
                    await UniTask.Yield( );
                }
                //if (httpWebResponse != null)
                if ( request.result == UnityWebRequest.Result.Success )
                {
                    //hr.Code = (int)httpWebResponse.StatusCode;
                    hr.Code = ( int ) request.responseCode;
                    //hr.RefCode = (int)httpWebResponse.StatusCode;
                    hr.RefCode = ( int ) request.responseCode;
                    //getHeaders(ref hr, httpWebResponse);
                    getHeaders( ref hr , request );
                    if (binaryMode)
					{
                        //int num = (int)httpWebResponse.ContentLength;
                        //hr.Data = new byte[num];
                        //int num2 = num;
                        //int num3 = 0;
                        //using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	while (num2 > 0)
                        //	{
                        //		num3 = binaryReader.Read(hr.Data, num - num2, num2);
                        //		num2 -= num3;
                        //	}
                        //}
                        hr.Data = request.downloadHandler.data;
                    }
					else
					{
                        //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        //{
                        //	hr.Text = streamReader.ReadToEnd();
                        //}
                        hr.Text = request.downloadHandler.text;
                    }
					//httpWebResponse.Close();
				}
			}
			//catch (WebException ex)
			//{
			//	HttpWebResponse httpWebResponse2 = ex.Response as HttpWebResponse;
			//	if (httpWebResponse2 != null)
			//	{
			//		hr.Code = (int)httpWebResponse2.StatusCode;
			//		hr.RefCode = (int)httpWebResponse2.StatusCode;
			//		getHeaders(ref hr, httpWebResponse2);
			//		using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
			//		{
			//			hr.Text = streamReader2.ReadToEnd();
			//		}
			//		httpWebResponse2.Close();
			//	}
			//}
			catch (Exception ex2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}] [{1}] [HTTP-POST-MPART] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
				for (Exception ex3 = ex2; ex3 != null; ex3 = ex3.InnerException)
				{
					stringBuilder.Append(ex3.Message + " ");
				}
				stringBuilder.AppendLine();
				hr.RefCode = 0;
				hr.RefText += stringBuilder.ToString();
			}
			finally
			{
                //if (httpWebRequest != null)
                //{
                //	httpWebRequest.Abort();
                //}
                if ( request != null )
                {
                    request.Dispose( );
                }
            }
			return hr;
		}

		private void getHeaders(ref HttpResult hr,
            //HttpWebResponse resp
            UnityWebRequest req
            )
		{
			if (
                //resp
                req
                == null)
			{
				return;
			}
			if (hr.RefInfo == null)
			{
				hr.RefInfo = new Dictionary<string, string>();
			}
            //hr.RefInfo.Add("ProtocolVersion", resp.ProtocolVersion.ToString());
            hr.RefInfo.Add( "ProtocolVersion" , "HTTP/1.1" );
            //if (!string.IsNullOrEmpty(resp.CharacterSet))
            //{
            //	hr.RefInfo.Add("Characterset", resp.CharacterSet);
            //}
            hr.RefInfo.Add( "Characterset" , req.GetResponseHeader( "charset" ) );
            //if (!string.IsNullOrEmpty(resp.ContentEncoding))
			//{
			//	hr.RefInfo.Add("ContentEncoding", resp.ContentEncoding);
            //}
            hr.RefInfo.Add( "ContentEncoding" , req.GetResponseHeader( "Content-Encoding" ) );
            //if (!string.IsNullOrEmpty(resp.ContentType))
            //{
            //	hr.RefInfo.Add("ContentType", resp.ContentType);
            //}
            hr.RefInfo.Add( "ContentType" , req.GetResponseHeader( "Content-Type" ) );
            //hr.RefInfo.Add("ContentLength", resp.ContentLength.ToString());
            hr.RefInfo.Add( "ContentLength" , req.GetResponseHeader( "Content-Length" ) );
            //WebHeaderCollection headers = resp.Headers;
            //if (headers != null && headers.Count > 0)
            //{
            //	if (hr.RefInfo == null)
            //	{
            //		hr.RefInfo = new Dictionary<string, string>();
            //	}
            //	string[] allKeys = headers.AllKeys;
            //	foreach (string text in allKeys)
            //	{
            //		hr.RefInfo.Add(text, headers[text]);
            //	}
            //}
            foreach ( var header in req.GetResponseHeaders( ) )
            {
                if ( !hr.RefInfo.ContainsKey( header.Key ) && header.Value != null )
                {
                    hr.RefInfo.Add( header.Key , header.Value );
                }
            }
        }
	}
}
