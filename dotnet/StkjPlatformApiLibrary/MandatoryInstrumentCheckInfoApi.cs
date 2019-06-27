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
        private HttpRequest httpRequest;
        private MandatoryInstrumentCheckInfoApi(Config config)
        {
            this.httpRequest = new HttpRequest(Main.getConfig());
        }

        /**
         * 退检
         * id 检定ID
         * 
         **/
        public static HttpResponse<T> back<T>(long id, String backedReason)
        {
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo();
            instrumentCheckInfo.backedReason = backedReason;
            instrumentCheckInfo.acceptedStatus = InstrumentCheckInfo.STATUS_BACKED;
            HttpRequest httpRequest = new HttpRequest();
            HttpResponse<T> httpResponse = httpRequest.put<T>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse;
        }

        public static HttpResponse<T> notNeedVerificated<T>(long id, String backedReason)
        {
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo();
            instrumentCheckInfo.backedReason = backedReason;
            instrumentCheckInfo.acceptedStatus = InstrumentCheckInfo.STATUS_DO_NOT_NEED_CHECKED;
            HttpRequest httpRequest = new HttpRequest();
            HttpResponse<T> httpResponse = httpRequest.put<T>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse;
        }

    }
}
