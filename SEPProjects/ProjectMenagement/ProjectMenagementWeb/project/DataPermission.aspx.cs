using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class DataPermission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 60000;
        }

        protected void btnSupport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSupporterID.Text.Trim()))
            {
                ESP.Finance.Entity.SupporterInfo model = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(this.txtSupporterID.Text.Trim()));
                saveSupporter(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('no record!');", true);
        }

        protected void btnSupportAll_Click(object sender, EventArgs e)
        {
            IList<ESP.Finance.Entity.SupporterInfo> ReturnList = ESP.Finance.BusinessLogic.SupporterManager.GetAllList();
            int listcount = ReturnList.Count;
            int count = 0;
            foreach (ESP.Finance.Entity.SupporterInfo model in ReturnList)
            {
                saveSupporter(model);
                count++;
            }
            if (listcount == count)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('failed!');", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtReturnID.Text.Trim()))
            {
                ESP.Finance.Entity.ReturnInfo model = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(this.txtReturnID.Text.Trim()));
                saveReturn(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('no record!');", true);
        }

        protected void btnReturnAll_Click(object sender, EventArgs e)
        {
            IList<ESP.Finance.Entity.ReturnInfo> ReturnList = ESP.Finance.BusinessLogic.ReturnManager.GetAllList();
            int listcount = ReturnList.Count;
            int count = 0;
            foreach (ESP.Finance.Entity.ReturnInfo model in ReturnList)
            {
                saveReturn(model);
                count++;
            }
            if (listcount == count)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('failed!');", true);
            }
        }

        protected void btnProject_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtProjectID.Text.Trim()))
            {
                ESP.Finance.Entity.ProjectInfo model = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(this.txtProjectID.Text.Trim()));
                saveProject(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('no record!');", true);
        }

        protected void btnProjectAll_Click(object sender, EventArgs e)
        {
            IList<ESP.Finance.Entity.ProjectInfo> projectList = ESP.Finance.BusinessLogic.ProjectManager.GetAllList();
            int listcount = projectList.Count;
            int count = 0;
            foreach (ESP.Finance.Entity.ProjectInfo model in projectList)
            {
                saveProject(model);
                count++;
            }
            if (listcount == count)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('failed!');", true);
            }
        }

        private void saveProject(ESP.Finance.Entity.ProjectInfo model)
        {
            ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
            datainfo.DataId = model.ProjectId;
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            ESP.Purchase.Entity.DataPermissionInfo permissionCreator = new ESP.Purchase.Entity.DataPermissionInfo();
            permissionCreator.UserId = model.CreatorID;
            permissionCreator.IsEditor = true;
            permissionCreator.IsViewer = true;
            permissionList.Add(permissionCreator);
            ESP.Purchase.Entity.DataPermissionInfo permissionResponser = new ESP.Purchase.Entity.DataPermissionInfo();
            permissionResponser.UserId = model.ApplicantUserID;
            permissionResponser.IsEditor = true;
            permissionResponser.IsViewer = true;
            permissionList.Add(permissionResponser);
            IList<ESP.Finance.Entity.ProjectMemberInfo> memberlist = ESP.Finance.BusinessLogic.ProjectMemberManager.GetListByProject(model.ProjectId, null, null);

            if (memberlist != null)
            {
                foreach (ESP.Finance.Entity.ProjectMemberInfo mem in memberlist)
                {
                    ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                    p.UserId = mem.MemberUserID.Value;
                    p.IsEditor = false;
                    p.IsViewer = true;
                    permissionList.Add(p);
                }
            }
            IList<ESP.Finance.Entity.AuditHistoryInfo> auditList = ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(" projectid=" + model.ProjectId);
            foreach (ESP.Finance.Entity.AuditHistoryInfo audit in auditList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p1 = new ESP.Purchase.Entity.DataPermissionInfo();
                p1.UserId = audit.AuditorUserID.Value;
                p1.IsEditor = true;
                p1.IsViewer = true;
                permissionList.Add(p1);
            }
            ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList);
        }

        private void saveReturn(ESP.Finance.Entity.ReturnInfo model)
        {
            ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Return;
            datainfo.DataId = model.ReturnID;
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            IList<ESP.Finance.Entity.ReturnAuditHistInfo> auditList = ESP.Finance.BusinessLogic.ReturnAuditHistManager.GetList(" returnid=" + model.ReturnID.ToString());
            ESP.Purchase.Entity.DataPermissionInfo prequest = new ESP.Purchase.Entity.DataPermissionInfo();
            prequest.UserId = model.RequestorID.Value;
            prequest.IsEditor = true;
            prequest.IsViewer = true;
            permissionList.Add(prequest);
            foreach (ESP.Finance.Entity.ReturnAuditHistInfo audit in auditList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
                p.UserId = audit.AuditorUserID.Value;
                p.IsEditor = true;
                p.IsViewer = true;
                permissionList.Add(p);
            }
            ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList);
        }

        private void saveSupporter(ESP.Finance.Entity.SupporterInfo model)
        {
            ESP.Purchase.Entity.DataInfo datainfo = new ESP.Purchase.Entity.DataInfo();
            datainfo.DataId = model.SupportID;
            datainfo.DataType = (int)ESP.Purchase.Common.State.DataType.Supporter;
            List<ESP.Purchase.Entity.DataPermissionInfo> permissionList = new List<ESP.Purchase.Entity.DataPermissionInfo>();
            ESP.Purchase.Entity.DataPermissionInfo p = new ESP.Purchase.Entity.DataPermissionInfo();
            p.UserId = model.LeaderUserID.Value;
            p.IsEditor = true;
            p.IsViewer = true;
            permissionList.Add(p);
            IList<ESP.Finance.Entity.SupportMemberInfo> memberList = ESP.Finance.BusinessLogic.SupportMemberManager.GetList(" supportID=" + model.SupportID.ToString());
            IList<ESP.Finance.Entity.SupporterAuditHistInfo> auditList = ESP.Finance.BusinessLogic.SupporterAuditHistManager.GetList(" SupporterID=" + model.SupportID.ToString());
            foreach (ESP.Finance.Entity.SupportMemberInfo mem in memberList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p1 = new ESP.Purchase.Entity.DataPermissionInfo();
                p1.UserId = mem.MemberUserID.Value;
                p1.IsEditor = false;
                p1.IsViewer = true;
                permissionList.Add(p1);
            }
            foreach (ESP.Finance.Entity.SupporterAuditHistInfo audit in auditList)
            {
                ESP.Purchase.Entity.DataPermissionInfo p2 = new ESP.Purchase.Entity.DataPermissionInfo();
                p2.UserId = audit.AuditorUserID.Value;
                p2.IsEditor = true;
                p2.IsViewer = true;
                permissionList.Add(p2);
            }
            ESP.Purchase.BusinessLogic.DataPermissionManager.AddDataPermission(datainfo, permissionList);
        }

        private void saveGeneral(ESP.Purchase.Entity.GeneralInfo model)
        { 
        
        }
        protected void btnPR_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPRID.Text.Trim()))
            {
                ESP.Purchase.Entity.GeneralInfo model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(Convert.ToInt32(this.txtPRID.Text.Trim()));
                saveGeneral(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('no record!');", true);
        }

        protected void btnPRAll_Click(object sender, EventArgs e)
        {
            IList<ESP.Purchase.Entity.GeneralInfo> projectList = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetStatusList("");
            int listcount = projectList.Count;
            int count = 0;
            foreach (ESP.Purchase.Entity.GeneralInfo model in projectList)
            {
                saveGeneral(model);
                count++;
            }
            if (listcount == count)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('success!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('failed!');", true);
            }
        }

        protected void btnCost_Click(object sender, EventArgs e)
        {
            ESP.Finance.BusinessLogic.CostRecordManager.InsertCost(txtProjectCode.Text);
        }

        protected void btnUserPoint_Click(object sender, EventArgs e)
        {
            ESP.UserPoint.Entity.UserPointInfo model = ESP.UserPoint.BusinessLogic.UserPointManager.GetModel(13414);
        }
    }
}
