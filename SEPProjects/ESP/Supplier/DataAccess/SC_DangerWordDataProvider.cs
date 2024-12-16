using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_DangerWordDataProvider
    {
        public SC_DangerWordDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 添加一条数据
        /// </summary>
        public void Add(SC_DangerWord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into SC_DangerWord(Word) Values(@Word) ");
            SqlParameter[] parameters = {
					new SqlParameter("@Word", SqlDbType.NVarChar)};
            parameters[0].Value = model.Word;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_DangerWord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_DangerWord set ");
            strSql.Append("Word=@Word,");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Word", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Word;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_DangerWord ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_DangerWord GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_DangerWord ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<SC_DangerWord>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_DangerWord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_DangerWord> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_DangerWord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_DangerWord>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_DangerWord> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_DangerWord>(GetList(""));
        }

        #endregion  成员方法
    }
}
