using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// WebConfig�Ĳ���
	/// </summary>
	public class clsConfigOperate
	{
		/// <summary>
		/// ��ȡ���ݿ������ַ���
		/// </summary>
		/// <returns>���ݿ������ַ���</returns>
		public static string CustomerSqlConnection()
		{
            return ESP.Configuration.ConfigurationManager.SafeConnectionStrings["MediaSqlConnection"].ConnectionString;
		}

		public string SqlConnection
		{
			get
			{
                return ESP.Configuration.ConfigurationManager.SafeConnectionStrings["MediaSqlConnection"].ConnectionString;
			}
		}
	}
}
