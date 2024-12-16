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
    /// 数据访问类F_ProjectMedia。
    /// </summary>
    internal class ProjectMediaDataProvider : ESP.Finance.IDataAccess.IProjectMediaDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ProjectMediaID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ProjectMedia");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ProjectMediaID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public int Add(ESP.Finance.Entity.ProjectMediaInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectMediaInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectMedia(");
            strSql.Append("ProjectId, SupplierId, CostRate, Recharge, BeginDate, EndDate)");
            strSql.Append(" values (");
            strSql.Append("@ProjectId, @SupplierId, @CostRate, @Recharge, @BeginDate, @EndDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@CostRate", SqlDbType.Decimal,9),
					new SqlParameter("@Recharge", SqlDbType.Decimal,9),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.CostRate;
            parameters[3].Value = model.Recharge;
            parameters[4].Value = model.BeginDate;
            parameters[5].Value = model.EndDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(ESP.Finance.Entity.ProjectMediaInfo model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ProjectMediaInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ProjectMedia set ");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("CostRate=@CostRate,");
            strSql.Append("Recharge=@Recharge,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@CostRate", SqlDbType.Decimal,9),
					new SqlParameter("@Recharge", SqlDbType.Decimal,9),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value =model.Id;
            parameters[1].Value = model.ProjectId;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.CostRate;
            parameters[4].Value = model.Recharge;
            parameters[5].Value = model.BeginDate;
            parameters[6].Value = model.EndDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ProjectMediaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectMedia ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = ProjectMediaID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectMediaInfo GetModel(int ProjectMediaID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id, ProjectId, SupplierId, CostRate, Recharge, BeginDate, EndDate from F_ProjectMedia ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = ProjectMediaID;

            return CBO.FillObject<ProjectMediaInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ProjectMediaInfo> GetList(string term,List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, ProjectId, SupplierId, CostRate, Recharge, BeginDate, EndDate ");
            strSql.Append(" FROM F_ProjectMedia ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_ProjectMedia>(DbHelperSQL.ExecuteReader(strSql.ToString(),ps));
            //}
            return CBO.FillCollection < ProjectMediaInfo >( DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion  成员方法
    }
}