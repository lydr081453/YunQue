<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdateLinkMan.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfoUpdateLinkMan"  MasterPageFile="~/MainPage.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>姓名：</td>
            <td><asp:TextBox ID="txtName" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>头像：</td>
            <td >
                <asp:FileUpload ID="fileIcon" runat="server" Width="300px" /><br />
                            <b>请注意：图片最大为2MB</b>
            </td>
        </tr>
        <tr>
            <td>性别：</td>
            <td><asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="男" Value="0" Selected></asp:ListItem>    
                    <asp:ListItem Text="女" Value="1"></asp:ListItem>    
                    <asp:ListItem Text="不明" Value="2"></asp:ListItem>    
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>职务：</td>
            <td><asp:TextBox ID="txtTitle" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>座机：</td>
            <td><asp:TextBox ID="txtTel" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>手机：</td>
            <td><asp:TextBox ID="txtMobile" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>传真：</td>
            <td><asp:TextBox ID="txtFax" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>地址：</td>
            <td><asp:TextBox ID="txtAddress" runat="server"  MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td>邮编：</td>
            <td><asp:TextBox ID="txtZIP" runat="server"  MaxLength="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email：</td>
            <td><asp:TextBox ID="txtEmail" runat="server"  MaxLength="25"></asp:TextBox></td>
        </tr>
        <tr>
            <td>QQ：</td>
            <td><asp:TextBox ID="txtQQ" runat="server"  MaxLength="13" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>MSN：</td>
            <td><asp:TextBox ID="txtMSN" runat="server" MaxLength="25" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>备注：</td>
            <td><asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" ></asp:TextBox></td>
        </tr>
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
</asp:Content>