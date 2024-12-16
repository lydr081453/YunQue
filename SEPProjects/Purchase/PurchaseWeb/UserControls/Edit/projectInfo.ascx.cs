using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class UserControls_Edit_projectInfo : System.Web.UI.UserControl
{
    private ESP.Purchase.Entity.GeneralInfo model;
    public ESP.Purchase.Entity.GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    private ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee CurrentUser
    {
        get { return currentUser; }
        set { currentUser = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindInfo()
    {
        for (int i = 0; i < State.money_type.Length; i++)
        {
            ddlMoneyType.Items.Add(new ListItem(State.money_type[i].ToString(), State.money_type[i].ToString()));
        }
        if (Model != null)
        {
            labTitleDateTime.Text = Model.lasttime.ToString();
            labTitleUser.Text = Model.requestorname;

            hidProejctId.Value = model.Project_id.ToString();
            if (Model.project_code.Trim() != "")
            {
                txtproject_code.Text = Model.project_code;
            }
            txtproject_descripttion.Text = Model.project_descripttion;

            labGlideNo.Text = Model.glideno;
            txtprNo.Text = Model.PrNo;
            if (!string.IsNullOrEmpty(Model.project_code))
            {
                ddlMoneyType.SelectedValue = Model.moneytype;
            }
            string adminDepts = ESP.Configuration.ConfigurationManager.SafeAppSettings["ProjectAdministrativeIDs"];
            string specialDepts = ESP.Configuration.ConfigurationManager.SafeAppSettings["SpecialDeptIDs"];
            string currentUserDept =","+currentUser.GetDepartmentIDs()[0].ToString()+",";
            List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(Model.Project_id);
            List<ESP.Purchase.Entity.V_GetProjectGroupList> list1 = new List<V_GetProjectGroupList>();
            foreach(ESP.Purchase.Entity.V_GetProjectGroupList p in list)
            {
                //如果支持方是FEE，则可以选择主申请方的成本组
                IList<ESP.Finance.Entity.SupporterInfo> supportList = ESP.Finance.BusinessLogic.SupporterManager.GetList("ProjectID=" + p.ProjectId.ToString() + " and GroupID=" + p.GroupID.ToString());
                if (supportList != null && supportList.Count > 0)
                {
                    if (supportList[0].IncomeType == "Fee")
                    {
                        ESP.Finance.Entity.ProjectInfo ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supportList[0].ProjectID);
                        ESP.Purchase.Entity.V_GetProjectGroupList newP = new V_GetProjectGroupList();
                        newP.ProjectId = ProjectModel.ProjectId;
                        newP.GroupID = ProjectModel.GroupID.Value;
                        newP.GroupName = ProjectModel.GroupName;
                        newP.COST = ESP.Finance.BusinessLogic.ContractCostManager.GetTotalAmountByProject(ProjectModel.GroupID.Value);
                        list1.Add(newP);
                    }
                }
                //只有申请人属于的成本组能显示出来
                if (adminDepts.IndexOf(currentUserDept) >= 0 || specialDepts.IndexOf(currentUserDept) >= 0 || ESP.Purchase.BusinessLogic.V_GetProjectList.MemberInProjectGroup(p.ProjectId, p.GroupID, int.Parse(currentUser.SysID)) || Model.project_code.Contains(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) || Model.project_code.Contains("-*GM-") || Model.project_code.Contains("-GM*-"))
                    list1.Add(p);
                
            }
            ddlDepartment.DataSource = list1;
            ddlDepartment.DataTextField = "groupName";
            ddlDepartment.DataValueField = "groupID";
            ddlDepartment.DataBind();
            ddlDepartment.SelectedValue = Model.Departmentid.ToString();
        }
        else
        {
            tabTitle.Visible = false;
        }
    }

    public ESP.Purchase.Entity.GeneralInfo setModelInfo()
    {
        if (Model.requestor == 0)
        {
            Model.app_date = DateTime.Now;
            Model.requestorname = CurrentUser.Name;
            Model.requestor_group = CurrentUser.GetDepartmentNames().Count == 0 ? "" : CurrentUser.GetDepartmentNames()[0].ToString();
            Model.requestor_info = CurrentUser.Telephone;
        }
        Model.requestor = int.Parse(CurrentUser.SysID);

        Model.Project_id = int.Parse(hidProejctId.Value == "" ? "0" : hidProejctId.Value);
        Model.project_code = txtproject_code.Text.Trim();
        Model.project_descripttion = txtproject_descripttion.Text.Trim();
        Model.moneytype = ddlMoneyType.SelectedValue;
        if ((ddlDepartment.SelectedValue != "" && int.Parse(ddlDepartment.SelectedValue) > 0) || hidDeptId.Value != "")
        {
            Model.Departmentid = int.Parse(ddlDepartment.SelectedValue == "" ? hidDeptId.Value.Split(',')[0] : ddlDepartment.SelectedValue);
            Model.Department = ddlDepartment.SelectedItem == null ? hidDeptId.Value.Split(',')[1] : ddlDepartment.SelectedItem.Text;
        }
        else
        {
            if (Model.Project_id != 0)
            {
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Model.Project_id);
                Model.Departmentid = projectModel.GroupID.Value;
                model.Department = projectModel.GroupName;
            }
            else
                Model.Departmentid = CurrentUser.GetDepartmentIDs()[0];
        }
        return Model;
    }
}
