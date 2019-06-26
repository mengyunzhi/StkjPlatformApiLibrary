using System;
using System.Collections.Generic;
using System.Text;
using StkjApiLibrary;

namespace StkjPlatformApi
{
    public class Main
    {
        public static Config config;
        public Main() {
        }

        public static void setConfig(Config config) 
        {
            Main.config = config;
        }

        public static Config getConfig()
        {
            return Main.config;
        }
    }
}
