using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Utilities;

namespace FinanceWeb.ExpenseAccount.Print
{
    public partial class TicketBatchPrint : ESP.Web.UI.PageBase
    {
        decimal total;
        int detailcount;
        int detailindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.BatchID]))
            {
                int batchid = Convert.ToInt32(Request[RequestName.BatchID]);
                ESP.Finance.Entity.PNBatchInfo batchModel =ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchid);
                IList<ESP.Finance.Entity.ReturnInfo> list = ESP.Finance.BusinessLogic.ReturnManager.GetTicketBatch(batchid);
                repExpense.DataSource = list;
                repExpense.DataBind();

                lblTitle.Text = "机票申请单";
                lab_TotalPrice.Text = total.ToString("#,##0.00");
                lblRetAmount.Text = batchModel.TicketReturnPoint.ToString("#,##0.00");
                lblTotalAmount.Text = (total - batchModel.TicketReturnPoint).ToString("#,##0.00");
                this.lblAppDate.Text = batchModel.CreateDate.Value.ToString("yyyy-MM-dd");
                this.lblBranch.Text = batchModel.BranchCode;
                this.lblPN.Text = batchModel.PurchaseBatchCode;
                this.lblRequestor.Text = new ESP.Compatible.Employee(batchModel.CreatorID.Value).Name;

                this.lblSupplier.Text = batchModel.SupplierName;
                this.lblBank.Text = batchModel.SupplierBankName;
                this.lblAccount.Text = batchModel.SupplierBankAccount;
            }
        }

        protected void repExpense_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Item.DataItem;
                if (model != null)
                {
                    Label lblRequestor = (Label)e.Item.FindControl("lblRequestor");
                    Label lblReturnCode = (Label)e.Item.FindControl("lblReturnCode");
                    Label lblProjectCode = (Label)e.Item.FindControl("lblProjectCode");
                    Label lblRequestDate = (Label)e.Item.FindControl("lblRequestDate");
                    Label lblGroup = (Label)e.Item.FindControl("lblGroup");
                    Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                    Label lblLog = (Label)e.Item.FindControl("lblLog");
                    Label lblRefNo = (Label)e.Item.FindControl("lblRefNo");
                    Label lblRefAmount = (Label)e.Item.FindControl("lblRefAmount");

                    Repeater repDetail = (Repeater)e.Item.FindControl("repDetail");

                    if (model.ParentID != null && model.ParentID.Value > 0)
                    {
                        ESP.Finance.Entity.ReturnInfo refModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ParentID.Value);
                        lblRefNo.Text = refModel.ReturnCode;
                        if (refModel.TicketNo > 0)
                        {
                            lblRefNo.Text += "-" + refModel.TicketNo.ToString();
                        }
                        lblRefAmount.Text = refModel.PreFee.Value.ToString("#,##0.00");
                    }
                    lblRequestor.Text = model.RequestEmployeeName;
                    lblReturnCode.Text = model.ReturnCode;
                    if (model.TicketNo != 0)
                        lblReturnCode.Text += "-" + model.TicketNo.ToString();
                    lblProjectCode.Text = model.ProjectCode;
                    lblRequestDate.Text = model.RequestDate.Value.ToString("yyyy-MM-dd");
                    ESP.Compatible.Department dept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(model.DepartmentID.Value);
                    lblGroup.Text = dept.DepartmentName;
                    lblTotal.Text = model.PreFee.Value.ToString("#,##0.00");
                    total += model.PreFee.Value;
                    lblLog.Text = getLog(model);

                    IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> details = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTicketDetail(model.ReturnID);
                    detailcount = details.Count;
                    repDetail.DataSource = details;
                    repDetail.DataBind();
                }
           
        }

        protected void repDetail_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detailModel = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Item.DataItem;
            if (detailModel != null)
            {
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
                if (detailModel.TicketStatus == 1)
                    return;
                detailindex++;
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

                Label labFee = (Label)e.Item.FindControl("labFee");
                if (labFee != null)
                {
                    labFee.Text = detailModel.ExpenseMoney.Value.ToString("#,##0.00");
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
                if (detailindex != detailcount)
                {
                    Label lblLine = (Label)e.Item.FindControl("lblLine");
                    lblLine.Text = "<hr align=\"center\" width=\"630px\" size=\"1px\" color=\"#a5c2a5\" />";
                }
                else
                {
                    detailindex = 0;
                }
            }
        }

        private string getLog(ESP.Finance.Entity.ReturnInfo returnModel)
        {
            string logstr = string.Empty;

            List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + returnModel.ReturnID.ToString());
            foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
            {
                int audittype = log.AuditType == null ? 0 : log.AuditType.Value;
                ESP.Framework.Entity.EmployeeInfo emp = null;
                if (log.AuditorUserID != null && log.AuditorUserID.Value != 0)
                    emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                if (emp != null)
                    logstr += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + "&nbsp;&nbsp;&nbsp;" + log.Suggestion + "&nbsp;&nbsp;&nbsp;" + ((returnModel.RequestorID.Value == log.AuditorUserID.Value && audittype == 0) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + "&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                else
                    logstr += log.AuditorEmployeeName + "&nbsp;&nbsp;&nbsp;" + log.Suggestion + "&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
            }
            return logstr;
        }
    }
}
