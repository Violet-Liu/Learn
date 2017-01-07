using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Abstract info of exhibition
    /// </summary>
    public class ExhibitAbs
    {
        /// <summary>
        /// md5 16 of e_name
        /// </summary>
        public string e_md { get; set; }
        /// <summary>
        /// exhibit name
        /// </summary>
        public string e_name { get; set; }
        /// <summary>
        /// count of exhibition companies
        /// </summary>
        public int e_count { get; set; }
        /// <summary>
        /// 展厅
        /// </summary>
        public string e_hall { get; set; }
        /// <summary>
        /// 展会所属行业
        /// </summary>
        public string e_trade { get; set; }
        /// <summary>
        /// 展会日期
        /// </summary>
        public string e_date { get; set; } = "-";
        /// <summary>
        /// 展会所在展厅的展位
        /// </summary>
        public string e_booth { get; set; }
    }

    /// <summary>
    /// Detail of exhibition
    /// </summary>
    public class ExhibitDtl
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; } = "";
        /// <summary>
        /// 参展地区
        /// </summary>
        public string area { get; set; } = "";
        /// <summary>
        /// 展会行业
        /// </summary>
        public string trade { get; set; } = "";
        public string name { get; set; } = "";
        /// <summary>
        /// 展厅名
        /// </summary>
        public string hall { get; set; } = "";
        ///// <summary>
        ///// 展会文档路径
        ///// </summary>
        //public string file { get; set; }
    }

    public class ExhibitCompany
    {
        public string oc_name { get; set; } = "";
        public string oc_code { get; set; } = "";
        public string oc_addr { get; set; } = "";
        public string oc_tel { get; set; } = "";
        public string oc_fax { get; set; } = "";
        public string oc_mail { get; set; } = "";
        public string oc_site { get; set; } = "";
        public string oc_area { get; set; } = "";
    }

    public class Trade_Intelli_Tip
    {
        public IList<string> fwd_names = new List<string>();
        public IList<string> exh_names = new List<string>();
        public Dictionary<string, string> gb_trades = new Dictionary<string, string>();
        public Dictionary<string, string> pro_trades = new Dictionary<string, string>();

        public static Trade_Intelli_Tip Default()
        {
            var tip = new Trade_Intelli_Tip();
            tip.fwd_names.Add("暂无数据");
            tip.fwd_names.Add("暂无数据");
            tip.exh_names.Add("暂无数据");
            tip.exh_names.Add("暂无数据");
            tip.gb_trades.Add("01", "暂无数据");
            tip.gb_trades.Add("02", "暂无数据");
            tip.pro_trades.Add("01", "暂无数据");
            tip.pro_trades.Add("02", "暂无数据");
            return tip;
        }

}


public class Resp_Exhibit_List
    {
        public List<SearchExhibit> exhibits { get; set; }
        public long count { get; set; }
        public Exhibit_Agg aggs { get; set; }
        public static Resp_Company_List Default { get { return new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = 0 }; } }

        public string cost { get; set; }
    }

    public class SearchExhibit
    {
        public int e_showid { get; set; }
        public int e_year { get; set; }
        public string e_name { get; set; }
        public string e_trade { get; set; }
        public string e_hall { get; set; }
        public string e_start { get; set; }
        public int e_count { get; set; }
        public string e_namemd { get; set; }
        /// <summary>
        /// Highlight hits
        /// Key -> field name; Value -> field value with highlight html tag
        /// </summary>
        public Dictionary<string, string> hits { get; set; } = new Dictionary<string, string>();
    }

    public class Exhibit_Agg
    {
        /// <summary>
        /// 按省份统计
        /// </summary>
        public List<Agg_Monad> provinces { get; set; } = new List<Agg_Monad>();

        /// <summary>
        /// 按日期统计
        /// </summary>
        public List<Agg_Monad> dates { get; set; } = new List<Agg_Monad>();

        /// <summary>
        /// 按行业统计
        /// </summary>
        public List<Agg_Monad> trades { get; set; } = new List<Agg_Monad>();
    }

    public class Company4FavorBrowse
    {
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string action_date { get; set; }
        public bool fromBrowse { get; set; }

        public Company4FavorBrowse(string oc_code, string oc_name, string oc_area, string date, bool frombrowse = true)
        {
            this.oc_code = oc_code;
            this.oc_name = oc_name;
            this.oc_area = oc_area;
            this.action_date = date;
            this.fromBrowse = frombrowse;
        }
    }
    #region business state
    public class CertificationInfo
    {
        /// <summary>
        /// 自动编号[ci_id]
        /// </summary>
        public int ci_id { get; set; }
        /// <summary>
        /// 证书编号[ci_certNo]
        /// </summary>
        public string ci_certNo { get; set; }

        /// <summary>
        /// 证书状态[ci_certStatus]
        /// </summary>
        public string ci_certStatus { get; set; }

        /// <summary>
        /// 发证机构机构名称[ci_issuedCompanyName]
        /// </summary>
        public string ci_issuedCompanyName { get; set; }
        /// <summary>
        /// 证书到期日期[ci_expiredDate]
        /// </summary>
        public DateTime ci_expiredDate { get; set; }
        /// <summary>
        /// 认证项目[ci_certificationProgram]
        /// </summary>
        public string ci_certificationProgram { get; set; }
        /// <summary>
        /// 获证组织名称[ci_oc_name]
        /// </summary>
        public string ci_oc_name { get; set; }
        /// <summary>
        /// 获证组织机构代码[ci_oc_code]
        /// </summary>
        public string ci_oc_code { get; set; }
        /// <summary>
        /// 证书详情[ci_detailInfo]
        /// </summary>
        public string ci_detailInfo { get; set; }
        /// <summary>
        /// 证书详情url[ci_detailInfoUrl]
        /// </summary>
        public string ci_detailInfoUrl { get; set; }
        /// <summary>
        /// 插入数据库时间[ci_addTime]
        /// </summary>
        public DateTime ci_addTime { get; set; }
        /// <summary>
        /// 更新到数据库时间[ci_updateTime]
        /// </summary>
        public DateTime ci_updateTime { get; set; }
    }

    public class Certification
    {
        /// <summary>
        /// 自动编号[ci_id]
        /// </summary>
        public string ci_id { get; set; }
        /// <summary>
        /// 证书编号[ci_certNo]
        /// </summary>
        public string ci_certNo { get; set; }
        /// <summary>
        /// 获证组织机构代码[ci_oc_code]
        /// </summary>
        public string ci_oc_code { get; set; }
        /// <summary>
        /// 认证项目[ci_certificationProgram]
        /// </summary>
        public string ci_certificationProgram { get; set; }

        /// <summary>
        /// 证书状态[ci_certStatus]
        /// </summary>
        public string ci_certStatus { get; set; }

        /// <summary>
        /// 证书到期日期[ci_expiredDate]
        /// </summary>
        public DateTime ci_expiredDate { get; set; }
    }

    public class Resp_Certifications
    {
        public bool isSuccess { get; set; }
        public List<Certification> certifications { get; set; }
        public int count { get; set; }
        public int errorCode { get; set; }
    }

    public class Resp_Regs
    {
        public string catestr { get; set; }

        public OrgGS1ItemInfo regInfo { get; set; }
    }



    public class OrgGS1RegListInfo
    {
        /// <summary>
        /// 自增ID[ori_id]
        /// </summary>
        public int ori_id { get; set; }

        /// <summary>
        /// 机构代码[ori_oc_code]
        /// </summary>
        public string ori_oc_code { get; set; }

        /// <summary>
        /// 厂商编码[ori_code]
        /// </summary>
        public string ori_code { get; set; }

        /// <summary>
        /// 中国商品编码中心ID，临时存储，可能以后有用[ori_gsid]
        /// </summary>
        //public int ori_gsid { get; set; }

        /// <summary>
        /// 厂商名称[ori_name]
        /// </summary>
        public string ori_name { get; set; }

        /// <summary>
        /// 地址[ori_address]
        /// </summary>
        public string ori_address { get; set; }

        /// <summary>
        /// 联系人[ori_contactMan]
        /// </summary>
        public string ori_contactMa { get; set; }

        /// <summary>
        /// 联系电话[ori_contactPhone]
        /// </summary>
        public string ori_contactPhone { get; set; }
        /// <summary>
        /// 注销日期[ori_logoutDate]
        /// </summary>
        //public string ori_logoutDate { get; set; }

        /// <summary>
        /// 状态[ori_status]
        /// </summary>
        public string ori_status { get; set; }

        /// <summary>
        /// 生效日期[ori_validDate]
        /// </summary>
        //public DateTime ori_validDate { get; set; }

        /// <summary>
        /// 更新时间[ori_updatetime]
        /// </summary>
        //public DateTime ori_updatetime { get; set; }

        /// <summary>
        /// 创建时间[ori_createTime]
        /// </summary>
        //public DateTime ori_createTime { get; }

    }

    public class OrgGS1ItemInfo
    {
        /// <summary>
        /// 自增ID[ogs_id]
        /// </summary>
        public int ogs_id { get; set; }

        /// <summary>
        /// 厂商代码[ogs_ori_code]
        /// </summary>
        public string ogs_ori_code { get; set; }
        /// <summary>
        /// 机构代码[ogs_oc_code]
        /// </summary>
        public string ogs_oc_code { get; set; }

        /// <summary>
        /// 商品编码[ogs_code]
        /// </summary>
        public string ogs_code { get; set; }

        /// <summary>
        /// 警告次数（条码追溯系统）？[ogs_alermCount]
        /// </summary>
        public int ogs_alermCount { get; set; }

        /// <summary>
        /// 诚信次数（条码追溯系统）？[ogs_honestCount]
        /// </summary>
        public int ogs_honestCount { get; set; }

        /// <summary>
        /// 质检通知次数（条码追溯系统）？[ogs_qualificationCount]
        /// </summary>
        public int ogs_qualificationCount { get; set; }

        /// <summary>
        /// 召回次数（条码追溯系统）？[ogs_recallCount]
        /// </summary>
        public int ogs_recallCount { get; set; }

        /// <summary>
        /// 毛重[ogs_itemGrossWeight]
        /// </summary>
        public string ogs_itemGrossWeight { get; set; }

        /// <summary>
        /// 净容量[ogs_itemNetContent]
        /// </summary>
        public string ogs_itemNetContent { get; set; }
        /// <summary>
        /// 净重量[ogs_itemNetWeight]
        /// </summary>
        public string ogs_itemNetWeight { get; set; }

        /// <summary>
        /// 分类号[ogs_itemClassCode]
        /// </summary>
        public string ogs_itemClassCode { get; set; }

        /// <summary>
        /// 包装类别号[ogs_itemPackagingTypeCode]
        /// </summary>
        public string ogs_itemPackagingTypeCode { get; set; }

        /// <summary>
        /// 包装深度[ogs_itemDepth]
        /// </summary>
        public string ogs_itemDepth { get; set; }

        /// <summary>
        /// 包装高度[ogs_itemHeight]
        /// </summary>
        public string ogs_itemHeight { get; set; }

        /// <summary>
        /// 包装宽度[ogs_itemWidth]
        /// </summary>
        public string ogs_itemWidth { get; set; }

        /// <summary>
        /// 批次号（条码追溯系统）？[ogs_batch]
        /// </summary>
        public string ogs_batch { get; set; }
        /// <summary>
        /// 金属包装编码？（条码追溯系统）？[ogs_itemPackagingMaterialCode]
        /// </summary>
        public string ogs_itemPackagingMaterialCode { get; set; }

        /// <summary>
        /// 更新时间[ogs_updateTime]
        /// </summary>
        public DateTime ogs_updateTime { get; set; }

        /// <summary>
        /// 数据创建时间[ogs_createTime]
        /// </summary>
        public DateTime ogs_createTime { get; set; }

        /// <summary>
        /// 产品名称[ogs_itemName]
        /// </summary>
        public string ogs_itemName { get; set; }

        /// <summary>
        /// 品牌名[ogs_brandName]
        /// </summary>
        public string ogs_brandName { get; set; }

        /// <summary>
        /// 图片存储路径[ogs_imagePath]
        /// </summary>
        public string ogs_imagePath { get; set; }

        /// <summary>
        /// 原始图片URL地址[ogs_originalImageUrl]
        /// </summary>
        public string ogs_originalImageUrl { get; set; }
        /// <summary>
        /// 产品规格[ogs_itemSpecification]
        /// </summary>
        public string ogs_itemSpecification { get; set; }
        /// <summary>
        /// 图片描述[ogs_imageDescription]
        /// </summary>
        public string ogs_imageDescription { get; set; }
        /// <summary>
        /// 产品简述[ogs_itemShortDescription]
        /// </summary>
        public string ogs_itemShortDescription { get; set; }

        /// <summary>
        /// 产品描述[ogs_itemDescription]
        /// </summary>
        public string ogs_itemDescription { get; set; }

        /// <summary>
        /// 质检？[ogs_QS]
        /// </summary>
        public string ogs_QS { get; set; }

        /// <summary>
        /// 之前的产品信息？[ogs_productEx]
        /// </summary>
        public string ogs_productEx { get; set; }

        /// <summary>
        /// 防伪（条码追溯系统）？[ogs_productFangWei]
        /// </summary>
        public string ogs_productFangWei { get; set; }
        /// <summary>
        /// 保持记录？公司集合，（条码追溯系统）？[ogs_keepOnRecord]
        /// </summary>
        public string ogs_keepOnRecord { get; set; }

    }


    public class OrgGS1Item
    {
        /// <summary>
        /// 自增ID[ogs_id]
        /// </summary>
        public string ogs_id { get; set; }

        /// <summary>
        /// 产品名称[ogs_itemName]
        /// </summary>
        public string ogs_itemName { get; set; }

        // <summary>
        /// 品牌名[ogs_brandName]
        /// </summary>
        public string ogs_brandName { get; set; }
        /// <summary>
        /// 机构代码[ogs_oc_code]
        /// </summary>
        public string ogs_code { get; set; }

    }

    #endregion


}
