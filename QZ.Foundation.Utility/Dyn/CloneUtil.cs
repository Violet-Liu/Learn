using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility.Dyn
{
    public delegate TResult Func<TParam, TResult>(TParam @param);

    public class CloneUtil<T> where T : new()
    {
        private static Dictionary<Type, Delegate> _shallowDict = new Dictionary<Type, Delegate>();
        private static Dictionary<Type, Delegate> _deepDict = new Dictionary<Type, Delegate>();



        private static LocalBuilder _lbfTemp;

        /// <summary>
        /// Clone via reflection
        /// Execute slowly
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T CloneWithReflection(T t)
        {
            var fis = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var nt = new T();
            foreach(var fi in fis)
            {
                fi.SetValue(nt, fi.GetValue(t));
            }
            return nt;
        }


        /// <summary>
        /// Clone a T instance shallowly
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T CloneWithILShallow(T t)
        {
            var type = typeof(T);
            Delegate @delegate = null;
            if(!_shallowDict.TryGetValue(type, out @delegate))
            {
                lock (_shallowDict)
                {
                    if (!_shallowDict.TryGetValue(type, out @delegate))
                    {
                        var dm = new DynamicMethod("ShallowClone" + type.Name, typeof(T), new Type[] { type }, Assembly.GetExecutingAssembly().ManifestModule, true);
                        var ctorInfo = type.GetConstructor(new Type[] { });
                        var il = dm.GetILGenerator();
                        var lb = il.DeclareLocal(type);
                        il.Emit(OpCodes.Newobj, ctorInfo);
                        il.Emit(OpCodes.Stloc_0);

                        // type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        foreach (var fi in t.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                        {
                            il.Emit(OpCodes.Ldloc_0);
                            il.Emit(OpCodes.Ldarg_0);
                            il.Emit(OpCodes.Ldfld, fi);
                            il.Emit(OpCodes.Stfld, fi);
                        }
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Ret);
                        @delegate = dm.CreateDelegate(typeof(Func<T, T>));
                        _shallowDict[type] = @delegate;
                    }
                }
            }
            return ((Func<T, T>)@delegate)(t);
        }

        /// <summary>
        /// Clone a T instance deeply
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T CloneWithILDeep(T t)
        {
            Delegate @delegate = null;
            Type type = typeof(T);
            if(!_deepDict.TryGetValue(type, out @delegate))
            {
                lock(_deepDict)
                {
                    if(!_deepDict.TryGetValue(type, out @delegate))
                    {
                        var dm = new DynamicMethod("DeepClone" + type.Name, typeof(T), new Type[] { type }, Assembly.GetExecutingAssembly().ManifestModule, true);
                        var ctorInfo = type.GetConstructor(new Type[] { });
                        var il = dm.GetILGenerator();
                        var lb = il.DeclareLocal(type);
                        il.Emit(OpCodes.Newobj, ctorInfo);  // new an instance of Type T
                        il.Emit(OpCodes.Stloc_0);           // pop out the new instance and save it at INDEX 0 of the local variable list

                        foreach(var fi in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                        {
                            if(fi.FieldType.IsValueType || fi.FieldType == typeof(string))
                            {
                                CopyValueType(il, fi);
                            }
                            else if(fi.FieldType.IsClass)
                            {
                                CopyReferenceType(il, fi);
                            }
                        }
                        il.Emit(OpCodes.Ldloc_0);   // load the new instance of Type T
                        il.Emit(OpCodes.Ret);       // return

                        @delegate = dm.CreateDelegate(typeof(Func<T, T>));
                        _deepDict[type] = @delegate;
                    }
                }
            }
            return ((Func<T, T>)@delegate)(t);
        }


        private static void CopyValueType(ILGenerator il, FieldInfo fi)
        {
            il.Emit(OpCodes.Ldloc_0);   // the new instance
            il.Emit(OpCodes.Ldarg_0);   // t
            il.Emit(OpCodes.Ldfld, fi); // get fi value of t
            il.Emit(OpCodes.Stfld, fi); // set fi value of t to the new instance
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <param name="fi">fi type is a reference type</param>
        private static void CopyReferenceType(ILGenerator il, FieldInfo fi)
        {
            _lbfTemp = il.DeclareLocal(fi.FieldType);   // declare a variable of Type 'fi.FieldType'
            if (fi.FieldType.GetInterface("IEnumerable") != null)   // 'fi.FieldType' inherit the interface 'IEnumerable'
            {
                if (fi.FieldType.IsGenericType)         // if 'fi.FieldType' is a generic type
                {
                    // type of generic argument
                    var argType = fi.FieldType.GetGenericArguments()[0];
                    // get the concrete type
                    var genericType = Type.GetType("System.Collections.Generic.IEnumerable`1[" + argType.FullName + "]");

                    var ci = fi.FieldType.GetConstructor(new Type[] { genericType });   // get the .Ctor of Type 'List<argType>', denote that the items of the list are shallowly copied
                    if (ci != null)
                    {
                        il.Emit(OpCodes.Ldarg_0);           // load t
                        il.Emit(OpCodes.Ldfld, fi);         // get the fi value of t

                        // new a List<argType> instance, and push it to the stack. 
                        //Because the .Ctor's param is type 'List<argType>', and the param value is the fi value above which was push to the stack
                        il.Emit(OpCodes.Newobj, ci);

                        il.Emit(OpCodes.Stloc, _lbfTemp);   // assign the new instance above to _lbfTemp, and pop it out from the stack

                        il.Emit(OpCodes.Ldloc_0);           // load the new instance of T
                        il.Emit(OpCodes.Ldloc, _lbfTemp);   // load _lbfTemp
                        il.Emit(OpCodes.Stfld, fi);         // set _lbfTemp to the fi value of T
                    }
                }
            }
            else
            {
                var ctorInfo = fi.FieldType.GetConstructor(new Type[] { });     // get the .Ctor of Type 'fi.FieldType'
                il.Emit(OpCodes.Newobj, ctorInfo);  // new a fi value
                il.Emit(OpCodes.Stloc, _lbfTemp);   // assign the new fi value to _lbfTemp, and pop it out from the stack

                il.Emit(OpCodes.Ldloc_0);           // load the new instance of Type T
                il.Emit(OpCodes.Ldloc, _lbfTemp);   // load _lbfTemp
                il.Emit(OpCodes.Stfld, fi);         // set _lbfTemp to fi value of the new instance

                foreach (var f in fi.FieldType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (f.FieldType.IsValueType || f.FieldType == typeof(string))
                    {
                        il.Emit(OpCodes.Ldloc_1);
                        il.Emit(OpCodes.Ldarg_0);       // load param t
                        il.Emit(OpCodes.Ldfld, fi);     // get fi value of t
                        il.Emit(OpCodes.Ldfld, f);      // get f value of fi value of t
                        il.Emit(OpCodes.Stfld, f);      // set f value of fi value of t to f value of _lbfTmep
                    }
                    else if (f.FieldType.IsClass)
                        CopyReferenceType(il, f);
                }
            }
        }

        
    }
}
