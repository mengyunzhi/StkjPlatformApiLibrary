using System;
using System.Collections.Generic;
using System.Text;
using Com.Lfshitong.Platform.Api;
using Com.Lfshitong.Platform.Api.Service;

namespace Com.Lfshitong.Platform.Api
{
    public class Main
    {
        private static Config _config;
        public Main() {
        }

        public static void Config(Config config) 
        {
            Main._config = config;
            HttpRequest.instanceByConfig(config);
        }

        public static Config Config()
        {
            return Main._config;
        }
    }
}
