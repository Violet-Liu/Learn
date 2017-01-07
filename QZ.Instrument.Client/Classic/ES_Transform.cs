
using QZ.Instrument.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Client
{
    public static class ES_Transform
    {
        public static Judge_Abs To_Judge_Abs(this ES_Judge doc)
        {
            var j = new Judge_Abs();
            j.court = doc.jd_court;
            j.oc_code = doc.jd_oc_code;
            j.date = doc.jd_date.ToString("yyyy-MM-dd");
            j.reference = doc.jd_num;
            j.title = doc.jd_title;
            j.title_raw = j.title.Replace("<font color=\"#FF4400\">", "").Replace("</font>", "");//<font color="#FF4400">石河子<font color="#FF4400">开发区<font color="#FF4400">天<font color="#FF4400">富<font color="#FF4400">房地产开发有限责任公司与解文庆房屋买卖合同纠纷一审民事判决书
            j.judge_id = doc.jd_id;
            return j;
        }
    }
}
