using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Data;

namespace AdministrativeWeb.Attendance
{
    public partial class ApprovedInfoList : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// 审批记录业务类
        /// </summary>
        private readonly ApproveLogManager approveLogManager = new ApproveLogManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        /// <summary>
        /// 绑定考勤统计数据
        /// </summary>
        protected void BindInfo()
        {
            string sqlStr = "";
            sqlStr += " AND ApproveID=" + UserID;

            DataSet ds = approveLogManager.GetApprovedList(sqlStr);
            DataColumn column = new DataColumn("MatterStateName");
            DataColumn column2 = new DataColumn("ApproveTypeName");
            ds.Tables[0].Columns.Add(column);
            ds.Tables[0].Columns.Add(column2);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                { 
                    int approveType = int.Parse(dr["Approvetype"].ToString());
                    int matterState = int.Parse(dr["matterstate"].ToString());

                    // 判断是否是OT单
                    if (approveType == (int)Status.MattersSingle.MattersSingle_OverTime)
                    {
                        // 判断事由的状态
                        if (matterState == Status.OverTimeState_NotSubmit)
                        {
                            dr["MatterStateName"] = "未提交";
                        }
                        else if (matterState == Status.OverTimeState_Passed)
                        {
                            dr["MatterStateName"] = "审批通过";
                        }
                        else if (matterState == Status.OverTimeState_WaitDirector)
                        {
                            dr["MatterStateName"] = "等待总监审批";
                        }
                        else if (matterState == Status.OverTimeState_WaitHR)
                        {
                            dr["MatterStateName"] = "等待人力审批";
                        }
                    }
                    else
                    {
                        // 判断事由的状态
                        if (matterState == Status.MattersState_NoSubmit)
                        {
                            dr["MatterStateName"] = "未提交";
                        }
                        else if (matterState == Status.MattersState_Passed)
                        {
                            dr["MatterStateName"] = "审批通过";
                        }
                        else if (matterState == Status.MattersState_WaitDirector)
                        {
                            dr["MatterStateName"] = "等待总监审批";
                        }
                        else if (matterState == Status.MattersState_WaitHR)
                        {
                            dr["MatterStateName"] = "等待人力审批";
                        }
                    }

                    // 判断事由单据的类型
                    if (approveType == (int)Status.MattersSingle.MattersSingle_Attendance)
                    {
                        dr["ApproveTypeName"] = "考勤月统计单";
                    }
                    else if (approveType == (int)Status.MattersSingle.MattersSingle_Leave)
                    {
                        dr["ApproveTypeName"] = "请假单";
                    }
                    else if (approveType == (int)Status.MattersSingle.MattersSingle_OffTune)
                    {
                        dr["ApproveTypeName"] = "调休单";
                    }
                    else if (approveType == (int)Status.MattersSingle.MattersSingle_OverTime)
                    {
                        dr["ApproveTypeName"] = "OT单";
                    }
                    else if (approveType == (int)Status.MattersSingle.MattersSingle_Travel)
                    {
                        dr["ApproveTypeName"] = "出差单";
                    }
                }
            }
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }
    }
}
