using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data;
using System.Data.SqlClient;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionIndemnityDataProvider
    {
        public DimissionIndemnityDataProvider()
        { }

        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DimissionIndemnityId", "sep_DimissionIndemnity");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionIndemnity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sep_DimissionIndemnity");
            strSql.Append(" where DimissionIndemnityId=@DimissionIndemnityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionIndemnityId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionIndemnity;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DimissionIndemnityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_DimissionIndemnity(");
            strSql.Append("IndemnityItem,IndemnityAmount,IndemnityDesc,CreateTime,UpdateTime,CreateUserid,DimissionId)");
            strSql.Append(" values (");
            strSql.Append("@IndemnityItem,@IndemnityAmount,@IndemnityDesc,@CreateTime,@UpdateTime,@CreateUserid,@DimissionId)");
            SqlParameter[] parameters = {
					new SqlParameter("@IndemnityItem", SqlDbType.NVarChar,1024),
					new SqlParameter("@IndemnityAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IndemnityDesc", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserid", SqlDbType.Int,4),
                    new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = model.IndemnityItem;
            parameters[1].Value = model.IndemnityAmount;
            parameters[2].Value = model.IndemnityDesc;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.CreateUserid;
            parameters[6].Value = model.DimissionId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DimissionIndemnityInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_DimissionIndemnity(");
            strSql.Append("IndemnityItem,IndemnityAmount,IndemnityDesc,CreateTime,UpdateTime,CreateUserid,DimissionId)");
            strSql.Append(" values (");
            strSql.Append("@IndemnityItem,@IndemnityAmount,@IndemnityDesc,@CreateTime,@UpdateTime,@CreateUserid,@DimissionId)");
            SqlParameter[] parameters = {
					new SqlParameter("@IndemnityItem", SqlDbType.NVarChar,1024),
					new SqlParameter("@IndemnityAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IndemnityDesc", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserid", SqlDbType.Int,4),
                    new SqlParameter("@DimissionId", SqlDbType.Int, 4)};
            parameters[0].Value = model.IndemnityItem;
            parameters[1].Value = model.IndemnityAmount;
            parameters[2].Value = model.IndemnityDesc;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.CreateUserid;
            parameters[6].Value = model.DimissionId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DimissionIndemnityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_DimissionIndemnity set ");
            strSql.Append("IndemnityItem=@IndemnityItem,");
            strSql.Append("IndemnityAmount=@IndemnityAmount,");
            strSql.Append("IndemnityDesc=@IndemnityDesc,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("CreateUserid=@CreateUserid,");
            strSql.Append("DimissionId=@DimissionId ");
            strSql.Append(" where DimissionIndemnityId=@DimissionIndemnityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@IndemnityItem", SqlDbType.NVarChar,1024),
					new SqlParameter("@IndemnityAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IndemnityDesc", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserid", SqlDbType.Int,4),
					new SqlParameter("@DimissionIndemnityId", SqlDbType.Int,4),
                    new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = model.IndemnityItem;
            parameters[1].Value = model.IndemnityAmount;
            parameters[2].Value = model.IndemnityDesc;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.CreateUserid;
            parameters[6].Value = model.DimissionIndemnityId;
            parameters[7].Value = model.DimissionId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DimissionIndemnityInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_DimissionIndemnity set ");
            strSql.Append("IndemnityItem=@IndemnityItem,");
            strSql.Append("IndemnityAmount=@IndemnityAmount,");
            strSql.Append("IndemnityDesc=@IndemnityDesc,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("CreateUserid=@CreateUserid,");
            strSql.Append("DimissionId=@DimissionId ");
            strSql.Append(" where DimissionIndemnityId=@DimissionIndemnityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@IndemnityItem", SqlDbType.NVarChar,1024),
					new SqlParameter("@IndemnityAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IndemnityDesc", SqlDbType.NVarChar,1024),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserid", SqlDbType.Int,4),
					new SqlParameter("@DimissionIndemnityId", SqlDbType.Int,4),
                    new SqlParameter("@DimissionId", SqlDbType.Int, 4)};
            parameters[0].Value = model.IndemnityItem;
            parameters[1].Value = model.IndemnityAmount;
            parameters[2].Value = model.IndemnityDesc;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.CreateUserid;
            parameters[6].Value = model.DimissionIndemnityId;
            parameters[7].Value = model.DimissionId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int DimissionIndemnity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sep_DimissionIndemnity ");
            strSql.Append(" where DimissionIndemnityId=@DimissionIndemnityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionIndemnityId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionIndemnity;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string DimissionIndemnitylist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sep_DimissionIndemnity ");
            strSql.Append(" where DimissionIndemnityId in (" + DimissionIndemnitylist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimissionIndemnityInfo GetModel(int DimissionIndemnity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from sep_DimissionIndemnity ");
            strSql.Append(" where DimissionIndemnity=@DimissionIndemnity ");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionIndemnityId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionIndemnity;

            DimissionIndemnityInfo model = new DimissionIndemnityInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
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
            strSql.Append(" FROM sep_DimissionIndemnity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<DimissionIndemnityInfo> GetDimissionIndemnityList(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM sep_DimissionIndemnity WHERE DimissionId=@DimissionId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            List<DimissionIndemnityInfo> dimissionIndemnityList = new List<DimissionIndemnityInfo>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DimissionIndemnityInfo model = new DimissionIndemnityInfo();
                    model.PopupData(dr);
                    dimissionIndemnityList.Add(model);
                }
            }
            return dimissionIndemnityList;
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM sep_DimissionIndemnity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
