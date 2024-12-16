using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using ESP.Administrative.Entity;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Attendance
{
    public partial class AuditEdit : ESP.Web.UI.PageBase
    {
        private SingleOvertimeManager overtimeManager = new SingleOvertimeManager();
        private ApproveLogManager approvelogManager = new ApproveLogManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
                if (Request["matterid"] != null)
                {
                    InitPage();     
                }
                // 判断是否有返回页面信息
                if (Request["backurl"] != null)
                {
                    BackUrl = Request["backurl"];
                }
                matLeave.BackUrl = BackUrl;
               // matOverTime.BackUrl = BackUrl;
                matTavel.BackUrl = BackUrl;
                matOut.BackUrl = BackUrl;
            }
        }

        protected void Show()
        {
            TabStripTab tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "0";
            tab.Text = "请假";
            tabMatters.Tabs.Add(tab);

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "1";
            tab.Text = "外出";
            tabMatters.Tabs.Add(tab);

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "2";
            tab.Text = "出差";
            tabMatters.Tabs.Add(tab);

        }

        /// <summary>
        /// 初始化页面内容
        /// </summary>
        protected void InitPage()
        {
            if (Request["matterid"] != null)
            {
                int approveId = int.Parse(Request["matterid"]);

                // 审批记录id
                hidApproveId.Value = approveId.ToString();
                ApproveLogInfo applogInfo = approvelogManager.GetModel(approveId);

                // 审批人用户ID
                string approveuserid = UserID.ToString(); 
                // 获得用户所代理的审批人
                IList<ESP.Framework.Entity.AuditBackUpInfo> Delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
                if (Delegates != null && Delegates.Count > 0)
                {
                    foreach (ESP.Framework.Entity.AuditBackUpInfo auditbackup in Delegates)
                    {
                        approveuserid += "," + auditbackup.UserID;
                    }
                }

                if (applogInfo != null && (applogInfo.ApproveID == UserID || approveuserid.IndexOf(applogInfo.ApproveID.ToString()) != -1))
                {
                    // 如果是请假单
                    if (applogInfo.ApproveType == (int)Status.MattersSingle.MattersSingle_Leave)
                    {
                        tabMatters.SelectedTab = tabMatters.Tabs[0];
                        MultiPage1.SelectedIndex = 0;
                        matLeave.Visible = true;
                    }
                    // 如果是外出单
                    if (applogInfo.ApproveType == (int)Status.MattersSingle.MattersSingle_Out)
                    {
                        tabMatters.SelectedTab = tabMatters.Tabs[1];
                        MultiPage1.SelectedIndex = 1;
                        matOut.Visible = true;
                    }
                     // 如果是出差单
                    if (applogInfo.ApproveType == (int)Status.MattersSingle.MattersSingle_Travel)
                    {
                        tabMatters.SelectedTab = tabMatters.Tabs[2];
                        MultiPage1.SelectedIndex = 2;
                        matTavel.Visible = true;
                    }
                   
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您没有权限对此数据进行操作。');", true);
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('您没有权限对此数据进行操作。');", true);
                return;
            }
        }

        private string _backUrl = "AuditList.aspx";
        /// <summary>
        /// 返回URL
        /// </summary>
        public string BackUrl
        {
            get
            {
                return _backUrl;
            }
            set
            {
                _backUrl = value;
            }
        }
    }
}
