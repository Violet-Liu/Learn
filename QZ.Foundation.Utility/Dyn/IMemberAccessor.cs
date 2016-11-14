using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility.Dyn
{
    public interface IMemberAccessor
    {
        object GetValue(object instance, string memberName);
        void SetValue(object instance, string memberName, object newVlaue);
    }

    /// <summary>
    /// Use refelction
    /// For value type, boxing operation will decrease performance
    /// Also, modify this type to 'ReflectionMemberAccessor&lt;T, TM&gt; where TM denotes Member's type
    /// </summary>
    public class ReflectionMemberAccessor<T>
    {
        public static object GetValue(T instance, string memberName)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(memberName);
            if (propertyInfo != null)
                return propertyInfo.GetValue(instance, null);
            else
            {
                var fieldInfo = type.GetField(memberName);
                if (fieldInfo != null)
                    return fieldInfo.GetValue(instance);
            }
            return null;
        }

        public static void SetValue(T instance, string memberName, object newValue)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(memberName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(instance, newValue, null);
                return;
            }
            var fi = type.GetField(memberName);
            if (fi != null)
                fi.SetValue(instance, newValue);
        }
    }
}
