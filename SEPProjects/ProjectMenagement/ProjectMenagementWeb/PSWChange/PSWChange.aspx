<%@ Page Language="C#" AutoEventWireup="true" Inherits="PSWChange_PSWChange" CodeBehind="PSWChange.aspx.cs" %>

<html>
<head runat="server">
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
    <table style="width: 100%">
        <tr>
            <td class="menusection-Packages" colspan="2">
                系统管理 &gt; 更改密码
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 2px;">
                &nbsp;<asp:Label ID="lblMsg" runat="server" CssClass="message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px">
                <table width="100%" class="tableForm" id="Table2">
                    <tr>
                        <td class="heading" colspan="3">
                            详细信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px">
                            旧密码:
                        </td>
                        <td class="oddrow-l" style="width: 120px">
                            <asp:TextBox ID="OldPSW" runat="server" Width="120" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="oddrow-l">
                            <asp:RequiredFieldValidator ID="RVOldPSW" runat="server" ControlToValidate="OldPSW"
                                ErrorMessage="请输入原密码">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px">
                            新密码:
                        </td>
                        <td class="oddrow-l" style="width: 120px">
                            <asp:TextBox ID="NewPSW" runat="server" Width="120" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="oddrow-l">
                            <asp:RequiredFieldValidator ID="RVPassword" Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToValidate="NewPSW" ErrorMessage="新密码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REVPassword" Font-Size="12px" Display="Dynamic"
                                runat="server" ControlToValidate="NewPSW" ErrorMessage="密码长度不能小于8。" ValidationExpression=".{8,256}"
                                SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="REVPassword2" Font-Size="12px" Display="Dynamic"
                                runat="server" ControlToValidate="NewPSW" ErrorMessage="密码过于简单。" ValidationExpression="^(?!password$).*$"
                                SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="REVPassword3" Font-Size="12px" Display="Dynamic"
                                runat="server" ControlToValidate="NewPSW" ErrorMessage="密码必须是由字母和数字组成。" ValidationExpression="^(?=.*\d)(?=.*[A-Za-z]).{8,256}$"
                                SetFocusOnError="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px">
                            确认密码:
                        </td>
                        <td class="oddrow-l" style="width: 120px">
                            <asp:TextBox ID="ConfirmPSW" runat="server" Width="120" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="oddrow-l">
                            <asp:RequiredFieldValidator ID="RVPasswordConfirm" Font-Size="12px" Display="Dynamic"
                                runat="server" ControlToValidate="ConfirmPSW" ErrorMessage="重复新密码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CVPassword" Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToCompare="NewPSW" ControlToValidate="ConfirmPSW" ErrorMessage="重复新密码 必须 与 新密码 相同。"
                                SetFocusOnError="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpdate" CssClass="widebuttons" Text="  提交  " runat="server" OnClick="btnUpdate_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
