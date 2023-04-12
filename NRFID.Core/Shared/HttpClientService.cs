using BSMediator.Core.ResourceFiles;
using Newtonsoft.Json;
using NFC.Core.Enums;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Shared
{
    public class HttpClientService
    {

        public async Task<ResponseResult> CallServicePost(string url, object request, string token)
        {
            string results = null;
            HttpStatusCode statusCode;
            string messageResponse = "";
            HttpWebResponse httpResponse = null;
            ResponseResult responseResult = null;
            try
            {
                Uri myUri = new Uri(url, UriKind.Absolute);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "Post";
                httpWebRequest.Timeout = 30000;
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "jwt " + "");
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var z = JsonConvert.SerializeObject(request);
                    streamWriter.Write(JsonConvert.SerializeObject(request));
                }
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream response = httpResponse.GetResponseStream();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    results =await streamReader.ReadToEndAsync();
                    responseResult = new ResponseResult { IsSucceeded = true, ApiStatusCode = (int)ApiStatusCodeEnum.OK, ReturnData = results };

                }
                return responseResult;
            }
            catch (WebException ex)
            {

                return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);
            }

        }

        public ResponseResult CallServicePostString(string url, object request, string token)
        {
            string results = null;
            string messageResponse = "";

            try
            {
                Uri myUri = new Uri(url, UriKind.Absolute);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "Post";
                httpWebRequest.Timeout = 30000;
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var z = JsonConvert.SerializeObject(request);
                    streamWriter.Write(JsonConvert.SerializeObject(request));
                }
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusDescription != "OK")
                    return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);
                Stream response = httpResponse.GetResponseStream();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    results = streamReader.ReadToEnd();
                }
                if (string.IsNullOrEmpty(results))
                    return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);

            }
            catch (Exception ex)
            {
                var e = ex.ToString();

                return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);
            }

            return new ResponseResult(results, ApiStatusCodeEnum.OK);
        }

        public async Task<ResponseResult> CallService(string url, string token)
        {
            try
            {
                Uri myUri = new Uri(url, UriKind.Absolute);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = 30000;
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "jwt " + token);

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string result = "";
                Stream response = httpResponse.GetResponseStream();
                if (response == null) return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);
                using (StreamReader streamReader = new StreamReader(response))
                {
                    result = await streamReader.ReadToEndAsync();
                }

                if (string.IsNullOrEmpty(result))
                    return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);

                return new ResponseResult { IsSucceeded = true, ReturnData = result, ApiStatusCode = (int)ApiStatusCodeEnum.OK };
            }
            catch (Exception ex)
            {
                return new ResponseResult(Messages.InvalidData, ApiStatusCodeEnum.BadRequest);
            }
        }

    }
}