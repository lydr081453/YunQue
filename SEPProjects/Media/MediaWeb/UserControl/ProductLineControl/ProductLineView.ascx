<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_ProductLineControl_ProductLineView" Codebehind="ProductLineView.ascx.cs" %>
<dl class="edit_tittle" style="width: 100%">
    <dt>产品线信息</dt>
    <dt class="divline" style="width: 100%"></dt>
</dl>
<table width="100%" border="0"><tr>
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
    </tr></table>
<table width="100%" border="1" class="tableForm">
                <tr>
                    <td colspan="4" class="heading">
                        产品线信息
                    </td>
                </tr>    
    <tr>
        <td style="width: 20%" class="oddrow">
            所属客户名称：
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:LinkButton ID="lnkClientName" runat="server" />
            <asp:Button ID="btnChangeClient" runat="server" Text="变更所属客户" CssClass="bigwidebuttons"
                OnClientClick="openClient();return false;" />
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="oddrow">
            产品线名称：
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:Label ID="labProductLineName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="oddrow">
            产品线图片：
        </td>
        <td colspan="3" class="oddrow-l">
            <div>
                <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )" class="highslide">
                    <asp:Image ID="imgTitle" runat="server" Visible="false" CssClass="ThumbnailPhoto" /></a>
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
        <td style="width: 20%" class="oddrow">
            描述：
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:Label ID="labDes" runat="server"></asp:Label>
        </td>
    </tr>
</table>
