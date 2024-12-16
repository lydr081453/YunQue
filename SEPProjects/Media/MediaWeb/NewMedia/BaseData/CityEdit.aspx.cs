using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class CityEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountries();
                int id = 0;
                if (this.ddlCountry.Items.Count > 0 && int.TryParse(this.ddlCountry.SelectedItem.Value, out id))
                {
                    BindProvinces(Convert.ToInt32(this.ddlCountry.SelectedItem.Value));
                    BindItem();
                }
            }
        }

        private void BindCountries()
        {
            this.ddlCountry.DataSource = ESP.MediaLinq.BusinessLogic.CountryManager.GetList();
            this.ddlCountry.DataBind();
        }

        private void BindProvinces(int countryID)
        {
            this.ddlCountry.DataSource = ESP.MediaLinq.BusinessLogic.CountryManager.GetListByCountryID(countryID);
            this.ddlCountry.DataBind();
        }

        private void BindItem()
        {
            int id = 0;
            if (!string.IsNullOrEmpty(Request["cid"]) && int.TryParse(Request["cid"], out id))
            {
                media_CityInfo info = ESP.MediaLinq.BusinessLogic.CityManager.GetModel(id);
                if (info != null)
                {
                    this.txtCityName.Text = info.City_Name;
                    this.txtLevel.Text = info.City_Level;

                    media_ProvinceInfo proInfo = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel(Convert.ToInt32(info.Province_ID));
                    if (proInfo != null)
                    {
                        media_CountryInfo countryInfo = ESP.MediaLinq.BusinessLogic.CountryManager.GetModel(Convert.ToInt32(proInfo.Country_ID));
                        if (countryInfo != null)
                        {
                            this.ddlCountry.SelectedItem.Value = countryInfo.CountryID.ToString();
                            BindProvinces(countryInfo.CountryID);
                            this.ddlProvince.SelectedValue = info.Province_ID.ToString();
                        }
                    }
                }
            }
        }

        private bool Save()
        {
            media_CityInfo info = new media_CityInfo();
            info.City_Level = this.txtLevel.Text;
            info.City_Name = this.txtCityName.Text;
            info.Province_ID = Convert.ToInt32(this.ddlProvince.SelectedValue);
            try
            {
                int id = 0;
                if (!string.IsNullOrEmpty(Request["cid"]) && int.TryParse(Request["cid"], out id))
                {
                    media_CityInfo exinfo = ESP.MediaLinq.BusinessLogic.CityManager.GetModel(id);
                    if (info != null)
                    {
                        info.City_ID = exinfo.City_ID;
                        ESP.MediaLinq.BusinessLogic.CityManager.Update(info);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ESP.MediaLinq.BusinessLogic.CityManager.Add(info);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功！');", true);
                Response.Redirect("Cityist.aspx");
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败，出现错误！');", true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityList.aspx");
        }
    }
}
