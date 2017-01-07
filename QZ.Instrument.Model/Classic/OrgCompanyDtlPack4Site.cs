using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyDtlPack4Site
    {
        /// <summary>
        /// 机构代码对象
        /// </summary>
        public OrgCompanyListInfo list;

        /// <summary>
        /// 工商主体详细信息对象
        /// </summary>
        public OrgCompanyDtlInfo dtl;

        /// <summary>
        /// 股东列表
        /// </summary>
        public List<OrgCompanyGsxtDtlGDInfo> gsxtGdList;

        /// <summary>
        /// 深圳地区专用的股东列表
        /// </summary>
        public List<OrgCompanyDtlGDInfo> dtlGdList;

        /// <summary>
        /// 公示系统变更列表
        /// </summary>
        public List<OrgCompanyGsxtBgsxInfo> gsxtBgsxList;

        /// <summary>
        /// 深圳地区专用变更列表
        /// </summary>
        public List<OrgCompanyDtl_EvtInfo> dtlBgsxList;

        /// <summary>
        /// 年报网站集合
        /// </summary>
        public List<OrgCompanyGsxtWwInfo> nbSiteList;

        /// <summary>
        /// 公司年报集合
        /// </summary>
        public List<OrgCompanyGsxtNbInfo> nbInfoList;
    }
}
