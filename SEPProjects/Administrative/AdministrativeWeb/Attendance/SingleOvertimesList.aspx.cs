using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Framework.Entity;
using System.Data;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using ESP.Framework.BusinessLogic;
using ComponentArt.Web.UI;
using System.Text;
using System.Net.Mail;
using System.Collections;

namespace AdministrativeWeb.Attendance
{
    public partial class SingleOvertimesList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PickerTo1.SelectedDate = DateTime.Now;

                this.PickerFrom1.SelectedDate = DateTime.Now.AddDays(-7);
                BindInfo();
            }
        }

        protected string GetUserDepartment(object userid)
        {
            string departmentname = string.Empty;
            int[] depts = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserGroup(userid.ToString());
            ESP.Finance.Entity.DepartmentViewInfo dept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(depts[0]);
            return dept.level1 + "-" + dept.level2 + "-" + dept.level3;
        }

        protected string GetUserOverTimes(object starttime, object overtime)
        {
            if (overtime != null)
                return starttime.ToString() + " - " + overtime.ToString();
            else
                return starttime.ToString();
        }

        /// <summary>
        /// 绑定人员用户信息
        /// </summary>
        private void BindInfo()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            // 查询字符串
            StringBuilder strBuilder = new StringBuilder();

            IList<EmployeeInfo> listUser = new ESP.Administrative.BusinessLogic.OperationAuditManageManager().GetListForSingleOvertimes(CurrentUserID);
            if (listUser != null && listUser.Count > 0)
            {
                string userids = string.Empty;
                foreach (EmployeeInfo emp in listUser)
                {
                    if (emp != null)
                        userids += emp.UserID + ",";
                }
                userids = userids.TrimEnd(',');

                IList<SingleOvertimeInfo> list = new SingleOvertimeManager().GetEffectiveSingleOverTime(userids, this.PickerFrom1.SelectedDate, this.PickerTo1.SelectedDate, this.txtUserName.Text, this.txtUserCode.Text, Convert.ToInt32(this.ddlOvertimeType.SelectedValue));

                #region DB
                //if (!string.IsNullOrEmpty(txtUserName.Text))
                //{
                //    strBuilder.Append(" AND u.EmployeeName LIKE '%'+@EmployeeName+'%' ");
                //    parameterList.Add(new SqlParameter("@EmployeeName", txtUserName.Text.Trim()));
                //}
                //if (!string.IsNullOrEmpty(txtUserCode.Text))
                //{
                //    strBuilder.Append(" AND u.UserCode LIKE '%'+@UserCode+'%' ");
                //    parameterList.Add(new SqlParameter("@UserCode", txtUserCode.Text.Trim()));
                //}
                //if (!string.IsNullOrEmpty(txtPositions.Text))
                //{
                //    strBuilder.Append(" AND p.DepartmentPositionName LIKE '%'+@Position+'%' ");
                //    parameterList.Add(new SqlParameter("@Position", txtPositions.Text.Trim()));
                //}
                //// 分公司
                //if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                //{
                //    strBuilder.Append(" AND d.level1id=@level1id ");
                //    SqlParameter p = new SqlParameter("@level1id", SqlDbType.NVarChar);
                //    p.Value = cbCompany.SelectedValue;
                //    parameterList.Add(p);
                //}
                //// 团队
                //if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                //{
                //    strBuilder.Append(" AND d.level2id=@level2id ");
                //    SqlParameter p = new SqlParameter("@level2id", SqlDbType.NVarChar);
                //    p.Value = cbDepartment1.SelectedValue;
                //    parameterList.Add(p);
                //}
                //// 部门
                //if (!string.IsNullOrEmpty(cbDepartment2.SelectedValue))
                //{
                //    strBuilder.Append(" AND d.level3id=@level3id ");
                //    SqlParameter p = new SqlParameter("@level3id", SqlDbType.NVarChar);
                //    p.Value = cbDepartment2.SelectedValue;
                //    parameterList.Add(p);
                //}
                //// 在职状态
                //if (cbStatus.SelectedValue == "1")
                //{
                //    strBuilder.Append(" AND y.status NOT IN (" + ESP.HumanResource.Common.Status.WaitDimission + "," + ESP.HumanResource.Common.Status.Dimission + ") ");
                //}
                //else
                //{
                //    strBuilder.Append(" AND y.status IN (" + ESP.HumanResource.Common.Status.WaitDimission + "," + ESP.HumanResource.Common.Status.Dimission + ") ");
                //}

                //MonthStatManager monthStatManager = new MonthStatManager();
                //UserAttBasicInfoManager manager = new UserAttBasicInfoManager();

                //// 获得统计用户信息
                //DataSet ds = manager.GetIntegratedQueryUserInfo(UserID, strBuilder.ToString(), parameterList);
                //Dictionary<int, MonthStatInfo> dic = monthStatManager.GetMonthStatInfoByUserID(UserID, int.Parse(drpYear.SelectedValue), int.Parse(drpMonth.SelectedValue));

                //DataCodeManager dataCodeManager = new DataCodeManager();
                //List<DataCodeInfo> dataCodeList = dataCodeManager.GetDataCodeByType("HR" + UserID);
                //if (dataCodeList != null && dataCodeList.Count > 0)
                //{
                //    DataCodeInfo dataCodeModel = dataCodeList[0];
                //    if (dataCodeModel != null)
                //    {
                //        DataSet ds2 = manager.GetIntegratedQueryUserInfo(int.Parse(dataCodeModel.Code), strBuilder.ToString(), parameterList);
                //        Dictionary<int, MonthStatInfo> dic2 = monthStatManager.GetMonthStatInfoByUserID(int.Parse(dataCodeModel.Code), int.Parse(drpYear.SelectedValue), int.Parse(drpMonth.SelectedValue));
                //        if (ds2 != null && ds2.Tables.Count > 0)
                //        {
                //            foreach (DataRow dr in ds2.Tables[0].Rows)
                //            {
                //                DataRow tempdr = ds.Tables[0].NewRow();
                //                tempdr.ItemArray = dr.ItemArray;
                //                ds.Tables[0].Rows.Add(tempdr);
                //            }
                //        }

                //        if (dic2 != null && dic2.Count > 0)
                //        {
                //            if (dic == null)
                //            {
                //                dic = new Dictionary<int, MonthStatInfo>();
                //            }
                //            foreach (KeyValuePair<int, MonthStatInfo> key in dic2)
                //            {
                //                if (!dic.ContainsKey(key.Key))
                //                    dic.Add(key.Key, key.Value);
                //            }
                //        }
                //    }
                //}

                //DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("UserID"));    // 用户编号
                //dt.Columns.Add(new DataColumn("UserName"));  // 用户姓名
                //dt.Columns.Add(new DataColumn("UserCode"));  // 员工编号
                //dt.Columns.Add(new DataColumn("Content"));   // 内容信息
                //dt.Columns.Add(new DataColumn("Time"));
                //dt.Columns.Add(new DataColumn("SubmitTime"));
                //dt.Columns.Add(new DataColumn("Department"));
                //dt.Columns.Add(new DataColumn("Position"));
                //dt.Columns.Add(new DataColumn("Deleted"));

                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow userDR in ds.Tables[0].Rows)
                //    {
                //        if (userDR != null)
                //        {
                //            int monthstatUserid = int.Parse(userDR["Userid"].ToString());
                //            DataRow[] datarows = dt.Select("UserID =" + monthstatUserid);
                //            if (datarows.Length > 0)
                //            {
                //                continue;
                //            }
                //            MonthStatInfo monthStatInfo;
                //            if (dic == null || !dic.TryGetValue(monthstatUserid, out monthStatInfo))
                //            {
                //                monthStatInfo = null;
                //            }

                //            string SubmitTime = "";
                //            if (monthStatInfo != null)
                //            {
                //                SubmitTime = monthStatInfo.CreateTime == null ? "" : monthStatInfo.CreateTime.ToString("yyyy-MM-dd HH:ss");
                //            }

                //            if (drpState.SelectedValue == "1" && (monthStatInfo == null || monthStatInfo.State == Status.MonthStatAppState_NoSubmit
                //                || monthStatInfo.State == Status.MonthStatAppState_Overrule))
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                if (monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_Overrule)
                //                {
                //                    dr[3] = "审批驳回";
                //                }
                //                else
                //                {
                //                    dr[3] = "未提交";
                //                }
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "2" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "等待TeamLeader审批";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "3" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "等待团队HR审批";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "4" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_WaitManager)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "等待团队总经理审批";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "5" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "等待考勤管理员确认";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "6" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_Passed)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "审批通过";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "7" && monthStatInfo != null && monthStatInfo.State == Status.MonthStatAppState_Overrule)
                //            {
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = "审批驳回";
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //            else if (drpState.SelectedValue == "0")
                //            {
                //                string title = "";
                //                if (monthStatInfo != null)
                //                {
                //                    if (monthStatInfo.State == Status.MonthStatAppState_NoSubmit)
                //                    {
                //                        title = "未提交";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                //                    {
                //                        title = "等待总监审批";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitStatisticians)
                //                    {
                //                        title = "等待考勤统计员审批";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_Passed)
                //                    {
                //                        title = "审批通过";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                //                    {
                //                        title = "等待考勤管理员确认";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                //                    {
                //                        title = "等待团队HR审批";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_WaitManager)
                //                    {
                //                        title = "等待团队总经理审批";
                //                    }
                //                    else if (monthStatInfo.State == Status.MonthStatAppState_Overrule)
                //                    {
                //                        title = "审批驳回";
                //                    }
                //                }
                //                else
                //                {
                //                    title = "未提交";
                //                }
                //                DataRow dr = dt.NewRow();
                //                dr[0] = userDR["Userid"];           // 用户编号
                //                dr[1] = userDR["EmployeeName"];     // 用户姓名
                //                dr[2] = userDR["UserCode"];         // 员工编号
                //                dr[4] = drpYear.SelectedValue + "年" + drpMonth.SelectedValue + "月";
                //                dr[5] = SubmitTime;
                //                dr[6] = userDR["Department"];
                //                dr[7] = userDR["DepartmentPositionName"];
                //                dr[3] = title;
                //                dr[8] = false;
                //                dt.Rows.Add(dr);
                //            }
                //        }
                //    }
                //}
                #endregion
                Grid1.DataSource = list;
                Grid1.DataBind();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }

        protected void Grid1_ItemDataBound(object sender, GridItemDataBoundEventArgs e)
        {
            SingleOvertimeInfo info = (SingleOvertimeInfo)e.Item.DataItem;
        }
    }
}
