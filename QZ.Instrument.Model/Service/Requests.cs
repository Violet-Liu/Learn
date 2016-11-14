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
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    public class Req_User
    {
        public int u_id { get; set; }
        public string u_name { get; set; }
        public string tel { get; set; }
    }

    public class Company
    {
        /// <summary>
        /// 公司搜索版本，为了向前兼容，本次版本设置为1，之前的为默认0
        /// </summary>
        public int v { get; set; }
        /// <summary>
        /// query string
        /// </summary>
        public string oc_name { get; set; }
        /// <summary>
        /// user query type
        /// </summary>
        public q_type q_type { get; set; }
        /// <summary>
        /// orgonization code
        /// </summary>
        public string oc_code { get; set; }
        //public string oc_name_0 { get; set; }
        public string oc_area { get; set; }
        /// <summary>
        /// register number
        /// </summary>
        public string oc_number { get; set; }
        /// <summary>
        /// artificial person
        /// </summary>
        public string oc_art_person { get; set; }
        public string oc_stock_holder { get; set; }
        public string oc_site { get; set; }
        public string oc_member { get; set; }
        public string oc_addr { get; set; }
        public string oc_business { get; set; }
        public string oc_reg_type { get; set; }
        public string oc_reg_capital_floor { get; set; }
        public string oc_reg_capital_ceiling { get; set; }
        public oc_sort oc_sort { get; set; }
        public int pg_index { get; set; } = 1;
        public int pg_size { get; set; } = 10;
        public bool flag { get; set; }
        public string oc_issue_time { get; set; }
        public string oc_ext { get; set; }
        /// <summary>
        /// true -> filter out all unnormal companies
        /// </summary>
        public string oc_status { get; set; } = "";

        public string oc_type { get; set; } = "";
        public string year { get; set; } = "";
        public string oc_regm { get; set; } = "";
        public string oc_trade { get; set; }
        public string oc_tel { get; set; }
        public string oc_mail { get; set; }
        
        /// <summary>
        /// whether jump to company detail page directly and quickly
        /// </summary>
        public bool quick_flag { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }

        /// <summary>
        /// Get if the current search is using filter. This method only make sense at the case of general search.
        /// True -> Do filter operation; False -> Do not filter
        /// </summary>
        /// <returns></returns>
        public static bool Filter_Flag_Get(Company c)
        {
            if (!string.IsNullOrEmpty(c.oc_area) && !c.oc_area.Equals("00"))    // the second judgement is only for downward compatibility
                return true;
            if (!string.IsNullOrEmpty(c.oc_reg_capital_floor))
                return true;
            if (!string.IsNullOrEmpty(c.oc_reg_capital_ceiling))
                return true;
            if (!string.IsNullOrEmpty(c.oc_status))
                return true;
            if (!string.IsNullOrEmpty(c.oc_type))
                return true;
            if (c.oc_trade != null && c.oc_trade == "00")
                return false;
            if (!string.IsNullOrEmpty(c.oc_trade))
                return true;
            //if (c.oc_trade == null || c.oc_trade == "00")     // ES中，""表示未知行业，如果不是筛选未知行业公司信息，此字段设置null
            //{
            //    if (c.oc_trade == "00") // 如果请求中不好设置为null，则使用"00"表示对行业不做限制
            //        c.oc_trade = null;
            //    return true;
            //}
            if (!string.IsNullOrEmpty(c.year))
                return true;
            if (!string.IsNullOrEmpty(c.oc_regm))
                return true;
            return c.oc_sort != oc_sort.none;
        }

        /// <summary>
        /// When using advanced search, validity must be guaranteed
        /// True -> Invalidity; False -> Validity
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool Invalid_Get(Company c)
        {
            //bool flag = true;
            if (!string.IsNullOrWhiteSpace(c.oc_name))
                return false;
            if (!string.IsNullOrEmpty(c.oc_area))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_code))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_number))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_art_person))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_stock_holder))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_addr))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_business))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_reg_type))
                return false;
            if (!string.IsNullOrWhiteSpace(c.oc_reg_capital_floor) || !string.IsNullOrWhiteSpace(c.oc_reg_capital_ceiling))
                return false;
            return true;
        }

        public Company Input(string input_s) => Fluent.Assign_1<Company, Company>(this, query => query.oc_name = input_s);
        public Company Pg_Index(int pg_index_i) => Fluent.Assign_1<Company, Company>(this, query => query.pg_index = pg_index_i);
        public Company Pg_Size(int pg_size_i) => Fluent.Assign_1<Company, Company>(this, query => query.pg_size = pg_size_i);
        public void Input_Cutoff()
        {
            if (this.oc_name.Length > 2)
                this.oc_name = this.oc_name.Substring(0, 2);
        }

        /// <summary>
        /// use for testing
        /// </summary>
        public static List<Company> Query_Tests
        {
            get
            {
                return new List<Company>()
                {
                    new Company() {oc_area = "4403", q_type = q_type.q_general, oc_name = "前瞻资讯股份有限公司", oc_sort = oc_sort.none},
                    new Company() {oc_name = "中视天脉（北京）科技有限公司", q_type =q_type.q_general, u_id = "30740", u_name = "来咯哦哦", pg_index = 1, pg_size = 10, v = 1 },
                    new Company() {  oc_name= "qq.com", q_type = q_type.q_general, u_id = "30533", u_name = "来咯哦哦"},
                    new Company() {  oc_art_person = "张启波", q_type = q_type.q_general, u_id = "30533", u_name = "来咯哦哦"},
                    new Company() {oc_name="华为", q_type = q_type.q_advanced, oc_area = "4403", u_id = "30533", u_name = "来咯哦哦"},
                    new Company() {oc_name = "腾讯", q_type = q_type.q_advanced, u_id = "30533", u_name = "来咯哦哦", oc_reg_capital_floor = "500", oc_reg_capital_ceiling = "10000" },
                    new Company() {oc_name = "华为", q_type = q_type.q_advanced, oc_reg_capital_floor = "2", oc_reg_capital_ceiling = "100", u_id = "30533", u_name = "来咯哦哦" },
                    new Company() {oc_business =
                                        "房地产投资；建筑材料、建筑机械及配套设备、通讯器材、汽车配件、电子产品、机电产品、轻纺产品、化工原料、五金家电、畜产品的销售"
                                        , u_id = "30533", u_name = "来咯哦哦"}
                };
            }
        }

        public static List<Company> Detail_Tests
        {
            get
            {
                return new List<Company>()
                {
                    new Company {oc_name = "招商银行股份有限公司", /*oc_code = "10001686X",*/ oc_area = "4403" },
                    new Company {oc_name =  "四川省峨眉山竹叶青茶业有限公司", oc_area = "5101", oc_code = "1000003QZ"}
                };
            }
        }
    }
    public class Req_TradeSearch
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }
        public string trd_code { get; set; }

        public string trd_name { get; set; }
        public static List<Req_TradeSearch> Defaults
        {
            get
            {
                return new List<Req_TradeSearch>()
                {
                    new Req_TradeSearch() { u_id = "test", u_name = "test", pg_index = 1, pg_size = 10, trd_code = "012" },
                    new Req_TradeSearch() {u_id="test", u_name="test", pg_index=1, pg_size=10, trd_code="302" }
                };
            }
        }
    }

    public class Req_Intelli_Tip
    {
        public string input { get; set; }
        public int pg_size { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public List<string> prefix_list { get; set; }
        public static List<Req_Intelli_Tip> Tests
        {
            get
            {
                return new List<Req_Intelli_Tip>()
                {
                    new Req_Intelli_Tip {input="垃圾", pg_size =10, u_id = "1927340" },
                    new Req_Intelli_Tip {input="<前瞻>", pg_size = 10 },
                    new Req_Intelli_Tip {input="（）前瞻）研究", pg_size =10 }
                };
            }
        }
    }
    public class Req_Oc_Map
    {
        /// <summary>
        /// D3Force，D3HEB，D3RadiaTree
        /// </summary>
        public string map_name { get; set; }
        public string oc_name { get; set; }
        public int map_dimession { get; set; }
        /// <summary>
        /// true, show area name in node name
        /// </summary>
        public bool flag { get; set; }

        public static List<Req_Oc_Map> Default
        {
            get
            {
                return new List<Req_Oc_Map>()
                {
                    new Req_Oc_Map() { map_dimession = 1, oc_name = "招商银行股份有限公司", flag = true, map_name = "D3Force" },
                    new Req_Oc_Map() {map_dimession =2, oc_name="北京奥都百升网络科技有限公司", flag = false, map_name = "D3HEB" }
                };
            }
        }
    }

    public class Req_Oc_Score
    {
        public string u_name { get; set; }
        public string u_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public int oc_score { get; set; }
    }

    public class Req_Oc_Mini
    {
        public string oc_area { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string u_email { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }

        public static List<Req_Oc_Mini> Defaults
        {
            get
            {
                return new List<Req_Oc_Mini>()
                {
                    new Req_Oc_Mini() { oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403", u_id = "30740", u_name = "来咯哦哦", pg_index = 1, pg_size = 10 },
                    new Req_Oc_Mini() { oc_area="3101", oc_code="MA1GKCKD9", oc_name = "上海瞻赢贸易有限公司", u_id = "30533", u_name = "来咯哦哦", pg_index = 1, pg_size = 10 },
                    new Req_Oc_Mini() { pg_index = 1, pg_size = 3 },     /* company_fresh_topic */
                    new Req_Oc_Mini() { pg_index = 1, pg_size = 10, u_id = "30533"},    /* company_topic_query */
                    new Req_Oc_Mini() { oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403", u_id = "30533", u_name = "来咯哦哦", u_email = "oe_luna@163.com" }   /* company report sending */
                };
            }
        }
        public static Req_Oc_Mini Oc_Brand_Get { get { return new Req_Oc_Mini() { oc_code = "708461136", pg_index = 1, pg_size = 10, u_id = "30740", u_name = "gaoshoufenmu", }; } }
        public static Req_Oc_Mini Oc_Patent_Get { get { return new Req_Oc_Mini() { oc_code = "", pg_index = 1, pg_size = 10, u_id = "30740", u_name = "gaoshoufenmu", }; } }
        
        public static Req_Oc_Mini Oc_Dishonest_Get { get { return new Req_Oc_Mini() { oc_code = "67877662X", pg_index = 1, pg_size = 10, u_id = "30740" }; } }
        public static Req_Oc_Mini Oc_Judge_Get { get { return new Req_Oc_Mini() { oc_code = "798180185", pg_index = 1, pg_size = 10, u_id = "30740" }; } }
        public static Req_Oc_Mini Report_Send { get { return new Req_Oc_Mini() { oc_code = "10001686X", oc_area = "4403", pg_index = 1, pg_size = 10, u_id = "30740" , u_email = "oe_luna@163.com", u_name = "gaoshoufenmu" }; } }
    }

    public class Req_Oc_Annual
    {
        public string oc_area { get; set; }
        public string oc_code { get; set; }
        public string year { get; set; }

        public static List<Req_Oc_Annual> Default
        {
            get
            {
                return new List<Req_Oc_Annual>()
                {
                    new Req_Oc_Annual() {oc_code = "10001686X", oc_area = "4403", year = "" },
                    new Req_Oc_Annual() {oc_code = "10001686X", oc_area = "4403", year = "2015" },
                    new Req_Oc_Annual() { oc_code = "10001686X", oc_area = "4403", year = "2014"}
                };
            }
        }
    }
    public class Req_Oc_Comment
    {
        //public string topic_title { get; set; }
        public string topic_tag { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string topic_content { get; set; }
        public List<string> pic_list { get; set; }

        #region reply special fields
        public int topic_id { get; set; }
        public string reply_content { get; set; }
        /// <summary>
        /// target user who the current user replies to
        /// </summary>
        public string target_u_id { get; set; }
        #endregion

        public static List<Req_Oc_Comment> Default
        {
            get
            {
                return new List<Req_Oc_Comment>()
                {
                    new Req_Oc_Comment() { topic_tag = "102", oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403", u_id = "30533", u_name = "来咯哦哦", topic_content ="this topic content is provided by test bot" },
                    new Req_Oc_Comment() { topic_id = 405, reply_content = "this content is provided by test bot", target_u_id = "30533", u_id = "30509", u_name = "Tim" }
                };
            }
        }
    }

    public class Req_Cm_Comment
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string topic_content { get; set; }
        public List<string> pic_list { get; set; }
        public Topic_Tag topic_tag { get; set; }

        #region reply
        public int topic_id { get; set; }
        public string reply_content { get; set; }
        /// <summary>
        /// target user who the current user replies to
        /// </summary>
        public string target_u_id { get; set; }
        #endregion
        public static List<Req_Cm_Comment> Defaults
        {
            get
            {
                return new List<Req_Cm_Comment>()
                {
                    new Req_Cm_Comment() {u_id = "30533", u_name = "来咯哦哦", topic_content = "this community topic content is provided by test bot", topic_tag = Topic_Tag.Cmt },
                    new Req_Cm_Comment() { u_id = "30533", u_name = "来咯哦哦",reply_content = "this community reply content is provided by test bot", topic_id = 433, target_u_id = "30542"},
                    new Req_Cm_Comment() {u_id="30740", u_name = "gaoshoufenmu", topic_content = "this community topic content is provided by test bot", topic_tag = Topic_Tag.Cmt,
                        pic_list = new List<string> {
                            "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"
                    } }
                };
            }
        }
    }
    public enum Topic_Tag
    {
        /// <summary>
        /// comment
        /// </summary>
        Cmt = 0,
        /// <summary>
        /// advertise
        /// </summary>
        Ads =1,       
        /// <summary>
        /// venting
        /// </summary>
        Vent=2
    }


    public class User_Login
    {
        public string u_x { get; set; }
        public string u_pwd { get; set; }
        public X_Type x_type { get; set; } = X_Type.Name;
        public User_Login X_Parse()
        {
            if (Judge.IsPhoneNum(u_x))
                x_type = X_Type.Phone;
            else if (Judge.IsEmail(u_x))
                x_type = X_Type.Email;
            return this;
        }

        public static List<User_Login> Defaults
        {
            get
            {
                return new List<User_Login>()
                {
                    new User_Login() {u_x="13670023001", u_pwd="123456" },   /* phone number */   
                    new User_Login() {u_x = "gaoshoufenmu", u_pwd="123456" }    /* user name, u_id = 30740 */
                };
            }
        }
    }

    public enum X_Type
    {
        Name,
        Phone,
        Email
    }
    public class User_Register
    {
        public string u_tel { get; set; }
        public string u_name { get; set; }
        public string u_pwd { get; set; }
        public string verify_code { get; set; }
        public Verify_Code_Type op_type { get; set; }
        public static List<User_Register> Defaults
        {
            get {
                return new List<User_Register>()
                {
                    new User_Register() { u_tel = "13670023001", u_name = "gaoshoufenmu", u_pwd = "123456", op_type = Verify_Code_Type.user_register, verify_code = "905892" },
                    new User_Register() { u_tel = "13670023001", u_name = "gaoshoufenmu", u_pwd = "123456", op_type = Verify_Code_Type.pwd_reset, verify_code = "107274"}
                };
            }
        }
    }
    public class Req_Open_Login
    {
        public int us_attentionsNum { get; set; }
        public int us_contentsNum { get; set; }
        public int us_fansNum { get; set; }
        public int us_favorsNum { get; set; }
        public string us_gender { get; set; }
        public string us_headImg { get; set; }
        public string us_location { get; set; }
        public string us_siteurl { get; set; }
        public string us_name { get; set; }
        public string us_nick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte us_type { get; set; }
        public string us_uid { get; set; }
        public bool us_verified { get; set; }
    }
    
    public class X_Topic_Tag
    {
        public int index { get; set; }      /* 字符串中的位置 */
        public int value { get; set; }     /* 值，为0，或1，或2 */
        public bool flag { get; set; }      /* true，正常使用此标签， false，此标签已不在使用 */
        public string name { get; set; } /* 标签名称 */
    }

    public class Topic_Tag_Set
    {   
        public int flag_criterion { get; set; } = 0xF;  /* 根据实际情况修改，或者向服务端获取以支持动态更换标签 */

        public string dest_str { get; set; } = "1021";
        public List<X_Topic_Tag> Tags { get; set; }

        public void Tags_Init()     /* 标签初始化 */
        {
            Tags = new List<X_Topic_Tag>()
            {
                new X_Topic_Tag() { index = 1, flag = true, name = ""}
            };
        }

        //public void Parse()
        //{
        //    var length = dest_str.Length;
        //    int i = 0;
        //    int cursor = 1;
        //    var tag = new X_Topic_Tag();
        //    while(i++ < length)
        //    {
        //        if((flag_criterion & cursor) != 0)
        //        {
        //            tag.flag = true; 
        //        }
        //        tag.value = Convert.ToInt32(dest_str[i]);
        //        tag.index = i;

        //        cursor >>= 1;
        //    }
        //}
    }

    public class Req_Portrait
    {
        public string u_id { get; set; }
        public string img { get; set; }
        public string img_s { get; set; } = "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";
        public static Req_Portrait Default { get { return new Req_Portrait() {
            u_id = "30740",
            img = "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"}; } }
    }

    public class Req_User_Info
    {
        public string u_id { get; set; }
        public string value { get; set; }
        public User_Info_Type type { get; set; }
        public static List<Req_User_Info> Defaults
        {
            get
            {
                return new List<Req_User_Info>()
                {
                    new Req_User_Info() { u_id = "30161", type = User_Info_Type.company, value = "qianzhan-test2" },    /* info set */
                    new Req_User_Info() { }     /* info get */
                };
            }
        }
    }
    
    public class Req_Topic_Dtl
    {
        public string oc_code { get; set; }
        public string u_id { get; set; }
        public int topic_id { get; set; }
        /// <summary>
        /// 0 -> company; 1 -> community
        /// </summary>
        public int topic_type { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }

        public static List<Req_Topic_Dtl> Defaults
        {
            get
            {
                return new List<Req_Topic_Dtl>()
                {
                    new Req_Topic_Dtl() {topic_id = 33, u_id = "30533", pg_index = 1, pg_size =10  },   /* company topic detail*/
                    new Req_Topic_Dtl() {topic_id = 433, u_id = "30533", pg_index = 1, pg_size = 10, topic_type = 1 }   /* community topic detail */
                };
            }
        }
    }

    public class Req_Cm_Topic
    {
        public string u_id { get; set; }
        /// <summary>
        /// 0 -> all topics; 1 -> topics which were post by myself; 2 -> topics on which i had replied
        /// </summary>
        public int op_type { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }

        public static List<Req_Cm_Topic> Defaults
        {
            get
            {
                return new List<Req_Cm_Topic>()
                {
                    new Req_Cm_Topic() { u_id = "30533", pg_index = 1, pg_size = 10 },
                    new Req_Cm_Topic() {u_id = "30533", pg_index = 1, pg_size = 10, op_type = 1 },
                    new Req_Cm_Topic() {u_id = "30533", pg_index = 1, pg_size = 10, op_type = 2 }
                };
            }
        }
        public static Req_Cm_Topic Favorites { get { return new Req_Cm_Topic() { u_id = "30740", pg_index = 1, pg_size = 10 }; } }
        public static Req_Cm_Topic Notices_Get { get { return new Req_Cm_Topic() { u_id = "30161", pg_index = 1, pg_size = 10 }; } }
    }
    public class Req_Topic_Vote
    {
        public string u_id { get; set; }
        public int topic_id { get; set; }
        public int topic_type { get; set; }
        public string u_name { get; set; }
        public int op_type { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public static List<Req_Topic_Vote> Defaults
        {
            get
            {
                return new List<Req_Topic_Vote>()
                {
                    new Req_Topic_Vote() {u_id="30533",u_name="来咯哦哦",topic_id=80,topic_type=0,op_type=1 },      /* company topic up */
                    new Req_Topic_Vote() {u_id="30533",u_name="来咯哦哦",topic_id=80,topic_type=0,op_type=2 },      /* company topic down */

                    new Req_Topic_Vote() {u_id="30533",u_name="来咯哦哦", oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403",op_type=1},   /* company up */
                    new Req_Topic_Vote() {u_id="30533",u_name="来咯哦哦", oc_name = "招商银行股份有限公司", oc_code = "10001686X", oc_area = "4403",op_type=2},   /* company down */

                    new Req_Topic_Vote() { u_id="30533",u_name="来咯哦哦",topic_id=433,topic_type=1,op_type=1 },    /* community topic up */
                    new Req_Topic_Vote() { u_id="30533",u_name="来咯哦哦",topic_id=433,topic_type=1,op_type=2 }     /* community topic down */
                };
            }
        }
    }
    public class Req_Oc_Correct
    {
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string u_tel { get; set; }
        public string crect_content { get; set; }
        public Correct_Type crect_type { get; set; }

        public static Req_Oc_Correct Default
        {
            get { return new Req_Oc_Correct() { u_id = "30533", u_name = "来咯哦哦", oc_name = "招商银行股份有限公司", oc_code = "10001686X", u_tel = "0123456789", crect_content = "test by bot" }; }
        }
        
    }
    
    public class Req_Query
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
        public int query_id { get; set; }
        public string oc_name { get; set; }
        public static Req_Query Default
        {
            get { return new Req_Query() { u_id = "30533", u_name = "xxx", query_id = 1 }; }
        }
    }
    public class Req_Browse
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
        public int browse_id { get; set; }
        public string oc_name { get; set; }
        public static Req_Browse Default
        {
            get { return new Req_Browse() { u_id = "30533", u_name = "xxx", browse_id = 233, oc_name = "xxx" }; }
        }
    }

    /// <summary>
    /// query structure for brand, patent and so on.
    /// </summary>
    public class Req_Info_Query
    {
        public string query_str { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        public string oc_code { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }
        public int status { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string cat_s { get; set; }
        public string area { get; set; }
        /// <summary>
        /// patent_type
        /// </summary>
        public string p_type { get; set; }
        //public int status { get; set; }
        public int year { get; set; }
        /// <summary>
        /// 1商标2专利3产品著作权4软件著作权5判决文书6失信
        /// 29按前瞻行业搜索30按展会标签搜索
        /// 31展会搜索
        /// </summary>
        public byte q_type { get; set; }
        /// <summary>
        /// 1 -> asc application date; 2 -> desc application date
        /// </summary>
        public int q_sort { get; set; }
        public Req_Info_Query Type_Set(byte type)
        {
            q_type = type;
            return this;
        }
        public Req_Info_Query Pg_Index(int pg_index)
        {
            this.pg_index = pg_index;
            return this;
        }

        public static List<Req_Info_Query> Defaults
        {
            get
            {
                return new List<Req_Info_Query>() {
                    new Req_Info_Query() { query_str = "华为", u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 20, cat_s = "21" },
                    new Req_Info_Query() {query_str = "血栓", u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 15, year = 2016 },
                    new Req_Info_Query() {query_str = "华为",  u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 15/*, p_type = "发明"*/ },
                    new Req_Info_Query() { query_str = "交通"/*"财产保险公司"*/, u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 15 },
                    new Req_Info_Query() {query_str = "四川台湾名品博览会",  u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 10, q_sort = 1 }
                };
            }
        }

        public static Req_Info_Query Judge_Query { get { return new Req_Info_Query() { query_str = "小米", u_id = "30740", u_name = "gaoshoufenmu", pg_index = 1, pg_size = 15 }; } }
        public static Req_Info_Query ExtQuery_History { get { return new Req_Info_Query() { pg_index = 1, pg_size = 10, u_id = "1927340", q_type = 30 }; } }
    }

    public class Req_Copyright
    {
        public int id { get; set; }
        public string u_id { get; set; }
        public string u_name { get; set; }
        
    }

    public class Req_Query_Dtl
    {
        public string s_id { get; set; }
        public int i_id { get; set; }


        public string u_id { get; set; }


        public static Req_Query_Dtl Brand_Dtl { get { return new Req_Query_Dtl() { i_id = 965076, u_id = "30740" }; } }
        public static Req_Query_Dtl Dishonest_Dtl { get { return new Req_Query_Dtl() { i_id = 4013, u_id = "30740" }; } }
        public static Req_Query_Dtl Judge_Dtl { get { return new Req_Query_Dtl() { s_id = "e8223b7feab549f88380fabaa760f927", u_id = "30740" }; } }
    }

    public class Req_Brand_Dtl
    {
        /// <summary>
        /// 对商标而言，表示注册号，regno
        /// </summary>
        public string reg_no { get; set; }
        /// <summary>
        /// 对商标而言，表示分类号，classno
        /// </summary>
        public string cat_no { get; set; }

        public string u_id { get; set; }


        public static Req_Brand_Dtl Default { get { return new Req_Brand_Dtl() { reg_no = "290040", cat_no = "7"}; } }
    }

    public class Req_Patent_Dtl
    {
        public string p_no { get; set; }
        public string m_cat { get; set; }
        public string u_id { get; set; }
        public static Req_Patent_Dtl Default { get { return new Req_Patent_Dtl() { p_no = "CN201630063972.5", m_cat = "CN303636219S" }; } }
    }

    /// <summary>
    /// Comment TipOff request
    /// </summary>
    public class Req_Cmt_TipOff
    {
        /// <summary>
        /// 举报人id
        /// </summary>
        public string accuse_uid { get; set; }
        /// <summary>
        /// 举报人用户名
        /// </summary>
        public string accuse_uname { get; set; }
        /// <summary>
        /// 评论类型（1：公司帖，2：公司帖回复，3：app帖，4：app帖回复）
        /// </summary>
        public byte cmt_type { get; set; }
        /// <summary>
        /// 帖子或回复id
        /// </summary>
        public int cmt_id { get; set; }
        /// <summary>
        /// 举报描述
        /// </summary>
        public string to_des { get; set; }
        /// <summary>
        /// 被举报人id
        /// </summary>
        public string accused_uid { get; set; }
        /// <summary>
        /// 被举报人名
        /// </summary>
        public string accused_uname { get; set; }
    }

    /// <summary>
    /// Comment Shield model
    /// </summary>
    public class Req_Cmt_Shield
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string u_id { get; set; }
        /// <summary>
        /// user name
        /// </summary>
        public string u_name { get; set; }
        /// <summary>
        /// 评论类型（1：公司帖，2：公司帖回复，3：app帖，4：app帖回复）
        /// </summary>
        public byte cmt_type { get; set; }
        /// <summary>
        /// 帖子或回复id
        /// </summary>
        public int cmt_id { get; set; }
    }

}
