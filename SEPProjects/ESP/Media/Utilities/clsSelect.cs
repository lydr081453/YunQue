using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// clsSelect ��ժҪ˵����
	/// </summary>
	public class clsSelect
	{
		public clsSelect()
		{

		}
		
		
		#region QueryBySql		
		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strSql">sql���</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable QueryBySql(string strSql)
		{
			
			if(strSql==null || strSql.Trim()=="") return null;
			DataTable dt=null;
			

			//ִ�в�ѯ����
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;	
		}
		
		/// <summary>
		/// �����ڲ�ѯ
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="ts"></param>
		/// <returns></returns>
		public static DataTable QueryBySql(string strSql, SqlTransaction ts)
		{
			if(strSql==null || strSql.Trim()=="") return null;
			//ִ�в�ѯ����
			return ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(ts,CommandType.Text,strSql).Tables[0];
		}
		

		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strSql">sql���</param>
		/// <param name="ErrorMessage">���صĴ�����Ϣ</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable QueryBySql(string strSql,ref string ErrorMessage)
		{
			
			if(strSql==null || strSql.Trim()=="") 
			{
				ErrorMessage="sql�ַ���Ϊ��";
				return null;
			}
			DataTable dt=null;
			

			//ִ�в�ѯ����
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;	
		}

		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strSql">sql�ַ���</param>
		/// <param name="param">�����б�</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable QueryBySql(string strSql,params SqlParameter[] param)
		{
			
			if(strSql==null || strSql.Trim()=="") return null;
			DataTable dt=null;

			//ִ�в�ѯ����
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
        /// ִ�в�ѯ����
        /// </summary>
        /// <param name="strSql">sql�ַ���</param>
        /// <param name="param">�����б�</param>
        /// <returns>���صĽ����,���û�в鵽,����null</returns>
        public static DataTable QueryBySql(SqlTransaction trans,string strSql, params SqlParameter[] param)
        {

            if (strSql == null || strSql.Trim() == "") return null;
            if (param == null || param.Length <= 0) return null;
            DataTable dt = null;

            //ִ�в�ѯ����
            dt = ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(trans, CommandType.Text, strSql, param).Tables[0];
            return dt;
        }


		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strSql">sql�ַ���</param>
		/// <param name="ErrorMessage">������Ϣ</param>
		/// <param name="param">�����б�</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable QueryBySql(string strSql,ref string ErrorMessage,params SqlParameter[] param)
		{
			
			if(strSql==null || strSql.Trim()=="") 
			{
				ErrorMessage="sql�ַ���Ϊ��";
				return null;
			}
			if(param==null || param.Length<=0)
			{
				ErrorMessage="SqlParameter[]����Ϊ��";
				return null;
			}
			DataTable dt=null;

			//ִ�в�ѯ����
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
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable funSelect(string strTableName)
		{
			return funSelect(strTableName,"*","");
		}


		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strTerms">Ҫ��ѯ����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable funSelect(string strTableName,IDictionary Terms)
		{
			return funSelect(strTableName,"*",Terms);
		}

		
		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable funSelect(string strTableName,string[] strColumns)
		{
			return funSelect(strTableName,strColumns,"");
		}

		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
		public static DataTable funSelect(string strTableName,string strColumns)
		{
			return funSelect(strTableName,strColumns,"");
		}

		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <param name="Terms">��ѯ����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
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
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <param name="Terms">��ѯ����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
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
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <param name="strTerms">��ѯ����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
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

			//ִ�в�ѯ����
			using(SqlConnection conn=new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
			{
				conn.Open();
				dt=ESP.Media.Access.Utilities.SqlHelper.ExecuteDataset(conn,CommandType.Text,strSql).Tables[0];
				conn.Close();
			}
			return dt;		
		}

		/// <summary>
		/// ִ�в�ѯ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumns">Ҫ��ѯ���ֶ���</param>
		/// <param name="Terms">��ѯ����</param>
		/// <returns>���صĽ����,���û�в鵽,����null</returns>
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
				strTerms=strTerms.Substring(0,strTerms.Length-2);//ȥ���ַ����е���ĩλ��", "

				if(strTerms.Trim()!="")
				{
					strSql+=" where " +strTerms;
				}
				//�������HashTableת��ΪSqlParameter
				param =  new SqlParameter[num];
				for(int j=0;j<num;j++)
				{
					param[j]=new SqlParameter("@"+Key[j],Value[j]);
				}
			}

			DataTable dt=null;

			//ִ�в�ѯ����
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
