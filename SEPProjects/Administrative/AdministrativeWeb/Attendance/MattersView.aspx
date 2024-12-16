<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MattersView.aspx.cs" MasterPageFile="~/Default.Master"
    Inherits="AdministrativeWeb.Attendance.MattersView" %>
<%@ OutputCache Duration="1" Location="none" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />

<script src="../../js/DatePickerChange.js" type="text/javascript"></script>
<asp:HiddenField ID="hidLeaveID" runat="server" />
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="17" height="20">
                        <strong>
                            <img src="<%=ESP.Configuration.ConfigurationManager.SafeAppSettings["AdminSite"] %>/images/090407_12-35.jpg" width="17" height="20" hspace="5" vspace="5" /></strong>
                    </td>
                    <td align="left">
                        <strong>事由查看 </strong>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="10" cellspacing="0" style="background-color: Gray">
                            <tr>
                                <td bgcolor="#F2F2F2" width="15%" align="left">
                                    姓名：
                                </td>
                                <td bgcolor="#F2F2F2" width="30%" align="left">
                                    <asp:HiddenField ID="hidUserid" runat="server" />
                                    <asp:Label ID="txtUserName" runat="server" Width="100px" Height="14px"></asp:Label>
                                </td>
                                <td bgcolor="#F2F2F2" width="15%" align="left">
                                    员工编号：
                                </td>
                                <td bgcolor="#F2F2F2" width="40%" align="left">
                                    <asp:Label ID="txtUserCode" runat="server" Width="100px" Height="14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White">
                                    业务团队：
                                </td>
                                <td align="left" bgcolor="White">
                                    <asp:Label ID="txtTeam" runat="server" Width="100px" Height="14px"></asp:Label>
                                </td>
                                <td align="left" bgcolor="White">
                                    组别：
                                </td>
                                <td align="left" bgcolor="White">
                                    <asp:Label ID="txtGroup" runat="server" Width="100px" Height="14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2" width="15%" align="left">
                                    申请时间：
                                </td>
                                <td bgcolor="#F2F2F2" width="30%" align="left">
                                    <asp:Label ID="labAppTime" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#F2F2F2" width="15%" align="left">
                                    申请事由：
                                </td>
                                <td bgcolor="#F2F2F2" width="40%" align="left">
                                    <asp:Label ID="labAppMatter" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="White" width="15%">
                                    事由时间：
                                </td>
                                <td align="left" bgcolor="White" width="30%" >
                                    <asp:Label ID="labMatterTimeInfo" runat="server"></asp:Label>
                                </td>
                                <td align="left" bgcolor="White" width="15%">
                                    项目号：
                                </td>
                                <td align="left" bgcolor="White" width="40%">
                                    <asp:Label ID="labProjectNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" bgcolor="#F2F2F2">
                                    事由原因：
                                </td>
                                <td bgcolor="#F2F2F2" width="45%" colspan="2">
                                    <asp:Label ID="txtLeaveCause" Width="500px" Height="50px" runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2" width="40%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="20" style="background-image: url(../../images/headerbg_left.jpg); background-repeat: no-repeat;
                                    background-position: center left;">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr><td colspan="2">&nbsp;</td></tr>
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
</asp:Content>