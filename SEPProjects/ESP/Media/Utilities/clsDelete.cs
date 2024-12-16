using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// 删除操作
	/// </summary>
	public class clsDelete
	{
		public clsDelete()
		{

		}
		
		/// <summary>
		/// 执行删除操作
		/// </summary>
		/// <param name="strTableName">表名(说明对哪张表进行操作)</param>
		/// <param name="strTerms">条件(说明在什么条件下执行操作)</param>
		/// <returns>执行影响的行数,如果为0表明未进行操作</returns>
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
