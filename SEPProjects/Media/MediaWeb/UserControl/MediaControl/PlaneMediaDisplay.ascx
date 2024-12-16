<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_MediaControl_PlaneMediaDisplay" Codebehind="PlaneMediaDisplay.ascx.cs" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Panel Style="width: 100%" runat="server" ID="panelDetail">
    <table style="width: 100%;" border="1" class="tableForm">
        <tr>
            <td colspan="4" class="menusection-Packages">
                平面媒体明细信息
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
                <asp:Label ID="labPlaneName" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
            </td>
            <td class="oddrow-l">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                媒体英文名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labPlaneEngName" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                媒体英文简称：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPlaneEngHTCName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                刊号：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labISSN" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                形态属性：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMediumSort" runat="server">平面</asp:Label>
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
        <tr>
            <td class="oddrow">
                所属单位：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labGoverningBody" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                主办单位：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDirectorUnit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                创刊日期：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labStartPublication" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                截稿日期：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEndPublication" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                发行日期：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPublishDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                发行周期：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPublishPeriods" runat="server"></asp:Label>
            </td>
        </tr>
        <%--        <tr>
            <td class="oddrow">
                覆盖区域：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labIssueRegion" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                社长：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labProprieter" runat="server"></asp:Label>
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
                副社长：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labSubProprieter" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                副总编：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labSubeditor" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                发行量：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCirculation" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                发行渠道：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPublishChannels" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                合作方式：
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
                省/市：
            </td>
            <td class="oddrow-l">
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
            <td colspan="4" class="heading">
                地址2：
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                省：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labProvinceAddr2" runat="server">
                </asp:Label>
            </td>
            <td class="oddrow">
                市：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCityAddr2" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                详细地址：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labAddress2" runat="server" Width="75%"></asp:Label>
            </td>
        </tr>--%>
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
                    <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )" class="highslide">
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
                媒体简介：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labMediaIntro" runat="server" Height="98px" Width="75%"></asp:Label>
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
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labRemarks" runat="server" Height="98px" Width="75%"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
