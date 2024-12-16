using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class AgencyDisplay : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();

        }

        private void InitPage()
        {
            if (!string.IsNullOrEmpty(Request["aid"]))
            {
                media_AgencyInfo model = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel(Convert.ToInt32(Request["aid"]));
                if (model != null)
                {
                    labAgencyName.Text = model.AgencyCName;
                    labAgencyEngName.Text = model.AgencyEName;
                    labResponsiblePerson.Text = model.ResponsiblePerson;
                    labContacter.Text = model.Contacter;
                    labTelephoneExchange.Text = model.TelephoneExchange;
                    labFax.Text = model.Fax;
                    labPhoneOne.Text = model.PhoneOne;
                    labPhoneTwo.Text = model.PhoneTwo;
                    labAddress1.Text = model.AddressOne;
                    labPostCode.Text = model.PostCode;
                    labAgencyIntro.Text = model.AgencyIntro;
                    labEngIntro.Text = model.EngIntro;
                    labRemarks.Text = model.Remarks;

                    //地域属性
                    labRegionAttribute.Text = ESP.Media.Access.Utilities.Global.RegionAttributeName[model.RegionAttribute.Value].ToString();
                    labCountry.Text = ESP.MediaLinq.BusinessLogic.CountryManager.GetModel(model.countryid.Value).CountryName;
                    labProvince.Text = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel(model.provinceid.Value).Province_Name;
                    labCity.Text = ESP.MediaLinq.BusinessLogic.CityManager.GetModel(model.cityid.Value).City_Name;

                    if (model.MediaID > 0)
                    {
                        media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(model.MediaID));
                        if(media != null)
                            lblMediaName.Text = media.MediaCName;

                    }

                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgencyList.aspx");
        }
    }
}
