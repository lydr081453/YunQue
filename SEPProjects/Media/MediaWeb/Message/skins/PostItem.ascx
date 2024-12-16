<%@ Control Language="C#" AutoEventWireup="true" Inherits="Message_skins_PostItem" Codebehind="PostItem.ascx.cs" %>
<%@ Register TagPrefix="uc" TagName="Post" Src="Post.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserView" Src="UserView.ascx" %>
<table width="100%" border="0" class="tableForm">
    <tr style="height: 300px;">
<%--        <td style="width: 30%;" align="center">
            <uc:UserView runat="server" ID="uvItem" />
        </td>--%>
        <td style="width: 70%;">
            <uc:Post runat="server" ID="pItem" />
        </td>
    </tr>
</table>
