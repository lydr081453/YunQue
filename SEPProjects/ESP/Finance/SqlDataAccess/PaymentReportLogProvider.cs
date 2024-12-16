using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类 PaymentReportLog。
    /// </summary>
    internal class PaymentReportLogProvider : ESP.Finance.IDataAccess.IPaymentReportLogProvider
    {
 
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int userid, DateTime dt1 ,DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PaymentReportLog");
            strSql.Append(" where userid=@userid and (ReadTime between @dt1 and @dt2)");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@dt1", SqlDbType.DateTime,8),
                    new SqlParameter("@dt2", SqlDbType.DateTime,8)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = dt1;
            parameters[2].Value = dt2;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.PaymentReportLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PaymentReportLog(UserId,ReadTime) values (@UserId,@ReadTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ReadTime", SqlDbType.DateTime,8)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.ReadTime;
 

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

    }
}
