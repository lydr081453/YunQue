using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace ESP.Supplier.DataAccess
{
    public class SC_SupplierScoreDataProvider
    {
        public SC_SupplierScoreDataProvider()
        { }

        #region  成员方法

        public static int getMaxPirmaryKey()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(PirmaryKey) from SC_SupplierScore ");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            };
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierScore model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierScore(");
            strSql.Append("SupplierId,Score,ScoreType,ScoreDes,CreatTime,CreatIP,LastUpdateTime,LastUpdateIP,Type,Status,SourceId,PirmaryKey)");
            strSql.Append(" values (");
            strSql.Append("@SupplierId,@Score,@ScoreType,@ScoreDes,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP,@Type,@Status,@SourceId,@PirmaryKey)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Score", SqlDbType.Decimal,9),
					new SqlParameter("@ScoreType", SqlDbType.Int,4),
					new SqlParameter("@ScoreDes", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@SourceId",SqlDbType.Int,4),
                    new SqlParameter("@PirmaryKey",SqlDbType.Int,4)};
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.Score;
            parameters[2].Value = model.ScoreType;
            parameters[3].Value = model.ScoreDes;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.CreatIP;
            parameters[6].Value = model.LastUpdateTime;
            parameters[7].Value = model.LastUpdateIP;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.SourceId;
            parameters[11].Value = model.PirmaryKey;

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
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_SupplierScore model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierScore(");
            strSql.Append("SupplierId,Score,ScoreType,ScoreDes,CreatTime,CreatIP,LastUpdateTime,LastUpdateIP,Type,Status,SourceId,PirmaryKey)");
            strSql.Append(" values (");
            strSql.Append("@SupplierId,@Score,@ScoreType,@ScoreDes,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP,@Type,@Status,@SourceId,@PirmaryKey)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Score", SqlDbType.Decimal,9),
					new SqlParameter("@ScoreType", SqlDbType.Int,4),
					new SqlParameter("@ScoreDes", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@SourceId",SqlDbType.Int,4),
                    new SqlParameter("@PirmaryKey",SqlDbType.Int,4)};
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.Score;
            parameters[2].Value = model.ScoreType;
            parameters[3].Value = model.ScoreDes;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.CreatIP;
            parameters[6].Value = model.LastUpdateTime;
            parameters[7].Value = model.LastUpdateIP;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.SourceId;
            parameters[11].Value = model.PirmaryKey;

             object obj = DbHelperSQL.GetSingle(strSql.ToString(),conn,trans, parameters);
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
        public void Update(SC_SupplierScore model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierScore set ");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("Score=@Score,");
            strSql.Append("ScoreType=@ScoreType,");
            strSql.Append("ScoreDes=@ScoreDes,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("CreatIP=@CreatIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastUpdateIP=@LastUpdateIP,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("SourceId=@SourceId,");
            strSql.Append("PirmaryKey=@PirmaryKey,");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@Score", SqlDbType.Decimal,9),
					new SqlParameter("@ScoreType", SqlDbType.Int,4),
					new SqlParameter("@ScoreDes", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@SourceId",SqlDbType.Int,4),
                    new SqlParameter("@PirmaryKey",SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.Score;
            parameters[3].Value = model.ScoreType;
            parameters[4].Value = model.ScoreDes;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.CreatIP;
            parameters[7].Value = model.LastUpdateTime;
            parameters[8].Value = model.LastUpdateIP;
            parameters[9].Value = model.Type;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.SourceId;
            parameters[12].Value = model.PirmaryKey;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierScore ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据供应商ID和得分类型删除一组数据
        /// </summary>
        public void DeleteBySupplierId(int supplierid,int scoretype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierScore ");
            strSql.Append(" where supplierid=@supplierid and scoretype=@scoretype");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
                    new SqlParameter("@scoretype",SqlDbType.Int,4)
				};
            parameters[0].Value = supplierid;
            parameters[1].Value = scoretype;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据供应商ID和得分类型删除一组数据
        /// </summary>
        public void DeleteBySupplierId(int supplierid, int scoretype, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierScore ");
            strSql.Append(" where supplierid=@supplierid and scoretype=@scoretype");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
                    new SqlParameter("@scoretype",SqlDbType.Int,4)
				};
            parameters[0].Value = supplierid;
            parameters[1].Value = scoretype;
            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierScore GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierScore ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            SC_SupplierScore model = new SC_SupplierScore();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.Id = Id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Score"].ToString() != "")
                {
                    model.Score = decimal.Parse(ds.Tables[0].Rows[0]["Score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ScoreType"].ToString() != "")
                {
                    model.ScoreType = int.Parse(ds.Tables[0].Rows[0]["ScoreType"].ToString());
                }
                model.ScoreDes = ds.Tables[0].Rows[0]["ScoreDes"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.CreatIP = ds.Tables[0].Rows[0]["CreatIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                model.LastUpdateIP = ds.Tables[0].Rows[0]["LastUpdateIP"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SourceId"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["SourceId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PirmaryKey"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["PirmaryKey"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [Id],[SupplierId],[Score],[ScoreType],[ScoreDes],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP],[Type],[Status],[SourceId],[PirmaryKey] ");
            strSql.Append(" FROM SC_SupplierScore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public static List<SC_SupplierScore> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierScore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierScore>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_SupplierScore> GetListBySupplierId(int sid)
        {
            string strWhere = string.Empty;
            strWhere += " SupplierId=@SupplierId";
            SqlParameter[] parameters = { new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            parameters[0].Value = sid;

            return GetList(strWhere, parameters);
        }

        public static List<SC_SupplierScore> GetListBySupplierId(int sid,int scoretype)
        {
            string strWhere = string.Empty;
            strWhere += " SupplierId=@SupplierId and ScoreType=@ScoreType";
            SqlParameter[] parameters = { new SqlParameter("@SupplierId", SqlDbType.Int, 4),
                                        new SqlParameter("@ScoreType",SqlDbType.Int,4)};
            parameters[0].Value = sid;
            parameters[1].Value = scoretype;

            return GetList(strWhere, parameters);
        }

        #endregion  成员方法

        public static int AddList(List<SC_SupplierScore> list,List<SC_NoCheckSupplierScore> nclist,SC_SupplierEvaluation se, SC_Supplier model, string Reviewers,SC_Log log)
        {
            if (list.Count <= 0)
                return 0;
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //删除审核人评分
                    new Supplier.DataAccess.SC_SupplierScoreDataProvider().DeleteBySupplierId(model.id, 1,trans,conn);
                    //删除供应商得分
                    new Supplier.DataAccess.SC_SupplierScoreDataProvider().DeleteBySupplierId(model.id, 2, trans, conn);
                    //删除未提交的审核人评分
                    new Supplier.DataAccess.SC_NoCheckSupplierScoreDataProvider().DeleteBySupplierId(model.id, 2, trans, conn);
                    //删除采购部意见备注
                    new Supplier.DataAccess.SC_SupplierEvaluationDataProvider().DeleteBySupplierId(model.id, trans, conn);
                    foreach (SC_SupplierScore sc in list)
                    {
                        ret = Add(sc, trans, conn); 
                    }
                    foreach (SC_NoCheckSupplierScore nc in nclist)
                    {
                        ret = new SC_NoCheckSupplierScoreDataProvider().Add(nc, trans, conn);
                    }

                    ret = new SC_SupplierEvaluationDataProvider().Add(se,trans,conn);
                   // SC_Supplier sup = new SC_SupplierDataProvider().GetModel(supplierId);
                    model.Status = ESP.Supplier.Common.State.SupplierStatus_questionChecked;
                    model.Reviewers = Reviewers;
                    new SC_SupplierDataProvider().Update(model, trans, conn);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();

                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    ret = 0;
                }
                return ret;
            }
            
        }
    }
}
