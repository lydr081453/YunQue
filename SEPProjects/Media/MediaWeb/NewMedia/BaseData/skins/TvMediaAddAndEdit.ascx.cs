using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.MediaLinq.Entity;
using System.Collections.Generic;

namespace MediaWeb.NewMedia.BaseData.skins
{
    public partial class TvMediaAddAndEdit : System.Web.UI.UserControl
    {
        media_MediaItemsInfo media = null;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.MediaItemManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CountryManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CityManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.ProvinceManager));
            #endregion

            // getIndustry();
            txtMediumSort.SelectedIndex = 3;//形态属性

            getLanMu();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["Mid"]))
                {
                    setValues();
                }
                else
                {
                    //getCoverRage();
                    getRegionAttribute();
                    getCountry(int.Parse(ddlRegionAttribute.SelectedValue));

                    //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvince);
                    getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvince);
                    getCity(int.Parse(ddlProvince.SelectedValue), ddlCity);

                    //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvinceAddr1);
                    getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvinceAddr1);
                    getCity(int.Parse(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);

                    //getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlCoverProvince);
                    //getCity(int.Parse(ddlCoverProvince.SelectedValue), ddlCoverCity);


                    setCity();
                }
                if (!string.IsNullOrEmpty(Request["Source"]) && Request["Source"] == "Audit")
                {
                    txtTvName.Enabled = false;
                    //txtTvEngName.Enabled = false;
                    //txtTvHTCName.Enabled = false;
                    //txtTvEngHTCName.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClearSelected control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnClearSelected_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.lbxBranchSelected.Items)
            {
                ListItem unItem = new ListItem(item.Text, item.Value);
                this.lbxBranch.Items.Add(unItem);
            }
            this.lbxBranchSelected.Items.Clear();
        }

        /// <summary>
        /// Handles the Click event of the btnSelectBranch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSelectBranch_Click(object sender, EventArgs e)
        {
            for (int i = this.lbxBranch.Items.Count - 1; i >= 0; i--)
            {
                if (this.lbxBranch.Items[i].Selected)
                {
                    this.lbxBranchSelected.Items.Add(this.lbxBranch.Items[i]);
                    this.lbxBranch.Items.Remove(this.lbxBranch.Items[i]);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnUnSelectBranch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUnSelectBranch_Click(object sender, EventArgs e)
        {

            for (int i = this.lbxBranchSelected.Items.Count - 1; i >= 0; i--)
            {
                if (this.lbxBranchSelected.Items[i].Selected)
                {
                    this.lbxBranch.Items.Add(this.lbxBranchSelected.Items[i]);
                    this.lbxBranchSelected.Items.Remove(this.lbxBranchSelected.Items[i]);
                }
            }
            if (this.lbxBranchSelected.SelectedIndex > -1)
            {
                ListItem item = this.lbxBranchSelected.SelectedItem;
                this.lbxBranch.Items.Add(item);
                this.lbxBranchSelected.Items.Remove(this.lbxBranchSelected.SelectedItem);
            }
        }

        /// <summary>
        /// Gets the branch objects.
        /// </summary>
        /// <returns></returns>
        public List<media_BranchInfo> GetBranchObjects()
        {
            List<media_BranchInfo> listBranch = new List<media_BranchInfo>();
            string strCityID = this.RoleColl.Value;
            string[] strCityIDList = strCityID.Split(',');
            for (int i = 0; i < strCityIDList.Length; i++)
            {
                media_BranchInfo branch = new media_BranchInfo();
                int mediaid = Convert.ToInt32(Request["Mid"]);
                branch.mediaitemid = mediaid;

                int cid = 0;
                bool res = int.TryParse(strCityIDList[i], out cid);
                if (res)
                {
                    media_CapitalInfo city = ESP.MediaLinq.BusinessLogic.CapitalManager.GetModel(cid);
                    branch.cityid = city.cityID;
                    branch.cityname = city.cityname;
                }
                listBranch.Add(branch);

            }
            return listBranch;
        }

        #region 获得对象
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <returns></returns>
        public media_MediaItemsInfo GetObject()
        {
            if (Request["Mid"] != null)
            {
                media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));

            }
            else
            {
                media = new media_MediaItemsInfo();
            }
            media.CreatedDate = media.CreatedDate == null ? DateTime.Now : media.CreatedDate;
            media.CreatedByUserID = media.CreatedByUserID == 0 ? 1 : media.CreatedByUserID;
            media.LastModifiedDate = DateTime.Now;
            media.LastModifiedByUserID = 1;

            //txtTvName.Text = media.Mediacname.Trim();//媒体中文名称
            //txtTvEngName.Text = media.Mediaename.Trim();//媒体英文名称
            //txtTvHTCName.Text = media.Cshortname.Trim();
            //txtTvEngHTCName.Text = media.Eshortname.Trim();

            media.MediaCName = txtTvName.Text.Trim();
            media.MediaEName = txtTvEngName.Text.Trim();
            //media.Cshortname = txtTvHTCName.Text.Trim();
            media.EShortName = txtTvEngHTCName.Text.Trim();

            media.ChannelName = txtTvChannelName.Text.Trim();//频道名称
            //media.Overriderange = ddlOverrideRange.SelectedValue;//覆盖范围
            //media.Override_provinceid = 0;
            //media.Override_cityid = 0;
            //if (ddlOverrideRange.SelectedValue == "2")
            //{
            //    media.Override_provinceid = int.Parse(hidCoverPro.Value);
            //}
            //if (ddlOverrideRange.SelectedValue == "3")
            //{
            //    media.Override_provinceid = int.Parse(hidCoverPro.Value);
            //    media.Override_cityid = int.Parse(hidCoverCity.Value);
            //}

            media.TopicName = txtTvTopicName.Text.Trim();//栏目名称

            media.Rating = txtRating.Text.Trim();//近期收视率
            media.MediumSort = txtMediumSort.Text.Trim();//形态属性
            media.MediaItemType = short.Parse(txtMediumSort.SelectedIndex.ToString());
            //行业属性
            // media.Industryid = int.Parse(ddlIndustry.SelectedValue);
            media.MediumSort = "电视媒体";

            media.MediaItemType = 3;
            media.TopicProperty = Convert.ToInt32(ddlLanMu.SelectedValue); //栏目

            media.GoverningBody = txtGoverningBody.Text.Trim();//主管单位
            media.Manager = txtManager.Text.Trim();//台长
            media.ChiefEditor = txtChiefEditor.Text.Trim();//总编（频道）

            media.Zhuren = txtZhuren.Text.Trim();//主任
            media.Producer = txtProducer.Text.Trim();//制片人
            media.TelephoneExchange = txtTelephoneExchange.Text.Trim();//总机
            media.Fax = txtFax.Text.Trim();//传真
            media.AddressOne = txtAddressOne.Text.Trim();//地址1
            //media.Addresstwo = txtAddressTwo.Text.Trim();//地址2
            media.PhoneOne = txtPhoneOne.Text.Trim();//热线1
            media.PhoneTwo = txtPhoneTwo.Text.Trim();//热线2
            media.PublishDate = dpTopicBegin.Text.Trim();//栏目播出时间
            media.TopicTime = txtTopicTime.Text.Trim();//播出时长

            media.AdsPhone = txtAdsPhone.Text.Trim();//广告部电话
            media.WebAddress = txtWebAddress.Text.Trim();//网址

            //  media.Adsprice = adsUploadPrice.FileName;//广告报价
            //  media.Medialogo = mediaUploadLogo.FileName;// 媒体LOGO
            //  media.Briefing = briefingUpload.FileName;//剪报

            //地址
            media.RegionAttribute = int.Parse(ddlRegionAttribute.SelectedValue);//地域属性 
            if (media.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_Centrality || media.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_District)
                media.countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            else
            {
                //media.Countryid = Convert.ToInt32(ddlCountry.SelectedValue);
                media.countryid = Convert.ToInt32(hidCountry.Value);
            }

            if (media.RegionAttribute == ESP.MediaLinq.Utilities.Global.RegionAttribute_District)
            {
                //media.Provinceid = Convert.ToInt32(ddlProvince.SelectedValue);
                media.provinceid = Convert.ToInt32(hidPro.Value);
                //media.Cityid = Convert.ToInt32(ddlCity.SelectedValue);
                media.cityid = Convert.ToInt32(hidCity.Value);
            }
            else
            {
                media.provinceid = 0;
                media.cityid = 0;
            }

            //media.Addr1_countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            media.addr1_countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            media.addr1_provinceid = Convert.ToInt32(ddlProvinceAddr1.SelectedValue);
            //media.Addr1_cityid = Convert.ToInt32(ddlCityAddr1.SelectedValue);
            if (!string.IsNullOrEmpty(hidCityAddr1.Value))
                media.addr1_cityid = Convert.ToInt32(hidCityAddr1.Value);
            else
                media.addr1_cityid = 0;

            //media.Addr2_countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            media.addr2_countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            //media.Addr2_provinceid = Convert.ToInt32(ddlProvinceAddr2.SelectedValue);
            //media.Addr2_cityid = Convert.ToInt32(ddlCityAddr2.SelectedValue);
            //media.Overriderange = ddlOverrideRange.SelectedValue;//覆盖范围

            //邮政编码
            media.PostCode = txtPostcode.Text.Trim();
            //media.Issueregion = txtIssueRegion.Text.Trim();

            #region MultiLine
            if (txtCooperate.Text.Trim().Length > 500)
                media.Cooperate = txtCooperate.Text.Trim().Substring(0, 500);//栏目合作方式
            else
                media.Cooperate = txtCooperate.Text.Trim();

            if (txtReaderSort.Text.Trim().Length > 500)
                media.ReaderSort = txtReaderSort.Text.Trim().Substring(0, 500);//受众描述(1全部2大众3行业4专业)
            else
                media.ReaderSort = txtReaderSort.Text.Trim();

            if (txtRemarks.Text.Trim().Length > 500)
                media.Remarks = txtRemarks.Text.Trim().Substring(0, 2000);//备注
            else
                media.Remarks = txtRemarks.Text.Trim();

            if (txtTopicIntro.Text.Trim().Length > 500)
                media.MediaIntro = txtTopicIntro.Text.Trim().Substring(0, 2000);//栏目简介
            else
                media.MediaIntro = txtTopicIntro.Text.Trim();
            #endregion
            return media;
        }
        #endregion

        /// <summary>
        /// Gets the industry.
        /// </summary>
        //private void getIndustry()
        //{
        //    DataTable dtindustry = ESP.Media.BusinessLogic.IndustriesManager.GetAllList();
        //   // ddlIndustry.DataSource = dtindustry;
        //   // ddlIndustry.DataTextField = "IndustryName";
        //   // ddlIndustry.DataValueField = "IndustryID";
        //   // ddlIndustry.DataBind();
        //   // ddlIndustry.Items.Insert(0, new ListItem("请选择", "0"));
        //}



        /// <summary>
        /// Gets the lan mu.
        /// </summary>
        void getLanMu()
        {
            ddlLanMu.Items.Clear();
            DataTable dtLanMu = ESP.MediaLinq.Utilities.Global.LanMuType.GetAllList();
            ddlLanMu.DataSource = dtLanMu.DefaultView;
            ddlLanMu.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.LanMu;
            ddlLanMu.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.LanMu;
            ddlLanMu.DataBind();
            ddlLanMu.SelectedIndex = 0;
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
        /// Handles the SelectedIndexChanged event of the ddlRegionAttribute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlRegionAttribute_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
        //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
        //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

        //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
        //    getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCoverProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlCoverProvince_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getCity(Convert.ToInt32(ddlCoverProvince.SelectedValue), ddlCoverCity);
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlOverrideRange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlOverrideRange_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //ddlCoverProvince.Visible = false;
        //labCoverProvince.Visible = false;
        //ddlCoverCity.Visible = false;
        //labCoverCity.Visible = false;

        //if (ddlOverrideRange.SelectedIndex == (int)ESP.Media.Access.Utilities.Global.CoverRageItem.NULL)
        //{

        //}
        //else if (ddlOverrideRange.SelectedIndex == (int)ESP.Media.Access.Utilities.Global.CoverRageItem.COUNTRY)//全国
        //{
        //} 
        //else if (ddlOverrideRange.SelectedIndex == (int)ESP.Media.Access.Utilities.Global.CoverRageItem.PROVINCE)//省
        //{
        //    ddlCoverProvince.Visible = true;
        //    labCoverProvince.Visible = true;
        //    ddlCoverCity.Visible = false;
        //    labCoverCity.Visible = false;
        //}
        //else if (ddlOverrideRange.SelectedIndex == (int)ESP.Media.Access.Utilities.Global.CoverRageItem.CITY)//市
        //{
        //    ddlCoverProvince.Visible = true;
        //    labCoverProvince.Visible = true;
        //    ddlCoverCity.Visible = true;
        //    labCoverCity.Visible = true;
        //}
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCountry control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
        //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

        //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
        //    getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);


        //    //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
        //    //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlProvinceAddr1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void ddlProvinceAddr1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlProvinceAddr2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlProvinceAddr2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);
        }

        #region 绑定页面信息
        /// <summary>
        /// Inits the page.
        /// </summary>
        /// <param name="oldmedia">The oldmedia.</param>
        public void InitPage(media_MediaItemsInfo oldmedia)
        {
            this.media = oldmedia;
        }
        #endregion

        /// <summary>
        /// Sets the city.
        /// </summary>
        private void setCity()
        {
            DataTable dtCities = ESP.MediaLinq.BusinessLogic.CapitalManager.GetDataTable();
            foreach (DataRow drCity in dtCities.Rows)
            {
                ListItem newitem = new ListItem(drCity["cityname"].ToString(), drCity["cityid"].ToString());
                this.lbxBranch.Items.Add(newitem);
            }
        }

        /// <summary>
        /// Sets the branch.
        /// </summary>
        private void setBranch()
        {
            DataTable dt = ESP.MediaLinq.BusinessLogic.BranchManager.GetListByMediaItemID(media.MediaitemID);
            if (dt != null && dt.Rows.Count > 0 && this.lbxBranch.Items.Count > 0)
            {
                for (int i = this.lbxBranch.Items.Count - 1; i >= 0; i--)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (this.lbxBranch.Items[i].Text == dr["cityname"].ToString() && this.lbxBranch.Items[i].Value == dr["cityid"].ToString())
                        {
                            ListItem selectItem = new ListItem(this.lbxBranch.Items[i].Text, this.lbxBranch.Items[i].Value);
                            this.lbxBranchSelected.Items.Add(selectItem);
                            this.lbxBranch.Items.Remove(this.lbxBranch.Items[i]);
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        private void setValues()
        {
            //getCoverRage();

            ////省市下拉框
            //getCountry();
            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
            //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
            //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr1);

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
            //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr2);

            //getProvince(ESP.Media.Access.Utilities.Global.ChinaID, ddlCoverProvince);
            //getCity(Convert.ToInt32(ddlCoverProvince.SelectedValue), ddlCoverCity);

            //受众描述
            txtReaderSort.Text = media.ReaderSort;

            if (media == null)
                return;
            industries = ESP.MediaLinq.BusinessLogic.MediaIndustryRelationManager.GetAllIndustryByMedia(media.MediaitemID);

            setCity();
            setBranch();
            txtTvName.Text = media.MediaCName;//媒体中文名称
            txtTvEngName.Text = media.MediaEName;//媒体英文名称
            //txtTvHTCName.Text = media.Cshortname;
            txtTvEngHTCName.Text = media.EShortName;

            txtTvChannelName.Text = media.ChannelName;//频道名称

            txtTvTopicName.Text = media.TopicName;//栏目名称
            txtCooperate.Text = media.Cooperate;//栏目合作方式
            txtRating.Text = media.Rating;//近期收视率
            txtMediumSort.SelectedIndex = 3;//形态属性

            //覆盖范围
            ////getCoverRage();
            //ddlOverrideRange.SelectedValue = media.Overriderange;

            //getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlCoverProvince);
            //ddlCoverProvince.SelectedValue = media.Override_provinceid.ToString();
            //hidCoverPro.Value = media.Override_provinceid.ToString();
            //getCity(Convert.ToInt32(ddlCoverProvince.SelectedValue), ddlCoverCity);
            //ddlCoverCity.SelectedValue = media.Override_cityid.ToString();
            //hidCoverCity.Value = media.Override_cityid.ToString();

            //行业属性
            //ddlIndustry.SelectedValue = media.Industryid.ToString();

            if(media.TopicProperty != null)
            ddlLanMu.SelectedIndex = Convert.ToInt32(media.TopicProperty);//栏目
            txtGoverningBody.Text = media.GoverningBody;//主管单位
            txtManager.Text = media.Manager;//台长
            txtChiefEditor.Text = media.ChiefEditor;//总编（频道）
            txtZhuren.Text = media.Zhuren;//主任
            txtProducer.Text = media.Producer;//制片人
            txtTelephoneExchange.Text = media.TelephoneExchange;//总机
            txtFax.Text = media.Fax;//传真
            txtAddressOne.Text = media.AddressOne;//地址1
            //txtAddressTwo.Text = media.Addresstwo;//地址2
            txtPhoneOne.Text = media.PhoneOne;//热线1
            txtPhoneTwo.Text = media.PhoneTwo;//热线2
            dpTopicBegin.Text = media.PublishDate;//栏目播出时间
            //if (dpTopicBegin.Text.Equals("1900-1-1"))
            //{
            //    dpTopicBegin.Text = "";
            //}

            txtTopicTime.Text = media.TopicTime;//播出时长

            txtAdsPhone.Text = media.AdsPhone;//广告部电话
            txtWebAddress.Text = media.WebAddress;//网址

            if (!string.IsNullOrEmpty(media.MediaLogo))
            {
                uploadimage.ImageUrl = media.MediaLogo;//媒体LOGO"E:\\zhu.JPG" ;

            }
            else
            {
                this.uploadimage.ImageUrl = ESP.MediaLinq.Utilities.ConfigManager.DefauleImgPath;
            }
            uploadimage.Width = 20;
            uploadimage.Height = 20;

            txtTopicIntro.Text = media.MediaIntro;//栏目简介
            txtRemarks.Text = media.Remarks;//备注

            //地域属性
            getRegionAttribute();
            ddlRegionAttribute.SelectedValue = media.RegionAttribute.ToString();
            //省市下拉框
            getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
            ddlCountry.SelectedValue = media.countryid.ToString();
            hidCountry.Value = media.countryid.ToString();

            getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
            ddlProvince.SelectedValue = media.provinceid.ToString();
            hidPro.Value = media.provinceid.ToString();

            getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
            ddlCity.SelectedValue = media.cityid.ToString();
            hidCity.Value = media.cityid.ToString();

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
            getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvinceAddr1);
            ddlProvinceAddr1.SelectedValue = media.addr1_provinceid.ToString();
            getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
            ddlCityAddr1.SelectedValue = media.addr1_cityid.ToString();
            hidCityAddr1.Value = media.addr1_cityid.ToString();

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
            //ddlProvinceAddr2.SelectedValue = media.Addr2_provinceid.ToString();
            //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);
            //ddlCityAddr2.SelectedValue = media.Addr2_cityid.ToString();


            //邮政编码
            txtPostcode.Text = media.PostCode;
            //txtIssueRegion.Text = media.Issueregion;
        }

        /// <summary>
        /// 
        /// </summary>
        media_mediaIndustryRelationInfo[] industries;

        /// <summary>
        /// Gets or sets the industries.
        /// </summary>
        /// <value>The industries.</value>
        public media_mediaIndustryRelationInfo[] Industries
        {
            get
            {
                return this.industries;
            }
            set
            {
                this.industries = value;
            }
        }

        ///// <summary>
        ///// Gets the adsprice.
        ///// </summary>
        ///// <value>The adsprice.</value>
        //public FileUpload Adsprice
        //{
        //    get
        //    {
        //        return this.adsUploadPrice;
        //    }

        //}

        /// <summary>
        /// Gets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public FileUpload Logo
        {
            get
            {
                return this.mediaUploadLogo;
            }

        }

        /// <summary>
        /// Gets the briefing.
        /// </summary>
        /// <value>The briefing.</value>
        public FileUpload Briefing
        {
            get
            {
                return this.briefingUpload;
            }

        }

    }
}