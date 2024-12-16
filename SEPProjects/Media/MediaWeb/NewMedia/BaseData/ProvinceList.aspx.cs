using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ProvinceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CountryBind();
                ProvinceBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ProvinceBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProvinceEdit.aspx?pid=0");
        }

        protected void grdProvince_ItemCommond(object sender, ComponentArt.Web.UI.GridItemCommandEventArgs e)
        {
            if (((ImageButton)e.Control).CommandName == "deleteprovince")
            {
                int id= Convert.ToInt32(((ImageButton)e.Control).CommandArgument);
                if (id > 0)
                {
                    ESP.MediaLinq.BusinessLogic.ProvinceManager.Delete(id);
                }
            }
        }

        private void CountryBind()
        {
            ComponentArt.Web.UI.ComboBoxItem item = new ComponentArt.Web.UI.ComboBoxItem("--- 请选择 ---");
            ddlCountry.Items.Add(item);
            IList<media_CountryInfo> list = ESP.MediaLinq.BusinessLogic.CountryManager.GetList();
            this.ddlCountry.DataSource = list;
            ddlCountry.DataBind();
        }

        private void ProvinceBind()
        {
            IList<media_ProvinceInfo> list = null;
            if (!string.IsNullOrEmpty(this.ddlCountry.SelectedItem.Value))
            {
                list = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetListByCountryID(Convert.ToInt32(this.ddlCountry.SelectedItem.Value));
            }
            else
            {
                list = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetList();
            }
            this.grdProvince.DataSource = list;
            grdProvince.DataBind();
        }
    }
}