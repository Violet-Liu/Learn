using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility.Dyn
{
    /// <summary>
    /// Provides a comparison to sort collections based on a dynamic property or field
    /// </summary>
    public class MemberComparer<T> : IComparer<T>
    {
        private delegate int CompareDeledate(T t1, T t2);
        private CompareDeledate _compare;

        public MemberComparer(string memberName)
        {
            _compare = GetCompareDelegate(memberName);
        }

        public int Compare(T t1, T t2) => _compare(t1, t2);

        private CompareDeledate GetCompareDelegate(string memberName)
        {
            var type = typeof(T);
            var pi = type.GetProperty(memberName);
            var fi = type.GetField(memberName);

            Type memberType = null;     // 成员类型
            bool isProperty = false;    // 是否是属性

            if (pi != null)
            {
                if (pi.GetGetMethod() != null)
                {
                    isProperty = true;
                    memberType = pi.PropertyType;
                }
                else
                    throw new Exception($"Property: '{memberName}' of Type '{type.Name}' does not have a public Get accessor");

            }
            else if (fi != null)
                memberType = fi.FieldType;
            else
                throw new Exception($"Member '{memberName}' is not a Property or field of Type '{type.Name}'");

            // use type 'memberType' to substitude for the generic type of Comparer<>
            // return typeof(Comparer<memberType>)
            var comparerType = typeof(Comparer<>).MakeGenericType(new Type[] { memberType });
            // get the Default sort comparer's Get method
            var getDefaultMethod = comparerType.GetProperty("Default").GetGetMethod();
            // get the Type 'Comparer<memberType>' 's 'Compare' method
            var compareMethod = getDefaultMethod.ReturnType.GetMethod("Compare");

            var dm = new DynamicMethod("Compare_" + memberName, typeof(int), new Type[] { type, type }, comparerType);

            var il = dm.GetILGenerator();
            // load Comparer<memberType>.Default's Get method onto the stack
            il.EmitCall(OpCodes.Call, getDefaultMethod, null);

            // load the member from arg 0 onto the stack
            il.Emit(OpCodes.Ldarg_0);
            if (isProperty)
                il.EmitCall(OpCodes.Callvirt, pi.GetGetMethod(), null);
            else
                il.Emit(OpCodes.Ldfld);

            // load the member from arg 1 onto the stack
            il.Emit(OpCodes.Ldarg_1);
            if (isProperty)
                il.EmitCall(OpCodes.Callvirt, pi.GetGetMethod(), null);
            else
                il.Emit(OpCodes.Ldfld);

            // call the Compare method
            il.EmitCall(OpCodes.Callvirt, compareMethod, null);
            il.Emit(OpCodes.Ret);

            return (CompareDeledate)dm.CreateDelegate(typeof(CompareDeledate));
        }
    }
}
