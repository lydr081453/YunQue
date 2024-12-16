using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    public class TimeSheetDataProvider
    {
        public TimeSheetDataProvider()
        { }


        public int Add(TimeSheetInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TimeSheetInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_TimeSheet(");
            strSql.Append("Hours,ProjectId,ProjectCode,ProjectName,UserId,UserName,WorkItem,CreateDate,CategoryId,CategoryName,FileUrl,SubmitDate,IP,Status,CommitId,TypeId,IsChecked,IsBillable)");
            strSql.Append(" values (");
            strSql.Append("@Hours,@ProjectId,@ProjectCode,@ProjectName,@UserId,@UserName,@WorkItem,@CreateDate,@CategoryId,@CategoryName,@FileUrl,@SubmitDate,@IP,@Status,@CommitId,@TypeId,@IsChecked,@IsBillable)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Hours", SqlDbType.Decimal,9),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,500),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkItem", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CommitId", SqlDbType.Int,4),
					new SqlParameter("@TypeId", SqlDbType.NChar,10),
					new SqlParameter("@IsChecked", SqlDbType.Bit,1),
                    new SqlParameter("@IsBillable", SqlDbType.Bit,1)                
                                        };

            parameters[0].Value = model.Hours;
            parameters[1].Value = model.ProjectId;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.UserId;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.WorkItem;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.CategoryId;
            parameters[9].Value = model.CategoryName;
            parameters[10].Value = model.FileUrl;
            parameters[11].Value = model.SubmitDate;
            parameters[12].Value = model.IP;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.CommitId;
            parameters[15].Value = model.TypeId;
            parameters[16].Value = model.IsChecked;
            parameters[17].Value = model.IsBillable;

            

            object obj = null;
            if(trans != null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public bool Update(TimeSheetInfo model)
        {
            return Update(model, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TimeSheetInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_TimeSheet set ");
            strSql.Append("Hours=@Hours,");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("WorkItem=@WorkItem,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("FileUrl=@FileUrl,");
            strSql.Append("SubmitDate=@SubmitDate,");
            strSql.Append("IP=@IP,");
            strSql.Append("Status=@Status,");
            strSql.Append("CommitId=@CommitId,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("IsChecked=@IsChecked,");
            strSql.Append("IsBillable=@IsBillable");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {

					new SqlParameter("@Hours", SqlDbType.Decimal,9),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,500),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkItem", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CommitId", SqlDbType.Int,4),
					new SqlParameter("@TypeId", SqlDbType.NChar,10),
					new SqlParameter("@IsChecked", SqlDbType.Bit,1),
                    new SqlParameter("@IsBillable", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4) 
                                        };

            parameters[0].Value = model.Hours;
            parameters[1].Value = model.ProjectId;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.UserId;
            parameters[5].Value = model.UserName;
            parameters[6].Value = model.WorkItem;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.CategoryId;
            parameters[9].Value = model.CategoryName;
            parameters[10].Value = model.FileUrl;
            parameters[11].Value = model.SubmitDate;
            parameters[12].Value = model.IP;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.CommitId;
            parameters[15].Value = model.TypeId;
            parameters[16].Value = model.IsChecked;
            parameters[17].Value = model.IsBillable;
            parameters[18].Value = model.Id;

            int rows = 0;
            if (trans == null)
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
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
        public bool Delete(int Id,SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheet ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheet ");
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

        public bool DeleteLongHoliday(string serialNo,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AD_TimeSheet ");
            strSql.Append(" where typeid=" + (int)ESP.Administrative.Common.TimeSheetType.Holiday + " and commitid  in (select id from ad_timesheetcommit where serialNo = '" + serialNo + "')  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans);
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
        public TimeSheetInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from AD_TimeSheet ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            TimeSheetInfo model = new TimeSheetInfo();
            return CBO.FillObject<TimeSheetInfo>( DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<TimeSheetInfo> GetList(string strWhere)
        {
            return GetList(strWhere, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TimeSheetInfo> GetList(string strWhere,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM AD_TimeSheet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if(trans != null)
                return CBO.FillCollection<TimeSheetInfo>(DbHelperSQL.Query(strSql.ToString(),trans));
            else
                return CBO.FillCollection<TimeSheetInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<TimeSheetInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM AD_TimeSheet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return  CBO.FillCollection<TimeSheetInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM AD_TimeSheet ");
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
        public List<TimeSheetInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from AD_TimeSheet T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return  CBO.FillCollection<TimeSheetInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

    }
}