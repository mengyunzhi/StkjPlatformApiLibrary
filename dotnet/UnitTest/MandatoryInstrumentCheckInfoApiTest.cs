using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.Lfshitong.Platform.Api.Entity;
using System.Net;
using Com.Lfshitong.Platform.Api;
using System.Collections.Generic;

namespace UnitTest
{
    
    
    /// <summary>
    ///这是 MandatoryInstrumentCheckInfoApiTest 的测试类，旨在
    ///包含所有 MandatoryInstrumentCheckInfoApiTest 单元测试
    ///</summary>
    [TestClass()]
    public class MandatoryInstrumentCheckInfoApiTest : MainTest
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
        ///uploadByTechnicalInstitution 的测试
        ///</summary>
        [TestMethod()]
        public void uploadByTechnicalInstitutionTest()
        {
            long id = 5703; // 检定ID
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo(id); 
          
            HttpResponse<object> result;
            result = MandatoryInstrumentCheckInfoApi.Back<object>(id, "测试原因");
            Assert.AreEqual(result.status, 200);

            result = MandatoryInstrumentCheckInfoApi.NotNeedVerificated<object>(id, "测试原因");
            Assert.AreEqual(result.status, 200);
        }

        /**
         * 批量退检
         * */
        [TestMethod]
        public void BatchBackTest() {
            List<long> instrumentCheckInfos = new List<long>();
            instrumentCheckInfos.Add(5703);
            instrumentCheckInfos.Add(5704);
            HttpResponse<object> result = MandatoryInstrumentCheckInfoApi.BatchBack<object>(instrumentCheckInfos, "这里写退回原因");
            Assert.AreEqual(result.status, 200);
        }

        /**
         * 批量不检
         * */
        [TestMethod]
        public void BatchNotNeedVerificatedTest()
        {
            List<long> instrumentCheckInfos = new List<long>();
            instrumentCheckInfos.Add(5705);
            instrumentCheckInfos.Add(5706);
            HttpResponse<object> result = MandatoryInstrumentCheckInfoApi.BatchNotNeedVerificated<object>(instrumentCheckInfos, "这里写不检原因");
            Assert.AreEqual(result.status, 200);
        }
    }
}
