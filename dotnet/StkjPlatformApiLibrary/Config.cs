using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Lfshitong.Platform.Api
{
    /**
     * 配置
     * */
    public class Config
    {
        // 地址
        public String uri{get;private set;}
        
        // 用户名
        public String username {get; private set;}

        // 密码
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
