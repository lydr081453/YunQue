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
using ESP.Media.BusinessLogic;

public partial class Media_skins_DABMediaContentsDisplay : System.Web.UI.UserControl
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
        labVersion.Text = media.Version + "";
        labDABName.Text = media.Mediacname.Trim();//媒体中文名称
        labDABEngName.Text = media.Mediaename.Trim();//媒体英文名称
        //labDABHTCName.Text = media.Cshortname.Trim();
        labDABEngHTCName.Text = media.Eshortname.Trim();

        labDABChannelName.Text = media.Channelname.Trim();//频道名称(音乐之声)
        labDABFM.Text = media.Dabfm.Trim();// 调频（FM103.9）
        labDABTopicName.Text = media.Topicname.Trim();//栏目名称（中国音乐排行榜）
        labDABTopicProperty.Text = ESP.Media.Access.Utilities.Global.LanMuType.TypeNames[media.Topicproperty];//栏目属性
        labMediumSort.Text = "广播媒体";//形态属性

        //行业属性
        DataTable dtIndustries = MediaindustryrelationManager.getAllListByMediaid(media.Mediaitemid);
        string industry = string.Empty;
        for (int i = 0; i < dtIndustries.Rows.Count; i++)
        {
            industry += dtIndustries.Rows[i]["industryname"] == DBNull.Value ? string.Empty :
            dtIndustries.Rows[i]["industryname"].ToString().Trim();
            industry += " ";
        }
        labIndustry.Text = industry;


        //覆盖范围
        //labOverrideRange.Text = ESP.Media.Access.Utilities.Global.CoverRage.CoverRages[Convert.ToInt32(media.Overriderange == "" ? "0" : media.Overriderange)];


        //受众描述
        labReaderSort.Text = media.Readersort.Trim();
        //string TopicTypeText = media.Readersort;

        //if (TopicTypeText.Equals("0"))
        //{
        //    labReaderSort.Text = "全部";
        //}
        //else if (TopicTypeText.Equals("1"))
        //{
        //    labReaderSort.Text = "大众";

        //}
        //else if (TopicTypeText.Equals("2"))
        //{
        //    labReaderSort.Text = "行业";

        //}
        //else if (TopicTypeText.Equals("3"))
        //{
        //    labReaderSort.Text = "专业";

        //}

        labRating.Text = media.Rating.Trim();//近期收视率
        labDirectorUnit.Text = media.Governingbody.Trim();//主管单位
        labCooperate.Text = media.Cooperate.Trim();//栏目合作方式
        labManager.Text = media.Manager.Trim();//台长
        labChiefEditor.Text = media.Chiefeditor.Trim();//总编
        labZhuren.Text = media.Zhuren.Trim();//主任
        labProducer.Text = media.Producer.Trim();//制片人
        labTelephoneExchange.Text = media.Telephoneexchange.Trim();//总机
        labFax.Text = media.Fax = labFax.Text.Trim();//传真
        labAddress1.Text = media.Addressone.Trim();//地址1
        //labAddress2.Text = media.Addresstwo.Trim();//地址2
        labPhoneOne.Text = media.Phoneone.Trim();//热线1
        labPhoneTwo.Text = media.Phonetwo.Trim();//热线2
        labAdsPhone.Text = media.Adsphone.Trim();//广告部电话
        labWebAddress.Text = media.Webaddress.Trim();//网址
        labTopicBegin.Text = media.Publishdate.Split(' ')[0];//栏目播出时间
        if (labTopicBegin.Text.Equals("1900-1-1"))
        {
            labTopicBegin.Text = "";
        }
        labTopicTime.Text = media.Topictime + "";//栏目时长

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

        //string adsprice = MediaattachmentsManager.GetModel(media.Adsprice).Attachmentpath;//广告报价
        //if (adsprice != null && adsprice.Length > 0)
        //{
        //    hlAdsPrice.NavigateUrl = adsprice;
        //}
        string briefing = MediaattachmentsManager.GetModel(media.Briefing).Attachmentpath;//剪报
        if (briefing != null && briefing.Length > 0)
        {
            hlBriefing.NavigateUrl = briefing;
        }

        labTopicIntro.Text = media.Mediaintro.Trim();//栏目简介
        labEngIntro.Text = media.Engintro.Trim();//英文简介
        labRemarks.Text = media.Remarks.Trim();//备注

        labCountry.Text = CountryManager.GetModel(media.Countryid).Countryname;
        labProvince.Text = ProvinceManager.GetModel(media.Provinceid).Province_name;
        labCity.Text = CityManager.GetModel(media.Cityid).City_name;

        labProvinceAddr1.Text = ProvinceManager.GetModel(media.Addr1_provinceid).Province_name;
        labCityAddr1.Text = CityManager.GetModel(media.Addr1_cityid).City_name;

        //labProvinceAddr2.Text = ProvinceManager.GetModel(media.Addr2_provinceid).Province_name;
        //labCityAddr2.Text = CityManager.GetModel(media.Addr2_cityid).City_name;
        labPostcode.Text = media.Postcode;
        labRegionAttribute.Text = ESP.Media.Access.Utilities.Global.RegionAttributeName[media.Regionattribute].ToString();
        // labIssueRegion.Text = media.Issueregion;//覆盖区域

        this.ltrBranch.Text = media.Branchs.Replace(",", "<br />");
    }
    #endregion

    private int itemid;
    public int MediaItemID
    {
        get { return this.itemid; }
        set { this.itemid = value; }
    }
}
