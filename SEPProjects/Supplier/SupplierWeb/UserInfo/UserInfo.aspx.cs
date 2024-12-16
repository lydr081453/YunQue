using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.ConfigCommon;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfo : System.Web.UI.Page
    {
        private int _supplierId = 0;
        private IList<SC_SupplierType> listSupplierType = null;

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

            SC_SupplierManager ssm = new SC_SupplierManager();
            SC_Supplier model = ssm.GetModel(_supplierId);

            if (null != model)
            {
                listSupplierType = SC_SupplierTypeDataProvider.GetListBySupplierId(_supplierId);
                BindInfo(model);
            }

            BindLinkMan();
        }

        private void BindInfo(SC_Supplier model)
        {
            if (null != model)
            {
                labLogName.Text = model.LogName;
                labSupplierName.Text = model.supplier_name;

                SC_AreaManager sam = new SC_AreaManager();
                SC_IndustriesManager sim = new SC_IndustriesManager();
                SC_ScaleManager ssm = new SC_ScaleManager();
                SC_PrincipalManager spm = new SC_PrincipalManager();
                SC_Area area = sam.GetModel(model.supplier_area);
                if (null != area)
                    labSupplierArea.Text = area.AreaDes;
                SC_Industries industry = sim.GetModel(model.supplier_industry);
                if (null != industry)
                    labSupplierIndustry.Text = industry.IndustryName;
                SC_Scale scale=null; // = ssm.GetModel(model.supplier_scale);
                if (null != scale)
                    labSupplierScale.Text = scale.ScaleDes;
                SC_Principal principal=null;//= spm.GetModel(model.supplier_principal);
                if (null != principal)
                    labSupplierPrincipal.Text = principal.PrincipalDes;

                labSupplierBuilttime.Text = model.supplier_builttime;
                labSupplierWebsite.Text = model.supplier_website;
                labSupplierTel.Text = model.contact_Tel;
                labSupplierFax.Text = model.contact_fax;
                labSupplierMobile.Text = model.contact_Mobile;
                labSupplierEmail.Text = model.contact_Email;
                labSupplierAdress.Text = model.contact_address;
                labSupplierZIP.Text = model.contact_ZIP;
                labSupplierIntro.Text = model.supplier_Intro;
            }
            this.rp1.DataSource = SC_TypeDataProvider.GetLevel1List();
            this.rp1.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdate.aspx");
        }
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdatePassword.aspx");
        }

        protected void btnUpdateIntro_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdateIntro.aspx");
        }

        private void BindLinkMan()
        {
            List<SC_LinkMan> linklist = (new SC_LinkManManager()).GetListBySupplierId(_supplierId);
            if (linklist.Count == 0)
            {
                labNA.Visible = true;
            }
            else
            {
                labNA.Visible = false;
                listLinkMan.DataSource = linklist;
                listLinkMan.DataBind();
            }
        }

        protected void listLinkMan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                SC_LinkMan l = e.Item.DataItem as SC_LinkMan;

                Label labName = e.Item.FindControl("labName") as Label;
                Label labSex = e.Item.FindControl("labSex") as Label;
                Label labTitle = e.Item.FindControl("labTitle") as Label;
                Label labTel = e.Item.FindControl("labTel") as Label;
                Label labMobile = e.Item.FindControl("labMobile") as Label;
                Label labFax = e.Item.FindControl("labFax") as Label;
                Label labAddress = e.Item.FindControl("labAddress") as Label;
                Label labZIP = e.Item.FindControl("labZIP") as Label;
                Label labEmail = e.Item.FindControl("labEmail") as Label;
                Label labQQ = e.Item.FindControl("labQQ") as Label;
                Label labMSN = e.Item.FindControl("labMSN") as Label;
                Label labNote = e.Item.FindControl("labNote") as Label;

                HyperLink hypUpdateLinkMan = e.Item.FindControl("hypUpdateLinkMan") as HyperLink;

                Image imgIcon = e.Item.FindControl("imgIcon") as Image;

                if (l != null)
                {
                    labName.Text = l.Name;
                    if (l.Sex == 0)
                        labSex.Text = "男";
                    else if (l.Sex == 1)
                        labSex.Text = "女";
                    else
                        labSex.Text = "不明";
                    labTitle.Text = l.Title;
                    labTel.Text = l.Tel;
                    labMobile.Text = l.Mobile;
                    labFax.Text = l.Fax;
                    labAddress.Text = l.Address;
                    labZIP.Text = l.ZIP;
                    labEmail.Text = l.Email;
                    labQQ.Text = l.QQ;
                    labMSN.Text = l.MSN;
                    labNote.Text = l.Note;

                    hypUpdateLinkMan.NavigateUrl = string.Format("UserInfoUpdateLinkMan.aspx?lid={0}", l.LinkerId.ToString());
                }
            }
        }

        protected void rp1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Type type1 = e.Item.DataItem as SC_Type;
                if (null != type1)
                {
                    Label lblMain = (Label)e.Item.FindControl("lblMain");
                    lblMain.Text = type1.TypeName;
                    DataList ListLevel2 = (DataList)e.Item.FindControl("ListLevel2");

                    IList<SC_Type> list = SC_TypeDataProvider.GetSupplierL2TypeListBySupplierId(_supplierId, type1.Id);
                    ListLevel2.DataSource = list;
                    ListLevel2.DataBind();
                }
            }

        }

        protected void List2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Type type2 = e.Item.DataItem as SC_Type;

                Label lblName = (Label) e.Item.FindControl("lblName");
                DataList ListLevel3 = (DataList)e.Item.FindControl("ListLevel3");
                ListLevel3.DataSource = SC_TypeDataProvider.GetListByParentId(type2.Id);
                ListLevel3.DataBind();

            }
        }

        protected void btnAddLinkMan_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdateLinkMan.aspx");
        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfoUpdateType.aspx");
        }
    }
}
