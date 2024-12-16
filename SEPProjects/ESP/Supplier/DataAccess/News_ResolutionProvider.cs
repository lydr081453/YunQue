using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supply.Common;
using System.Data;
using System.Data.SqlClient;
using Supply.Entity;
using ESP.Supplier.Common;
using ESP.ConfigCommon;

namespace Supply.DataAccess
{
        /// <summary>
        /// 数据访问类Resolution。
        /// </summary>
        public class ResolutionProvider
        {
            public ResolutionProvider()
            { }
            #region  成员方法

            /// <summary>
            /// 增加一条数据
            /// </summary>
            public int Add(Resolution model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Resolution(");
                strSql.Append("Title,Content,createTime,createUserId,sysId,createIp,viewCount,parentId,status)");
                strSql.Append(" values (");
                strSql.Append("@Title,@Content,@createTime,@createUserId,@sysId,@createIp,@viewCount,@parentId,@status)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Content", SqlDbType.NVarChar,2000),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createUserId", SqlDbType.Int,4),
					new SqlParameter("@sysId", SqlDbType.Int,4),
					new SqlParameter("@createIp", SqlDbType.NVarChar,30),
					new SqlParameter("@viewCount", SqlDbType.Int,4),
					new SqlParameter("@parentId", SqlDbType.Int,4),
                    new SqlParameter("@status",SqlDbType.Int,4)                        
                                            };
                parameters[0].Value = model.Title;
                parameters[1].Value = model.Content;
                parameters[2].Value = model.createTime;
                parameters[3].Value = model.createUserId;
                parameters[4].Value = model.sysId;
                parameters[5].Value = model.createIp;
                parameters[6].Value = model.viewCount;
                parameters[7].Value = model.parentId;
                parameters[8].Value = model.status;

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
            public void Update(Resolution model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Resolution set ");
                strSql.Append("Title=@Title,");
                strSql.Append("Content=@Content,");
                strSql.Append("createTime=@createTime,");
                strSql.Append("createUserId=@createUserId,");
                strSql.Append("sysId=@sysId,");
                strSql.Append("createIp=@createIp,");
                strSql.Append("viewCount=@viewCount,");
                strSql.Append("parentId=@parentId,status=@status");
                strSql.Append(" where Id=@Id ");
                SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Content", SqlDbType.NVarChar,2000),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createUserId", SqlDbType.Int,4),
					new SqlParameter("@sysId", SqlDbType.Int,4),
					new SqlParameter("@createIp", SqlDbType.NVarChar,30),
					new SqlParameter("@viewCount", SqlDbType.Int,4),
					new SqlParameter("@parentId", SqlDbType.Int,4),
                                            new SqlParameter("@status",SqlDbType.Int,4) };
                parameters[0].Value = model.Id;
                parameters[1].Value = model.Title;
                parameters[2].Value = model.Content;
                parameters[3].Value = model.createTime;
                parameters[4].Value = model.createUserId;
                parameters[5].Value = model.sysId;
                parameters[6].Value = model.createIp;
                parameters[7].Value = model.viewCount;
                parameters[8].Value = model.parentId;
                parameters[9].Value = model.status;

                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public void Delete(int Id)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from Resolution ");
                strSql.Append(" where Id=@Id ");
                SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = Id;

                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public Resolution GetModel(int Id)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select  top 1 a.*,
                                (case when (createUserId is not null and createUserId <> 0 ) then b.realfirstname + b.reallastname
                                else a.createip end) as username
                                 from Resolution  as a 
                                left join siteusers as b on a.createuserid=b.id
                ");
                strSql.Append(" where a.Id=@Id ");
                SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
                parameters[0].Value = Id;

                return CBO.FillObject<Resolution>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }

            /// <summary>
            /// 获得数据列表
            /// </summary>
            public List<Resolution> GetList(int Top,string strWhere, List<SqlParameter> parms)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select " + (Top==0? "" : "top " + Top) + @" a.*,(
                    case when (createUserId is not null and createUserId <> 0 ) then b.realfirstname + b.reallastname
                         when (sysid is not null and sysid <> 0 ) then c.username
                    else a.createip end) as username
                    FROM Resolution as a
                    left join siteusers as b on a.createuserid=b.id
                    left join users as c on a.sysid=c.id");
                strSql.Append(" where 1=1 ");
                strSql.Append(strWhere);
                strSql.Append(" order by a.id desc");
                return CBO.FillCollection<Resolution>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
            }

            public int GetListCount(string strWhere, List<SqlParameter> parms)
            {
                strWhere += " and (parentId is null or parentId=0)";
                string strSql = "";
                strSql = " select count(id) Nums ";
                strSql += " FROM Resolution as a where 1=1";
                strSql += strWhere;
                return int.Parse(DbHelperSQL.Query(strSql, parms.ToArray()).Tables[0].Rows[0]["Nums"].ToString());
            }

            /// <summary>
            /// 回复条数
            /// </summary>
            /// <param name="strWhere"></param>
            /// <param name="parms"></param>
            /// <returns></returns>
            public int GetChildCount(int parentId)
            {
                string strWhere = " and parentId="+parentId + " and status="+(int)Supply.Common.Status.resoStatus.audited;
                string strSql = "";
                strSql = " select count(id) Nums ";
                strSql += " FROM Resolution where 1=1";
                strSql += strWhere;
                return int.Parse(DbHelperSQL.Query(strSql).Tables[0].Rows[0]["Nums"].ToString());
            }

            public List<Resolution> GetList(int pageSize, int pageIndex, string strWhere, string orderBy, List<SqlParameter> parms)
            {
                if (orderBy == "")
                    orderBy = " a.id desc";
                strWhere += " and (parentId is null or parentId=0)";
                DataTable dt = new DataTable();
                string sql = @"SELECT TOP (@PageSize) * FROM(
                            select a.*,(
                                case when (createUserId is not null and createUserId <> 0 ) then b.realfirstname + b.reallastname
                                     when (sysid is not null and sysid <> 0 ) then c.username
                                else a.createip end) as username, ROW_NUMBER() OVER (ORDER BY @@@ORDERBY@@@) AS [__i_RowNumber] FROM Resolution as a
                                left join siteusers as b on a.createuserid=b.id
                                left join users as c on a.sysid=c.id
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

                return CBO.FillCollection<Resolution>(DbHelperSQL.Query(sql, parms.ToArray()));
            }

            #endregion
        }

}
