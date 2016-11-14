using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.DataAccess;
using QZ.Instrument.Model;

namespace QZ.Instrument.Global
{
    public class AreaData
    {
        // 以这些字结尾的词语，认为是一个地区名
        public const string LastChars = "县区市省";

        private static IDictionary<string, string> _oldAreas;
        /// <summary>
        /// 老地区字典，key为地区码，value为地区名
        /// </summary>
        public static IDictionary<string, string> OldAreas
        {
            get { return _oldAreas; }
            set
            {
                if (_oldAreas == null)
                    _oldAreas = OldAreas_Get();
            }
        }

        public static IDictionary<string, string> OldAreas_Get()
        {
            List<AreaList> list;
            using (var access = new QZOrgCompanyAppAccess("QZBase166"))
            {
                list = access.AreaList_SelectWhere("");
            }
            if (list.Count == 0) return new Dictionary<string, string>();

            return list.Where(l => l.a_name.Length < 6).ToDictionary(k => k.a_code, v => v.a_name);
        }
    }
}
