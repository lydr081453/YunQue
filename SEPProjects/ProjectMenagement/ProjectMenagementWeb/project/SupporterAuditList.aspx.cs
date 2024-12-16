using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowModel;
using WorkFlowImpl;
using ESP.Finance.Entity;
using System.Linq;

public partial class project_SupporterAuditList : ESP.Web.UI.PageBase
{
    WorkFlowImpl.WorkItemData workitemdata = new WorkFlowImpl.WorkItemData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetWorkFlowList();
        }
    }

    private void GetWorkFlowList()
    {
        List<WorkFlowModel.WorkItemData> list1 = workitemdata.getProcessDataList(CurrentUser.SysID, "支持方申请");
        List<SupporterInfo> list = new List<SupporterInfo>();
        IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
         List<WorkFlowModel.WorkItemData> list2 = new List<WorkFlowModel.WorkItemData>();
        List<WorkFlowModel.WorkItemData> listDelegate = null;
        foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
        {
            listDelegate = workitemdata.getProcessDataList(model.UserID.ToString(), "支持方申请");
            foreach (WorkFlowModel.WorkItemData o in listDelegate)
            {
                list2.Add(o);
            }
        }
        foreach (WorkFlowModel.WorkItemData o in list1)
        {
            SupporterInfo model = (SupporterInfo)o.ItemData;
            model.WorkItemID = o.WorkItemID;
            model.WorkItemName = o.WorkItemName;
            model.ProcessID = o.ProcessID;
            model.InstanceID = o.InstanceID;
            list.Add(model);
        }
        if (list2 != null && list2.Count > 0)
        {
            foreach (WorkFlowModel.WorkItemData o in list2)
            {
                SupporterInfo model = (SupporterInfo)o.ItemData;
                model.WorkItemID = o.WorkItemID;
                model.WorkItemName = o.WorkItemName;
                model.ProcessID = o.ProcessID;
                model.InstanceID = o.InstanceID;
                list.Add(model);
            }
        }

 //       var tmplist = list.OrderBy(N=>N.co);

        gridList.DataSource = list;
        gridList.DataBind();
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
            hylEdit.NavigateUrl = "SupporterAuditOperation.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID;
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

    protected void gridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridList.PageIndex = e.NewPageIndex;
        GetWorkFlowList();
    }
}