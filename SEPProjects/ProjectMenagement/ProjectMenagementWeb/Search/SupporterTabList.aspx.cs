using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class project_SupporterTabList : ESP.Web.UI.PageBase
{
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

    private string GetDelegateUser()
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

    void GridProject_NeedRebind(object sender, EventArgs e)
    {
        Search();
    }

    void GridProject_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
    {
        GridProject.CurrentPageIndex = e.NewIndex;
    }

    private string GetUser()
    {
        string user = "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
        user += ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + ",";
        return user;
    }
    private string GetManagerDept()
    {
        string deptids = string.Empty;
        DataSet ds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetList(" directorid=" + CurrentUser.SysID + " or managerid=" + CurrentUser.SysID + " or ceoid=" + CurrentUser.SysID);
        var projectAuditList = ESP.Finance.BusinessLogic.BranchProjectManager.GetList(CurrentUserID);
        
        if (ds != null && ds.Tables[0] != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                deptids += ds.Tables[0].Rows[i]["DepId"].ToString() + ",";
            }
            deptids = deptids.TrimEnd(',');
        }

        if (projectAuditList != null && projectAuditList.Count > 0)
        {
            foreach (var p in projectAuditList)
            {
                deptids += p.DeptId + ",";
            }
        }

        return deptids;
    }

    private void Search()
    {
        string term = string.Empty;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        string users = GetUser();
        string BranchCodes = string.Empty;
        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" otherfinancialusers like '%," + CurrentUserID + ",%'");
        foreach (ESP.Finance.Entity.BranchInfo model in branchList)
        {
            BranchCodes += "'" + model.BranchCode + "',";
        }
        BranchCodes = BranchCodes.TrimEnd(',');
        string delegateusers = GetDelegateUser();
        if (!string.IsNullOrEmpty(delegateusers))
        {
            term = " (SupportID in(select SupportID from F_SupportMember where MemberUserID=@memberuserid) or " +
                   "applicantUserID=" + CurrentUser.SysID + " or leaderuserID=" + CurrentUser.SysID + " or " +
                   "supportid in(select supporterid from F_SupporterAuditHist where auditoruserid = " + CurrentUser.SysID + " or auditoruserid in(" + delegateusers + "))";
        }
        else
            term = " (SupportID in(select SupportID from F_SupportMember where MemberUserID=@memberuserid) or " +
                    "applicantUserID=" + CurrentUser.SysID + " or leaderuserID=" + CurrentUser.SysID + " or " +
                    "supportid in(select supporterid from F_SupporterAuditHist where auditoruserid = " + CurrentUser.SysID + ")";
        SqlParameter p1 = new SqlParameter("@memberuserid", System.Data.SqlDbType.Int, 4);
        p1.Value = CurrentUserID;
        paramlist.Add(p1);
        if (!string.IsNullOrEmpty(BranchCodes))
        {
            term += " or projectid in(select projectid from f_project where branchcode in(" + BranchCodes + ")))";
        }
        else
            term += ")";

        if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
        {
            term += " and (supportercode like '%'+@key+'%' or ApplicantEmployeeName like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+@key+'%' or ServiceDescription like '%'+@key+'%')";
            SqlParameter p3 = new SqlParameter("@key", System.Data.SqlDbType.NVarChar, 50);
            p3.Value = this.txtKey.Text.Trim();
            paramlist.Add(p3);
        }
        if (this.ddlStatus.SelectedItem.Value != "-1")
        {
            switch (this.ddlStatus.SelectedItem.Value)
            {
                case "1":
                    term += " and status not in(0,32,33,34)";
                    break;
                case "32":
                    term += " and status in (32)";
                    break;
            }
        }
        this.GridProject.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term, paramlist);
        this.GridProject.DataBind();

    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }

    protected void GridProject_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
    {
        //所有部门级联字符串拼接
        ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.Item.DataItem;
        ESP.Finance.Entity.ProjectInfo projectModel =ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(supporterModel.ProjectID);

        List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
        depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(int.Parse(e.Item["GroupID"].ToString()), depList);
        string groupname = string.Empty;
        foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
        {
            groupname += dept.DepartmentName + "-";
        }
        ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(projectModel.BranchCode);
        ESP.Finance.Entity.BranchProjectInfo branchProject = null;
        if (branchModel != null)
            branchProject = ESP.Finance.BusinessLogic.BranchProjectManager.GetModel(branchModel.BranchID, projectModel.GroupID.Value);

        ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(supporterModel.GroupID.Value);
        string users = string.Empty;
        if (branchModel != null && branchModel.BranchCode != null)
        {
            users = "," + branchModel.FirstFinanceID.ToString() + "," + branchModel.FinalAccounter.ToString() + "," + branchModel.PaymentAccounter.ToString() + "," + branchModel.OtherFinancialUsers + "," + branchModel.ContractAccounter.ToString() + "," + branchModel.ProjectAccounter.ToString() + ",";
        }
        else
            users = ",";
        if (branchProject != null)
        {
            users += branchProject.AuditorID + ",";
        }

        users += supporterModel.LeaderUserID + ",";

        if (operationModel != null)
            users += operationModel.DirectorId.ToString() + "," + operationModel.ManagerId.ToString() + "," + operationModel.FAId.ToString() + "," + operationModel.CEOId.ToString() + "," + operationModel.RiskControlAccounter.ToString() + operationModel.CostView;

        if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0)
        {
            e.Item["CBX"] = " <a href=\"/CostView/SupSinglePrjView.aspx?" + ESP.Finance.Utility.RequestName.SupportID + "=" + e.Item["SupportID"] + "\"  target=\"_blank\">" +
                                                            "<img src=\"/images/dc.gif\" border=\"0px;\" title=\"查看成本\" /></a>";
        }
        else
        {
            e.Item["CBX"] = "";
        }

        if (!string.IsNullOrEmpty(groupname))
            e.Item["GroupName"] = groupname.Substring(0, groupname.Length - 1);
        e.Item["StatusText"] = State.SetState(int.Parse(e.Item["Status"].ToString()));
               e.Item["ViewAudit"] = "<a href=\"/project/ProjectWorkFlow.aspx?Type=supporter&FlowID=" + e.Item["SupportID"].ToString() + "\" target=\"_blank\">" +
                                                       "<img src=\"/images/AuditStatus.gif\" border=\"0px;\" title=\"审批状态\" /></a>";
       
     
        if (supporterModel.Status == (int)ESP.Finance.Utility.Status.FinanceAuditComplete)
        {
            string changeurl = string.Format("/project/SupporterModify.aspx?" + RequestName.SupportID + "={0}" + "&" + RequestName.ProjectID + "={1}" + "&" + RequestName.BackUrl + "=/Search/SupporterTabList.aspx", supporterModel.SupportID.ToString(), supporterModel.ProjectID.ToString());
            e.Item["Change"] = "<a href=\"" + changeurl + "\">" +
                                                            "<img src=\"/images/edit.gif\" border=\"0px;\" title=\"变更\" /></a>";
        }

        e.Item["View"] = "<a href=\"/project/SupporterDisplay.aspx?" + ESP.Finance.Utility.RequestName.SupportID + "=" + e.Item["SupportID"] + "&" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] + "\"  target=\"_blank\">" +
                                                           "<img src=\"../images/dc.gif\" border=\"0px;\" title=\"查看\" /></a>";

        if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0 || Convert.ToInt32(CurrentUser.SysID) == supporterModel.LeaderUserID.Value)
        {
            
            e.Item["Print"] = "<a href='/project/SupporterPrint.aspx?" + ESP.Finance.Utility.RequestName.SupportID + "=" + e.Item["SupportID"] + "&" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] +
                                                            "' target=\"_blank\"><img title=\"支持方打印预览\" src=\"/images/ProjectPrint.gif\" border=\"0px;\" /></a>";

            string historyurl = string.Format("/project/SupportHist.aspx?{0}={1}", ESP.Finance.Utility.RequestName.SupportID, supporterModel.SupportID.ToString());
            e.Item["History"] = "<a href=\"" + historyurl + "\"  target=\"_blank\">" +
                                                            "<img src=\"/images/history.gif\" border=\"0px;\" title=\"历史\" /></a>";
        }
        else
        {
            e.Item["View"] = "";
            e.Item["Print"] = "";
            e.Item["History"] = "";
        }
    }

}


