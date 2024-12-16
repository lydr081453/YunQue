using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfoUpdatePassword : System.Web.UI.Page
    {
        private int _supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                _supplierId = int.Parse(Session["user"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SC_SupplierManager ssm = new SC_SupplierManager();
            SC_SupplierHistoryManager sshm = new SC_SupplierHistoryManager();
            SC_Supplier model = ssm.GetModel(_supplierId);

            if (null != model)
            {
                MD5 m = MD5CryptoServiceProvider.Create();
                byte[] oldpassword = m.ComputeHash(Encoding.Default.GetBytes(txtOldPassword.Text));
                byte[] newpassword = m.ComputeHash(Encoding.Default.GetBytes(txtNewPassword.Text));

                if(Encoding.Default.GetString(oldpassword)!=model.Password)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('原始密码填写不正确!');", true);
                }

                SC_SupplierHistory shmodel = sshm.SetModel(model);

                model.Password = Encoding.Default.GetString(newpassword);
                model.LastUpdateTime = DateTime.Now;

                if (sshm.Add(shmodel) > 0)
                {
                    ssm.Update(model);
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('密码更新成功!');window.location='UserInfo.aspx';", true);
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('密码更新失败!');window.location='UserInfo.aspx';", true);

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }
    }
}
