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
    internal class MediumForProjectDataProvider : ESP.Finance.IDataAccess.IMediumForProjectDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_MediumForProject");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.MediumForProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_MediumForProject(");
            strSql.Append("ProjectID,MediaID,CreatedDate,CreatedUserID,IsDel,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@AreaCode,@AreaName,@SearchCode,@Description,@Others)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int),
					new SqlParameter("@MediaID", SqlDbType.Int),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int)};
            parameters[0].Value =model.ProjectID;
            parameters[1].Value =model.MediaID;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = 0;
            parameters[5].Value = model.ModifiedUserID;
            parameters[6].Value = model.CreatedUserID;

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
        public int Update(ESP.Finance.Entity.MediumForProjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_MediumForProject set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("MediaID=@MediaID,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,8),
					new SqlParameter("@ProjectID", SqlDbType.Int),
					new SqlParameter("@MediaID", SqlDbType.Int),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int)};
            parameters[0].Value =model.ID;
            parameters[1].Value =model.ProjectID;
            parameters[2].Value =model.MediaID;
            parameters[3].Value =model.IsDel;
            parameters[4].Value =DateTime.Now;
            parameters[5].Value =model.ModifiedUserID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_MediumForProject ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.MediumForProjectInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_MediumForProject ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,8)};
            parameters[0].Value = id;

            return CBO.FillObject<MediumForProjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.MediumForProjectInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_MediumForProject ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_Area>(DbHelperSQL.ExecuteReader(strSql.ToString(),ps));
            //}
            return CBO.FillCollection<ESP.Finance.Entity.MediumForProjectInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
    }
}