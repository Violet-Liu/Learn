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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Configuration;
using Bogus;

namespace QZ.Instrument.Model
{
    [DataContract]
    public class Resp_Index
    {
        [DataMember(Name = "a_ver")]
        public string a_ver { get; set; }
        [DataMember(Name = "i_ver")]
        public string i_ver { get; set; }
        [DataMember(Name = "open_login_flag")]
        public bool Open_Login_Flag { get; set; }
        [DataMember(Name = "a_flag")]
        public bool A_Flag { get; set; }
        [DataMember(Name = "i_flag")]
        public bool I_Flag { get; set; }
        [DataMember(Name = "a_pack_addr")]
        public string A_Pack_Addr { get; set; }
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "ctag_flag")]
        public bool ctag_flag { get; set; }
        public Resp_Index SetToken(string token)
        {
            Token = token;
            return this;
        }

        public static Faker<Resp_Index> Generators { get; } =
            new Faker<Resp_Index>()
            .RuleFor(p => p.ctag_flag, p => bool.Parse(ConfigurationManager.AppSettings["ctag_flag"]))
            .RuleFor(p => p.a_ver, p => ConfigurationManager.AppSettings["a_ver"])
            .RuleFor(p => p.i_ver, p => ConfigurationManager.AppSettings["i_ver"])
            .RuleFor(p => p.Open_Login_Flag, p => bool.Parse(ConfigurationManager.AppSettings["open_login_flag"]))
            .RuleFor(p => p.A_Flag, p => bool.Parse(ConfigurationManager.AppSettings["a_flag"]))
            .RuleFor(p => p.I_Flag, p => bool.Parse(ConfigurationManager.AppSettings["i_flag"]))
            .RuleFor(p => p.A_Pack_Addr, p => ConfigurationManager.AppSettings["a_pack_addr"]);

        public static IEnumerable<Resp_Index> Resp_Indices { get; } =
            Generators.Generate(1);
    }

    public class Resp_Company_List
    {
        public List<Resp_Oc_Abs> oc_list { get; set; }
        public long count { get; set; }
        public Company_Agg aggs { get; set; }
        public static Resp_Company_List Default { get { return new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = 0 }; } }

        public string cost { get; set; }
    }
    /// <summary>
    /// company abstract infos, used as model of listview
    /// </summary>
    public class Resp_Oc_Abs
    {
        /// <summary>
        /// whether this company has detail info
        /// </summary>
        public bool flag { get; set; }
        public string oc_addr { get; set; } = string.Empty;
        public string oc_area { get; set; } = string.Empty;
        public string oc_code { get; set; } = string.Empty;
        /// <summary>
        /// company type
        /// </summary>
        public string oc_type { get; set; } = string.Empty;
        /// <summary>
        /// company issue time
        /// </summary>
        public string oc_issue_time { get; set; } = string.Empty;
        /// <summary>
        /// company name with highlight html tag
        /// </summary>
        public string oc_name_hl { get; set; } = string.Empty;
        public string oc_name { get; set; } = string.Empty;
        /// <summary>
        /// company state: true, if is normal
        /// </summary>
        public bool oe_status { get; set; }
        /// <summary>
        /// artificial person
        /// </summary>
        public string oc_art_person { get; set; } = string.Empty;
        /// <summary>
        /// register capital
        /// </summary>
        public string oc_reg_capital { get; set; } = string.Empty;
        public string oc_status { get; set; } = string.Empty;
        /// <summary>
        /// Highlight hits
        /// Key -> field name; Value -> field value with highlight html tag
        /// </summary>
        public Dictionary<string, string> hits { get; set; } = new Dictionary<string, string>();

        public List<string> gb_trades { get; set; }
    }
    /// <summary>
    /// 公司指标聚合统计
    /// </summary>
    public class Company_Agg
    {
        /// <summary>
        /// 按企业状态统计
        /// </summary>
        public List<Agg_Monad> statuses { get; set; } = new List<Agg_Monad>();
        /// <summary>
        /// 按注册资金统计
        /// </summary>
        public List<Agg_Monad> regms { get; set; } = new List<Agg_Monad>();
        /// <summary>
        /// 按发布日期统计
        /// </summary>
        public List<Agg_Monad> dates { get; set; } = new List<Agg_Monad>();
        /// <summary>
        /// 按地区统计
        /// </summary>
        public List<Agg_Monad> areas { get; set; } = new List<Agg_Monad>();
        /// <summary>
        /// 按行业统计
        /// </summary>
        public List<Agg_Monad> trades { get; set; } = new List<Agg_Monad>();
        /// <summary>
        /// 按公司类型统计
        /// </summary>
        public List<Agg_Monad> types { get; set; } = new List<Agg_Monad>();
    }
    public class Agg_Monad
    {
        /// <summary>
        /// 标签，用户UI控件显示
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 某分类对应的值，用于筛选查询
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 某分类统计数量
        /// </summary>
        public long count { get; set; }

        public Agg_Monad() { }
        public Agg_Monad(string label, string value, long count)
        {
            this.label = label;
            this.value = value;
            this.count = count;
        }
    }

    /// <summary>
    /// Info for company detail
    /// </summary>
    public class Resp_Company_Detail
    {
        public List<string> tel_list { get; set; } = new List<string>();
        public Dictionary<string, string> gb_trades { get; set; } = new Dictionary<string, string>();
        public string oc_code { get; set; } = string.Empty;
        public string oc_area { get; set; } = string.Empty;
        /// <summary>
        /// using for UI showing
        /// </summary>
        public string oc_code_s { get; set; } = string.Empty;
        public string oc_number { get; set; } = string.Empty;
        public string oc_name { get; set; } = string.Empty;
        public string oc_addr { get; set; } = string.Empty;
        public string oc_art_person { get; set; } = string.Empty;
        /// <summary>
        /// company business
        /// </summary>
        public string oc_business { get; set; } = string.Empty;
        
        public string oc_reg_capital { get; set; } = string.Empty;
        public string oc_paid_capital { get; set; } = string.Empty;
        public string oc_reg_type { get; set; } = string.Empty;
        public string oc_reg_date { get; set; } = string.Empty;
        public string oc_check_date { get; set; } = string.Empty;
        /// <summary>
        /// company operate time
        /// </summary>
        public string oc_operate_time { get; set; } = string.Empty;
        public string oc_annual_review { get; set; } = string.Empty;
        public string oc_create_time { get; set; } = string.Empty;
        public string oc_ext { get; set; } = string.Empty;
        public string oc_detail_weburi { get; set; } = string.Empty;
        /// <summary>
        /// 经营状态
        /// </summary>
        public string oc_status { get; set; } = string.Empty;
        public string oc_area_name { get; set; } = string.Empty;
        /// <summary>
        /// company type
        /// </summary>
        public string oc_type { get; set; } = string.Empty;
        /// <summary>
        /// registered orgonization name
        /// </summary>
        public string oc_reg_name { get; set; } = string.Empty;
        public string oc_valid_period { get; set; } = string.Empty;
        public List<Company_Member> oc_member_list { get; set; }

        public static Resp_Company_Detail Default { get { return new Resp_Company_Detail() { oc_member_list = new List<Company_Member>() }; } }
    }

    public class Company_Member
    {
        public string oc_member_name { get; set; } = string.Empty;
        public string oc_member_position { get; set; } = string.Empty;

        public byte oc_member_status { get; set; }
    }

    public class Resp_Intelli_Tip
    {
        public List<Company_Mini_Info> tip_list { get; set; }
        public static Resp_Intelli_Tip Default { get { return new Resp_Intelli_Tip() { tip_list = new List<Company_Mini_Info>() }; } }
    }
    public class Company_Mini_Info
    {
        public string oc_code { get; set; } = string.Empty;
        public string oc_name { get; set; } = string.Empty;
        public string oc_area { get; set; } = string.Empty;

        public static Company_Mini_Info Default
        {
            get
            {
                return new Company_Mini_Info() { oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403" };
            }
        }
    }
    /// <summary>
    /// company investment
    /// </summary>
    public class Resp_Invest
    {
        public List<Company_Mini_Info> invest_list { get; set; }
        public static Resp_Invest Default { get { return new Resp_Invest() { invest_list = new List<Company_Mini_Info>() }; } }
    }
    /// <summary>
    /// company map
    /// </summary>
    public class Resp_Oc_Map
    {
        public string map_html { get; set; } = string.Empty;
        public int map_node_count { get; set; }
        public int map_link_count { get; set; }
        public int map_depth { get; set; }
        public static Resp_Oc_Map Default { get { return new Resp_Oc_Map(); } }
    }

    /// <summary>
    /// response of querying for company stock holder
    /// </summary>
    public class Resp_Oc_Sh
    {
        public List<Company_Sh> sh_list { get; set; }
        public static Resp_Oc_Sh Default { get { return new Resp_Oc_Sh() { sh_list = new List<Company_Sh>() }; } }
    }
    /// <summary>
    /// profile of company stock holder
    /// </summary>
    public class Company_Sh
    {
        /// <summary>
        /// name of stock holder
        /// </summary>
        public string sh_name { get; set; } = string.Empty;
        /// <summary>
        /// how much money the stock holder provides
        /// </summary>
        public decimal sh_money { get; set; }
        /// <summary>
        /// category of the stock holder
        /// </summary>
        public string sh_cat { get; set; } = string.Empty;
        public string sh_type { get; set; } = string.Empty;
        /// <summary>
        /// ratio of money, the stock holder provides
        /// </summary>
        public decimal sh_money_ratio { get; set; }
        /// <summary>
        /// unit of money
        /// </summary>
        public string sh_money_unit { get; set; } = string.Empty;
        public string oc_code { get; set; } = string.Empty;
        public string oc_area { get; set; } = string.Empty;
        public byte sh_status { get; set; }
    }

    public class Resp_Oc_Change
    {
        public List<Oc_Change> change_list { get; set; }
        public static Resp_Oc_Change Default { get { return new Resp_Oc_Change() { change_list = new List<Oc_Change>() }; } }
    }
    public class Oc_Change
    {
        public string date { get; set; } = string.Empty;
        public List<Change_Item> item_list { get; set; }
    }
    public class Change_Item
    {
        public string item_name { get; set; }
        public string item_pre { get; set; }
        public string item_post { get; set; }
    }
    public class Resp_Query_Hot
    {
        public List<Query_Hot> hot_list { get; set; }
        public static List<Query_Hot> Default
        {
            get
            {
                return new List<Query_Hot>()
                {
                    new Query_Hot(),    /* 华为，腾讯，小米等数据 */
                    new Query_Hot(),
                    new Query_Hot()
                };   
            }
        }
    }
    public class Query_Hot
    {
        public string title { get; set; } = string.Empty;
        public string oc_code { get; set; } = string.Empty;
        public string oc_name { get; set; } = string.Empty;
        public string oc_area { get; set; } = string.Empty;
    }
    public class Resp_Icpl
    {
        public List<Company_Icpl> icpl_list { get; set; }
        public static Resp_Icpl Default { get { return new Resp_Icpl() { icpl_list = new List<Company_Icpl>() }; } }
    }
    public class Company_Icpl
    {
        public DateTime icpl_check_time { get; set; }
        public DateTime icpl_create_time { get; set; }
        public string icpl_domain { get; set; }
        public string icpl_ext { get; set; }
        public string icpl_host { get; set; }
        public string icpl_type { get; set; }
        public int icpl_id { get; set; }
        public string icpl_number { get; set; }
        public string oc_code { get; set; }
        public string icpl_uri { get; set; }
        public string icpl_name { get; set; }
        public string icpl_operate_status { get; set; }
    }
    public class Resp_Branch
    {
        public List<Company_Mini_Info> branch_list { get; set; }
        public static Resp_Branch Default { get { return new Resp_Branch() { branch_list = new List<Company_Mini_Info>() }; } }
    }
    public class Resp_Annual_Abs
    {
        public List<Company_Annual_Abs> annual_list { get; set; }
        public static Resp_Annual_Abs Default { get { return new Resp_Annual_Abs() { annual_list = new List<Company_Annual_Abs>() }; } }
    }
    public class Company_Annual_Abs
    {
        public string year { get; set; }
        public string create_time { get; set; }
        public string oc_status { get; set; }
    }

    public class Company_Annual_Dtl
    {
        public Annual_Company annual { get; set; }
        public List<Annual_Oc_Warranty> warranty_list { get; set; }
        public List<Annual_Oc_Invest> invest_list { get; set; }
        public List<Annual_Sh_Contribute> sh_contribute_list { get; set; }
        public List<Annual_Stock_Change> stock_change_list { get; set; }
        public List<Annual_Oc_Site> site_list { get; set; }
        public List<Annual_Oc_Change> change_list { get; set; }
        public static Company_Annual_Dtl Default
        {
            get
            {
                return new Company_Annual_Dtl()
                {
                    annual = new Annual_Company(),
                    warranty_list = new List<Annual_Oc_Warranty>(),
                    invest_list = new List<Annual_Oc_Invest>(),
                    sh_contribute_list = new List<Annual_Sh_Contribute>(),
                    stock_change_list = new List<Annual_Stock_Change>(),
                    site_list = new List<Annual_Oc_Site>(),
                    change_list = new List<Annual_Oc_Change>()
                };
            }
        }
        public static Company_Annual_Dtl Candidate
        {
            get
            {
                return new Company_Annual_Dtl();
            }
        }
    }
    public class Annual_Company
    {
        public string oc_code { get; set; } = string.Empty;
        public string year { get; set; } = string.Empty;
        public string oc_name { get; set; } = string.Empty;
        public string oc_tel { get; set; } = string.Empty;
        public string oc_post { get; set; } = string.Empty;
        public string oc_addr { get; set; } = string.Empty;
        public string oc_email { get; set; } = string.Empty;
        public bool flag_stock_transfer { get; set; }
        /// <summary>
        /// operation status
        /// </summary>
        public string oc_status { get; set; } = string.Empty;
        public bool flag_website { get; set; }
        /// <summary>
        /// true, if there is other stocks
        /// </summary>
        public bool flag_other_stock { get; set; }
        public string oc_worker_count { get; set; } = string.Empty;
        public string oc_assets { get; set; } = string.Empty;
        /// <summary>
        /// net value
        /// </summary>
        public string oc_equity_total { get; set; } = string.Empty;
        public string oc_income_total { get; set; } = string.Empty;
        public string oc_profit_total { get; set; } = string.Empty;
        public string oc_profit_net { get; set; } = string.Empty;
        public string oc_income_main { get; set; } = string.Empty;
        public string oc_tax_total { get; set; } = string.Empty;
        public string oc_debt_total { get; set; } = string.Empty;
        public string oc_number { get; set; } = string.Empty;
        /// <summary>
        /// annual create time
        /// </summary>
        public DateTime create_time { get; set; } = new DateTime();
    }

    public class Annual_Oc_Warranty
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        public string creditor { get; set; }
        public string debtor { get; set; }
        public string credit_cat { get; set; }
        public string credit_amount { get; set; }
        public string credit_period { get; set; }
        public string warranty_period { get; set; }
        public string warranty_style { get; set; }
        public string warranty_range { get; set; }
    }
    public class Annual_Oc_Invest
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        /// <summary>
        /// name of invest company
        /// </summary>
        public string invest_com { get; set; }
        /// <summary>
        /// register number
        /// </summary>
        public string reg_num { get; set; }
    }
    /// <summary>
    /// contribution of stock holder
    /// </summary>
    public class Annual_Sh_Contribute
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        public string stock_holder { get; set; }
        public string subscribe_capital { get; set; }
        public DateTime subscribe_time { get; set; }
        public string subscribe_style { get; set; }
        public string paid_contribute { get; set; }
        public DateTime contribute_time { get; set; }
        public string contribute_style { get; set; }
    }
    public class Annual_Stock_Change
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        public string stock_holder { get; set; }
        public string stock_ratio_pre { get; set; }
        public string stock_ratio_post { get; set; }
        public DateTime stock_change_time { get; set; }
    }
    public class Annual_Oc_Site
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public string website { get; set; }
        public string name { get; set; }

    }
    public class Annual_Oc_Change
    {
        public string oc_code { get; set; }
        public string year { get; set; }
        public string change_item { get; set; }
        public string change_pre { get; set; }
        public string change_post { get; set; }
        public DateTime change_date { get; set; }
    }
    public class Company_Impression
    {
        public bool favorite_flag { get; set; }
        public int favorite_count { get; set; }
        public Topic_Up_Down up_down_flag { get; set; }
        public int up_count { get; set; }
        public int topic_count { get; set; }
        public int down_count { get; set; }
    }
    public class Comment_State
    {
        /// <summary>
        /// here the file is image
        /// </summary>
        public File_Upload_State File_State { get; set; }
        /// <summary>
        /// topic or reply state
        /// </summary>
        public TopicReply_State T_R_State { get; set; }

        /// <summary>
        /// count of images whose uris were insert into database successfully
        /// </summary>
        public int Count { get; set; }
    }

    public enum Topic_Up_Down
    {
        None = 0,
        Up = 1,
        Down = 2
    }

    /// <summary>
    /// response of contains binary status denotes that operation is success or failed
    /// </summary>
    public class Resp_Binary
    {
        public bool status { get; set; }
        public string remark { get; set; }

        public static Resp_Binary Default
        {
            get { return new Resp_Binary() { remark = "操作失败" }; }
        }
    }

    public class Resp_Topics_Abs
    {
        public List<Topic_Abs> topic_list { get; set; }
        public int count { get; set; }
        public static Resp_Topics_Abs Default { get { return new Resp_Topics_Abs() { topic_list = new List<Topic_Abs>() }; } }
    }

    public class Topic_Abs
    {
        public string topic_tag { get; set; }
        /// <summary>
        /// 是否被屏蔽
        /// </summary>
        public bool t_shield { get; set; }
        public int topic_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        /// <summary>
        /// 发帖人id
        /// </summary>
        public string u_id { get; set; }
        /// <summary>
        /// 发帖人名
        /// </summary>
        public string u_name { get; set; }
        public string u_face { get; set; }
        public string topic_content { get; set; }
        public DateTime topic_date { get; set; }
        public string topic_gentle_time { get; set; }
    }

    public class Resp_Topics_Dtl
    {
        public List<Topic_Dtl> topic_list { get; set; }
        public int count { get; set; }
        public static Resp_Topics_Dtl Default { get { return new Resp_Topics_Dtl { topic_list = new List<Topic_Dtl>() }; } }
    }

    public class Resp_Cm_Topics_Dtl
    {
        public List<Cm_Topic_Dtl> topic_list { get; set; }
        public int count { get; set; }
        public static Resp_Cm_Topics_Dtl Default { get { return new Resp_Cm_Topics_Dtl { topic_list = new List<Cm_Topic_Dtl>() }; } }
    }
    public class Topic_Dtl : Topic_Abs
    {
        public int up_count { get; set; }
        public int down_count { get; set; }
        public Topic_Up_Down up_down_flag { get; set; }
        /// <summary>
        /// image uris below this topic
        /// </summary>
        public List<string> pic_list { get; set; }
        //public List<Reply_Dtl> reply_list { get; set; }
        public int reply_count { get; set; }

        /// <summary>
        /// used for special case
        /// </summary>
        public bool status { get; set; }
    }
    public class Cm_Topic_Dtl
    {
        public string topic_tag { get; set; } = "0";
        public int topic_id { get; set; }
        /// <summary>
        /// 是否屏蔽topic
        /// </summary>
        public bool t_shield { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string u_face { get; set; }
        public string topic_content { get; set; }
        public DateTime topic_date { get; set; }
        public string topic_gentle_time { get; set; }
        public int reply_count { get; set; }
        public int up_count { get; set; }
        public int down_count { get; set; }
        public Topic_Up_Down up_down_flag { get; set; }
        /// <summary>
        /// image uris below this topic
        /// </summary>
        public List<string> pic_list { get; set; }
        /// <summary>
        /// used for special case
        /// </summary>
        public bool status { get; set; }
    }
    public class Topic_Dtl_Comparer : IComparer<Topic_Dtl>
    {
        public int Compare(Topic_Dtl x, Topic_Dtl y)
        {
            if (x.topic_date == null && y.topic_date == null) return 0;
            if (x.topic_date == null) return -1;
            if (y.topic_date == null) return 1;

            var diff = (x.topic_date - y.topic_date).Milliseconds;
            if (diff > 0)
                return 1;
            else if (diff < 0)
                return -1;
            return 0;
        }
    }

    public class Reply_Dtl
    {
        public int reply_id { get; set; }
        public int topic_id { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string u_face { get; set; }
        public string reply_content { get; set; }
        public DateTime reply_date { get; set; }
        public string reply_gentle_time { get; set; }
        public List<string> pic_list { get; set; }
    }



    public class News
    {
        public string n_id { get; set; }
        public string n_cat_id { get; set; }
        public string n_title { get; set; }
        public string n_summery { get; set; }
        public string n_tags { get; set; }
        public string n_date { get; set; }
        public string n_type { get; set; }
        public string n_pic_0 { get; set; }
        public string n_pic_1 { get; set; }
    }


    public class Resp_Login
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string u_email { get; set; }
        public string u_face { get; set; }
        public string token { get; set; }
        public Login_State state { get; set; }
    }

    public class User_Append_Info
    {
        public string u_name { get; set; } = string.Empty;
        public string u_face { get; set; } = string.Empty;
        public int u_name_count { get; set; }
        public string u_email { get; set; } = string.Empty;
        public bool u_email_status { get; set; }
        public string u_position { get; set; } = string.Empty;
        public string u_company { get; set; } = string.Empty;
        public string u_business { get; set; } = string.Empty;
        public string pos_favor { get; set; } = string.Empty;
        public string bus_favor { get; set; } = string.Empty;
        public string u_signature { get; set; } = string.Empty;
    }

    public class Resp_User_Info_Set : Resp_Binary
    {
        public string ext { get; set; } = string.Empty;
        public static new Resp_User_Info_Set Default { get { return new Resp_User_Info_Set() { remark = "操作失败" }; } }
    }

    public class Resp_Favorites
    {
        public List<Favorite_Log> favorite_list { get; set; }
        public int count { get; set; }
        public static Resp_Favorites Default
        {
            get { return new Resp_Favorites() { favorite_list = new List<Favorite_Log>() }; }
        }
    }


    public class Resp_Oc_Notice
    {
        public List<Oc_Notice> notice_list { get; set; }
        public int count { get; set; }
        public static Resp_Oc_Notice Default { get { return new Resp_Oc_Notice() { notice_list = new List<Oc_Notice>() }; } }
    }
    public class Oc_Notice
    {
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public bool read_flag { get; set; }
        public string notice_date { get; set; }
    }
    public class Resp_Topic_Notice
    {
        public List<Topic_Notice> notice_list { get; set; }
        public int count { get; set; }
        public static Resp_Topic_Notice Default
        {
            get {
                return new Resp_Topic_Notice()
                {
                    notice_list = new List<Topic_Notice>()
                };
            }
        }
    }
    public class Topic_Notice
    {
        public int notice_id { get; set; }
        public bool notice_status { get; set; }
        public int topic_id { get; set; }
        /// <summary>
        /// 0 -> company topic; 1 -> community topic
        /// </summary>
        public int topic_type { get; set; }
        public string topic_tag { get; set; }
        /// <summary>
        /// count of unread message
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// user who post this topic
        /// </summary>
        public string u_id { get; set; }
        /// <summary>
        /// user who post this topic
        /// </summary>
        public string u_name { get; set; }
        /// <summary>
        /// user who post this topic
        /// </summary>
        public string u_face { get; set; }
        public string topic_content { get; set; }
        public string topic_gentle_time { get; set; }
        public DateTime topic_date { get; set; }
        public int up_count { get; set; }
        public int down_count { get; set; }
        public Topic_Up_Down up_down_flag { get; set; }
        public int reply_count { get; set; }
        public List<string> pic_list { get; set; }
        /// <summary>
        /// company topic append info
        /// </summary>
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
    }

    public class Resp_Brands
    {
        public List<Brand_Abs> brand_list { get; set; }
        public Dictionary<string, long> class_agg { get; set; }
        public long count { get; set; }
        public static Resp_Brands Default
        {
            get { return new Resp_Brands() { brand_list = new List<Brand_Abs>() }; }
        }
    }
    public class Brand_Abs : Company_Mini_Info
    {
        public int b_id { get; set; }
        public string name { get; set; } = string.Empty;    /* 中文名 */
        public string name_raw { get { return name.Replace("<font color=\"#FF4400\">", "").Replace("</font>", ""); } }
        //public string en_name { get; set; }     /* 英文名 */
        public string status { get; set; } = string.Empty;     /* 状态 */
        public string applicant { get; set; } = string.Empty;   /* 申请人 */
        public string reg_no { get; set; } = string.Empty;     /* 注册号 */
        public string cat_no { get; set; } = string.Empty;
        public string application_date { get; set; } = string.Empty;   /* 申请日期 */
        public string category { get; set; } = string.Empty;   /* 类别 */
        public string img { get; set; } = string.Empty;
    }

    public class Resp_Brand_Dtls
    {
        public List<Brand_Dtl> brand_list { get; set; }
        public static Resp_Brand_Dtls Default { get { return new Resp_Brand_Dtls() { brand_list = new List<Brand_Dtl>() }; } }
    }
    public class Brand_Dtl : Brand_Abs
    {
        public List<Brand_Process> process_list { get; set; }
        public string agent { get; set; } = string.Empty;
        public List<string> services { get; set; }
        public string time_limit { get; set; } = string.Empty; /* time limit for using */

        public static Brand_Dtl Default { get { return new Brand_Dtl() { process_list = new List<Brand_Process>(), services = new List<string>() }; } }
    }
    public class Brand_Process
    {
        public string date { get; set; }    /* one date in the brand handling process */
        public string status { get; set; }  /* status at the date */
    }
    public class Resp_Patents
    {
        public List<Patent_Abs> patent_list { get; set; }
        public Dictionary<string, long> type_agg { get; set; }
        //public List<YearCount> year_agg { get; set; }
        public Dictionary<string, long> year_agg { get; set; }
        public long count { get; set; }
        // public long count2 { get; set; }
        public static Resp_Patents Default
        {
            get { return new Resp_Patents() { patent_list = new List<Patent_Abs>() }; }
        }
    }
    public class YearCount
    {
        public string year { get; set; }
        public long count { get; set; }
        public YearCount(string year, long count)
        {
            this.year = year;
            this.count = count;
        }
    }
    public class YearCountComparer : IComparer<YearCount>
    {
        public int Compare(YearCount x, YearCount y)
        {
            return x.year.CompareTo(y.year);
        }
    }
    public class Patent_Abs : Company_Mini_Info
    {
        /// <summary>
        /// patent id
        /// </summary>
        public int p_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string name_raw { get { return name.Replace("<font color=\"#FF4400\">", "").Replace("</font>", ""); } }
        /// <summary>
        /// 申请人
        /// </summary>
        public string applicant { get; set; } = string.Empty;
        /// <summary>
        /// 对应于 公开号
        /// </summary>
        public string m_cat { get; set; } = string.Empty;   /* 主分类号 */
        /// <summary>
        /// patent number
        /// </summary>
        public string p_no { get; set; } = string.Empty;
        public string application_date { get; set; } = string.Empty;
    }
    //public class Patent_Dtl
    //{
    //    public string status { get; set; }
    //    public string proxy { get; set; }
    //    public string priority { get; set; }
    //    public string apply_no { get; set; }
    //    public string publish_date { get; set; }
    //}

    public class Resp_Patent_Dtls
    {
        public List<Patent_Dtl> patent_list { get; set; }
        public static Resp_Patent_Dtls Default { get { return new Resp_Patent_Dtls() { patent_list = new List<Patent_Dtl>() }; } }
    }
    public class Patent_Dtl : Patent_Abs
    {
        public string img { get; set; } = string.Empty;
        public string application_no { get; set; } = string.Empty;
        //public string grant_no { get; set; } => m_cat
        public string grant_date { get; set; } = string.Empty;
        public string inventer { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        /// <summary>
        /// 优先权
        /// </summary>
        public string priority { get; set; } = string.Empty;
        public string detail { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string addr { get; set; } = string.Empty;
        public static Patent_Dtl Default { get { return new Patent_Dtl(); } }
    }

    public class Resp_Judges
    {
        public List<Judge_Abs> judge_list { get; set; }
        public long count { get; set; }
        public static Resp_Judges Default { get { return new Resp_Judges() { judge_list = new List<Judge_Abs>() }; } }
    }
    public class Judge_Abs : Company_Mini_Info
    {
        public string title { get; set; }
        public string title_raw { get; set; }
        public string court { get; set; }
        public string reference { get; set; }
        public string date { get; set; }
        public string judge_id { get; set; }
    }
    public class Judge_Dtl
    {
        public string content { get; set; }
        public string jdg_oc_code { get; set; }
        public List<Company_Mini_Info> company_infos { get; set; }
    }
    public class Resp_Dishonests
    {
        public List<Dishonest_Abs> dishonest_list { get; set; }
        public Dictionary<string, long> area_agg { get; set; }
        public long count { get; set; }
        public static Resp_Dishonests Default { get { return new Resp_Dishonests() { dishonest_list = new List<Dishonest_Abs>() }; } }
        //public
    }
    public class Dishonest_Abs
    {
        public int dh_id { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string name { get; set; }
        public string name_raw { get { return name.Replace("<font color=\"#FF4400\">", "").Replace("</font>", ""); } }
        /// <summary>
        /// 相关责任人
        /// </summary>
        public string person { get; set; } = string.Empty;
        /// <summary>
        /// 立案时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 执行法院
        /// </summary>
        public string court { get; set; }
        /// <summary>
        /// 案号
        /// </summary>
        public string num { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }

    public class Resp_Copyrights
    {
        public List<Software_Abs> sw_list { get; set; }
        public int sw_count { get; set; }
        public List<Product_Abs> p_list { get; set; }
        public int p_count { get; set; }
    }
    public class Software_Abs
    {
        //public int id { get; set; }
        public string name { get; set; }
        public string s_name { get; set; }
        public string reg_no { get; set; }
        public string reg_date { get; set; }
        public string cat_no { get; set; }
        public string author { get; set; }
    }
    public class Product_Abs
    {
        //public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string reg_no { get; set; }
        public string reg_date { get; set; }
        public string finish_date { get; set; }
        public string publish_date { get; set; }
        public string author { get; set; }
    }
    public class Software_Ext
    {
        /// <summary>
        /// category number
        /// </summary>
        public string cat_no { get; set; }
        public string version { get; set; }
        /// <summary>
        /// author of copyright
        /// </summary>
        public string author { get; set; }
        public string country { get; set; }
    }

    public class Dishonest_Dtl : Company_Mini_Info
    {
        /// <summary>
        /// natural person -> id card number; company -> oc_code
        /// </summary>
        public string code { get; set; } = string.Empty;
        public string province { get; set; } = string.Empty;
        /// <summary>
        /// execution number
        /// </summary>
        public string exe_no { get; set; } = string.Empty;
        public string approvel_unit { get; set; } = string.Empty;
        public string duty { get; set; } = string.Empty;
        public string disrupt { get; set; } = string.Empty;
        public string publish_date { get; set; } = string.Empty;
        public static Dishonest_Dtl Default { get { return new Dishonest_Dtl(); } }
    }

    public class Trades
    {
        /// <summary>
        /// 国家标准行业分类
        /// </summary>
        public List<Trade> gb_trades { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public List<ProCat> pro_trades { get; set; }
    }

    public class Trade
    {
        public string code { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public List<Trade> trades { get; set; }
        public Trade(string code, string name)
        {
            this.code = code;
            this.name = name;
            this.level = code.Length;
            trades = new List<Trade>();
        }
    }

    public class ProCat
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<ProCat> pros { get; set; }

        public ProCat(string code, string name, bool isLeaf = false)
        {
            this.code = code;
            this.name = name;
            if (!isLeaf)
                pros = new List<ProCat>(10);
            else
                pros = new List<ProCat>(1);
        }

    }

}
