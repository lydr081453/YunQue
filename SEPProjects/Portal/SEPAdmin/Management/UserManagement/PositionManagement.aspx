<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="PositionManagement.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.PositionManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%">
    <tr>
        <td class="oddrow">名称：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtName" runat="server" /></td>
    </tr>
        <tr>
        <td class="oddrow">ChargeRate：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtChargeRate" runat="server" /></td>
    </tr>
        <tr>
        <td class="oddrow">等级：</td>
        <td class="oddrow-l"><asp:DropDownList ID="ddlLevels" runat="server" /></td>
    </tr>
    <tr>
    <td colspan="2"><asp:Button ID="btnSave" runat="server" Text="保存" CssClass="widebuttons" OnClick="btnSave_Click" />
        &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CausesValidation="false" OnClick="btnBack_Click" />
    </td>
    </tr>
</table>
</asp:Content>
