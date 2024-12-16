using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Data.PuJi
{
    /// <summary>
    /// 组成员类
    /// </summary>
    public class PhuketUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }

    /// <summary>
    /// 组成员数据类
    /// </summary>
    public class PhuketUserManager
    {
        /// <summary>
        /// 根据用户id获取用户所在的组id
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>组id</returns>
        public static int GetGroupId(int userId)
        {
            int result = 0;
            try
            {
                string sql = "select * from t_phuketuser where UserId=@UserId";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "UserId", System.Data.DbType.Int32, userId);
                DataSet ds = db.ExecuteDataSet(cmd);
                SqlDataReader reader = (SqlDataReader)db.ExecuteReader(cmd);
                while (reader.Read())
                {
                    result = int.Parse(reader["GroupId"].ToString());
                }
                reader.Close();
                reader = null;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
    }
}
