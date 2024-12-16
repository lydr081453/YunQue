using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace Portal.Data.PuJi
{
    /// <summary>
    /// 投票类
    /// </summary>
    public class PhuketVote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 投票组Id
        /// </summary>
        public int GroupId { get; set; }
        public DateTime VoteTime { get; set; }
        public string Ip { get; set; }
    }

    /// <summary>
    /// 投票数据类
    /// </summary>
    public class PhuketVoteManager
    {
        public static int Add(PhuketVote vote)
        {
            int result = 0;
            try
            {
                string sql = "insert into t_phuketvote values(@UserId, @GroupId, @VoteTime,@Ip)";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "UserId", System.Data.DbType.String, vote.UserId);
                db.AddInParameter(cmd, "GroupId", System.Data.DbType.String, vote.GroupId);
                db.AddInParameter(cmd, "VoteTime", System.Data.DbType.String, vote.VoteTime);
                db.AddInParameter(cmd, "Ip", System.Data.DbType.String, vote.Ip);
                result = db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 判断当前登录人是否参与过投票
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int CheckVote(int userId)
        {
            string sql = "select count(*) from t_phuketvote where UserId=@UserId";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserId", System.Data.DbType.String, userId);
            object obj = db.ExecuteScalar(cmd);
            return Convert.ToInt32(obj);
        }
    }
}
