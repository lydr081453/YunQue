using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using AdministrativeWeb.UserControls.Matter;
using System.Net.Mail;

namespace AdministrativeWeb.Attendance
{
    public partial class SingleOvertimeDetails : System.Web.UI.Page
    {
        #region 成员变量
        /// <summary>
        /// OT单业务类
        /// </summary>
        private SingleOvertimeManager overtimeManager = new SingleOvertimeManager();
        /// <summary>
        /// 审批日志业务类
        /// </summary>
        private ApproveLogManager approvelogManager = new ApproveLogManager();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 初始化页面内容
        /// </summary>
        protected void InitPage()
        {
            // 获得OT单信息
            SingleOvertimeInfo overtimeInfo = overtimeManager.GetModel(Convert.ToInt32(Request["overtimeID"]));
            if (overtimeInfo != null)
            {
                singlOverTimeId.Value = overtimeInfo.ID.ToString();
                hidUserid.Value = overtimeInfo.UserID.ToString();
                txtUserName.Text = overtimeInfo.EmployeeName;
                txtUserCode.Text = overtimeInfo.UserCode;
                IList<ESP.Framework.Entity.EmployeePositionInfo> list = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(overtimeInfo.UserID);
                if (list != null && list.Count > 0)
                {
                    ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                    txtGroup.Text = emppos.DepartmentName;
                    int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                    txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                }
                labAppTime.Text = overtimeInfo.AppTime.ToString("yyyy年MM月dd日 HH时");
                hidOverTimeProjectId.Value = overtimeInfo.ProjectID.ToString();
                txtOverTimeProjectNo.Text = overtimeInfo.ProjectNo;
                PickerFrom1.SelectedDate = overtimeInfo.BeginTime;
                PickerTo1.SelectedDate = overtimeInfo.EndTime;
                txtDes.Text = overtimeInfo.OverTimeCause;

                this.grdMatterDetails.DataSource = new MatterReasonManager().GetList(" SingleOverTimeID=" + overtimeInfo.ID.ToString());
                this.grdMatterDetails.DataBind();

                // 判断是否是事后申请
                if (overtimeInfo.CreateTime > overtimeInfo.BeginTime)
                {
                    labAfterApprove.Text = "事后申请：<img src=\"../../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\">";
                }
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect(BackUrl);
        }

        protected void grdMatterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MatterReasonInfo info = (MatterReasonInfo)e.Row.DataItem;
            if (info != null)
            {
                Label lblTimeStart = (Label)e.Row.FindControl("lblTimeStart");
                Label lblTimeEnd = (Label)e.Row.FindControl("lblTimeEnd");
                Label txtMatterDetails = (Label)e.Row.FindControl("txtMatterDetails");

                lblTimeStart.Text = info.StartDate.ToString();
                lblTimeEnd.Text = info.EndDate.ToString();

                txtMatterDetails.Text = info.Details;
            }
        }
    }
}
