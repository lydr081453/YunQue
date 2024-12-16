using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Finance.DataAccess
{
    internal class ProjectScheduleDataProvider : ESP.Finance.IDataAccess.IProjectScheduleDataProvider
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.ProjectScheduleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ProjectSchedule(");
            strSql.Append("ProjectID,YearValue,monthValue,MonthPercent,Fee,OperationFee)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@YearValue,@monthValue,@MonthPercent,@Fee,@OperationFee)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@YearValue", SqlDbType.Int,4),
					new SqlParameter("@monthValue", SqlDbType.Int,4),
					new SqlParameter("@MonthPercent", SqlDbType.Decimal,20),
					new SqlParameter("@Fee", SqlDbType.Decimal,20),
                    new SqlParameter("@OperationFee",SqlDbType.Decimal,20)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.YearValue;
            parameters[2].Value = model.monthValue;
            parameters[3].Value = model.MonthPercent;
            parameters[4].Value = model.Fee;
            parameters[5].Value = model.OperationFee;

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
        public int Update(Entity.ProjectScheduleInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ProjectSchedule set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("YearValue=@YearValue,");
            strSql.Append("monthValue=@monthValue,");
            strSql.Append("MonthPercent=@MonthPercent,");
            strSql.Append("Fee=@Fee,OperationFee=@OperationFee");
            strSql.Append(" where ScheduleID=@ScheduleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScheduleID", SqlDbType.Int,4),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@YearValue", SqlDbType.Int,4),
					new SqlParameter("@monthValue", SqlDbType.Int,4),
					new SqlParameter("@MonthPercent", SqlDbType.Decimal,20),
					new SqlParameter("@Fee", SqlDbType.Decimal,20),
                    new SqlParameter("@OperationFee", SqlDbType.Decimal,20)
                                        };
            parameters[0].Value = model.ScheduleID;
            parameters[1].Value = model.ProjectID;
            parameters[2].Value = model.YearValue;
            parameters[3].Value = model.monthValue;
            parameters[4].Value = model.MonthPercent;
            parameters[5].Value = model.Fee;
            parameters[6].Value = model.OperationFee;


            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ScheduleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectSchedule ");
            strSql.Append(" where ScheduleID=@ScheduleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScheduleID", SqlDbType.Int,4)};
            parameters[0].Value = ScheduleID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Delete(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ProjectSchedule ");
            strSql.Append(" where  " + condition);


            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.ProjectScheduleInfo GetModel(int ScheduleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ScheduleID,ProjectID,YearValue,monthValue,MonthPercent,Fee,OperationFee from F_ProjectSchedule ");
            strSql.Append(" where ScheduleID=@ScheduleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScheduleID", SqlDbType.Int,4)};
            parameters[0].Value = ScheduleID;

            return ESP.Finance.Utility.CBO.FillObject<Entity.ProjectScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.ProjectScheduleInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ScheduleID,ProjectID,YearValue,monthValue,MonthPercent,Fee,OperationFee ");
            strSql.Append(" FROM F_ProjectSchedule ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by YearValue,monthValue");
            return ESP.Finance.Utility.CBO.FillCollection<Entity.ProjectScheduleInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
    }
}
