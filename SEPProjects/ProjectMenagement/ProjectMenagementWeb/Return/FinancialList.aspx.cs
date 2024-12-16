using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class Return_FinancialList : ESP.Web.UI.PageBase
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
        string DelegateUsers = string.Empty;
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            DelegateUsers += model.UserID.ToString() + ",";
        }
        DelegateUsers = DelegateUsers.TrimEnd(',');

        if (this.ddlStatus.SelectedIndex == 0 || this.ddlStatus.SelectedIndex == 1)
        {
            if(!string.IsNullOrEmpty(DelegateUsers))
                term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where (AuditorUserID=@AuditorUserID or AuditorUserID in("+DelegateUsers+")) and AuditType=@AuditType)";
            else
                term = " PaymentStatus=@PaymentStatus and PaymentID in (select PaymentID from F_PaymentAuditHist where AuditorUserID=@AuditorUserID and AuditType=@AuditType)";
            SqlParameter p1 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
            p1.SqlValue = (int)ReturnStatus.MajorCommit;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p2.SqlValue = Convert.ToInt32(CurrentUser.SysID);
            paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@AuditType", SqlDbType.Int, 4);
            p3.SqlValue = (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
            paramlist.Add(p3);
        }
        else
        {
            term = " PaymentStatus=@PaymentStatus";
            SqlParameter p4 = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
            p4.SqlValue = this.ddlStatus.SelectedValue;
            paramlist.Add(p4);
        }
        if (!string.IsNullOrEmpty(term))
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text))
            {
                term += " and (PaymentCode like '%'+@Key+'%' or ProjectCode like '%'+@Key+'%' or PaymentContent like '%'+@Key+'%' )";
                SqlParameter p5 = new SqlParameter("@Key", SqlDbType.NVarChar, 50);
                p5.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p5);
            }
            IList<ESP.Finance.Entity.PaymentInfo> paymentList = ESP.Finance.BusinessLogic.PaymentManager.GetList(term, paramlist);
           // var tmplist = paymentList.OrderBy(N => N.Lastupdatetime);
            this.gvG.DataSource = paymentList;
            this.gvG.DataBind();
        }
    }


    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.PaymentInfo model =null;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(e.Row.Cells[1].Text));
            model = (ESP.Finance.Entity.PaymentInfo)e.Row.DataItem;

            Label lblResponser = (Label)e.Row.FindControl("lblResponser");
            if (lblResponser != null)
            {
                lblResponser.Text = projectModel.ApplicantEmployeeName;
                lblResponser.Attributes["onclick"] = "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectModel.ApplicantUserID) + "');";
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
                        lblStatus.Text = "总监确认";
                        break;
                    case (int)ReturnStatus.FinancialHold:
                        lblStatus.Text = "已挂账";
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
            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            HyperLink hylSign = (HyperLink)e.Row.FindControl("hylSign");
            if (hylAudit != null)
            {
                if (model.PaymentStatus == (int)ReturnStatus.MajorCommit)
                {
                    hylAudit.NavigateUrl = "FinancialOperation.aspx?" + RequestName.PaymentID + "=" + e.Row.Cells[0].Text;
                    this.gvG.Columns[13].Visible = true;
                }
                else
                {
                    this.gvG.Columns[13].Visible = false;
                    hylAudit.Visible = false;
                }
            }
            if (hylSign != null)
            {
                if (model.PaymentStatus == (int)ReturnStatus.FinancialHold || model.PaymentStatus == (int)ReturnStatus.FinancialOver)
                {
                    hylSign.NavigateUrl = "FinancialOperation.aspx?" + RequestName.PaymentID + "=" + e.Row.Cells[0].Text;
                    this.gvG.Columns[14].Visible = true;
                }
                else
                {
                    hylSign.Visible = false;
                    this.gvG.Columns[14].Visible = false;
                }
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
        this.ddlStatus.SelectedIndex = 0;
        Search();
    }
}