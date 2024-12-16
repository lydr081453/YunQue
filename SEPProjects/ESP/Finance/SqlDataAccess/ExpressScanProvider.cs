using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_ExpressScan。
    /// </summary>
    internal class ExpressScanProvider : ESP.Finance.IDataAccess.IExpressScanProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string expressNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ExpressScan");
            strSql.Append(" where expressNo=@expressNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@expressNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = expressNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpressScanInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpressScan(");
            strSql.Append("ExpressNo,ExpYear,ExpMonth,Company,Creater,CreateDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@ExpressNo,@ExpYear,@ExpMonth,@Company,@Creater,@CreateDate,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpYear", SqlDbType.Int,4),
					new SqlParameter("@ExpMonth", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@Creater", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ExpressNo;
            parameters[1].Value = model.ExpYear;
            parameters[2].Value = model.ExpMonth;
            parameters[3].Value = model.Company;
            parameters[4].Value = model.Creater;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.Status;


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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ExpressScanInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpressScan set ");
            strSql.Append(" ExpressNo=@ExpressNo,ExpYear=@ExpYear,ExpMonth=@ExpMonth,Company=@Company,Creater=@Creater,CreateDate=@CreateDate,Status=@Status");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
					new SqlParameter("@ExpressNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpYear", SqlDbType.Int,4),
					new SqlParameter("@ExpMonth", SqlDbType.Int,4),
					new SqlParameter("@Company", SqlDbType.NVarChar,50),
					new SqlParameter("@Creater", SqlDbType.Int,4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                     new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ExpressNo;
            parameters[1].Value = model.ExpYear;
            parameters[2].Value = model.ExpMonth;
            parameters[3].Value = model.Company;
            parameters[4].Value = model.Creater;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpressScan ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpressScanInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_ExpressScan ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<ExpressScanInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpressScanInfo GetModel(string expNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from F_ExpressScan ");
            strSql.Append(" where expressno=@expressno ");
            SqlParameter[] parameters = {
					new SqlParameter("@expressno", SqlDbType.NVarChar,50)};
            parameters[0].Value = expNo;

            return CBO.FillObject<ExpressScanInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ExpressScanInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_ExpressScan ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }

            return CBO.FillCollection<ExpressScanInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
  
    }
}
