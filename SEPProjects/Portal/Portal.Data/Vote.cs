using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Data
{
    public class VoteManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Add(Vote vote)
        {
            try
            {
                string sql = "insert into T_Vote values(@Question, @Answer, @UserId, getdate(),@IP)";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "Question", System.Data.DbType.String, vote.Question);
                db.AddInParameter(cmd, "Answer", System.Data.DbType.String, vote.Answer);
                db.AddInParameter(cmd, "UserId", System.Data.DbType.String, vote.UserId);
                db.AddInParameter(cmd, "IP", System.Data.DbType.String, vote.IP);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static void Add(List<Vote> votes)
        {
            
            using (SqlConnection conn = new SqlConnection(ESP.Configuration.ConfigurationManager.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand comm = null;
                    foreach (var vote in votes)
                    {
                        comm = conn.CreateCommand();
                        comm.Transaction = trans;
                        comm.CommandText = "insert into T_Vote values(@Question, @Answer, @UserId, getdate(),@IP)";
                        comm.Parameters.Add(new SqlParameter("Question", vote.Question));
                        comm.Parameters.Add(new SqlParameter("Answer", vote.Answer));
                        comm.Parameters.Add(new SqlParameter("UserId",  vote.UserId));
                        comm.Parameters.Add(new SqlParameter("IP", vote.IP));
                        comm.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }

        public static DataTable GetSupplierNames(int userId)
        {
            string sql = @"select distinct c.supplier_name,c.id from T_GeneralInfo a left join T_OrderInfo b on a.id=b.general_id
                    left join T_Supplier c on b.supplierId=c.id
                    where requestor=@UserId and c.Supplier_type=1 and a.source='协议供应商'";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "UserId", System.Data.DbType.Int32, userId);
            var ds = db.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }

        public static int GetVoteCount(int userid)
        {
            string sql = @"select count(*) from t_vote where userid=@userid";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "userid", System.Data.DbType.Int32, userid);
            int count = Convert.ToInt32(db.ExecuteScalar(cmd));
            return count;
        }
    }

    public class Vote
    {
        public Vote()
        { }
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string IP { get; set; }
    }
}
