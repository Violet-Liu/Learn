using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Delegation of creating object
    /// </summary>
    /// <returns></returns>
    public delegate object CreateDelegate();

    public class DynConstructor
    {
        private static Dictionary<string, Type> _typeDict = new Dictionary<string, Type>();
        private static Hashtable _deleTable = Hashtable.Synchronized(new Hashtable());
        private static Type _deleType = typeof(CreateDelegate);

        private static object _lock = new object();

        public static List<T> List_Generate<T>(DataRowCollection rows)
        {
            var list = new List<T>();
            foreach(DataRow dr in rows)
            {
                list.Add(Generate<T>(dr));
            }
            return list;
        }

        public static T Generate<T>(DataRow dr)
        {
            Type type = typeof(T);


            var t = Generate<T>(type);

            var pros = type.GetProperties();
            foreach (var p in pros)
            {
                var v = dr[p.Name];
                if (v != DBNull.Value)
                    p.SetValue(t, Convert.ChangeType(v, p.PropertyType), null);
            }
            return t;
        }

        /// <summary>
        /// Generate an instance of a given class
        /// In fact, it can be simply written to "var t = new T()"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Generate<T>() where T : class => Generate<T>(typeof(T));
        

        private static T Generate<T>(Type type)
        {
            var name = type.Name;
            var dele = _deleTable[type] as CreateDelegate;
            if (dele == null)
            {
                lock (_deleTable.SyncRoot)
                {
                    dele = _deleTable[type] as CreateDelegate;
                    if (dele == null)
                    {
                        var dynMd = new DynamicMethod("DM_" + name, typeof(object), null, type);
                        var ilGen = dynMd.GetILGenerator();

                        ilGen.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
                        ilGen.Emit(OpCodes.Ret);
                        dele = (CreateDelegate)dynMd.CreateDelegate(_deleType);
                        _deleTable.Add(type, dele);
                    }
                }
            }
            return (T)dele.Invoke();
        }
    }
}
