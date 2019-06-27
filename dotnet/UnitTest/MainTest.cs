using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StkjApiLibrary.entity;
using StkjApiLibrary;

namespace UnitTest
{
    public class MainTest
    {
        protected Config config;
        public MainTest()
        {
            this.config = new Config("http://192.160.1.101:8010/api-cs/", "bzjl", "123456");
        }
    }
}
