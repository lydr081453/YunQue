using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Edit
{
    public partial class OOPTabEdit : ESP.Web.UI.PageBase
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
            conditionStr += " and returnStatus in(0,1,136,138,-1,107,108)";
            if (this.ddlType.SelectedIndex == 0)
            {
                conditionStr += " and returnType in(30,31,32,33,34,35,36,37)";
            }
            else
            {
                conditionStr += " and returnType=" + ddlType.SelectedItem.Value;
            }

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

        void GridProject_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            //撤销付款申请
            int ID = int.Parse(e.Item["returnID"].ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(ID);

            if (returnModel.ReturnStatus > 1 && returnModel.ReturnStatus != 138)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('报销单无法删除！');", true);
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

        protected void lnkChox_Click(object sender, EventArgs e)
        {
            int returnID = int.Parse(hidValue.Value);
            ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
            int newReturnID = ESP.Finance.BusinessLogic.ExpenseAccountManager.CreateRecivingForm(model);
            if (newReturnID > 0)
            {
                Response.Redirect("/ExpenseAccount/WaitReceivingEdit.aspx?id=" + newReturnID);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('创建冲销单失败！');", true);
            }
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
                if (((returnModel.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && (returnModel.IsFixCheque == null || returnModel.IsFixCheque.Value == false))) && returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving)
                {
                    e.Item["StatusText"] = "已还款";
                }
                else
                    e.Item["StatusText"] = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(returnModel.ReturnStatus.Value, returnModel.ReturnType.Value);
                e.Item["RequestEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');\">" + e.Item["RequestEmployeeName"] + "</a>";


                if (returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && returnModel.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
                {
                    printPage = "/ExpenseAccount/Print/ExpensePrint.aspx?expenseID=" + returnModel.ReturnID;
                }
                else
                {
                    printPage = "/ExpenseAccount/Print/ThirdPartyPrint.aspx?expenseID=" + returnModel.ReturnID;
                }

                e.Item["Print"] = "<a target='_blank' href='" + printPage + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";

                e.Item["ReturnCode"] = "<a target='_blank' href=\"/ExpenseAccount/Display.aspx?id=" + returnModel.ReturnID.ToString() + "\">" + returnModel.ReturnCode + "</a>";

                e.Item["Delete"] = "";

                switch (returnModel.ReturnType)
                {
                    case 30:
                        e.Item["TypeName"] = "常规报销";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/ExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        e.Item["Chox"] = "";//无冲销
                        break;
                    case 31:
                        e.Item["TypeName"] = "支票/电汇付款";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/CashExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving && (returnModel.IsFixCheque != null && returnModel.IsFixCheque == true))
                            e.Item["Chox"] = "<a onclick=\"document.getElementById('" + hidValue.ClientID + "').value =" + returnModel.ReturnID + ";return confirm('您是否要创建冲销单？');\" href=\"javascript:__doPostBack('" + lnkChox.ClientID.Replace('_', '$') + "','');\" ><img src='/images/recipent_icon.gif' border='0' /></a>";//冲销
                        break;
                    case 32:
                        e.Item["TypeName"] = "现金借款";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/CashExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving)
                            e.Item["Chox"] = "<a onclick=\"document.getElementById('" + hidValue.ClientID + "').value =" + returnModel.ReturnID + ";return confirm('您是否要创建冲销单？');\" href=\"javascript:__doPostBack('" + lnkChox.ClientID.Replace('_', '$') + "','');\" ><img src='/images/recipent_icon.gif' border='0' /></a>";//冲销
                        break;
                    case 33:
                        e.Item["TypeName"] = "商务卡报销";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/CashExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        e.Item["Chox"] = "";
                        break;
                    case 36:
                    case 34:
                        e.Item["TypeName"] = "PR现金借款冲销";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/WaitReceivingEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        e.Item["Chox"] = "";
                        break;
                    case 35:
                        e.Item["TypeName"] = "第三方报销";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/ExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "'><img src='/images/edit.gif' border='0' /></a>";
                        e.Item["Chox"] = "";
                        break;
                    case 37:
                        e.Item["TypeName"] = "媒体预付申请";
                        e.Item["Edit"] = "<a href='/ExpenseAccount/CashExpenseAccountEdit.aspx?id=" + returnModel.ReturnID + "&Media=1'><img src='/images/edit.gif' border='0' /></a>";
                        e.Item["Chox"] = "";
                        break;
                }
                if (returnModel.ReturnStatus != (int)PaymentStatus.ConfirmReceiving && returnModel.ReturnStatus != (int)PaymentStatus.Rejected && returnModel.ReturnStatus != (int)PaymentStatus.Created && returnModel.ReturnStatus != (int)PaymentStatus.Save)
                {
                    e.Item["Edit"] = "";
                    e.Item["Delete"] = "";

                }
            }
        }
    }
}
