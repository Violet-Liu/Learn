using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace QZ.Foundation.Utility.Dyn
{
    /// <summary>
    /// Fast dynamic Property/Field accessor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeUtil<T>
    {
        public delegate TM GetValueDelegate<TM>(T t);

        public delegate void SetValueDelegate<TM>(T t, TM tm);

        private static Dictionary<string, Delegate> _getDelegateDict = new Dictionary<string, Delegate>();
        private static Dictionary<string, Delegate> _setDelegateDict = new Dictionary<string, Delegate>();

        private static Type[] ParamTypes = new Type[] { typeof(object).MakeByRefType(), typeof(object) };

        /// <summary>
        /// Get the member set delegate of a given member name with delegate cached
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private static SetValueDelegate<TM> OptimizeSetMemberDelegate<TM>(string memberName)
        {
            if (_setDelegateDict.ContainsKey(memberName))
                return (SetValueDelegate<TM>)_setDelegateDict[memberName];
            lock(_setDelegateDict)
            {
                if (_setDelegateDict.ContainsKey(memberName))
                    return (SetValueDelegate<TM>)_setDelegateDict[memberName];

                var @delegate = DirectSetMemberDelegate<TM>(memberName);
                _setDelegateDict[memberName] = @delegate;
                return @delegate;
            }
        }

        /// <summary>
        /// Get the member get delegate of a given member name with delegate cached
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static GetValueDelegate<TM> OptimizeGetMemberDelegate<TM>(string memberName)
        {
            if (_getDelegateDict.ContainsKey(memberName))
                return (GetValueDelegate<TM>)_getDelegateDict[memberName];

            lock(_getDelegateDict)
            {
                if (_getDelegateDict.ContainsKey(memberName))
                    return (GetValueDelegate<TM>)_getDelegateDict[memberName];

                var @delegate = DirectGetMemberDelegate<TM>(memberName);
                _getDelegateDict[memberName] = @delegate;
                return @delegate;
            }
        }

        /// <summary>
        /// Get the member set delegate of a given member name without delegate cached
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static SetValueDelegate<TM> DirectSetMemberDelegate<TM>(string memberName)
        {
            var type = typeof(T);
            var pi = type.GetProperty(memberName);
            if(pi != null)
            {
                var mi = pi.GetSetMethod();
                if (mi != null)
                    return (SetValueDelegate<TM>)Delegate.CreateDelegate(typeof(SetValueDelegate<TM>), mi);
                throw new Exception($"Property: {memberName} of Type: '{type.Name}' does not have a public Get accessor");
            }
            throw new Exception();
            //else
            //{
            //    var fi = type.GetField(memberName);
            //    if (fi != null)
            //    {
            //        var dm = new DynamicMethod("Set" + memberName, typeof(void), new Type[] { fi.FieldType }, fi.ReflectedType.Module, true);
            //        var il = dm.GetILGenerator();
            //        il.Emit(OpCodes.Ldarg_0);
            //        il.Emit(OpCodes.Ldind)
            //    }
            //}
        }

        /// <summary>
        /// Get the member get delegate of a given member name without delegate cached
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static GetValueDelegate<TM> DirectGetMemberDelegate<TM>(string memberName)
        {
            var type = typeof(T);
            var pi = type.GetProperty(memberName);
            if(pi != null)
            {
                var mi = pi.GetGetMethod();
                if(mi != null)
                {
                    // maybe, Delegate.CreateDelegate is faster/cleaner than Reflection.Emit
                    // for calling a property's get accessor.
                    return (GetValueDelegate<TM>)Delegate.CreateDelegate(typeof(GetValueDelegate<TM>), mi);
                }
                throw new Exception($"Property: {memberName} of Type: '{type.Name}' does not have a public Get accessor");
            }
            else
            {
                var fi = type.GetField(memberName);
                if(fi != null)
                {
                    var dm = new DynamicMethod("Get" + memberName, typeof(TM), new Type[] { type }, type);
                    var il = dm.GetILGenerator();
                    // load the instance of the object (argument 0) onto the stack
                    il.Emit(OpCodes.Ldarg_0);
                    // load the value of object's field (fi) onto the stack
                    il.Emit(OpCodes.Ldfld, fi);
                    // return the value on the top of the stack
                    il.Emit(OpCodes.Ret);

                    return (GetValueDelegate<TM>)dm.CreateDelegate(typeof(GetValueDelegate<TM>));
                }
                else
                    throw new Exception($"Member: {memberName} is not a public Property or Field of Type: '{type.Name}'");
            }
        }

        /// <summary>
        /// Get the member value of a given object <typeparamref name="t"/> with a given member name <typeparamref name="memberName"/>
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public TM GetValue<TM>(T t, string memberName) => OptimizeGetMemberDelegate<TM>(memberName)(t);

        /// <summary>
        /// Set the member value of a given object <typeparamref name="t"/> with a given member name <typeparamref name="memberName"/>
        /// to <typeparamref name="tm"/>
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <param name="tm"></param>
        public void SetValue<TM>(T t, string memberName, TM tm) => OptimizeSetMemberDelegate<TM>(memberName)(t, tm);

        /// <summary>
        /// Instantiate the type T
        /// </summary>
        /// <returns></returns>
        public static T NewInstance()
        {
            var type = typeof(T);
            var ctorInfo = type.GetConstructor(Type.EmptyTypes);

            var dm = new DynamicMethod("Instantiate" + type.Name, type, new Type[0], ctorInfo.DeclaringType);
            var il = dm.GetILGenerator();
            il.DeclareLocal(ctorInfo.DeclaringType);
            il.Emit(OpCodes.Newobj, ctorInfo);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            var method = (Func<T>)dm.CreateDelegate(typeof(Func<T>));
            return method();
        }

        /// <summary>
        /// Get the value of a given instance <typeparamref name="t"/> with a given member name <typeparamref name="memberName"/>.
        /// The execution efficiency is little worse than 'Delegate.CreateDelegate'
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static TM GetValueByExpr<TM>(T t, string memberName)
        {
            var type = typeof(T);
            var key = "Expr" + type.Name + memberName;
            Delegate @delegate;

            if(!_getDelegateDict.TryGetValue(key, out @delegate))
            {
                lock(_getDelegateDict)
                {
                    if (!_getDelegateDict.TryGetValue(key, out @delegate))
                    {
                        var pi = type.GetProperty(memberName);
                        var target = Expression.Parameter(type, "target");

                        var getter = Expression.Lambda(typeof(GetValueDelegate<TM>),
                            Expression.Property(target, pi),
                            target);

                        @delegate = (GetValueDelegate<TM>)getter.Compile();
                        _getDelegateDict[key] = @delegate;
                    }
                }
            }
            return ((GetValueDelegate<TM>)@delegate)(t);
        }

        /// <summary>
        /// Set the member value of a given instance <typeparamref name="t"/> with a given member name <typeparamref name="memberName"/>
        /// and a value of Type <seealso cref="TM"/>.
        /// The execution efficiency is little worse than 'Delegate.CreateDelegate'
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public void SetValueByExpr<TM>(T t, string memberName, TM tm)
        {
            var type = typeof(T);
            var key = "Expr" + type.Name + memberName;

            Delegate @delegate;

            if(!_setDelegateDict.TryGetValue(key, out @delegate))
            {
                lock(_setDelegateDict)
                {
                    if(!_setDelegateDict.TryGetValue(key, out @delegate))
                    {
                        var pi = type.GetProperty(memberName);
                        var target = Expression.Parameter(type, "target");
                        var value = Expression.Parameter(typeof(TM), "value");

                        var setter = Expression.Lambda(typeof(SetValueDelegate<TM>),
                            Expression.Assign(Expression.Property(Expression.Convert(target, type), pi), value),
                            target, value);

                        @delegate = (SetValueDelegate<TM>)setter.Compile();
                        _setDelegateDict[key] = @delegate;
                    }
                }
            }
            ((SetValueDelegate<TM>)@delegate)(t, tm);
        }

        /// <summary>
        /// Fast get a member value of a given type instance
        /// Execution efficiency is some better than 'GetValueByExpr'
        /// </summary>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        public static object FastGetValueByExpr(T t, string memberName) =>
            _getFunc(t, memberName);

        /// <summary>
        /// Execution efficiency is some better than 'SetValueByExpr'
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <param name="tm"></param>
        public static void FastSetValueByExpr<TM>(T t, string memberName, TM tm) =>
            _setFunc(t, memberName, tm);

        private static Func<T, string, object> _getFunc;
        private static Action<T, string, object> _setFunc;
        static TypeUtil()
        {
            _getFunc = GenerateGetValueByExpr();
            _setFunc = GenerateSetValueByExpr();
        }

        /// <summary>
        /// Dynamicly generate 'GetValue' for a given type <seealso cref="T"/>
        /// Notice: the property name can not be comprehentive and count of properties should not be too large
        /// </summary>
        /// <returns></returns>
        private static Func<T, string, object> GenerateGetValueByExpr()
        {
            var type = typeof(T);
            var instance = Expression.Parameter(typeof(T), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var property = Expression.Property(/*Expression.Convert(instance, typeof(T))*/instance, propertyInfo.Name);
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Func<T, string, object>>(methodBody, instance, memberName).Compile();
        }

        /// <summary>
        /// Dynamicly generate 'SetValue' for a given type <seealso cref="T"/>
        /// Notice: the property name can not be comprehentive and count of properties should not be too large
        /// </summary>
        /// <returns></returns>
        private static Action<T, string, object> GenerateSetValueByExpr()
        {
            var type = typeof(T);
            var instance = Expression.Parameter(typeof(T), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var newValue = Expression.Parameter(typeof(object), "newValue");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var property = Expression.Property(/*Expression.Convert(instance, typeof(T))*/instance, propertyInfo.Name);
                var setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Action<T, string, object>>(methodBody, instance, memberName, newValue).Compile();
        }
    }

    /// <summary>
    /// If we want to access some class which has implemented the interface <seealso cref="IMemberAccessor"/>, but the concrete type of the instance
    /// is not known, we can use this Class methods to work it out
    /// </summary>
    public class TypeUtil
    {
        private static Dictionary<Type, IMemberAccessor> _accessDict = new Dictionary<Type, IMemberAccessor>();

        private static IMemberAccessor NewInstanceOfIAccessor(Type type)
        {
            var ctorInfo = type.GetConstructor(Type.EmptyTypes);

            var dm = new DynamicMethod("Instantiate_IMemberAccessor" + type.Name, type, new Type[0], ctorInfo.DeclaringType);
            var il = dm.GetILGenerator();
            il.DeclareLocal(ctorInfo.DeclaringType);
            il.Emit(OpCodes.Newobj, ctorInfo);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            var method = (Func<IMemberAccessor>)dm.CreateDelegate(typeof(Func<IMemberAccessor>));
            return method();
        }

        /// <summary>
        /// Get accessor of class level
        /// Note that: instance type must inherit interface <seealso cref="IMemberAccessor"/>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private IMemberAccessor GetClassAccessor(object instance)
        {
            var type = instance.GetType();

            IMemberAccessor accessor;
            if(!_accessDict.TryGetValue(type, out accessor))
            {
                lock(_accessDict)
                {
                    if(!_accessDict.TryGetValue(type, out accessor))
                    {
                        //accessor = NewInstance(typeof(TypeUtil<>).MakeGenericType(type));
                        accessor = Activator.CreateInstance(typeof(TypeUtil<>).MakeGenericType(type)) as IMemberAccessor;
                        _accessDict[type] = accessor;
                    }
                }
            }
            return accessor;
        }

        public object GetValue(object instance, string memberName) => GetClassAccessor(instance).GetValue(instance, memberName);
        public void SetValue(object instance, string memberName, object newValue) => GetClassAccessor(instance).SetValue(instance, memberName, newValue);


        #region Implemented with PropertyAccessor's help
        private static Dictionary<PropertyAccessorKey, PropertyAccessor> propertyAccessors = new Dictionary<PropertyAccessorKey, PropertyAccessor>();
        public static object Get(object obj, string propertyName)
        {
            PropertyAccessor propertyAccessor;
            PropertyAccessorKey key = new PropertyAccessorKey(obj.GetType(), propertyName);
            if (propertyAccessors.ContainsKey(key))
            {
                propertyAccessor = propertyAccessors[key];
                return propertyAccessor.Get(obj);
            }
            lock (PropertyAccessor._lock)
            {
                if (propertyAccessors.ContainsKey(key))
                {
                    propertyAccessor = propertyAccessors[key];
                    return propertyAccessor.Get(obj);
                }
                propertyAccessor = new PropertyAccessor(obj.GetType(), propertyName);
                propertyAccessors[key] = propertyAccessor;
            }
            return propertyAccessor.Get(obj);
        }

        public static void Set(object obj, string propertyName, object value)
        {
            PropertyAccessor propertyAccessor;
            PropertyAccessorKey key = new PropertyAccessorKey(obj.GetType(), propertyName);
            if (propertyAccessors.ContainsKey(key))
            {
                propertyAccessor = propertyAccessors[key];
                propertyAccessor.Set(obj, value);
                return;
            }
            lock (PropertyAccessor._lock)
            {
                if (propertyAccessors.ContainsKey(key))
                {
                    propertyAccessor = propertyAccessors[key];
                    propertyAccessor.Set(obj, value);
                    return;
                }
                propertyAccessor = new PropertyAccessor(obj.GetType(), propertyName);
                propertyAccessors[key] = propertyAccessor;
            }
            propertyAccessor.Set(obj, value);
        }

        #endregion
    }


    /// <summary>
    /// Given a type and a property name of the type, 
    /// this class provides a pair methods of GetValue and SetValue which can 
    /// access the special property of all instances of this type
    /// </summary>
    public class PropertyAccessor
    {
        private static Dictionary<Type, OpCode> _valueTypeOpCodes;
        public static readonly object _lock = new object();
        static PropertyAccessor()
        {
            InitTypeOpCodes();
        }

        private static void InitTypeOpCodes()
        {
            _valueTypeOpCodes = new Dictionary<Type, OpCode>(15);
            _valueTypeOpCodes[typeof(byte)] = OpCodes.Ldind_U1;
            _valueTypeOpCodes[typeof(char)] = OpCodes.Ldind_U2;
            _valueTypeOpCodes[typeof(ushort)] = OpCodes.Ldind_U2;
            _valueTypeOpCodes[typeof(uint)] = OpCodes.Ldind_U4;
            _valueTypeOpCodes[typeof(sbyte)] = OpCodes.Ldind_I1;
            _valueTypeOpCodes[typeof(bool)] = OpCodes.Ldind_I1;
            _valueTypeOpCodes[typeof(short)] = OpCodes.Ldind_I2;
            _valueTypeOpCodes[typeof(int)] = OpCodes.Ldind_I4;
            _valueTypeOpCodes[typeof(long)] = OpCodes.Ldind_I8;
            _valueTypeOpCodes[typeof(ulong)] = OpCodes.Ldind_I8;
            _valueTypeOpCodes[typeof(float)] = OpCodes.Ldind_R4;
            _valueTypeOpCodes[typeof(double)] = OpCodes.Ldind_R8;
        }

        public string PropertyName { get; private set; }
        public Type Type { get; private set; }
        private Action<object, object> _setValue;
        private Func<object, object> _getValue;
        public PropertyAccessor(Type type, string propertyName)
        {
            PropertyName = propertyName;
            Type = type;
            _getValue = CreateGetFunc();
            _setValue = CreateSetAct();
        }

        public object Get(object obj) => _getValue(obj);
        public void Set(object obj, object newVal) => _setValue(obj, newVal);


        private Func<object, object> CreateGetFunc()
        {
            var dm = new DynamicMethod("GetValue", typeof(object), new Type[] { typeof(object) });
            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(object));
            il.Emit(OpCodes.Ldarg_0);       // Func<object, object> 中的第一个输入参数object
            il.Emit(OpCodes.Castclass, Type);   // 强制转换到类类型
            var getMethod = Type.GetMethod($"get_{PropertyName}");
            
            il.EmitCall(OpCodes.Call, getMethod, null);
            if (getMethod.ReturnType.IsValueType)
                il.Emit(OpCodes.Box, getMethod.ReturnType);     // Boxing

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            dm.DefineParameter(1, ParameterAttributes.In, "value");
            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }

        private Action<object, object> CreateSetAct()
        {
            var dm = new DynamicMethod("SetValue", null, new Type[] { typeof(object), typeof(object) });
            var il = dm.GetILGenerator();

            var setMethod = Type.GetMethod($"set_{PropertyName}");
            var paramtype = setMethod.GetParameters()[0].ParameterType;

            il.DeclareLocal(paramtype);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, Type);

            il.Emit(OpCodes.Ldarg_1);

            if (paramtype.IsValueType)
            {
                il.Emit(OpCodes.Unbox, paramtype);  // unboxing
                if (_valueTypeOpCodes.ContainsKey(paramtype))
                {
                    var load = (OpCode)_valueTypeOpCodes[paramtype];
                    il.Emit(load);
                }
                else
                    il.Emit(OpCodes.Ldobj, paramtype);
            }
            else
                il.Emit(OpCodes.Castclass, paramtype);

            il.EmitCall(OpCodes.Callvirt, setMethod, null);
            il.Emit(OpCodes.Ret);

            dm.DefineParameter(1, ParameterAttributes.In, "obj");
            dm.DefineParameter(2, ParameterAttributes.In, "value");

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }
    }

    public class PropertyAccessorKey
    {
        public Type Type { get; private set; }
        public string PropertyName { get; private set; }
        public PropertyAccessorKey(Type type, string propertyName)
        {
            Type = type;
            PropertyName = propertyName;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ PropertyName.GetHashCode();
        }
    }
}
