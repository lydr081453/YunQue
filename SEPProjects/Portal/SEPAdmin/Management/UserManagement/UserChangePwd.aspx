<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserChangePwd.aspx.cs"
    Inherits="SEPAdmin.Management.UserManagement.UserChangePwd" %>

<html>
<head id="HEAD1" runat="server">
    <title>更改密码</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../public/css/style.css" rel="stylesheet">

    <script language="javascript" src="../../public/js/syscomm.js"></script>

</head>
<body>
    <form id="frmMain" runat="server">
    <table width="100%" class="tableForm" id="Table2">
        <tr>
            <td class="heading" colspan="2">
                修改密码
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%;">
                用户名:
            </td>
            <td class="oddrow-l" style="width: 120px;">
                <asp:TextBox ID="txtUsername" runat="server" Height="21" Width="120"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%;">
                新密码:
            </td>
            <td class="oddrow-l" style="width: 120px">
                <asp:TextBox ID="NewPSW" runat="server" Width="120" Height="21" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RVNewPSW" runat="server" ControlToValidate="NewPSW"
                    ErrorMessage="请输入新密码">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%;">
                确认密码:
            </td>
            <td class="oddrow-l" style="width: 120px">
                <asp:TextBox ID="ConfirmPSW" runat="server" Width="120" Height="21" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RVConfirmPSW" runat="server" ControlToValidate="ConfirmPSW"
                    ErrorMessage="请输入确认密码">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpdate" CssClass="widebuttons" Text="  提交  " runat="server" OnClick="btnUpdate_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 2px;">
                &nbsp;<asp:Label ID="lblMsg" runat="server" CssClass="message"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
