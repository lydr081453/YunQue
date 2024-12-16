using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserRegister
{
    public partial class UserRegisterCompanyStep2 : System.Web.UI.Page
    {
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
                if(!IsPostBack)
                    BindInfo();
            }
        }

        private void BindInfo()
        {
            SC_Supplier model = Session["Register"] as SC_Supplier;
            if (null != model)
            {
                if (!string.IsNullOrEmpty(model.supplier_builttime))
                    txtBuilttime.Text = model.supplier_builttime.ToString();
                if (null != model.filialeamount && model.filialeamount > 0)
                    txtFilialeAmount.Text = model.filialeamount.ToString();
                if (!string.IsNullOrEmpty(model.filialeaddress))
                    txtFilialeAdd.Text = model.filialeaddress;
                if (!string.IsNullOrEmpty(model.service_area))
                    txtService_area.Text = model.service_area;

                if (null != model.supplier_area && model.supplier_area > 0)
                {
                    hidAreaIdP.Value = model.supplier_area.ToString();
                    txtAreaName.Text = (new SC_AreaDataProvider()).GetModel(model.supplier_area).AreaDes;
                }
                if (null != model.supplier_industry && model.supplier_industry > 0)
                {
                    hidIndustriesIdP.Value = model.supplier_industry.ToString();
                    txtIndustriesName.Text =
                        (new SC_IndustriesDataProvider()).GetModel(model.supplier_industry).IndustryName;
                }
                if (null != model.supplier_principal && model.supplier_principal > 0)
                {
                    hidPrincipalIdP.Value = model.supplier_principal.ToString();
                  //  txtPrincipal.Text = (new SC_PrincipalDataProvider()).GetModel(model.supplier_principal).PrincipalDes;
                }
                if (null != model.supplier_property && model.supplier_property > 0)
                {
                    hidPropertyIdP.Value = model.supplier_property.ToString();
                    txtProperty.Text = (new SC_PropertyDataProvider()).GetModel(model.supplier_property).PropertyDes;
                }
                if (null != model.supplier_scale && model.supplier_scale > 0)
                {
                    hidScaleIdP.Value = model.supplier_scale.ToString();
                   // txtScale.Text = (new SC_ScaleDataProvider()).GetModel(model.supplier_scale).ScaleDes;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
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
                if (!string.IsNullOrEmpty(hidAreaIdP.Value))
                    model.supplier_area = int.Parse(hidAreaIdP.Value);
                if (!string.IsNullOrEmpty(hidIndustriesIdP.Value))
                    model.supplier_industry = int.Parse(hidIndustriesIdP.Value);
                if (!string.IsNullOrEmpty(hidPrincipalIdP.Value))
                    model.supplier_principal = int.Parse(hidPrincipalIdP.Value);
                if (!string.IsNullOrEmpty(hidPropertyIdP.Value))
                    model.supplier_property = int.Parse(hidPropertyIdP.Value);
                if (!string.IsNullOrEmpty(hidScaleIdP.Value))
                    model.supplier_scale = int.Parse(hidScaleIdP.Value);

                model.supplier_builttime = txtBuilttime.Text.Trim();
                if (!string.IsNullOrEmpty(txtFilialeAmount.Text.Trim()))
                    model.filialeamount = int.Parse(txtFilialeAmount.Text.Trim());
                model.filialeaddress = txtFilialeAdd.Text.Trim();
                model.service_area = txtService_area.Text.Trim();

                Session["Register"] = model;
                Response.Redirect("UserRegisterCompanyStep3.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('人员注册失败!');window.location='../Default.aspx';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //返回首页
            Response.Redirect("UserRegistCompanyStep1.aspx");
        }
    }
}
