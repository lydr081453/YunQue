using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.MediaLinq.Utilities;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class AgencyAddAndEdit : ESP.Web.UI.PageBase
    {
        int Mid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.MediaItemManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CountryManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CityManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.ProvinceManager));
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
        protected void btnLink_Click(object sender, EventArgs e)
        {
            //string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            //param = Global.AddParam(param, "Rid", Request["Rid"]);
            //param = Global.AddParam(param, "Mid", Mid.ToString());
            //param = Global.AddParam(param, "Operate", "MediaSelect");
            //param = Global.AddParam(param, "backurl", "ReporterAddAndEdit.aspx");
            //string url = string.Format(@"AgencySelectMediaList.aspx?{0}", param);
            //Response.Redirect(url);
        }

        protected void InitPage()
        {
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                ESP.MediaLinq.Entity.media_AgencyInfo model = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel(Convert.ToInt32(Request["aid"]));
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


                    if (model.MediaID > 0)
                    {
                        media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(model.MediaID));
                        if (media != null)
                        {
                            txtMediaName.Text = media.MediaCName;
                            this.hidMedia.Value = model.MediaID.ToString();
                        }

                    }
                }
            }
            else
            {
                getRegionAttribute();
                getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
                getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvince);
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
            DataTable dtcountry = ESP.MediaLinq.BusinessLogic.CountryManager.getListByRegionAttributeID(regionAttributeID);
            if (dtcountry != null && dtcountry.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtcountry;
                ddlCountry.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.Country;
                ddlCountry.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.Country;
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
            DataTable dtprovince = ESP.MediaLinq.BusinessLogic.ProvinceManager.getAllListByCountry(countryid);
            if (dtprovince != null && dtprovince.Rows.Count > 0)
            {
                ddlprov.DataSource = dtprovince;
                ddlprov.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.Province;
                ddlprov.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.Province;
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
            DataTable dtcity = ESP.MediaLinq.BusinessLogic.CityManager.getAllListByProvince(provinceid);
            if (dtcity != null && dtcity.Rows.Count > 0)
            {
                ddlcity.DataSource = dtcity;
                ddlcity.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.City;
                ddlcity.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.City;
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
            string[] regionAttributes = ESP.MediaLinq.Utilities.Global.RegionAttributeName;
            for (int i = 1; i < regionAttributes.Length; i++)
            {
                ddlRegionAttribute.Items.Insert(i - 1, new ListItem(regionAttributes[i].ToString(), i.ToString()));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ESP.MediaLinq.Entity.media_AgencyInfo model = null;
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                 model = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel(Convert.ToInt32(Request["aid"]));
                if (model != null)
                {

                    model = getModel(model);
                    model.LastModifiedByUserID = UserInfo.UserID;
                    model.LastModifiedDate = DateTime.Now;

                    if (ESP.MediaLinq.BusinessLogic.AgencyManager.Update(model))
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AgencyList.aspx';alert('机构修改成功！');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('机构修改失败！');", true);
                    }
                }
            }
            else
            {
                model = new ESP.MediaLinq.Entity.media_AgencyInfo();
                model = getModel(model);
                model.CreatedByUserID = UserInfo.UserID;
                model.LastModifiedByUserID = UserInfo.UserID;
                model.CreatedDate = DateTime.Now;
                model.LastModifiedDate = DateTime.Now;
                int rel = ESP.MediaLinq.BusinessLogic.AgencyManager.Add(model);
                if (rel > 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AgencyList.aspx';alert('机构添加成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('机构添加失败！');", true);
                }

            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgencyList.aspx");
        }

        private ESP.MediaLinq.Entity.media_AgencyInfo getModel(ESP.MediaLinq.Entity.media_AgencyInfo model)
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
            model.MediaID = Convert.ToInt32(this.hidMedia.Value);
            //地址
            model.RegionAttribute = int.Parse(ddlRegionAttribute.SelectedValue);//地域属性 
            if (model.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_Centrality || model.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_District)
                model.countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            else
            {
                model.countryid = Convert.ToInt32(hidCountry.Value);
            }

            if (model.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_District)
            {
                model.provinceid = Convert.ToInt32(hidPro.Value);
                model.cityid = Convert.ToInt32(hidCity.Value);
            }
            else
            {
                model.provinceid = 0;
                model.cityid = 0;
            }
            return model;
        }

        private void DelData()
        {
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                if (ESP.MediaLinq.BusinessLogic.AgencyManager.Delete(int.Parse(Request["aid"].ToString())))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AgencyList.aspx';alert('删除机构成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AgencyList.aspx';alert('删除机构失败！');", true);
                }
            }
        }
    }
}
