
namespace ESP.Media.Access.Utilities
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    /// <summary>
    /// ȫ�ְ���������
    /// </summary>
    public class Common
    {
        public Common()
        {
        }
        /// <summary>
        /// ���ַ����е�\r\n�滻����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceUR(string str)
        {
            str = str.Replace("\r", "");
            str = str.Replace("\n", "\\n");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        /// <summary>
        /// �ֵ������ת��ΪSqlParameter����
        /// </summary>
        /// <param name="dict">����KEY-VALUE���ݵ��ֵ������</param>
        /// <returns>SqlParameter����</returns>
        public static SqlParameter[] DictToSqlParam(IDictionary dict)
        {
            SqlParameter[] sqlParams = null;
            if (dict == null || dict.Keys.Count == 0)
                return sqlParams;

            ArrayList alParam = new ArrayList();
            foreach (DictionaryEntry entry in dict)
            {
                string strParamName = entry.Key.ToString();
                object paramValue = entry.Value;
                SqlParameter param = new SqlParameter(strParamName, typeof(string));
                param.Value = paramValue;
                alParam.Add(param);
                
            }
            sqlParams = new SqlParameter[alParam.Count];
            System.Array.Copy(alParam.ToArray(typeof(SqlParameter)), 0, sqlParams, 0, alParam.Count);
            return sqlParams;
        }


        public static int Find(List<SqlParameter> param, string key)
        {
            if (param == null || param.Count == 0) return -1;
            for (int i = 0; i < param.Count; i++)
            {
                SqlParameter p = param[i];
                if (p.ParameterName == key)
                {
                    return i;
                }
            }
            return -1;
        }


        public static void DataRowsToDataTable(DataRow[] drs, ref DataTable dt)
        {
            foreach (DataRow dr in drs)
            {
                dt.ImportRow(dr);
            }
        }

        public static DateTime? StringToDateTime(string strtime)
        {
            if (string.IsNullOrEmpty(strtime)) return null;
            DateTime time = DateTime.Now;
            bool ret = DateTime.TryParse(strtime, out time);
            if (ret)
            {
                return time;
            }
            return null;
        }



    }
}
