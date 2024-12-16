using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Collections.Generic;

namespace ESP.Purchase.DataAccess
{
    public class FeedBackProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Purchase.Entity.FeedBackInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO [ESP].[dbo].[SC_FeedBack]([supplierId],[supplierName],[feedback],[creator],[creatorName],[createTime]");
            strSql.Append(",[createIp],[status],[PriceScore],[ServiceScore],[QualityScore],[TimelinessScore],[Score],[ManagerFeedBack],[ModifiedDate],[ModifiedManagerId])");
            strSql.Append("VALUES(@supplierId,@supplierName,@feedback,@creator,@creatorName,@createTime,@createIp,@status,@PriceScore,@ServiceScore,@QualityScore,@TimelinessScore");
            strSql.Append(" ,@Score,@ManagerFeedBack,@ModifiedDate,@ModifiedManagerId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@supplierName", SqlDbType.NVarChar,200),
					new SqlParameter("@feedback", SqlDbType.NVarChar,2000),
					new SqlParameter("@creator", SqlDbType.Int,4),
					new SqlParameter("@creatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createIp", SqlDbType.NVarChar,50),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@PriceScore", SqlDbType.NVarChar,50),
					new SqlParameter("@ServiceScore", SqlDbType.NVarChar,50),
					new SqlParameter("@QualityScore", SqlDbType.NVarChar,50),
					new SqlParameter("@TimelinessScore", SqlDbType.NVarChar,50),
					new SqlParameter("@Score", SqlDbType.NVarChar,50),
					new SqlParameter("@ManagerFeedBack", SqlDbType.NVarChar,1000),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedManagerId", SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.supplierId;
            parameters[1].Value = model.supplierName;
            parameters[2].Value = model.feedback;
            parameters[3].Value = model.creator;
            parameters[4].Value = model.creatorName;
            parameters[5].Value = model.createTime;
            parameters[6].Value = model.createIp;
            parameters[7].Value = model.status;
            parameters[8].Value = model.PriceScore;
            parameters[9].Value = model.ServiceScore;
            parameters[10].Value = model.QualityScore;
            parameters[11].Value = model.TimelinessScore;
            parameters[12].Value = model.Score;
            parameters[13].Value = model.ManagerFeedBack;
            parameters[14].Value = model.ModifiedDate;
            parameters[15].Value = model.ModifiedManagerId;

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
    }
}
