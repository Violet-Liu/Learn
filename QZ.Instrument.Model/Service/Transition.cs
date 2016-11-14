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
    public static class Transition
    {
        public static Resp_Binary To_Resp_Binary(this Comment_State state)
        {
            var resp = new Resp_Binary();
            if (state.File_State == File_Upload_State.Count_Err)
            {
                resp.remark = "图片数量不能大于9";
            }
            else
            {
                if (state.T_R_State == TopicReply_State.Content_Empty)
                {
                    resp.remark = "帖子内容不能为空";
                }
                else if(state.T_R_State == TopicReply_State.Db_Insert_Err)
                {
                    resp.remark = "发表失败";
                }
                else
                {
                    resp.status = true;
                    //if (state.File_State == File_Upload_State.None)
                    //{
                    //    resp.remark = "发表成功";
                    //}
                    if(state.File_State == File_Upload_State.Success)
                    {
                        resp.remark = $"发表成功\n成功上传{state.Count}张图片";
                    }
                    else
                    {
                        resp.remark = "发表成功";
                    }
                }
            }
            return resp;
        }

        public static Resp_Topics_Abs To_Resp_Topics_Abs(this List<Topic_Abs> list, int count)
        {
            var resp = new Resp_Topics_Abs() { topic_list = list, count = count };
            return resp;
        }
    }
    
}
