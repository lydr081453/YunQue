<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MattersMail.aspx.cs" Inherits="AdministrativeWeb.MailTemplate.MattersMail" %>

<%@ OutputCache Duration="1" Location="none" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <link href="<%=ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] %>/css/a.css" rel="stylesheet" type="text/css" />
    <link href="<%=ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] %>/css/style.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        body
        {
            background-color: #ececec;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #585858;
            line-height: 170%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <asp:HiddenField ID="hidLeaveID" runat="server" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="17" height="20">
                            <strong>
                                <img src="<%=ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] %>/images/090407_12-35.jpg"
                                    width="17" height="20" hspace="5" vspace="5" /></strong>
                        </td>
                        <td align="left">
                            <strong>���ɲ鿴 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <table width="100%" border="0" cellpadding="10" cellspacing="0" style="background-color: Gray">
                                <tr>
                                    <td bgcolor="#F2F2F2" width="15%" align="left">
                                        ������
                                    </td>
                                    <td bgcolor="#F2F2F2" width="30%" align="left">
                                        <asp:HiddenField ID="hidUserid" runat="server" />
                                        <asp:Label ID="txtUserName" runat="server" Width="100px" Height="14px"></asp:Label>
                                    </td>
                                    <td bgcolor="#F2F2F2" width="15%" align="left">
                                        Ա����ţ�
                                    </td>
                                    <td bgcolor="#F2F2F2" width="40%" align="left">
                                        <asp:Label ID="txtUserCode" runat="server" Width="100px" Height="14px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White">
                                        ҵ���Ŷӣ�
                                    </td>
                                    <td align="left" bgcolor="White">
                                        <asp:Label ID="txtTeam" runat="server" Width="100px" Height="14px"></asp:Label>
                                    </td>
                                    <td align="left" bgcolor="White">
                                        ���
                                    </td>
                                    <td align="left" bgcolor="White">
                                        <asp:Label ID="txtGroup" runat="server" Width="100px" Height="14px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F2F2" width="15%" align="left">
                                        ����ʱ�䣺
                                    </td>
                                    <td bgcolor="#F2F2F2" width="30%" align="left">
                                        <asp:Label ID="labAppTime" runat="server"></asp:Label>
                                    </td>
                                    <td bgcolor="#F2F2F2" width="15%" align="left">
                                        �������ɣ�
                                    </td>
                                    <td bgcolor="#F2F2F2" width="40%" align="left">
                                        <asp:Label ID="labAppMatter" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="White" width="15%">
                                        ����ʱ�䣺
                                    </td>
                                    <td align="left" bgcolor="White" width="30%">
                                        <asp:Label ID="labMatterTimeInfo" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" bgcolor="White" width="15%">
                                        ��Ŀ�ţ�
                                    </td>
                                    <td align="left" bgcolor="White" width="40%">
                                        <asp:Label ID="labProjectNo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" bgcolor="#F2F2F2">
                                        ����ԭ��
                                    </td>
                                    <td bgcolor="#F2F2F2" width="45%" colspan="2">
                                        <asp:Label ID="txtLeaveCause" Width="500px" Height="50px" runat="server" />
                                    </td>
                                    <td bgcolor="#F2F2F2" width="40%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                                <tr>
                                    <td colspan="2">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnLeaveBack" Text="<img src='../../images/t2_03-22.jpg'/>" runat="server"
                                            Width="56" Height="24" OnClick="btnLeaveBack_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
