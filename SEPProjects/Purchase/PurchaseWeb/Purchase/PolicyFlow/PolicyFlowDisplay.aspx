<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_PolicyFlow_PolicyFlowDisplay" Codebehind="PolicyFlowDisplay.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm">
    <tr>
        <td colspan="2" class="heading">查看政策流程</td>
    </tr>
    <tr>
        <td style="width:20%" class="oddrow">标题：</td>
        <td class="oddrow-l"><asp:Label ID="txtTitle" runat="server" Width="90%" /></td>
    </tr>
    <tr>
        <td class="oddrow">简介：</td>
        <td class="oddrow-l"><asp:Label ID="txtSynopsis" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">内容：</td>
        <td class="oddrow-l"><asp:Label ID="txtContents" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2" class="oddrow-l">
            &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
        </td>
    </tr>
</table>
</asp:Content>

