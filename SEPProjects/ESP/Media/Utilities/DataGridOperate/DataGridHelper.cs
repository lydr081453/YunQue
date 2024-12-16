
namespace ESP.Media.Access.Utilities.DataGridOperate
{
	using System;
	using System.Data;
	using System.Collections;
	using System.Data.SqlClient;
	using System.Text;

	/// <summary>
	/// 全局帮助工具类
	/// </summary>
	public class DataGridHelper
	{
		public DataGridHelper()
		{
		}
		/// <summary>
		/// 将字符串中的\r\n替换函数
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ReplaceUR(string str)
		{
			str=str.Replace("\r","");
			str=str.Replace("\n","\\n");
			str=str.Replace("\"","\\\"");
			return str;
		}

		/// <summary>
		/// 字典类变量转化为SqlParameter数组
		/// </summary>
		/// <param name="dict">含有KEY-VALUE数据的字典类变量</param>
		/// <returns>SqlParameter数组</returns>
		public static SqlParameter[] DictToSqlParam(IDictionary  dict)
		{
			SqlParameter[] sqlParams=null;
			if(dict==null||dict.Keys.Count==0)
				return sqlParams;

			ArrayList alParam=new ArrayList();
			foreach(DictionaryEntry entry in dict)
			{
				string strParamName=entry.Key.ToString();
				object paramValue=entry.Value;
				alParam.Add(new SqlParameter(strParamName,paramValue));
			}

			sqlParams=new SqlParameter[alParam.Count];
			System.Array.Copy(alParam.ToArray(typeof(SqlParameter)),0,sqlParams,0,alParam.Count);
			return sqlParams;
		}
	}
}
