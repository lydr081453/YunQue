<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfileUpdatePhoto.aspx.cs" Inherits="SupplierWeb.UserProfile.UserProfileUpdatePhoto" MasterPageFile="~/MainPage.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td width="30%">描述：</td>
            <td><asp:TextBox ID="txtShowTxt" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">内容：</td>
            <td><asp:FileUpload ID="fileIcon" runat="server" Width="300px" /><br />
                            <b>请注意：图片最大为2MB</b></td>
        </tr>
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
</asp:Content>