using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Print
{
    public partial class HeadCountMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["haid"]) )
                {
                    initForm(int.Parse(Request["haid"].ToString()));
                }
            }
        }

        protected void initForm(int haid)
        {
            imgs.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/mail_03.jpg";

            ESP.HumanResource.Entity.HeadAccountInfo headModel = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetModel(haid);
            ESP.Finance.Entity.DepartmentViewInfo deptModel =ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(headModel.GroupId);
            ESP.HumanResource.Entity.PositionLevelsInfo levelModel =ESP.HumanResource.BusinessLogic.PositionLevelsManager.GetModel(headModel.LevelId);
            var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + headModel.Id);

            this.lblDept.Text = deptModel.level1 + "-" + deptModel.level2 + "-" + deptModel.level3;
           
            this.lblLevel.Text = headModel.LevelName;
            this.lblPosition.Text = headModel.Position;
            //this.lblSalary.Text = levelModel.SalaryLow.ToString("#,##0.00") +"-" + levelModel.SalaryHigh.ToString("#,##0.00");
            if (headModel.ReplaceUserId != 0)
            {
                ESP.HumanResource.Entity.UsersInfo empModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(headModel.ReplaceUserId);
                this.lblName.Text = empModel.LastNameCN + empModel.FirstNameCN;
            }

            if (headModel.Status == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.Commit)
                lblMessage.Text = headModel.Creator + "提交了Headcount申请，信息如下";
            else 
            {
                string logstr=string.Empty;
                foreach (var log in loglist)
                {
                    string auditstatus = "审批通过";
                    if (log.Status == 2)
                        auditstatus = "审批驳回";
                    else if (log.Status == 3)
                    {
                        auditstatus = "审批留言";
                    }
                    logstr += string.Format("[{0}] {1}{2}：{3}（{4}）", ESP.HumanResource.Common.Status.HeadAccountStatus_Names[log.AuditType], log.Auditor, auditstatus, log.Remark, log.AuditDate.ToString()) + "<br />";
                }
                 lblMessage.Text = logstr ;
            }

        }

      
    }
}
