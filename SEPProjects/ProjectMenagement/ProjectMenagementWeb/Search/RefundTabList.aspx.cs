using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Linq;

namespace FinanceWeb.Search
{
    public partial class RefundTabList : ESP.Web.UI.PageBase
    {
        IList<ESP.Finance.Entity.BranchInfo> branches;
        int userid;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 600;

            userid = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
            if (!IsPostBack && !this.GridRefund.CausedCallback)
            {
                BindInfo();
                Search();
            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GridRefund.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(GridRefund_NeedRebind);
            GridRefund.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(GridRefund_PageIndexChanged);

        }
        void GridRefund_NeedRebind(object sender, EventArgs e)
        {
            Search();
        }

        void GridRefund_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            GridRefund.CurrentPageIndex = e.NewIndex;
        }

        private string GetDelegateUser()
        {
            string users = string.Empty;
            DataTable dt = ESP.Framework.BusinessLogic.AuditBackUpManager.GetList("backupuserid=" + userid + " and type=3").Tables[0];
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
            managerid = userid.ToString();
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
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(userid);
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers += GetDelegateUser();
            delegateusers = delegateusers.TrimEnd(',');
            string Branchs = string.Empty;

            branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var branchList = branches.Where(x => x.OtherFinancialUsers != null && x.OtherFinancialUsers.Contains("," + userid + ","));
            if (branchList != null)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += "'" + b.BranchCode + "',";
                }
            }
            Branchs = Branchs.TrimEnd(',');
            
            string groupids = getGroupIds();
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            if (!string.IsNullOrEmpty(delegateusers))
            {
                term = " ((RequestorID=@currentUserId or RequestorID in(" + delegateusers + ")) or id in(select modelid from F_workflow where auditoruserid =@currentUserId and modelType=13)";
            }
            else
            {
                term = " (RequestorID=@currentUserId or id in(select modelid from F_workflow where auditoruserid =@currentUserId and modelType=13)";
            }



            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = userid;
            paramlist.Add(puserid);

            if (!string.IsNullOrEmpty(Branchs))
            {
                term += " or SUBSTRING(ProjectCode,1,1) in(" + Branchs + ")";
            }
            if (!string.IsNullOrEmpty(groupids))
            {
                term += " or deptid in(" + groupids + ")";
            }
            term += ")";

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or refundCode like '%'+@prno+'%' or prid like '%'+@prno+'%' or Amounts  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }
            #region 新增查询条件
            if (!string.IsNullOrEmpty(txtProjectCode.Text.Trim()))
            {
                term += string.Format(" and ProjectCode like '%{0}%'  ", txtProjectCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtRequestEmployeeName.Text.Trim()))
            {
                term += string.Format(" and RequestEmployeeName like '%{0}%'  ", txtRequestEmployeeName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBegin.Text.Trim()))
            {
                term += string.Format(" and '{0}' <= Convert(Char(10),RequestDate,120) ", txtBegin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEnd.Text.Trim()))
            {
                term += string.Format(" and '{0}' >= Convert(Char(10),RequestDate,120) ", txtEnd.Text.Trim());
            }
            if (ddlDepartment3.SelectedValue != "-1")
            {
                term += " and deptid=@GroupId";
                paramlist.Add(new SqlParameter("@GroupId", ddlDepartment3.SelectedValue));
            }
            else
            {
                if (ddlDepartment2.SelectedValue != "-1")
                {
                    term += @" and deptid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                where b.DepartmentID=@GroupID)";
                    paramlist.Add(new SqlParameter("@GroupId", ddlDepartment2.SelectedValue));
                }
                else
                {
                    if (ddlDepartment1.SelectedValue != "-1")
                    {
                        term += @" and deptid in (select c.DepartmentID from sep_Departments as c
                                inner join sep_Departments as b on c.ParentID=b.DepartmentID
                                inner join sep_Departments as a on b.ParentID=a.DepartmentID
                                where a.DepartmentID=@GroupID)";
                        paramlist.Add(new SqlParameter("@GroupId", ddlDepartment1.SelectedValue));
                    }
                }
            }
            #endregion

            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and Status=@Status";
                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
                p2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(p2);
            }

            IList<ESP.Finance.Entity.RefundInfo> refundList = ESP.Finance.BusinessLogic.RefundManager.GetList(term, paramlist);

            this.GridRefund.CallbackCachingEnabled = true;
            this.GridRefund.CallbackCacheLookAhead = 2;

            this.GridRefund.DataSource = refundList;
            this.GridRefund.DataBind();

     }

        protected void GridRefund_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.RefundInfo refundModel = (ESP.Finance.Entity.RefundInfo)e.DataItem;

            e.Item["StatusName"] = ReturnPaymentType.ReturnStatusString(refundModel.Status, 0, false);
            e.Item["ViewAudit"] = "<a href=\"/project/ProjectWorkFlow.aspx?Type=refund&FlowID=" + e.Item["Id"].ToString() + "\" target=\"_blank\">" +
                                                     "<img src=\"/images/AuditStatus.gif\" border=\"0px;\" title=\"审批状态\" /></a>";
            e.Item["Print"] = "<a target='_blank' href='/Refund/RefundPrint.aspx?" + RequestName.RefundID + "=" + refundModel.Id + "'><img src='/images/Icon_Output.gif' border='0' ></img></a>";
        }

        private int[] GetFinanceUser(ESP.Finance.Entity.RefundInfo returnModel)
        {
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditer = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" Id =" + returnModel.Id.ToString() + " and auditoruserid =" + CurrentUserID.ToString());
            if (branches == null)
                branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var branchCode = returnModel.ProjectCode.Substring(0, 1);
            var branch = branches.Where(x => x.BranchCode == branchCode).FirstOrDefault();
            if (branch != null)
            {
                if (auditer == null || auditer.Count == 0)
                    return new int[] { branch.FirstFinanceID, branch.FinalAccounter };
                else
                    return new int[] { branch.FirstFinanceID, branch.FinalAccounter, CurrentUserID };
            }
            return new int[0];
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
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