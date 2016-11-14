/*
 * Author: Sha Jianjian
 * Date: 2016-05-28
 * Description: SqlEntities loads all sql entities and save there types and public instance members. Notice that the first member is always ID of one type.
 * 
 * */
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace QZ.Instrument.Model
{
    public class DbEntity
    {
        private static DbEntity _instance = new DbEntity();
        public static DbEntity Instance { get { return _instance; } }

        private static Dictionary<string, Type> _typeDict = new Dictionary<string, Type>();

        private static object _lock = new object();




        #region obsolute
        private IDictionary<string, IList<PropertyInfo>> _entities = new Dictionary<string, IList<PropertyInfo>>();
        /// <summary>
        /// all sql entities.
        /// key is type of entity, and value is a list of the type's public members
        /// </summary>
        public IDictionary<string, IList<PropertyInfo>> Entities
        {
            get
            {
                if (_entities.Count < 1 || _entities.First().Value.Count < 1)
                    CaptureMetadata();
                return _entities;
            }
        }

        private void CaptureMetadata()
        {
            // load all sql entities
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            var sqlEntityType = typeof(DbEntityAttribute);
            var idType = typeof(IdAttribute);
            foreach(var t in types)
            {
                var attr = t.GetCustomAttribute(sqlEntityType);
                if(attr != null)
                {
                    var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    var list = new List<PropertyInfo>();

                    // mark it true if id field is found
                    bool flag = false;
                    foreach(var f in properties)
                    {
                        list.Add(f);
                        // if has not found id field
                        if (!flag)  
                        {
                            var mattr = f.GetCustomAttribute(idType);
                            if (mattr != null)  // current is id field
                            {
                                flag = true;
                                if (list.Count > 0)
                                {
                                    var temp = list[0];
                                    list[0] = f;
                                    list[list.Count - 1] = temp;
                                }
                            }
                        }   
                    }

                    _entities[t.Name] = list;
                }
            }
        }
        #endregion

    }
}
