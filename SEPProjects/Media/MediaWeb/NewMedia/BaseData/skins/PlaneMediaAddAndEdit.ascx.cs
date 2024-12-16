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
    public partial class PlaneMediaAddAndEdit : System.Web.UI.UserControl
    {
        media_MediaItemsInfo media = null;
        media_mediaIndustryRelationInfo[] industries;

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

            txtMediumSort.SelectedIndex = 1;

            getIndustry();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["Mid"]))
                {
                    setValues();
                }
                else
                {
                    getRegionAttribute();
                    getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
                    //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvince);
                    //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvinceAddr1);
                    getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvince);
                    getProvince(ESP.MediaLinq.Utilities.Global.DefaultCountry, ddlProvinceAddr1);
                    getCity(int.Parse(ddlProvince.SelectedValue), ddlCity);
                    getCity(int.Parse(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
                    setCity();
                }
                if (!string.IsNullOrEmpty(Request["Source"]) && Request["Source"] == "Audit")
                {
                    txtPlaneName.Enabled = false;
                    //txtPlaneEngName.Enabled = false;
                    //txtPlaneHTCName.Enabled = false;
                    //txtPlaneEngHTCName.Enabled = false;
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
                media.CreatedDate = DateTime.Now;
            }
          //  media.CreatedDate = media.CreatedDate == null ? DateTime.Now : media.CreatedDate;
            media.CreatedByUserID = media.CreatedByUserID == 0 ? 1 : media.CreatedByUserID;
            media.LastModifiedDate = DateTime.Now;
            media.LastModifiedByUserID = 1;

            media.MediaCName = this.txtPlaneName.Text.Trim();//媒体中文名称
            media.MediaEName = txtPlaneEngName.Text.Trim();//媒体英文名称
            //media.Cshortname = txtPlaneHTCName.Text.Trim();//媒体中文简称
            media.EShortName = txtPlaneEngHTCName.Text.Trim();//媒体英文简称
            media.ISSN = txtISSN.Text.Trim();//刊号
            media.MediumSort = txtMediumSort.Text.Trim();//形态属性


            media.MediaItemType = short.Parse(txtMediumSort.SelectedIndex.ToString());

            //行业属性
            media.IndustryID = int.Parse(ddlIndustry.SelectedValue);

            media.GoverningBody = txtDirectorUnit.Text.Trim();//所属单位
            media.FrontFor = txtDirectorUnit.Text.Trim();//主办单位
            media.Proprieter = txtProprieter.Text.Trim();//社长
            media.ChiefEditor = txtChiefEditor.Text.Trim();//总编
            media.SubProprieter = txtSubProprieter.Text.Trim();//副社长
            media.Subeditor = txtSubeditor.Text.Trim();//副总编
            //发行量
            string txtCirculationText = txtCirculation.Text.Trim();
            if (!txtCirculationText.Equals(""))
            {
                media.Circulation = Convert.ToInt32(txtCirculationText);
            }
            media.PublishChannels = txtPublishChannels.Text.Trim();//发行渠道
            media.PublishPeriods = txtPublishPeriods.Text.Trim();//发行周期
            media.TelephoneExchange = txtTelephoneExchange.Text.Trim();//总机
            media.Fax = txtFax.Text.Trim();//传真
            media.AddressOne = txtAddress1.Text.Trim();//地址1
            //media.Addresstwo = txtAddress2.Text.Trim();//地址2
            media.PhoneOne = txtPhoneOne.Text.Trim();//热线1
            media.PhoneTwo = txtPhoneTwo.Text.Trim();//热线2
            try
            {
                media.StartPublication = DateTime.Parse(dpStartPublication.Text.Trim());//创刊日期
            }
            catch { }
            media.PublishDate = dpPublishDate.Text.Trim();//发行日期
            media.EndPublication = dpEndPublication.Text.Trim();//截稿日期
            media.AdsPhone = txtAdsPhone.Text.Trim();//广告部电话

            //广告报价
            //媒体LOGO
            //剪报


            //地址
            media.RegionAttribute = int.Parse(ddlRegionAttribute.SelectedValue);//地域属性 
            //media.Countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            //media.Provinceid = Convert.ToInt32(ddlProvince.SelectedValue);
            //media.Cityid = Convert.ToInt32(ddlCity.SelectedValue);
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
            //media.Addr1_provinceid = Convert.ToInt32(ddlProvinceAddr1.SelectedValue);
            //media.Addr1_cityid = Convert.ToInt32(ddlCityAddr1.SelectedValue);
            media.addr1_countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            media.addr1_provinceid = Convert.ToInt32(ddlProvinceAddr1.SelectedValue);
            if (!string.IsNullOrEmpty(hidCityAddr1.Value))
                media.addr1_cityid = Convert.ToInt32(hidCityAddr1.Value);
            else
                media.addr1_cityid = 0;

            //media.Addr2_countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            media.addr2_countryid = ESP.MediaLinq.Utilities.Global.DefaultCountry;
            //media.Addr2_provinceid = Convert.ToInt32(ddlProvinceAddr2.SelectedValue);
            //media.Addr2_cityid = Convert.ToInt32(ddlCityAddr2.SelectedValue);

            //邮政编码
            media.PostCode = txtPostCode.Text.Trim();
            //media.Issueregion = txtIssueRegion.Text.Trim();

            #region MultiLine
            if (txtCooperate.Text.Trim().Length > 500)
                media.Cooperate = txtCooperate.Text.Trim().Substring(0, 500);//栏目合作方式
            else
                media.Cooperate = txtCooperate.Text.Trim();

            if (txtRemarks.Text.Trim().Length > 2000)
                media.Remarks = txtRemarks.Text.Trim().Substring(0, 2000);//备注
            else
                media.Remarks = txtRemarks.Text.Trim();

            if (txtMediaIntro.Text.Trim().Length > 2000)
                media.MediaIntro = txtMediaIntro.Text.Trim().Substring(0, 2000);//栏目简介
            else
                media.MediaIntro = txtMediaIntro.Text.Trim();

            if (txtReaderSort.Text.Trim().Length > 500)
                media.ReaderSort = txtReaderSort.Text.Trim().Substring(0, 500);//受众描述(1全部2大众3行业4专业)
            else
                media.ReaderSort = txtReaderSort.Text.Trim();

            if (txtEngIntro.Text.Trim().Length > 500)
                media.EngIntro = txtEngIntro.Text.Trim().Substring(0, 500);//英文简介
            else
                media.EngIntro = txtEngIntro.Text.Trim();


            //media.Mediaintro = txtMediaIntro.Text.Trim();//媒体简介
            //media.Engintro = txtEngIntro.Text.Trim();//英文简介
            //media.Remarks = txtRemarks.Text.Trim();//备注
            //media.Readersort = txtReaderSort.Text.Trim();//受众描述
            //media.Cooperate = txtCooperate.Text.Trim();//合作方式

            #endregion

            return media;
        }
        #endregion

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
            // getCoverRage();

            //省市下拉框
            //getCountry();
            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
            //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
            //getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);

            //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
            //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);

            //getProvince(ESP.Media.Access.Utilities.Global.ChinaID, ddlCoverProvince);
            //getCity(Convert.ToInt32(ddlCoverProvince.SelectedValue), ddlCoverCity);

            if (media != null)
            {
                industries = ESP.MediaLinq.BusinessLogic.MediaIndustryRelationManager.GetAllIndustryByMedia(media.MediaitemID);
            }

            setCity();
            setBranch();

            txtPlaneName.Text = media.MediaCName;//媒体中文名称
            txtPlaneEngName.Text = media.MediaEName;//媒体英文名称
            //txtPlaneHTCName.Text = media.Cshortname;//媒体中文简称
            txtPlaneEngHTCName.Text = media.EShortName;//媒体英文简称
            txtISSN.Text = media.ISSN;//刊号
            txtCooperate.Text = media.Cooperate;//合作方式
            txtMediumSort.SelectedIndex = 1;//形态属性

            //受众描述
            //string TopicTypeText = media.Readersort;

            //if (TopicTypeText.Equals("0"))
            //{
            //    ddlReaderSort.SelectedIndex = 0;//全部
            //}
            //else if (TopicTypeText.Equals("1"))
            //{
            //    ddlReaderSort.SelectedIndex = 1;//大众
            //}
            //else if (TopicTypeText.Equals("2"))
            //{
            //    ddlReaderSort.SelectedIndex = 3;//行业
            //}
            //else if (TopicTypeText.Equals("3"))
            //{
            //    ddlReaderSort.SelectedIndex = 3;//专业
            //}
            txtReaderSort.Text = media.ReaderSort;

            //行业属性
            if(media.IndustryID != null)
            ddlIndustry.SelectedValue = media.IndustryID.ToString();


            txtGoverningBody.Text = media.GoverningBody;//所属单位
            txtDirectorUnit.Text = media.FrontFor;//主办单位
            txtProprieter.Text = media.Proprieter;//社长
            txtChiefEditor.Text = media.ChiefEditor;//总编
            txtSubProprieter.Text = media.SubProprieter;//副社长
            txtSubeditor.Text = media.Subeditor;//副总编
            //发行量
            txtCirculation.Text = media.Circulation + "";

            txtPublishPeriods.Text = media.PublishPeriods;//发行渠道
            txtPublishChannels.Text = media.PublishChannels;//发行渠道
            txtTelephoneExchange.Text = media.TelephoneExchange;//总机
            txtFax.Text = media.Fax;//传真
            txtAddress1.Text = media.AddressOne;//地址1
            //txtAddress2.Text=media.Addresstwo;//地址2
            txtPhoneOne.Text = media.PhoneOne;//热线1
            txtPhoneTwo.Text = media.PhoneTwo;//热线2
            if (media.StartPublication != null)
            {
                string StartpublicationTime = media.StartPublication.ToString().Trim().Split(' ')[0];
                if (StartpublicationTime.Equals("1900-1-1"))
                {
                    StartpublicationTime = "";
                }

                dpStartPublication.Text = StartpublicationTime;//创刊日期
            }
            if (media.PublishDate != null)
            {
                string PublishdateTime = media.PublishDate.ToString().Trim().Split(' ')[0];
                if (PublishdateTime.Equals("1900-1-1"))
                {
                    PublishdateTime = "";
                }
                dpPublishDate.Text = PublishdateTime;//发行日期
            }

            if (media.EndPublication != null)
            {
                string EndpublicationTime = media.EndPublication.ToString().Trim().Split(' ')[0];
                if ((EndpublicationTime.Equals("1900-1-1")))
                {
                    EndpublicationTime = "";
                }
                dpEndPublication.Text = EndpublicationTime;//截稿日期
            }
            txtAdsPhone.Text = media.AdsPhone;//广告部电话



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

            txtMediaIntro.Text = media.MediaIntro;//媒体简介
            txtEngIntro.Text = media.EngIntro;//英文简介
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
            txtPostCode.Text = media.PostCode;
            //txtIssueRegion.Text = media.Issueregion;
            //getProvince(ESP.Media.Access.Utilities.Global.ChinaID, ddlCoverProvince);
            //getCity(Convert.ToInt32(ddlCoverProvince.SelectedValue), ddlCoverCity);
        }

        /// <summary>
        /// Gets the industry.
        /// </summary>
        private void getIndustry()
        {
            DataTable dtindustry = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetDataTable();
            ddlIndustry.DataSource = dtindustry;
            ddlIndustry.DataTextField = "IndustryName";
            ddlIndustry.DataValueField = "IndustryID";
            ddlIndustry.DataBind();
            ddlIndustry.Items.Insert(0, new ListItem("请选择", "0"));
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

        /// <summary>
        /// Gets or sets the industries.
        /// </summary>
        /// <value>The industries.</value>
        public ESP.MediaLinq.Entity.media_mediaIndustryRelationInfo[] Industries
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