using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Talents
{
    public partial class BackupPrint : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(Request["talentid"]))
            {
                ESP.HumanResource.Entity.TalentInfo model = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetModel(int.Parse(Request["talentid"]));
                lblUserName.Text = model.NameCN;
                lblMobile.Text = model.Mobile;
                lblBirthday.Text = model.BirthDay.ToString("yyyy-MM-dd");
                
                lblLanguage.Text = model.Language;
                lblEducation.Text = model.Education;
                lblWorkBegin.Text = model.WorkBegin.ToString("yyyy-MM-dd");
              
                lblLocation.Text = model.DeptShunya;
                lblPosition.Text = model.Position;

                lblProfessional.Text = model.Professional;
                lblService.Text = model.Customer;
                lblResume.Text = model.Resume;

                lblHRAudit.Text = model.HRInterview;
                lblGroupAudit.Text = model.GroupInterview;

            }
        }
    }
}
