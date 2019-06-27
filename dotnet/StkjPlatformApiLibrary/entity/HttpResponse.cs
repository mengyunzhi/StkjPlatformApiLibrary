using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Com.Lfshitong.Platform.Api.Entity
{
    public class HttpResponse<T>
    {
        public HttpWebResponse httpWebResponse { get; private set; }
        public T body { get; private set; }
        private HttpResponse() { }
        public long timestamp {get; set;}
        public short status {get; set;}
        public string error {get; set;}
        public string message {get; set;}
        public string path {get; set;}
        public string url { get; set; }
        public string method { get; set; }

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

        public HttpResponse(HttpWebResponse httpWebResponse, string responseString) 
        {
            this.httpWebResponse = httpWebResponse;
            this.body = JsonConvert.DeserializeObject<T>(responseString);
            this.status = (short)httpWebResponse.StatusCode;
        }
    }
}
