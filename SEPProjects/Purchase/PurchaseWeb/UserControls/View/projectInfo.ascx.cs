using System;
using ESP.Purchase.Entity;
using System.Collections.Generic;

public partial class UserControls_View_projectInfo : System.Web.UI.UserControl
{
    private bool isEditPage;
    public bool IsEditPage { get { return isEditPage; } set { isEditPage = value; } }
    private GeneralInfo model;
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    public void HideTabTitle()
    {
        tabTitle.Visible = false;
    }

    public int PurchaseAuditor { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindInfo()
    {
        ESP.Finance.Entity.DepartmentViewInfo costDept = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(Model.Departmentid);

        this.labTitleDateTime.Text = Model.lasttime.ToString();
        this.labTitleUser.Text = Model.requestorname;

        // txtproject_code.Text = Model.project_code;

        hyperProjectCode.Text = model.project_code;
        hyperProjectCode.Target = "_blank";

        hyperProjectCode.NavigateUrl = "http://xf.shunyagroup.com/project/ProjectDisplay.aspx?ProjectID=" + model.Project_id;

        txtproject_descripttion.Text = Model.project_descripttion;
        labGlideNo.Text = Model.glideno;
        txtprNo.Text = Model.PrNo;
        labMoneyType.Text = Model.moneytype;
        labDepartment.Text = costDept.level1 + "-" + costDept.level2 + "-" + costDept.level3;

        if (Model.Project_id > 0)
        {
            List<ProjectCodeChangedInfo> projectCodeList = ESP.Purchase.DataAccess.ProjectCodeChangedLogProvider.GetChangedListForPurchase(model.id);
            if (projectCodeList.Count > 0)
            {
                labOldProjectCode.Text = "历史项目号：";
                foreach (ProjectCodeChangedInfo codeModel in projectCodeList)
                {
                    labOldProjectCode.Text += codeModel.OldProjectCode + " ";
                }
            }
        }

        string alertMessage = "";
        if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.Disable)
        {
            labMessage.Text = "[" + ESP.Purchase.Common.State.DisabledMessageForPRView + "]";
            alertMessage = ESP.Purchase.Common.State.DisabledMessageForPRView;
        }
        else if (model.InUse == (int)ESP.Purchase.Common.State.PRInUse.DisableProject)
        {
            labMessage.Text = "[" + ESP.Purchase.Common.State.DisabledMessageForProjectView + "]";
            alertMessage = ESP.Purchase.Common.State.DisabledMessageForProjectView;
        }
        else
        {
            labMessage.Text = "";
        }
        if (model.status == ESP.Purchase.Common.State.requisition_Stop)
        {
            labMessage.Text = "[" + ESP.Purchase.Common.State.StopMessageForPRView + "]";
            alertMessage = ESP.Purchase.Common.State.StopMessageForPRView;
        }
        if (!IsPostBack)
        {
            if (isEditPage && alertMessage != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMessage + "您不能进行任何操作！');window.location.href='/Purchase/Default.aspx';", true);
            }
        }
    }
}
