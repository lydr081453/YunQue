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
    public partial class UserRegistCompanyStep1 : System.Web.UI.Page
    {
        //private int _supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                Response.Redirect("../UserPage/MainPage.aspx");
            }
            if (Session["Register"] == null)
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('超时,请重新填写!');window.location='../Default.aspx';", true);
            }
            else
            {
                BindInfo();
            }
            //if(null != Request["sid"] && !string.IsNullOrEmpty(Request["sid"]))
            //{
            //    _supplierId = int.Parse(Request["sid"]);
            //}
            
        }

        private void BindInfo()
        {
            SC_Supplier model = Session["Register"] as SC_Supplier;
            if(null != model)
            {
                if (!string.IsNullOrEmpty(model.supplier_name))
                    txtSupplierName.Text = model.supplier_name;
                if (!string.IsNullOrEmpty(model.supplier_nameEN))
                    txtSupplierNameEn.Text = model.supplier_nameEN;
                if (!string.IsNullOrEmpty(model.contact_Tel))
                    txtTel.Text = model.contact_Tel;
                if (!string.IsNullOrEmpty(model.contact_Email))
                    txtEmail.Text = model.contact_Email;
                if (!string.IsNullOrEmpty(model.contact_address))
                    txtAddress.Text = model.contact_address;
                if (!string.IsNullOrEmpty(model.supplier_website))
                    txtWebsite.Text = model.supplier_website;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //SC_Supplier model = (new SC_SupplierManager()).GetModel(_supplierId);

            SC_Supplier model=null;
            if (Session["Register"] != null)
            {
                model = Session["Register"] as SC_Supplier;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('超时,请重新填写!');window.location='../Default.aspx';", true);
            }
            if (null != model)
            {
                model.supplier_name = txtSupplierName.Text.Trim();
                model.supplier_nameEN = txtSupplierNameEn.Text.Trim();
                model.contact_Tel = txtTel.Text.Trim();
                model.contact_Email = txtEmail.Text.Trim();
                model.contact_address = txtAddress.Text.Trim();
                model.supplier_website = txtWebsite.Text.Trim();
                Session["Register"] = model;
                //model.Status = State.SupplierStatus_save;
                //model.CreatTime = DateTime.Now;
                //model.LastUpdateTime = DateTime.Now;
                //SC_SupplierManager scsm = new SC_SupplierManager();
                //int result = scsm.Add(model);
                //if (result > 0)
                //{
                //    //ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册成功,请等待激活信!');window.location='../Default.aspx';", true);

                //}
                //(new SC_SupplierManager()).Update(model);
                Response.Redirect("UserRegisterCompanyStep2.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof (string), "",
                                                   "alert('人员注册失败!');window.location='../Default.aspx';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //返回首页
            Response.Redirect("UserRegist.aspx");
        }
    }
}
