using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// WebConfig的操作
	/// </summary>
	public class clsConfigOperate
	{
		/// <summary>
		/// 获取数据库连接字符串
		/// </summary>
		/// <returns>数据库连接字符串</returns>
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
