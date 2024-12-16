using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class CityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CountryBind();
                CityBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CityBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityEdit?cid=0");
        }

        private void CountryBind()
        {
            this.ddlCountry.DataSource = ESP.MediaLinq.BusinessLogic.CountryManager.GetList();
            ddlCountry.DataBind();
        }

        protected void ddlCountry_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int countryId = int.Parse(ddlCountry.SelectedValue);
            IList<media_ProvinceInfo> list = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetList(countryId);
            ddlProvince.DataSource = list;
            ddlProvince.DataBind();
            ddlProvince.Text = "";
            ddlCity.Items.Clear();
            ddlCity.Text = "";
        }

        private void CityBind()
        {
            if (ddlProvince.SelectedValue != null)
            {
                int provinceId = 0; 
                int.TryParse(ddlProvince.SelectedValue, out provinceId);
                if (provinceId > 0)
                {
                    ddlCity.DataSource = ESP.MediaLinq.BusinessLogic.CityManager.GetListByProvinceID(provinceId);
                    grdCity.DataBind();
                }
            }
        }

        protected void ddlProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CityBind();
        }
    }
}