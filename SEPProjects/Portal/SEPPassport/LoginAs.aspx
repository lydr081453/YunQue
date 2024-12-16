<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAs.aspx.cs" Inherits="PassportWeb.LoginAs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblAdminUsername" Text="Admin 用户名：" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAdminUsername" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblAdminPassword" Text="Admin 密码：" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAdminPassword" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblLoginAs" Text="登录为：" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtLoginAs" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" />
                </td>
            </tr>
        </table>
        <div>
        <asp:Label runat="server" ID="lblFailureText" EnableViewState="false" ForeColor="Red" />
        </div>
    </div>
    </form>
</body>
</html>
