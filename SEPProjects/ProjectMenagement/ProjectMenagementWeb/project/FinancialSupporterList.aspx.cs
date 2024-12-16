using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class project_FinancialSupporterList : ESP.Web.UI.PageBase
{

    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Search();
        }
    }


    protected void gridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridList.PageIndex = e.NewPageIndex;
        Search();
    }


    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + CurrentUser.SysID);
        string BranchCodes = string.Empty;
        string finalCounters=ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        string[] finals=finalCounters.Split(',');
        string FinalCounterDelegate=","+finalCounters;
        for(int i=0;i<finals.Length;i++)
        {
            if(!string.IsNullOrEmpty(finals[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model= ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(finals[i]));
                if (model != null)
                    FinalCounterDelegate += "," + model.BackupUserID.ToString();
            }
        }
        FinalCounterDelegate+=",";
        IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        IList<ESP.Finance.Entity.BranchInfo> DelegateList = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in Delegates)
        {
            if (branchList == null)
                branchList = new List<ESP.Finance.Entity.BranchInfo>();
            DelegateList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + model.UserID.ToString());
            foreach (ESP.Finance.Entity.BranchInfo bmodel in DelegateList)
            {
                branchList.Add(bmodel);
            }
        }
        foreach (ESP.Finance.Entity.BranchInfo model in branchList)
        {
            BranchCodes += "'" + model.BranchCode + "',";
        }
        BranchCodes = BranchCodes.TrimEnd(',');

        if (branchList != null && branchList.Count>0)//PR公关北京上海广州--任媛
        {
//            term = "  ((Status=@Status and BudgetAllocation<=@BudgetAllocation) or Status=@Status2) and (";
            term = "  (Status=@Status or Status=@Status2) and (";
            foreach (ESP.Finance.Entity.BranchInfo model in branchList)
            {
                term += "ProjectCode like '"+model.BranchCode+"%' or ";   
            }
            term = term.Substring(0,term.Length-3)+")";

            SqlParameter p1 = new SqlParameter("@Status",SqlDbType.Int,4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p1);
            SqlParameter p3 = new SqlParameter("@Status2", SqlDbType.Int, 4);
            p3.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditing;
            paramlist.Add(p3);
            //SqlParameter p2 = new SqlParameter("@BudgetAllocation", SqlDbType.Decimal, 18);
            //p2.SqlValue = ESP.Finance.Configuration.ConfigurationManager.FinancialAmount;
            //paramlist.Add(p2);
        }
        //else if (FinalCounterDelegate.IndexOf(CurrentUserID.ToString()) >= 0)//eddy
        //{
        //    term = "  Status=@Status and BudgetAllocation>@BudgetAllocation";
        //    SqlParameter p1 = new SqlParameter("@Status", SqlDbType.Int, 4);
        //    p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
        //    paramlist.Add(p1);
        //    SqlParameter p2 = new SqlParameter("@BudgetAllocation", SqlDbType.Decimal, 18);
        //    p2.SqlValue = ESP.Finance.Configuration.ConfigurationManager.FinancialAmount;
        //    paramlist.Add(p2);
        //}
       
        if (term != string.Empty)
        {
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (SupporterCode like '%'+@txtKey+'%' or ProjectCode like '%'+@txtKey+'%' or GroupName like '%'+@txtKey+'%' or ServiceType like '%'+@txtKey+'%' or ApplicantUserName like '%'+@txtKey+'%' )";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@txtKey", this.txtKey.Text.Trim()));
            }
            IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
           // var tmplist = supporterList.OrderBy(N=>N.Lastupdatetime);
            this.gridList.DataSource = supporterList;
            this.gridList.DataBind();
        }

        if (this.gridList.DataSource != null && this.gridList.Rows.Count > 0)
        {
            trNoRecord.Visible = false;
            trSource.Visible = true;
        }
        else
        {
            trNoRecord.Visible = true;
            trSource.Visible = false;
        }
    }

    protected void gridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
         
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Row.DataItem;
            ESP.Finance.Entity.ProjectInfo projectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);

            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporterModel.LeaderUserID.Value) + "');");

            HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
            hylEdit.NavigateUrl = "FinancialSupporter.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID;
            if (projectModel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
                hylEdit.Visible = false;
            //所有部门级联字符串拼接
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporterModel.GroupID.Value, depList);
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
}