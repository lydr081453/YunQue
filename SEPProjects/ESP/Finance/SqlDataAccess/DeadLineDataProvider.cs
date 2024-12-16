using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_DeadLine。
    /// </summary>
    internal class DeadLineDataProvider : ESP.Finance.IDataAccess.IDeadLineDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DeadLineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_DeadLine");
            strSql.Append(" where DeadLineID=@DeadLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLineID", SqlDbType.Int,4)};
            parameters[0].Value = DeadLineID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.DeadLineInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_DeadLine(");
            strSql.Append(@"DeadLine,DeadLineYear,DeadLineMonth,DeadLineDay,CreateUserID,CreateUserCode,CreateUserName,CreateUserEmpName,ExpenseDeadLine,ExpenseCommitDeadLine,ExpenseAuditDeadLine,Status,ProjectDeadLine,ProjectDeadLineYear,ProjectDeadLineMonth,ProjectDeadLineDay,DeadLine2,DeadLineYear2,DeadLineMonth2,DeadLineDay2,ExpenseDeadLine2,ExpenseCommitDeadLine2,ExpenseAuditDeadLine2,SalaryDate)");

            strSql.Append(" values (");
            strSql.Append(@"@DeadLine,@DeadLineYear,@DeadLineMonth,@DeadLineDay,@CreateUserID,@CreateUserCode,@CreateUserName,@CreateUserEmpName,@ExpenseDeadLine,@ExpenseCommitDeadLine,@ExpenseAuditDeadLine,@Status,@ProjectDeadLine,@ProjectDeadLineYear,@ProjectDeadLineMonth,@ProjectDeadLineDay,@DeadLine2,@DeadLineYear2,@DeadLineMonth2,@DeadLineDay2,@ExpenseDeadLine2,@ExpenseCommitDeadLine2,@ExpenseAuditDeadLine2,@SalaryDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLine", SqlDbType.DateTime),
					new SqlParameter("@DeadLineYear", SqlDbType.Int,4),
					new SqlParameter("@DeadLineMonth", SqlDbType.Int,4),
					new SqlParameter("@DeadLineDay", SqlDbType.Int,4),
                    new SqlParameter("@CreateUserID", SqlDbType.Int,4),
                    new SqlParameter("@CreateUserCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateUserEmpName",SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpenseDeadLine", SqlDbType.DateTime),
                    new SqlParameter("@ExpenseCommitDeadLine",SqlDbType.DateTime),
                    new SqlParameter("@ExpenseAuditDeadLine",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ProjectDeadLine",SqlDbType.DateTime),
                    new SqlParameter("@ProjectDeadLineYear", SqlDbType.Int,4),
					new SqlParameter("@ProjectDeadLineMonth", SqlDbType.Int,4),
					new SqlParameter("@ProjectDeadLineDay", SqlDbType.Int,4),
                    new SqlParameter("@DeadLine2", SqlDbType.DateTime),
					new SqlParameter("@DeadLineYear2", SqlDbType.Int,4),
					new SqlParameter("@DeadLineMonth2", SqlDbType.Int,4),
					new SqlParameter("@DeadLineDay2", SqlDbType.Int,4),
                    new SqlParameter("@ExpenseDeadLine2", SqlDbType.DateTime),
                    new SqlParameter("@ExpenseCommitDeadLine2",SqlDbType.DateTime),
                    new SqlParameter("@ExpenseAuditDeadLine2",SqlDbType.DateTime),
                    new SqlParameter("@SalaryDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.DeadLine;
            parameters[1].Value = model.DeadLineYear;
            parameters[2].Value = model.DeadLineMonth;
            parameters[3].Value = model.DeadLineDay;
            parameters[4].Value = model.CreateUserID;
            parameters[5].Value = model.CreateUserCode;
            parameters[6].Value = model.CreateUserName;
            parameters[7].Value = model.CreateUserEmpName;
            parameters[8].Value = model.ExpenseDeadLine;
            parameters[9].Value = model.ExpenseCommitDeadLine;
            parameters[10].Value = model.ExpenseAuditDeadLine;
            parameters[11].Value = model.Status;

            parameters[12].Value = model.ProjectDeadLine;
            parameters[13].Value = model.ProjectDeadLineYear;
            parameters[14].Value = model.ProjectDeadLineMonth;
            parameters[15].Value = model.ProjectDeadLineDay;

            parameters[16].Value = model.DeadLine2;
            parameters[17].Value = model.DeadLineYear2;
            parameters[18].Value = model.DeadLineMonth2;
            parameters[19].Value = model.DeadLineDay2;
            parameters[20].Value = model.ExpenseDeadLine2;
            parameters[21].Value = model.ExpenseCommitDeadLine2;
            parameters[22].Value = model.ExpenseAuditDeadLine2;
            parameters[23].Value = model.SalaryDate;

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
        public int Update(ESP.Finance.Entity.DeadLineInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_DeadLine set ");
            strSql.Append("DeadLine=@DeadLine,");
            strSql.Append("DeadLineYear=@DeadLineYear,");
            strSql.Append("DeadLineMonth=@DeadLineMonth,");
            strSql.Append("DeadLineDay=@DeadLineDay,");
            strSql.Append("CreateUserID=@CreateUserID,");
            strSql.Append("CreateUserCode=@CreateUserCode,");
            strSql.Append("CreateUserName=@CreateUserName,");
            strSql.Append("CreateUserEmpName=@CreateUserEmpName,");
            strSql.Append("ExpenseDeadLine=@ExpenseDeadLine,ExpenseCommitDeadLine=@ExpenseCommitDeadLine,ExpenseAuditDeadLine=@ExpenseAuditDeadLine,Status=@Status, ");
            strSql.Append("ProjectDeadLine=@ProjectDeadLine,");
            strSql.Append("ProjectDeadLineYear=@ProjectDeadLineYear,");
            strSql.Append("ProjectDeadLineMonth=@ProjectDeadLineMonth,");
            strSql.Append("ProjectDeadLineDay=@ProjectDeadLineDay,");
            strSql.Append("DeadLine2=@DeadLine2,DeadLineYear2=@DeadLineYear2,DeadLineMonth2=@DeadLineMonth2,DeadLineDay2=@DeadLineDay2,ExpenseDeadLine2=@ExpenseDeadLine2,ExpenseCommitDeadLine2=@ExpenseCommitDeadLine2,ExpenseAuditDeadLine2=@ExpenseAuditDeadLine2,SalaryDate=@SalaryDate");
            strSql.Append(" where DeadLineID=@DeadLineID");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLineID", SqlDbType.Int,4),
					new SqlParameter("@DeadLine", SqlDbType.DateTime),
					new SqlParameter("@DeadLineYear", SqlDbType.Int,4),
					new SqlParameter("@DeadLineMonth", SqlDbType.Int,4),
					new SqlParameter("@DeadLineDay", SqlDbType.Int,4),
                    new SqlParameter("@CreateUserID", SqlDbType.Int,4),
                    new SqlParameter("@CreateUserCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateUserEmpName",SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpenseDeadLine", SqlDbType.DateTime),
                    new SqlParameter("@ExpenseCommitDeadLine", SqlDbType.DateTime),
                    new SqlParameter("@ExpenseAuditDeadLine", SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.Int,4),
                    new SqlParameter("@ProjectDeadLine", SqlDbType.DateTime),
					new SqlParameter("@ProjectDeadLineYear", SqlDbType.Int,4),
					new SqlParameter("@ProjectDeadLineMonth", SqlDbType.Int,4),
					new SqlParameter("@ProjectDeadLineDay", SqlDbType.Int,4),
                    
                    new SqlParameter("@DeadLine2", SqlDbType.DateTime),
					new SqlParameter("@DeadLineYear2", SqlDbType.Int,4),
					new SqlParameter("@DeadLineMonth2", SqlDbType.Int,4),
					new SqlParameter("@DeadLineDay2", SqlDbType.Int,4),
                    new SqlParameter("@ExpenseDeadLine2", SqlDbType.DateTime),
                    new SqlParameter("@ExpenseCommitDeadLine2",SqlDbType.DateTime),
                    new SqlParameter("@ExpenseAuditDeadLine2",SqlDbType.DateTime),
                    new SqlParameter("@SalaryDate",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.DeadLineID;
            parameters[1].Value = model.DeadLine;
            parameters[2].Value = model.DeadLineYear;
            parameters[3].Value = model.DeadLineMonth;
            parameters[4].Value = model.DeadLineDay;
            parameters[5].Value = model.CreateUserID;
            parameters[6].Value = model.CreateUserCode;
            parameters[7].Value = model.CreateUserName;
            parameters[8].Value = model.CreateUserEmpName;
            parameters[9].Value = model.ExpenseDeadLine;
            parameters[10].Value = model.ExpenseCommitDeadLine;
            parameters[11].Value = model.ExpenseAuditDeadLine;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.ProjectDeadLine;
            parameters[14].Value = model.ProjectDeadLineYear;
            parameters[15].Value = model.ProjectDeadLineMonth;
            parameters[16].Value = model.ProjectDeadLineDay;

            parameters[17].Value = model.DeadLine2;
            parameters[18].Value = model.DeadLineYear2;
            parameters[19].Value = model.DeadLineMonth2;
            parameters[20].Value = model.DeadLineDay2;
            parameters[21].Value = model.ExpenseDeadLine2;
            parameters[22].Value = model.ExpenseCommitDeadLine2;
            parameters[23].Value = model.ExpenseAuditDeadLine2;
            parameters[24].Value = model.SalaryDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DeadLineID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_DeadLine ");
            strSql.Append(" where DeadLineID=@DeadLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLineID", SqlDbType.Int,4)};
            parameters[0].Value = DeadLineID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.DeadLineInfo GetModel(int DeadLineID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(@" * ");
            strSql.Append(" FROM F_DeadLine ");
            strSql.Append(" where DeadLineID=@DeadLineID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLineID", SqlDbType.Int,4)};
            parameters[0].Value = DeadLineID;

            return CBO.FillObject<ESP.Finance.Entity.DeadLineInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

       public ESP.Finance.Entity.DeadLineInfo GetMonthModel(int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(@" * ");
            strSql.Append(" FROM F_DeadLine ");
            strSql.Append(" where DeadLineYear=@DeadLineYear and DeadLineMonth=@DeadLineMonth ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeadLineYear", SqlDbType.Int,4),
                    new SqlParameter("@DeadLineMonth", SqlDbType.Int,4)
                                        };
            parameters[0].Value = year;
            parameters[1].Value = month;

            return CBO.FillObject<ESP.Finance.Entity.DeadLineInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.DeadLineInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(@" * ");
            strSql.Append(" FROM F_DeadLine ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by DeadLine desc");
            return CBO.FillCollection<ESP.Finance.Entity.DeadLineInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
    }
}
