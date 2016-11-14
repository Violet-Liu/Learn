using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace QZ.Instrument.Model
{
    public class Constants
    {
        /// <summary>
        /// Type map between sql and C#
        /// </summary>
        public static readonly IDictionary<string, DbType> Sql_Csharp_TypeMap = new Dictionary<string, DbType>()
        {
            ["String"] = DbType.String,
            ["bool"] = DbType.Boolean,
            ["int"] = DbType.Int32,
            ["DateTime"] = DbType.DateTime,
            ["double"] = DbType.Double,
            ["decimal"] = DbType.Decimal
        };

        /// <summary>
        /// Page-select store procedure parameters and related type
        /// </summary>
        public static readonly IDictionary<string, string> Page_Select_Sp_Params = new Dictionary<string, string>()
        {
            ["@columns"] = "string",
            ["@where"] = "string",
            ["@order"] = "string",
            ["@page"] = "int",
            ["@pagesize"] = "int"
        };

        #region app setting key in config file
        // C-Client / S-Server
        public const string C_Dyn_0 = "ClientDynamicKey0";
        public const string C_Dyn = "ClientDynamicKey";
        public const string S_Dyn = "ServerDynamicKey";
        public const string S_Tok = "ServerTokenKey";
        public const string S_Msg = "ServerMessageVersion";

        //public const string A_Ver = "AndroidVersion";
        //public const string A_Flag = "AndroidFlag";
        //public const string A_Pack_Addr = "AndroidPackAddr";
        //public const string I_Ver = "IosVersion";
        //public const string I_Flag = "IosFlag";
        //public const string Token = "SessionToken";
        //public const string Open_Login_Flag = "OpenLoginFlag";
        #endregion

        #region Service setting
        public const string Elasticsearch_Name = "Elasticsearch";
        public const string ES_5_0_0_Name = "ES_5.0.0";
        public const string CompanyNameIndex_Name = "CompanyNameIndex";
        public const string CompanyMap_Name = "CompanyMap";
        public const string Upload_Name = "Upload";
        public const string Portrait_Upload_Name = "Portrait_Upload";
        public const string ShortMsg_Name = "ShortMsg";
        public const string CompanyTrade_Name = "CompanyTradeAnalysis";
        #endregion

        #region file path
        public static readonly string Service_Path = "Service.config";
        public static readonly string Uri_Metadata_Path = "Uri_Metadata.json";
        #endregion

        public static Dictionary<int, string> Brand_Classes = new Dictionary<int, string>
        {
            [1] = "化学原料",
            [2] = "颜料油漆",
            [3] = "日化用品",
            [4] = "燃料油脂",
            [5] = "医药",
            [6] = "金属材料",
            [7] = "机械设备",
            [8] = "手工器械",
            [9] = "科学仪器",
            [10] = "医疗器械",
            [11] = "灯具空调",
            [12] = "运输工具",
            [13] = "军火烟火",
            [14] = "珠宝钟表",
            [15] = "乐器",
            [16] = "办公品",
            [17] = "橡胶制品",
            [18] = "皮革皮具",
            [19] = "建筑材料",
            [20] = "家具",
            [21] = "厨房洁具",
            [22] = "绳网袋篷",
            [23] = "纱线丝",
            [24] = "布料床单",
            [25] = "服装鞋帽",
            [26] = "钮扣拉链",
            [27] = "地毯席垫",
            [28] = "键身器材",
            [29] = "食品",
            [30] = "方便食品",
            [31] = "饲料种籽",
            [32] = "啤酒饮料",
            [33] = "酒",
            [34] = "烟草烟具",
            [35] = "广告销售",
            [36] = "金融物管",
            [37] = "建筑修理",
            [38] = "通讯服务",
            [39] = "运输贮藏",
            [40] = "材料加工",
            [41] = "教育娱乐",
            [42] = "设计研究",
            [43] = "餐饮住宿",
            [44] = "医疗园艺",
            [45] = "社会法律"
        };

        public static string[] Brand_Cats = new string[]{"化学原料",
            "颜料油漆",
            "日化用品",
            "燃料油脂",
            "医药",
            "金属材料",
            "机械设备",
            "手工器械",
            "科学仪器",
            "医疗器械",
            "灯具空调",
            "运输工具",
            "军火烟火",
            "珠宝钟表",
            "乐器",
            "办公品",
            "橡胶制品",
            "皮革皮具",
            "建筑材料",
            "家具",
            "厨房洁具",
            "绳网袋篷",
            "纱线丝",
            "布料床单",
            "服装鞋帽",
            "钮扣拉链",
            "地毯席垫",
            "键身器材",
            "食品",
            "方便食品",
            "饲料种籽",
            "啤酒饮料",
            "酒",
            "烟草烟具",
            "广告销售",
            "金融物管",
            "建筑修理",
            "通讯服务",
            "运输贮藏",
            "材料加工",
            "教育娱乐",
            "设计研究",
            "餐饮住宿",
            "医疗园艺",
            "社会法律"};
    
    public static string[] Brand_Status = new string[]{"商标已注册",
            "商标异议申请中",
            "商标异议申请完成",
            "商标注册申请受理通知书发文",
            "商标注册申请注册公告排版完成",
            "商标续展完成",
            "商标转让完成",
            "商标变更完成 ",
            "商标无效"};


        public static IDictionary<string, string> Primary_Trades = new Dictionary<string, string>()
        {
            ["A"] = "农、林、牧、渔业",
            ["B"] = "采矿业",
            ["C"] = "制造业",
            ["D"] = "电力、热力、燃气及水产和供应业",
            ["E"] = "建筑业",
            ["F"] = "批发和零售业",
            ["G"] = "交通运输、仓储和邮政业",
            ["H"] = "住宿和餐饮业",
            ["I"] = "信息传输、软件和信息技术服务业",
            ["J"] = "金融业",
            ["K"] = "房地产业",
            ["L"] = "租赁和商务服务业",
            ["M"] = "科学研究和技术服务业",
            ["N"] = "水利、环境和公共设施管理业",
            ["O"] = "居民服务、修理和其他服务业",
            ["P"] = "教育",
            ["Q"] = "卫生和社会工作",
            ["R"] = "文化、体育和娱乐业",
            ["S"] = "公共管理、社会保障和社会组织",
            ["T"] = "国际组织",
            [""] = "其他"
        };


    }

    
}
