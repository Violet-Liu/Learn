using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QZ.Foundation.Model;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;
using System.ServiceModel.Activation;

namespace QZ.Service.Enterprise
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“User”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 User.svc 或 User.svc.cs，然后开始调试。
    [ErrorBehavior]
    [VisitorStaticticBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class User : IUser
    {
        public Response Login(Request request) => ServiceImpl.Process_Login(request);
        public Response Open_Login(Request request) => ServiceImpl.Process_Open_Login(request);
        public Response Register(Request request) => ServiceImpl.Process_Register(request);
        public Response Verify_Code_Get(Request request) => ServiceImpl.Process_Verify_Code_Get(request);
        public Response Pwd_Reset(Request request) => ServiceImpl.Process_Pwd_Reset(request);
        public Response Face_Reset(Request request) => ServiceImpl.Process_Face_Reset(request);
        public Response Info_Set(Request request) => ServiceImpl.Process_Info_Set(request);
        public Response Info_Get(Request request) => ServiceImpl.Process_Info_Get(request);

        public Response Query(Request request) => ServiceImpl.Process_Query(request);


        public Response Query_Delete(Request request) => ServiceImpl.Process_Query_Delete(request);


        public Response Query_Drop(Request request) => ServiceImpl.Process_Query_Drop(request);

        public Response Browse_Get(Request request) => ServiceImpl.Process_Browse_Get(request);

        public Response Browse_Delete(Request request) => ServiceImpl.Process_Browse_Delete(request);

        public Response Browse_Drop(Request request) => ServiceImpl.Process_Browse_Drop(request);

        public Response Favorites_Get(Request request) => ServiceImpl.Process_Favorites_Get(request);

        public Response Favorites_NewGet(Request request) => ServiceImpl.Process_Favorites_NewGet(request);

        public Response Notice_Status(Request request) => ServiceImpl.Process_Notice_Status(request);

        public Response Notice_Topics_Get(Request request) => ServiceImpl.Process_Notice_Topics_Get(request);

        //public Response Notice_Topic_Detail(Request request) => ServiceImpl.Process_Notice_Topic_Detail(request);

        public Response Notice_Companies(Request request) => ServiceImpl.Process_Notice_Companies(request);

        public Response Notice_Company_Remove(Request request) => ServiceImpl.Process_Notice_Company_Remove(request);

        public Response Notice_Topic_Remove(Request request) => ServiceImpl.Process_Notice_Topic_Remove(request);

        public Response Notice_Topic_Drop(Request request) => ServiceImpl.Process_Notice_Topic_Drop(request);
        public Response Ext_SearchHistory(Request request) => ServiceImpl.Process_Ext_SearchHistory(request);
        public Response Ext_SearchHistory_Drop(Request request) => ServiceImpl.Process_Ext_SearchHistory_Drop(request);

        public Response Suggestion_Add(Request request) => ServiceImpl.Process_Suggestion_Add(request);
        public Response FavoriteGroups_Get(string u_id) => ServiceImpl.Process_Favorite_Group_Get(u_id);
        public Response Favorites_GetBygid(Request request) => ServiceImpl.Process_Favorites_GetbyId(request);
        public Response Favorite_Group_Insert(Request request) => ServiceImpl.Process_Favorite_Group_Insert(request);
        public Response Favorite_Group_Del(Request request) => ServiceImpl.Process_Favorite_Group_Del(request);
        public Response Favorite_Group_Update(Request request) => ServiceImpl.Process_Favorite_Group_Update(request);
        public Response UnGroupedFavorites_Get(Request request) => ServiceImpl.Process_UnGroupedFavorites_Get(request);
        public Response Favorite_Into_Group(Request request) => ServiceImpl.Favorite_Into_Group(request);
        public Response Favorite_Out_Group(string fl_id) => ServiceImpl.Favorite_Out_Group(fl_id);
        public Response Favorite_Note_Add(Request request) => ServiceImpl.Favorite_Note_Add(request);
        public Response Favorite_Note_UP(Request request)=> ServiceImpl.Favorite_Note_UP(request);
        public Response Favorite_Note_Del(string n_id) => ServiceImpl.Favorite_Note_Del(n_id);
        public Response Favorite_Note_Get(Request request) => ServiceImpl.Favorite_Note_Get(request);
        public Response SysNotices_Get(Request request) => ServiceImpl.Process_SysNotices_Get(request);
        public Response SysNotice_Dtl_Get(Request request) => ServiceImpl.Process_SysNoticeDtl_Get(request);
        public Response SysNotice_Single_Del(Request request) => ServiceImpl.Process_SysNotice_SingleDel(request);
        public Response SysNotice_All_Del(string u_id) => ServiceImpl.Process_SysNotice_AllDel(u_id);
        #region my claim company
        public Response Claim_Company_Get(Request request) => ServiceImpl.Process_Claims_Get(request);
        #endregion

        #region vip gallery
        public Response Vip_Order_Submit(Request request) => ServiceImpl.Process_VipOrder_Submit(request);

        public Response Vip_Order_Notify(Request request) => ServiceImpl.Process_VipOrder_Notify(request);

        public string Vip_Order_AliPayNotify(string form) => ServiceImpl.Process_VipOrder_AliPayNotify(form);

        public Response Invoices_Get(Request request) => ServiceImpl.Process_Invoices_Get(request);

        public Response Invoice_Add(Request request) => ServiceImpl.Process_Invoice_Add(request);

        public Response Orders_Get(Request request) => ServiceImpl.Process_Orders_Get(request);

        #endregion

    }
}
