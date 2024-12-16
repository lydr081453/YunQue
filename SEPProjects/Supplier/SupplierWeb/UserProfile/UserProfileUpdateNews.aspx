<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfileUpdateNews.aspx.cs" Inherits="SupplierWeb.UserProfile.UserProfileUpdateNews"  MasterPageFile="~/MainPage.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td width="30%">标题：</td>
            <td><asp:TextBox ID="txtTitle" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">内容：</td>
            <td><asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="600px" Height="400px"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
</asp:Content>