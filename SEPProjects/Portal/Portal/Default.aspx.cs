using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using System.Text;
using System.Data.SqlClient;

namespace Portal.WebSite
{
    public partial class Default1 : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUserInfo();
                getAssetInfo();
                imgUserIcon.Src = UserIcon;

            }
        }

        private void getUserInfo()
        {
            string script = string.Empty;
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(UserID);
            IList<string> depts = emp.GetDepartmentNames();
            string deptstr = string.Empty;
            if (depts != null && depts.Count != 0)
            {
                for (int i = 0; i < depts.Count; i++)
                {
                    deptstr += depts[i].ToString() + ",";
                }
            }
            string empname = emp.Name == string.Empty ? "&nbsp;" : emp.Name;
            string empITCode = emp.ITCode == string.Empty ? "&nbsp;" : emp.ITCode;
            string empMobile = emp.Mobile == string.Empty ? "&nbsp;" : emp.Mobile;
            string empEmail = emp.EMail == string.Empty ? "&nbsp;" : emp.EMail;
            string empTel = emp.Telephone == string.Empty ? "&nbsp;" : emp.Telephone;
            string strdept = deptstr.TrimEnd(',') == string.Empty ? "&nbsp;" : deptstr.TrimEnd(',');

            script += "<span class=\"title_red\">" + empname + "</span><br />";
            script += "<span class=\"text_red\">员工账号：" + empITCode + "</span><br />";
            script += "员工编号：" + emp.ID + "<br />";
            script += "所属部门：" + strdept + "<br />";

            labUserInfo.Text = script;

            ESP.Finance.Entity.DeadLineInfo deadLine = ESP.Finance.BusinessLogic.DeadLineManager.GetCurrentMonthModel();

            if (DateTime.Now > deadLine.ExpenseAuditDeadLine)
            {
                ESP.Finance.Entity.DeadLineInfo deadLine2 = ESP.Finance.BusinessLogic.DeadLineManager.GetNextMonthModel();
                if (DateTime.Now > deadLine.ExpenseAuditDeadLine2)
                {
                    if (deadLine2 != null)
                    {
                        lblNotify.Text = "本次报销提交截止日期:" + deadLine2.ExpenseCommitDeadLine.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" +
                                            "本次报销审批截止日期:" + deadLine2.ExpenseAuditDeadLine.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    lblNotify.Text = "本次报销提交截止日期:" + deadLine.ExpenseCommitDeadLine2.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" +
                                     "本次报销审批截止日期:" + deadLine.ExpenseAuditDeadLine2.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                lblNotify.Text = "本次报销提交截止日期:" + deadLine.ExpenseCommitDeadLine.ToString("yyyy-MM-dd") + "&nbsp;&nbsp;&nbsp;&nbsp;" +
                                 "本次报销审批截止日期:" + deadLine.ExpenseAuditDeadLine.ToString("yyyy-MM-dd");
            }
        }

        private void getAssetInfo()
        {
            string script = "<font style='color:orange;'>●</font>&nbsp;&nbsp;";
            string assetString = string.Empty;
            string terms = " usercode=@usercode and status=@status";
            List<SqlParameter> parms = new List<SqlParameter>();
            int icount = 0;
            parms.Add(new SqlParameter("@usercode", CurrentUser.ID));
            parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

            IList<ESP.Finance.Entity.ITAssetReceivingInfo> list = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.Finance.Entity.ITAssetReceivingInfo rec in list)
                {
                    if (icount >= 2)
                        break;
                    ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(rec.AssetId);
                    if (model != null)
                    {
                        assetString += script + model.AssetName +"&nbsp;&nbsp;"+rec.ReceiveDate.ToString("yyyy-MM-dd")+ "<br/>";
                        icount++;
                    }
                }
                if (list.Count > 2)
                    assetString += "<font style='color:orange;'>●</font>&nbsp;&nbsp; <span onclick=\"ShowAssetMsg(" + CurrentUserID + ")\" onmouseover=\"this.style.cursor='pointer',this.style.color='#f97b02'\" onmouseout=\"this.style.cursor='auto',this.style.color='#666666'\" >更多("+list.Count+")...</span>";
            }

            this.lblAsset.Text = assetString;

        }

        protected string UserIcon
        {
            get
            {
                if (UserID > 0)
                {
                    ESP.Framework.Entity.EmployeeInfo info = ESP.Framework.BusinessLogic.EmployeeManager.Get(UserID);
                    if (info != null && info.Photo.Trim().Length > 0)
                    {
                        return Portal.Common.Global.USER_ICON_FOLDER + info.Photo;
                    }
                }
                return "";
            }
        }
    }
}
