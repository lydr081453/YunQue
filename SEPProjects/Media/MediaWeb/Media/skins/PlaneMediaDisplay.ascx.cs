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

public partial class Media_skins_PlaneMediaDisplay : System.Web.UI.UserControl
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
    public void InitPage(MediaitemsInfo media)
    {
        if (media  == null)
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
        labPlaneName.Text = media.Mediacname.Trim();//媒体中文名称
        labPlaneEngName.Text = media.Mediaename.Trim();//媒体英文名称
        //labPlaneHTCName.Text = media.Cshortname.Trim();//媒体中文简称
        labPlaneEngHTCName.Text = media.Eshortname.Trim();//媒体英文简称
        labISSN.Text = media.Issn.Trim();//刊号
        labCooperate.Text = media.Cooperate.Trim();//合作方式
        labMediumSort.Text = "平面媒体";//形态属性

        //受众描述
        labReaderSort.Text = media.Readersort.Trim();
        //string TopicTypeText = media.Readersort;

        //if (TopicTypeText.Equals("1"))
        //{
        //    labReaderSort.Text = "全部";//全部
        //}
        //else if (TopicTypeText.Equals("2"))
        //{
        //    labReaderSort.Text = "大众"; //大众

        //}
        //else if (TopicTypeText.Equals("3"))
        //{
        //    labReaderSort.Text = "行业";//行业

        //}
        //else if (TopicTypeText.Equals("4"))
        //{
        //    labReaderSort.Text = "专业";//专业

        //}


        //行业属性
        labIndustry.Text = ESP.Media.BusinessLogic.IndustriesManager.GetModel(media.Industryid).Industryname;
        //地域属性
        labRegionAttribute.Text = ESP.Media.Access.Utilities.Global.RegionAttributeName[media.Regionattribute].ToString();


        labGoverningBody.Text = media.Governingbody.Trim();//所属单位
        labDirectorUnit.Text = media.Frontfor.Trim();//主办单位
        labProprieter.Text = media.Proprieter.Trim();//社长
        labChiefEditor.Text = media.Chiefeditor.Trim();//总编
        labSubProprieter.Text = media.Subproprieter.Trim();//副社长
        labSubeditor.Text = media.Subeditor.Trim();//副总编
        //发行量
        labCirculation.Text = media.Circulation + "";

        labPublishChannels.Text = media.Publishchannels.Trim();//发行渠道
        labPublishPeriods.Text = media.Publishperiods.Trim();//发行渠道
        labTelephoneExchange.Text = media.Telephoneexchange.Trim();//总机
        labFax.Text = media.Fax.Trim();//传真
        labAddress1.Text = media.Addressone.Trim();//地址1
        //labAddress2.Text = media.Addresstwo.Trim();//地址2
        labPhoneOne.Text = media.Phoneone.Trim();//热线1
        labPhoneTwo.Text = media.Phonetwo.Trim();//热线2
        labStartPublication.Text = media.Startpublication.Trim().Split(' ')[0];//创刊日期
        if (labStartPublication.Text.Equals("1900-1-1"))
        {
            labStartPublication.Text = "";
        }
        labPublishDate.Text = media.Publishdate.Trim().Split(' ')[0];//发行日期
        if (labPublishDate.Text.Equals("1900-1-1"))
        {
            labPublishDate.Text = "";
        }
        labEndPublication.Text = media.Endpublication.Trim().Split(' ')[0];//截稿日期
        if (labEndPublication.Text.Equals("1900-1-1"))
        {
            labEndPublication.Text = "";
        }
        labAdsPhone.Text = media.Adsphone.Trim();//广告部电话


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
        //    hlAdsPrice.Target = "_blank";
        //}
        string briefing = ESP.Media.BusinessLogic.MediaattachmentsManager.GetModel(media.Briefing).Attachmentpath;//剪报
        if (briefing != null && briefing.Length > 0)
        {
            hlBriefing.NavigateUrl = briefing;
            hlBriefing.Target = "_blank";
        }

        labMediaIntro.Text = media.Mediaintro.Trim();//媒体简介
        labEngIntro.Text = media.Engintro.Trim();//英文简介
        labRemarks.Text = media.Remarks.Trim();//备注


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

        setBranch(media.Mediaitemid);
    }

    private void setBranch(int mediaID)
    {
        DataTable dt = ESP.Media.BusinessLogic.BranchManager.GetListByMediaItemID(mediaID);
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
