using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_FinancialAuditList : ESP.Web.UI.PageBase
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


    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        Search();
    }


    private void Search()
    {
        term = string.Empty;
        paramlist.Clear();
        string BranchCodes = string.Empty;
        string customerBackup = string.Empty;
        string customerwhere = string.Empty;
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + CurrentUser.SysID);
        IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
        IList<ESP.Finance.Entity.BranchInfo> DelegateList = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in Delegates)
        {
            if (branchList == null)
                branchList = new List<ESP.Finance.Entity.BranchInfo>();
            DelegateList = ESP.Finance.BusinessLogic.BranchManager.GetList(" ProjectAccounter=" + model.UserID.ToString());
            customerBackup += model.UserID.ToString() + ",";
            foreach (ESP.Finance.Entity.BranchInfo bmodel in DelegateList)
            {
                branchList.Add(bmodel);
            }
        }
        foreach (ESP.Finance.Entity.BranchInfo model in branchList)
        {
            BranchCodes += "'"+model.BranchCode + "',";
        }
        BranchCodes = BranchCodes.TrimEnd(',');

        string finalCounters = ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters();
        string[] finals = finalCounters.Split(',');
        string FinalCounterDelegate = "," + finalCounters;
        for (int i = 0; i < finals.Length; i++)
        {
            if (!string.IsNullOrEmpty(finals[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(finals[i]));
                if (model != null)
                    FinalCounterDelegate += "," + model.BackupUserID.ToString();
            }
        }
        FinalCounterDelegate += ",";

        string ContractCounters = ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters();
        string[] Contracts = ContractCounters.Split(',');
        string ContractCounterDelegate = "," + ContractCounters;
        for (int i = 0; i < Contracts.Length; i++)
        {
            if (!string.IsNullOrEmpty(Contracts[i]))
            {
                ESP.Framework.Entity.AuditBackUpInfo model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelByUserID(Convert.ToInt32(Contracts[i]));
                if (model != null)
                    ContractCounterDelegate += "," + model.BackupUserID.ToString();
            }
        }
        ContractCounterDelegate += ",";

        if (string.IsNullOrEmpty(customerBackup))
        {
            customerBackup = CurrentUser.SysID;
        }
        else
        {
            customerBackup += CurrentUser.SysID;
        }
        IList<ESP.Finance.Entity.CustomerAuditorInfo> cuslist = ESP.Finance.BusinessLogic.CustomerAudtiorManager.GetList(" projectAuditor in(" + customerBackup+")");
        if (cuslist != null && cuslist.Count > 0)
        {
            foreach (ESP.Finance.Entity.CustomerAuditorInfo cus in cuslist)
            {
                customerwhere += "(BranchCode='" + cus.BranchCode + "' and customerCode='" + cus.CustomerCode + "') or ";
            }
            customerwhere = customerwhere.Substring(0, customerwhere.Length - 3);
        }
        string contractIds = "(" + ESP.Finance.Configuration.ConfigurationManager.CAStatus + "," + ESP.Finance.Configuration.ConfigurationManager.FCAStatus + ")";
        if (!string.IsNullOrEmpty(BranchCodes))//PR公关北京上海广州--任媛
        {
            term = " and ((Status=@Status and totalamount<=@totalamount) or Status=@Status2 or ((ContractStatusID not in "+contractIds+" || (ContractStatusID in "+contractIds+" and projectid in(select projectid from f_contractauditlog))) and Status=@Status3 and totalamount<=@totalamount)) and branchcode in(" + BranchCodes + ")";
            if (!string.IsNullOrEmpty(customerwhere))
            {
                term = " and ((Status=@Status and totalamount<=@totalamount) or Status=@Status2 or ((ContractStatusID not in " + contractIds + " || (ContractStatusID in " + contractIds + " and projectid in(select projectid from f_contractauditlog))) and Status=@Status3 and totalamount<=@totalamount)) and (branchcode in(" + BranchCodes + ") or (" + customerwhere + "))";
            }
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.ContractAudit;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p0 = new System.Data.SqlClient.SqlParameter("@totalamount", System.Data.SqlDbType.Decimal, 18);
            p0.SqlValue = ESP.Finance.Configuration.ConfigurationManager.FinancialAmount;
            paramlist.Add(p0);
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@Status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditing;
            paramlist.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@ContractStatusID", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = ESP.Finance.Configuration.ConfigurationManager.CAStatus;
            paramlist.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new System.Data.SqlClient.SqlParameter("@Status3", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p4);
        }
       
        else if (FinalCounterDelegate.IndexOf(CurrentUserID.ToString()) >= 0)//eddy
        {
            term = " and ((Status=@Status and totalamount>@totalamount) or ((ContractStatusID not in "+contractIds+"  || (ContractStatusID in "+contractIds+" and projectid in(select projectid from f_contractauditlog))) and Status=@Status3 and totalamount<=@totalamount))";
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.ContractAudit;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p0 = new System.Data.SqlClient.SqlParameter("@totalamount", System.Data.SqlDbType.Decimal, 18);
            p0.SqlValue = ESP.Finance.Configuration.ConfigurationManager.FinancialAmount;
            paramlist.Add(p0);
            System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@ContractStatusID", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = ESP.Finance.Configuration.ConfigurationManager.CAStatus;
            paramlist.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new System.Data.SqlClient.SqlParameter("@Status3", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p4);
        }
        else if (ContractCounterDelegate.IndexOf(CurrentUserID.ToString()) >= 0)//合同审批
        {
            term = " and (Status=@Status and projectid not in(select projectid from f_contractauditlog) and ContractStatusID in "+contractIds+" ) or CheckContract=@CheckContract";
            System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)ESP.Finance.Utility.Status.BizAuditComplete;
            paramlist.Add(p1);
            System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@ContractStatusID", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = ESP.Finance.Configuration.ConfigurationManager.CAStatus;
            paramlist.Add(p3);
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@CheckContract", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ProjectCheckContract.ContractUpdate;
            paramlist.Add(p2);
        }
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
                term += " and (serialcode like '%'+@serialcode+'%' or projectcode like '%'+@projectcode+'%' or GroupName like '%'+@GroupName+'%' or BranchName like '%'+@BranchName+'%' or BusinessTypeName like '%'+@BusinessTypeName+'%' ";
                if (!string.IsNullOrEmpty(customerIDs))
                    term += " or CustomerID in(" + customerIDs + "))";
                else
                    term += ")";
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@serialcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectcode", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@GroupName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BranchName", this.txtKey.Text.Trim()));
                paramlist.Add(new System.Data.SqlClient.SqlParameter("@BusinessTypeName", this.txtKey.Text.Trim()));

            }
            IList<ESP.Finance.Entity.ProjectInfo> projectList= ESP.Finance.BusinessLogic.ProjectManager.GetList(term, paramlist);

           var tmplist = projectList.OrderBy(N=>N.SubmitDate);
           this.gvG.DataSource = tmplist.ToList();
            this.gvG.DataBind();
        }

        if (this.gvG.DataSource != null && this.gvG.Rows.Count > 0)
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

     protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ESP.Finance.Entity.ProjectInfo projectmodel = (ESP.Finance.Entity.ProjectInfo)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblName");
            lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(projectmodel.ApplicantUserID) + "');");

            HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
            hylAudit.NavigateUrl = "FinancialAuditOperation.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + projectmodel.ProjectId;
            Label lblGroupName = (Label)e.Row.FindControl("lblGroupName");
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>() ;
            depList=ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(projectmodel.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            lblGroupName.Text = groupname.Substring(0,groupname.Length-1);

            if (projectmodel.InUse != (int)ESP.Finance.Utility.ProjectInUse.Use)
            {
                hylAudit.Visible = false;
            }
            Literal litUse = (Literal)e.Row.FindControl("litUse");
            litUse.Text = ESP.Finance.Utility.Common.ProjectInUse_Names[projectmodel.InUse];
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
