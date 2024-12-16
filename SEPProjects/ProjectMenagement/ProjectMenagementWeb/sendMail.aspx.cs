using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb
{
    public partial class sendMail :ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTitle.Text = "{0}项目关闭提醒";
                txtBody.Text = @"{0}{1}按计划将于12月14日关闭。<br /><br />
                                {0}将在12月14日下班时提前关闭，烦请按照财务部张靖 2015年11月24日 18:31分邮件要求在此前完成相关项目的所有采购和报销申请。错过上述时间节点将无法再提交采购和报销单。多谢配合！
                                  <br /><br />"; 
            }
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {

            string terms = " and (enddate between '" + txtBegin.Text + "' and '" + txtEnd.Text + "') and projectcode <> ''";

            string projectStr = string.Empty;

            if (!string.IsNullOrEmpty(txtProject.Text))
            {
                string[] projects = txtProject.Text.Split(',');
                for (int i = 0; i < projects.Count(); i++)
                {
                    projectStr += "'" + projects[i] + "',";
                }

                projectStr = projectStr.TrimEnd(',');

                terms = " and projectcode in(" + projectStr + ")";
            }

            var plist = ESP.Finance.BusinessLogic.ProjectManager.GetList(terms, null);

            foreach (ESP.Finance.Entity.ProjectInfo project in plist)
            {
                string title = txtTitle.Text;// "{0}项目关闭提醒";
                string body = txtBody.Text;

                body = string.Format(body, project.ProjectCode, project.BusinessDescription);
                title = string.Format(title, project.ProjectCode);
                try
                {
                    string RemindEmail = GetRemindEmail(project.ProjectId);
                    ESP.Finance.Utility.SendMail.Send1(title, RemindEmail, body, true);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private static string GetRemindEmail(int projectId)
        {
            string strEmails = string.Empty;
            string terms = " projectID=" + projectId;
            IList<ESP.Finance.Entity.ProjectMemberInfo> projectMembers = new ESP.Finance.DataAccess.ProjectMemberDataProvider().GetList(terms, null);
            IList<ESP.Finance.Entity.SupportMemberInfo> supportMembers = new ESP.Finance.DataAccess.SupportMemberDataProvider().GetList(terms, null);
            foreach (ESP.Finance.Entity.ProjectMemberInfo project in projectMembers)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(project.MemberUserID.Value);
                if (emp != null && emp.Status==1)
                {
                    string email = emp.Email;
                    if (email.Trim() != "")
                        strEmails += email + ",";
                }
            }
            foreach (ESP.Finance.Entity.SupportMemberInfo support in supportMembers)
            {
                ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(support.MemberUserID.Value);
                if (emp != null && emp.Status == 1)
                {
                    string email = emp.Email;
                    if (email.Trim() != "")
                        strEmails += email + ",";
                }
            }
            return strEmails ;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string terms = " and (enddate between '" + txtBegin.Text + "' and '" + txtEnd.Text + "') and projectcode <> ''";

            string projectStr = string.Empty;

            if (!string.IsNullOrEmpty(txtProject.Text))
            {
                string[] projects = txtProject.Text.Split(',');
                for (int i = 0; i < projects.Count(); i++)
                {
                    projectStr += "'" + projects[i] + "',";
                }

                projectStr = projectStr.TrimEnd(',');

                terms = " and projectcode in(" + projectStr + ")";
            }

            var plist = ESP.Finance.BusinessLogic.ProjectManager.GetList(terms, null);

            this.gvG.DataSource = plist;
            this.gvG.DataBind();
        }
    }
}