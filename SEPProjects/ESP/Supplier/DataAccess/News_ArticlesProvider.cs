using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Supply.Common;
using Supply.Entity;
using System.Data;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
    public class ArticlesProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Articles model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Articles(");
            strSql.Append(@"[Title],[Summery],[Body],[CreatedDate],[ModifiedDate],[Author],[CreatedUserId],[CreatedUserName],[CreatedUserType],[RightDate],[IsCanComments],[ArticleTypeID]
                ,[IsHot],[IsRecommend],[IsOnMainPage],[ViewCount],[Topics],[Status],[PicPath],[VideoPath],[VideoOldPath],[IsUserPublish],[FLASHFilePath],[AttPath])");
            strSql.Append(" values (");
            strSql.Append(@"@Title,@Summery,@Body,@CreatedDate,@ModifiedDate,@Author,@CreatedUserId,@CreatedUserName,@CreatedUserType,@RightDate,@IsCanComments,@ArticleTypeID,@IsHot,@IsRecommend
                ,@IsOnMainPage,@ViewCount,@Topics,@Status,@PicPath,@VideoPath,@VideoOldPath,@IsUserPublish,@FLASHFilePath,@AttPath)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@Summery", SqlDbType.NVarChar,500),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@Author", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedUserType", SqlDbType.Int,4),
					new SqlParameter("@RightDate", SqlDbType.DateTime),
					new SqlParameter("@IsCanComments", SqlDbType.Bit,1),
					new SqlParameter("@ArticleTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsHot", SqlDbType.Bit,1),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@IsOnMainPage", SqlDbType.Bit,1),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@Topics", SqlDbType.NVarChar,100),
                    new SqlParameter("@Status", SqlDbType.NVarChar,200),
                    new SqlParameter("@PicPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@VideoPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@VideoOldPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@IsUserPublish", SqlDbType.NVarChar,200),
                    new SqlParameter("@FLASHFilePath", SqlDbType.NVarChar,200),
                    new SqlParameter("@AttPath", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Summery;
            parameters[2].Value = model.Body;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = model.Author;
            parameters[6].Value = model.CreatedUserId;
            parameters[7].Value = model.CreatedUserName;
            parameters[8].Value = model.CreatedUserType;
            parameters[9].Value = model.RightDate;
            parameters[10].Value = model.IsCanComments;
            parameters[11].Value = model.ArticleTypeID;
            parameters[12].Value = model.IsHot;
            parameters[13].Value = model.IsRecommend;
            parameters[14].Value = model.IsOnMainPage;

            parameters[15].Value = model.ViewCount;
            parameters[16].Value = model.Topics;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.PicPath;
            parameters[19].Value = model.VideoPath;
            parameters[20].Value = model.VideoOldPath;
            parameters[21].Value = model.IsUserPublish;
            parameters[22].Value = model.FLASHFilePath;
            parameters[23].Value = model.AttPath;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Articles model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Articles set ");

            strSql.Append(@"Title = @Title,
      ,Summery = @Summery,
      ,Body = @Body,
      ,CreatedDate = @CreatedDate,
      ,ModifiedDate = @ModifiedDate,
      ,Author = @Author,
      ,CreatedUserId = @CreatedUserId,
      ,CreatedUserName = @CreatedUserName,
      ,CreatedUserType = @CreatedUserType,
      ,RightDate = @RightDate,
      ,IsCanComments = @IsCanComments,
      ,ArticleTypeID = @ArticleTypeID,
      ,IsHot = @IsHot,
      ,IsRecommend = @IsRecommend,
      ,IsOnMainPage = @IsOnMainPage,
      ,ViewCount = @ViewCount,
      ,Topics = @Topics,
      ,Status = @Status,
      ,PicPath = @PicPath,
      ,VideoPath = @VideoPath,
      ,VideoOldPath = @VideoOldPath,
      ,IsUserPublish = @IsUserPublish,
      ,FLASHFilePath = @FLASHFilePath,
      ,AttPath = @AttPath");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,500),
					new SqlParameter("@Summery", SqlDbType.NVarChar,500),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@Author", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedUserType", SqlDbType.Int,4),
					new SqlParameter("@RightDate", SqlDbType.DateTime),
					new SqlParameter("@IsCanComments", SqlDbType.Bit,1),
					new SqlParameter("@ArticleTypeID", SqlDbType.Int,4),
					new SqlParameter("@IsHot", SqlDbType.Bit,1),
					new SqlParameter("@IsRecommend", SqlDbType.Bit,1),
					new SqlParameter("@IsOnMainPage", SqlDbType.Bit,1),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@Topics", SqlDbType.NVarChar,100),
                    new SqlParameter("@Status", SqlDbType.NVarChar,200),
                    new SqlParameter("@PicPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@VideoPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@VideoOldPath", SqlDbType.NVarChar,200),
                    new SqlParameter("@IsUserPublish", SqlDbType.NVarChar,200),
                    new SqlParameter("@FLASHFilePath", SqlDbType.NVarChar,200),
                    new SqlParameter("@AttPath", SqlDbType.NVarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Summery;
            parameters[2].Value = model.Body;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = model.Author;
            parameters[6].Value = model.CreatedUserId;
            parameters[7].Value = model.CreatedUserName;
            parameters[8].Value = model.CreatedUserType;
            parameters[9].Value = model.RightDate;
            parameters[10].Value = model.IsCanComments;
            parameters[11].Value = model.ArticleTypeID;
            parameters[12].Value = model.IsHot;
            parameters[13].Value = model.IsRecommend;
            parameters[14].Value = model.IsOnMainPage;

            parameters[15].Value = model.ViewCount;
            parameters[16].Value = model.Topics;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.PicPath;
            parameters[19].Value = model.VideoPath;
            parameters[20].Value = model.VideoOldPath;
            parameters[21].Value = model.IsUserPublish;
            parameters[22].Value = model.FLASHFilePath;
            parameters[23].Value = model.AttPath;
            parameters[24].Value = model.ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void UpdateViewCount(int id)
        {
            string strSql = "update Articles set viewCount=viewCount+1 where id="+id;
            DbHelperSQL.ExecuteSql(strSql);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Articles ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Articles GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Articles ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            Articles model = CBO.FillObject<Articles>(DbHelperSQL.Query(strSql.ToString(), parameters));
            if (model != null && System.Configuration.ConfigurationManager.AppSettings["supplyManagerPath"] != null)
                model.Body = model.Body.Replace("src=\"/", "src=\"" + System.Configuration.ConfigurationManager.AppSettings["supplyManagerPath"].ToString());
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Articles> GetList(int Top,string strWhere,List<SqlParameter> parms,string orderBy)
        {
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            string strSql = "";
            strSql = " select {0} * ";
            strSql += " FROM Articles where 1=1";
            strSql += strWhere + " {1} "; 
            strSql = string.Format(strSql,((Top > 0) ? " top " + Top.ToString() : ""),(orderBy == "" ? " order by id desc" : " order by " + orderBy));
            return CBO.FillCollection<Articles>(DbHelperSQL.Query(strSql,parms.ToArray()));
        }

        public int GetListCount(string strWhere, List<SqlParameter> parms)
        {
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            string strSql = "";
            strSql = " select count(id) Nums ";
            strSql += " FROM Articles where 1=1";
            strSql += strWhere;
            return int.Parse(DbHelperSQL.Query(strSql, parms.ToArray()).Tables[0].Rows[0]["Nums"].ToString());
        }

        public List<Articles> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
        {
            if (orderBy == "")
                orderBy = " id desc";
            strWhere += " and status != " + (int)Supply.Common.Status.NewsStatus.Deleted;
            DataTable dt = new DataTable();
            string sql = @"SELECT TOP (@PageSize) * FROM(
                            select *, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] from Articles
                            WHERE 1=1 @@@WHERE@@@
                            ) t
                            WHERE t.[__i_RowNumber] > @PageStart
                            ";
            sql = sql.Replace("@@@ORDERBY@@@", orderBy);
            sql = sql.Replace("@@@WHERE@@@", strWhere);
            if (parms == null)
                parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@PageSize", pageSize));
            parms.Add(new SqlParameter("@PageStart", pageSize * pageIndex));

            return CBO.FillCollection<Articles>(DbHelperSQL.Query(sql, parms.ToArray()));
        }
    }
}
