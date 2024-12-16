using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// ���ݿ���²���
	/// </summary>
	public class clsUpdate
	{
		public clsUpdate()
		{

		}

		/// <summary>
		/// ִ�и��²���
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ���µ��ֶμ�Ҫ���³ɵ�ֵ</param>
		/// <param name="strTableName">Ҫ���µı���</param>
		/// <param name="strTerms">��������</param>
		/// <returns>ִ�и��²���Ӱ�������</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//Ҫ���µ��ֶ�
				strSql+="=";	
				strSql+="@"+Key[i];//Ҫ���³ɵ�ֵ
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//ȥ���ַ����е���ĩλ��", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";
			

			//�������HashTableת��ΪSqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}
			

			//ִ�и��²���
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				rows=ESP.Media.Access.Utilities.SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql,param);
				conn.Close();
			}
			return rows;		
		}

		
		/// <summary>
		/// ���²���
		/// </summary>
		/// <param name="strSql">sql���</param>
		/// <returns>���²���Ӱ�������</returns>
		public static int funUpdate(string strSql)
		{
			return funUpdate(strSql,false);
		}
		
		/// <summary>
		/// ���²���
		/// </summary>
		/// <param name="strSql">sql���</param>
		/// <param name="isTrans">�Ƿ�������</param>
		/// <returns>���²���Ӱ�������</returns>
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
		/// ��ȡ����Sql���
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ���µ��ֶμ�Ҫ���³ɵ�ֵ</param>
		/// <param name="strTableName">Ҫ���µı���</param>
		/// <param name="strTerms">��������</param>
		/// <returns>����Sql���</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//Ҫ���µ��ֶ�
				strSql+="=";	
				strSql+="@"+Key[i];//Ҫ���³ɵ�ֵ
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//ȥ���ַ����е���ĩλ��", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";

			//�������HashTableת��ΪSqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}	
			return param;		
		}

		
		/// <summary>
		/// ��ȡ����Sql���
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ���µ��ֶμ�Ҫ���³ɵ�ֵ</param>
		/// <param name="strTableName">Ҫ���µı���</param>
		/// <param name="strTerms">��������</param>
		/// <returns>����Sql���</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strSql+=Key[i];//Ҫ���µ��ֶ�
				strSql+="=";	
				strSql+="@"+Key[i];//Ҫ���³ɵ�ֵ
				strSql+=",";
				i++;
			}
			strSql=strSql.Substring(0,strSql.Length-1);//ȥ���ַ����е���ĩλ��", "
			if(strTerms!=null && strTerms.Trim()!="")
			{
				strSql+=" where 1 = 1  and " + strTerms;
			}
			strSql+=";";

			return strSql;		
		}

	}
}
