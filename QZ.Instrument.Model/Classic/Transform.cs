/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 * Some old models are still used, and need to be transfer to meet data contract.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using QZ.Instrument.Utility;
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    public static class Transform
    {
        public static List<Resp_Oc_Abs> To_Resp_Oc_Abs(this IEnumerable<OrgCompanyCombine> set, Company company, bool flag = true)
        {
            var list = new List<Resp_Oc_Abs>(company.pg_size);
            var end_list = new List<Resp_Oc_Abs>();
            
            var oc_name_input = company.oc_name?.Replace("公司", "");
            foreach (var c in set)
            {
                var r = new Resp_Oc_Abs();
                if (c != null)
                {
                    r.flag = c.od_CreateTime.Year != 1900;
                    r.oc_addr = c.oc_address ?? "";
                    r.oc_area = c.oc_area;
                    r.oc_code = c.oc_code;
                    r.oc_art_person = c.od_faRen ?? string.Empty;
                    r.oc_issue_time = c.oc_issuetime.ToString("yyyy-MM-dd") ?? "";
                    r.oc_name_hl = flag ? HighLighter.HighLight(c.oc_name, company.oc_name) : c.oc_name;
                    r.oc_name = c.oc_name;
                    r.oc_reg_capital = c.od_regMoney ?? "";
                    r.oe_status = c.oc_issuetime < DateTime.Now;
                    r.oc_type = c.oc_companytype ?? "";
                    r.oc_status = r.flag ? Util.To_OpStatus(c.od_ext) : "未知";
                }
                if (string.IsNullOrEmpty(oc_name_input))
                {
                    list.Add(r);
                }
                else
                {
                    bool b_existed = false;
                    for (int i = 0; i < oc_name_input.Length; i++)
                    {
                        if (c.oc_name.Contains(oc_name_input[i]))
                        {
                            list.Add(r);
                            b_existed = true;
                            break;
                        }
                    }
                    if (!b_existed)
                    {
                        end_list.Add(r);
                    }
                }
            }

            list.AddRange(end_list);
            return list;
        }

        public static List<Resp_Oc_Abs> Query2CompanyList(this IEnumerable<ES_Company> @enum, Company com)
        {
            var list = new List<Resp_Oc_Abs>(com.pg_size);
            foreach (var c in @enum)
            {
                var r = new Resp_Oc_Abs();
                if (c != null)
                {
                    r.flag = c.od_createtime.Year != 1900;
                    r.oc_addr = c.oc_address ?? "";
                    r.oc_area = c.oc_area;
                    r.oc_code = c.oc_code;
                    r.oc_art_person = c.od_faren ?? string.Empty;
                    r.oc_issue_time = c.oc_issuetime.ToString("yyyy-MM-dd") ?? "";
                    r.oc_name_hl = c.oc_name;
                    r.oc_name = c.oc_name;
                    r.oc_reg_capital = c.od_regmoney ?? "";
                    r.oe_status = c.oc_issuetime < DateTime.Now;
                    r.oc_type = c.oc_companytype ?? "";
                    r.oc_status = r.flag ? Util.To_OpStatus(c.od_ext) : "未知";
                }
                list.Add(r);
            }
            return list;
        }

        public static Brand_Abs To_Brand_Abs(this OrgCompanyBrand b)
        {
            var r = new Brand_Abs();
            r.status = b.ob_status ?? string.Empty;
            
            r.b_id = b.ob_id;
            r.oc_code = b.ob_oc_code ?? string.Empty;
            r.name = b.ob_name;
            //r.name_raw = b.ob_name.Replace("<font color =\"#FF4400\">", "").Replace("</font >", "");
            r.reg_no = b.ob_regNo;
            r.img = ConfigurationManager.AppSettings["brand_domain"] + b.ob_img;
            r.category = b.ob_class_name ?? string.Empty;
            r.cat_no = b.ob_classNo;//Constants.Brand_Classes.FirstOrDefault(p => p.Value == r.category).Key.ToString();
            if(string.IsNullOrEmpty(r.category))
            {
                int no = 0;
                if(int.TryParse(b.ob_classNo, out no))
                {
                    if (no > 0 && no < 46)
                        r.category = Constants.Brand_Cats[no -1];
                }
            }
            r.applicant = b.ob_proposer ?? string.Empty;
            if (b.ob_applicationDate.Year == 1900)
                r.application_date = "--";
            else
                r.application_date = b.ob_applicationDate.ToString("yyyy-MM-dd") ?? "--";
            return r;
        }
        public static Patent_Abs To_Patent_Abs(this CompanyPatent p)
        {
            var r = new Patent_Abs();
            r.applicant = p.Patent_sqr??string.Empty;
            r.application_date = p.sq_date.ToString("yyyy-MM-dd");
            r.name = p.Patent_Name ?? string.Empty;
            
            r.m_cat = p.Patent_gkh ?? string.Empty;
            r.p_no = p.Patent_No ?? string.Empty;
            r.p_id = p.ID;
            return r;
        }
        public static SearchHistoryInfo To_SearchHistoryInfo(this Company c)
        {
            var s = new SearchHistoryInfo();
            s.sh_oc_code = c.oc_code.To_Sql_Safe();
            s.sh_oc_area = c.oc_area.To_Sql_Safe();
            s.sh_oc_number = c.oc_number.To_Sql_Safe();
            s.sh_od_faRen = c.oc_art_person.To_Sql_Safe();
            s.sh_od_gd = c.oc_stock_holder.To_Sql_Safe();
            s.sh_oc_name = c.oc_name.To_Sql_Safe();
            s.sh_oc_address = c.oc_addr.To_Sql_Safe();
            s.sh_od_bussinessDes = c.oc_business.To_Sql_Safe();
            s.sh_od_regType = c.oc_reg_type.To_Sql_Safe();
            s.sh_od_ext = c.oc_ext.To_Sql_Safe();
            s.sh_u_uid = c.u_id.ToInt();
            s.sh_u_name = c.u_name;
            s.sh_searchType = (int)c.q_type + 1;
            s.sh_od_orderBy = (int)c.oc_sort;
            s.sh_od_regMUpper = decimal.Parse(c.oc_reg_capital_ceiling);
            s.sh_od_regMLower = decimal.Parse(c.oc_reg_capital_floor);
            s.sh_date = DateTime.Now;
            return s;
        }

        public static Resp_Company_Detail To_Company_Detail(this Tuple<OrgCompanyDtlInfo, OrgCompanyListInfo> t)
        {
            var c = new Resp_Company_Detail();
            if(t.Item1 != null)
            {
                var flag = t.Item1.od_CreateTime.Year != 1900;
                c.oc_code_s = Private_Util.To_Code_Display(t.Item1.od_oc_code) ?? string.Empty;
                c.oc_code = t.Item1.od_oc_code ?? string.Empty;
                c.oc_number = Private_Util.To_Number_Display(t.Item1.oc_number) ?? string.Empty;
                c.oc_name = t.Item1.oc_name ?? string.Empty;
                c.oc_addr = t.Item1.oc_address ?? string.Empty;
                c.oc_art_person = t.Item1.od_faRen ?? string.Empty;
                c.oc_business = t.Item1.od_bussinessDes ?? string.Empty;
                c.oc_reg_capital = t.Item1.od_regMoney ?? string.Empty;
                c.oc_paid_capital = t.Item1.od_factMoney ?? string.Empty;
                c.oc_reg_type = t.Item1.od_regType ?? string.Empty;
                c.oc_reg_date = t.Item1.od_regDate ?? string.Empty;
                c.oc_operate_time = flag ? (string.IsNullOrEmpty(t.Item1.od_bussinessS) ? "****" : t.Item1.od_bussinessS)
                    + " 至 "
                    + (string.IsNullOrEmpty(t.Item1.od_bussinessE) ? "永续经营" : t.Item1.od_bussinessE) : "";
                c.oc_check_date = t.Item1.od_chkDate;
                c.oc_annual_review = t.Item1.od_yearChk;
                // generating time of this record
                c.oc_create_time = t.Item1.od_CreateTime.ToString("yyyy-MM-dd HH:mm");
                c.oc_ext = t.Item1.od_ext ?? string.Empty;
                c.oc_status = Private_Util.Operation_Status_Get(c.oc_ext);
            }
            if(t.Item2 != null)
            {
                c.oc_detail_weburi = $"{ConfigurationManager.AppSettings["oc_detail_weburi"]}/?a=detail&c={HttpUtility.UrlEncode(Cipher_Aes.EncryptToBase64(t.Item2.oc_code, ConfigurationManager.AppSettings["code_key"]), Encoding.UTF8)}";
                if (string.IsNullOrEmpty(c.oc_code_s))
                {
                    c.oc_code_s = Private_Util.To_Code_Display(t.Item2.oc_code);
                    c.oc_code = t.Item2.oc_code;          
                }
                c.oc_area = t.Item2.oc_area ?? string.Empty;
                c.oc_area_name = t.Item2.oc_areaName ?? string.Empty;
                if (string.IsNullOrEmpty(c.oc_name))
                    c.oc_name = t.Item2.oc_name;
                
                c.oc_type = t.Item2.oc_companytype;
                c.oc_reg_name = t.Item2.oc_regOrgName;
                c.oc_number = t.Item2.oc_number;
                if (string.IsNullOrEmpty(c.oc_number))
                    c.oc_number = Private_Util.To_Number_Display(t.Item2.oc_number);
                c.oc_creditcode = t.Item2.oc_creditcode;
                if (string.IsNullOrEmpty(c.oc_addr))
                    c.oc_addr = t.Item2.oc_address;
                c.oc_valid_period = t.Item2.oc_issuetime.ToString("yyyy-MM-dd") + " 至 " + t.Item2.oc_invalidtime.ToString("yyyy-MM-dd"); // 有效期
                if(string.IsNullOrEmpty(c.oc_create_time))
                    c.oc_create_time = t.Item2.oc_createTime.ToString("yyyy-MM-dd HH:mm");
            }
            return c;
        }

        //private static string[] Cat_Names = {
        //    "化学原料",
        //    "颜料油漆",
        //    "日化用品",
        //    "燃料油脂",
        //    "医药",
        //    "金属材料",
        //    "机械设备",
        //    "手工器械",
        //    "科学仪器",
        //    "医疗器械",
        //    "灯具空调",
        //    "运输工具",
        //    "军火烟火",
        //    "珠宝钟表",
        //    "乐器",
        //    "办公品",
        //    "橡胶制品",
        //    "皮革皮具",
        //    "建筑材料",
        //    "家具",
        //    "厨房洁具",
        //    "绳网袋篷",
        //    "纱线丝",
        //    "布料床单",
        //    "服装鞋帽",
        //    "钮扣拉链",
        //    "地毯席垫",
        //    "键身器材",
        //    "食品",
        //    "方便食品",
        //    "饲料种籽",
        //    "啤酒饮料",
        //    "酒",
        //    "烟草烟具",
        //    "广告销售",
        //    "金融物管",
        //    "建筑修理",
        //    "通讯服务",
        //    "运输贮藏",
        //    "材料加工",
        //    "教育娱乐",
        //    "设计研究",
        //    "餐饮住宿",
        //    "医疗园艺",
        //    "社会法律"};

        public static Brand_Dtl To_Brand_Dtl(this OrgCompanyBrand_Dtl d)
        {
            var b = new Brand_Dtl();
            b.name = d.ob_name;
            b.reg_no = d.ob_regNo;
            b.agent = d.ob_dlrmc;
            b.applicant = d.ob_proposer;
            b.application_date = d.ob_applicationDate.ToShortDateString();
            var i = d.ob_classNo.ToInt();
            if(i > 0 && i < Constants.Brand_Cats.Length)
            {
                b.category = Constants.Brand_Cats[i - 1];
            }
            b.img = "domain" + d.ob_img;
            if(string.IsNullOrEmpty(d.oe_service))
            {
                b.services = new List<string>();
            }
            else
            {
                b.services = d.oe_service.Split(';').ToList();
                if(!string.IsNullOrEmpty(d.oe_serviceCode))
                {
                    var scs = d.oe_serviceCode.Split(';');
                    for(int j = 0; j < scs.Length; j++)
                    {
                        b.services[j] = b.services[j] + " (" + scs[j] + ")";
                    }
                }
            }
            b.process_list = new List<Brand_Process>();
            if (!string.IsNullOrEmpty(d.oe_brandProcess))
            {
                var bps = d.oe_brandProcess.Split(';');
                foreach(var bp in bps)
                {
                    var ds = bp.Split('-');
                    var p = new Brand_Process() { status = ds[0] };
                    if (ds.Length > 1)
                        p.date = ds[1];
                }
            }
            return b;
        }

        public static Patent_Dtl To_Patent_Dtl(this CompanyPatent p)
        {
            var d = new Patent_Dtl();
            d.p_id = p.ID;
            d.applicant = p.Patent_sqr ?? string.Empty;
            d.m_cat = p.Patent_gkh ?? string.Empty;
            //d.m_cat = p.Patent_lknflh;
            d.name = p.Patent_Name ?? string.Empty;
            //d.application_date = p.Patent_Day;
            //d.cat_no = p.Patent_flh;
            //d.grant_date = p.Patent_gkr;
            //d.inventer = p.Patent_sjr;
            d.img = ConfigurationManager.AppSettings["patent_domain"] + p.Patent_img;
            //d.detail = p.Patent_zy;
            d.p_no = p.Patent_No ?? string.Empty;
            d.status = p.Patent_Status ?? string.Empty;
            d.type = p.Patent_Type;
            d.application_date = p.patent_date;
            return d;
        }

        public static Judge_Abs To_Judge_Abs(this WenshuIndex w)
        {
            var j = new Judge_Abs();
            j.court = w.jd_ch;
            j.oc_code = w.oc_code;
            j.date = w.jd_date.ToString("yyyy-MM-dd");
            j.reference = w.jd_num;
            j.title = w.jd_title;
            j.title_raw = j.title.Replace("<font color=\"#FF4400\">", "").Replace("</font>", "");//<font color="#FF4400">石河子<font color="#FF4400">开发区<font color="#FF4400">天<font color="#FF4400">富<font color="#FF4400">房地产开发有限责任公司与解文庆房屋买卖合同纠纷一审民事判决书
            j.judge_id = w.jd_id;
            return j;
        }
        public static Dishonest_Abs To_Dishonest_Abs(this ShixinIndex s)
        {
            var d = new Dishonest_Abs();
            d.dh_id = s.sx_id;
            d.num = s.sx_caseCode;
            d.date = s.sx_publishDate.ToString("yyyy-MM-dd");
            d.court = s.sx_courtName;
            d.status = s.sx_performance;
            if (string.IsNullOrEmpty(s.sx_iname))   // natural person
            {
                if (!string.IsNullOrEmpty(s.sx_businessEntity))
                    d.name = s.sx_businessEntity;
            }
            else
            {
                d.name = s.sx_iname;
                if (!string.IsNullOrEmpty(s.sx_businessEntity))
                    d.person = s.sx_businessEntity;
            }
            return d;
        }
    }
}
