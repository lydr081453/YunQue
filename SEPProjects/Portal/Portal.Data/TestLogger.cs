using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Portal.Data
{
    public class TestLogger
    {
        /// <summary>
        /// 记录测试日志
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public static void Log(string subject, string content)
        {

            try
            {
                string sql = "insert into Test_Log values(@subject, @content, getdate())";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "subject", System.Data.DbType.String, subject);
                db.AddInParameter(cmd, "content", System.Data.DbType.String, content);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }

        public static void CostRecordLog(string subject, string content)
        {
            try
            {
                string sql = "insert into Test_Log values(@subject, @content, getdate())";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "subject", System.Data.DbType.String, subject);
                db.AddInParameter(cmd, "content", System.Data.DbType.String, content);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
