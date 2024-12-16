using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["user"]!=null && int.Parse(Session["user"].ToString())>0)
            {
                Response.Redirect("../UserPage/MainPage.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            SC_Supplier model= new SC_Supplier();
            if (!string.IsNullOrEmpty(txtName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()) && !string.IsNullOrEmpty(txtPasswordConfrim.Text.Trim()))
            {
                MD5 m = MD5CryptoServiceProvider.Create();
                byte[] p = m.ComputeHash(Encoding.Default.GetBytes(txtPassword.Text));

                model.LogName = txtName.Text.Trim();
                //model.Password = txtPassword.Text.Trim();
                model.Password = Encoding.Default.GetString(p);
                model.supplier_name = txtSupplierName.Text.Trim();
                model.IsPerson = int.Parse(radlIsPerson.SelectedValue);
                model.contact_Mobile = txtMobile.Text.Trim();
                model.contact_Tel = txtTel.Text.Trim();
                model.contact_Email = txtEmail.Text.Trim();
                model.contact_address = txtAddress.Text.Trim();
                //model.supplier_sn = txtSN.Text.Trim();
                model.Status = State.SupplierStatus_save;
                model.CreatTime = DateTime.Now;
                model.LastUpdateTime = DateTime.Now;
                SC_SupplierManager scsm= new SC_SupplierManager();
                int result = scsm.Add(model);
                if(result > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册成功,请等待激活信!');window.location='../Default.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册失败!');window.location='../Default.aspx';", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写完整信息!');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //返回首页
            Response.Redirect("../Default.aspx");
        }
    }
}
