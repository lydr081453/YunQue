using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// ɾ������
	/// </summary>
	public class clsDelete
	{
		public clsDelete()
		{

		}
		
		/// <summary>
		/// ִ��ɾ������
		/// </summary>
		/// <param name="strTableName">����(˵�������ű���в���)</param>
		/// <param name="strTerms">����(˵����ʲô������ִ�в���)</param>
		/// <returns>ִ��Ӱ�������,���Ϊ0����δ���в���</returns>
		public static int funDelete(string strTableName,string  strTerms)
		{ 
			if(strTableName==null || strTableName.Trim()=="") return 0;
			if(strTerms==null || strTerms.Trim()=="") return 0;
			int rows=0;
			string strSql="delete @tableName where @terms";
			SqlParameter[] param=new SqlParameter[2];
			param[0]=new SqlParameter("@tableName",strTableName);
			param[1]=new SqlParameter("@terms",strTerms);
			using(SqlConnection conn=new SqlConnection(clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				rows=SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql,param);
				conn.Close();
			}
			return rows;
		}

	}
}
