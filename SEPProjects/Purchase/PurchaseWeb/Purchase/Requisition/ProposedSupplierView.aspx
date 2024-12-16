<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Purchase_Requisition_ProposedSupplierView" Title="推荐供应商信息查看" Codebehind="ProposedSupplierView.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%@ Register Src="../../UserControls/View/SupplierView.ascx" TagName="SupplierView"
    TagPrefix="uc1" %>    
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">推荐供应商信息查看</td>
    </tr>
    
    <tr>
        <td colspan="4">
    <uc1:SupplierView ID="SupplierView" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="4" align="right"><asp:Button ID="btnBack" runat="server" Text=" 返回 " Visible="false" CssClass="widebuttons" OnClick="btnBack_Click" /></td>
    </tr>
</table>
</asp:Content>
