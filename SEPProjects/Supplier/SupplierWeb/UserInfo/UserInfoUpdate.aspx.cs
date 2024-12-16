using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfoUpdate : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ListBind();

                SC_SupplierManager ssm = new SC_SupplierManager();
                SC_Supplier model = ssm.GetModel(_supplierId);

                if (null != model)
                {
                    BindInfo(model);
                }
            }

        }

        private void BindInfo(SC_Supplier model)
        {
            if (null != model)
            {
                labLogName.Text = model.LogName;
                txtSupplierName.Text = model.supplier_name;

                ddlSupplierScale.SelectedValue = model.supplier_scale.ToString();
                ddlSupplierArea.SelectedValue = model.supplier_area.ToString();
                ddlSupplierIndustry.SelectedValue = model.supplier_industry.ToString();
                ddlSupplierPrincipal.SelectedValue = model.supplier_principal.ToString();

                txtSupplierBuilttime.Text = model.supplier_builttime;
                txtSupplierWebsite.Text = model.supplier_website;
                txtSupplierTel.Text = model.contact_Tel;
                txtSupplierFax.Text = model.contact_fax;
                txtSupplierMobile.Text = model.contact_Mobile;
                txtSupplierEmail.Text = model.contact_Email;
                txtSupplierAdress.Text = model.contact_address;
                txtSupplierZIP.Text = model.contact_ZIP;
            }
        }

        private void ListBind()
        {
            #region 绑定地区

            List<SC_Area> listarea = (new SC_AreaManager()).GetAllLists();
            ddlSupplierArea.DataTextField = "AreaDes";
            ddlSupplierArea.DataValueField = "AreaId";
            ddlSupplierArea.DataSource = listarea;
            ddlSupplierArea.DataBind();
            ddlSupplierArea.Items.Insert(0, new ListItem("请选择","0"));

            #endregion 绑定地区

            #region 绑定行业
            List<SC_Industries> listindustry = (new SC_IndustriesManager()).GetAllLists();
            ddlSupplierIndustry.DataTextField = "IndustryName";
            ddlSupplierIndustry.DataValueField = "IndustryID";
            ddlSupplierIndustry.DataSource = listindustry;
            ddlSupplierIndustry.DataBind();
            ddlSupplierIndustry.Items.Insert(0, new ListItem("请选择", "0"));
            #endregion 绑定行业

            #region 绑定规模
            List<SC_Scale> listscale = (new SC_ScaleManager()).GetAllLists();
            ddlSupplierScale.DataTextField = "ScaleDes";
            ddlSupplierScale.DataValueField = "ScaleId";
            ddlSupplierScale.DataSource = listscale;
            ddlSupplierScale.DataBind();
            ddlSupplierScale.Items.Insert(0, new ListItem("请选择", "0"));
            #endregion 绑定规模

            #region 绑定注册金额
            List<SC_Principal> listprincipal = (new SC_PrincipalManager()).GetAllLists();
            ddlSupplierPrincipal.DataTextField = "PrincipalDes";
            ddlSupplierPrincipal.DataValueField = "PrincipalId";
            ddlSupplierPrincipal.DataSource = listprincipal;
            ddlSupplierPrincipal.DataBind();
            ddlSupplierPrincipal.Items.Insert(0, new ListItem("请选择", "0"));
            #endregion 绑定注册金额
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SC_SupplierManager ssm = new SC_SupplierManager();
            SC_SupplierHistoryManager sshm = new SC_SupplierHistoryManager();
            SC_Supplier model = ssm.GetModel(_supplierId);

            if(null != model)
            {
                SC_SupplierHistory shmodel = sshm.SetModel(model);

                model.supplier_name = txtSupplierName.Text.Trim();
                model.supplier_area = int.Parse(ddlSupplierArea.SelectedValue);
                model.supplier_industry = int.Parse(ddlSupplierIndustry.SelectedValue);
                model.supplier_scale = int.Parse(ddlSupplierScale.SelectedValue);
                model.supplier_principal = int.Parse(ddlSupplierPrincipal.SelectedValue);
                model.supplier_builttime = txtSupplierBuilttime.Text.Trim();
                model.supplier_website = txtSupplierWebsite.Text.Trim();
                model.contact_Tel = txtSupplierTel.Text.Trim();
                model.contact_fax = txtSupplierFax.Text.Trim();
                model.contact_Mobile = txtSupplierMobile.Text.Trim();
                model.contact_Email = txtSupplierEmail.Text.Trim();
                model.contact_address = txtSupplierAdress.Text.Trim();
                model.contact_ZIP = txtSupplierZIP.Text.Trim();
                model.LastUpdateTime = DateTime.Now;

                if(sshm.Add(shmodel) > 0)
                {
                    ssm.Update(model);
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新成功!');window.location='UserInfo.aspx';", true);
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新失败!');window.location='UserInfo.aspx';", true);

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }
    }
}
