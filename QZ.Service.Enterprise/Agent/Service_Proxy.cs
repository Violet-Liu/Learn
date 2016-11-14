using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QZ.Instrument.Client;
using QZ.Instrument.Global;
using QZ.Instrument.Model;

namespace QZ.Service.Enterprise
{
    public class CompanyNameIndex_Proxy : NetTcpClient<ICompanyNameIndexChannel>
    {

        public static List<KeyValuePair<string, string>> Prefix_Get(List<string> perfix_list, string input, int limit)
        {
            try
            {
                var channel = CreateChannel(DataBus.CompanyNameIndex_Uris[0]);
                var result = channel.PerfixFetch(perfix_list, input, limit);
                (channel as IDisposable)?.Dispose();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(perfix_list.Aggregate((a, b) => a + b) + input, e);
            }
        }

        public static List<KeyValuePair<string, string>> Company_Mini_Info_Get(List<string> oc_name_List)
        {
            try
            {
                var channel = CreateChannel(DataBus.CompanyNameIndex_Uris[0]);
                var result = channel.Fetches(oc_name_List);
                (channel as IDisposable)?.Dispose();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(oc_name_List.Aggregate((a, b) => a + b), e);
            }
        }

        public static List<KeyValuePair<string, string>> Company_Mini_Info_Get(string oc_name, int pg_index, int pg_size)
        {
            try
            {
                var channel = CreateChannel(DataBus.CompanyNameIndex_Uris[0]);
                List<KeyValuePair<string, string>> result = channel.FetchList(oc_name, pg_index, pg_size);
                (channel as IDisposable)?.Dispose();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(oc_name, e);
            }
        }
    }
    public class CompanyMap_Proxy: NetTcpClient<ICompanyMapChannel>
    {
        public static List<CompanyRelation> Company_Invest_Get(string oc_name)
        {
            var channel = CreateChannel(DataBus.CompanyMap_Uris[0]);
            var relation = channel.GetAllInvestCompanys(oc_name, 10);
            channel.Close();
            if (relation != null && relation.nextRelations != null)
                return relation.nextRelations;
            return new List<CompanyRelation>();
        }

        /// <summary>
        /// Get company map
        /// </summary>
        /// <param name="oc_name">company name</param>
        /// <param name="dimession"></param>
        /// <param name="min_invisible_filter"></param>
        /// <param name="enable_area"></param>
        /// <returns></returns>
        public static JsonResult Company_Map_Get(string oc_name, int dimession, int min_invisible_filter, bool enable_area)
        {
            var channel = CreateChannel(DataBus.CompanyMap_Uris[0]);
            var jr = channel.GetMapJsonResult(oc_name, dimession, min_invisible_filter, enable_area);
            (channel as IDisposable)?.Dispose();
            return jr;
        }
    }

    public class Upload_Proxy
    {
        private static List<Upload> Channel_Get(string[] uris)
        {
            var list = new List<Upload>();
            for(int i = 0; i < uris.Length; i++)
            {
                var upload = new Upload();
                upload.Url = uris[i];
                list.Add(upload);
            }
            return list;
        }


        public static UploadedImageInfo Image_Upload(string u_id, string file_name, string file_ext, byte[] data, bool save_check)
        {
            var channels = Channel_Get(DataBus.Upload_Uris);
            UploadedImageInfo info = null;
            for(int i = channels.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                    info = channels[0].UploadFile(Guid.NewGuid().ToString(), Constants.Topic_Img_Key, file_name, file_ext, data, u_id, true);
                else
                    channels[i].UploadFile(Guid.NewGuid().ToString(), Constants.Topic_Img_Key, file_name, file_ext, data, u_id, false);
                (channels[i] as IDisposable)?.Dispose();
            }
            return info;
        }

        public static UploadedImageInfo[] Portrait_Upload(string u_id, string image_uri, byte[] data, Portrait_Type type, bool save_check)
        {
            var channels = Channel_Get(DataBus.Portrait_Uris);
            UploadedImageInfo[] lst = new UploadedImageInfo[] { };

            string image_guid = Guid.NewGuid().ToString();

            for (int i = channels.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    lst = channels[i].UploadUserHeaderImage1(
                        image_guid,
                        Upload_Path_Type.Upload_NewSiteUserHeader_Path.ToString(),
                        u_id,
                        type.ToString(),
                        image_uri,
                        data,
                        true
                    );
                }
                else
                {
                    channels[i].UploadUserHeaderImage1(
                        image_guid,
                        Upload_Path_Type.Upload_NewSiteUserHeader_Path.ToString(),
                        u_id,
                        type.ToString(),
                        image_uri,
                        data,
                        true
                    );
                }

                (channels[i] as IDisposable).Dispose();
            }

            return lst;
        }
    }

    public class ShortMsg_Proxy : NetTcpClient<IShortMsg>
    {
        public static SMSResult ShortMsg_Send(string taskName, string user, string mobile, string smsContent)
        {
            var channel = CreateChannel(DataBus.ShortMsg_Uris[0]);
            SMSResult result = channel.SendSMS(taskName, user, mobile, smsContent);
            (channel as IDisposable).Dispose();
            return result;
        }
    }

    public class CompanyTrade_Proxy : NetTcpClient<ICompanyTradeService>
    {
        public static AnalysesResult AnalysisAllTrade(string input, int count)
        {
            var channel = CreateChannel(DataBus.CompanyTrade_Uris[0]);
            var result = channel.AnalysisAllTrade(input, count, true);
            (channel as IDisposable).Dispose();
            return result;
        }
    }
}