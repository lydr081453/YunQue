using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
namespace ESP.Finance.DataAccess
{
        /// <summary>
        /// 数据访问类F_returnInvoice。
        /// </summary>
        internal class ReturnInvoiceDataProvider:ESP.Finance.IDataAccess.IReturnInvoiceProvider
        {
            #region  成员方法
            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(int InvID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from F_returnInvoice");
                strSql.Append(" where InvID=@InvID ");
                SqlParameter[] parameters = {
					new SqlParameter("@InvID", SqlDbType.Int,4)};
                parameters[0].Value = InvID;

                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 增加一条数据
            /// </summary>
            public int Add(ESP.Finance.Entity.ReturnInvoiceInfo model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into F_returnInvoice(");
                strSql.Append("Status,ReturnID,InvoiceCode,RequestorID,RequestRemark,FAID,FARemark,FinanceID,FinanceRemark)");
                strSql.Append(" values (");
                strSql.Append("@Status,@ReturnID,@InvoiceCode,@RequestorID,@RequestRemark,@FAID,@FARemark,@FinanceID,@FinanceRemark)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@RequestRemark", SqlDbType.NVarChar,2000),
					new SqlParameter("@FAID", SqlDbType.Int,4),
					new SqlParameter("@FARemark", SqlDbType.NVarChar,2000),
					new SqlParameter("@FinanceID", SqlDbType.Int,4),
					new SqlParameter("@FinanceRemark", SqlDbType.NVarChar,2000)};
                parameters[0].Value = model.Status;
                parameters[1].Value = model.ReturnID;
                parameters[2].Value = model.InvoiceCode;
                parameters[3].Value = model.RequestorID;
                parameters[4].Value = model.RequestRemark;
                parameters[5].Value = model.FAID;
                parameters[6].Value = model.FARemark;
                parameters[7].Value = model.FinanceID;
                parameters[8].Value = model.FinanceRemark;

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
            public int Update(ESP.Finance.Entity.ReturnInvoiceInfo model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update F_returnInvoice set ");
                strSql.Append("Status=@Status,");
                strSql.Append("ReturnID=@ReturnID,");
                strSql.Append("InvoiceCode=@InvoiceCode,");
                strSql.Append("RequestorID=@RequestorID,");
                strSql.Append("RequestRemark=@RequestRemark,");
                strSql.Append("FAID=@FAID,");
                strSql.Append("FARemark=@FARemark,");
                strSql.Append("FinanceID=@FinanceID,");
                strSql.Append("FinanceRemark=@FinanceRemark");
                strSql.Append(" where InvID=@InvID ");
                SqlParameter[] parameters = {
					new SqlParameter("@InvID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@InvoiceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@RequestRemark", SqlDbType.NVarChar,2000),
					new SqlParameter("@FAID", SqlDbType.Int,4),
					new SqlParameter("@FARemark", SqlDbType.NVarChar,2000),
					new SqlParameter("@FinanceID", SqlDbType.Int,4),
					new SqlParameter("@FinanceRemark", SqlDbType.NVarChar,2000)};
                parameters[0].Value = model.InvID;
                parameters[1].Value = model.Status;
                parameters[2].Value = model.ReturnID;
                parameters[3].Value = model.InvoiceCode;
                parameters[4].Value = model.RequestorID;
                parameters[5].Value = model.RequestRemark;
                parameters[6].Value = model.FAID;
                parameters[7].Value = model.FARemark;
                parameters[8].Value = model.FinanceID;
                parameters[9].Value = model.FinanceRemark;

            return    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public int Delete(int InvID)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete F_returnInvoice ");
                strSql.Append(" where InvID=@InvID ");
                SqlParameter[] parameters = {
					new SqlParameter("@InvID", SqlDbType.Int,4)};
                parameters[0].Value = InvID;

             return   DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public ESP.Finance.Entity.ReturnInvoiceInfo GetModel(int InvID)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 InvID,Status,ReturnID,InvoiceCode,RequestorID,RequestRemark,FAID,FARemark,FinanceID,FinanceRemark from F_returnInvoice ");
                strSql.Append(" where InvID=@InvID ");
                SqlParameter[] parameters = {
					new SqlParameter("@InvID", SqlDbType.Int,4)};
                parameters[0].Value = InvID;

           return CBO.FillObject<ESP.Finance.Entity.ReturnInvoiceInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }

            public ESP.Finance.Entity.ReturnInvoiceInfo GetModelByReturnID(int ReturnID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 InvID,Status,ReturnID,InvoiceCode,RequestorID,RequestRemark,FAID,FARemark,FinanceID,FinanceRemark from F_returnInvoice ");
                strSql.Append(" where ReturnID=@ReturnID ");
                SqlParameter[] parameters = {
					new SqlParameter("@ReturnID", SqlDbType.Int,4)};
                parameters[0].Value = ReturnID;

                return CBO.FillObject<ESP.Finance.Entity.ReturnInvoiceInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }

            /// <summary>
            /// 获得数据列表
            /// </summary>
            public IList<ESP.Finance.Entity.ReturnInvoiceInfo> GetList(string strWhere,List<SqlParameter>paramList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select InvID,Status,ReturnID,InvoiceCode,RequestorID,RequestRemark,FAID,FARemark,FinanceID,FinanceRemark ");
                strSql.Append(" FROM F_returnInvoice ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                           return CBO.FillCollection<ESP.Finance.Entity.ReturnInvoiceInfo>(DbHelperSQL.Query(strSql.ToString(),paramList.ToArray()));
            }
            #endregion  成员方法
        }
}
