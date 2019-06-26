using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using StkjApiLibrary.entity;
using StkjApiLibrary.service;
using StkjPlatformApi;

namespace StkjApiLibrary
{
    /**
     * 强检器具检定信息
     * */
    class MandatoryInstrumentCheckInfoApi
    {
        private static string url = "/MandatoryInstrumentCheckInfo";
        private HttpService httpService;
        private MandatoryInstrumentCheckInfoApi(Config config)
        {
            this.httpService = new HttpService(Main.getConfig());
        }

        public static HttpStatusCode uploadByTechnicalInstitution(long id, InstrumentCheckInfo instrumentCheckInfo)
        {
            HttpService httpServer = new HttpService();
            HttpResponse<object> httpResponse = httpServer.put<object>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse.httpWebResponse.StatusCode;
        }
    }
}
