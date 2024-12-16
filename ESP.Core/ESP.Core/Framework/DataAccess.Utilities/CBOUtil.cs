using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ESP.Framework.DataAccess.Utilities
{
    /// <summary>
    /// OR转换辅助类
    /// </summary>
    public static class CBOUtil
    {
        /// <summary>
        /// 从DataReader中指定字段读取Boolean型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static bool GetBoolean(IDataReader reader, string field, bool defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetBoolean(index);
        }

        /// <summary>
        /// 从DataReader中指定字段读取Byte型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static byte GetByte(IDataReader reader, string field, byte defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetByte(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Byte型数组，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static byte[] GetBytes(IDataReader reader, string field, byte[] defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            long length = reader.GetBytes(index, 0, null, 0, 0);

            if (length > int.MaxValue)
                throw new OutOfMemoryException();

            byte[] buf = new byte[length];
            reader.GetBytes(index, 0, buf, 0, buf.Length);

            return buf;

        }

        /// <summary>
        /// 从DataReader中指定字段读取Char型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static char GetChar(IDataReader reader, string field, char defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetChar(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Char型数组，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static char[] GetChars(IDataReader reader, string field, char[] defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            long length = reader.GetChars(index, 0, null, 0, 0);

            if (length > int.MaxValue)
                throw new OutOfMemoryException();

            char[] buf = new char[length];
            reader.GetChars(index, 0, buf, 0, buf.Length);

            return buf;
        }

        /// <summary>
        /// 从DataReader中指定字段读取DateTime型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static DateTime GetDateTime(IDataReader reader, string field, DateTime defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetDateTime(index);
        }

        /// <summary>
        /// 从DataReader中指定字段读取Decimal型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static decimal GetDecimal(IDataReader reader, string field, decimal defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetDecimal(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Double型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static double GetDouble(IDataReader reader, string field, double defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetDouble(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Single型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static float GetFloat(IDataReader reader, string field, float defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetFloat(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Guid型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static Guid GetGuid(IDataReader reader, string field, Guid defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetGuid(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Int16型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static short GetInt16(IDataReader reader, string field, short defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetInt16(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Int32型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static int GetInt32(IDataReader reader, string field, int defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetInt32(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取Int64型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static long GetInt64(IDataReader reader, string field, long defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetInt64(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取String型值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字段的值</returns>
        public static string GetString(IDataReader reader, string field, string defaultValue)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return defaultValue;
            }

            return reader.GetString(index);

        }

        /// <summary>
        /// 从DataReader中指定字段读取任意类型的值，如果该字段为空，返回默认值
        /// </summary>
        /// <param name="reader">DataReader对象</param>
        /// <param name="field">要读取的字段</param>
        /// <returns>字段的值</returns>
        public static object GetValue(IDataReader reader, string field)
        {
            int index;
            try
            {
                index = reader.GetOrdinal(field);
            }
            catch
            {
                index = -1;
            }

            if (index == -1 || reader.IsDBNull(index))
            {
                return null;
            }

            return reader.GetValue(index);
        }

    }
}
