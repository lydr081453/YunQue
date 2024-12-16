<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdate.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfoUpdate"  MasterPageFile="~/MainPage.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td width="30%">用户名：</td>
            <td><asp:Label ID="labLogName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td width="30%">全称：</td>
            <td><asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">所在地区：</td>
            <td><asp:DropDownList ID="ddlSupplierArea" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="30%">所处行业：</td>
            <td><asp:DropDownList ID="ddlSupplierIndustry" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="30%">规模：</td>
            <td><asp:DropDownList ID="ddlSupplierScale" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="30%">注册金额：</td>
            <td><asp:DropDownList ID="ddlSupplierPrincipal" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td width="30%">成立时间：</td>
            <td><asp:TextBox ID="txtSupplierBuilttime" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">网站：</td>
            <td><asp:TextBox ID="txtSupplierWebsite" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">电话：</td>
            <td><asp:TextBox ID="txtSupplierTel" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">传真：</td>
            <td><asp:TextBox ID="txtSupplierFax" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">手机：</td>
            <td><asp:TextBox ID="txtSupplierMobile" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">Email：</td>
            <td><asp:TextBox ID="txtSupplierEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">地址：</td>
            <td><asp:TextBox ID="txtSupplierAdress" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td width="30%">邮编：</td>
            <td><asp:TextBox ID="txtSupplierZIP" runat="server"></asp:TextBox></td>
        </tr>   
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
</asp:Content>
