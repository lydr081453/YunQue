using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace FinanceWeb.Edit
{
    public partial class TicketReceipient : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                Search();
            }

        }

        private void Search()
        {
            string conditionStr = string.Empty;
            conditionStr += string.Format(" RequestorID = {0}", CurrentUser.SysID);
            conditionStr += " and returnStatus in(0,1,-1)";
            conditionStr += " and returnType=40";

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                conditionStr += string.Format(" and ProjectCode + ReturnCode +  RequestEmployeeName +  CAST(PreFee AS nvarchar) like '%{0}%' ", txtKey.Text.Trim());
            }

            List<ESP.Finance.Entity.ReturnInfo> list = (List<ESP.Finance.Entity.ReturnInfo>)ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr);

            this.GridProject.DataSource = list;
            this.GridProject.DataBind();
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            GridProject.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(GridProject_DeleteCommand);
            GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
            GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);

        }

        void GridProject_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridProject.CurrentPageIndex = e.NewIndex;
        }

        protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Item.DataItem;
            string printPage = string.Empty;
            if (returnModel.DepartmentID != null)
            {
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                if (returnModel.DepartmentID != null && returnModel.DepartmentID != 0)
                {
                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(returnModel.DepartmentID.Value, depList);
                    string groupname = string.Empty;
                    foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                    {
                        groupname += dept.DepartmentName + "-";
                    }
                    if (!string.IsNullOrEmpty(groupname))
                        e.Item["DepartmentName"] = groupname.Substring(0, groupname.Length - 1);
                }
                e.Item["StatusText"] = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(returnModel.ReturnStatus.Value,returnModel.ReturnType.Value);
                printPage = "/ExpenseAccount/Print/TicketMail.aspx?expenseID=" + returnModel.ReturnID;
                e.Item["Print"] = "<a target='_blank' href='" + printPage + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
                e.Item["ReturnCode"] = "<a target='_blank' href=\"/ExpenseAccount/Display.aspx?id=" + returnModel.ReturnID.ToString() + "\">" + returnModel.ReturnCode + "</a>";
                if (returnModel.ReturnStatus == 0 || returnModel.ReturnStatus == 1 || returnModel.ReturnStatus == -1)
                {
                    e.Item["Edit"] = "<a href='/ExpenseAccount/TicketEdit.aspx?id=" + returnModel.ReturnID +"'><img src='/images/edit.gif' border='0' /></a>";
                }
                if (returnModel.ReturnStatus < 108)
                {
                    e.Item["Print"] = "";
                }
                else
                {
                    e.Item["Print"] = "<a target='_blank' href='/ExpenseAccount/Print/TicketMail.aspx?expenseId=" + returnModel.ReturnID.ToString() + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
                }
                //if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm)
                //{
                //    e.Item["Confirm"] = "<a href='/ExpenseAccount/TicketConfirm.aspx?id=" + returnModel.ReturnID + "'><img src='/images/Audit.gif' border='0' /></a>";
                //}
                //else
                //{
                //    e.Item["Confirm"] = "";
                //}

            }
        }

        void GridProject_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            //撤销付款申请
            int ID = int.Parse(e.Item["returnID"].ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(ID);
            if (returnModel.ReturnStatus != -1 && returnModel.ReturnStatus != 1 && returnModel.ReturnStatus != 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('机票申请已经过确认，无法删除！');", true);
                return;
            }
            if (ESP.Finance.BusinessLogic.ReturnManager.Delete(ID) == DeleteResult.Succeed)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
            }
        }

    }
}
