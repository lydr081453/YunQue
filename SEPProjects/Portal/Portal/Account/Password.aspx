<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="Portal.WebSite.Account.Password"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td class="nav">
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td class="title">设置</td></tr>
                </table>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td width="30%" valign="top" class="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" align="right">基本资料</td></tr>
                                <tr><td height="40" align="right"><%--<a href="/Account/IM.aspx">绑定设置</a>--%></td></tr>
                            </table>
                        </td>
                        <td width="70%" valign="top" class="right">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" colspan="2" class="btn"> 修改密码
                                <em>|</em><a href="/Account/Settings.aspx"> 头像 </a><em>|</em></td>
                                </tr>
                                <tr><td>
                                        <table border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td align="right">
                                                    当前密码
                                                </td>
                                                <td>
                                                    <input type="password" class="input" id="Password1" name="OldPassword" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    新密码
                                                </td>
                                                <td>
                                                    <input type="password" class="input" id="Password2" name="NewPassword" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    确认新密码
                                                </td>
                                                <td>
                                                    <input type="password" class="input" id="Password3" name="NewPasswordConfirm" />
                                                </td>
                                            </tr>
                                        </table>
                                        <p><%= SavePasswordError %></p>
                                        <p>
                                            <input type="submit" id="Submit1" class="enterbtn" value="保存" name="SavePassword" /></p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <%--<div id="container">
        <p class="top">
            设置</p>
        <div id="wtMainBlock">
            <div class="leftdiv">
                <ul class="leftmenu">
                    <li><a href="/account/settings.aspx" class="now">基本资料</a></li>
                    <li><a href="/Account/IM.aspx">绑定设置</a></li>
                </ul>
            </div>
            <!-- leftdiv -->
            <div class="rightdiv">
                <div class="lookfriend">
                    <form id="f" action="" enctype="multipart/form-data" method="post" name="f" runat="server">
                    <p class="right14">
                        &nbsp;&nbsp;<a href="/Account/Password.aspx" class="now">修改密码</a>&nbsp;|
                        &nbsp;&nbsp;<a href="/Account/Settings.aspx">头像</a>&nbsp;|
                    </p>
                    <div class="accountBox">
                        <table>
                            <tr>
                                <td align="right">
                                    当前密码
                                </td>
                                <td>
                                    <input type="password" class="inputStyle" id="OldPassword" name="OldPassword" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    新密码
                                </td>
                                <td>
                                    <input type="password" class="inputStyle" id="NewPassword" name="NewPassword" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    确认新密码
                                </td>
                                <td>
                                    <input type="password" class="inputStyle" id="NewPasswordConfirm" name="NewPasswordConfirm" />
                                </td>
                            </tr>
                        </table>
                        <p><%= SavePasswordError %></p>
                        <p>
                            <input type="submit" id="SavePassword" class="submitbutton" value="保存" name="SavePassword" /></p>
                    </div>
                    <!-- accountBox -->
                    <div style="overflow: hidden; clear: both; height: 50px; line-height: 1px; font-size: 1px;">
                    </div>
                    </form>
                </div>
                <!-- lookfriend -->
                <div style="overflow: hidden; clear: both; height: 50px; line-height: 1px; font-size: 1px;">
                </div>
            </div>
            <!-- rightdiv -->
        </div>
        <!-- #wtMainBlock -->
        <div style="overflow: hidden; clear: both; height: 7px; line-height: 1px; font-size: 1px;">
        </div>
    </div>
    <!-- #container -->--%>
</asp:Content>
