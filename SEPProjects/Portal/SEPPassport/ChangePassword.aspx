<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="PassportWeb.ChangePassword" %>

<%@ Register Assembly="ESP.Core" Namespace="ESP.Web.UI" TagPrefix="esp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <link rel="Stylesheet" type="text/css" href="css/login.css" />
    <style type="text/css">
        body
        {
            background-repeat: no-repeat;
            background-image: url();
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            margin-top: 0px;
            margin-bottom: 0px;
        }
        .book
        {
            height: 14px;
        }
        .imgbj
        {
            background-attachment: fixed;
            background-image: url(images/img_01.jpg);
            background-repeat: no-repeat;
            background-position: right bottom;
        }
        .biankuang
        {
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CCCCCC;
        }
        .red-star
        {
            color: Red;
            padding-left: 3px;
        }
        .button
        {
            background-image: url(images/button-bg.jpg);
            width: 91px;
            height: 26px;
            border-style: hidden;
            color: White;
            font-family: 黑体;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
    
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/dialog1.js"></script>
    <script type="text/javascript">
        function ArticleIsRead(uid, uname, ucode, rurl) {
            $(function() {
                dialog("条款确认", "iframe:ArticleRead.aspx?uid=" + uid + "&uname=" + uname + "&ucode="+ ucode + "&rurl="+rurl, "800px", "600px", "text");
            });
        }
    </script>
</head>
<body>
    <div style="background-image:none; background-position: center top;
        height: 85px; width: 1107px; margin: auto; background-repeat: no-repeat; width:auto;">
    </div>
    <div style="background-image: url(images/left.jpg); background-position: left center;
        width: 690px; margin: auto; background-repeat: no-repeat">
        <div style="float: right; margin: 100px 20px 100px 20px; width: 343px; background-image: url(images/form-bg.jpg)">
            <div style="padding: 30px;">
                <div style="color: Red">
                    <b>星言云汇内网系统执行密码安全策略要求</b>， 你必须修改密码才能继续操作。为此给您带来的不便，敬请谅解。
                    <br />
                    <br />
                </div>
                <form id="frmMain" runat="server" defaultfocus="UserCode">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="right">
                            <font face="幼圆" size="2" color="gray"><strong>旧密码</strong></font>：&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="OldPassword" runat="server" MaxLength="20" CssClass="inparea" TextMode="Password"
                                Width="180px" Height="20px" Style="border-style: solid; border-width: 1px; border-color: #abb7cd"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height: 25px;">
                            <asp:RequiredFieldValidator ID="RVOldPassword" runat="server" Font-Size="12px" Display="Dynamic"
                                ControlToValidate="OldPassword" ErrorMessage="旧密码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 3px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="幼圆" size="2" color="gray"><strong>新密码</strong></font>：&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Password" runat="server" MaxLength="20" CssClass="inparea" TextMode="Password"
                                Width="180px" Height="20px" Style="border-style: solid; border-width: 1px; border-color: #abb7cd"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height: 25px;">
                            <asp:RequiredFieldValidator ID="RVPassword" Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToValidate="Password" ErrorMessage="新密码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REVPassword"  Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToValidate="Password" ErrorMessage="密码长度不能小于8。" ValidationExpression=".{8,256}"  SetFocusOnError="true"/>
                            <asp:RegularExpressionValidator ID="REVPassword2"  Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToValidate="Password" ErrorMessage="密码过于简单。" ValidationExpression="^(?!password$).*$"  SetFocusOnError="true"/>
                            <asp:RegularExpressionValidator ID="REVPassword3"  Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToValidate="Password" ErrorMessage="密码必须是由字母和数字组成。" ValidationExpression="^(?=.*\d)(?=.*[A-Za-z]).{8,256}$"  SetFocusOnError="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 3px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="幼圆" size="2" color="gray"><strong>重复新密码</strong></font>：&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="PasswordConfirm" runat="server" MaxLength="20" CssClass="inparea"
                                TextMode="Password" Width="180px" Height="20px" Style="border-style: solid; border-width: 1px;
                                border-color: #abb7cd"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height: 25px;">
                            <asp:RequiredFieldValidator ID="RVPasswordConfirm" Font-Size="12px" Display="Dynamic"
                                runat="server" ControlToValidate="PasswordConfirm" ErrorMessage="重复新密码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CVPassword" Font-Size="12px" Display="Dynamic" runat="server"
                                ControlToCompare="Password" ControlToValidate="PasswordConfirm" ErrorMessage="重复新密码 必须 与 新密码 相同。"  SetFocusOnError="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 3px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlCaptcha">
                        <tr>
                            <td align="right">
                                <font face="幼圆" size="2" color="gray"><strong>验证码</strong></font>：&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="20" CssClass="inparea" TextMode="Password"
                                    Width="60px" Height="20px" Style="border-style: solid; border-width: 1px; border-color: #abb7cd"></asp:TextBox>
                                <asp:Image runat="server" ID="imgCaptcha" AlternateText="看不清？点击图片换一张。" ImageUrl="Captcha.aspx"
                                    Width="80" Height="30" ImageAlign="AbsMiddle" BorderColor="Silver" BorderWidth="1px"
                                    BorderStyle="Solid" />
                                <asp:Image runat="server" ID="imgRefreshCaptcha" AlternateText="看不清？点击图片换一张。" ImageUrl="~/images/hip_reload.gif"
                                    ImageAlign="AbsMiddle" Style="cursor: pointer; cursor: hand;" />
                                <esp:CaptchaExtender ChangeImageButton="imgRefreshCaptcha" ImageControl="imgCaptcha"
                                    InputControl="txtCaptcha" runat="server" ID="ctlCaptcha" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="height: 25px;">
                                <asp:RequiredFieldValidator ID="RVCaptcha" Font-Size="12px" Display="Dynamic" runat="server"
                                    ControlToValidate="txtCaptcha" ErrorMessage="验证码 字段是必需的。" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ForeColor="Black" CssClass="button" ID="btnLogin" runat="server" Text="修改" OnClick="btnLogin_Click" />
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <div style="background-image: url(images/form-bottom.jpg); height: 76px; width: 343px;">
            </div>
        </div>
        <div style="clear: both">
        </div>
    </div>
</body>
</html>
