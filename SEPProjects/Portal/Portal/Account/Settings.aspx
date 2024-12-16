<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Portal.WebSite.Account.Settings" MasterPageFile="~/Default.Master"%>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
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
                                <tr><td height="40" colspan="2" class="btn"><a href="/Account/Password.aspx">修改密码</a>
                                <em>|</em> 头像 <em>|</em></td>
                                </tr>
                                <tr><td width="10%">
                                        <table width="90" border="0" cellpadding="2" cellspacing="1" bgcolor="#C9C9C9">
                                            <tr><td bgcolor="#FFFFFF"><img width="96" height="96" title="头像" id="imgUserIcon" runat="server" /></td></tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="90%" border="0" cellspacing="10" cellpadding="0">
                                            <tr>
                                                <td style="line-height: 150%;">
                                                    <span class="f12">上传新头像<br />
                                                    </span>支持.jpg .gif .png的图片，最大可上传2M大小的图片
                                                </td>
                                            </tr>
                                            <tr><td>
                                            <input name="profile_image" type="file" class="input"/>
                                            </td></tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Button ID="Button1" CssClass="enterbtn" Text="保 存" OnClick="Button1_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
