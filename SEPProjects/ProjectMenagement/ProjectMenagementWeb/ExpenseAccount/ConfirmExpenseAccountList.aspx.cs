/************************************************************************\
 * 报销单确认收货 列表页
 *      
 * 如果是现场购买或现金借款报销单，有申请人需要确认收货的流程，则需要在此页操作
 *
\************************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.ExpenseAccount
{
    public partial class ConfirmExpenseAccountList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
            }
        }

        private void bindList()
        {

            List<ESP.Finance.Entity.ExpenseAccountExtendsInfo> list = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowItemsByRecive(int.Parse(CurrentUser.SysID), "", (int)ESP.Workflow.WorkItemStatus.Open);

            var q = from expense in list select expense;

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                q = q.Where(expense => expense.ProjectCode.Contains(txtKey.Text.Trim()) || expense.ReturnCode.Contains(txtKey.Text.Trim()) || expense.RequestEmployeeName.Contains(txtKey.Text.Trim()) || expense.PreFee.ToString().Contains(txtKey.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                q = q.Where(expense => expense.LastUpdateDateTime >= DateTime.Parse(txtBeginDate.Text.Trim() + " 0:00:00"));
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                q = q.Where(expense => expense.LastUpdateDateTime <= DateTime.Parse(txtEndDate.Text.Trim() + " 23:59:59"));
            }

            this.gvG.DataSource = q.ToArray<ESP.Finance.Entity.ExpenseAccountExtendsInfo>();
            this.gvG.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ExpenseAccountExtendsInfo model = (ESP.Finance.Entity.ExpenseAccountExtendsInfo)e.Row.DataItem;
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);

                Label labProjectName = (Label)e.Row.FindControl("labProjectName");
                if (project != null)
                    labProjectName.Text = project.BusinessDescription;

                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                hylEdit.NavigateUrl = model.WebPage + "&workitemid=" + model.WorkItemID.Value;

                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "Print/ExpensePrint.aspx?expenseID=" + model.ReturnID;
                }
                if (model.ReturnStatus >= (int)PaymentStatus.Submit)
                {
                    hylPrint.Visible = true;
                }
                else
                {
                    hylPrint.Visible = false;
                }

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(model.ReturnStatus.Value, model.ReturnType.Value);

                Label labRequestUserName = (Label)e.Row.FindControl("labRequestUserName");
                labRequestUserName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
                
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

    }
}
