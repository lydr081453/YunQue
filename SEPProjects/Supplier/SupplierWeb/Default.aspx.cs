using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;

namespace SupplierWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                Session["user"] = null;
            }
            if (Session["Register"] != null)
            {
                Session["Register"] = null;
            }
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                SC_SupplierManager ssm = new SC_SupplierManager();
                int userid = 0;

                MD5 m = MD5CryptoServiceProvider.Create();
                byte[] p = m.ComputeHash(Encoding.Default.GetBytes(txtPassword.Text));

                userid = ssm.Login(txtName.Text.Trim(), Encoding.Default.GetString(p));
                if (userid > 0)
                {
                    Session["user"] = userid.ToString();
                    ClientScript.RegisterStartupScript(typeof(string), "", "window.location='UserPage/MainPage.aspx'", true);
                }
                else
                {
                    if(userid <0)
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('请等待管理员审核您的帐户!');", true);
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('用户名密码有误!');", true);
                }
            }
        }
    }
}
