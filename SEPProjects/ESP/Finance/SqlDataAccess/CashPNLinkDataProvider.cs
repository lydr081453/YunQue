using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.DataAccess;

namespace ESP.Finance.DataAccess
{
    internal class CashPNLinkDataProvider : IDataAccess.ICashPNLinkProvider
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.CashPNLinkInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 现金抵消关联和更新Return
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="returnModel"></param>
        /// <returns></returns>
        public bool AddAndUpdateReturn(List<ESP.Finance.Entity.CashPNLinkInfo> modelList, List<ESP.Finance.Entity.ReturnInfo> returnModels)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //更新return信息
                    foreach (ESP.Finance.Entity.ReturnInfo returnModel in returnModels)
                    {
                        new ESP.Finance.DataAccess.ReturnDataProvider().Update(returnModel, trans);
                    }
                    foreach (ESP.Finance.Entity.CashPNLinkInfo model in modelList)
                    {
                        Add(model, trans);
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.Message, "现金抵消", ESP.Logging.LogLevel.Error, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.CashPNLinkInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_CashPNLink(");
            strSql.Append("returnId,returnCashPrice,linkReturnId,linkRemark,linker,linkDate,oldPrice,linkType)");
            strSql.Append(" values (");
            strSql.Append("@returnId,@returnCashPrice,@linkReturnId,@linkRemark,@linker,@linkDate,@oldPrice,@linkType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@returnId", SqlDbType.Int,4),
					new SqlParameter("@returnCashPrice", SqlDbType.Decimal,18),
					new SqlParameter("@linkReturnId", SqlDbType.Int,4),
					new SqlParameter("@linkRemark", SqlDbType.NVarChar),
					new SqlParameter("@linker", SqlDbType.Int,4),
					new SqlParameter("@linkDate", SqlDbType.DateTime),
                    new SqlParameter("@oldPrice",SqlDbType.Decimal,18),      
                    new SqlParameter("@linkType",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.returnId;
            parameters[1].Value = model.returnCashPrice;
            parameters[2].Value = model.linkReturnId;
            parameters[3].Value = model.linkRemark;
            parameters[4].Value = model.linker;
            parameters[5].Value = model.linkDate;
            parameters[6].Value = model.oldPrice;
            parameters[7].Value = model.linkType;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.CashPNLinkInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_CashPNLink set ");
            strSql.Append("returnId=@returnId,");
            strSql.Append("returnCashPrice=@returnCashPrice,");
            strSql.Append("linkReturnId=@linkReturnId,");
            strSql.Append("linkRemark=@linkRemark,");
            strSql.Append("linker=@linker,");
            strSql.Append("linkDate=@linkDate,oldPrice=@oldPrice,linkType=@linkType");
            strSql.Append(" where CashPNLinkId=@CashPNLinkId");
            SqlParameter[] parameters = {
					new SqlParameter("@CashPNLinkId", SqlDbType.Int,4),
					new SqlParameter("@returnId", SqlDbType.Int,4),
					new SqlParameter("@returnCashPrice", SqlDbType.Decimal,18),
					new SqlParameter("@linkReturnId", SqlDbType.Int,4),
					new SqlParameter("@linkRemark", SqlDbType.NVarChar),
					new SqlParameter("@linker", SqlDbType.Int,4),
					new SqlParameter("@linkDate", SqlDbType.DateTime),
                    new SqlParameter("@oldPrice",SqlDbType.Decimal,18),      
                    new SqlParameter("@linkType",SqlDbType.Int,4)};
            parameters[0].Value = model.CashPNLinkId;
            parameters[1].Value = model.returnId;
            parameters[2].Value = model.returnCashPrice;
            parameters[3].Value = model.linkReturnId;
            parameters[4].Value = model.linkRemark;
            parameters[5].Value = model.linker;
            parameters[6].Value = model.linkDate;
            parameters[7].Value = model.oldPrice;
            parameters[8].Value = model.linkType;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CashPNLinkId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_CashPNLink ");
            strSql.Append(" where CashPNLinkId=@CashPNLinkId");
            SqlParameter[] parameters = {
					new SqlParameter("@CashPNLinkId", SqlDbType.Int,4)
				};
            parameters[0].Value = CashPNLinkId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.CashPNLinkInfo GetModel(int CashPNLinkId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_CashPNLink ");
            strSql.Append(" where CashPNLinkId=@CashPNLinkId");
            SqlParameter[] parameters = {
					new SqlParameter("@CashPNLinkId", SqlDbType.Int,4)};
            parameters[0].Value = CashPNLinkId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Finance.Entity.CashPNLinkInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ESP.Finance.Entity.CashPNLinkInfo> GetList(string terms, List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CashPNLinkId],[returnId],[returnCashPrice],[linkReturnId],[linkRemark],[linker],[linkDate],oldPrice,linkType ");
            strSql.Append(" FROM F_CashPNLink where 1=1");
            strSql.Append(" where " + terms);
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Finance.Entity.CashPNLinkInfo>(DbHelperSQL.Query(strSql.ToString(), parms));
        }
        #endregion  成员方法
    }
}
