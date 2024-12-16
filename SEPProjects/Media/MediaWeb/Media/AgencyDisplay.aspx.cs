using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaWeb.Media
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
                ESP.Media.Entity.AgencyInfo model = new ESP.Media.BusinessLogic.AgencyManager().GetModel(Convert.ToInt32(Request["aid"]));
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
                    labRegionAttribute.Text = ESP.Media.Access.Utilities.Global.RegionAttributeName[model.RegionAttribute].ToString();
                    labCountry.Text = ESP.Media.BusinessLogic.CountryManager.GetModel(model.countryid).Countryname;
                    labProvince.Text = ESP.Media.BusinessLogic.ProvinceManager.GetModel(model.provinceid).Province_name;
                    labCity.Text = ESP.Media.BusinessLogic.CityManager.GetModel(model.cityid).City_name;


                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
        }
    }
}
