<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_PolicyFlow_PolicyFlowEdit" Codebehind="PolicyFlowEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm">
    <tr>
        <td colspan="2" class="heading">编辑政策流程</td>
    </tr>
    <tr>
        <td style="width:20%" class="oddrow">标题：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtTitle" runat="server" Width="90%" /></td>
    </tr>
    <tr>
        <td class="oddrow">简介：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtSynopsis" runat="server" Width="90%" TextMode="MultiLine" Height="120px" /></td>
    </tr>
    <tr>
        <td class="oddrow">附件：</td>
        <td class="oddrow-l"><asp:FileUpload ID="filContents" runat="server" />&nbsp;<asp:Label ID="labUpload" runat="server" />&nbsp;<asp:CheckBox ID="chkContents" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" /></td>
    </tr>
    <tr>
        <td colspan="2" class="oddrow-l"><asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
            &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
        </td>
    </tr>
</table>
</asp:Content>

