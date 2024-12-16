using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// 数据库更新操作
	/// </summary>
	public class clsUpdate
	{
		public clsUpdate()
		{

		}

		/// <summary>
		/// 执行更新操作
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要更新的字段及要更新成的值</param>
		/// <param name="strTableName">要更新的表名</param>
		/// <param name="strTerms">更新条件</param>
		/// <returns>执行更新操作影响的行数</returns>
		public static int funUpdate(IDictionary dict,string strTableName,string strTerms)
		{
			if(dict==null||dict.Keys.Count==0) return 0;
			if (strTableName==null || strTableName.Trim()=="") return 0;
			//if (strTerms==null || strTerms.Trim()=="") return 0;

			int rows=0;
			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			string strSql=@"update "+strTableName+" set ";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//要更新的字段
				strSql+="=";	
				strSql+="@"+Key[i];//要更新成的值
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//去掉字符串中的最末位的", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";
			

			//将传入的HashTable转换为SqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}
			

			//执行更新操作
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				rows=ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql,param);
				conn.Close();
			}
			return rows;		
		}

		
		/// <summary>
		/// 更新操作
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <returns>更新操作影响的行数</returns>
		public static int funUpdate(string strSql)
		{
			return funUpdate(strSql,false);
		}
		
		/// <summary>
		/// 更新操作
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <param name="isTrans">是否是事务</param>
		/// <returns>更新操作影响的行数</returns>
		public static int funUpdate(string strSql,bool isTrans)
		{
			int rows=0;
			if(isTrans)
			{
				using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
				{
					conn.Open();
					SqlTransaction trans=conn.BeginTransaction();
					try
					{
						rows=ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strSql);
						trans.Commit();
					}
					catch(Exception ex)
					{
						rows=0;
						trans.Rollback();
						throw new Exception (ex.Message);
					}
					finally
					{
						conn.Close();
					}
				}
			}
			else
			{
				using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
				{
					conn.Open();
					rows=ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql);
					conn.Close();
				}
			}
			return rows;

		}


		/// <summary>
		/// 获取更新Sql语句
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要更新的字段及要更新成的值</param>
		/// <param name="strTableName">要更新的表名</param>
		/// <param name="strTerms">更新条件</param>
		/// <returns>更新Sql语句</returns>
		public static SqlParameter[] getUpdateParam(IDictionary dict,string strTableName,string strTerms,ref string strSql)
		{
			if(dict==null||dict.Keys.Count==0) return null;
			if (strTableName==null || strTableName.Trim()=="") return null;
			//if (strTerms==null || strTerms.Trim()=="") return 0;

			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			strSql=@"update "+strTableName+" set ";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//要更新的字段
				strSql+="=";	
				strSql+="@"+Key[i];//要更新成的值
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//去掉字符串中的最末位的", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";

			//将传入的HashTable转换为SqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}	
			return param;		
		}

		
		/// <summary>
		/// 获取更新Sql语句
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要更新的字段及要更新成的值</param>
		/// <param name="strTableName">要更新的表名</param>
		/// <param name="strTerms">更新条件</param>
		/// <returns>更新Sql语句</returns>
		public static string getUpdateSql(IDictionary dict,string strTableName,string strTerms)
		{
			if(dict==null||dict.Keys.Count==0) return null;
			if (strTableName==null || strTableName.Trim()=="") return null;
			//if (strTerms==null || strTerms.Trim()=="") return 0;

			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			string strSql=@"update "+strTableName+" set ";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//要更新的字段
				strSql+="=";	
				strSql+="@"+Key[i];//要更新成的值
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//去掉字符串中的最末位的", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";

			return strSql;		
		}

	}
}
