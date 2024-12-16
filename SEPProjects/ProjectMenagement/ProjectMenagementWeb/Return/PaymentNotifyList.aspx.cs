using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class Return_PaymentNotifyList : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Search();
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }


    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        term = " ProjectID in(select ProjectID from F_Project where Status=@Status and ApplicantUserID=@UserID) ";
        SqlParameter p = new SqlParameter("@Status",SqlDbType.Int,4);
        p.SqlValue = (int)Status.FinanceAuditComplete;
        paramlist.Add(p);
        SqlParameter p1 = new SqlParameter("@UserID",SqlDbType.Int,4);
        p1.SqlValue = Convert.ToInt32(CurrentUser.SysID);
        paramlist.Add(p1);
        //SqlParameter p1 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
        //if (this.ddlStatus.SelectedIndex == 0)
        //{
        //    p1.SqlValue = (int)ReturnStatus.Save;
        //}
        //else
        //{
        //    p1.SqlValue = Convert.ToInt32(this.ddlStatus.SelectedValue);
        //}
       // paramlist.Add(p1);
        if (!string.IsNullOrEmpty(term))
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text))
            {
                term += " and (PaymentCode like '%'+@Key+'%' or ProjectCode like '%'+@Key+'%' or PaymentContent like '%'+@Key+'%' )";
                SqlParameter p2 = new SqlParameter("@Key", SqlDbType.NVarChar, 50);
                p2.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p2);
            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
            this.gvG.DataBind();
        }
    }


    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(e.Row.Cells[1].Text));
            ESP.Finance.Entity.PaymentInfo model = ESP.Finance.BusinessLogic.PaymentManager.GetModel(Convert.ToInt32(e.Row.Cells[0].Text));

            Label lblResponser = (Label)e.Row.FindControl("lblResponser");
            if (lblResponser != null)
            {
                lblResponser.Text = projectModel.ApplicantEmployeeName;
                lblResponser.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectModel.ApplicantUserID) + "');");
            }
            Label lblPaymentContent = (Label)e.Row.FindControl("lblPaymentContent");
            if (lblPaymentContent != null)
            {
                if (model.PaymentContent != null && model.PaymentContent.Length > 10)
                {
                    lblPaymentContent.Text = model.PaymentContent.Substring(0, 10) + "...";
                }
                else if (model.PaymentContent != null && model.PaymentContent.Length < 10)
                {
                    lblPaymentContent.Text = model.PaymentContent;
                }
            }

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus != null)
            {
                switch (Convert.ToInt32(lblStatus.Text))
                { 
                    case (int)ReturnStatus.Save:
                        lblStatus.Text = "已保存";
                        break;
                    case (int)ReturnStatus.Submit:
                        lblStatus.Text = "已提交";
                        break;
                    case (int)ReturnStatus.MajorCommit:
                        lblStatus.Text = "总监已确认";
                        break;
                    case (int)ReturnStatus.FinancialHold:
                        lblStatus.Text = "财务已挂账";
                        break;
                    case (int)ReturnStatus.FinancialOver:
                        lblStatus.Text = "已付款";
                        break;
                }
            }

            Label lblPaymentBudget = (Label)e.Row.FindControl("lblPaymentBudget");
            if (lblPaymentBudget != null)
            {
                lblPaymentBudget.Text = model.PaymentBudget == null ? "0.00" : model.PaymentBudget.Value.ToString("#,##0.00");
            }
            HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
            if (hylEdit != null)
            {
                if (model.PaymentStatus == (int)ReturnStatus.Save)
                    hylEdit.NavigateUrl = "PaymentNotifyEdit.aspx?" + RequestName.PaymentID + "=" + e.Row.Cells[0].Text;
                else
                    hylEdit.Visible = false;

                if (projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                    hylEdit.Visible = false;
            }
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            Search();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        this.txtKey.Text = string.Empty;
        //this.ddlStatus.SelectedIndex = 0;
        Search();
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        DateTime beginDate = new DateTime(2005, 1, 1);
        DateTime endDate = DateTime.Now;

        IList<ESP.Finance.Entity.PaymentNotifyReporterInfo> list = ESP.Finance.BusinessLogic.PaymentNotifyReportManager.GetAllList();
        string retstr = ESP.Finance.BusinessLogic.PaymentNotifyReportManager.GetPaymentReport(list, "天津星言云汇网络科技有限公司");
        Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "window.open('/Tmp/TestProjectRecords.aspx?" + RequestName.BackUrl + "=" + Server.UrlEncode(retstr) + "','_self');", true);

    }
}
