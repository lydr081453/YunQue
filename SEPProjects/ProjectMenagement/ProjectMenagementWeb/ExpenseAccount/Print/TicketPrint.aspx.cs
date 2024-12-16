using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ExpenseAccount.Print
{
    public partial class TicketPrint : System.Web.UI.Page
    {
        ESP.Finance.Entity.ReturnInfo model = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
                string url = HttpContext.Current.Request.Url.ToString();
                string no_http = url.Substring(url.IndexOf("//") + 2);
                string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1) + "ExpenseAccount/";
            }
            initPrintPage();
        }

        private void initPrintPage()
        {
            List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
            if (!string.IsNullOrEmpty(Request["expenseID"]))
            {
                int eid = int.Parse(Request["expenseID"].ToString());
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

                list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + Request["expenseID"] + " and (TicketStatus is null or TicketStatus=0) ");
                lblTitle.Text += "机票申请单";

                lab_TotalPrice.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(eid).ToString();
                lblRequestDate.Text = model.CommitDate == null ? model.RequestDate.Value.ToString("yyyy-MM-dd") : model.CommitDate.Value.ToString("yyyy-MM-dd");

                List<ESP.Finance.Entity.ExpenseAuditDetailInfo> auditLogs = ESP.Finance.BusinessLogic.ExpenseAuditDetailManager.GetList(" and ExpenseAuditID = " + model.ReturnID);
                foreach (ESP.Finance.Entity.ExpenseAuditDetailInfo log in auditLogs)
                {
                    if (log == null)
                        continue;
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID.Value);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorUserID.Value && (log.AuditType==null ||log.AuditType.Value == 0)) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditeStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
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
            if (detailModel.TicketStatus == 1)
                return;

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

            Label lblCardType = (Label)e.Item.FindControl("lblCardType");
            if (lblCardType != null)
            {
                lblCardType.Text = detailModel.BoarderIDType;
            }
        }

    }
}
