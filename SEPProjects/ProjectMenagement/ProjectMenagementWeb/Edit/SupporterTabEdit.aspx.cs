using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace FinanceWeb.Edit
{
    public partial class SupporterTabEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            string term = string.Empty;
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

            string users = "," + ESP.Finance.BusinessLogic.BranchManager.GetContractAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetFinalAccounters() + "," + ESP.Finance.BusinessLogic.BranchManager.GetProjectAccounters() + ",";
            //string Branchs = string.Empty;
            //IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" OtherFinancialUsers like '%," + CurrentUser.SysID + ",%'");
            //if (branchList != null && branchList.Count > 0)
            //{
            //    foreach (ESP.Finance.Entity.BranchInfo b in branchList)
            //    {
            //        Branchs += b.BranchID.ToString() + ",";
            //    }
            //}
            //Branchs = Branchs.TrimEnd(',');

            term = " status in('" + ((int)Status.Saved).ToString() + "','" + ((int)Status.BizReject).ToString() + "','" + ((int)Status.FinanceReject) + "')";
            if (users.IndexOf("," + CurrentUser.SysID + ",") >= 0)
            {
                term += "  and projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + " or status=" + (int)ESP.Finance.Utility.Status.ProjectPreClose + ")";
            }
            //else if (!string.IsNullOrEmpty(Branchs))
            //{
            //    term += " and projectid in(select projectid from f_project where branchid in(" + Branchs + "))";
            //}
            else
            {
                term += " and (leaderuserid=" + CurrentUser.SysID + " or applicantuserid=" + CurrentUser.SysID + " or supportid in(select supportid from F_Supportmember where MemberUserID=" + CurrentUser.SysID + ")) and  projectid in (select projectid from f_project where status=" + (int)ESP.Finance.Utility.Status.FinanceAuditComplete + " or status=" + (int)ESP.Finance.Utility.Status.ProjectPreClose + ")";

            }
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                term += " and (supportercode like '%'+@key+'%' or ApplicantEmployeeName like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+@key+'%' or ServiceDescription like '%'+@key+'%')";
                SqlParameter p3 = new SqlParameter("@key", System.Data.SqlDbType.NVarChar, 50);
                p3.Value = this.txtKey.Text.Trim();
                paramlist.Add(p3);
            }
            this.GridSupporter.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetList(term,paramlist);
            this.GridSupporter.DataBind();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            ListBind();
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            GridSupporter.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(GridSupporter_ItemDataBound);
        }

        protected void GridSupporter_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.SupporterInfo supporterModel = (ESP.Finance.Entity.SupporterInfo)e.DataItem;

            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(supporterModel.GroupID.Value, depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            if (!string.IsNullOrEmpty(groupname))
                e.Item["GroupName"] = groupname.Substring(0, groupname.Length - 1);
            e.Item["LeaderEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(supporterModel.LeaderUserID.Value) + "');\">" + supporterModel.LeaderEmployeeName + "</a>";
            e.Item["StatusName"] = State.SetState(supporterModel.Status);
            e.Item["View"] = "<a target='_blank' href='/project/SupporterDisplay.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID + "&BackUrl=/Edit/SupporterTabEdit.aspx'><img src='../images/dc.gif' border='0px;' title='查看'/></a>";
            if (int.Parse(CurrentUser.SysID) == supporterModel.LeaderUserID.Value)
            {
                e.Item["Edit"] = "<a href='/project/SupporterEdit.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID + "&BackUrl=/Edit/SupporterTabEdit.aspx'><img src='../images/edit.gif' border='0px;' title='编辑'/></a>";
            }
            else
            {
                e.Item["Edit"] = "";
            }
            e.Item["Print"] = "<a target='_blank' href='/project/SupporterPrint.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + supporterModel.ProjectID + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + supporterModel.SupportID + "'><img src='/images/SupporterPrint.gif' border='0px;' title='打印预览' /></a>";
        }
    }
}
