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
    public class PostsManager
    {
        public PostsManager()
        {

        }


        /// <summary>
        /// 获取一个帖子对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PostsInfo GetModel(int id)
        {
            PostsInfo post = null;
            if (id > 0)
            {
                post = ESP.Media.DataAccess.PostsDataProvider.Load(id);
            }
            if (post == null)
                post = new ESP.Media.Entity.PostsInfo();
            return post;
        }

        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="post"></param>
        /// <param name="currentuser"></param>
        /// <returns></returns>
        public static int Issue(PostsInfo post,int userid)
        {
            string now = System.DateTime.Now.ToShortDateString();
            post.Parentid = 0;
            post.No = 0;
            post.Issuedate = now;
            post.Type = (int)Global.PostType.Issue;
            post.Userid = userid;
            post.Lastreplyuserid = userid;
            post.Lastreplydate = now;
            post.Del = (int)Global.FiledStatus.Usable;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = ESP.Media.DataAccess.PostsDataProvider.insertinfo(post,trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.IssueMsg, userid,trans);//发帖日志
                    post.Id = id;
                    post.Parentid = id;
                    ESP.Media.DataAccess.PostsDataProvider.updateInfo(trans, null, post, null);
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static int Reply(PostsInfo post,int userid)
        {
            if (userid <= 0 )
            { return 0; }
            PostsInfo parent = GetModel(post.Parentid);
            string now = System.DateTime.Now.ToShortDateString();

            post.Parentid = parent.Id;
            post.Issuedate = now;
            post.Type = (int)Global.PostType.Reply;
            post.No = parent.No + 1;
            post.Userid = userid;
            post.Lastreplyuserid = userid;
            post.Lastreplydate = now;
            parent.Lastreplyuserid = userid;
            parent.Lastreplydate = now;
            post.Del = (int)Global.FiledStatus.Usable;
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = ESP.Media.DataAccess.PostsDataProvider.insertinfo(post,trans);
                    OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.ReplyMsg, userid,trans);//回复日志
                    ESP.Media.DataAccess.PostsDataProvider.updateInfo(trans, null, parent, null);
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 编辑一个帖子
        /// </summary>
        /// <param name="post">要编辑的帖子</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool modify(PostsInfo post,int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(post, trans);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 编辑一个帖子(带事务的)
        /// </summary>
        /// <param name="post">要编辑的帖子</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public static bool modify(PostsInfo post, SqlTransaction trans)
        {
            bool result = false;
            result = ESP.Media.DataAccess.PostsDataProvider.updateInfo(trans, null, post, null, null);
            return result;
        }


        /// <summary>
        /// 删帖
        /// </summary>
        /// <param name="post">要删除的帖子</param>
        /// <param name="userid">操作人</param>
        /// <returns></returns>
        public static bool del(PostsInfo post, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    post.Del = (int)Global.FiledStatus.Del;
                    bool result = modify(post, trans);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 删帖(带事务的)
        /// </summary>
        /// <param name="post">要删除的帖子</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public static bool del(PostsInfo post, SqlTransaction trans)
        {
            bool result = false;
            result = ESP.Media.DataAccess.PostsDataProvider.updateInfo(trans, null, post, null, null);
            return result;
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public static DataTable GetSubjectPostByPostId(int PostId)
        {
            string term = " and ( a.id = @postid ) and a.type = @type and a.del != @del order by a.id desc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@postid", SqlDbType.Int);
            param[0].Value = PostId;
            param[1] = new SqlParameter("@type", SqlDbType.Int);
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 获取相应回复的列表
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public static DataTable GetReplyPostByPostId(int PostId)
        {
            string term = " and ( a.parentid = @postid ) and a.type = @type and a.del != @del order by a.id desc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@postid", SqlDbType.Int);
            param[0].Value = PostId;
            param[1] = new SqlParameter("@type", SqlDbType.Int);
            param[1].Value = (int)Global.PostType.Reply;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 根据帖子查找所有回复
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public static DataTable GetPostListByPostId(int PostId)
        {
            string term = " and parentid = @postid  and a.del != @del order by a.id desc";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@postid", SqlDbType.Int);
            param[0].Value = PostId;
            param[1] = new SqlParameter("@del", SqlDbType.Int);
            param[1].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 获取发布的公告列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysMsg()
        {
            string term = " and ( a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del order by a.id desc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isSysMsg", SqlDbType.Int);
            param[0].Value = (int)Global.IsSystem.SysMsg;
            param[1] = new SqlParameter("@type", typeof(int));//(int)Global.PostType.Issue);
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }

        /// <summary>
        /// 获取发布的博客列表
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public static DataTable GetBlogMsg()
        {
            string term = " and ( a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del order by issuedate desc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isSysMsg",typeof(int));
            param[0].Value =  (int)Global.IsSystem.Blog;
            param[1] = new SqlParameter("@type", typeof(int));
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del",SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }


        /// <summary>
        /// 获取发布的帖子列表
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public static DataTable GetPostMsg()
        {
            string term = " and ( a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isSysMsg", SqlDbType.Int);
            param[0].Value = (int)Global.IsSystem.Post;
            param[1] = new SqlParameter("@type", typeof(int));//(int)Global.PostType.Issue);
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }


        public static DataTable GetPostMsg(string key)
        {
            string term = " and ( a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@isSysMsg", SqlDbType.Int);
            param[0].Value = (int)Global.IsSystem.Post;
            param[1] = new SqlParameter("@type", typeof(int));//(int)Global.PostType.Issue);
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;

            if (!string.IsNullOrEmpty(key))
            {
                term += " and (a.subject like  '%'+@key+'%' or a.body like  '%'+@key+'%')";
                param[3] = new SqlParameter("@key", SqlDbType.NVarChar);
                param[3].Value = key;

            }
            return ESP.Media.DataAccess.PostsDataProvider.QueryInfo(term, param);
        }

        public static DataTable GetPostMsgTop10(int count)
        {
            string sql = @"select top {0} a.id as id,a.parentid as parentid,a.no as no,a.type as type,a.issysmsg as issysmsg,a.issuedate as issuedate,
                        a.userid as userid,a.subject as subject,a.body as body,a.lastreplyuserid as lastreplyuserid,
                        a.lastreplydate as lastreplydate,a.del as del from media_Posts as a where {1}";
            string term = @"(a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del order by a.id desc";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isSysMsg", typeof(int));
            param[0].Value = (int)Global.IsSystem.SysMsg;
            param[1] = new SqlParameter("@type", typeof(int));
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;
            sql = string.Format(sql, count, term);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }
        /// <summary>
        /// 获取公告的TopN
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DataTable GetCommonMsgTopN(int count)
        {
            string sql = @"SELECT top {0} a.id as id,
	                        a.subject as subject,
                            a.issuedate as issuedate,
                            a.lastreplydate as lastreplydate,
                            a.userid as userid,
                            a.body as body
                            FROM media_Posts as a where {1}";
            //string jointable = @"inner join framework.dbo.F_Employee as employee on employee.SysUserID = a.UserID";
            string term = @"(a.isSysMsg = @isSysMsg ) and a.type = @type and a.del != @del order by a.id desc";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isSysMsg", typeof(int));
            param[0].Value = (int)Global.IsSystem.SysMsg;
            param[1] = new SqlParameter("@type", typeof(int));
            param[1].Value = (int)Global.PostType.Issue;
            param[2] = new SqlParameter("@del", SqlDbType.Int);
            param[2].Value = (int)Global.FiledStatus.Del;

            sql = string.Format(sql, count, term);
            return ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql, param);
        }

    }
}
