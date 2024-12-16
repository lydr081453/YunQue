using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Employees
{
    public partial class AddressBookEdit : ESP.Web.UI.PageBase
    {
        private string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]) && !string.IsNullOrEmpty(Request["DepartmentId"]))
                {
                    userid = Request["userid"].Trim();
                    initForm(int.Parse(Request["userid"].Trim().ToString()), int.Parse(Request["DepartmentId"].Trim().ToString()));
                }
            }
        }

        /// <summary>
        /// Inits the form.
        /// </summary>
        /// <param name="sysid">The sysid.</param>
        protected void initForm(int sysid, int departmentId)
        {
            ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(sysid);

            ESP.HumanResource.Entity.UsersInfo user = UsersManager.GetModel(sysid);

            txtUserCode.Text = model.Code;   // 员工编号
            txtCNName.Text = user.LastNameCN + user.FirstNameCN;  // 中文名
            txtENName.Text = user.FirstNameEN + "." + user.LastNameEN;  // 英文名
            txtPhone1.Text = model.Phone1;  // 员工分机号
            txtMobilePhone.Text = model.MobilePhone;  // 员工手机
            txtEmail.Text = model.InternalEmail;
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID + " and a.DepartmentId=" + departmentId);
            if (eiplist.Count != 0)
            {
                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];
                txtPosition.Text = deps.DepartmentPositionName;
                txtJob_JoinJob.Value = deps.DepartmentPositionID.ToString();
            }
            hidDeptId.Value = departmentId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = EmployeeBaseManager.GetModel(int.Parse(Request["userid"].Trim().ToString()));
                if (model != null)
                {
                    model.Phone1 = txtPhone1.Text.Trim();
                    model.MobilePhone = txtMobilePhone.Text.Trim();
                    model.InternalEmail = txtEmail.Text.Trim();
                    string pageIndex = "";
                    if (!string.IsNullOrEmpty(Request["pageIndex"]))
                    {
                        pageIndex = Request["pageIndex"].Trim();
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(Request["DepartmentId"]))
                        {
                            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eiplist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + model.UserID + " and a.DepartmentId=" + int.Parse(Request["DepartmentId"].Trim().ToString()));
                            if (eiplist.Count != 0)
                            {
                                ESP.HumanResource.Entity.EmployeesInPositionsInfo deps = eiplist[0];
                                deps.DepartmentPositionID = int.Parse(txtJob_JoinJob.Value);
                                deps.DepartmentPositionName = txtPosition.Text;
                                deps.DepartmentID = deps.GroupID;
                                int ret = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.UpdateByDepartmentID(deps);
                                
                                if (ret <= 0)
                                {
                                    ShowCompleteMessage("修改失败", "AddressBookList.aspx?pageIndex=" + pageIndex);
                                }
                            }
                        }
                        
                        EmployeeBaseManager.Update(model);
                        ShowCompleteMessage("修改成功", "AddressBookList.aspx?pageIndex=" + pageIndex);
                    }
                    catch (Exception)
                    {
                        ShowCompleteMessage("修改失败", "AddressBookList.aspx?pageIndex=" + pageIndex);
                    }
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string pageIndex = "";
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Request["pageIndex"].Trim();
            }
            string userCode = "";
            if (Request.QueryString["userCode"] != "" && Request.QueryString["userCode"] != null)
            {
                userCode = Request.QueryString["userCode"];
            }
            string userName = "";
            if (Request.QueryString["userName"] != "" && Request.QueryString["userName"] != null)
            {
                userName = Request.QueryString["userName"];
            }
            Response.Redirect("AddressBookList.aspx?pageIndex=" + pageIndex + "&userCode=" + userCode + "&userName=" + userName);
        }

    }
}
