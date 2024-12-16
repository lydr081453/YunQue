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
    /// 数据访问类F_Area。
    /// </summary>
    internal class AreaDataProvider : ESP.Finance.IDataAccess.IAreaDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AreaID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Area");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.AreaInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Area(");
            strSql.Append("AreaCode,AreaName,SearchCode,Description,Others)");
            strSql.Append(" values (");
            strSql.Append("@AreaCode,@AreaName,@SearchCode,@Description,@Others)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@SearchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),
					new SqlParameter("@Others", SqlDbType.NVarChar,100)};
            parameters[0].Value =model.AreaCode;
            parameters[1].Value =model.AreaName;
            parameters[2].Value =model.SearchCode;
            parameters[3].Value =model.Description;
            parameters[4].Value =model.Others;

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
        public int Update(ESP.Finance.Entity.AreaInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Area set ");
            strSql.Append("AreaCode=@AreaCode,");
            strSql.Append("AreaName=@AreaName,");
            strSql.Append("SearchCode=@SearchCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("Others=@Others");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@AreaCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@SearchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@Description", SqlDbType.NVarChar,100),
					new SqlParameter("@Others", SqlDbType.NVarChar,100)};
            parameters[0].Value =model.AreaID;
            parameters[1].Value =model.AreaCode;
            parameters[2].Value =model.AreaName;
            parameters[3].Value =model.SearchCode;
            parameters[4].Value =model.Description;
            parameters[5].Value =model.Others;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int AreaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Area ");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.AreaInfo GetModel(int AreaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AreaID,AreaCode,AreaName,SearchCode,Description,Others from F_Area ");
            strSql.Append(" where AreaID=@AreaID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4)};
            parameters[0].Value = AreaID;

            return CBO.FillObject<AreaInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<AreaInfo> GetList(string term,List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AreaID,AreaCode,AreaName,SearchCode,Description,Others ");
            strSql.Append(" FROM F_Area ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_Area>(DbHelperSQL.ExecuteReader(strSql.ToString(),ps));
            //}
            return CBO.FillCollection < AreaInfo >( DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion  成员方法
    }
}