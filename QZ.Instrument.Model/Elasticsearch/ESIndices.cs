/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class Es_Consts
    {
        #region previous generation
        /// <summary>
        /// 企业数据索引名
        /// </summary>
        public const string Enterprise_Idx = "orgcompany";
        /// <summary>
        /// 企业数据联合类型
        /// </summary>
        public const string Enterprise_Type = "combine";

        public const string Gsxt_Idx = "companygsxt";
        public const string Brand_Type_Old = "companybrand";
        public const string Patent_Idx = "companygsxt";//"compatent"; //; //  //
        public const string Patent_Type_Old = "companypatent";
        public const string Judge_Type_Old = "judgementdoc";
        public const string Dishonest_Type_Old = "shixin";
        #endregion

        #region next generation
        public const string Company_Index = "company_nextgen";
        public const string Company_Type = "company";
        public const string Trade_Type = "trade";

        public const string Company_Ext_Type = "company_ext";
        public const string Exhibit_Type = "exhibit";
        public const string Dishonest_Type = "dishonest";
        public const string Brand_Type = "brand";
        public const string Patent_Type = "patent";
        #endregion


        #region field name
        public const string Company_GBTrade = "gb_codes";
        public const string Company_ProTrade = "pro_codes";
        public const string Company_FwdTrade = "fwd_trades";
        public const string Company_ExhTrade = "exh_trades";
        #endregion
    }
}
