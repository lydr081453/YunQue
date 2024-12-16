
namespace ESP.Media.Access.Utilities.DataGridOperate
{
	using System;
	using System.Data;
	using System.Collections;
	using System.Data.SqlClient;
	using System.Text;

	/// <summary>
	/// ȫ�ְ���������
	/// </summary>
	public class DataGridHelper
	{
		public DataGridHelper()
		{
		}
		/// <summary>
		/// ���ַ����е�\r\n�滻����
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
		/// �ֵ������ת��ΪSqlParameter����
		/// </summary>
		/// <param name="dict">����KEY-VALUE���ݵ��ֵ������</param>
		/// <returns>SqlParameter����</returns>
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
