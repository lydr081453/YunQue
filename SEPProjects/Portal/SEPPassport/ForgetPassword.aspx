<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs"
    Inherits="PassportWeb.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" ID="pnl1">
            <table border="0" cellpadding="0">
                <tr>
                    <td align="center" colspan="2">
                        是否忘记了您的密码?
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        要接收您的密码，请输入您的用户名。
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">用户名:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="必须填写“用户名”。" ToolTip="必须填写“用户名”。" ValidationGroup="PasswordRecovery">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="color: Red;">
                        <asp:Label ForeColor="Red" ID="FailureText" runat="server" EnableViewState="False" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="SubmitButton" runat="server" Text="提交" ValidationGroup="PasswordRecovery"
                            OnClick="SubmitButton_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnl2" Visible="false">
            <div>
                一封重置密码的邮件已经发送到了你的安全邮箱，请注意查收并按邮件中指示完成重置密码操作。</div>
            <div>
                如果你没有收到上述邮件，请点击
                <asp:LinkButton runat="server" ID="btnResend" Text="这里" 
                    onclick="btnResend_Click" />
                重新发送。
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
