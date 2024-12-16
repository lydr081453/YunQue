using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_CompleteList : ESP.Web.UI.PageBase
{
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(project_CompleteList));
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

    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters()+ ",";
        return user;
    }
    private string GetManagerDept()
    {
        string deptids = string.Empty;
        DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(" directorid=" + CurrentUser.SysID + " or managerid=" + CurrentUser.SysID + " or ceoid=" + CurrentUser.SysID);
        if (ds != null && ds.Tables[0] != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                deptids += ds.Tables[0].Rows[i]["DepId"].ToString()+",";
            }
            deptids = deptids.TrimEnd(',');
        }
        return deptids;
    }
    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        string users = GetUser();// ConfigurationManager.ContractAdmin + "," + ConfigurationManager.FinancialAdmin1 + "," + ConfigurationManager.FinancialAdmin2 + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        string Branchs=string.Empty;
        IList<ESP.Finance.Entity.BranchInfo> branchList =ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%,"+CurrentUser.SysID+",%'");
        if(branchList!=null && branchList.Count>0)
        {
           foreach(ESP.Finance.Entity.BranchInfo b in branchList)
           {
                 Branchs+=b.BranchID.ToString()+",";
           }
        }
        Branchs=Branchs.TrimEnd(',');
        string DeptIDs = this.GetManagerDept();
        if (users.IndexOf(","+CurrentUserID.ToString()+",") >= 0)
        {
            term = " and (Status=@Status or Status=@StatusClosed or Status=@StatusPreClosed)";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@Status", ESP.Finance.Utility.Status.FinanceAuditComplete));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusClosed", ESP.Finance.Utility.Status.ProjectClosed));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusPreClosed", ESP.Finance.Utility.Status.ProjectPreClose));
        }
        else if (!string.IsNullOrEmpty(Branchs))
        {
            term = "  and (Status=@Status or Status=@StatusClosed or Status=@StatusPreClosed) and branchid in(" + Branchs + ")";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@Status", ESP.Finance.Utility.Status.FinanceAuditComplete));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusClosed", ESP.Finance.Utility.Status.ProjectClosed));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusPreClosed", ESP.Finance.Utility.Status.ProjectPreClose));
        }
        else
        {
            term = "  and (Status=@Status or Status=@StatusClosed or Status=@StatusPreClosed) and (projectid in(select projectid from f_projectmember where memberuserid=@memberuserid) or projectid in(select projectid from F_AuditHistory where AuditorUserID=@memberuserid) ";
            string strdept = ESP.Finance.BusinessLogic.PrjUserRelationManager.GetUsableBranchID(CurrentUserID);
            if (!string.IsNullOrEmpty(strdept))
            {
                term +=  " or branchid in(" + strdept + ")";
            }
            if (!string.IsNullOrEmpty(DeptIDs))
            {
                term += " or groupid in(" + DeptIDs + ")";
            }
            term += ")";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@Status", ESP.Finance.Utility.Status.FinanceAuditComplete));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusClosed", ESP.Finance.Utility.Status.ProjectClosed));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@StatusPreClosed", ESP.Finance.Utility.Status.ProjectPreClose));
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@memberuserid", CurrentUserID));
        }
      
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
            if (!string.IsNullOrEmpty(this.txtBeginDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                term += " and SubmitDate between @BeginDate and @EndDate";
                SqlParameter p2 = new SqlParameter("@BeginDate", SqlDbType.DateTime, 8);
                p2.Value = this.txtBeginDate.Text.Trim();
                paramlist.Add(p2);

                SqlParameter p3 = new SqlParameter("@EndDate", SqlDbType.DateTime, 8);
                p3.Value = Convert.ToDateTime(this.txtEndDate.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59";
                paramlist.Add(p3);
            }
            if (!string.IsNullOrEmpty(this.hidBranchID.Value) && this.hidBranchID.Value != "-1")
            {
                term += " and BranchID =@BranchID";
                SqlParameter p4 = new SqlParameter("@BranchID", SqlDbType.Int, 4);
                p4.Value = this.hidBranchID.Value;
                paramlist.Add(p4);
            }
         
            IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);
            //var tmplist = projectList.OrderBy(N=>N.SubmitDate);
            this.gvG.DataSource = projectList;
            this.gvG.DataBind();
    }

    protected void lbNewProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep1.aspx");
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string users = GetUser();// ConfigurationManager.ContractAdmin + "," + ConfigurationManager.FinancialAdmin1 + "," + ConfigurationManager.FinancialAdmin2 + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.HyperLink hylEdit = (System.Web.UI.WebControls.HyperLink)e.Row.FindControl("hylEdit");
            if (hylEdit != null)
            {
                if (users.IndexOf("," + CurrentUserID.ToString() + ",") < 0)
                {
                    hylEdit.Visible = false;
                }
                hylEdit.NavigateUrl = string.Format(State.addstatus_8+"&"+RequestName.BackUrl+"=CompleteList.aspx", e.Row.Cells[0].Text.ToString());
            }
            Label labState = ((Label)e.Row.FindControl("labState"));
            labState.Text = State.SetState(int.Parse(labState.Text));
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");
            if (projectmodel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
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
}
