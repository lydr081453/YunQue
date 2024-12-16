using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Linq;

namespace FinanceWeb.project
{
    public partial class OOPTabList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.GridProject.CausedCallback)
            {
                BindInfo();
                Search();
            }

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GridProject.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridProject_NeedRebind);
            GridProject.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridProject_PageIndexChanged);
           // GridProject.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(GridProject_NeedDataSource);

        }

        //void GridProject_NeedDataSource(object sender, EventArgs e)
        //{
        //    GridCallBack();
        //}
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

        private string getGroupIds()
        {
            string managerid = string.Empty;
            //if (CurrentUser.SysID == System.Configuration.ConfigurationSettings.AppSettings["TCGAssistant"].ToString().Trim())
            //{
            //    managerid = System.Configuration.ConfigurationSettings.AppSettings["TCGManager"].ToString().Trim();
            //}
            //else
                managerid = CurrentUser.SysID;
            string str = string.Format(" (directorid={0} or managerid={0} or ceoid ={0} or faid={0}) and depid>0 ", managerid);
            DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(str);

            string groupids = string.Empty;
            if (ds != null && ds.Tables[0] != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    groupids += ds.Tables[0].Rows[i]["depid"].ToString() + ",";
                }
            }
            return groupids.TrimEnd(',');
        }

        private void Search()
        {
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
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
            string groupids = getGroupIds();
            string conditionStr = string.Empty;
            conditionStr += string.Format(" (RequestorID = {0}", CurrentUser.SysID);
            if (!string.IsNullOrEmpty(Branchs))
            {
                conditionStr += "or branchcode in(" + Branchs + ")";
            }
            if (!string.IsNullOrEmpty(groupids))
            {
                conditionStr += "or departmentid in(" + groupids + ")";
            }
            conditionStr += ")";

            bool ischoose = false;


            if (this.ddlType.SelectedIndex == 0)
            {
                conditionStr += " and returnType in(30,31,32,33,35,34,36,37)";
            }
            else
            {
                ischoose = true;
                conditionStr += " and returnType=" + ddlType.SelectedItem.Value;
            }
            
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                ischoose = true;
                conditionStr += string.Format(" and ((ProjectCode + ReturnCode +  RequestEmployeeName +  CAST(PreFee AS nvarchar) like '%{0}%') or returnId in(select returnId from F_ExpenseAccountDetail where ExpenseDesc like  '%{0}%')) ", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProjectCode.Text.Trim()))
            {
                conditionStr += string.Format(" and ProjectCode like '%{0}%'  ", txtProjectCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtRequestEmployeeName.Text.Trim()))
            {
                conditionStr += string.Format(" and RequestEmployeeName like '%{0}%'  ", txtRequestEmployeeName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBegin.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' <= Convert(Char(10),RequestDate,120) ", txtBegin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
            {
                conditionStr += string.Format(" and '{0}' >= Convert(Char(10),RequestDate,120) ", txtEnd.Text.Trim());
            }
            if (ddlDepartment3.SelectedValue != "-1")
            {
                conditionStr += " and departmentid=@GroupId";
                paramlist.Add(new SqlParameter("@GroupId", ddlDepartment3.SelectedValue));
            }
            else
            {
                if (ddlDepartment2.SelectedValue != "-1")
                {
                    conditionStr += @" and departmentid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                where b.DepartmentID=@GroupID)";
                    paramlist.Add(new SqlParameter("@GroupId", ddlDepartment2.SelectedValue));
                }
                else
                {
                    if (ddlDepartment1.SelectedValue != "-1")
                    {
                        conditionStr += @" and departmentid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                inner join sep_Departments as a on b.ParentID=a.DepartmentID
                                where a.DepartmentID=@GroupID)";
                        paramlist.Add(new SqlParameter("@GroupId", ddlDepartment1.SelectedValue));
                    }
                }
            }
            if (ddlStatus.SelectedValue.Equals("0"))
            {
                conditionStr += " and returnStatus not in(0,-1,1)";
            }
            else
            {
                ischoose = true;
                conditionStr += " and returnStatus in(" + ddlStatus.SelectedItem.Value + ")";
            }
            
            if (ischoose == false)
            {
                conditionStr += " and RequestDate>'2024-1-1'";
            }

            int total;
            int offset = this.GridProject.CurrentPageIndex * this.GridProject.PageSize;
            


            IList<ESP.Finance.Entity.ReturnInfo> list = ESP.Finance.BusinessLogic.ReturnManager.GetList(conditionStr, paramlist, offset, 60, out total).OrderByDescending(x => x.ReturnCode).ToList();

            this.GridProject.CallbackCachingEnabled = true;
            this.GridProject.CallbackCacheLookAhead = 2;

            this.GridProject.DataSource = list;
            this.GridProject.DataBind();
            this.GridProject.RecordCount = total;
            labTotal.Text = list.Sum(x => x.PreFee) == null ? "0.00" : list.Sum(x => x.PreFee).Value.ToString("#,##0.00");
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
                //e.Item["Auditor"] = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeNameByWorkItemID(returnModel.ReturnID, (int)ESP.Workflow.WorkItemStatus.Open, dataContext);
                e.Item["Auditor"] = "<a target='_blank' href='/project/ProjectWorkFlow.aspx?Type=oop&FlowID=" + returnModel.ReturnID.ToString() + "'><img src='/images/AuditStatus.gif' border='0' title='审批状态'></img></a>";

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

                e.Item["TypeName"] = ESP.Finance.Utility.Common.ReturnType_Names[returnModel.ReturnType.Value];

            }
        }

        protected void BindInfo()
        {
            BindDepartment(ddlDepartment1, 0);
            ddlDepartment2.Items.Insert(0, new ListItem("请选择", "-1"));
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        private void BindDepartment(DropDownList ddl, int parentId)
        {
            ddl.DataSource = ESP.Compatible.DepartmentManager.GetByParent(parentId);
            ddl.DataTextField = "NodeName";
            ddl.DataValueField = "UniqID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        protected void ddlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartment(ddlDepartment2, int.Parse(ddlDepartment1.SelectedValue));
            ddlDepartment3.Items.Clear();
            ddlDepartment3.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        protected void ddlDepartment2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment3.Items.Clear();
            BindDepartment(ddlDepartment3, int.Parse(ddlDepartment2.SelectedValue));
        }

    }
}
