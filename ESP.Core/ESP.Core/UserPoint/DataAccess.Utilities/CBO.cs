using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace ESP.UserPoint.DataAccess.Utilities
{
    /// <summary>
    /// OR映射类
    /// </summary>
    public static class CBO
    {
        /// <summary>
        /// 逻辑空值
        /// </summary>
        public static readonly NullValues NullValues = new NullValues();

        /// <summary>
        /// 从DataReader中加载对象列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <returns>对象列表</returns>
        public static IList<T> LoadList<T>(IDataReader reader) where T : new()
        {
            IList<T> list = new List<T>();

            if (reader == null)
                return list;

            LoadObjectDelegate<T> d = ORMapperGenerator.GetDelegate<T>();
            try
            {
                while (reader.Read())
                {
                    T t = new T();
                    d(reader, ref t);
                    list.Add(t);
                }
            }
            finally
            {
                reader.Close();
            }
            return list;
        }

                /// <summary>
        /// 从DataReader中加载标量值列表
        /// </summary>
        /// <typeparam name="T">标量类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <returns>标量值列表</returns>
        public static IList<T> LoadScalarList<T>(IDataReader reader)
        {
            return LoadScalarList<T>(reader, true);
        }

        /// <summary>
        /// 从DataReader中加载标量值列表
        /// </summary>
        /// <typeparam name="T">标量类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <param name="withNull">是否包含空值记录</param>
        /// <returns>标量值列表</returns>
        public static IList<T> LoadScalarList<T>(IDataReader reader, bool withNull)
        {
            IList<T> list = new List<T>();
            try
            {
                while (reader.Read())
                {
                    object obj;

                    if (reader.IsDBNull(0))
                    {
                        if (!withNull)
                            continue;

                        obj = CBO.NullValues[typeof(T)];
                    }
                    else
                    {
                        obj = reader.GetValue(0);
                    }

                    if (obj is T)
                    {
                        list.Add((T)obj);
                    }
                    else
                    {
                        try
                        {
                            list.Add((T)Convert.ChangeType(obj, typeof(T)));
                        }
                        catch
                        {
                            obj = CBO.NullValues[typeof(T)];
                            list.Add((T)obj);
                        }
                    }
                }

                return list;
            }
            finally
            {
                reader.Close();
            }
        }



        /// <summary>
        /// 用DataReader中的数据填充对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <param name="target">要填充的对象</param>
        /// <returns>填充好的对象</returns>
        public static T FillObject<T>(IDataReader reader, ref T target) where T : new()
        {
            if (reader == null)
                return default(T);

            LoadObjectDelegate<T> d = ORMapperGenerator.GetDelegate<T>();
            try
            {
                if (reader.Read())
                {
                    d(reader, ref target);
                    return target;
                }
            }
            finally
            {
                reader.Close();
            }
            return target;

        }

        /// <summary>
        /// 从DataReader中的加载对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <param name="internalContext">内部文本参数</param>
        /// <returns>加载的对象</returns>
        public static T LoadObject<T>(IDataReader reader, ref object internalContext) where T : new()
        {
            T t = new T();
            if (internalContext == null)
                internalContext = ORMapperGenerator.GetDelegate<T>();

            LoadObjectDelegate<T> del = (LoadObjectDelegate<T>)internalContext;
            del(reader, ref t);
            return t;
        }

        /// <summary>
        /// 从DataReader中的加载对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">DataReader对象</param>
        /// <returns>加载的对象</returns>
        public static T LoadObject<T>(IDataReader reader) where T : new()
        {
            if (reader == null)
                return default(T);

            LoadObjectDelegate<T> d = ORMapperGenerator.GetDelegate<T>();
            try
            {
                if(reader.Read())
                {
                    T t = new T();
                    d(reader, ref t);
                    return t;
                }
            }
            finally
            {
                reader.Close();
            }
            return default(T);
        }
    }

    /// <summary>
    /// 逻辑空值
    /// </summary>
    public class NullValues
    {
        /// <summary>
        /// 空Boolean
        /// </summary>
        public const bool Boolean = false;

        /// <summary>
        /// 空Byte
        /// </summary>
        public const byte Byte = 0;

        /// <summary>
        /// 空字符
        /// </summary>
        public const char Char = '\0';

        /// <summary>
        /// 空DateTime
        /// </summary>
        public static readonly DateTime DateTime = new DateTime(1754, 1, 1);

        /// <summary>
        /// 空Decimal
        /// </summary>
        public const decimal Decimal = 0.0M;

        /// <summary>
        /// 空Double
        /// </summary>
        public const double Double = 0.0;

        /// <summary>
        /// 空Int16
        /// </summary>
        public const short Int16 = 0;

        /// <summary>
        /// 空Int32
        /// </summary>
        public const int Int32 = 0;

        /// <summary>
        /// 空Int64
        /// </summary>
        public const long Int64 = 0;

        ///// <summary>
        ///// 空SByte
        ///// </summary>
        //public const sbyte SByte = 0;

        /// <summary>
        /// 空浮点
        /// </summary>
        public const float Single = 0.0f;

        /// <summary>
        /// 空字符串
        /// </summary>
        public const string String = "";

        ///// <summary>
        ///// 空UInt16
        ///// </summary>
        //public const ushort UInt16 = 0;

        ///// <summary>
        ///// 空UInt32
        ///// </summary>
        //public const uint UInt32 = 0;

        ///// <summary>
        ///// 空UInt64
        ///// </summary>
        //public const ulong UInt64 = 0;

        /// <summary>
        /// 空Guid
        /// </summary>
        public static readonly Guid Guid = Guid.Empty;

        /// <summary>
        /// 空字节数组
        /// </summary>
        public static readonly byte[] ByteArray = null;

        /// <summary>
        /// 空字符数组
        /// </summary>
        public static readonly char[] CharArray = null;

        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly object Object = null;

        /// <summary>
        /// 根据类型获取逻辑空值
        /// </summary>
        /// <param name="key">类型</param>
        /// <returns>指定类型的逻辑空值</returns>
        public object this[Type key]
        {
            get
            {
                if(key.IsEnum)
                    key = Enum.GetUnderlyingType(key);
                TypeCode typeCode = Type.GetTypeCode(key);
                switch(typeCode)
                {
                    case TypeCode.Boolean:
                        return Boolean;

                    case TypeCode.Byte:
                        return Byte;

                    case TypeCode.Char:
                        return Char;

                    case TypeCode.DateTime:
                        return DateTime;

                    case TypeCode.Decimal:
                        return Decimal;

                    case TypeCode.Double:
                        return Double;

                    case TypeCode.Int16:
                        return Int16;

                    case TypeCode.Int32:
                        return Int32;

                    case TypeCode.Int64:
                        return Int64;

                    //case TypeCode.SByte:
                    //    return SByte;

                    case TypeCode.Single:
                        return Single;

                    case TypeCode.String:
                        return String;

                    //case TypeCode.UInt16:
                    //    return UInt16;

                    //case TypeCode.UInt32:
                    //    return UInt32;

                    //case TypeCode.UInt64:
                    //    return UInt64;
                }

                if (key == typeof(Guid))
                    return Guid;
                if (key == typeof(byte[]))
                    return ByteArray;
                if (key == typeof(char[]))
                    return CharArray;

                return Object;
            }
        }
    }

}
