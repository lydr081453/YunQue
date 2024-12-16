<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_ProductView" Title="采购物品查看" Codebehind="ProductView.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">物品查看</td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">供应商</td>
        <td class="oddrow-l" colspan="3"><asp:Label ID="labSupplier" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">物品名称</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="labProductName" runat="server" /></td>
        <td class="oddrow" style="width:20%">物料种类</td>
        <td class="oddrow-l" style="width:30%"><asp:Label ID="labproductClass" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">物品单位</td>
        <td class="oddrow-l"><asp:Label ID="labproductUnit" runat="server" /></td>
        <td class="oddrow">物品描述</td>
        <td class="oddrow-l"><asp:Label ID="labproductDes" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="4" align="right"><asp:Button ID="btnBack" runat="server" Text=" 返回 " OnClick="btnBack_Click"  CssClass="widebuttons" /></td>
    </tr>
</table>
</asp:Content>

