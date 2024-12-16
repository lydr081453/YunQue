using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount
{
    public partial class ConfirmTicket : System.Web.UI.Page
    {

        ESP.Finance.Entity.ReturnInfo model = null;
        ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string returnId = crypto.DecrypString(Request["expenseID"].ToString().Trim()).Trim();
                
                initPrintPage(returnId);
                this.labSuggestion.Visible = false;
                if (!string.IsNullOrEmpty(returnId) && (string.IsNullOrEmpty(Request["Print"]) || Request["Print"].ToString().Trim() != "1"))
                {
                   
                }
                else
                {
                    this.lblResult.Visible = false;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string returnId = crypto.DecrypString(Request["expenseID"].ToString().Trim()).Trim();

            initPrintPage(returnId);
            this.labSuggestion.Visible = false;
            if (!string.IsNullOrEmpty(returnId) && (string.IsNullOrEmpty(Request["Print"]) || Request["Print"].ToString().Trim() != "1"))
            {

            }
            else
            {
                this.lblResult.Visible = false;
            }

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "window.print();", true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string returnId = crypto.DecrypString(Request["expenseID"].ToString().Trim()).Trim();
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> ticketlist = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            for (int i = 0; i < repExpense.Items.Count; i++)
            {
                TextBox txtFee = (TextBox)repExpense.Items[i].FindControl("txtFee");
                HiddenField hidExpenseId = (HiddenField)repExpense.Items[i].FindControl("hidExpenseId");
                ESP.Finance.Entity.ExpenseAccountDetailInfo ticketmodel = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetModel(int.Parse(hidExpenseId.Value));
                if (Convert.ToDecimal(txtFee.Text) > ticketmodel.GoAmount)
                {
                    this.lblResult.Text = "机票确认价格不能高于订单发出的价格!";
                    return;
                }
                ticketmodel.ExpenseMoney = Convert.ToDecimal(txtFee.Text);
                ticketlist.Add(ticketmodel);
            }
            int ret = ESP.Finance.BusinessLogic.ReturnManager.ConfirmTicketOrder(Convert.ToInt32(returnId), ticketlist);
            if (ret == 1)
            {
                this.lblResult.Text = "机票申请单确认完成!";
            }
            else if (ret == 2)
            {
                this.lblResult.Text = "机票申请单确认失败，请检查是否已经确认完毕!";
            }
            else
            {
                this.lblResult.Text = "机票申请单确认失败，请重试!";
            }
        }

        

        private void initPrintPage(string returnId)
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
            if (!string.IsNullOrEmpty(returnId))
            {
                int eid = int.Parse(returnId);
                model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(eid);

                lblPN.Text = model.ReturnCode;
                if (model.TicketNo != 0)
                    lblPN.Text += "-" + model.TicketNo.ToString();
                lblApplicantUser.Text = model.RequestUserName;
                lblProjectCode.Text = model.ProjectCode;
                lblDepartment.Text = model.DepartmentName;

                if (model.ParentID != null && model.ParentID.Value > 0)
                {
                    ESP.Finance.Entity.ReturnInfo refModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ParentID.Value);
                    this.lblRefPn.Text = "<font color='black'>原PN单号:</font>" + refModel.ReturnCode + "&nbsp;&nbsp;&nbsp;&nbsp;<font color='black'>原申请金额:</font>" + refModel.PreFee.Value.ToString("#,##0.00");
                }

                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + returnId + " and (TicketStatus is null or TicketStatus=0) ");
                lblTitle.Text = "机票申请单";

                lab_TotalPrice.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(eid).ToString();
                lblRequestDate.Text = model.CommitDate == null ? model.RequestDate.Value.ToString("yyyy-MM-dd") : model.CommitDate.Value.ToString("yyyy-MM-dd");

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && log.AuditType.Value == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                    else
                        labSuggestion.Text += log.AuditorEmployeeName + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }
            }
            else
            {
                list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            }

            repExpense.DataSource = list;
            repExpense.DataBind();

            logoImg.ImageUrl = "images/xingyan.png";



        }

        protected void repExpense_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detailModel = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Item.DataItem;
            Label labType = (Label)e.Item.FindControl("labType");
            if (labType != null)
            {
                if (detailModel.TripType == 2)
                    labType.Text = "退票申请";
                else
                {
                    labType.Text = "机票申请";
                }
            }
            HiddenField hidExpenseId = (HiddenField)e.Item.FindControl("hidExpenseId");
            if (hidExpenseId != null)
            {
                hidExpenseId.Value = detailModel.ID.ToString();
            }
            Label labBoarder = (Label)e.Item.FindControl("labBoarder");
            if (labBoarder != null)
            {
                labBoarder.Text = detailModel.Boarder;
            }
            Label lblMobile = (Label)e.Item.FindControl("lblMobile");
            if (lblMobile != null)
            {
                lblMobile.Text = detailModel.BoarderMobile;
            }

            Label labAirNo = (Label)e.Item.FindControl("labAirNo");
            if (labAirNo != null)
            {
                labAirNo.Text = detailModel.GoAirNo;
            }

            Label labID = (Label)e.Item.FindControl("labID");
            if (labID != null)
            {
                labID.Text = detailModel.BoarderIDCard;
            }

            Label labDFrom = (Label)e.Item.FindControl("labDFrom");
            if (labDFrom != null)
            {
                labDFrom.Text = detailModel.TicketSource;
            }

            Label labTo = (Label)e.Item.FindControl("labTo");
            if (labTo != null)
            {
                labTo.Text = detailModel.TicketDestination;
            }

            TextBox txtFee = (TextBox)e.Item.FindControl("txtFee");
            if (txtFee != null)
            {
                txtFee.Text = detailModel.ExpenseMoney.Value.ToString("#,##0.00");
            }

            Label labDate = (Label)e.Item.FindControl("labDate");
            if (labDate != null)
            {
                labDate.Text = detailModel.ExpenseDate.Value.ToString("yyyy-MM-dd");
            }
            Label labRemark = (Label)e.Item.FindControl("labRemark");
            if (labRemark != null)
            {
                labRemark.Text = detailModel.ExpenseDesc;
            }
        }
    }
}
