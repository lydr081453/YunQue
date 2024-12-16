using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpWorkEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["workid"]))
                {
                    int workid = int.Parse(Request["workid"]);
                    ESP.HumanResource.Entity.EmpWorkExpInfo model = ESP.HumanResource.BusinessLogic.EmpWorkExpManager.GetModel(workid);
                    this.txtCompany.Text = model.Company;
                    this.txtDept.Text = model.Dept;
                    this.txtDirector.Text = model.Director;
                    this.txtEmail.Text = model.Email;
                    this.txtExperience.Text = model.Experience;
                    this.txtJoinDate.Text = model.JoinDate.ToString("yyyy-MM-dd");
                    this.txtPosition.Text = model.Position;
                    this.txtServeYear.Text = model.ServeYear;
                    this.txtSkill.Text = model.Skill;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["workid"]))
            {
                int workid = int.Parse(Request["workid"]);
                ESP.HumanResource.Entity.EmpWorkExpInfo model = ESP.HumanResource.BusinessLogic.EmpWorkExpManager.GetModel(workid);
                ESP.HumanResource.BusinessLogic.EmpWorkExpManager.Delete(workid);

                string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

                ClientScript.RegisterStartupScript(typeof(string), "", str, true);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            int serveyear = 0;
            int.TryParse(txtServeYear.Text,out serveyear);
            if (String.IsNullOrEmpty(Request["workid"]))
            {
                userid = int.Parse(Request["userid"]);
                ESP.HumanResource.Entity.EmpWorkExpInfo model = new ESP.HumanResource.Entity.EmpWorkExpInfo();
                model.UserId = userid;
                model.Company = this.txtCompany.Text;
                model.Dept = this.txtDept.Text;
                model.Director = this.txtDirector.Text;
                model.Email = this.txtEmail.Text;
                model.Experience = this.txtExperience.Text;
                if (!string.IsNullOrEmpty(this.txtJoinDate.Text))
                    model.JoinDate = Convert.ToDateTime(this.txtJoinDate.Text);
                model.Position = this.txtPosition.Text;
                model.ServeYear = serveyear.ToString();
                model.Skill = txtSkill.Text;
                ESP.HumanResource.BusinessLogic.EmpWorkExpManager.Add(model);
            }
            else
            {
                int workid = int.Parse(Request["workid"]);
                ESP.HumanResource.Entity.EmpWorkExpInfo model = new ESP.HumanResource.Entity.EmpWorkExpInfo();
                model.Company = this.txtCompany.Text;
                model.Dept = this.txtDept.Text;
                model.Director = this.txtDirector.Text;
                model.Email = this.txtEmail.Text;
                model.Experience = this.txtExperience.Text;
                if (!string.IsNullOrEmpty(this.txtJoinDate.Text))
                    model.JoinDate = Convert.ToDateTime(this.txtJoinDate.Text);
                model.Position = this.txtPosition.Text;
                model.ServeYear = serveyear.ToString();
                model.Skill = txtSkill.Text;
                ESP.HumanResource.BusinessLogic.EmpWorkExpManager.Update(model);
                userid = model.UserId;
            }
            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);
        }
    }
}
