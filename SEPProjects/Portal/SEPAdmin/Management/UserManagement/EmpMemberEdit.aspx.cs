using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class EmpMemberEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["memberid"]))
                {
                    int memberid = int.Parse(Request["memberid"]);
                    ESP.HumanResource.Entity.EmpFamilyInfo model = ESP.HumanResource.BusinessLogic.EmpFamilyManager.GetModel(memberid);
                    txtAge.Text = model.Age.ToString();
                    txtCompany.Text = model.Company;
                    txtEmail.Text = model.Email;
                    txtMemberName.Text = model.MemberName;
                    txtPhone.Text = model.Phone;
                    txtRelation.Text = model.Relation;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["memberid"]))
            {
                int memberid = int.Parse(Request["memberid"]);
                ESP.HumanResource.Entity.EmpFamilyInfo model = ESP.HumanResource.BusinessLogic.EmpFamilyManager.GetModel(memberid);
                ESP.HumanResource.BusinessLogic.EmpFamilyManager.Delete(memberid);

                string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

                ClientScript.RegisterStartupScript(typeof(string), "", str, true);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid=0;
            int age = 0;
            int.TryParse(txtAge.Text, out age);

            if (String.IsNullOrEmpty(Request["memberid"]))
            {
                userid = int.Parse(Request["userid"]);
                ESP.HumanResource.Entity.EmpFamilyInfo model = new ESP.HumanResource.Entity.EmpFamilyInfo();
                model.UserId = userid;
                model.Age = age;
                model.Company = txtCompany.Text;
                model.Email = txtEmail.Text;
                model.MemberName = txtMemberName.Text;
                model.Phone = txtPhone.Text;
                model.Relation = txtRelation.Text;

                ESP.HumanResource.BusinessLogic.EmpFamilyManager.Add(model);
            }
            else
            {
                int memberid = int.Parse(Request["memberid"]);
                ESP.HumanResource.Entity.EmpFamilyInfo model = ESP.HumanResource.BusinessLogic.EmpFamilyManager.GetModel(memberid);
                model.Age = age;
                model.Company = txtCompany.Text;
                model.Email = txtEmail.Text;
                model.MemberName = txtMemberName.Text;
                model.Phone = txtPhone.Text;
                model.Relation = txtRelation.Text;
                ESP.HumanResource.BusinessLogic.EmpFamilyManager.Update(model);
                userid = model.UserId;
            }


            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);

        }
       
    }
}
