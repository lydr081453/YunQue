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
public partial class ExpenseAccount_Print_ThirdPartyPrint : ESP.Web.UI.PageBase
{
    ESP.Finance.Entity.ReturnInfo model = null;
    decimal tempTotal = 0;
 
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

            //lblPN.Text = model.ReturnCode;

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
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_AirTicket)
                lblTitle.Text += "机票申请单";
            else if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                lblTitle.Text += "媒体预付申请单";

            var auditLogs = ESP.Finance.BusinessLogic.AuditLogManager.GetOOPList(model.ReturnID);
            foreach (var log in auditLogs)
            {
                labSuggestion.Text += log.AuditorEmployeeName + "(" + log.AuditorUserName + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ((model.RequestorID.Value == log.AuditorSysID.Value) ? "提交" : ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Names[log.AuditStatus.Value]) + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
            }

            litPNno.Text = model.ReturnCode;
            litDate.Text = model.CommitDate == null ? model.RequestDate.Value.ToString("yyyy-MM-dd") : model.CommitDate.Value.ToString("yyyy-MM-dd");
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
            litTotal.Text = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalMoneyByReturnID(eid).ToString("#,##0.00"); ;
        }
        else
        {
            list = new List<ESP.Finance.Entity.ExpenseAccountDetailInfo>();
        }

        //repExpense.DataSource = list;
        //repExpense.DataBind();

        rep1.DataSource = list;
        rep1.DataBind();

        logoImg.ImageUrl = "/images/xingyan.png";
        
    }

    protected void repExpense_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ESP.Finance.Entity.ExpenseAccountDetailInfo detailModel = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Item.DataItem;

        Label labProjectCode = (Label) e.Item.FindControl("labProjectCode");
        if(labProjectCode!=null)
        {
            labProjectCode.Text = model.ProjectCode;
        }

        Label labReturnFactDate = (Label) e.Item.FindControl("labReturnFactDate");
        if(labReturnFactDate!=null)
        {
            labReturnFactDate.Text = detailModel.ExpenseDate.Value.ToString("yyyy-MM-dd");
        }

        Label labRequestorUserName = (Label) e.Item.FindControl("labRequestorUserName");
        if(labRequestorUserName!=null)
        {
            labRequestorUserName.Text = model.RequestEmployeeName + "<br/>" + model.RequestUserCode;
        }

        Label lanExpenseType = (Label)e.Item.FindControl("lanExpenseType");
        if (lanExpenseType != null)
        {
            lanExpenseType.Text = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value).ExpenseType;
        }

        Label labDepartment = (Label) e.Item.FindControl("labDepartment");
        if(labDepartment!=null)
        {
            labDepartment.Text = model.DepartmentName;
        }

        Label labReturnContent = (Label) e.Item.FindControl("labReturnContent");
        if(labReturnContent!=null)
        {
            string typeStr = string.Empty;

            if (detailModel.ExpenseType != null)
            {
                ESP.Finance.Entity.ExpenseTypeInfo typeModel = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value);
                typeStr = typeModel.ExpenseType + ":";
            }

            labReturnContent.Text =typeStr+ detailModel.ExpenseDesc;
        }

        Label labPreFee = (Label)e.Item.FindControl("labPreFee");
        if (labPreFee != null)
        {
            labPreFee.Text = detailModel.ExpenseMoney.Value.ToString();
        }

        Label labRecipient = (Label)e.Item.FindControl("labRecipient");
        Label labCity = (Label)e.Item.FindControl("labCity");
        Label labBankName = (Label)e.Item.FindControl("labBankName");
        Label labBankAccountNo = (Label)e.Item.FindControl("labBankAccountNo");
        labRecipient.Text = detailModel.Recipient;
        labCity.Text = detailModel.City;
        labBankName.Text = detailModel.BankName;
        labBankAccountNo.Text = detailModel.BankAccountNo;
    }

    ESP.Finance.Entity.ExpenseAccountDetailInfo tmpModel = null;
    protected void rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detailModel = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Item.DataItem;
            Literal litRemark = (Literal)e.Item.FindControl("litRemark");
            Literal litDate = (Literal)e.Item.FindControl("litDate");
            Literal litPrice = (Literal)e.Item.FindControl("litPrice");
            Literal litDemo = (Literal)e.Item.FindControl("litDemo");

            string typeStr = string.Empty;

            if (detailModel.ExpenseType != null)
            {
                ESP.Finance.Entity.ExpenseTypeInfo typeModel = ESP.Finance.BusinessLogic.ExpenseTypeManager.GetModel(detailModel.ExpenseType.Value);
                typeStr = typeModel.ExpenseType + ":";
            }

            litRemark.Text = typeStr + detailModel.ExpenseDesc;
            litDate.Text = detailModel.ExpenseDate.Value.ToString("yyyy-MM-dd");
            litPrice.Text = detailModel.ExpenseMoney.Value.ToString("#,##0.00");

            if (tmpModel != null && tmpModel.Recipient != detailModel.Recipient && tmpModel.BankAccountNo != detailModel.BankAccountNo)
            {
                litDemo.Text += "<tr style='height:20px'><td align='right' class='test_title_right' colspan='2'>小计：</td><td  align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tempTotal.ToString("#,##0.00") + "</td></tr>";
                tempTotal = detailModel.ExpenseMoney.Value;
                litDemo.Text += "<tr style='height:20px'><td align='right' class='test_title_right'>收款方：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.Recipient + "</td></tr><tr style='height:20px'><td align='right' class='test_title_right'>所在城市：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.City + "</td></tr><tr style='height:20px'><td align='right' class='test_title_right'>银行名称：</td><td colspan='2' align='right' class='f12pxGgray_right'  style='font-size: 12px;'>" + tmpModel.BankName + "</td><tr><tr style='height:20px'><td align='right' class='test_title_right'>银行账号：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.BankAccountNo + "</td><tr>";
            }
            else
            {
                tempTotal += detailModel.ExpenseMoney.Value;
            }
            tmpModel = detailModel;
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            Literal litDemo = (Literal)e.Item.FindControl("litDemo");
            litDemo.Text += "<tr style='height:20px'><td align='right' class='test_title_right' colspan='2'>小计：</td><td  align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tempTotal.ToString("#,##0.00") + "</td></tr>";
            tempTotal = 0;
            litDemo.Text += "<tr style='height:20px'><td align='right' class='test_title_right'>收款方：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.Recipient + "</td></tr><tr style='height:20px'><td align='right' class='test_title_right'>所在城市：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.City + "</td></tr><tr style='height:20px'><td align='right' class='test_title_right'>银行名称：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.BankName + "</td><tr><tr style='height:20px'><td align='right' class='test_title_right'>银行账号：</td><td colspan='2' align='right' class='f12pxGgray_right' style='font-size: 12px;'>" + tmpModel.BankAccountNo + "</td><tr>";
        }
    }
}
