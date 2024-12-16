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
using ESP.Media.Entity;

public partial class Media_skins_TvMediaContentsDisplay : System.Web.UI.UserControl
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    /// <summary>
    /// Inits the page.
    /// </summary>
    /// <param name="media">The media.</param>
    public void InitPage(MediaitemshistInfo media)
    {
        if (media == null)
        {
            panelDetail.Visible = false;
            Response.Write("此记录不存在！");
            return;
        }
        if (media.Lastmodifiedbyuserid > 0)
        {
            labLastModifyUser.Text = new ESP.Compatible.Employee(media.Lastmodifiedbyuserid).Name;
        }
        if (media.Lastmodifieddate != null && media.Lastmodifieddate.Length > 0)
        {
            labLastModifyDate.Text = media.Lastmodifieddate;
        }
        DataTable dtIndustries = ESP.Media.BusinessLogic.MediaindustryrelationManager.getAllListByMediaid(media.Mediaitemid);
        labVersion.Text = media.Version+"";
        labTvName.Text = media.Mediacname.Trim();//媒体中文名称
        labTvEngName.Text = media.Mediaename.Trim();//媒体英文名称
        //labTvHTCName.Text = media.Cshortname.Trim();
        labTvEngHTCName.Text = media.Eshortname.Trim();
        labTvChannelName.Text = media.Channelname.Trim();//频道名称
        LabMediumSort.Text = media.Mediumsort.Trim();//形态属性
        labTvTopicName.Text = media.Topicname.Trim();//栏目名称
        labCooperate.Text = media.Cooperate.Trim();//栏目合作方式
        labRating.Text = media.Rating.Trim();//近期收视率


        //覆盖范围
        //if (!string.IsNullOrEmpty(media.Overriderange))
        //{
        //    labCoverRage.Text = ESP.Media.Access.Utilities.Global.CoverRage.CoverRages[Convert.ToInt32(media.Overriderange)];
        //}

        //受众描述
        labReaderSort.Text = media.Readersort.Trim();

        //行业属性
        string industry = string.Empty;
        for (int i = 0; i < dtIndustries.Rows.Count; i++)
        {
            industry += dtIndustries.Rows[i]["industryname"] == DBNull.Value ? string.Empty : dtIndustries.Rows[i]["industryname"].ToString().Trim();
            industry += " ";
        }
        labIndustry.Text = industry;

        //栏目类型
        string TopicTypeText = ESP.Media.Access.Utilities.Global.LanMuType.TypeNames[Convert.ToInt32(media.Topicproperty)]; 
        labLanMu.Text = TopicTypeText;

        labGoverning.Text = media.Governingbody.Trim();//主管单位
        labManager.Text = media.Manager.Trim();//台长
        labChiefEditor.Text = media.Chiefeditor.Trim();//总编（频道）

        labDirector.Text = media.Zhuren.Trim();//主任
        labMoviemaking.Text = media.Producer.Trim();//制片人
        labTelExchange.Text = media.Telephoneexchange.Trim();//总机
        labFax.Text = media.Fax.Trim();//传真
        //labAddrDetail1.Text = media.Addressone.Trim();//地址1
        //labAddrDetail2.Text = media.Addresstwo.Trim();//地址2
        labHotLine1.Text = media.Phoneone.Trim();//热线1
        labHotLine2.Text = media.Phonetwo.Trim();//热线2
        labShowDate.Text = media.Publishdate.Trim().Split(' ')[0];//栏目播出时间
        if (labShowDate.Text.Equals("1900-1-1"))
        {
            labShowDate.Text = "";
        }
        labTimeOut.Text = media.Topictime ;//播出时长

        labAdsPhone.Text = media.Adsphone.Trim();//广告部电话
        labWebAddress.Text = media.Webaddress.Trim();//网址

        //hlAdsPrice.NavigateUrl = ESP.Media.BusinessLogic.MediaattachmentsManager.GetModel(media.Adsprice).Attachmentpath;
        if (!media.Medialogo.Equals(""))
        {
            imgMediaLogo.ImageUrl = media.Medialogo;//媒体LOGO"E:\\zhu.JPG" ;
            imgTitleFull.ImageUrl = media.Medialogo.Replace(".jpg", "_full.jpg");
        }
        else
        {
            imgMediaLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
            this.imgMediaLogo.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath.Replace(".jpg", "_full.jpg");
        }
        imgMediaLogo.Width = 50;
        imgMediaLogo.Height = 50;


        //string adsprice = ESP.Media.BusinessLogic.MediaattachmentsManager.GetModel(media.Adsprice).Attachmentpath;//广告报价
        //if (adsprice != null && adsprice.Length > 0)
        //{
        //    hlAdsPrice.NavigateUrl = adsprice;
        //}
        string briefing = ESP.Media.BusinessLogic.MediaattachmentsManager.GetModel(media.Briefing).Attachmentpath;//剪报
        if (briefing != null && briefing.Length > 0)
        {
            hlBriefing.NavigateUrl = briefing;
        }

        labLanMuIntro.Text = media.Mediaintro.Trim();//栏目简介
        labRemark.Text = media.Remarks.Trim();//备注


        labCountry.Text = ESP.Media.BusinessLogic.CountryManager.GetModel(media.Countryid).Countryname;
        labProvince.Text = ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Provinceid).Province_name;
        labCity.Text = ESP.Media.BusinessLogic.CityManager.GetModel(media.Cityid).City_name;

        labProvinceAddr1.Text = ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Addr1_provinceid).Province_name;
        labCityAddr1.Text = ESP.Media.BusinessLogic.CityManager.GetModel(media.Addr1_cityid).City_name;

        //labProvinceAddr2.Text = ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Addr2_provinceid).Province_name;
        //labCityAddr2.Text = ESP.Media.BusinessLogic.CityManager.GetModel(media.Addr2_cityid).City_name;
        labPostcode.Text = media.Postcode;
        //labIssueRegion.Text = media.Issueregion;
        labRegionAttribute.Text = ESP.Media.Access.Utilities.Global.RegionAttributeName[media.Regionattribute].ToString();

        this.ltrBranch.Text = media.Branchs.Replace(",", "<br />");
    }
}
