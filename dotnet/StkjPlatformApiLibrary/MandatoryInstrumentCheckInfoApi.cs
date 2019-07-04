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
        /**
         * 二级地址
         * */
        private static string url = "/MandatoryInstrumentCheckInfo";

        /**
         * 
         * 退检
         * id 检定ID
         * backedReason 退检原因
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

        /**
         * 不检
         * id 检定ID
         * backedReason 原因
         * 
         * */
        public static HttpResponse<T> NotNeedVerificated<T>(long id, String backedReason)
        {
            InstrumentCheckInfo instrumentCheckInfo = new InstrumentCheckInfo();
            instrumentCheckInfo.backedReason = backedReason;
            instrumentCheckInfo.acceptedStatus = InstrumentCheckInfo.STATUS_DO_NOT_NEED_CHECKED;
            HttpRequest httpRequest = HttpRequest.getInstance();
            HttpResponse<T> httpResponse = httpRequest.Put<T>(MandatoryInstrumentCheckInfoApi.url + "/uploadByTechnicalInstitution/" + id.ToString(), instrumentCheckInfo);
            return httpResponse;
        }

        /**
         * 批量退检
         * ids 检定记录ID
         * reason 退检原因
         * */
        public static HttpResponse<T> BatchBack<T>(List<long> ids, string reason)
        {
            return batchUpload<T>(ids, reason, InstrumentCheckInfo.STATUS_BACKED);
        }

        /**
         * 批量不检
         * ids 检定记录ID
         * reason 原因
         * 
         * */
        internal static HttpResponse<T> BatchNotNeedVerificated<T>(List<long> ids, string reason)
        {
            return batchUpload<T>(ids, reason, InstrumentCheckInfo.STATUS_DO_NOT_NEED_CHECKED);
        }

        /**
         * 批量上传检定结果
         * ids 检定记录ID
         * reason 原因
         * status 检定状态
         * 
         * */
        private static HttpResponse<T> batchUpload<T>(List<long> ids, string reason, sbyte status)
        {
            List<InstrumentCheckInfo> instrumentCheckInfos = new List<InstrumentCheckInfo>();
            foreach (long id in ids)
            {
                instrumentCheckInfos.Add(new InstrumentCheckInfo(id, reason, status));
            }

            HttpRequest httpRequest = HttpRequest.getInstance();
            return httpRequest.Put<T>(MandatoryInstrumentCheckInfoApi.url + "/batchUpload", instrumentCheckInfos);
        }
    }
}
