using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class CloseProjectList : ESP.Web.UI.PageBase
    {

        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(project_ProjectList));
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

            if (!IsPostBack)
            {
                Search();
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }

        private void Search()
        {
            term = string.Empty;
            paramlist.Clear();
            term = "  and (enddate='" + DateTime.Now.AddMonths(-2).AddDays(7).ToString("yyyy-MM-dd") + "' and projectcode <> '')";
            if (term != string.Empty)
            {
                if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
                {
                    string customerIDs = string.Empty;
                    IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(" NameCN1 like '%" + this.txtKey.Text.Trim() + "%' or  NameEN1 like '%" + this.txtKey.Text.Trim() + "%' ");
                    foreach (ESP.Finance.Entity.CustomerTmpInfo cusModel in customerList)
                    {
                        customerIDs += cusModel.CustomerTmpID.ToString() + ",";
                    }
                    customerIDs = customerIDs.TrimEnd(',');
                    term += " and (serialcode like '%'+@serialcode+'%' or BusinessDescription like '%'+@serialcode+'%' or projectcode like '%'+@serialcode+'%' or GroupName like '%'+@serialcode+'%' or BranchName like '%'+@serialcode+'%' or BusinessTypeName like '%'+@serialcode+'%' ";
                    if (!string.IsNullOrEmpty(customerIDs))
                        term += " or CustomerID in(" + customerIDs + "))";
                    else
                        term += ")";
                    paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
                }
                if (!string.IsNullOrEmpty(this.hidBranchID.Value) && this.hidBranchID.Value != "-1")
                {
                    term += " and BranchID =@BranchID";
                    SqlParameter p4 = new SqlParameter("@BranchID", SqlDbType.Int, 4);
                    p4.Value = this.hidBranchID.Value;
                    paramlist.Add(p4);
                }
                this.gvG.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
                this.gvG.DataBind();
            }
        }
        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SendMail")
            {
                int projectid = int.Parse(e.CommandArgument.ToString());

            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.HyperLink hylClose = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylClose");
                hylClose.NavigateUrl = "CloseProject.aspx?" + RequestName.ProjectID + "=" + projectmodel.ProjectId.ToString();
                Label lblName = (Label)e.Row.FindControl("lblName");
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
                
                Label labState = ((Label)e.Row.FindControl("labState"));
                labState.Text = State.SetState(int.Parse(labState.Text));
                //所有部门级联字符串拼接
                Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectmodel.GroupID.Value, depList);
                string groupname = string.Empty;
                foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                {
                    groupname += dept.DepartmentName + "-";
                }
                if (!string.IsNullOrEmpty(groupname))
                    lblGroupName.Text = groupname.Substring(0, groupname.Length - 1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            Search();
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBranchList()
        {
            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
            List<List<string>> list = new List<List<string>>();
            List<string> item = null;
            foreach (ESP.Finance.Entity.BranchInfo branch in blist)
            {
                item = new List<string>();
                item.Add(branch.BranchID.ToString());
                item.Add(branch.BranchName);
                list.Add(item);
            }
            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string ProjectIDs = string.Empty;
            CollectSelected();
            if (SelectedItems != null && SelectedItems.Count > 0)
            {
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    ProjectIDs += SelectedItems[i].ToString() + ",";
                }
                ProjectIDs = ProjectIDs.TrimEnd(',');
            }
            if (!string.IsNullOrEmpty(ProjectIDs))
            {
                IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and projectID in(" + ProjectIDs + ")");

                try
                {
                ESP.Finance.Utility.SendMailHelper.SendRemindEmail(projectList);
                }
                catch
                {
                 
                }
            }
        }

        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new ArrayList();
            else
                SelectedItems.Clear();
            string MID = string.Empty;

            for (int i = 0; i < this.gvG.Rows.Count; i++)
            {
                MID = gvG.Rows[i].Cells[1].Text.Trim();
                CheckBox cb = this.gvG.Rows[i].FindControl("chkPrj") as CheckBox;
                if (SelectedItems.Contains(MID) && !cb.Checked)
                    SelectedItems.Remove(MID);
                if (!SelectedItems.Contains(MID) && cb.Checked)
                    SelectedItems.Add(MID);
            }
            this.SelectedItems = SelectedItems;
        }

        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
            }
            set
            {
                ViewState["mySelectedItems"] = value;
            }
        }

    }
}
