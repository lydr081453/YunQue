using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    internal class PNBatchRelationDataProvider : ESP.Finance.IDataAccess.IPNBatchRelationProvider
    {
        public PNBatchRelationDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int batchID, int ReturnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PNBatchRelation");
            strSql.Append(" where batchID=@batchID and ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@batchID", SqlDbType.Int,4),
                    new SqlParameter("@ReturnID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = batchID;
            parameters[1].Value = ReturnID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool Exists(int batchID, int ReturnID, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PNBatchRelation");
            strSql.Append(" where batchID=@batchID and ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@batchID", SqlDbType.Int,4),
                    new SqlParameter("@ReturnID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = batchID;
            parameters[1].Value = ReturnID;
            return DbHelperSQL.Exists(strSql.ToString(), trans, parameters);
        }

        public int DeleteByBatchID(int BatchID)
        {
            return DeleteByBatchID(BatchID, null);
        }

        public int DeleteByBatchID(int BatchID, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PNBatchRelation ");
            strSql.Append(" where BatchID=@BatchID ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p2 = new SqlParameter("@BatchID", SqlDbType.Int, 4);
            p2.Value = BatchID;
            parameters.Add(p2);
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters.ToArray());
        }

        public int DeleteByReturnID(int batchid, ESP.Finance.Entity.ReturnInfo model)
        {
            string mediaids = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PNBatchRelation ");
            strSql.Append(" where ReturnID=@ReturnID ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter p2 = new SqlParameter("@ReturnID", SqlDbType.Int, 4);
            p2.Value = model.ReturnID;
            parameters.Add(p2);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update F_PNBatch set Amounts=Amounts-@Amount where BatchID=@BatchID ");
            List<SqlParameter> parameters2 = new List<SqlParameter>();
            SqlParameter pp1 = new SqlParameter("@Amount", SqlDbType.Decimal, 20);
            SqlParameter pp2 = new SqlParameter("@BatchID", SqlDbType.Int, 4);
            pp1.Value = model.PreFee;
            pp2.Value = batchid;
            parameters2.Add(pp1);
            parameters2.Add(pp2);

            int ret = 0;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    ret = DbHelperSQL.ExecuteSql(strSql2.ToString(), trans, parameters2.ToArray());
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters.ToArray());

                    if (!string.IsNullOrEmpty(model.MediaOrderIDs))
                    {
                        mediaids = model.MediaOrderIDs.TrimEnd(',');
                        ret = DbHelperSQL.ExecuteSql("update  t_mediaorder set ispayment=0,paymentuserid=0 where meidaorderid in(" + mediaids + ")", trans, null);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
            return ret;
        }

        public int Add(ESP.Finance.Entity.PNBatchRelationInfo model)
        {
            return Add(model, null);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.PNBatchRelationInfo model, SqlTransaction trans)
        {
            if (Exists(model.BatchID.Value, model.ReturnID.Value, trans) == true)
            {
                return 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PNBatchRelation(");
            strSql.Append("BatchID,ReturnID)");
            strSql.Append(" values (");
            strSql.Append("@BatchID,@ReturnID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = model.BatchID;
            parameters[1].Value = model.ReturnID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Add(List<ESP.Finance.Entity.PNBatchRelationInfo> relationList)
        {
            int count = 0;

            if (relationList != null && relationList.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        foreach (ESP.Finance.Entity.PNBatchRelationInfo model in relationList)
                        {
                            if (Add(model, trans) > 0)
                                count++;
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                    finally
                    {
                    }
                }

            }
            return count;
        }

        public int Add(List<ESP.Finance.Entity.PNBatchRelationInfo> relationList, SqlTransaction trans)
        {
            int count = 0;
            if (relationList != null && relationList.Count > 0)
            {
                foreach (ESP.Finance.Entity.PNBatchRelationInfo model in relationList)
                {
                    if (Add(model, trans) > 0)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.PNBatchRelationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PNBatchRelation set ");
            strSql.Append("BatchID=@BatchID,");
            strSql.Append("ReturnID=@ReturnID");
            strSql.Append(" where BatchRelationID=@BatchRelationID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchRelationID", SqlDbType.Int,4),
					new SqlParameter("@BatchID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = model.BatchRelationID;
            parameters[1].Value = model.BatchID;
            parameters[2].Value = model.ReturnID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BatchRelationID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PNBatchRelation ");
            strSql.Append(" where BatchRelationID=@BatchRelationID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchRelationID", SqlDbType.Int,4)};
            parameters[0].Value = BatchRelationID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int batchid, int returnid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PNBatchRelation ");
            strSql.Append(" where BatchID=@BatchID and ReturnID=@ReturnID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchID", SqlDbType.Int,4),new SqlParameter("@ReturnID", SqlDbType.Int,4)};
            parameters[0].Value = batchid;
            parameters[1].Value = returnid;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.PNBatchRelationInfo GetModel(int BatchRelationID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BatchRelationID,BatchID,ReturnID from F_PNBatchRelation ");
            strSql.Append(" where BatchRelationID=@BatchRelationID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BatchRelationID", SqlDbType.Int,4)};
            parameters[0].Value = BatchRelationID;
            return CBO.FillObject<ESP.Finance.Entity.PNBatchRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public ESP.Finance.Entity.PNBatchRelationInfo GetModelByReturnId(int returnId, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BatchRelationID,BatchID,ReturnID from F_PNBatchRelation ");
            strSql.Append(" where returnId=@returnId ");
            SqlParameter[] parameters = {
					new SqlParameter("@returnId", SqlDbType.Int,4)};
            parameters[0].Value = returnId;
            if (trans != null)
                return CBO.FillObject<ESP.Finance.Entity.PNBatchRelationInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
            else
                return CBO.FillObject<ESP.Finance.Entity.PNBatchRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BatchRelationID,BatchID,ReturnID ");
            strSql.Append(" FROM F_PNBatchRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PNBatchRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ESP.Finance.Entity.PNBatchRelationInfo> GetList(string strWhere, List<SqlParameter> parameters, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BatchRelationID,BatchID,ReturnID ");
            strSql.Append(" FROM F_PNBatchRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.PNBatchRelationInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters.ToArray()));
        }
        #endregion
    }
}