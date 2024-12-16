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

namespace MediaWeb.NewMedia.BaseData.skins
{
    public partial class WebMediaDisplay : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 绑定页面信息
        /// <summary>
        /// Inits the page.
        /// </summary>
        /// <param name="media">The media.</param>
        public void InitPage(media_MediaItemsInfo media)
        {
            if (media == null)
            {
                panelDetail.Visible = false;
                Response.Write("此记录不存在！");
                return;
            }

            if (media.LastModifiedByUserID > 0)
            {
                labLastModifyUser.Text = new ESP.Compatible.Employee((int)media.LastModifiedByUserID).Name;
            }
            if (media.LastModifiedDate != null)
            {
                labLastModifyDate.Text = media.LastModifiedDate.ToString();
            }
            labWebName.Text = media.MediaCName;//媒体中文名称
            labWebEngName.Text = media.MediaEName;//媒体英文名称
            //labWebHTCName.Text = media.Cshortname;//媒体中文简称
            labWebEngHTCName.Text = media.EShortName;//媒体英文简称
            labWebAddress.Text = media.WebAddress;//媒体网址
            labMediumSort.Text = "网络媒体";//形态属性
            labWebChannelName.Text = media.ChannelName;//频道名称
            //  labChannelWebAdder.Text = media.Channelwebaddress;//频道网址

            //行业属性
            if (media.IndustryID != null)
            {
                media_IndustriesInfo iinfo = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetModel((int)media.IndustryID);
                if (iinfo != null)
                    labIndustry.Text = iinfo.IndustryName;
            }
            //地域属性
            if (media.RegionAttribute != null)
                labRegionAttribute.Text = ESP.MediaLinq.Utilities.Global.RegionAttributeName[(int)media.RegionAttribute].ToString();


            //受众描述
            labReaderSort.Text = media.ReaderSort;
            //string TopicTypeText = media.Readersort;

            //if (TopicTypeText.Equals("1"))
            //{
            //    labReaderSort.Text = "全部";
            //}
            //else if (TopicTypeText.Equals("2"))
            //{
            //    labReaderSort.Text = "大众";

            //}
            //else if (TopicTypeText.Equals("3"))
            //{
            //    labReaderSort.Text = "行业";

            //}
            //else if (TopicTypeText.Equals("4"))
            //{
            //    labReaderSort.Text = "专业";

            //}

            labGoverningBody.Text = media.GoverningBody;//所属单位
            labMajordomo.Text = media.Manager;//频道总监
            labCooperate.Text = media.Cooperate;//合作方式


            labChiefEditor.Text = media.ChiefEditor;//总编
            labAdminEditor.Text = media.AdminEditor;//执行总编

            labTelephoneExchange.Text = media.TelephoneExchange;//总机
            labFax.Text = media.Fax;//传真
            labAddress1.Text = media.AddressOne;//地址1
            //labAddress2.Text = media.Addresstwo;//地址2
            labPhoneOne.Text = media.PhoneOne;//热线1
            labPhoneTwo.Text = media.PhoneTwo;//热线2
            labAdsPhone.Text = media.AdsPhone;//广告部电话

            if (!string.IsNullOrEmpty(media.MediaLogo))
            {
                imgMediaLogo.ImageUrl = media.MediaLogo;//媒体LOGO"E:\\zhu.JPG" ;
                imgTitleFull.ImageUrl = media.MediaLogo.Replace(".jpg", "_full.jpg");
            }
            else
            {
                imgMediaLogo.ImageUrl = ESP.MediaLinq.Utilities.ConfigManager.DefauleImgPath;
                this.imgMediaLogo.ImageUrl = ESP.MediaLinq.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
            }
            imgMediaLogo.Width = 50;
            imgMediaLogo.Height = 50;


            //string adsprice = ESP.Media.BusinessLogic.MediaattachmentsManager.GetModel(media.Adsprice).Attachmentpath;//广告报价
            //if (adsprice != null && adsprice.Length > 0)
            //{
            //    hlAdsPrice.NavigateUrl = adsprice;
            //    hlAdsPrice.Target = "_blank";
            //}
            if (media.Briefing != null)
            {
                media_MediaAttachmentsInfo mainfo = ESP.MediaLinq.BusinessLogic.MediaAttachmentsManager.GetModel((int)media.Briefing);
                if (mainfo != null)
                {
                    string briefing = mainfo.AttachmentPath;//剪报
                    if (briefing != null && briefing.Length > 0)
                    {
                        hlBriefing.NavigateUrl = briefing;
                        hlBriefing.Target = "_blank";
                    }
                }
            }


            labMediaIntro.Text = media.MediaIntro;//媒体简介
            labEngIntro.Text = media.EngIntro;//英文简介
            labRemarks.Text = media.Remarks;//备注

            try
            {
                labCountry.Text = ESP.MediaLinq.BusinessLogic.CountryManager.GetModel((int)media.countryid).CountryName;
            }
            catch { }
            try
            {
                labProvince.Text = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel((int)media.provinceid).Province_Name;
                labCity.Text = ESP.MediaLinq.BusinessLogic.CityManager.GetModel((int)media.cityid).City_Name;
            }
            catch { }
            try
            {
                labProvinceAddr1.Text = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel((int)media.addr1_provinceid).Province_Name;
            }
            catch { }
            try
            {
                labCityAddr1.Text = ESP.MediaLinq.BusinessLogic.CityManager.GetModel((int)media.addr1_cityid).City_Name;
            }
            catch { }

            //labProvinceAddr2.Text = ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Addr2_provinceid).Province_name;
            //labCityAddr2.Text = ESP.Media.BusinessLogic.CityManager.GetModel(media.Addr2_cityid).City_name;
            labPostcode.Text = media.PostCode;
            //labIssueRegion.Text = media.Issueregion;
            labRegionAttribute.Text = ESP.MediaLinq.Utilities.Global.RegionAttributeName[(int)media.RegionAttribute].ToString();

            setBranch(media.MediaitemID);
        }

        private void setBranch(int mediaID)
        {
            DataTable dt = ESP.MediaLinq.BusinessLogic.BranchManager.GetListByMediaItemID(mediaID);
            string strBranch = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    strBranch += dr["cityname"].ToString() + "<br />";
                }
            }
            this.ltrBranch.Text = strBranch;
        }
        #endregion
    }
}