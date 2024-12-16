using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
public partial class UserControls_Project_ProjectSupporterDisplay : System.Web.UI.UserControl
{
    //int projectid = 0;
    //ESP.Finance.Entity.ProjectInfo projectinfo;

    private IList<ESP.Finance.Entity.SupporterInfo> supporterlist;

    public void InitProjectInfo()
    {
        //if (projectinfo == null )
        //{
        //    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        //    projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);

        //}

        var projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);

        supporterlist = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(projectid, null, null);

        this.gvSupporter.DataSource = supporterlist;
        this.gvSupporter.DataBind();

        if (this.gvSupporter.Rows.Count > 0)
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        else
        {
            this.trGrid.Visible = false; ;
            this.trNoRecord.Visible = true;
        }

        BindTotal(supporterlist);
    }

    public void InitProjectInfo(ESP.Finance.Entity.ProjectInfo customModel)
    {
        this.gvSupporter.DataSource = customModel.Supporters;
        this.gvSupporter.DataBind();

        if (this.gvSupporter.Rows.Count > 0)
        {
            this.trGrid.Visible = true;
            this.trNoRecord.Visible = false;
        }
        else
        {
            this.trGrid.Visible = false; ;
            this.trNoRecord.Visible = true;
        }

        BindTotal(customModel.Supporters);
    }

    private void BindTotal(IList<SupporterInfo> list)
    {
        this.trTotal.Visible = true;
        this.lblTotal.Text = "总计:";
        decimal total = 0;
        foreach (SupporterInfo item in list)
        {
            total += Convert.ToDecimal(item.BudgetAllocation);
        }
        if (total == 0)
        {
            this.trTotal.Visible = false;
        }
        this.lblTotal.Text = "总计:"+total.ToString("#,##0.00");
    }

    protected void gvSupporter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.SupporterInfo supporter = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
                ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporter.ProjectID);
                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (projectModel.ContractTaxID != null && projectModel.ContractTaxID.Value != 0)
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectModel.ContractTaxID.Value);

                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1).ToString();
                SupporterInfo item = (SupporterInfo)e.Row.DataItem;
                Label lblBudgetAllocation = (Label)e.Row.FindControl("lblBudgetAllocation");
                Label lblBudgetNoVAT = (Label)e.Row.FindControl("lblBudgetNoVAT");
                Label lblTaxVAT = (Label)e.Row.FindControl("lblTaxVAT");
                if (lblBudgetAllocation != null && item.BudgetAllocation != null)
                {
                    lblBudgetAllocation.Text = supporter.BudgetAllocation.Value.ToString("#,##0.00");
                    if (rateModel != null && projectModel.IsCalculateByVAT==1)
                    {
                        lblBudgetNoVAT.Text = (supporter.BudgetAllocation.Value / rateModel.VATParam1).ToString("#,##0.00");
                        lblTaxVAT.Text = ESP.Finance.BusinessLogic.CheckerManager.GetSupporterTaxByVAT(supporter, projectModel, rateModel).ToString("#,##0.00");
                    }
                }
              

                //所有部门级联字符串拼接
                Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporter.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(e.Row.Cells[4].Text)) + "');");

        }
    }

    public bool DontBindOnLoad { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack && !DontBindOnLoad)
        {
            InitProjectInfo();
        }
    }

}
