using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class FeedBackDataProvider
    {
        public FeedBackDataProvider()
        { }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(FeedBackInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"Update SC_FeedBack set supplierId=@supplierId, supplierName=@supplierName, feedback=@feedback, status=@status , PriceScore=@PriceScore, ServiceScore=@ServiceScore
                , QualityScore=@QualityScore, TimelinessScore=@TimelinessScore, Score=@Score, ManagerFeedBack=@ManagerFeedBack, ModifiedDate=@ModifiedDate, ModifiedManagerId=@ModifiedManagerId");

            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@supplierName", SqlDbType.Int,4),
					new SqlParameter("@feedback", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@PriceScore", SqlDbType.Int,4),
					new SqlParameter("@ServiceScore", SqlDbType.Int,4),
					new SqlParameter("@QualityScore", SqlDbType.Int,4),
					new SqlParameter("@TimelinessScore", SqlDbType.Int,4),
					new SqlParameter("@Score", SqlDbType.Int,4),
					new SqlParameter("@ManagerFeedBack", SqlDbType.Int,4),
					new SqlParameter("@ModifiedDate", SqlDbType.Int,4),
					new SqlParameter("@ModifiedManagerId", SqlDbType.Int,4)
				};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.SupplierName;
            parameters[3].Value = model.FeedBack;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.PriceScore;
            parameters[6].Value = model.ServiceScore;
            parameters[7].Value = model.QualityScore;
            parameters[8].Value = model.TimelinessScore;
            parameters[9].Value = model.Score;
            parameters[10].Value = model.ManagerFeedBack;
            parameters[11].Value = DateTime.Now; ;
            parameters[12].Value = model.ModifiedManagerId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update SC_FeedBack set status=1 ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
				};
            parameters[0].Value = Id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public List<FeedBackInfo> GetList(string condition, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_FeedBack");
            if(!string.IsNullOrEmpty(condition))
                strSql.Append(" Where " + condition);

            if (!string.IsNullOrEmpty(orderby))
                strSql.Append(" Order by " + orderby);

            
            return ESP.ConfigCommon.CBO.FillCollection<FeedBackInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FeedBackInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_FeedBack ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            return ESP.ConfigCommon.CBO.FillObject<FeedBackInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<FeedBackInfo> GetListBySupplierId(int SupplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_FeedBack where status<>1 and supplierId="+SupplierId);
            return ESP.ConfigCommon.CBO.FillCollection<FeedBackInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
