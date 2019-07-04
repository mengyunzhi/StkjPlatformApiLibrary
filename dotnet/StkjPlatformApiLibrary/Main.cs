using System;
using System.Collections.Generic;
using System.Text;
using Com.Lfshitong.Platform.Api;
using Com.Lfshitong.Platform.Api.Service;

namespace Com.Lfshitong.Platform.Api
{
    /**
     * 入口类
     * */
    public class Main
    {
        // 配置信息
        private static Config _config;
        private Main() {
        }

        public static bool Config(string url, string username, string password)
        { 
            return Main.Config(new Config(url, username, password));
        }

        /**
         * 设置配置信息
         * config 配置
         * */
        public static bool Config(Config config) 
        {
            Main._config = config;
            HttpRequest httpRequest = HttpRequest.instanceByConfig(config);
            return httpRequest.login;
        }

        /**
         * 获取当前配置
         * */
        public static Config Config()
        {
            return Main._config;
        }
    }
}
