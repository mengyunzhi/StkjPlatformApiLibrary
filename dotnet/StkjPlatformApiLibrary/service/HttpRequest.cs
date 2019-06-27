using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Com.Lfshitong.Platform.Api.Entity;
using System.Net.Mime;
using Com.Lfshitong.Platform.Api;

namespace Com.Lfshitong.Platform.Api.Service
{
    /**
     * http请求
     * 封装了请求地址，用户名，密码，认证TOKEN
     * 
     * */
    class HttpRequest
    {
        private string uri;                         // 请求地址
        private string username;                    // 用户名
        private string password;                    // 密码
        private string xAutoToken;                  // 认证TOKEN
        private bool authing = false;               // 是否正在进行认证
        private static HttpRequest instance;        // 单例

        /**
         * 获取单例
         * 
         * */
        public static HttpRequest getInstance()
        {
            if (instance == null)
            {
                instance = new HttpRequest(Main.Config());
            }
            return instance;
        }

        /**
         * 初始化单例
         * config 配置信息
         * 
         * */
        public static HttpRequest instanceByConfig(Config config)
        {
            instance = new HttpRequest(config);
            return instance;
        }

        private HttpRequest()
        {
        }

        private HttpRequest(Config config)
        {
            this.username = config.username;
            this.password = config.password;
            this.uri = config.uri;
            Auth();
        }


        // 认证
        public bool Auth()
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
            HttpResponse<User> userResponse = this.Get<User>("/User/login", null, headers);
            authing = false;
            if (userResponse.httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                this.xAutoToken = userResponse.httpWebResponse.Headers.Get("x-auth-token");
                return true;
            }
            return false;
        }

        /**
         * get请求
         * uri 地址
         * param 参数
         * 
         * */
        public HttpResponse<T> Get<T>(String uri, Dictionary<String, Object> param)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("x-auth-token", this.xAutoToken);
            HttpResponse<T> response = this.Get<T>(uri, param, headers);
            if (response.httpWebResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) && Auth())
            {
                headers.Set("x-auth-token", this.xAutoToken);
                return this.Get<T>(uri, param, headers);
            }
            return response;
        }

        /**
         *  get 请求
         *  uri: 请求相对路径
         *  param：请求参数
         * */
        protected HttpResponse<T> Get<T>(String uri, Dictionary<String, Object> param, WebHeaderCollection headers)
        {
            String requestUri = this.uri + uri + "?";
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
            if (headers != null)
            {
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
            catch (WebException exp)
            {
                response = (HttpWebResponse)exp.Response;
            }

            return new HttpResponse<T>(response, resultString);
        }

        /**
         * 
        * Put 请求
        * uri 地址
        * data 请求数据
        * 
        * */
        public HttpResponse<T> Put<T>(String uri, Object data)
        {

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
                if (response.StatusCode.Equals(HttpStatusCode.Unauthorized) && Auth())
                {
                    return this.Put<T>(uri, data);
                }
                else
                {
                    return new HttpResponse<T>(response);
                }
            }
            return new HttpResponse<T>(response, resultString);
        }

        /**
         * 写入数据流
         * obj 写入的数据
         * httpRequest http请求
         * 
         * */
        internal void WriteStream(object obj, HttpWebRequest httpRequest)
        {
            if (obj != null)
            {
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    if (obj is string)
                        streamWriter.Write(obj);
                    else
                    {
                        String jsonString = JsonConvert.SerializeObject(obj);
                        streamWriter.Write(jsonString);
                    }
                }
            }
        }
    }
}
