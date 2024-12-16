using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;

namespace ESP.Administrative.BusinessLogic
{
    public class ApproveLogManager
    {
        private ApproveLogDataProvider dal = new ApproveLogDataProvider();
        public ApproveLogManager()
        { }
        
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(ApproveLogInfo model, SqlConnection conn, SqlTransaction trans)
		{
            return dal.Add(model, conn, trans);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ApproveLogInfo model)
        {
            return dal.Add(model);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(ApproveLogInfo model, SqlConnection conn, SqlTransaction trans)
		{
           return dal.Update(model, conn, trans);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ApproveLogInfo model)
        {
            return dal.Update(model);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ID, SqlConnection conn, SqlTransaction trans)
		{
           return dal.Delete(ID, conn, trans);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ApproveLogInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ApproveLogInfo GetModel(int approveID,int approveDateID)
        {
            return dal.GetModel(approveID, approveDateID);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return dal.GetList("");
        }

        /// <summary>
        /// 获得用户待审批考勤事由信息列表
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <param name="single">考勤事由单据类型</param>
        /// <returns>返回一个带审批考勤事由列表</returns>
        public List<MattersInfo> GetWaitApproveList(int UserID, Status.MattersSingle single)
        {
            List<MattersInfo> list = new List<MattersInfo>();
            DataSet ds = new DataSet();
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(@"select a.*, m.* from ad_approvelog a left join ad_Matters m on a.approvedateid=m.id 
                            where a.approvestate=0 and a.approvetype=" + ((int)single) + " and a.approveID=" + UserID + "");
            ds = dal.GetWaitApproveList(strbuild.ToString());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                MattersInfo mattersModel = new MattersInfo();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    mattersModel.PopupData(dr);
                    list.Add(mattersModel);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得用户待审批考勤事由信息列表
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回一个带审批考勤事由列表</returns>
        public DataSet GetWaitApproveList(string sqlStr)
        {
            DataSet ds = new DataSet();
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append("select * from ad_v_WaitApprove where 1=1 ");
            if (!string.IsNullOrEmpty(sqlStr))
            {
                strbuild.Append(sqlStr);
            }
            ds = dal.GetWaitApproveList(strbuild.ToString());
            return ds;
        }

        /// <summary>
        /// 获得用户待审批考勤事由信息列表
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>返回一个带审批考勤事由列表</returns>
        public DataSet GetApprovedList(string sqlStr)
        {
            DataSet ds = new DataSet();
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append("SELECT * FROM AD_V_ApprovedInfo WHERE 1=1 ");
            if (!string.IsNullOrEmpty(sqlStr))
            {
                strbuild.Append(sqlStr);
            }
            ds = dal.GetApprovedList(strbuild.ToString());
            return ds;
        }

        /// <summary>
        /// 获得用户月审列表
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetApproveList(string approveid, int state, int Year, int Month, string strwhere, List<SqlParameter> parameter)
        {
            return dal.GetApproveList(approveid, state, Year, Month, strwhere, parameter);
        }

        public DataSet GetNewApproveList(string adminId, int state, int Year, int Month, string strwhere, List<SqlParameter> parameter)
        {
            return dal.GetNewApproveList(adminId, state, Year, Month, strwhere, parameter);
        }

        /// <summary>
        /// 获得用户导出信息
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetExportApproveList(string approveid, int state, int year, int month)
        {
            return dal.GetExportApproveList(approveid, state, year, month);
        }

        /// <summary>
        /// 获取用户已经审批通过的考勤统计信息
        /// </summary>
        /// <param name="approveid">审批人ID</param>        
        /// <param name="state">等待总监审批：2；等待考勤统计员审批：3；等待HRAdmin审批：4；等待团队经理审批：5； 等待考勤管理员审批：6；审批通过 ：7；</param>
        /// <param name="Year">审批年份</param>
        /// <param name="Month">审批月份</param>
        /// <returns></returns>
        public DataSet GetExportApprovedList(string approveid, int state, int year, int month)
        {
            return dal.GetExportApprovedList(approveid, state, year, month);
        }
        
        /// <summary>
        /// 获得在职人员的考勤信息
        /// </summary>
        /// <param name="approveid">审批人编号</param>
        /// <param name="state"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="userids"></param>
        /// <returns></returns>
        public DataSet GetExprotApprovedList2(string approveid, int state, int year, int month, string userids, int attendanceSubType)
        {
            return dal.GetExprotApprovedList2(approveid, state, year, month, userids, attendanceSubType);
        }

        public DataSet GetExprotApprovedListNormal(string approveid, int state, int year, int month, string userids, int attendanceSubType)
        {
            return dal.GetExprotApprovedListNormal(approveid, state, year, month, userids, attendanceSubType);
        }

        /// <summary>
        /// 撤销刚提交的审批记录
        /// </summary>
        /// <param name="approveDataId"></param>
        /// <param name="approveState"></param>
        /// <returns></returns>
        public int CancelApproveLog(int approveDataId, int approveState)
        {
            return dal.CancelApproveLog(approveDataId, approveState);
        }

        /// <summary>
        /// 获得用户待审批的考勤事由信息信息内容
        /// </summary>
        /// <param name="UserID">用户编号</param>
        public DataTable GetWaitApproveList(int UserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UserID"));        // 用户编号
            dt.Columns.Add(new DataColumn("EmployeeName"));  // 用户中文名
            dt.Columns.Add(new DataColumn("AppId"));         // 申请人ID
            dt.Columns.Add(new DataColumn("AppUserName"));   // 申请人姓名
            dt.Columns.Add(new DataColumn("Content"));       // 事由信息
            dt.Columns.Add(new DataColumn("SubmitTime"));    // 提交时间
            dt.Columns.Add(new DataColumn("ID"));    // 提交时间

            // 事由信息集合
            DataSet ds = new DataSet();
            StringBuilder strbuild = new StringBuilder();
            string sqlStr = " and ApproveId=" + UserID;
            strbuild.Append("select * from AD_V_WaitApprove where 1=1 ");
            if (!string.IsNullOrEmpty(sqlStr))
            {
                strbuild.Append(sqlStr);
            }
            ds = dal.GetWaitApproveList(strbuild.ToString());

            // 月考勤统计单信息
            DataSet monthStatDs = new DataSet();
            StringBuilder strbuild2 = new StringBuilder();
            string sqlStr2 = " and ApproveId=" + UserID;
            strbuild2.Append("select * from AD_V_WaitApprove where 1=1 ");
            if (!string.IsNullOrEmpty(sqlStr2))
            {
                strbuild.Append(sqlStr2);
            }
            monthStatDs = dal.GetApproveMonthStatInfo(UserID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                { 
                    int userid = int.Parse(dr["ApproveId"].ToString());
                    int appUserId = int.Parse(dr["mUserId"].ToString());
                    string appUserName = dr["username"].ToString();
                    int approvetype = int.Parse(dr["ApproveType"].ToString());
                    ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(userid);

                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = userid;
                    dataRow[1] = userinfo.FullNameCN;
                    dataRow[2] = appUserId;
                    dataRow[3] = appUserName;
                    string content = "";
                    switch (approvetype)
                    { 
                        case (int)Status.MattersSingle.MattersSingle_Leave:
                            content += appUserName + "的请假单";
                            break;
                        case (int)Status.MattersSingle.MattersSingle_OffTune:
                            content += appUserName + "的调休单";
                            break;
                        case (int)Status.MattersSingle.MattersSingle_Other:
                            content += appUserName + "的其他事由单";
                            break;
                        case (int)Status.MattersSingle.MattersSingle_Out:
                            content += appUserName + "的外出单";
                            break;
                        case (int)Status.MattersSingle.MattersSingle_OverTime:
                            content += appUserName + "的OT单";
                            break;
                        case (int)Status.MattersSingle.MattersSingle_Travel:
                            content += appUserName + "的出差单";
                            break;
                        default:
                            break;
                    }
                    dataRow[4] = content;
                    dataRow[5] = dr["CreateTime"].ToString();
                    dataRow[6] = dr["id"].ToString();
                    dt.Rows.Add(dataRow);
                }
            }
            // 月考勤统计单信息集合
            if (monthStatDs != null && monthStatDs.Tables.Count > 0 && monthStatDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in monthStatDs.Tables[0].Rows)
                {
                    int userid = int.Parse(dr["ApproveId"].ToString());
                    ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(userid);

                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = int.Parse(dr["ApproveId"].ToString());
                    dataRow[1] = dr["ApproveName"].ToString();
                    dataRow[2] = int.Parse(dr["UserID"].ToString());
                    dataRow[3] = dr["EmployeeName"].ToString();
                    dataRow[4] = dr["EmployeeName"].ToString() + dr["Year"].ToString() + "年" + dr["Month"].ToString() + "月考勤统计信息";
                    dataRow[5] = dr["CreateTime"].ToString();
                    dataRow[6] = dr["id"].ToString();
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        /// <summary>
        /// 更新待审批记录信息
        /// </summary>
        /// <param name="ids">待审批记录ID</param>
        /// <param name="userid">接受该审批记录的用户ID</param>
        /// <param name="username">接受该审批记录的用户姓名</param>
        /// <returns>操作失败返回0</returns>
        public int UpdateWaitApproveInfo(string ids, int userid, string username)
        {
            int updateids = 0;

            if (string.IsNullOrEmpty(ids))
            {
                return 0;
            }
            string[] idarr = ids.Split(new char[] { ',' });
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (string id in idarr)
                    {
                        int approveid = int.Parse(id);
                        ApproveLogInfo model = GetModel(approveid);
                        model.ApproveID = userid;
                        model.ApproveName = username;
                        model.UpdateTime = DateTime.Now;
                        model.OperateorID = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
                        dal.Update(model, conn, trans);
                    }
                    trans.Commit();
                    updateids = 1;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    updateids = 0;
                }
            }
            return updateids;
        }
        #endregion
    }
}
