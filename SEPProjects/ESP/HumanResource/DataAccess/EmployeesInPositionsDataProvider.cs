using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class EmployeesInPositionsDataProvider
    {
        public EmployeesInPositionsDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(EmployeesInPositionsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_EmployeesInPositions(");
            strSql.Append("UserID,DepartmentPositionID,IsManager,IsActing,DepartmentID,BeginDate)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@DepartmentPositionID,@IsManager,@IsActing,@DepartmentID,@BeginDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
					new SqlParameter("@IsManager", SqlDbType.Bit,1),
					new SqlParameter("@IsActing", SqlDbType.Bit,1),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
                     new SqlParameter("@BeginDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.DepartmentPositionID;
            parameters[2].Value = model.IsManager;
            parameters[3].Value = model.IsActing;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.BeginDate;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(EmployeesInPositionsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_EmployeesInPositions(");
            strSql.Append("UserID,DepartmentPositionID,IsManager,IsActing,DepartmentID,BeginDate)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@DepartmentPositionID,@IsManager,@IsActing,@DepartmentID,@BeginDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
					new SqlParameter("@IsManager", SqlDbType.Bit,1),
					new SqlParameter("@IsActing", SqlDbType.Bit,1),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime) };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.DepartmentPositionID;
            parameters[2].Value = model.IsManager;
            parameters[3].Value = model.IsActing;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.BeginDate;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(EmployeesInPositionsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_EmployeesInPositions set ");
            strSql.Append("IsManager=@IsManager,");
            strSql.Append("IsActing=@IsActing,");
            strSql.Append("DepartmentID=@DepartmentID,BeginDate=@BeginDate");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
					new SqlParameter("@IsManager", SqlDbType.Bit,1),
					new SqlParameter("@IsActing", SqlDbType.Bit,1),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime)    };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.DepartmentPositionID;
            parameters[2].Value = model.IsManager;
            parameters[3].Value = model.IsActing;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.BeginDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateByDepartmentID(EmployeesInPositionsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_EmployeesInPositions set ");
            strSql.Append("IsManager=@IsManager,");
            strSql.Append("IsActing=@IsActing,");
            strSql.Append("DepartmentPositionID=@DepartmentPositionID");
            strSql.Append(" where UserID=@UserID and DepartmentID=@DepartmentID,BeginDate=@BeginDate ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
					new SqlParameter("@IsManager", SqlDbType.Bit,1),
					new SqlParameter("@IsActing", SqlDbType.Bit,1),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime)    
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.DepartmentPositionID;
            parameters[2].Value = model.IsManager;
            parameters[3].Value = model.IsActing;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.BeginDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(EmployeesInPositionsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_EmployeesInPositions set ");
            strSql.Append("IsManager=@IsManager,");
            strSql.Append("IsActing=@IsActing,");
            strSql.Append("DepartmentID=@DepartmentID,BeginDate=@BeginDate");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
					new SqlParameter("@IsManager", SqlDbType.Bit,1),
					new SqlParameter("@IsActing", SqlDbType.Bit,1),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
                    new SqlParameter("@BeginDate", SqlDbType.DateTime)                    
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.DepartmentPositionID;
            parameters[2].Value = model.IsManager;
            parameters[3].Value = model.IsActing;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.BeginDate;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID, int DepartmentPositionID, int DepartmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID and DepartmentID=@DepartmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = DepartmentPositionID;
            parameters[2].Value = DepartmentID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID, int DepartmentPositionID, int DepartmentID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID and DepartmentID=@DepartmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = DepartmentPositionID;
            parameters[2].Value = DepartmentID;

            int res = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        public void Delete(int UserID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = UserID;

            int res = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeesInPositionsInfo GetModel(int UserID, int DepartmentPositionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,DepartmentPositionID,IsManager,IsActing,RowVersion,DepartmentID,BeginDate from sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = DepartmentPositionID;

            EmployeesInPositionsInfo model = new EmployeesInPositionsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString() != "")
                {
                    model.DepartmentPositionID = int.Parse(ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsManager"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsManager"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsManager"].ToString().ToLower() == "true"))
                    {
                        model.IsManager = true;
                    }
                    else
                    {
                        model.IsManager = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsActing"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActing"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActing"].ToString().ToLower() == "true"))
                    {
                        model.IsActing = true;
                    }
                    else
                    {
                        model.IsActing = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RowVersion"].ToString() != "")
                {
                    model.RowVersion = (Byte[])ds.Tables[0].Rows[0]["RowVersion"];
                }
                if (ds.Tables[0].Rows[0]["DepartmentID"].ToString() != "")
                {
                    model.DepartmentID = int.Parse(ds.Tables[0].Rows[0]["DepartmentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["BeginDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public EmployeesInPositionsInfo GetModel(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,DepartmentPositionID,IsManager,IsActing,RowVersion,DepartmentID,BeginDate from sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            EmployeesInPositionsInfo model = new EmployeesInPositionsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString() != "")
                {
                    model.DepartmentPositionID = int.Parse(ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsManager"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsManager"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsManager"].ToString().ToLower() == "true"))
                    {
                        model.IsManager = true;
                    }
                    else
                    {
                        model.IsManager = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsActing"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActing"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActing"].ToString().ToLower() == "true"))
                    {
                        model.IsActing = true;
                    }
                    else
                    {
                        model.IsActing = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RowVersion"].ToString() != "")
                {
                    model.RowVersion = (Byte[])ds.Tables[0].Rows[0]["RowVersion"];
                }
                if (ds.Tables[0].Rows[0]["DepartmentID"].ToString() != "")
                {
                    model.DepartmentID = int.Parse(ds.Tables[0].Rows[0]["DepartmentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["BeginDate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeesInPositionsInfo GetModel(int UserID, int DepartmentPositionID, int DepartmentID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,DepartmentPositionID,IsManager,IsActing,RowVersion,DepartmentID,BeginDate from sep_EmployeesInPositions ");
            strSql.Append(" where UserID=@UserID and DepartmentPositionID=@DepartmentPositionID and DepartmentID=@DepartmentID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentPositionID", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentID",SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = DepartmentPositionID;
            parameters[2].Value = DepartmentID;

            EmployeesInPositionsInfo model = new EmployeesInPositionsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString() != "")
                {
                    model.DepartmentPositionID = int.Parse(ds.Tables[0].Rows[0]["DepartmentPositionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsManager"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsManager"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsManager"].ToString().ToLower() == "true"))
                    {
                        model.IsManager = true;
                    }
                    else
                    {
                        model.IsManager = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsActing"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActing"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActing"].ToString().ToLower() == "true"))
                    {
                        model.IsActing = true;
                    }
                    else
                    {
                        model.IsActing = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RowVersion"].ToString() != "")
                {
                    model.RowVersion = (Byte[])ds.Tables[0].Rows[0]["RowVersion"];
                }
                if (ds.Tables[0].Rows[0]["DepartmentID"].ToString() != "")
                {
                    model.DepartmentID = int.Parse(ds.Tables[0].Rows[0]["DepartmentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginDate"].ToString() != "")
                {
                    model.BeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["BeginDate"].ToString());
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
            strSql.Append("select a.*,e.Code,e.WorkCity,e.WorkCountry,e.WorkAddress,b.lastNameCn+b.firstNameCn as userName,c.level1ID as companyID,c.level1 as companyName,c.level2ID as DepartmentId ,c.level2 as DepartmentName, d.DepartmentPositionName,c.level3 as groupName,c.level3ID as groupID,b.email,f.LevelName ");
            strSql.Append("FROM sep_EmployeesInPositions a inner join sep_users b on a.userid=b.userid ");
            strSql.Append("left join v_Department c on a.departmentId = c.level3Id  ");
            strSql.Append("left join sep_DepartmentPositions d on a.departmentpositionid=d.DepartmentPositionID ");
            strSql.Append("left join sep_Employees e on a.userid = e.userid ");
            strSql.Append("left join sep_DepartmentPositions g on a.DepartmentPositionID= g.DepartmentPositionID ");
            strSql.Append("left join SEP_PositionBase f on f.Id =g.PositionBaseId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int GroupID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,e.Code,e.WorkCity,e.WorkCountry,e.WorkAddress,b.lastNameCn+b.firstNameCn as userName,c.level1ID as companyID,c.level1 as companyName,c.level2ID ,c.level2 as DepartmentName, c.level3 as groupName,c.level3ID as groupID,b.email ");
            strSql.Append("FROM sep_EmployeesInPositions a inner join sep_users b on a.userid=b.userid ");
            strSql.Append("left join v_Department c on a.departmentId = c.level3Id  ");
            strSql.Append("left join sep_Employees e on e.userid=a.userid ");
            strSql.Append(" where  (e.status=1 or e.status =3) and a.departmentid=" + GroupID.ToString());

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得用户的团队ID数组
        /// </summary>
        /// <param name="userids">用户ID字符串</param>
        /// <returns></returns>
        public int[] GetUserGroup(string userids)
        {
            int[] deps;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select departmentid FROM sep_EmployeesInPositions ");

            if (userids.Trim() != "")
            {
                strSql.Append(" where userid in (" + userids + ") ");
            }
            strSql.Append(" group by departmentid");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                deps = new int[ds.Tables[0].Rows.Count];
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["departmentid"].ToString() != "")
                    {
                        deps[n] = int.Parse(ds.Tables[0].Rows[n]["departmentid"].ToString());
                    }
                }
            }
            else
            {
                deps = new int[0];
            }
            return deps;
        }

        /// <summary>
        /// 获得用户的部门ID数组
        /// </summary>
        /// <param name="userids">用户ID字符串</param>
        /// <returns></returns>
        public int[] GetUserDepartmentID(string userids)
        {
            int[] deps;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.level2ID  as DepartmentID ");
            strSql.Append("FROM sep_EmployeesInPositions a inner join sep_users b on a.userid=b.userid ");
            strSql.Append("left join v_Department c on a.departmentId = c.level3Id  ");
            strSql.Append("left join sep_DepartmentPositions d on a.departmentpositionid=d.DepartmentPositionID ");

            if (userids.Trim() != "")
            {
                strSql.Append(" where a.userid in (" + userids + ") ");
            }
            strSql.Append(" group by c.level2ID");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                deps = new int[ds.Tables[0].Rows.Count];
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["DepartmentID"].ToString() != "")
                    {
                        deps[n] = int.Parse(ds.Tables[0].Rows[n]["DepartmentID"].ToString());
                    }
                }
            }
            else
            {
                deps = new int[0];
            }
            return deps;
        }

        /// <summary>
        /// 获得全公司现有的所有职位
        /// </summary>
        /// <returns></returns>
        public DataSet GetPositionList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select distinct b.departmentpositionname from sep_employeesinpositions a 
                            join sep_DepartmentPositions  b on a.departmentpositionid =b.departmentpositionid
                            join sep_employees e on a.userid = e.userid 
                            join sep_Departments d on a.departmentid = d.departmentid
                            where e.status != 5 and d.departmentname not like '%作废%' 
                            order by departmentpositionname");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 通过部门id获得本部门的人数
        /// </summary>
        /// <param name="level2id"></param>
        /// <returns></returns>
        public int GetJobNumberByLevel2Id(int level2id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.departmentid from sep_employeesinpositions a
join sep_employees e on a.userid = e.userid 
join v_department v on a.departmentid = v.level3Id 
where e.status != 5 and v.level3 not like '%作废%' and v.level2id="+ level2id);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 通过公司id获得本公司的人数
        /// </summary>
        /// <param name="level1id"></param>
        /// <returns></returns>
        public int GetJobNumberByLevel1Id(int level1id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.departmentid from sep_employeesinpositions a
join sep_employees e on a.userid = e.userid 
join v_department v on a.departmentid = v.level3Id 
where e.status != 5 and v.level3 not like '%作废%' and v.level1id=" + level1id);
            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count; 
        }

        /// <summary>
        /// 通过部门id和职位名字获得该部门、职位下的人数
        /// </summary>
        /// <param name="level2id"></param>
        /// <param name="departmentpositionname"></param>
        /// <returns></returns>
        public int GetJobNumberBylv2ordp(int level2id, string departmentpositionname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.departmentid from sep_employeesinpositions a
join sep_employees e on a.userid = e.userid 
join v_department v on a.departmentid = v.level3Id 
join sep_DepartmentPositions  b on a.departmentpositionid =b.departmentpositionid
where e.status != 5 and v.level3 not like '%作废%' and
 v.level2id=" + level2id+" and DepartmentPositionName = '" + departmentpositionname+"'");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 通过公司id和职位名字获得该公司、职位下的人数
        /// </summary>
        /// <param name="level1id"></param>
        /// <param name="departmentpositionname"></param>
        /// <returns></returns>
        public int GetJobNumberBylv1ordp(int level1id, string departmentpositionname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.departmentid from sep_employeesinpositions a
join sep_employees e on a.userid = e.userid 
join v_department v on a.departmentid = v.level3Id 
join sep_DepartmentPositions  b on a.departmentpositionid =b.departmentpositionid
where e.status != 5 and v.level3 not like '%作废%' and
 v.level1id=" + level1id + " and DepartmentPositionName = '" + departmentpositionname+"'");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows.Count;
        }

        #endregion  成员方法
    }
}
