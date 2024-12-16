using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Collections.Generic;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{    
    public class SalaryDetailDataProvider
    {
        public SalaryDetailDataProvider()
        { }
        #region  成员方法

        public int Add(SalaryDetailInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SalaryDetailInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_SalaryDetailInfo(");
            strSql.Append("creater,createrName,createDate,sysid,sysUserName,nowBasePay,nowMeritPay,newBasePay,newMeritPay,status)");
            strSql.Append(" values (");
            strSql.Append("@creater,@createrName,@createDate,@sysid,@sysUserName,@nowBasePay,@nowMeritPay,@newBasePay,@newMeritPay,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@nowBasePay", SqlDbType.Decimal,9),
					new SqlParameter("@nowMeritPay", SqlDbType.Decimal,9),
					new SqlParameter("@newBasePay", SqlDbType.Decimal,9),
					new SqlParameter("@newMeritPay", SqlDbType.Decimal,9),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.creater;
            parameters[1].Value = model.createrName;
            parameters[2].Value = model.createDate;
            parameters[3].Value = model.sysid;
            parameters[4].Value = model.sysUserName;
            parameters[5].Value = model.nowBasePay;
            parameters[6].Value = model.nowMeritPay;
            parameters[7].Value = model.newBasePay;
            parameters[8].Value = model.newMeritPay;
            parameters[9].Value = model.status;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public void Update(SalaryDetailInfo model)
        {
            Update(model, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SalaryDetailInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_SalaryDetailInfo set ");
            strSql.Append("creater=@creater,");
            strSql.Append("createrName=@createrName,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("sysid=@sysid,");
            strSql.Append("sysUserName=@sysUserName,");
            strSql.Append("nowBasePay=@nowBasePay,");
            strSql.Append("nowMeritPay=@nowMeritPay,");
            strSql.Append("newBasePay=@newBasePay,");
            strSql.Append("newMeritPay=@newMeritPay,");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@creater", SqlDbType.Int,4),
					new SqlParameter("@createrName", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.SmallDateTime),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@sysUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@nowBasePay", SqlDbType.Decimal,9),
					new SqlParameter("@nowMeritPay", SqlDbType.Decimal,9),
					new SqlParameter("@newBasePay", SqlDbType.Decimal,9),
					new SqlParameter("@newMeritPay", SqlDbType.Decimal,9),
                    new SqlParameter("@status",SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.creater;
            parameters[2].Value = model.createrName;
            parameters[3].Value = model.createDate;
            parameters[4].Value = model.sysid;
            parameters[5].Value = model.sysUserName;
            parameters[6].Value = model.nowBasePay;
            parameters[7].Value = model.nowMeritPay;
            parameters[8].Value = model.newBasePay;
            parameters[9].Value = model.newMeritPay;
            parameters[10].Value = model.status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        public void Delete(int id)
        {
            Delete(id, null);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_SalaryDetailInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SalaryDetailInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,creater,createrName,createDate,sysid,sysUserName,nowBasePay,nowMeritPay,newBasePay,newMeritPay,status from SEP_SalaryDetailInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SalaryDetailInfo model = new SalaryDetailInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["creater"].ToString() != "")
                {
                    model.creater = int.Parse(ds.Tables[0].Rows[0]["creater"].ToString());
                }
                model.createrName = ds.Tables[0].Rows[0]["createrName"].ToString();
                if (ds.Tables[0].Rows[0]["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                model.sysUserName = ds.Tables[0].Rows[0]["sysUserName"].ToString();
                if (ds.Tables[0].Rows[0]["nowBasePay"].ToString() != "")
                {
                    model.nowBasePay = decimal.Parse(ds.Tables[0].Rows[0]["nowBasePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["nowMeritPay"].ToString() != "")
                {
                    model.nowMeritPay = decimal.Parse(ds.Tables[0].Rows[0]["nowMeritPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["newBasePay"].ToString() != "")
                {
                    model.newBasePay = decimal.Parse(ds.Tables[0].Rows[0]["newBasePay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["newMeritPay"].ToString() != "")
                {
                    model.newMeritPay = decimal.Parse(ds.Tables[0].Rows[0]["newMeritPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
            strSql.Append("select id,creater,createrName,createDate,sysid,sysUserName,nowBasePay,nowMeritPay,newBasePay,newMeritPay,status ");
            strSql.Append(" FROM SEP_SalaryDetailInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得对象列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<SalaryDetailInfo> GetModelList(string strWhere, List<SqlParameter> parms)
        {
            string strSql = "select * from SEP_SalaryDetailInfo where 1=1 ";
            strSql += strWhere;
            List<SalaryDetailInfo> list = new List<SalaryDetailInfo>();
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql, parms))
            {
                while (r.Read())
                {
                    SalaryDetailInfo model = new SalaryDetailInfo();
                    model.PopupData(r);
                    list.Add(model);
                }
                r.Close();
            }
            return list;
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public SalaryDetailInfo GetTopModel(int sysId)
        {
            string strSql = "select top 1 * from SEP_SalaryDetailInfo where status = 1 and sysId=" + sysId;
            strSql += " order by id desc";
            SalaryDetailInfo model = null;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    model = new SalaryDetailInfo();
                    model.PopupData(r);
                }
                r.Close();
            }
            return model;
        }
        #endregion  成员方法
    }
}
