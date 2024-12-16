<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="RequestForSealView.aspx.cs" Inherits="AdministrativeWeb.RequestForSeal.RequestForSealView" %>

<%@ Register Src="~/RequestForSeal/Ctl_View.ascx" TagPrefix="uc1" TagName="Ctl_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Ctl_View runat="server" id="Ctl_View" />
    <br />
    <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
</asp:Content>
