using StkjApiLibrary.service;
using StkjApiLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StkjApiLibrary.entity;
using System.Net;
namespace UnitTest
{
    
    
    /// <summary>
    ///这是 HttpServiceTest 的测试类，旨在
    ///包含所有 HttpServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class HttpServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///get 的测试
        ///</summary>
        public void getTestHelper<T>()
        {
            HttpRequest target = new HttpRequest(new Config("http://47.95.200.109/api-cs", "admin", "shitong")); // TODO: 初始化为适当的值
            string uri = string.Empty; // TODO: 初始化为适当的值
            Dictionary<string, object> param = null; // TODO: 初始化为适当的值


            HttpResponse<object> xxx = target.get<object>("/User/login", param);
            Assert.AreEqual(xxx.httpWebResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void getTest()
        {
            getTestHelper<GenericParameterHelper>();
            authTest();
        }

        /// <summary>
        ///auth 的测试
        ///</summary>
        public void authTest()
        {
            HttpRequest target = new HttpRequest(new Config("http://47.95.200.109:81/api-cs", "bzjl", "123456")); // TODO: 初始化为适当的值
            bool result = target.auth();
            Assert.AreEqual(result, true);
        }
    }
}
