using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Media.Access;
using ESP.Media.Entity;

using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{
    public class ProjectmembersManager
    {
        /// <summary>
        /// 获得一个Media_projectmembers对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProjectmembersInfo GetModel(int id)
        {
            ProjectmembersInfo pm = null;
            pm = ESP.Media.DataAccess.ProjectmembersDataProvider.Load(id);
            if (pm == null)
                pm = new ESP.Media.Entity.ProjectmembersInfo();
            return pm;
        }

        /// <summary>
        /// 根据用户ID取得一个列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable GetListByUserid(int userid)
        {
            string term = " userid=@userid and del!=@del order by a.id desc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userid", SqlDbType.Int);
            param[0].Value = userid;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.ProjectmembersDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 获取项目中已有成员列表
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static DataTable GetHaveProjectUser(int projectid)
        {
            string term = " and projectid = @projectid and del!=@del order by a.id desc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@projectid",SqlDbType.Int);
            param[0].Value = projectid;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.ProjectmembersDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 获取项目中已有成员ID
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public static int[] GetHaveProjectUserID(int projectid)
        {
            DataTable dt = GetHaveProjectUser(projectid);
            if (dt != null && dt.Rows.Count > 0)
            {
                int[] uids = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    uids[i] = dt.Rows[i]["userid"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["userid"]);
                }
                return uids;
            }
            return null;
        }

        /// <summary>
        /// 获得一个Media_projectmembers对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProjectmembersInfo GetModelByprojectmember(int projectid,int uid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ProjectmembersInfo mem = GetModelByprojectmember(projectid, uid, trans);
                    trans.Commit();
                    conn.Close();
                    return mem;
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    return new ESP.Media.Entity.ProjectmembersInfo();
                }
            }
        }

        /// <summary>
        /// 获得一个Media_projectmembers对象
        /// </summary>
        /// <param name="projectid">The projectid.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        public static ProjectmembersInfo GetModelByprojectmember(int projectid, int uid,SqlTransaction trans)
        {
            string term = " projectid=@projectid and userid=@userid and del!=@del ";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@projectid", SqlDbType.Int);
            param[0].Value = projectid;
            param[1] = new SqlParameter("@userid", SqlDbType.Int);//(int)Global.PostType.Issue);
            param[1].Value = uid;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            DataTable dt = ESP.Media.DataAccess.ProjectmembersDataProvider.QueryInfo(trans, term, param);
            if (dt == null || dt.Rows.Count <= 0) return new ProjectmembersInfo();
            int id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            if (id <= 0) return new ESP.Media.Entity.ProjectmembersInfo();
            return GetModel(id);
        }
    }
}
