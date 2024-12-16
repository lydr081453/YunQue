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
    /// 数据访问类PaymentPeriodDataHelper。
    /// </summary>
    public class PaymentPeriodDataHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentPeriodDataHelper"/> class.
        /// </summary>
        public PaymentPeriodDataHelper()
        { }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_PaymentPeriod");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PaymentPeriodInfo model)
        {
            return Add(model, null,null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PaymentPeriodInfo model,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_PaymentPeriod(");
            strSql.Append("periodType,periodDatumPoint,periodDay,dateType,expectPaymentPrice,expectPaymentPercent,");
            strSql.Append("gid,beginDate,endDate,periodRemark,inceptPrice,inceptDate,");
            strSql.Append("finallyPaymentPrice,finallyPaymentDate,finallyPaymentUserId,finallyPaymentUserName,Status,TaxTypes)");
            strSql.Append(" values (");
            strSql.Append("@periodType,@periodDatumPoint,@periodDay,@dateType,@expectPaymentPrice,@expectPaymentPercent,");
            strSql.Append("@gid,@beginDate,@endDate,@periodRemark,@inceptPrice,@inceptDate,");
            strSql.Append("@finallyPaymentPrice,@finallyPaymentDate,@finallyPaymentUserId,@finallyPaymentUserName,@Status,@TaxTypes)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@periodType", SqlDbType.Int,4),
					new SqlParameter("@periodDatumPoint", SqlDbType.Int,4),
					new SqlParameter("@periodDay", SqlDbType.NVarChar,50),
					new SqlParameter("@dateType", SqlDbType.Int,4),
					new SqlParameter("@expectPaymentPrice", SqlDbType.Decimal,9),
					new SqlParameter("@expectPaymentPercent", SqlDbType.Decimal,9),
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@beginDate", SqlDbType.DateTime),
					new SqlParameter("@endDate", SqlDbType.DateTime),
					new SqlParameter("@periodRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
					new SqlParameter("@inceptDate", SqlDbType.DateTime),
					new SqlParameter("@finallyPaymentPrice", SqlDbType.Decimal,9),
					new SqlParameter("@finallyPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@finallyPaymentUserId", SqlDbType.NVarChar,10),
					new SqlParameter("@finallyPaymentUserName", SqlDbType.NVarChar,10),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@TaxTypes", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value = model.periodType;
            parameters[1].Value = model.periodDatumPoint;
            parameters[2].Value = model.periodDay;
            parameters[3].Value = model.dateType;
            parameters[4].Value = model.expectPaymentPrice;
            parameters[5].Value = model.expectPaymentPercent;
            parameters[6].Value = model.gid;
            parameters[7].Value = model.beginDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.beginDate;
            parameters[8].Value = model.endDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.endDate;
            parameters[9].Value = model.periodRemark;
            parameters[10].Value = model.inceptPrice;
            parameters[11].Value = model.inceptDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.inceptDate;
            parameters[12].Value = model.FinallyPaymentPrice;
            parameters[13].Value = model.FinallyPaymentDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.FinallyPaymentDate;
            parameters[14].Value = model.FinallyPaymentUserId;
            parameters[15].Value = model.FinallyPaymentUserName;
            parameters[16].Value = model.Status;
            parameters[17].Value = model.TaxTypes;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(PaymentPeriodInfo model) 
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(PaymentPeriodInfo model,SqlConnection conn,  SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_PaymentPeriod set ");
            strSql.Append("periodType=@periodType,");
            strSql.Append("periodDatumPoint=@periodDatumPoint,");
            strSql.Append("periodDay=@periodDay,");
            strSql.Append("dateType=@dateType,");
            strSql.Append("expectPaymentPrice=@expectPaymentPrice,");
            strSql.Append("expectPaymentPercent=@expectPaymentPercent,");
            strSql.Append("gid=@gid,");
            strSql.Append("beginDate=@beginDate,");
            strSql.Append("endDate=@endDate,");
            strSql.Append("periodRemark=@periodRemark,");
            strSql.Append("inceptPrice=@inceptPrice,");
            strSql.Append("inceptDate=@inceptDate,");
            strSql.Append("finallyPaymentPrice=@finallyPaymentPrice,");
            strSql.Append("finallyPaymentDate=@finallyPaymentDate,");
            strSql.Append("finallyPaymentUserId=@finallyPaymentUserId,");
            strSql.Append("finallyPaymentUserName=@finallyPaymentUserName,");
            strSql.Append("Status=@Status, returnId=@returnId, returnCode=@returnCode,TaxTypes=@TaxTypes ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@periodType", SqlDbType.Int,4),
					new SqlParameter("@periodDatumPoint", SqlDbType.Int,4),
					new SqlParameter("@periodDay", SqlDbType.NVarChar,50),
					new SqlParameter("@dateType", SqlDbType.Int,4),
					new SqlParameter("@expectPaymentPrice", SqlDbType.Decimal,9),
					new SqlParameter("@expectPaymentPercent", SqlDbType.Decimal,9),
					new SqlParameter("@gid", SqlDbType.Int,4),
					new SqlParameter("@beginDate", SqlDbType.DateTime),
					new SqlParameter("@endDate", SqlDbType.DateTime),
					new SqlParameter("@periodRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@inceptPrice", SqlDbType.Decimal,9),
					new SqlParameter("@inceptDate", SqlDbType.DateTime),
					new SqlParameter("@finallyPaymentPrice", SqlDbType.Decimal,9),
					new SqlParameter("@finallyPaymentDate", SqlDbType.DateTime),
					new SqlParameter("@finallyPaymentUserId", SqlDbType.NVarChar,10),
					new SqlParameter("@finallyPaymentUserName", SqlDbType.NVarChar,10),
					new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@returnId",SqlDbType.Int,4),
                    new SqlParameter("@returnCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@TaxTypes", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.periodType;
            parameters[2].Value = model.periodDatumPoint;
            parameters[3].Value = model.periodDay;
            parameters[4].Value = model.dateType;
            parameters[5].Value = model.expectPaymentPrice;
            parameters[6].Value = model.expectPaymentPercent;
            parameters[7].Value = model.gid;
            parameters[8].Value = model.beginDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.beginDate;
            parameters[9].Value = model.endDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.endDate;
            parameters[10].Value = model.periodRemark;
            parameters[11].Value = model.inceptPrice;
            parameters[12].Value = model.inceptDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.inceptDate;
            parameters[13].Value = model.FinallyPaymentPrice;
            parameters[14].Value = model.FinallyPaymentDate == DateTime.MinValue ? DateTime.Parse(Common.State.datetime_minvalue) : model.FinallyPaymentDate;
            parameters[15].Value = model.FinallyPaymentUserId;
            parameters[16].Value = model.FinallyPaymentUserName;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.ReturnId;
            parameters[19].Value = model.ReturnCode;
            parameters[20].Value = model.TaxTypes;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_PaymentPeriod ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);
        }

        public void Delete(int id)
        {
            Delete(id, null);
        }

        /// <summary>
        /// 删出申请单的付款账期信息
        /// </summary>
        /// <param name="gid"></param>
        public void DeletePeriod(int gid,SqlConnection conn, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_PaymentPeriod ");
            strSql.Append(" where gid=@gid and periodType=@type");
            SqlParameter[] parameters = {
					new SqlParameter("@gid", SqlDbType.Int,4),
                    new SqlParameter("@type",SqlDbType.Int,4)
				};
            parameters[0].Value = gid;
            parameters[1].Value = (int)State.PeriodType.period;
            DbHelperSQL.ExecuteSql(strSql.ToString(),conn,trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void DeleteAllByGeneralId(int gid, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM T_PaymentPeriod WHERE gid = @gid ");
            SqlParameter[] parameters = {
					new SqlParameter("@gid", SqlDbType.Int,4)};
            parameters[0].Value = gid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 根据ReturnId获得付款帐期
        /// </summary>
        /// <param name="returnId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public PaymentPeriodInfo GetModelByReturnId(int returnId,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 * FROM T_PaymentPeriod WHERE returnId = @returnId ");
            SqlParameter[] parameters = {
					new SqlParameter("@returnId", SqlDbType.Int,4)};
            parameters[0].Value = returnId;

            return ESP.Finance.Utility.CBO.FillObject<PaymentPeriodInfo>(DbHelperSQL.Query(strSql.ToString(), trans.Connection, trans, parameters));
        }
        public PaymentPeriodInfo GetModelByReturnId(int returnId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 * FROM T_PaymentPeriod WHERE returnId = @returnId ");
            SqlParameter[] parameters = {
					new SqlParameter("@returnId", SqlDbType.Int,4)};
            parameters[0].Value = returnId;

            return ESP.Finance.Utility.CBO.FillObject<PaymentPeriodInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PaymentPeriodInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,periodType,periodDatumPoint,periodDay,dateType,expectPaymentPrice,expectPaymentPercent,");
            strSql.Append("gid,beginDate,endDate,periodRemark,inceptPrice,inceptDate,");
            strSql.Append("finallyPaymentPrice,finallyPaymentDate,finallyPaymentUserId,finallyPaymentUserName,Status,ReturnId,ReturnCode,TaxTypes ");
            strSql.Append(" from T_PaymentPeriod ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            PaymentPeriodInfo model = new PaymentPeriodInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["periodType"].ToString() != "")
                {
                    model.periodType = int.Parse(ds.Tables[0].Rows[0]["periodType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["periodDatumPoint"].ToString() != "")
                {
                    model.periodDatumPoint = int.Parse(ds.Tables[0].Rows[0]["periodDatumPoint"].ToString());
                }

                model.periodDay = ds.Tables[0].Rows[0]["periodDay"].ToString();

                if (ds.Tables[0].Rows[0]["dateType"].ToString() != "")
                {
                    model.dateType = int.Parse(ds.Tables[0].Rows[0]["dateType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["expectPaymentPrice"].ToString() != "")
                {
                    model.expectPaymentPrice = decimal.Parse(ds.Tables[0].Rows[0]["expectPaymentPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["expectPaymentPercent"].ToString() != "")
                {
                    model.expectPaymentPercent = decimal.Parse(ds.Tables[0].Rows[0]["expectPaymentPercent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gid"].ToString() != "")
                {
                    model.gid = int.Parse(ds.Tables[0].Rows[0]["gid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["beginDate"].ToString() != "")
                {
                    model.beginDate = DateTime.Parse(ds.Tables[0].Rows[0]["beginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endDate"].ToString() != "")
                {
                    model.endDate = DateTime.Parse(ds.Tables[0].Rows[0]["endDate"].ToString());
                }
                model.periodRemark = ds.Tables[0].Rows[0]["periodRemark"].ToString();
                if (ds.Tables[0].Rows[0]["inceptPrice"].ToString() != "")
                {
                    model.inceptPrice = decimal.Parse(ds.Tables[0].Rows[0]["inceptPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["inceptDate"].ToString() != "")
                {
                    model.inceptDate = DateTime.Parse(ds.Tables[0].Rows[0]["inceptDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["finallyPaymentPrice"].ToString() != "")
                {
                    model.FinallyPaymentPrice = decimal.Parse(ds.Tables[0].Rows[0]["finallyPaymentPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["finallyPaymentDate"].ToString() != "")
                {
                    model.FinallyPaymentDate = DateTime.Parse(ds.Tables[0].Rows[0]["finallyPaymentDate"].ToString());
                }
                model.FinallyPaymentUserId = ds.Tables[0].Rows[0]["finallyPaymentUserId"].ToString();
                model.FinallyPaymentUserName = ds.Tables[0].Rows[0]["finallyPaymentUserName"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReturnId"].ToString() != "")
                {
                    model.ReturnId = int.Parse(ds.Tables[0].Rows[0]["ReturnId"].ToString());
                }
                model.ReturnCode = ds.Tables[0].Rows[0]["ReturnCode"].ToString();

                if (ds.Tables[0].Rows[0]["TaxTypes"].ToString() != "")
                {
                    model.TaxTypes = int.Parse(ds.Tables[0].Rows[0]["TaxTypes"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM T_PaymentPeriod ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string strWhere,System.Data.SqlClient.SqlTransaction trans)
        {
            if (trans == null)
                return null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_PaymentPeriod ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans);
        }

        public IList<PaymentPeriodInfo> GetModelList(string strWhere, System.Data.SqlClient.SqlTransaction trans)
        {
            if (trans == null)
                return null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_PaymentPeriod ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.Finance.Utility.CBO.FillCollection<PaymentPeriodInfo>(DbHelperSQL.Query(strSql.ToString()));
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetGeneralPaymentList(string strWhere, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   c.returnStatus,  a.*,b.prno,b.id as generalid,b.project_code,b.account_name,b.account_bank,b.account_number,b.orderid,b.Department,b.requestor,b.requestorname,b.requisition_committime ");
            strSql.Append(" FROM  T_PaymentPeriod AS a inner join T_GeneralInfo as b on a.gid=b.id ");
            strSql.Append("left join F_Return as c on a.ReturnId=c.returnid");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            if(parms != null)
                return DbHelperSQL.Query(strSql.ToString(),parms.ToArray());
            else
                return DbHelperSQL.Query(strSql.ToString());
        }

        public decimal getPaymentTotal(int generalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(expectpaymentprice) from T_PaymentPeriod where Gid={0}");
            string sql = string.Format(strSql.ToString(), generalid);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }


        public decimal getPaymentTypeTotal(int TypeID,int projectid,string projectcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select sum(expectpaymentprice) from T_PaymentPeriod where  gid in(
           select a.id from t_generalinfo a inner join t_orderinfo b on a.id=b.general_id 
           inner join t_type c on b.producttype=c.typeid 
           where c.parentid=" + TypeID.ToString() + " and a.status not in(0,2,-1,21) and a.prtype not in (1,6) and (project_code='" + projectcode + "'))");
            
            object obj = null;
            obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 取得一个pr单里面所有的已提交至财务系统的付款总金额.
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <returns></returns>
        public decimal getPaymentSum(int generalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(inceptPrice) from T_PaymentPeriod where Gid={0} and Status>{1} ");
            string sql = string.Format(strSql.ToString(), generalid, State.PaymentStatus_wait);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 获得申请单下已使用并未和收货关联的预付帐期
        /// </summary>
        /// <param name="generalid"></param>
        /// <returns></returns>
        public decimal getPayemntYF(int generalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(inceptPrice) from t_paymentperiod");
            strSql.Append(" where gid={0} and status>{1} and periodType={2} and id not in (select periodid from t_periodrecipient)");
            string sql = string.Format(strSql.ToString(), generalid, State.PaymentStatus_commit, (int)State.PeriodType.prepay);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        public decimal getPaymentSum(int generalid, System.Data.SqlClient.SqlConnection conn, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(inceptPrice) from T_PaymentPeriod where Gid={0} and Status={1} ");
            string sql = string.Format(strSql.ToString(), generalid, State.PaymentStatus_commit);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString(),conn,trans,null);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 取得一个pr单里面所有的已提交至财务系统的付款总金额..
        /// </summary>
        /// <param name="generalid">The generalid.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public decimal getPaymentSum(int generalid,int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(expectpaymentprice) from T_PaymentPeriod where Gid={0} and Status={1} ");
            string sql = string.Format(strSql.ToString(), generalid, status);
            object obj = null;
            obj = DbHelperSQL.GetSingle(sql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(obj.ToString());
            }
        }

        #endregion  成员方法
  }
}