using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// clsSelect 的摘要说明。
	/// </summary>
	public class clsSelect
	{
		public clsSelect()
		{

		}
		
		
		#region QueryBySql		
		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable QueryBySql(string strSql)
		{
			
			if(strSql==null || strSql.Trim()=="") return null;
			DataTable dt=null;
			

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;	
		}
		
		/// <summary>
		/// 事务内查询
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="ts"></param>
		/// <returns></returns>
		public static DataTable QueryBySql(string strSql, SqlTransaction ts)
		{
			if(strSql==null || strSql.Trim()=="") return null;
			//执行查询操作
			return ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(ts,CommandType.Text,strSql).Tables[0];
		}
		

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <param name="ErrorMessage">返回的错误信息</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable QueryBySql(string strSql,ref string ErrorMessage)
		{
			
			if(strSql==null || strSql.Trim()=="") 
			{
				ErrorMessage="sql字符串为空";
				return null;
			}
			DataTable dt=null;
			

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;	
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strSql">sql字符串</param>
		/// <param name="param">参数列表</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable QueryBySql(string strSql,params SqlParameter[] param)
		{
			
			if(strSql==null || strSql.Trim()=="") return null;
			DataTable dt=null;

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
                if (param == null || param.Length <= 0)
                {
                    dt = ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql).Tables[0];
                }
                else
                {
                    dt = ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql, param).Tables[0];
                }
				conn.Close();
			}
			return dt;	
		}

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="strSql">sql字符串</param>
        /// <param name="param">参数列表</param>
        /// <returns>返回的结果集,如果没有查到,返回null</returns>
        public static DataTable QueryBySql(SqlTransaction trans,string strSql, params SqlParameter[] param)
        {

            if (strSql == null || strSql.Trim() == "") return null;
            if (param == null || param.Length <= 0) return null;
            DataTable dt = null;

            //执行查询操作
            dt = ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(trans, CommandType.Text, strSql, param).Tables[0];
            return dt;
        }


		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strSql">sql字符串</param>
		/// <param name="ErrorMessage">错误信息</param>
		/// <param name="param">参数列表</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable QueryBySql(string strSql,ref string ErrorMessage,params SqlParameter[] param)
		{
			
			if(strSql==null || strSql.Trim()=="") 
			{
				ErrorMessage="sql字符串为空";
				return null;
			}
			if(param==null || param.Length<=0)
			{
				ErrorMessage="SqlParameter[]参数为空";
				return null;
			}
			DataTable dt=null;

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql,param).Tables[0];
				conn.Close();
			}
			return dt;	
		}
		#endregion

		
		#region funSelect
		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName)
		{
			return funSelect(strTableName,"*","");
		}


		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strTerms">要查询条件</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,IDictionary Terms)
		{
			return funSelect(strTableName,"*",Terms);
		}

		
		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string[] strColumns)
		{
			return funSelect(strTableName,strColumns,"");
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string strColumns)
		{
			return funSelect(strTableName,strColumns,"");
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <param name="Terms">查询条件</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string[] strColumns,IDictionary Terms)
		{
			if (strTableName==null || strTableName.Trim()=="") return null;
			if (strColumns==null || strColumns.Length<=0) return null;
			string selectColumns="";
			for(int i=0;i<strColumns.Length;i++)
			{
				if(strColumns[i].Trim()!=null && strColumns[i].Trim()!="")
				{
					selectColumns+=strColumns[i].Trim()+", ";
				}
			}
			selectColumns=selectColumns.Substring(0,selectColumns.Length-2);
			return funSelect(strTableName,selectColumns,Terms);
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <param name="Terms">查询条件</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string[] strColumns,string strTerms)
		{
			if (strTableName==null || strTableName.Trim()=="") return null;
			if (strColumns==null || strColumns.Length<=0) return null;
			string selectColumns="";
			for(int i=0;i<strColumns.Length;i++)
			{
				if(strColumns[i].Trim()!=null && strColumns[i].Trim()!="")
				{
					selectColumns+=strColumns[i].Trim()+", ";
				}
			}
			selectColumns=selectColumns.Substring(0,selectColumns.Length-2);
			return funSelect(strTableName,selectColumns,strTerms);
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <param name="strTerms">查询条件</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string strColumns,string strTerms)
		{
			if (strTableName==null || strTableName.Trim()=="") return null;
			if (strColumns==null || strColumns.Trim()=="") return null;
			string strSql="";
			if(strColumns.Trim()=="*")
			{
				strSql+="select * from "+strTableName;
			}
			else
			{
				strSql+="select "+strColumns+ " from "+strTableName;
			}

			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where " +strTerms;
			}

			DataTable dt=null;

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;		
		}

		/// <summary>
		/// 执行查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumns">要查询的字段名</param>
		/// <param name="Terms">查询条件</param>
		/// <returns>返回的结果集,如果没有查到,返回null</returns>
		public static DataTable funSelect(string strTableName,string strColumns,IDictionary Terms)
		{
			if (strTableName==null || strTableName.Trim()=="") return null;
			if (strColumns==null || strColumns.Trim()=="") return null;
			string strSql="";
			if(strColumns.Trim()=="*")
			{
				strSql+="select * from "+strTableName;
			}
			else
			{
				strSql+="select "+strColumns+ " from "+strTableName;
			}

			SqlParameter[] param =null;
			if(Terms!=null && Terms.Keys.Count>0)
			{
				string strTerms="";
				int num=Terms.Count;
				string[] Key=new string[num];
				string[] Value=new string[num];
				int i=0;

				foreach(DictionaryEntry entry in Terms)
				{
					Key[i]=entry.Key.ToString();
					Value[i]=entry.Value.ToString();
					strTerms+=Key[i];
					strTerms+="=";	
					strTerms+="@"+Key[i];
					strTerms+=", ";
					i++;
				}
				strTerms=strTerms.Substring(0,strTerms.Length-2);//去掉字符串中的最末位的", "

				if(strTerms.Trim()!="")
				{
					strSql+=" where " +strTerms;
				}
				//将传入的HashTable转换为SqlParameter
				param =  new SqlParameter[num];
				for(int j=0;j<num;j++)
				{
					param[j]=new SqlParameter("@"+Key[j],Value[j]);
				}
			}

			DataTable dt=null;

			//执行查询操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql,param).Tables[0];
				conn.Close();
			}
			return dt;		
		}
		#endregion

	}
}
