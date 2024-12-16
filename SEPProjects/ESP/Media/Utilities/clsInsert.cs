using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// clsInsert ��ժҪ˵����
	/// </summary>
	public class clsInsert
	{
		public clsInsert()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ִ�в������
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ������ֶμ�Ҫ�����ֵ</param>
		/// <param name="strTableName">Ҫ����ı���</param>
		/// <returns>ִ�в������Ӱ�������</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//Ҫ������ֶ�
				strValues+="@"+Key[i].Trim()+",";//Ҫ�����ֵ
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
		/// ��ȡ������ַ���
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ������ֶμ�Ҫ�����ֵ</param>
		/// <param name="strTableName">Ҫ����ı���</param>
		/// <returns>���������Sql�ַ���</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//Ҫ������ֶ�
				strValues+="@"+Key[i].Trim()+",";//Ҫ�����ֵ
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
		/// ��ȡ������ַ���������
		/// </summary>
		/// <param name="dict">ʵ��IDictionary�ӿڵ�,����HashTable,��������Ҫ������ֶμ�Ҫ�����ֵ</param>
		/// <param name="strTableName">Ҫ����ı���</param>
		/// <param name="strSql">���������Sql�ַ���</param>
		/// <returns>�����б�</returns>
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
				
			//���ݴ����HashTable�ļ�, ƴSql���
			foreach(DictionaryEntry entry in dict)
			{
				Key[i]=entry.Key.ToString();
				Value[i]=entry.Value.ToString();
				strKeys+=Key[i].Trim()+",";//Ҫ������ֶ�
				strValues+="@"+Key[i].Trim()+",";//Ҫ�����ֵ
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

			//�������HashTableת��ΪSqlParameter
			SqlParameter[] param =  new SqlParameter[num];
			for(int j=0;j<num;j++)
			{
				param[j]=new SqlParameter("@"+Key[j],Value[j]);
			}
			return param;
		}
	}
}
