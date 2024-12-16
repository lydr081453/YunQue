using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class SupportAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !this.grComplete.CausedCallback)
            {
                ListBind();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void ListBind()
        {
            this.AuditTab.BindData();
            var list = this.AuditTab.Supporter;
            var keyword = txtKey.Text.Trim();

            //string terms = "";
            //List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            //if (txtKey.Text.Trim() != "")
            //{
            //    terms += " and (supportercode like '%'+@key+'%' or ApplicantEmployeeName like '%'+@key+'%' or projectcode like '%'+@key+'%' or GroupName like '%'+@key+'%' or ServiceType like '%'+@key+'%' or ServiceDescription like '%'+@key+'%')";
            //    parms.Add(new System.Data.SqlClient.SqlParameter("@key", this.txtKey.Text.Trim()));
            //}

            list = list.Where(x => (x.SupporterCode != null && x.SupporterCode.Contains(keyword))
                || (x.ApplicantEmployeeName != null && x.ApplicantEmployeeName.Contains(keyword))
                || (x.ProjectCode != null && x.ProjectCode.Contains(keyword))
                || (x.GroupName != null && x.GroupName.Contains(keyword))
                || (x.ServiceType != null && x.ServiceType.Contains(keyword))
                || (x.ServiceDescription != null && x.ServiceDescription.Contains(keyword))
                ).ToList();

            grComplete.DataSource = list; //ESP.Finance.BusinessLogic.SupporterManager.GetWaitAuditList(GetDelegateUser(), terms, parms);
            grComplete.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            grComplete.ItemDataBound += new ComponentArt.Web.UI.Grid.ItemDataBoundEventHandler(grComplete_ItemDataBound);
            grComplete.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(grComplete_NeedRebind);
            grComplete.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(grComplete_PageIndexChanged);

        }

        void grComplete_NeedRebind(object sender, EventArgs e)
        {
            ListBind();
        }

        void grComplete_PageIndexChanged(object sender, ComponentArt.Web.UI.GridPageIndexChangedEventArgs e)
        {
            grComplete.CurrentPageIndex = e.NewIndex;
        }

        void grComplete_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
            depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(int.Parse(e.Item["GroupID"].ToString()), depList);
            string groupname = string.Empty;
            foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
            {
                groupname += dept.DepartmentName + "-";
            }
            e.Item["GroupID"] = groupname.Substring(0, groupname.Length - 1);
            e.Item["LeaderEmployeeName"] = "<a onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(int.Parse(e.Item["LeaderUserID"].ToString())) + "');\">" + e.Item["LeaderEmployeeName"] + "</a>";
            e.Item["AuditStatus"] = "<a href='ProjectWorkFlow.aspx?Type=supporter&FlowID=" + e.Item["SupportID"] + "' target='_blank'><img src='/images/AuditStatus.gif' border='0px;' title='审批状态'></img></a>";
            e.Item["AuditLink"] = "<a href='" + string.Format((int.Parse(e.Item["Status"].ToString()) == 1 || int.Parse(e.Item["Status"].ToString()) == 11 ? Common.SupporterBizUrl : Common.SupporterFinanceUrl), e.Item["SupportID"], e.Item["ProjectID"]) + "&BackUrl=SupportAuditList.aspx'><img src='/images/Audit.gif' border='0px'></img></a>";
            e.Item["PrintLink"] = "<a target='_blank' href='SupporterPrint.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + e.Item["ProjectID"] + "&" + ESP.Finance.Utility.RequestName.SupportID + "=" + e.Item["SupportID"] + "'><img src='/images/SupporterPrint.gif' border='0px;' title='打印预览'></img></a>";
        }

        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }
    }
}
