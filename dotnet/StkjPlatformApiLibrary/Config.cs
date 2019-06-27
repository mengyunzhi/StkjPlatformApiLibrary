using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Lfshitong.Platform.Api
{
    public class Config
    {
        public String uri{get;private set;}
        public String username {get; private set;}
        public String password {get; private set;}
        private Config() { }
        
        public Config(String uri, String username, String password)
        {
            this.uri = uri;
            this.username = username;
            this.password = password;
        }
    }
}
