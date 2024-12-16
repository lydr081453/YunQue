using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Search
{
    public partial class TicketList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                Search();
            }

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
            GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        void GridProject_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridProject.CurrentPageIndex = e.NewIndex;
        }
        private string GetDelegateUser()
        {
            string users = string.Empty;
            string users2 = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            users2 = GetFinanceDelegateUser();
            if (!string.IsNullOrEmpty(users2))
                users += "," + users2;
            return users;
        }

        private string GetFinanceDelegateUser()
        {
            string users = string.Empty;
            DataTable dt = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList("backupuserid=" + CurrentUser.SysID + " and type=3").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    users += dt.Rows[i]["UserID"].ToString().Trim() + ",";
                }
            }
            return users.TrimEnd(',');
        }

        private void Search()
        {
            string Branchs = string.Empty;
            IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + CurrentUser.SysID + ",%'");
            if (branchList != null && branchList.Count > 0)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += "'" + b.BranchCode + "',";
                }
            }
            Branchs = Branchs.TrimEnd(',');

            IList<ESP.Finance.Entity.BranchDeptInfo> branchDeptList = ESP.Finance.BusinessLogic.BranchDeptManager.GetList(CurrentUserID);

            string bdsql = string.Empty;

            if (branchDeptList != null && branchDeptList.Count > 0)
            {
                foreach (ESP.Finance.Entity.BranchDeptInfo dept in branchDeptList)
                {
                    bdsql = "(branchcode='" + dept.BranchCode + "' and departmentid="+dept.DeptId+") or ";
                }

                bdsql = bdsql.Substring(0,bdsql.Length-3);
            }
            

            string conditionStr = string.Empty;
            conditionStr += string.Format(" (RequestorID = {0} or departmentid in(select depid from sep_operationauditmanage where directorid={0} or managerid={0} or faid={0})", CurrentUser.SysID);
            if (!string.IsNullOrEmpty(Branchs))
            {
                conditionStr += " or branchcode in(" + Branchs + ")";
            }

            if (!string.IsNullOrEmpty(bdsql))
            {
                conditionStr += " or (" + bdsql + ")";
            }

            conditionStr += ")";

            conditionStr += " and returnType=40";

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                conditionStr += string.Format(" and ((ProjectCode + ReturnCode +  RequestEmployeeName +  CAST(PreFee AS nvarchar) like '%{0}%') or (returnid in(select returnid from F_ExpenseAccountDetail where boarder like '%{0}%'))) ", txtKey.Text.Trim());
            }

            if (ddlStatus.SelectedValue.Equals("0"))
            {
                conditionStr += " and returnStatus not in(0,1,-1)";
            }
            else
            {
                conditionStr += " and returnStatus in(" + ddlStatus.SelectedItem.Value + ")";
            }

            List<ESP.Finance.Entity.ReturnInfo> list = (List<ESP.Finance.Entity.ReturnInfo>)ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr);

            this.GridProject.DataSource = list;
            this.GridProject.DataBind();
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
                e.Item["StatusText"] = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, returnModel.ReturnType.Value, returnModel.IsDiscount);
                e.Item["RequestEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(returnModel.RequestorID.Value) + "');\">" + e.Item["RequestEmployeeName"] + "</a>";
                string auditor = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeNameByWorkItemID(returnModel.ReturnID, (int)ESP.Workflow.WorkItemStatus.Open, dataContext);
                if (returnModel.ReturnStatus == 100)
                    auditor = "前台";
                else if (returnModel.ReturnStatus == 107)
                    auditor = "供应商";
                e.Item["Auditor"] = auditor;

                printPage = "/ExpenseAccount/Print/TicketPrint.aspx?expenseID=" + returnModel.ReturnID;
                if (returnModel.ReturnStatus >= (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm)
                    e.Item["Print"] = "<a target='_blank' href='" + printPage + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
                else
                    e.Item["Print"] = "";
                e.Item["ReturnCode"] = "<a target='_blank' href=\"/ExpenseAccount/Display.aspx?id=" + returnModel.ReturnID.ToString() + "\">" + returnModel.ReturnCode + "</a>";
            }
        }
    }
}
