using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类T_MediaPREditHis。
    /// </summary>
    public class MediaPREditHisDataProvider
    {
        public MediaPREditHisDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Purchase.Entity.MediaPREditHisInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo from T_MediaPREditHis ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.MediaPREditHisInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public int ADD(ESP.Purchase.Entity.MediaPREditHisInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_MediaPREditHis(");
            strSql.Append("OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo,OldPaymentID,OrderType)");
            strSql.Append(" values (");
            strSql.Append("@OldPRId,@OldPRNo,@NewPRId,@NewPRNo,@NewPNId,@NewPNNo,@OldPaymentID,@OrderType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OldPRId", SqlDbType.Int,4),
					new SqlParameter("@OldPRNo", SqlDbType.NChar,20),
					new SqlParameter("@NewPRId", SqlDbType.Int,4),
					new SqlParameter("@NewPRNo", SqlDbType.NChar,20),
					new SqlParameter("@NewPNId", SqlDbType.Int,4),
					new SqlParameter("@NewPNNo", SqlDbType.NChar,20),
                    new SqlParameter("@OldPaymentID", SqlDbType.NVarChar,500),
                    new SqlParameter("@OrderType", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.OldPRId;
            parameters[1].Value = model.OldPRNo;
            parameters[2].Value = model.NewPRId;
            parameters[3].Value = model.NewPRNo;
            parameters[4].Value = model.NewPNId;
            parameters[5].Value = model.NewPNNo;
            parameters[6].Value = model.OldPaymentID;
            parameters[7].Value = model.OrderType;

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

        public void Update(ESP.Purchase.Entity.MediaPREditHisInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_MediaPREditHis set ");
            strSql.Append("OldPRId=@OldPRId,");
            strSql.Append("OldPRNo=@OldPRNo,");
            strSql.Append("NewPRId=@NewPRId,");
            strSql.Append("NewPRNo=@NewPRNo,");
            strSql.Append("NewPNId=@NewPNId,");
            strSql.Append("NewPNNo=@NewPNNo,");
            strSql.Append("OldPaymentID=@OldPaymentID,");
            strSql.Append("OrderType=@OrderType");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@OldPRId", SqlDbType.Int,4),
					new SqlParameter("@OldPRNo", SqlDbType.NChar,20),
					new SqlParameter("@NewPRId", SqlDbType.Int,4),
					new SqlParameter("@NewPRNo", SqlDbType.NChar,20),
					new SqlParameter("@NewPNId", SqlDbType.Int,4),
					new SqlParameter("@NewPNNo", SqlDbType.NChar,20),
                    new SqlParameter("@OldPaymentID", SqlDbType.NVarChar,500),
                    new SqlParameter("@OrderType", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.OldPRId;
            parameters[2].Value = model.OldPRNo;
            parameters[3].Value = model.NewPRId;
            parameters[4].Value = model.NewPRNo;
            parameters[5].Value = model.NewPNId;
            parameters[6].Value = model.NewPNNo;
            parameters[7].Value = model.OldPaymentID;
            parameters[8].Value = model.OrderType;

            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }
        public ESP.Purchase.Entity.MediaPREditHisInfo GetModelByNewPRID(int NewPrId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo,OldPaymentID,OrderType from T_MediaPREditHis ");
            strSql.Append(" where NewPRId=@NewPRId ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewPRId",SqlDbType.Int,4)};
            parameters[0].Value = NewPrId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.MediaPREditHisInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public ESP.Purchase.Entity.MediaPREditHisInfo GetModelByOldPRID(int OldPRID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo,OldPaymentID,OrderType from T_MediaPREditHis ");
            strSql.Append(" where OldPRID=@OldPRID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OldPRID",SqlDbType.Int,4)};
            parameters[0].Value = OldPRID;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.MediaPREditHisInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Purchase.Entity.MediaPREditHisInfo>  GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo,OldPaymentID,OrderType ");
            strSql.Append(" FROM T_MediaPREditHis ");
            if (term.Trim() != "")
            {
                strSql.Append(" where " + term);
            }
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.MediaPREditHisInfo>(DbHelperSQL.Query(strSql.ToString(), param.ToArray()));
        }

        public IList<ESP.Purchase.Entity.MediaPREditHisInfo> GetList(string term, System.Data.SqlClient.SqlConnection conn,System.Data.SqlClient.SqlTransaction trans, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,OldPRId,OldPRNo,NewPRId,NewPRNo,NewPNId,NewPNNo,OldPaymentID,OrderType ");
            strSql.Append(" FROM T_MediaPREditHis ");
            if (term.Trim() != "")
            {
                strSql.Append(" where " + term);
            }
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.MediaPREditHisInfo>(DbHelperSQL.Query(strSql.ToString(),conn,trans,param.ToArray()));
        }
        #endregion  成员方法
    }
}
