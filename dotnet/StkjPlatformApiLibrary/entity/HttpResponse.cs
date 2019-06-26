using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace StkjApiLibrary.entity
{
    class HttpResponse<T>
    {
        public HttpWebResponse httpWebResponse { get; private set; }
        public T body { get; private set; }
        private HttpResponse() { }
        public HttpResponse(HttpWebResponse httpWebResponse, string responseString) 
        {
            this.httpWebResponse = httpWebResponse;
            this.body = JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
