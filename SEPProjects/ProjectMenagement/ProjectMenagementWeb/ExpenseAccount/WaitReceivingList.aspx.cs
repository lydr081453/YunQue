﻿/************************************************************************\
 * 个人报销单列表页
 *      
 * 只显示自己提交的报销单列表
 * 
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
    public partial class WaitReceivingList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkItem workItem = null;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        int ReturnType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ReturnType"]))
            {
                ReturnType = Convert.ToInt32(Request["ReturnType"]);
            }
            initReturnTypeName();
            if (!IsPostBack)
            {
                bindList();
            }
        }

        protected void initReturnTypeName()
        {
            if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
            {
                labHeadText.Text = "现金借款单列表";
                
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic)
            {
                labHeadText.Text = "支票/电汇付款列表";
                
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountBusinessCard)
            {
                labHeadText.Text = "商务卡报销单列表";
                
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff)
            {
                labHeadText.Text = "PR现金借款冲销单列表";
                
            }
            else if (ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving)
            {
                labHeadText.Text = "借款冲销单列表";
            }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("WaitReceivingEdit.aspx?ReturnType=" + ReturnType);
        }

        private void bindList()
        {
            string conditionStr = string.Empty;
            conditionStr += string.Format(" RequestorID = {0} and ReturnType in ({1}) ", CurrentUser.SysID, (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountPRWriteOff + "," + (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountWaitReceiving);
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                conditionStr += string.Format(" and ProjectCode + ReturnCode +  RequestEmployeeName +  CAST(PreFee AS nvarchar)  like '%{0}%' ", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' <= Convert(Char(10),RequestDate,120) ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' >= Convert(Char(10),RequestDate,120) ", txtEndDate.Text.Trim());
            }
            if (!ddlStatus.SelectedValue.Equals("-1"))
            { 
                
            }
            
            List<ESP.Finance.Entity.ReturnInfo> list = (List<ESP.Finance.Entity.ReturnInfo>) ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr);

            this.gvG.DataSource = list;
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
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                ESP.Finance.Entity.ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(model.ProjectID.Value);

                Label labProjectName = (Label)e.Row.FindControl("labProjectName");
                if(project!=null)
                    labProjectName.Text = project.BusinessDescription;


                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");

                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

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
                lblStatus.Text = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(model.ReturnStatus.Value,model.ReturnType.Value);

                if (model.ReturnStatus == (int)PaymentStatus.ConfirmReceiving)
                {
                    hylEdit.NavigateUrl = "WaitReceivingEdit.aspx?id=" + model.ReturnID;
                    hylEdit.ImageUrl = "/images/edit.gif";
                }
                else
                {
                    hylEdit.Visible = false;
                    //btnDelete.Visible = false;
                }

                Label labRequestUserName = (Label)e.Row.FindControl("labRequestUserName");
                labRequestUserName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");


                Label lblAssigneeName = (Label)e.Row.FindControl("lblAssigneeName");
                lblAssigneeName.Text = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeNameByWorkItemID(model.ReturnID, (int)ESP.Workflow.WorkItemStatus.Open, dataContext);
               
                
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //if (e.CommandName == "Del")
            //{
            //    //撤销付款申请
            //    int ID = int.Parse(e.CommandArgument.ToString());
            //    if (ESP.Finance.BusinessLogic.ReturnManager.Delete(ID) == DeleteResult.Succeed)
            //    {
            //        bindList();
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
            //    }
            //    else
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
            //    }
            //}
        }

    }
}
