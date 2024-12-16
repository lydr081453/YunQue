using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpEducationEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["eduid"]))
                {
                    int eduid = int.Parse(Request["eduid"]);
                    ESP.HumanResource.Entity.EmpEducationInfo model = ESP.HumanResource.BusinessLogic.EmpEducationManager.GetModel(eduid);
                    if (model.BeginDate == null)
                        txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    else
                        txtBeginDate.Text = model.BeginDate.ToString("yyyy-MM-dd");

                    if (model.EndDate == null)
                        txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    else
                        txtEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");

                    txtBase_Education.SelectedValue = model.Degree;
                    txtProfession.Text = model.Profession;
                    txtSchool.Text = model.School;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["eduid"]))
            {
                int eduid = int.Parse(Request["eduid"]);
                ESP.HumanResource.Entity.EmpEducationInfo model = ESP.HumanResource.BusinessLogic.EmpEducationManager.GetModel(eduid);
                ESP.HumanResource.BusinessLogic.EmpEducationManager.Delete(eduid);
                //string str = string.Format("art.dialog.close();art.dialog.opener.location='/Management/UserManagement/EmpMgt.aspx?userid=" + model.UserId + "&tabindex=0'");
                //ClientScript.RegisterStartupScript(typeof(string), "", str, true);

                string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

                ClientScript.RegisterStartupScript(typeof(string), "", str, true);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            if (String.IsNullOrEmpty(Request["eduid"]))
            {
                userid = int.Parse(Request["userid"]);
                ESP.HumanResource.Entity.EmpEducationInfo model = new ESP.HumanResource.Entity.EmpEducationInfo();
                model.UserId = userid;
                model.BeginDate = Convert.ToDateTime(txtBeginDate.Text);
                model.EndDate = Convert.ToDateTime(txtEndDate.Text);
                model.Degree = txtBase_Education.SelectedValue;
                model.Profession = txtProfession.Text;
                model.School = txtSchool.Text;
                ESP.HumanResource.BusinessLogic.EmpEducationManager.Add(model);
            }
            else
            {
                int eduid = int.Parse(Request["eduid"]);
                ESP.HumanResource.Entity.EmpEducationInfo model = ESP.HumanResource.BusinessLogic.EmpEducationManager.GetModel(eduid);
                model.BeginDate = Convert.ToDateTime(txtBeginDate.Text);
                model.EndDate = Convert.ToDateTime(txtEndDate.Text);
                model.Degree = txtBase_Education.SelectedValue;
                model.Profession = txtProfession.Text;
                model.School = txtSchool.Text;
                ESP.HumanResource.BusinessLogic.EmpEducationManager.Update(model);
                userid = model.UserId;
            }

            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);

        }

    }
}
