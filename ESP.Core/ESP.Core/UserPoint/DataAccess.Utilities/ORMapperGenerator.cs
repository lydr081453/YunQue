using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Reflection.Emit;
using System.Data;
using System.Threading;
using System.Data.SqlClient;

namespace ESP.UserPoint.DataAccess.Utilities
{
    internal delegate void LoadObjectDelegate<T>(IDataReader reader, ref T obj);

    internal static class ORMapperGenerator
    {
        static ReaderWriterLock _lock = new ReaderWriterLock();
        static Hashtable _Mappers = new Hashtable();

        class MemberInformation
        {
            public MemberInfo member;
            public string column;
            //public DataColumnAction action;
            public object NullValue;
            public Type dataConvertor;
            public Type memberType;
            public MethodInfo setMethod;
        }

        /*
        private static MemberInformation[] GetAllMembers(Type type, DataColumnAction defaultAction)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            ArrayList members = new ArrayList();

            ArrayList list = new ArrayList();
            list.AddRange(properties);
            list.AddRange(fields);

            foreach (MemberInfo member in list)
            {
                MemberInformation m = new MemberInformation();
                object[] memberAttrs = member.GetCustomAttributes(typeof(DataColumnAttribute), false);
                DataColumnAttribute memberAttr = (memberAttrs == null || memberAttrs.Length == 0) ? null : memberAttrs[0] as DataColumnAttribute;
                if (memberAttr == null)
                {
                    m.action = defaultAction;
                    m.column = member.Name;
                    m.dataConvertor = null;
                    m.NullValue = null;
                }
                else
                {
                    m.action = memberAttr.Action;
                    m.column = memberAttr.ColumnName == null ? member.Name : memberAttr.ColumnName;
                    m.dataConvertor = memberAttr.DataConvertor;
                    m.NullValue = memberAttr.NullValue;
                }
                m.member = member;
                members.Add(m);
            }
            return (MemberInformation[])members.ToArray(typeof(MemberInformation));
        }
        */

        /*
        private static Type GetMemberType(MemberInfo member)
        {
            if (member is FieldInfo)
            {
                FieldInfo f = (FieldInfo)member;
                return f.FieldType;
            }
            else if (member is PropertyInfo)
            {
                PropertyInfo p = (PropertyInfo)member;
                return p.PropertyType;
            }

            return null;
        }
        */

        private static void EmitDefaultValue(ILGenerator ilg, object nullValue, Type type)
        {

            if (nullValue == null)
            {
                nullValue = CBO.NullValues[type];
            }

            TypeCode typeCode = Type.GetTypeCode(type);

            try
            {
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        {
                            bool bValue = (bool)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)(bValue ? 1 : 0));
                        }
                        return;
                    case TypeCode.Byte:
                        {
                            byte byteValue = (byte)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)byteValue);
                        }
                        return;
                    case TypeCode.Char:
                        {
                            char charValue = (char)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)charValue);
                        }
                        return;
                    case TypeCode.DateTime:
                        {
                            long ticksValue;
                            if (nullValue is long)
                            {
                                ticksValue = (long)nullValue;
                            }
                            else if (nullValue is string)
                            {
                                DateTime dt = DateTime.Parse((string)nullValue);
                                ticksValue = dt.Ticks;
                            }
                            else if (nullValue is DateTime)
                            {
                                ticksValue = ((DateTime)nullValue).Ticks;
                            }
                            else
                            {
                                throw new FormatException();
                            }

                            ConstructorInfo dateTimeConstructor = typeof(DateTime).GetConstructor(new Type[] { typeof(long) });

                            ilg.Emit(OpCodes.Ldc_I8, ticksValue);
                            ilg.Emit(OpCodes.Newobj, dateTimeConstructor);
                        }
                        return;
                    case TypeCode.Decimal:
                        {
                            decimal decValue = (decimal)nullValue;
                            int[] bits = decimal.GetBits(decValue);

                            int lo, mid, hi;
                            int isNegative;
                            int scale;

                            lo = bits[0];
                            mid = bits[1];
                            hi = bits[2];
                            isNegative = (int)((bits[3] & 0x80000000) == 0 ? 0 : 1);
                            scale = ((bits[3] & 0xFF0000) >> 16);

                            ConstructorInfo decimalConstructor = typeof(decimal).GetConstructor(
                                new Type[] { typeof(int), typeof(int), typeof(int), typeof(bool), typeof(byte) });

                            ilg.Emit(OpCodes.Ldc_I4, lo);
                            ilg.Emit(OpCodes.Ldc_I4, mid);
                            ilg.Emit(OpCodes.Ldc_I4, hi);
                            ilg.Emit(OpCodes.Ldc_I4, isNegative);
                            ilg.Emit(OpCodes.Ldc_I4, scale);
                            ilg.Emit(OpCodes.Newobj, decimalConstructor);
                        }
                        return;
                    case TypeCode.Double:
                        {
                            double doubleValue = (double)nullValue;
                            ilg.Emit(OpCodes.Ldc_R8, doubleValue);
                        }
                        return;
                    case TypeCode.Int16:
                        {
                            short shortValue = (short)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)shortValue);
                        }
                        return;
                    case TypeCode.Int32:
                        {
                            int intValue = (int)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, intValue);
                        }
                        return;
                    case TypeCode.Int64:
                        {
                            long longValue = (long)nullValue;
                            ilg.Emit(OpCodes.Ldc_I8, longValue);
                        }
                        return;
                    case TypeCode.SByte:
                        {
                            sbyte sbyteValue = (sbyte)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)sbyteValue);
                        }
                        return;
                    case TypeCode.Single:
                        {
                            float floatValue = (float)nullValue;
                            ilg.Emit(OpCodes.Ldc_R4, floatValue);
                        }
                        return;
                    case TypeCode.String:
                        {
                            string stringValue = (string)nullValue;
                            if (stringValue == null)
                                ilg.Emit(OpCodes.Ldnull);
                            else
                                ilg.Emit(OpCodes.Ldstr, stringValue);
                        }
                        return;
                    case TypeCode.UInt16:
                        {
                            ushort ushorValue = (ushort)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, (int)ushorValue);
                        }
                        return;
                    case TypeCode.UInt32:
                        {
                            uint uintValue = (uint)nullValue;
                            ilg.Emit(OpCodes.Ldc_I4, uintValue);
                        }
                        return;
                    case TypeCode.UInt64:
                        {
                            ulong ulongValue = (ulong)nullValue;
                            ilg.Emit(OpCodes.Ldc_I8, ulongValue);
                        }
                        return;
                }

                if (type == typeof(Guid))
                {
                    Guid guidValue;
                    if (nullValue is string)
                    {
                        string stringValue = (string)nullValue;
                        guidValue = new Guid(stringValue);
                    }
                    else if (nullValue is byte[])
                    {
                        byte[] bytesValue = (byte[])nullValue;
                        guidValue = new Guid(bytesValue);
                    }
                    else
                    {
                        throw new FormatException();
                    }

                    byte[] bytes = guidValue.ToByteArray();

                    uint a = bytes[3];
                    a = a << 8;
                    a = a | bytes[2];
                    a = a << 8;
                    a = a | bytes[1];
                    a = a << 8;
                    a = a | bytes[0];

                    int b = bytes[5];
                    b = b << 8;
                    b = b | bytes[4];

                    int c = bytes[7];
                    c = c << 8;
                    c = c | bytes[6];

                    ConstructorInfo guidConstructor = typeof(Guid).GetConstructor(new Type[] { 
                        typeof(uint), typeof(ushort), typeof(ushort), 
                        typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte)
                    });

                    ilg.Emit(OpCodes.Ldc_I4, a);
                    ilg.Emit(OpCodes.Ldc_I4, b);
                    ilg.Emit(OpCodes.Ldc_I4, c);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[8]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[9]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[10]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[11]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[12]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[13]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[14]);
                    ilg.Emit(OpCodes.Ldc_I4_S, bytes[15]);

                    ilg.Emit(OpCodes.Newobj, guidConstructor);
                    return;
                }

                if (type == typeof(byte[]))
                {
                    byte[] buf = (byte[])nullValue;
                    if (buf == null)
                    {
                        ilg.Emit(OpCodes.Ldnull);
                    }
                    else
                    {
                        ilg.Emit(OpCodes.Ldc_I4, buf.Length);
                        ilg.Emit(OpCodes.Newarr, typeof(byte));

                        for (int i = 0; i < buf.Length; i++)
                        {
                            ilg.Emit(OpCodes.Dup);
                            ilg.Emit(OpCodes.Ldc_I4, i);
                            ilg.Emit(OpCodes.Ldc_I4_S, buf[i]);
                            ilg.Emit(OpCodes.Stelem_I1);
                        }
                    }

                    return;
                }

                if (type == typeof(char[]))
                {
                    char[] buf = (char[])nullValue;
                    if (buf == null)
                    {
                        ilg.Emit(OpCodes.Ldnull);
                    }
                    else
                    {
                        ilg.Emit(OpCodes.Ldc_I4, buf.Length);
                        ilg.Emit(OpCodes.Newarr, typeof(char));

                        for (int i = 0; i < buf.Length; i++)
                        {
                            ilg.Emit(OpCodes.Dup);
                            ilg.Emit(OpCodes.Ldc_I4, i);
                            ilg.Emit(OpCodes.Ldc_I4, buf[i]);
                            ilg.Emit(OpCodes.Stelem_I2);
                        }
                    }

                    return;
                }
            }
            catch
            {
                throw new InvalidCastException();
            }

            throw new InvalidCastException();
        }

        /*
        private static MemberInformation[] GetAllMembers(Type type)
        {
            object[] typeAttrs = type.GetCustomAttributes(typeof(DataTableAttribute), false);
            DataTableAttribute typeAttr = (typeAttrs == null || typeAttrs.Length == 0) ? null : typeAttrs[0] as DataTableAttribute;
            DataColumnAction defaultAction = typeAttr == null ? DataColumnAction.None : typeAttr.DefaultAction;

            return GetAllMembers(type, defaultAction);

        }
        */

        private static MemberInformation[] GetAllMembers(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ArrayList list = new ArrayList(properties.Length);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    MemberInformation info = new MemberInformation();
                    //info.action = DataColumnAction.Load;
                    info.column = property.Name;
                    info.dataConvertor = null;
                    info.member = property;
                    info.memberType = property.PropertyType;
                    info.NullValue = null;
                    info.setMethod = property.GetSetMethod(true);

                    list.Add(info);
                }
            }

            return (MemberInformation[])list.ToArray(typeof(MemberInformation));
        }


        private static LoadObjectDelegate<T> BuildTypeLoad<T>() where T : new()
        {
            Type type = typeof(T);

            MemberInformation[] members = GetAllMembers(type);

            ConstructorInfo constructor = type.GetConstructor(System.Type.EmptyTypes);

            Type cboUtilType = typeof(CBOUtil);

            // 不同类型的成员，使用不同类型的辅助方法从 IDataRecord 中取值
            Dictionary<Type, MethodInfo> methods = new Dictionary<Type, MethodInfo>();
            methods.Add(typeof(bool), cboUtilType.GetMethod("GetBoolean"));
            methods.Add(typeof(byte), cboUtilType.GetMethod("GetByte"));
            methods.Add(typeof(sbyte), cboUtilType.GetMethod("GetByte"));
            methods.Add(typeof(byte[]), cboUtilType.GetMethod("GetBytes"));
            methods.Add(typeof(char), cboUtilType.GetMethod("GetChar"));
            methods.Add(typeof(char[]), cboUtilType.GetMethod("GetChars"));
            methods.Add(typeof(DateTime), cboUtilType.GetMethod("GetDateTime"));
            methods.Add(typeof(decimal), cboUtilType.GetMethod("GetDecimal"));
            methods.Add(typeof(double), cboUtilType.GetMethod("GetDouble"));
            methods.Add(typeof(float), cboUtilType.GetMethod("GetFloat"));
            methods.Add(typeof(Guid), cboUtilType.GetMethod("GetGuid"));
            methods.Add(typeof(short), cboUtilType.GetMethod("GetInt16"));
            methods.Add(typeof(ushort), cboUtilType.GetMethod("GetInt16"));
            methods.Add(typeof(int), cboUtilType.GetMethod("GetInt32"));
            methods.Add(typeof(uint), cboUtilType.GetMethod("GetInt32"));
            methods.Add(typeof(long), cboUtilType.GetMethod("GetInt64"));
            methods.Add(typeof(ulong), cboUtilType.GetMethod("GetInt64"));
            methods.Add(typeof(string), cboUtilType.GetMethod("GetString"));
            // object 类型的成员不能直接支持，需特殊处理，辅助方法单独存放
            MethodInfo methods_object = cboUtilType.GetMethod("GetValue");

            // 用于执行数据类型转换的方法
            //MethodInfo dataConvertor_load = typeof(IDataConvertor).GetMethod("Load");

            // 定义动态方法以便使用 IDataRecord 中的数据填充对象
            // T LoadObject_xxxxxx(IDataRecord dr, ref T obj)
            string methodName = "LoadObject_" + Guid.NewGuid().ToString("N");
            DynamicMethod dynamicMethod = new DynamicMethod(methodName, null,
                new Type[] { typeof(IDataRecord), type.MakeByRefType() }, type, true);

            // 获取动态方法的 IL 生成器
            ILGenerator ilg = dynamicMethod.GetILGenerator();

            // 声明局部变量
            LocalBuilder target = null;
            LocalBuilder value = ilg.DeclareLocal(typeof(object));
            //LocalBuilder convertor = ilg.DeclareLocal(typeof(IDataConvertor));

            if (!type.IsValueType)
            {
                target = ilg.DeclareLocal(type);
                //定义 Label
                Label target_Ready = ilg.DefineLabel();

                // 检查 T& 类型参数(1)是否指向空引用 
                ilg.Emit(OpCodes.Ldarg_1);
                ilg.Emit(OpCodes.Ldind_Ref);
                ilg.Emit(OpCodes.Brtrue_S, target_Ready);

                // 如果参数(1)指向空引用则创建一个新的 T 类型对象并将其引用(T&)存入参数(1)
                ilg.Emit(OpCodes.Ldarg_1);
                ilg.Emit(OpCodes.Newobj, constructor);
                ilg.Emit(OpCodes.Stind_Ref);

                // 参数(1)中的对象已就绪
                ilg.MarkLabel(target_Ready);

                // 将参数(1)所指向的对象存入局部变量 target 备用
                ilg.Emit(OpCodes.Ldarg_1);
                ilg.Emit(OpCodes.Ldind_Ref);
                ilg.Emit(OpCodes.Stloc, target);
            }
            // 对每一个成员进行数据加载
            foreach (MemberInformation m in members)
            {
                //if ((m.action & DataColumnAction.Load) != DataColumnAction.Load)
                //    continue;

                Type memberType = m.memberType;
                object nullValue = m.NullValue;
                Type dataConvertor = m.dataConvertor;

                if (type.IsValueType)
                {
                    ilg.Emit(OpCodes.Ldarg_1);
                }
                else
                {
                    ilg.Emit(OpCodes.Ldloc, target);
                }

                if (dataConvertor != null)
                {
                    ilg.Emit(OpCodes.Newobj, dataConvertor.GetConstructor(System.Type.EmptyTypes));
                }


                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldstr, m.column);

                if (dataConvertor == null)
                {
                    EmitDefaultValue(ilg, nullValue, memberType);
                }

                Type key = memberType.IsEnum ? Enum.GetUnderlyingType(memberType) : memberType;

                MethodInfo mth = null;
                //if (dataConvertor != null)
                //{
                //    mth = methods_object;
                //}
                //else 
                if (methods.ContainsKey(key))
                {
                    mth = methods[key];
                }
                else
                {
                    throw new InvalidCastException();
                }

                ilg.EmitCall(OpCodes.Call, mth, null);

                //if (dataConvertor != null)
                //{
                //    ilg.Emit(OpCodes.Callvirt, dataConvertor_load);
                //}

                if (m.setMethod == null)
                {
                    ilg.Emit(OpCodes.Stfld, (FieldInfo)m.member);
                }
                else
                {
                    ilg.Emit(OpCodes.Callvirt, m.setMethod);
                }
            }

            // 返回
            ilg.Emit(OpCodes.Ret);

            // 生成动态方法的代理
            return (LoadObjectDelegate<T>)dynamicMethod.CreateDelegate(typeof(LoadObjectDelegate<T>));
        }

        public static object _LockObj = new object();
        public static LoadObjectDelegate<T> GetDelegate<T>() where T : new()
        {
            LoadObjectDelegate<T> d = null;
            lock (_LockObj)
            {
                d = (LoadObjectDelegate<T>)_Mappers[typeof(T).AssemblyQualifiedName];
                if (d == null)
                {
                    d = BuildTypeLoad<T>();
                    _Mappers[typeof(T).AssemblyQualifiedName] = d;
                }
            }


            return d;
        }
    }
}
