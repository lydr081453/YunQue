using System;
using System.Collections.Generic;
using ESP.Finance.Utility;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Project_PrepareDisplay : System.Web.UI.UserControl
{
    ESP.Finance.Entity.ProjectInfo project;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[RequestName.ProjectID]));
            InitProjectInfo();
        }
    }
    public void InitProjectInfo(ESP.Finance.Entity.ProjectInfo CustomerModel)
    {
        if (CustomerModel != null)
        {
            this.lblBizDesc.Text = CustomerModel.BusinessDescription;
            this.lblContactStatus.Text = CustomerModel.ContractStatusName;
           //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(CustomerModel.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblGroup.Text = groupname.Substring(0, groupname.Length - 1);

            if (!string.IsNullOrEmpty(CustomerModel.ProjectCode))
            {
                this.lblProjectCode.Text = CustomerModel.ProjectCode;
            }
            this.lblApplicant.Text = CustomerModel.ApplicantEmployeeName;
            lblApplicant.Attributes["onclick"] = "ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(CustomerModel.ApplicantUserID) + "');";
            this.lblSerialCode.Text = CustomerModel.SerialCode;
            this.lblProjectType.Text = CustomerModel.ProjectTypeName;
            if (CustomerModel.ProjectTypeLevel2ID != null && CustomerModel.ProjectTypeLevel2ID > 0)
            {
                this.lblProjectType.Text += "-" + ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(CustomerModel.ProjectTypeLevel2ID.Value).ProjectTypeName;
            }
            if (CustomerModel.ProjectTypeLevel3ID != null && CustomerModel.ProjectTypeLevel3ID > 0)
            {
                this.lblProjectType.Text += "-" + ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(CustomerModel.ProjectTypeLevel3ID.Value).ProjectTypeName;
            }
          
            this.lblBizType.Text = CustomerModel.BusinessTypeName;
            labBrands.Text = CustomerModel.Brands;
            setRelevanceProjectCode(CustomerModel);

            lblBDProject.Text = CustomerModel.BDProjectCode;
            labBusinessPersonName.Text = CustomerModel.BusinessPersonName;
            if (CustomerModel.BusinessPersonId != null)
                labBusinessPersonName.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(CustomerModel.BusinessPersonId.Value) + "');";

            labCreator.Text = CustomerModel.CreatorName;
            labCreator.Attributes["onclick"] = "ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(CustomerModel.CreatorID) + "');";
            labAdvertiserID.Text = CustomerModel.AdvertiserID;
            labCustomerProjectCode.Text = CustomerModel.CustomerProjectCode;
        }
    }

    private void setRelevanceProjectCode(ESP.Finance.Entity.ProjectInfo model)
    {
        labRelevanceProjectCode.Text = model.RelevanceProjectCode;

        if (string.IsNullOrEmpty(model.RelevanceProjectCode))
        {
            //反向显示关联项目号
            var relevanceProjectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and RelevanceProjectId=" + model.ProjectId);
            if (relevanceProjectList != null)
            {
                foreach (var item in relevanceProjectList) {
                    labRelevanceProjectCode.Text += item.ProjectCode + ";";
                }
                
            }
        }
    }

    private  void InitProjectInfo()
    { 
        if(project!=null)
        {
            this.lblBizDesc.Text = project.BusinessDescription;
            this.lblContactStatus.Text = project.ContractStatusName;
            //所有部门级联字符串拼接
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(project.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            this.lblGroup.Text = groupname.Substring(0, groupname.Length - 1);

            if (!string.IsNullOrEmpty(this.project.ProjectCode))
            {
                this.lblProjectCode.Text = this.project.ProjectCode;
            }
            this.lblApplicant.Text = project.ApplicantEmployeeName;
            lblApplicant.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(project.ApplicantUserID) + "');";
            this.lblSerialCode.Text = project.SerialCode;
            this.lblProjectType.Text = project.ProjectTypeName;
            if (project.ProjectTypeLevel2ID != null && project.ProjectTypeLevel2ID > 0)
            {
                this.lblProjectType.Text += "-" + ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel2ID.Value).ProjectTypeName;
            }
            if (project.ProjectTypeLevel3ID != null && project.ProjectTypeLevel3ID > 0)
            {
                this.lblProjectType.Text += "-" + ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel3ID.Value).ProjectTypeName;
            }

            this.lblBizType.Text = project.BusinessTypeName;
            SetOldCode(project.ProjectId);
            labBrands.Text = project.Brands;
            labAdvertiserID.Text = project.AdvertiserID;
            labCustomerProjectCode.Text = project.CustomerProjectCode;
            setRelevanceProjectCode(project);
            lblBDProject.Text = project.BDProjectCode;
            labBusinessPersonName.Text = project.BusinessPersonName;
            if(project.BusinessPersonId != null)
                labBusinessPersonName.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(project.BusinessPersonId.Value) + "');";
            labCreator.Text = project.CreatorName;
            labCreator.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(project.CreatorID) + "');";
       }
    }

    private void SetOldCode(int projectId)
    {
        List<ESP.Purchase.Entity.ProjectCodeChangedInfo> projectCodeList = ESP.Purchase.DataAccess.ProjectCodeChangedLogProvider.GetChangedListForProject(projectId);
        if (projectCodeList.Count > 0)
        {
            labOldProjectCode.Text = "历史项目号：";
            foreach (ESP.Purchase.Entity.ProjectCodeChangedInfo codeModel in projectCodeList)
            {
                labOldProjectCode.Text += codeModel.OldProjectCode + " ";
            }
        }
    }
}
