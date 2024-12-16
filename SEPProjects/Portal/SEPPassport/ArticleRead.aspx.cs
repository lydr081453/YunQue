using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.Entity;
using System.Web.Security;

namespace PassportWeb
{
    public partial class ArticleRead : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["uid"]) )
            {
                this.trManual.Visible = true;
                this.CustomValidator2.Visible = true;
                this.trClause.Visible = true;
                this.CustomValidator1.Visible = true;

            }
            if (!IsPostBack)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(Request["uid"]));
                ESP.Compatible.Employee emp2 = new ESP.Compatible.Employee(int.Parse(Request["uid"]));
                this.lblUserName.Text = emp2.Name;
                this.lblUserCode.Text = emp2.ID;
                //this.txtID1.Text = emp.IDNumber;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //if (radFW1.Checked)
            //{
            if (!string.IsNullOrEmpty(Request["uid"]))
                {
                    //ESP.HumanResource.Entity.EmployeeBaseInfo emp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(int.Parse(Request["uid"]));
                    //emp.IDNumber = this.txtID1.Text;
                    //ESP.HumanResource.BusinessLogic.EmployeeBaseManager.Update(emp);

                    ESP.Purchase.Entity.sepArticleInfo air = new ESP.Purchase.Entity.sepArticleInfo();
                    air.SysUserID = Convert.ToInt32(Request["uid"]);
                    air.IsRead = true;
                    air.CreatedDate = DateTime.Now;
                    new ESP.Purchase.BusinessLogic.sepArticleManager().Add(air);

                    string script = string.Empty;
                    string ucode = string.Empty;
                    if (!string.IsNullOrEmpty(Request["ucode"]))
                        ucode = Request["ucode"];

                    string url = Server.UrlDecode(Request["rurl"]);

                    ESP.Security.PassportAuthentication.SetAuthCookie(Convert.ToInt32(Request["uid"]), Request["uname"]);

                    script += "parent.document.URL= '" + url + "';";
                    script += @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
                }
         
        }

      
    }
}