<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Portal.WebSite.Account.Settings" MasterPageFile="~/Default.Master"%>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td class="nav">
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td class="title">����</td></tr>
                </table>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td width="30%" valign="top" class="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" align="right">��������</td></tr>
                                <tr><td height="40" align="right"><%--<a href="/Account/IM.aspx">������</a>--%></td></tr>
                            </table>
                        </td>
                        <td width="70%" valign="top" class="right">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" colspan="2" class="btn"><a href="/Account/Password.aspx">�޸�����</a>
                                <em>|</em> ͷ�� <em>|</em></td>
                                </tr>
                                <tr><td width="10%">
                                        <table width="90" border="0" cellpadding="2" cellspacing="1" bgcolor="#C9C9C9">
                                            <tr><td bgcolor="#FFFFFF"><img width="96" height="96" title="ͷ��" id="imgUserIcon" runat="server" /></td></tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="90%" border="0" cellspacing="10" cellpadding="0">
                                            <tr>
                                                <td style="line-height: 150%;">
                                                    <span class="f12">�ϴ���ͷ��<br />
                                                    </span>֧��.jpg .gif .png��ͼƬ�������ϴ�2M��С��ͼƬ
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
                                    <td><asp:Button ID="Button1" CssClass="enterbtn" Text="�� ��" OnClick="Button1_Click" runat="server" />
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
