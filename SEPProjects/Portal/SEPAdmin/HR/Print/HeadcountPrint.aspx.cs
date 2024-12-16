using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Print
{
    public partial class HeadcountPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindInfo();
            }
        }

        private void bindInfo()
        {
            if (!string.IsNullOrEmpty(Request["haid"]))
            {
                int haid = int.Parse(Request["haid"]);
                var model = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetModel(haid);
                if (model.OfferLetterUserId != 0)
                {
                    ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(model.OfferLetterUserId);
                    ESP.HumanResource.Entity.EmployeeBaseInfo empModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.OfferLetterUserId);
                    ESP.Administrative.Entity.OperationAuditManageInfo audit = (new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(model.OfferLetterUserId);
                    ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.OfferLetterUserId);
                    ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(position.DepartmentID);
                    var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + model.Id);

                    this.lblUserName.Text = userModel.FullNameCN;
                    lblAddress.Text = empModel.Address;
                    lblApprove.Text = "";
                    lblApproveDate.Text = "";
                    lblAuditor.Text = audit.TeamLeaderName;
                    lblBirthday.Text = empModel.Birthday == new DateTime(1754, 1, 1) ? "" : empModel.Birthday.ToString("yyyy-MM-dd");
                    lblDirector.Text = operation.DirectorName;
                    lblEducation.Text = empModel.Education;


                    lblGender.Text = ESP.HumanResource.Common.Status.Gender_Names[(int)empModel.Gender];
                    lblMarry.Text = ESP.HumanResource.Common.Status.MaritalStatus_Names[(int)empModel.MaritalStatus];

                    lblJoinDate.Text = position.BeginDate.ToString("yyyy-MM-dd");
                    lblLocation.Text = empModel.BirthPlace;

                    lblPosition.Text = model.Position;
                    lblPositionTest.Text = position.DepartmentPositionName;
                    lblSalary.Text = empModel.Pay.ToString();
                    lblSalary2.Text = empModel.Performance.ToString();

                    var hrlog = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewHR).FirstOrDefault();
                    var grouplog = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewGroup).OrderByDescending(x=>x.AuditDate).FirstOrDefault();
                    var approvelog = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.VPApproved).FirstOrDefault();
                    lblHRAudit1.Text = hrlog == null ? "" : hrlog.Remark;
                    lblHR1.Text = hrlog == null ? "" : hrlog.Auditor;
                    lblHRDate1.Text = hrlog == null ? "" : hrlog.AuditDate.ToString("yyyy-MM-dd");

                    lblGroupAudit.Text = grouplog == null ? "" : grouplog.Remark;
                    lblGroup.Text = grouplog == null ? "" : grouplog.Auditor;
                    lblGroupDate.Text = grouplog == null ? "" : grouplog.AuditDate.ToString("yyyy-MM-dd");

                    lblApprove.Text = approvelog == null ? "" : approvelog.Auditor;
                    lblApproveDate.Text = approvelog == null ? "" : approvelog.AuditDate.ToString("yyyy-MM-dd");

                    lblAppearance.Text = empModel.Appearance;
                    lblEqual.Text = empModel.Equal;
                    lblKnow.Text = empModel.Know;
                    lblQuality.Text = empModel.Quality;
                    lblEQ.Text = empModel.EQ;
                    lbl4D.Text = empModel.FourD;
                    lblReason.Text = empModel.Motivation;
                }
            }
        }
    }
}
