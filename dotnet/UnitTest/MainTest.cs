using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Lfshitong.Platform.Api.Entity;
using Com.Lfshitong.Platform.Api;

namespace UnitTest
{
    public class MainTest
    {
        protected Config config;
        public MainTest()
        {
            this.config = new Config("http://47.95.200.109:81/api-cs/", "bzjl", "123456");
        }
    }
}
