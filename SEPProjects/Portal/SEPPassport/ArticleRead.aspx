<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleRead.aspx.cs" Inherits="PassportWeb.ArticleRead" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/login.css" />
    <link rel="Stylesheet" type="text/css" href="css/style.css" />

    <script language="javascript" type="text/javascript">

        function IsRead0(sender, args) {
            var flag = false;
            if (ifmZero.document.body.scrollTop + ifmZero.document.body.offsetHeight >= ifmZero.document.body.scrollHeight)
                flag = true;

            if (flag) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        function IsRead1(sender, args) {
            var flag = false;
            if (ye_xy.document.body.scrollTop + ye_xy.document.body.offsetHeight >= ye_xy.document.body.scrollHeight)
                flag = true;

            if (flag) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        function IsRead2(sender, args) {
            var flag = false;
            if (ifmIn.document.body.scrollTop + ifmIn.document.body.offsetHeight >= ifmIn.document.body.scrollHeight)
                flag = true;

            if (flag) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <table width="100%" height="100%" bgcolor="#cccccc">
        <tr>
            <td width="100%" height="100%" align="center" valign="middle">
                <table width="640px" class="tableForm">
                    <tr>
                        <td class="heading" colspan="2" align="left">
                            个人信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            员工姓名：
                        </td>
                        <td class="oddrow-l" align="left">
                            <asp:Label runat="server" ID="lblUserName" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            员工编号：
                        </td>
                        <td class="oddrow-l" align="left">
                            <asp:Label runat="server" ID="lblUserCode" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnNext" CssClass="widebuttons" OnClick="btnNext_Click"
                    Text="我同意以下各项条款及规范，并承诺遵守各项条款及规范。" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="100%" height="100%" align="center" valign="middle">
                <table width="640px" border="0" cellspacing="0" cellpadding="0">
                    <tr id="trManual" runat="server" visible="false">
                        <td colspan="2" style="border-bottom: 1px solid #CCC;">
                            <table>
                                <tr>
                                    <td valign="top" align="left">
                                        <font color="red">* </font><font style="font-size: 14px; font-weight: bold;">员工手册：</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        <iframe border="0" name="ifmZero" marginwidth="0" framespacing="0" marginheight="0"
                                            src="Manual.htm" frameborder="0" noresize width="600px" scrolling="auto"
                                            height="190px" vspale="0"></iframe>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="IsRead0"
                                            Display="None" ErrorMessage="请阅读 员工手册" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="15px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trClause" runat="server" visible="false">
                        <td colspan="2" style="border-bottom: 1px solid #CCC;">
                            <table>
                                <tr>
                                    <td valign="top" align="left">
                                        <font color="red">* </font><font style="font-size: 14px; font-weight: bold;">网络服务协议：</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        <iframe border="0" name="ifmZero" marginwidth="0" framespacing="0" marginheight="0"
                                            src="ClausePage.htm" frameborder="0" noresize width="600px" scrolling="auto"
                                            height="190px" vspale="0"></iframe>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="IsRead0"
                                            Display="None" ErrorMessage="请阅读 网络行为规范" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="15px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
