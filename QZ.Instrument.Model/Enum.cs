using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// sort mode of company list
    /// </summary>
    public enum oc_sort
    {
        none=0,
        oc_reg_capital=1,
        oc_issue_time=2
    }

    /// <summary>
    /// query type
    /// </summary>
    public enum q_type
    {
        q_general=0,    /* general query */

        q_advanced  /* 高级搜索 */
    }
    public enum Correct_Type
    {
        MainBody=1,
        Orgnization=2,
        StockHolder=3,
        Member=4,
        CompanyChange=5,
        Icpl=6,
        Other=9
    }
    public enum File_Upload_State
    {
        /// <summary>
        /// no file to be uploaded
        /// </summary>
        None = 0,
        Success = 1,
        Upload_Err = 2,
        /// <summary>
        /// count of files error
        /// </summary>
        Count_Err = 3

    }

    public class File_Upload_Info
    {
        public File_Upload_State State { get; set; }
        public List<string> Uris { get; set; }
    }

    public enum TopicReply_State
    {
        Sucess = 0,
        /// <summary>
        /// content is empty
        /// </summary>
        Content_Empty = 1,
        /// <summary>
        /// insert into database error
        /// </summary>
        Db_Insert_Err = 2
    }


    public enum Login_State
    {
        Success=0,
        Name_Err,   /* 用户名错误 */
        Pwd_Err,    /* 密码错误 */
        State_Err,  /* 用户帐号异常 */
        ADBlack_Err /* 广告黑名单 */
    }

    public enum Users_State
    {
        Register=0,
        Normal=1,
        Prohibit=2,
        Closed=3,
        ADBlack= 4  /* 广告黑名单 */
    }
    public enum User_Level
    {
        external=-1,
        normal=0,
        vip=1,
        vvip=2
    }
    public enum Verify_Code_Type
    {
        user_register=1,
        pwd_reset=2
    }

    public enum Login_Type
    {
        Local=0,
        QQ=1,
        Sina=2,
        WeChat=3
    }
    public enum Portrait_Type
    {
        large,
        middle,
        small
    }

    public enum Upload_Path_Type
    {
        /// <summary>
        /// 前瞻网头像地址
        /// </summary>
        Upload_NewSiteUserHeader_Path,
    }
    public enum User_Info_Type
    {
        name=0,
        email=1,
        position=2,
        business=3,
        company=4,
        pos_favor,
        bus_favor,
        per_sign    /* personal significance */
    }

    /// <summary>
    /// 论坛全站黑名单状态
    /// </summary>
    public enum SiteBlackStatus
    {
        评论 = 1,
        禁止论坛 = 2,
        禁止展会 = 4,
        禁止微报 = 8
    }

    public enum Brand_Status
    {
        商标已注册 = 1,
        商标异议申请中 = 10,
        商标异议申请完成 = 11,
        商标注册申请受理通知书发文 = 20,
        商标注册申请注册公告排版完成 = 21,
        商标续展完成 = 30,
        商标转让完成 = 40,
        商标变更完成 = 50,
        商标无效 = 100
    }

    public enum TradeQueryType
    {
        forward = 1,
        exhibit = 2
    }
}
