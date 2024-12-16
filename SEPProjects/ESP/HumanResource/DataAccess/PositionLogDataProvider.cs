using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class PositionLogDataProvider
    {
        public PositionLogDataProvider()
        { }


        public int Add(PositionLogInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PositionLogInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sep_PositionLog(");
            strSql.Append("UserId,UserName,UserCode,DepartmentId,DepartmentName,PositionId,PositionName,PositionBaseId,PositionBaseName,LevelId,LevelName,BeginDate,EndDate,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserName,@UserCode,@DepartmentId,@DepartmentName,@PositionId,@PositionName,@PositionBaseId,@PositionBaseName,@LevelId,@LevelName,@BeginDate,@EndDate,@CreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionId", SqlDbType.Int,4),
					new SqlParameter("@PositionName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionBaseId", SqlDbType.Int,4),
					new SqlParameter("@PositionBaseName", SqlDbType.NVarChar,50),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@LevelName", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.PositionId;
            parameters[6].Value = model.PositionName;
            parameters[7].Value = model.PositionBaseId;
            parameters[8].Value = model.PositionBaseName;
            parameters[9].Value = model.LevelId;
            parameters[10].Value = model.LevelName;
            parameters[11].Value = model.BeginDate;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.CreateDate;

            object obj;
            if(trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public bool Update(PositionLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sep_PositionLog set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("PositionId=@PositionId,");
            strSql.Append("PositionName=@PositionName,");
            strSql.Append("PositionBaseId=@PositionBaseId,");
            strSql.Append("PositionBaseName=@PositionBaseName,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("LevelName=@LevelName,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionId", SqlDbType.Int,4),
					new SqlParameter("@PositionName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionBaseId", SqlDbType.Int,4),
					new SqlParameter("@PositionBaseName", SqlDbType.NVarChar,50),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@LevelName", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.PositionId;
            parameters[6].Value = model.PositionName;
            parameters[7].Value = model.PositionBaseId;
            parameters[8].Value = model.PositionBaseName;
            parameters[9].Value = model.LevelId;
            parameters[10].Value = model.LevelName;
            parameters[11].Value = model.BeginDate;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.Id;

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

        public bool Update(PositionLogInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sep_PositionLog set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("PositionId=@PositionId,");
            strSql.Append("PositionName=@PositionName,");
            strSql.Append("PositionBaseId=@PositionBaseId,");
            strSql.Append("PositionBaseName=@PositionBaseName,");
            strSql.Append("LevelId=@LevelId,");
            strSql.Append("LevelName=@LevelName,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionId", SqlDbType.Int,4),
					new SqlParameter("@PositionName", SqlDbType.NVarChar,50),
					new SqlParameter("@PositionBaseId", SqlDbType.Int,4),
					new SqlParameter("@PositionBaseName", SqlDbType.NVarChar,50),
					new SqlParameter("@LevelId", SqlDbType.Int,4),
					new SqlParameter("@LevelName", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.PositionId;
            parameters[6].Value = model.PositionName;
            parameters[7].Value = model.PositionBaseId;
            parameters[8].Value = model.PositionBaseName;
            parameters[9].Value = model.LevelId;
            parameters[10].Value = model.LevelName;
            parameters[11].Value = model.BeginDate;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parameters);
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sep_PositionLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sep_PositionLog ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public PositionLogInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,UserName,UserCode,DepartmentId,DepartmentName,PositionId,PositionName,PositionBaseId,PositionBaseName,LevelId,LevelName,BeginDate,EndDate,CreateDate from Sep_PositionLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            PositionLogInfo model = new PositionLogInfo();
            return CBO.FillObject<PositionLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public PositionLogInfo GetModel(int UserId, int DepartmentPositionId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,UserName,UserCode,DepartmentId,DepartmentName,PositionId,PositionName,PositionBaseId,PositionBaseName,LevelId,LevelName,BeginDate,EndDate,CreateDate from Sep_PositionLog ");
            strSql.Append(" where EndDate is null and UserId=@UserId and PositionId=@PositionId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@PositionId",SqlDbType.Int,4)
			};
            parameters[0].Value = UserId;
            parameters[1].Value = DepartmentPositionId;

            PositionLogInfo model = new PositionLogInfo();
            return CBO.FillObject<PositionLogInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PositionLogInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,UserName,UserCode,DepartmentId,DepartmentName,PositionId,PositionName,PositionBaseId,PositionBaseName,LevelId,LevelName,BeginDate,EndDate,CreateDate ");
            strSql.Append(" FROM Sep_PositionLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by beginDate asc " );
            return CBO.FillCollection<PositionLogInfo>( DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<PositionLogInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,UserId,UserName,UserCode,DepartmentId,DepartmentName,PositionId,PositionName,PositionBaseId,PositionBaseName,LevelId,LevelName,BeginDate,EndDate,CreateDate ");
            strSql.Append(" FROM Sep_PositionLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return CBO.FillCollection<PositionLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Sep_PositionLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public List<PositionLogInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Sep_PositionLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return CBO.FillCollection<PositionLogInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
    }
}
