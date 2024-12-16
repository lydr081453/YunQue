<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_ClientControl_ClientView" Codebehind="ClientDisplay.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td colspan="4" align="left">
            <dl class="edit_tittle" style="width: 100%">
                <dt>客户详细信息</dt>
                <dt class="divline" style="width: 100%"></dt>
            </dl>
        </td>
    </tr>
    <tr>
        <td style="width: 20%">
            最后更新人：
        </td>
        <td style="width: 30%">
            <asp:Label ID="labLastModifyUser" runat="server"></asp:Label>
        </td>
        <td style="width: 20%">
            最后更新时间：
        </td>
        <td style="width: 30%">
            <asp:Label ID="labLastModifyDate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="4">
            <table width="100%" border="1" class="tableForm">
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 30%">
                    </td>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
                <tr>
                    <td class="heading" colspan="4">
                        <asp:Label ID="labClientName" runat="server">公司名称</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="7" style="width: 15%; text-align: center;" class="oddrow">
                        <div>
                            <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )" class="highslide">
                                <asp:Image runat="server" ID="imgLogo" /></a>
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
                    <td style="width: 20%" class="oddrow">
                        中文全称：
                    </td>
                    <td style="width: 50%" class="oddrow-l" colspan="2">
                        <asp:Label ID="labChFullName" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%" class="oddrow">
                        中文简称：
                    </td>
                    <td style="width: 70%" class="oddrow-l" colspan="2">
                        <asp:Label ID="labChShortName" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        英文全称：
                    </td>
                    <td class="oddrow-l" colspan="2">
                        <asp:Label ID="labEnFullName" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        英文简称：
                    </td>
                    <td class="oddrow-l" colspan="2">
                        <asp:Label ID="labEnShortName" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        省：
                    </td>
                    <td class="oddrow-l" colspan="2">
                        <asp:Label ID="labProvince" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        市：
                    </td>
                    <td class="oddrow-l" colspan="2">
                        <asp:Label ID="labCity" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        简介：
                    </td>
                    <td class="oddrow-l" colspan="2">
                        <asp:Label ID="labBrief" runat="server" Width="90%"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
