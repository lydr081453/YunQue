using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ESP.Framework.DataAccess.Utilities;

namespace Portal.Data.PuJi
{
    /// <summary>
    /// 组类
    /// </summary>
    public class PhuketGroup
    {
        public int Id { get; set; }
        /// <summary>
        /// 组作品
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 票数
        /// </summary>
        public int VoteCount { get; set; }

        public string ImageUrl { get; set; }
        public string WorkUrl { get; set; }
    }

    /// <summary>
    /// 组数据类
    /// </summary>
    public class PhuketGroupManager
    {
        /// <summary>
        /// 根据组id修改支持计数
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static int Update(int groupId)
        {
            int result = 0;
            int voteCount = GetGroupById(groupId).VoteCount;
            try
            {
                string sql = "update t_phuketgroup set VoteCount=@VoteCount where Id=@Id";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "VoteCount", System.Data.DbType.Int32, voteCount + 1);
                db.AddInParameter(cmd, "Id", System.Data.DbType.Int32, groupId);
                result = db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 根据id查找组
        /// </summary>
        /// <param name="id">组标识id</param>
        /// <returns></returns>
        public static PhuketGroup GetGroupById(int id)
        {
            try
            {
                string sql = "select * from t_phuketgroup where Id=@Id";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "Id", System.Data.DbType.Int32, id);
                DataSet ds = db.ExecuteDataSet(cmd);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];

                PhuketGroup group = new PhuketGroup();
                group.Id = int.Parse(dr["Id"].ToString());
                group.ItemName = dr["ItemName"].ToString();
                group.GroupName = dr["GroupName"].ToString();
                group.VoteCount = string.IsNullOrEmpty(dr["VoteCount"].ToString()) ? 0 : int.Parse(dr["VoteCount"].ToString());
                group.ImageUrl = dr["ImageUrl"].ToString();
                group.WorkUrl = dr["WorkUrl"].ToString();
                return group;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return null;
        }

        /// <summary>
        /// 获取所有组集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetList()
        {
            List<PhuketGroup> list = new List<PhuketGroup>();
            string sql = "select * from t_phuketgroup";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            var ds = db.ExecuteDataSet(cmd);
            return ds.Tables[0];
        }

        /// <summary>
        /// 按获得的票数排名获取所以的组集合
        /// </summary>
        /// <returns></returns>
        public static List<PhuketGroup> GetWorksByCount()
        {
            List<PhuketGroup> list = new List<PhuketGroup>();
            string sql = "select * from T_phuketGroup order by VoteCount desc";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            DataRowCollection rows = dt.Rows;
            PhuketGroup group = null;
            foreach (DataRow row in rows)
            {
                group = new PhuketGroup();
                group.Id = int.Parse(row["Id"].ToString());
                group.ItemName = row["ItemName"].ToString();
                group.GroupName = row["GroupName"].ToString();
                group.VoteCount = string.IsNullOrEmpty(row["VoteCount"].ToString()) ? 0 : int.Parse(row["VoteCount"].ToString());
                group.ImageUrl = row["ImageUrl"].ToString();
                group.WorkUrl = row["WorkUrl"].ToString();
                list.Add(group);
            }
            return list;
        }
    }
}
