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

namespace SupplierWeb.UserRegister
{
    public partial class UserRegist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                Response.Redirect("../UserPage/MainPage.aspx");
            }
            if (Session["Register"] != null)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            SC_Supplier model = Session["Register"] as SC_Supplier;
            if (null != model)
            {
                if (!string.IsNullOrEmpty(model.LogName))
                    txtName.Text = model.LogName;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    txtPassword.Text = model.Password;
                    txtPasswordConfrim.Text = model.Password;
                }
                if(null != model.IsPerson)
                    radlIsPerson.SelectedValue = model.IsPerson.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SC_Supplier model = new SC_Supplier();
            if (!string.IsNullOrEmpty(txtName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()) && !string.IsNullOrEmpty(txtPasswordConfrim.Text.Trim()))
            {
                //MD5 m = MD5CryptoServiceProvider.Create();
                //byte[] p = m.ComputeHash(Encoding.Default.GetBytes(txtPassword.Text));

                if ((new SC_SupplierManager()).GetListByName(txtName.Text.Trim()).Count>0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('该用户名已存在!');", true);
                    return;
                }

                model.LogName = txtName.Text.Trim();
                //model.Password = Encoding.Default.GetString(p);
                model.Password = txtPassword.Text.Trim();
                model.IsPerson = int.Parse(radlIsPerson.SelectedValue);
                Session["Register"] = model;
                //model.IsPerson = int.Parse(radlIsPerson.SelectedValue);
                //model.Status = State.SupplierStatus_save;
                //model.CreatTime = DateTime.Now;
                //model.LastUpdateTime = DateTime.Now;
                //SC_SupplierManager scsm = new SC_SupplierManager();
                //int result = scsm.Add(model);
                //if (result > 0)
                //{
                //    //ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册成功,请等待激活信!');window.location='../Default.aspx';", true);
                //    if(model.IsPerson >0)
                //        Response.Redirect(string.Format("UserRegisterPersonStep1.aspx?sid={0}",result.ToString()));
                //    else
                //        Response.Redirect(string.Format("UserRegistCompanyStep1.aspx?sid={0}", result.ToString()));
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册失败!');window.location='../Default.aspx';", true);
                //}
                if(model.IsPerson >0)
                    Response.Redirect("UserRegisterPersonStep1.aspx");
                else
                    Response.Redirect("UserRegistCompanyStep1.aspx");
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
