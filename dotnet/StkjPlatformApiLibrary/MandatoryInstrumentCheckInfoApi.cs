using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Com.Lfshitong.Platform.Api.Entity;
using Com.Lfshitong.Platform.Api.Service;
using Com.Lfshitong.Platform.Api;

namespace Com.Lfshitong.Platform.Api
{
    /**
     * 强检器具检定信息
     * */
    public class MandatoryInstrumentCheckInfoApi
    {
        private static string url = "/MandatoryInstrumentCheckInfo";
        private HttpRequest httpRequest;
        private MandatoryInstrumentCheckInfoApi(Config config)
        {
            this.httpRequest = HttpRequest.instanceByConfig(config);
        }

        /**
         * 退检
         * id 检定ID
         * 
         **/
        public static HttpResponse<T> Back<T>(long id, String backedReason)
        {
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo();
            instrumentCheckInfo.backedReason = backedReason;
            instrumentCheckInfo.acceptedStatus = InstrumentCheckInfo.STATUS_BACKED;
            HttpRequest httpRequest = HttpRequest.getInstance();
            HttpResponse<T> httpResponse = httpRequest.Put<T>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse;
        }

        public static HttpResponse<T> NotNeedVerificated<T>(long id, String backedReason)
        {
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo();
            instrumentCheckInfo.backedReason = backedReason;
            instrumentCheckInfo.acceptedStatus = InstrumentCheckInfo.STATUS_DO_NOT_NEED_CHECKED;
            HttpRequest httpRequest = HttpRequest.getInstance();
            HttpResponse<T> httpResponse = httpRequest.Put<T>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse;
        }

    }
}
