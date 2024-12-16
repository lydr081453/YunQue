<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdateIntro.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfoUpdateIntro"  MasterPageFile="~/MainPage.Master"%>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%">
    <tr>
        <td>
            <FCKeditorV2:FCKeditor ID="txtNeiRong" runat="server" Width="90%" Height="400px">
                                </FCKeditorV2:FCKeditor>
        </td>
    </tr>
</table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
</asp:Content>
