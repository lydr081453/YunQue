using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MediaWeb.Media
{
    public partial class AgencyAddAndEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.MediaitemsManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CountryManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CityManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.ProvinceManager));
            #endregion
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["Operate"]))
                {
                    if (Request["Opearte"] == "Del")
                    {
                        DelData();
                    }
                }
                else
                {
                    InitPage();
                }
            }

        }

        protected void InitPage()
        {
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                ESP.Media.Entity.AgencyInfo model = new ESP.Media.BusinessLogic.AgencyManager().GetModel(Convert.ToInt32(Request["aid"]));
                if (model != null)
                {
                    txtAgencyName.Text = model.AgencyCName;
                    txtAgencyEngName.Text = model.AgencyEName;
                    txtResponsiblePerson.Text = model.ResponsiblePerson;
                    txtContacter.Text = model.Contacter;
                    txtTelephoneExchange.Text = model.TelephoneExchange;
                    txtFax.Text = model.Fax;
                    txtPhoneOne.Text = model.PhoneOne;
                    txtPhoneTwo.Text = model.PhoneTwo;
                    txtAddress1.Text = model.AddressOne;
                    txtPostCode.Text = model.PostCode;
                    txtAgencyIntro.Text = model.AgencyIntro;
                    txtEngIntro.Text = model.EngIntro;
                    txtRemarks.Text = model.Remarks;

                    //地域属性
                    getRegionAttribute();
                    ddlRegionAttribute.SelectedValue = model.RegionAttribute.ToString();
                    //省市下拉框
                    getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
                    ddlCountry.SelectedValue = model.countryid.ToString();
                    hidCountry.Value = model.countryid.ToString();

                    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
                    ddlProvince.SelectedValue = model.provinceid.ToString();
                    hidPro.Value = model.provinceid.ToString();

                    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
                    ddlCity.SelectedValue = model.cityid.ToString();
                    hidCity.Value = model.cityid.ToString();


                }
            }
            else
            {
                getRegionAttribute();
                getCountry(int.Parse(ddlRegionAttribute.SelectedValue));                
                getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlProvince);                
                getCity(int.Parse(ddlProvince.SelectedValue), ddlCity);
                
            }
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <param name="regionAttributeID">The region attribute ID.</param>
        void getCountry(int regionAttributeID)
        {
            ddlCountry.Items.Clear();
            DataTable dtcountry = ESP.Media.BusinessLogic.CountryManager.getListByRegionAttributeID(regionAttributeID);
            if (dtcountry != null && dtcountry.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtcountry;
                ddlCountry.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Country;
                ddlCountry.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Country;
                ddlCountry.DataBind();
            }
        }

        /// <summary>
        /// Gets the province.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <param name="ddlprov">The ddlprov.</param>
        void getProvince(int countryid, DropDownList ddlprov)
        {
            ddlprov.Items.Clear();
            DataTable dtprovince = ESP.Media.BusinessLogic.ProvinceManager.getAllListByCountry(countryid);
            if (dtprovince != null && dtprovince.Rows.Count > 0)
            {
                ddlprov.DataSource = dtprovince;
                ddlprov.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Province;
                ddlprov.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Province;
                ddlprov.DataBind();

            }
            ddlprov.Items.Insert(0, new ListItem("请选择", "0"));
            ddlprov.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <param name="provinceid">The provinceid.</param>
        /// <param name="ddlcity">The ddlcity.</param>
        void getCity(int provinceid, DropDownList ddlcity)
        {
            ddlcity.Items.Clear();
            DataTable dtcity = ESP.Media.BusinessLogic.CityManager.getAllListByProvince(provinceid);
            if (dtcity != null && dtcity.Rows.Count > 0)
            {
                ddlcity.DataSource = dtcity;
                ddlcity.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.City;
                ddlcity.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.City;
                ddlcity.DataBind();
            }
            ddlcity.Items.Insert(0, new ListItem("请选择", "0"));
            ddlcity.SelectedIndex = 0;
        }
        /// <summary>
        /// Gets the region attribute.
        /// </summary>
        void getRegionAttribute()
        {
            string[] regionAttributes = ESP.Media.Access.Utilities.Global.RegionAttributeName;
            for (int i = 1; i < regionAttributes.Length; i++)
            {
                ddlRegionAttribute.Items.Insert(i - 1, new ListItem(regionAttributes[i].ToString(), i.ToString()));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ESP.Media.Entity.AgencyInfo model = null;
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                model = new ESP.Media.BusinessLogic.AgencyManager().GetModel(Convert.ToInt32(Request["aid"]));
                getModel(ref model);
                model.LastModifiedByUserID = UserInfo.UserID;
                model.LastModifiedDate = DateTime.Now;
                int rel = new ESP.Media.BusinessLogic.AgencyManager().Update(model);
                if (rel > 1)
                {
 
                }
            }
            else
            {
                model = new ESP.Media.Entity.AgencyInfo();
                getModel(ref model);
                model.CreatedByUserID = model.LastModifiedByUserID = UserInfo.UserID;
                model.CreatedDate = model.LastModifiedDate = DateTime.Now;
                int rel = new ESP.Media.BusinessLogic.AgencyManager().Add(model);
                if (rel > 1)
                {

                }

            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgencyList.aspx");
        }

        private void getModel(ref ESP.Media.Entity.AgencyInfo model)
        {
            model.AgencyCName = txtAgencyName.Text.Trim();
            model.AgencyEName = txtAgencyEngName.Text.Trim();
            model.ResponsiblePerson = txtResponsiblePerson.Text.Trim();
            model.Contacter = txtContacter.Text.Trim();
            model.TelephoneExchange = txtTelephoneExchange.Text.Trim();
            model.Fax = txtFax.Text.Trim();
            model.PhoneOne = txtPhoneOne.Text.Trim();
            model.PhoneTwo = txtPhoneTwo.Text.Trim();
            model.AddressOne = txtAddress1.Text.Trim();
            model.PostCode = txtPostCode.Text.Trim();
            model.AgencyIntro = txtAgencyIntro.Text.Trim();
            model.EngIntro = txtEngIntro.Text.Trim();
            model.Remarks = txtRemarks.Text.Trim();
            //地址
            model.RegionAttribute = int.Parse(ddlRegionAttribute.SelectedValue);//地域属性 
            if (model.RegionAttribute == ESP.Media.Access.Utilities.Global.RegionAttribute_Centrality || model.RegionAttribute == ESP.Media.Access.Utilities.Global.RegionAttribute_District)
                model.countryid = ESP.Media.Access.Utilities.Global.DefaultCountry;
            else
            {                
                model.countryid = Convert.ToInt32(hidCountry.Value);
            }

            if (model.RegionAttribute == ESP.Media.Access.Utilities.Global.RegionAttribute_District)
            {                
                model.provinceid = Convert.ToInt32(hidPro.Value);                
                model.cityid = Convert.ToInt32(hidCity.Value);
            }
            else
            {
                model.provinceid = 0;
                model.cityid = 0;
            }
        }

        private void DelData()
        {
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                new ESP.Media.BusinessLogic.AgencyManager().Delete(int.Parse(Request["aid"].ToString()));
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AgencyList.aspx';alert('删除机构成功！');", true);
            }
        }
       
    }
}
