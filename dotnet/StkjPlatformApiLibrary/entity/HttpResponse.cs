using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Com.Lfshitong.Platform.Api.Entity
{
    /**
     * http响应
     * 
     * */
    public class HttpResponse<T>
    {
        public HttpWebResponse httpWebResponse { get; private set; }    // 原生响应
        public T body { get; private set; }                             // 返回主体
        public long timestamp {get; set;}                               // 时间戳
        public short status {get; set;}                                 // 状态码
        public string error {get; set;}                                 // 错误
        public string message {get; set;}                               // 信息
        public string path {get; set;}                                  // 请求路径
        public string url { get; set; }                                 // 请求路径
        public string method { get; set; }                              // 请求方法

        private HttpResponse() { }     
 
        /**
         * 构造函数
         * response 原生的http响应
         * */
        public HttpResponse(HttpWebResponse response)
        {
            this.status = (short)response.StatusCode;
            this.httpWebResponse = response;

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string resultString = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            
            HttpResponse<object> me = JsonConvert.DeserializeObject<HttpResponse<object>>(resultString);
            this.timestamp = me.timestamp;
            this.error = me.error;
            this.message = me.message;
            this.path = me.path;
            this.url = me.url;
            this.method = me.method;
        }

        /**
         * 构造函数
         * httpWebResponse 原生的http响应
         * responseString 返回的字符串
         * */
        public HttpResponse(HttpWebResponse httpWebResponse, string responseString) 
        {
            this.httpWebResponse = httpWebResponse;
            this.body = JsonConvert.DeserializeObject<T>(responseString);
            this.status = (short)httpWebResponse.StatusCode;
        }
    }
}
