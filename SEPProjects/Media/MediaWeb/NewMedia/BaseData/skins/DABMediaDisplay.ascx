<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DABMediaDisplay.ascx.cs" Inherits="MediaWeb.NewMedia.BaseData.skins.DABMediaDisplay" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Panel runat="server" ID="panelDetail">
    <table border="1" style="width: 100%" class="tableForm">
        <tr>
            <td colspan="4" class="menusection-Packages">
                广播媒体明细信息
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages" style="width: 20%">
                最后更新人：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyUser" runat="server"></asp:Label>
            </td>
            <td class="menusection-Packages" style="width: 20%">
                最后新时间：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                基本信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                媒体中文名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labDABName" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                频道名称：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDABChannelName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                栏目名称：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDABTopicName" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                调频：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDABFM" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                媒体英文名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labDABEngName" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                媒体英文简称：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDABEngHTCName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                形态属性：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMediumSort" runat="server">广播媒体</asp:Label>
            </td>
            <td class="oddrow">
                栏目属性：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDABTopicProperty" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                行业属性：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labIndustry" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                地域属性：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labRegionAttribute" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                国家：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCountry" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                省/市：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labProvince" runat="server"></asp:Label>/<asp:Label ID="labCity" runat="server"></asp:Label>
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow">
                覆盖范围：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labOverrideRange" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                覆盖区域：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labIssueRegion" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                栏目播出时间：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labTopicBegin" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                近期收听率：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labRating" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            
            <td class="oddrow">
                栏目时长：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labTopicTime" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                主管单位：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDirectorUnit" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="oddrow">
                台长：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labManager" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                总编：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labChiefEditor" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                主任：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labZhuren" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                制片人：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labProducer" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                栏目合作方式：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labCooperate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                受众描述：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labReaderSort" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                分部所在地：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Literal ID="ltrBranch" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 30px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                联系方式
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                总机：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labTelephoneExchange" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                传真：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labFax" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                热线1：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPhoneOne" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                热线2：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPhoneTwo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                广告部电话：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labAdsPhone" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                网址：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labWebAddress" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                省/市：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labProvinceAddr1" runat="server">
                </asp:Label>/<asp:Label ID="labCityAddr1" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                详细地址：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labAddress1" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                邮政编码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPostcode" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 30px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                其他信息
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow">
                广告报价：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:HyperLink ID="hlAdsPrice" runat="server">下载附件</asp:HyperLink>
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow" valign="bottom">
                媒体LOGO：
            </td>
            <td class="oddrow-l" colspan="3">
                <div>
                    <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )"
                        class="highslide">
                        <asp:Image ID="imgMediaLogo" runat="server" ImageAlign="bottom" /></a>
                    <div class="highslide-html-content" id="photo-div">
                        <div class="highslide-header">
                            <ul>
                                <li class="highslide-move"><a href="#" onclick="return false">移动</a> </li>
                                <li class="highslide-close"><a href="#" onclick="return hs.close(this)">关闭</a> </li>
                            </ul>
                        </div>
                        <div class="highslide-body">
                            <asp:Image ID="imgTitleFull" runat="server" />
                        </div>
                        <div class="highslide-footer">
                            <div>
                                <span class="highslide-resize" title="Resize"><span></span></span>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                剪报：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:HyperLink ID="hlBriefing" runat="server">下载附件</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                栏目简介：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labTopicIntro" runat="server" Height="98px" Width="75%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                英文简介：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labEngIntro" runat="server" Height="98px" Width="75%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                备注：
            </td>
            <td class="oddrow-l" colspan="3" style="height: 113px">
                <asp:Label ID="labRemarks" runat="server" Height="98px" Width="75%"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
