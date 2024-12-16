using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Attendance
{
    public partial class MattersView : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["matterid"]))
                { 
                    MattersInfo model = new MattersManager().GetModel(int.Parse(Request["matterid"]));
                    if (model != null)
                    {
                        hidLeaveID.Value = model.ID.ToString();
                        
                        txtLeaveCause.Text = model.MatterContent;
                        // 获得用户基本信息和用户部门组别信息
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtUserCode.Text = emp.Code;
                        txtUserName.Text = emp.FullNameCN;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labAppTime.Text = model.CreateTime.ToString("yyyy-MM-dd HH:mm");
                        string matterType = "";
                        switch (model.MatterType)
                        {
                            case 1:
                                matterType = "病假";
                                break;

                            case 2:
                                matterType = "事假";
                                break;

                            case 3:
                                matterType = "年假";
                                break;

                            case 4:
                                matterType = "婚假";
                                break;

                            case 5:
                                matterType = "产假";
                                break;

                            case 6:
                                matterType = "丧假";
                                break;

                            case 7:
                                matterType = "出差";
                                break;

                            case 8:
                                matterType = "外出";
                                break;

                            case 9:
                                matterType = "调休";
                                break;
                            case 10:
                                matterType = "其它";
                                break;
                        }
                        labAppMatter.Text = matterType;

                        labMatterTimeInfo.Text = model.BeginTime.ToString("yyyy-MM-dd HH:mm") 
                            + " 至 " + model.EndTime.ToString("yyyy-MM-dd HH:mm");
                        labProjectNo.Text = model.ProjectNo;
                    }
                }
                else if (!string.IsNullOrEmpty(Request["singleid"]))
                {
                    SingleOvertimeInfo model = new SingleOvertimeManager().GetModel(int.Parse(Request["singleid"]));
                    if (model != null)
                    {
                        hidLeaveID.Value = model.ID.ToString();

                        txtLeaveCause.Text = model.OverTimeCause;
                        // 获得用户基本信息和用户部门组别信息
                        ESP.Framework.Entity.UserInfo userinfoModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserID);
                        ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.UserID);
                        txtUserCode.Text = emp.Code;
                        txtUserName.Text = emp.FullNameCN;
                        IList<ESP.Framework.Entity.EmployeePositionInfo> list =
                            ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(model.UserID);
                        if (list != null && list.Count > 0)
                        {
                            ESP.Framework.Entity.EmployeePositionInfo emppos = list[0];
                            txtGroup.Text = emppos.DepartmentName;
                            int parnetId = ESP.Framework.BusinessLogic.DepartmentManager.Get(emppos.DepartmentID).ParentID;
                            txtTeam.Text = ESP.Framework.BusinessLogic.DepartmentManager.Get(parnetId).DepartmentName;
                        }
                        labAppTime.Text = model.CreateTime.ToString("yyyy-MM-dd HH:mm");
                        labAppMatter.Text = "OT";
                        labMatterTimeInfo.Text = model.BeginTime.ToString("yyyy-MM-dd HH:mm")
                            + " 至 " + model.EndTime.ToString("yyyy-MM-dd HH:mm");
                        labProjectNo.Text = model.ProjectNo;
                    }
                }

                if (!string.IsNullOrEmpty(Request["backurl"]))
                {
                    BackUrl = Request["backurl"];
                }

                if (!string.IsNullOrEmpty(Request["flag"]))
                {
                    btnLeaveBack.Visible = false;
                }
            }
        }

        /// <summary>
        /// 返回到上一个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLeaveBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        private string BackUrl
        {
            get
            {
                return this.ViewState["BackUrl"] as string;
            }
            set
            {
                this.ViewState["BackUrl"] = value;
            }
        }
    }
}
