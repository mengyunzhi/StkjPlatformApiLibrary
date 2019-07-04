using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Com.Lfshitong.Platform.Api.Entity
{
    /**
     * 器具检定信息
     * */
    class InstrumentCheckInfo
    {
        public static sbyte STATUS_HAVE_NOT_ACCEPTED = 0;            // 未受理
        public static sbyte STATUS_ACCEPTED_AND_UN_CHECKED = 1;    // 已受理，未检定
        public static sbyte STATUS_CHECKED = 2;                    // 检毕
        public static sbyte STATUS_BACKED = -1;                    // 退检
        public static sbyte STATUS_DO_NOT_NEED_CHECKED = -2;        // 不检

        public long id { get; set; }

        //"证书编号")
        public String certificateNum { get; set; }

        //"应收费用 单位:分")
        public long cost { get; set; }

        //"检定日期")
        [JsonIgnore]
        public DateTime _checkDate { get; set;}

        //"有效期至")
        [JsonIgnore]
        public DateTime _validityDate { get; set; }

        //"证书状态 关联证书状态实体")
        // public CertificateStatus certificateStatus;

        //"证书类型 关联证书类型实体")
        //public InstrumentCertificateType instrumentCertificateType;

        //"检定机构 关联部门实体")
        //public Department checkDepartment;

        //"检定结果 关联检定结果实体")
        //public CheckResult checkResult;

        //"强检器具 关联强检器具使用信息实体")
        //public MandatoryInstrument mandatoryInstrument;

        //"检定申请。每次检定前，必须先提出检定申请。所以检定记录，必然对应检定申请")
        //public MandatoryInstrumentCheckApply mandatoryInstrumentCheckApply { get; set; }

        //"受理状态")
        public sbyte acceptedStatus { get; set; }

        //"备注")
        public string remarks { get; set; }

        //"退检或不检原因")
        public string backedReason { get; set; }

        public InstrumentCheckInfo()
        {

        }

        public InstrumentCheckInfo(long id)
        {
            this.id = id;
        }

        public InstrumentCheckInfo(long id, string backedReason, sbyte status)
        {
            this.id = id;
            this.backedReason = backedReason;
            this.acceptedStatus = status;
        }

    }
}
