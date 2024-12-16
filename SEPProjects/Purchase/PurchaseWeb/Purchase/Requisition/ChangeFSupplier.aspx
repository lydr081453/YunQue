<%@ Page Language="C#" Title="编辑非协议供应商信息" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ChangeFSupplier.aspx.cs" Inherits="Purchase_Requisition_ChangedFSupplier" %>

<%@ Register Src="../../UserControls/Edit/supplierInfo.ascx" TagName="supplierInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons"
        OnClick="btnSave_Click" />
        <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
