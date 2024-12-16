/************************************************************************\
 * 报销单打印页
 *      
 * 
 *
\************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;
public partial class ExpenseAccount_Print_ExpensePrint : ESP.Web.UI.PageBase
{
    ESP.Finance.Entity.ReturnInfo model = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        initPrintPage();
    }

    private void initPrintPage()
    {
        List<ESP.Finance.Entity.ExpenseAccountDetailInfo> list = null;
        if (!string.IsNullOrEmpty(Request["expenseID"]))
        {
            int eid = int.Parse(Request["expenseID"].ToString());
            model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(eid);

            lblPN.Text = model.ReturnCode;
            litProjectCode.Text = model.ProjectCode;
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            if (model.DepartmentID != null && model.DepartmentID != 0)
            {
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(model.DepartmentID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    litGroup.Text = groupname.Substring(0, groupname.Length - 1);
            }
            litRec.Text = model.RequestEmployeeName + "<br/>" + model.RequestUserCode; ;
            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo creatorModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.RequestorID.Value);
                litBank.Text = creatorModel.SalaryBank;
                litAccount.Text = creatorModel.SalaryCardNo;
            }
            if (model.ParentID != null && model.ParentID.Value > 0)
            {
                ESP.Finance.Entity.ReturnInfo refModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(model.ParentID.Value);
                this.lblRefPn.Text = "<font color='black'>原PN单号：</font>" + refModel.ReturnCode + "&nbsp;&nbsp;&nbsp;&nbsp;<font color='black'>原申请金额：</font>" + refModel.PreFee.Value.ToString("#,##0.00");
            }
            else
            {
                IList<ESP.Finance.Entity.ReturnInfo> reflist = ESP.Finance.BusinessLogic.ReturnManager.GetList(" parentid=" + model.ReturnID);
                if (reflist != null && reflist.Count > 0)
                {
                    this.lblRefPn.Text = "<font color='black'>冲销单号：</font>" + reflist[0].ReturnCode + "&nbsp;&nbsp;&nbsp;&nbsp;<font color='black'>冲销金额：</font>" + reflist[0].PreFee.Value.ToString("#,##0.00");
                }
            }

            list = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and ReturnID = " + Request["expenseID"]);
            this.lblTitle.Text = "";//model.RequestDate.Value.Year + "年" + model.RequestDate.Value.Month + "月";

            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
                lblTitle.Text += "报销单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                lblTitle.Text += "现金借款单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
                lblTitle.Text += "支票/电汇付款单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
                lblTitle.Text += "商务卡报销单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
                lblTitle.Text += "PR现金借款冲销";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty)
                lblTitle.Text += "第三方报销单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
                lblTitle.Text += "借款冲销单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                lblTitle.Text += "机票申请单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                lblTitle.Text += "媒体预付申请单";

            lab_TotalPrice.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(eid).ToString();
            lblRequestDate.Text = model.CommitDate == null ? model.RequestDate.Value.ToString("yyyy-MM-dd") : model.CommitDate.Value.ToString("yyyy-MM-dd");

            var auditLogs = ESP.Finance.BusinessLogic.AuditLogManager.GetOOPList(model.ReturnID);
            foreach (var log in auditLogs)
            {
                labSuggestion.Text += log.AuditorEmployeeName + "(" + log.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorSysID.Value) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
            }
        }
        else
        {
            list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
        }

        if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
        {
            var tmplist = from item in list
                          where item.TicketStatus == null || item.TicketStatus == 0
                          select item;
            list = tmplist.ToList();
        }
        repExpense.DataSource = list;
        repExpense.DataBind();

        if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
        {
            if (list == null || list.Count == 0)
            {
                List<ESP.Finance.Entity.ReturnInfo> returnList = new List<ESP.Finance.Entity.ReturnInfo>();
                returnList.Add(model);
                repReceiving.DataSource = returnList;
                repReceiving.DataBind();
                lab_TotalPrice.Text = model.PreFee.Value.ToString("#,##0.00");
            }
            else
            {
                repReceiving.Visible = false;
            }

        }
        else
        {
            repReceiving.Visible = false;
        }

        logoImg.ImageUrl = "images/xingyan.png";



    }

    protected void repExpense_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ESP.Finance.Entity.ExpenseAccountDetailInfo detailModel = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Item.DataItem;


        Label labReturnFactDate = (Label)e.Item.FindControl("labReturnFactDate");
        if (labReturnFactDate != null)
        {
            labReturnFactDate.Text = detailModel.ExpenseDate.Value.ToString("yyyy-MM-dd");
        }


        Label lanExpenseType = (Label)e.Item.FindControl("lanExpenseType");
        if (lanExpenseType != null)
        {
            lanExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value).ExpenseType;
        }


        Label labReturnContent = (Label)e.Item.FindControl("labReturnContent");
        if (labReturnContent != null)
        {
            string mobileInfo = string.Empty;

            string typeStr = string.Empty;

            if (detailModel.ExpenseType != null)
            {
                ESP.Finance.Entity.ExpenseTypeInfo typeModel = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value);
                typeStr = typeModel.ExpenseType + ":";
            }

            if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
            {
               
                labReturnContent.Text = typeStr  + detailModel.ExpenseDesc + "(" + detailModel.Boarder + ":" + (detailModel.TripType == 0 ? "单程" : "往返") + " " + detailModel.TicketSource + "-" + detailModel.TicketDestination + ";)";
            }
            else
            {
                if (detailModel.ExpenseType.Value == Convert.ToInt32(ESP.Configuration.ConfigurationManager.SafeAppSettings["ExpenseType_Cellphone"]))
                {

                    ESP.Finance.Entity.MobileListInfo mobile = ESP.Finance.BusinessLogic.MobileListManager.GetModel(detailModel.Creater.Value);
                    if (mobile != null)
                    {
                        mobileInfo = "<font color='red' style='font-style:italic'>从 " + mobile.EndDate.Year + "年" + mobile.EndDate.Month + "月起享受话费补贴</font>";
                   
                    }
                }
                labReturnContent.Text =typeStr+ detailModel.ExpenseDesc + mobileInfo;
            }
        }



        Label labPreFee = (Label)e.Item.FindControl("labPreFee");
        if (labPreFee != null)
        {
            labPreFee.Text = detailModel.ExpenseMoney.Value.ToString();
        }
        Literal liter = (Literal)e.Item.FindControl("liter");
        if (!string.IsNullOrEmpty(detailModel.Recipient) && !string.IsNullOrEmpty(detailModel.BankName) && !string.IsNullOrEmpty(detailModel.BankAccountNo))
        {
            liter.Text = "<tr><td align='left' height='20px' class='f12pxGgray' style=' font-size:12px;'>收款方:</td><td align='left' colspan='7' class='f12pxGgray_right' style='font-size:12px;'>" + detailModel.Recipient + "</td></tr>";
            liter.Text += "<tr><td align='left' height='20px' class='f12pxGgray' style=' font-size:12px;' >城市:</td><td align='left' colspan='7' class='f12pxGgray_right' style='font-size:12px;'>" + detailModel.City + "</td></tr>";
            liter.Text += "<tr><td align='left' height='20px' class='f12pxGgray' style=' font-size:12px;'>开户行:</td><td align='left' colspan='7' class='f12pxGgray_right' style='font-size:12px;'>" + detailModel.BankName + "</td></tr>";
            liter.Text += "<tr><td align='left' height='20px' class='f12pxGgray' style=' font-size:12px;'>银行帐号:</td><td align='left' colspan='7' class='f12pxGgray_right' style='font-size:12px;'>" + detailModel.BankAccountNo + "</td></tr>";
        }
    }

    protected void repReceiving_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ESP.Finance.Entity.ReturnInfo detailModel = (ESP.Finance.Entity.ReturnInfo)e.Item.DataItem;

        Label labProjectCode = (Label)e.Item.FindControl("labProjectCode");
        if (labProjectCode != null)
        {
            labProjectCode.Text = model.ProjectCode;
        }

        Label labReturnFactDate = (Label)e.Item.FindControl("labReturnFactDate");
        if (labReturnFactDate != null)
        {
            labReturnFactDate.Text = detailModel.PreBeginDate == null ? "" : detailModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
        }

        Label labRequestorUserName = (Label)e.Item.FindControl("labRequestorUserName");
        if (labRequestorUserName != null)
        {
            labRequestorUserName.Text = model.RequestEmployeeName + "<br/>" + model.RequestUserCode;
        }

        Label lanExpenseType = (Label)e.Item.FindControl("lanExpenseType");
        if (lanExpenseType != null)
        {
            //lanExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value).ExpenseType;
        }

        Label labDepartment = (Label)e.Item.FindControl("labDepartment");
        if (labDepartment != null)
        {
            labDepartment.Text = model.DepartmentName;
        }

        Label labReturnContent = (Label)e.Item.FindControl("labReturnContent");
        if (labReturnContent != null)
        {
            labReturnContent.Text = detailModel.ReturnContent;
        }

        Label labPreFee = (Label)e.Item.FindControl("labPreFee");
        if (labPreFee != null)
        {
            labPreFee.Text = detailModel.PreFee.Value.ToString();
        }
    }
}
