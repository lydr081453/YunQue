using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Data.SqlClient;

public partial class Purchase_CreateNewPayment : ESP.Web.UI.PageBase
{
    int returnId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.DimissionStatus == ESP.HumanResource.Common.Status.DimissionFinanceAudit)
        {
            Response.Redirect("/Edit/ReturnTabEdit.aspx");
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    /// <summary>
    /// 绑定页面信息
    /// </summary>
    private void BindInfo()
    {
        foreach (PaymentTypeInfo typeModel in ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(null, null))
        {
            if (typeModel.Tag != null && typeModel.Tag.ToLower() == "media")
                ddlPaymentType.Items.Insert(0, new ListItem(typeModel.PaymentTypeName, typeModel.PaymentTypeID.ToString()));
        }

        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        if (returnModel != null)
        {
            List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(returnModel.ProjectID.Value);
            List<ESP.Purchase.Entity.V_GetProjectGroupList> list1 = new List<ESP.Purchase.Entity.V_GetProjectGroupList>();

            if (returnModel.ProjectID != 0)
            {
                foreach (ESP.Purchase.Entity.V_GetProjectGroupList p in list)
                {
                    //只有申请人属于的成本组能显示出来
                    if (ESP.Purchase.BusinessLogic.V_GetProjectList.MemberInProjectGroup(p.ProjectId, p.GroupID, int.Parse(CurrentUser.SysID)) || returnModel.ProjectCode.Contains(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) || returnModel.ProjectCode.Contains("-*GM-") || returnModel.ProjectCode.Contains("-GM*-"))
                        list1.Add(p);
                }
            }
            else
            {
                ESP.Purchase.Entity.V_GetProjectGroupList p = new ESP.Purchase.Entity.V_GetProjectGroupList();
                p.ProjectId = 0;
                p.GroupID = returnModel.DepartmentID.Value;
                p.GroupName = returnModel.DepartmentName;
                list1.Add(p);
            }
            ddlDepartment.DataSource = list1;
            ddlDepartment.DataTextField = "groupName";
            ddlDepartment.DataValueField = "groupID";
            ddlDepartment.DataBind();
            ddlDepartment.SelectedValue = returnModel.DepartmentID.ToString();

            if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
            {
                TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(returnModel.ProjectID.Value);
                decimal allTraffic = ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee总额(TopMessage.ProjectModel, int.Parse(ddlDepartment.SelectedValue));
                labAllTraffic.Text = allTraffic.ToString("#,##0.##");
                txtSYTraffic.Text = (allTraffic - ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee使用总额(TopMessage.ProjectModel.ProjectCode, int.Parse(ddlDepartment.SelectedValue))).ToString("#,##0.##");
            }
            if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
            }
            txtRemark.Text = returnModel.Remark;
            hidProjectID.Value = returnModel.ProjectID.ToString();
            txtProjectCode.Text = returnModel.ProjectCode;
            txtBeginDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
            ddlPaymentType.SelectedValue = returnModel.PaymentTypeID.Value.ToString();
            txtPreFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.##");
            txtDesc.Text = returnModel.ReturnContent;
        }
    }

    protected void lnkDepartment_Click(object sender, EventArgs e)
    {
        List<ESP.Purchase.Entity.V_GetProjectGroupList> list = ESP.Purchase.BusinessLogic.V_GetProjectGroupList.GetGroupListByPid(int.Parse(hidProjectID.Value));
        List<ESP.Purchase.Entity.V_GetProjectGroupList> list1 = new List<ESP.Purchase.Entity.V_GetProjectGroupList>();
        foreach (ESP.Purchase.Entity.V_GetProjectGroupList p in list)
        {
            //只有申请人属于的成本组能显示出来
            ProjectInfo projectModel = ProjectManager.GetModelWithOutDetailList(p.ProjectId);
            if (ESP.Purchase.BusinessLogic.V_GetProjectList.MemberInProjectGroup(p.ProjectId, p.GroupID, int.Parse(CurrentUser.SysID)) || projectModel.ProjectCode.Contains(ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN) || projectModel.ProjectCode.Contains("-*GM-") || projectModel.ProjectCode.Contains("-GM*-"))
                list1.Add(p);
        }
        ddlDepartment.DataSource = list1;
        ddlDepartment.DataTextField = "groupName";
        ddlDepartment.DataValueField = "groupID";
        ddlDepartment.DataBind();
        if (list1.Count == 1)
        {
            ddlDepartment_SelectedIndexChanged(new object(), new EventArgs());
        }
        else
        {
            ddlDepartment.Items.Insert(0, new ListItem("请选择...", "0"));
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int projectId = int.Parse(hidProjectID.Value);
        int departmentId = int.Parse(ddlDepartment.SelectedValue);
        if (projectId != 0)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectId);
            decimal totalTraffice = ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee总额(projectModel, departmentId);  // GetAllTraffic(projectId, departmentId).ToString("#,##0.####");
            labAllTraffic.Text = totalTraffice.ToString("#,##0.00");
            txtSYTraffic.Text = (totalTraffice - ESP.Finance.BusinessLogic.CheckerManager.得到TrafficFee使用总额(projectModel.ProjectCode, departmentId)).ToString("#,##0.00");// GetSYTraffic(projectId, departmentId, decimal.Parse(labAllTraffic.Text)).ToString("#,##0.####");
        }
    }

    /// <summary>
    /// 获得对象
    /// </summary>
    /// <returns></returns>
    private bool SaveModel()
    {
        ESP.Finance.Entity.ReturnInfo returnModel = new ReturnInfo();
        if (returnId > 0)
            returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        returnModel.ProjectID = int.Parse(hidProjectID.Value);
        returnModel.ProjectCode = txtProjectCode.Text.Trim();
        returnModel.ReturnPreDate = DateTime.Parse(txtBeginDate.Text.Trim());
        returnModel.PreBeginDate = DateTime.Parse(txtBeginDate.Text.Trim());
        returnModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
        returnModel.PaymentTypeName = ddlPaymentType.SelectedItem.Text;
        returnModel.PreFee = decimal.Parse(txtPreFee.Text.Trim());
        returnModel.ReturnContent = txtDesc.Text.Trim();
        returnModel.Remark = txtRemark.Text.Trim();
        returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PROJECT_Media;
        if ((ddlDepartment.SelectedValue != "" && int.Parse(ddlDepartment.SelectedValue) > 0) || hidDeptId.Value != "")
        {
            returnModel.DepartmentID = int.Parse(ddlDepartment.SelectedValue == "" ? hidDeptId.Value.Split(',')[0] : ddlDepartment.SelectedValue);
            returnModel.DepartmentName = ddlDepartment.SelectedItem == null ? hidDeptId.Value.Split(',')[1] : ddlDepartment.SelectedItem.Text;
        }
        else
            returnModel.DepartmentID = 0;
        if (returnModel.ReturnID > 0)
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);//更新操作
        else
        {
            returnModel.RequestorID = int.Parse(CurrentUser.SysID);
            returnModel.RequestUserCode = CurrentUser.ITCode;
            returnModel.RequestUserName = CurrentUser.ITCode;
            returnModel.RequestEmployeeName = CurrentUser.Name;
            returnModel.RequestDate = DateTime.Now;
            returnModel.ReturnStatus = (int)PaymentStatus.Save;
            returnId = ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(returnModel); //添加操作
        }
        return true;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReturnList.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveModel())
            ShowCompleteMessage("保存成功！", "ReturnList.aspx");
    }

    protected void btnSetting_Click(object sender, EventArgs e)
    {
        if (SaveModel())
            Response.Redirect("SetAuditor.aspx?backUrl=CreateNewPayment.aspx&" + RequestName.ReturnID + "=" + returnId);
    }

}
