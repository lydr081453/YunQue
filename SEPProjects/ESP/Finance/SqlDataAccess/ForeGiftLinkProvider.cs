using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace ESP.Finance.DataAccess
{
    public class ForeGiftLinkDataProvider : IDataAccess.IForeGiftProvider
    {
        public ForeGiftLinkDataProvider()
        { }
        #region  成员方法

        public int Add(ESP.Finance.Entity.ForeGiftLinkInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ForeGiftLinkInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ForeGiftLink(");
            strSql.Append("foregiftPrId,foregiftReturnId,killforegiftPrId,killforegiftReturnId,linker,linkDate,killRemark,killPrice)");
            strSql.Append(" values (");
            strSql.Append("@foregiftPrId,@foregiftReturnId,@killforegiftPrId,@killforegiftReturnId,@linker,@linkDate,@killRemark,@killPrice)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@foregiftPrId", SqlDbType.Int,4),
					new SqlParameter("@foregiftReturnId", SqlDbType.Int,4),
					new SqlParameter("@killforegiftPrId", SqlDbType.Int,4),
					new SqlParameter("@killforegiftReturnId", SqlDbType.Int,4),
					new SqlParameter("@linker", SqlDbType.Int,4),
					new SqlParameter("@linkDate", SqlDbType.DateTime),
					new SqlParameter("@killRemark", SqlDbType.NVarChar,1000),
                    new SqlParameter("@killPrice",SqlDbType.Decimal,18)                    
                                        };
            parameters[0].Value = model.foregiftPrId;
            parameters[1].Value = model.foregiftReturnId;
            parameters[2].Value = model.killforegiftPrId;
            parameters[3].Value = model.killforegiftReturnId;
            parameters[4].Value = model.linker;
            parameters[5].Value = model.linkDate;
            parameters[6].Value = model.killRemark;
            parameters[7].Value = model.killPrice;

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
        /// 添加抵消押金信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="trans"></param>
        public void addList(List<ESP.Finance.Entity.ForeGiftLinkInfo> modelList, SqlTransaction trans)
        {
            foreach (ESP.Finance.Entity.ForeGiftLinkInfo model in modelList)
            {
                Add(model, trans);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ForeGiftLinkInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ForeGiftLink set ");
            strSql.Append("foregiftPrId=@foregiftPrId,");
            strSql.Append("foregiftReturnId=@foregiftReturnId,");
            strSql.Append("killforegiftPrId=@killforegiftPrId,");
            strSql.Append("killforegiftReturnId=@killforegiftReturnId,");
            strSql.Append("linker=@linker,");
            strSql.Append("linkDate=@linkDate,");
            strSql.Append("killRemark=@killRemark,");
            strSql.Append("killPrice=@killPrice");
            strSql.Append(" where linkId=@linkId ");
            SqlParameter[] parameters = {
					new SqlParameter("@linkId", SqlDbType.Int,4),
					new SqlParameter("@foregiftPrId", SqlDbType.Int,4),
					new SqlParameter("@foregiftReturnId", SqlDbType.Int,4),
					new SqlParameter("@killforegiftPrId", SqlDbType.Int,4),
					new SqlParameter("@killforegiftReturnId", SqlDbType.Int,4),
					new SqlParameter("@linker", SqlDbType.Int,4),
					new SqlParameter("@linkDate", SqlDbType.DateTime),
					new SqlParameter("@killRemark", SqlDbType.NVarChar,1000),
                    new SqlParameter("@killPrice",SqlDbType.Decimal,18)                    
                                        };
            parameters[0].Value = model.linkId;
            parameters[1].Value = model.foregiftPrId;
            parameters[2].Value = model.foregiftReturnId;
            parameters[3].Value = model.killforegiftPrId;
            parameters[4].Value = model.killforegiftReturnId;
            parameters[5].Value = model.linker;
            parameters[6].Value = model.linkDate;
            parameters[7].Value = model.killRemark;
            parameters[8].Value = model.killPrice;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int linkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ForeGiftLink ");
            strSql.Append(" where linkId=@linkId ");
            SqlParameter[] parameters = {
					new SqlParameter("@linkId", SqlDbType.Int,4)};
            parameters[0].Value = linkId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ForeGiftLinkInfo GetModel(int linkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 linkId,foregiftPrId,foregiftReturnId,killforegiftPrId,killforegiftReturnId,linker,linkDate,killRemark,killPrice from F_ForeGiftLink ");
            strSql.Append(" where linkId=@linkId ");
            SqlParameter[] parameters = {
					new SqlParameter("@linkId", SqlDbType.Int,4)};
            parameters[0].Value = linkId;
            return CBO.FillObject<ESP.Finance.Entity.ForeGiftLinkInfo>(DbHelperSQL.Query(strSql.ToString(),parameters));
        }

        public bool Exists(int returnid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ForeGiftLink");
            strSql.Append(" where foregiftreturnid=@foregiftreturnid ");
            SqlParameter[] parameters = {
					new SqlParameter("@foregiftreturnid", SqlDbType.Int,4)};
            parameters[0].Value = returnid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.ForeGiftLinkInfo> GetList(string strWhere,List<SqlParameter> paramList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select linkId,foregiftPrId,foregiftReturnId,killforegiftPrId,killforegiftReturnId,linker,linkDate,killRemark,killPrice ");
            strSql.Append(" FROM F_ForeGiftLink ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<ESP.Finance.Entity.ForeGiftLinkInfo>(DbHelperSQL.Query(strSql.ToString(),paramList));
        }

        /// <summary>
        /// 获得已和押金关联的付款申请
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parmList"></param>
        /// <returns></returns>
        public DataTable GetKillList(string strWhere, List<SqlParameter> parmList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM F_ForeGiftLink as a");
            strSql.Append(" LEFT JOIN F_Return as b on a.killforegiftReturnId = b.ReturnID");
            strSql.Append(" WHERE 1=1" + strWhere);
            return DbHelperSQL.Query(strSql.ToString(), parmList).Tables[0];
        }

        #endregion  成员方法
    }
}
