using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using StkjApiLibrary.entity;
using System.Net.Mime;
using StkjPlatformApi;

namespace StkjApiLibrary.service
{
    class HttpRequest
    {
        private string uri;
        private string username;
        private string password;
        private string xAutoToken;
        private bool authing = false;

        public  HttpRequest() {
            this.username = Main.getConfig().username;
            this.password = Main.getConfig().password;
            this.uri = Main.getConfig().uri;
        }

        public HttpRequest(Config config)
        {
            this.username = config.username;
            this.password = config.password;
            this.uri = config.uri;
        }
            

        // 认证
        public bool auth()
        {
            if (authing)
            {
                authing = false;
                return false;
            }
           
            authing = true;
            string authInfo = this.username + ":" + this.password;
            string encoded = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", "Basic " + encoded);
            HttpResponse<User> userResponse = this.get<User>("/User/login", null, headers);
            authing = false;
            if (userResponse.httpWebResponse.StatusCode.Equals(HttpStatusCode.OK)) {
                this.xAutoToken = userResponse.httpWebResponse.Headers.Get("x-auth-token");
                return true;
            }
            return false;
        }

        protected String getAuthToken()
        {
            using (WebClient webClient = new WebClient())
            {

            }
            return "";
        }

        public HttpResponse<T> get<T>(String uri, Dictionary<String, Object> param)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("x-auth-token", this.xAutoToken);
            HttpResponse<T> response = this.get<T>(uri, param, headers);
            if (response.httpWebResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) && auth())
            {
                headers.Set("x-auth-token", this.xAutoToken);
                return this.get<T>(uri, param, headers);
            }
            return response;
        }

        /**
         *  get 请求
         *  uri: 请求相对路径
         *  param：请求参数
         * */
        protected HttpResponse<T> get<T>(String uri, Dictionary<String, Object> param, WebHeaderCollection headers)
        {
            String  requestUri = this.uri + uri + "?";
            if (param != null)
            {
                foreach (KeyValuePair<String, Object> entry in param)
                {
                    requestUri += entry.Value + "=" + entry.Value.ToString();
                    requestUri += "&";
                }
            }

            WebRequest request = WebRequest.Create(requestUri);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            if (headers != null) {
                request.Headers = headers;
            }
          

            String resultString = String.Empty;
            HttpWebResponse response;
            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    resultString = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (WebException exp) {
                response = (HttpWebResponse)exp.Response;
            }

            return new HttpResponse<T>(response, resultString);
        }

        public HttpResponse<T> put<T>(String uri, Object data) {
           
            string requestUri = this.uri + uri;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Set("x-auth-token", this.xAutoToken);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "PUT";
            request.Headers = headers;
            request.ContentType = "application/json";
        
            WriteStream(data, request);

           
            String resultString = String.Empty;
     
            HttpWebResponse response;
            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    resultString = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();

                }
            }
            catch (WebException exp)
            {
                response = (HttpWebResponse)exp.Response;
                if (response.StatusCode.Equals(HttpStatusCode.Unauthorized) && auth())
                {
                    return this.put<T>(uri, data);
                }
                else {
                    return new HttpResponse<T>(response);
                }
            }
            return new HttpResponse<T>(response, resultString);
        }

        internal void WriteStream(object obj, HttpWebRequest httpRequest)
        {
            if (obj != null)
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    if (obj is string)
                        streamWriter.Write(obj);
                    else {
                        String jsonString = JsonConvert.SerializeObject(obj);
                        streamWriter.Write(jsonString);
                    }
                }
            }
        }
    }
}
