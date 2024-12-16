using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Talents
{
    public partial class TalentEdit : System.Web.UI.Page
    {
        ESP.Administrative.Entity.TalentInfo talentModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = int.Parse(Request["userid"]);

            talentModel = (new ESP.Administrative.BusinessLogic.TalentManager()).GetModel(userid);

            if (!IsPostBack)
            {
                bindData();
            }
        }

        private void bindData()
        {

            lblDept.Text = talentModel.DeptName;
            lblEmail.Text = talentModel.PrivateEmail;
            lblEducation.Text = talentModel.GraduatedFrom;

            lblMobile.Text = talentModel.Phone2;
            lblNameCN.Text = talentModel.UserName;
            lblNameEN.Text = talentModel.NameEN;
            lblPosition.Text = talentModel.DepartmentPositionName;
            lblPosition2.Text = talentModel.DepartmentPositionName;

            txtResume.Text = talentModel.WorkExperience;

            lblSalary.Text = "";
            if (talentModel.WorkBegin != null)
                lblWorkBegin.Text = talentModel.WorkBegin <= new DateTime(1900, 1, 1) ? "" : talentModel.WorkBegin.ToString("yyyy-MM-dd");


            if (!string.IsNullOrEmpty(talentModel.Resume))
            {
                labResume.Text = "<a target='_blank' href='" + talentModel.Resume + "'><img src='/Images/dc.gif' border='0' /></a>";
                labResume.Visible = true;
            }
            else
            {
                labResume.Visible = false;
            }

            ESP.HumanResource.Entity.HeadAccountInfo headcount = new ESP.HumanResource.BusinessLogic.HeadAccountManager().GetList(" and OfferLetterUserId=" + talentModel.UserID).FirstOrDefault();

            if (headcount != null)
            {
                var loglist = new ESP.HumanResource.BusinessLogic.HeadAccountLogManager().GetList(" and HeadAccountId=" + headcount.Id);

                var hrDesc = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewHR).FirstOrDefault();
                var groupDesc = loglist.Where(x => x.AuditType == (int)ESP.HumanResource.Common.Status.HeadAccountStatus.InterviewGroup).FirstOrDefault();
                this.lblGroup.Text = groupDesc == null ? "" : groupDesc.Remark;
                this.lblHR.Text = hrDesc == null ? "" : hrDesc.Remark;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(talentModel.UserID);

            emp.WorkExperience = txtResume.Text;

            ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(emp);

            string bakurl = Request["bakurl"];
            if (!string.IsNullOrEmpty(bakurl))
                Response.Redirect(bakurl);
            else
            Response.Redirect("InternalList.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string bakurl = Request["bakurl"];
            if (!string.IsNullOrEmpty(bakurl))
                Response.Redirect(bakurl);
            else
                Response.Redirect("InternalList.aspx");
        }

    }
}
