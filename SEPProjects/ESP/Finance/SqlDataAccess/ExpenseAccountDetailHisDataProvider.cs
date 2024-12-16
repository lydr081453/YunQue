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
    /// 数据访问类ExpenseAccountDetailHisDataProvider。
    /// </summary>
    internal class ExpenseAccountDetailHisDataProvider : ESP.Finance.IDataAccess.IExpenseAccountDetailHisProvider
    {
        public ExpenseAccountDetailHisDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseAccountDetailHis(");
            strSql.Append("ExpenseAccountDetailID,ReturnID,ExpenseDescOld,ExpenseDescNew,ExpenseMoneyOld,ExpenseMoneyNew,CurrentPreFeeOld,CurrentPreFeeNew,ModifyUserID,ModifyUserName,ModifyDateTime)");
            strSql.Append(" values (");
            strSql.Append("@ExpenseAccountDetailID,@ReturnID,@ExpenseDescOld,@ExpenseDescNew,@ExpenseMoneyOld,@ExpenseMoneyNew,@CurrentPreFeeOld,@CurrentPreFeeNew,@ModifyUserID,@ModifyUserName,@ModifyDateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpenseAccountDetailID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDescOld", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseDescNew", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseMoneyOld", SqlDbType.Decimal,9),
					new SqlParameter("@ExpenseMoneyNew", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentPreFeeOld", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentPreFeeNew", SqlDbType.Decimal,9),
					new SqlParameter("@ModifyUserID", SqlDbType.Int,4),
					new SqlParameter("@ModifyUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModifyDateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ExpenseAccountDetailID;
            parameters[1].Value = model.ReturnID;
            parameters[2].Value = model.ExpenseDescOld;
            parameters[3].Value = model.ExpenseDescNew;
            parameters[4].Value = model.ExpenseMoneyOld;
            parameters[5].Value = model.ExpenseMoneyNew;
            parameters[6].Value = model.CurrentPreFeeOld;
            parameters[7].Value = model.CurrentPreFeeNew;
            parameters[8].Value = model.ModifyUserID;
            parameters[9].Value = model.ModifyUserName;
            parameters[10].Value = model.ModifyDateTime;

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
        public void Update(ESP.Finance.Entity.ExpenseAccountDetailHisInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseAccountDetailHis set ");
            strSql.Append("ExpenseAccountDetailID=@ExpenseAccountDetailID,");
            strSql.Append("ReturnID=@ReturnID,");
            strSql.Append("ExpenseDescOld=@ExpenseDescOld,");
            strSql.Append("ExpenseDescNew=@ExpenseDescNew,");
            strSql.Append("ExpenseMoneyOld=@ExpenseMoneyOld,");
            strSql.Append("ExpenseMoneyNew=@ExpenseMoneyNew,");
            strSql.Append("CurrentPreFeeOld=@CurrentPreFeeOld,");
            strSql.Append("CurrentPreFeeNew=@CurrentPreFeeNew,");
            strSql.Append("ModifyUserID=@ModifyUserID,");
            strSql.Append("ModifyUserName=@ModifyUserName,");
            strSql.Append("ModifyDateTime=@ModifyDateTime");
            strSql.Append(" where HisID=@HisID ");
            SqlParameter[] parameters = {
					new SqlParameter("@HisID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseAccountDetailID", SqlDbType.Int,4),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@ExpenseDescOld", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseDescNew", SqlDbType.NVarChar,2000),
					new SqlParameter("@ExpenseMoneyOld", SqlDbType.Decimal,9),
					new SqlParameter("@ExpenseMoneyNew", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentPreFeeOld", SqlDbType.Decimal,9),
					new SqlParameter("@CurrentPreFeeNew", SqlDbType.Decimal,9),
					new SqlParameter("@ModifyUserID", SqlDbType.Int,4),
					new SqlParameter("@ModifyUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModifyDateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.HisID;
            parameters[1].Value = model.ExpenseAccountDetailID;
            parameters[2].Value = model.ReturnID;
            parameters[3].Value = model.ExpenseDescOld;
            parameters[4].Value = model.ExpenseDescNew;
            parameters[5].Value = model.ExpenseMoneyOld;
            parameters[6].Value = model.ExpenseMoneyNew;
            parameters[7].Value = model.CurrentPreFeeOld;
            parameters[8].Value = model.CurrentPreFeeNew;
            parameters[9].Value = model.ModifyUserID;
            parameters[10].Value = model.ModifyUserName;
            parameters[11].Value = model.ModifyDateTime;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int HisID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseAccountDetailHis ");
            strSql.Append(" where HisID=@HisID ");
            SqlParameter[] parameters = {
					new SqlParameter("@HisID", SqlDbType.Int,4)};
            parameters[0].Value = HisID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpenseAccountDetailHisInfo GetModel(int HisID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 HisID,ExpenseAccountDetailID,ReturnID,ExpenseDescOld,ExpenseDescNew,ExpenseMoneyOld,ExpenseMoneyNew,CurrentPreFeeOld,CurrentPreFeeNew,ModifyUserID,ModifyUserName,ModifyDateTime from F_ExpenseAccountDetailHis ");
            strSql.Append(" where HisID=@HisID ");
            SqlParameter[] parameters = {
					new SqlParameter("@HisID", SqlDbType.Int,4)};
            parameters[0].Value = HisID;

            return CBO.FillObject<ExpenseAccountDetailHisInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetDsList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HisID,ExpenseAccountDetailID,ReturnID,ExpenseDescOld,ExpenseDescNew,ExpenseMoneyOld,ExpenseMoneyNew,CurrentPreFeeOld,CurrentPreFeeNew,ModifyUserID,ModifyUserName,ModifyDateTime ");
            strSql.Append(" FROM F_ExpenseAccountDetailHis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExpenseAccountDetailHisInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HisID,ExpenseAccountDetailID,ReturnID,ExpenseDescOld,ExpenseDescNew,ExpenseMoneyOld,ExpenseMoneyNew,CurrentPreFeeOld,CurrentPreFeeNew,ModifyUserID,ModifyUserName,ModifyDateTime ");
            strSql.Append(" FROM F_ExpenseAccountDetailHis ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ExpenseAccountDetailHisInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
    }
}

