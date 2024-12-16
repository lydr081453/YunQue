using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// clsInsert 的摘要说明。
	/// </summary>
	public class clsInsert
	{
		public clsInsert()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 执行插入操作
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要插入的字段及要插入的值</param>
		/// <param name="strTableName">要插入的表名</param>
		/// <returns>执行插入操作影响的行数</returns>
		public static int funInsert(IDictionary dict,string strTableName)
		{
			if(dict==null||dict.Keys.Count==0) return 0;
			if (strTableName==null || strTableName.Trim()=="") return 0;

			int rows=0;
			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			string strSql=@"insert into  "+strTableName;
			string strKeys="";
			string strValues="";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//要插入的字段
				strValues+="@"+Key[i].Trim()+",";//要插入的值
				i++;
			}
			if(strKeys.Length>1)
			{
				strKeys=strKeys.Substring(0,strKeys.Length-1);
			}
			if(strValues.Length>1)
			{
				strValues=strValues.Substring(0,strValues.Length-1);
			}
			if(strKeys.Length<=0 || strValues.Length<=0) return 0;
			strSql+="("+strKeys+")"+" values ("+strValues+");";
			

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
		/// 获取插入的字符串
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要插入的字段及要插入的值</param>
		/// <param name="strTableName">要插入的表名</param>
		/// <returns>插入操作的Sql字符串</returns>
		public static string getInsertSql(IDictionary dict,string strTableName)
		{
			if(dict==null||dict.Keys.Count==0) return null;
			if (strTableName==null || strTableName.Trim()=="") return null;

			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			string strSql=@"insert into  "+strTableName;
			string strKeys="";
			string strValues="";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//要插入的字段
				strValues+="@"+Key[i].Trim()+",";//要插入的值
				i++;
			}
			if(strKeys.Length>1)
			{
				strKeys=strKeys.Substring(0,strKeys.Length-1);
			}
			if(strValues.Length>1)
			{
				strValues=strValues.Substring(0,strValues.Length-1);
			}
			if(strKeys.Length<=0 || strValues.Length<=0) return null;
			strSql+="("+strKeys+")"+" values ("+strValues+");";
			return strSql;
		}

		
		/// <summary>
		/// 获取插入的字符串及参数
		/// </summary>
		/// <param name="dict">实现IDictionary接口的,比如HashTable,用来表名要插入的字段及要插入的值</param>
		/// <param name="strTableName">要插入的表名</param>
		/// <param name="strSql">插入操作的Sql字符串</param>
		/// <returns>参数列表</returns>
		public static SqlParameter[] getInsertParam(IDictionary dict,string strTableName,ref string strSql)
		{
			if(dict==null||dict.Keys.Count==0) return null;
			if (strTableName==null || strTableName.Trim()=="") return null;

			int num=dict.Count;
			string[] Key=new string[num];
			string[] Value=new string[num];
			strSql=@"insert into  "+strTableName;
			string strKeys="";
			string strValues="";
			int i=0;
				
			//根据传入的HashTable的键, 拼Sql语句
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//要插入的字段
				strValues+="@"+Key[i].Trim()+",";//要插入的值
				i++;
			}
			if(strKeys.Length>1)
			{
				strKeys=strKeys.Substring(0,strKeys.Length-1);
			}
			if(strValues.Length>1)
			{
				strValues=strValues.Substring(0,strValues.Length-1);
			}
			if(strKeys.Length<=0 || strValues.Length<=0) return null;
			strSql+="("+strKeys+")"+" values ("+strValues+");";

			//将传入的HashTable转换为SqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}
			return param;
		}
	}
}
