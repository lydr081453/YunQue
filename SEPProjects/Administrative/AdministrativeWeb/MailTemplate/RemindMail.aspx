<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemindMail.aspx.cs" Inherits="AdministrativeWeb.MailTemplate.RemindMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
        <tr>
            <td>
                <asp:Image runat="server" ID="imgs" Width="680" Height="54" />
            </td>
        </tr>
        <tr>
            <td style="background-repeat: repeat-x; padding: 0 10px 20px 10px;">
                <strong><%=Sender %>提示您,<%=RemindText %><br />
                    <br />
                </strong>
                <br />
                <div id="divEnable" runat="server">
                    
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>月份</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>迟到30分钟内</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>迟到30分钟以上</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>半天未打卡</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>全天未打卡</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>上下班打卡记录不全</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>早退</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labMonth" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labLateCount1" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labLateCount2" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labAbsentCount1" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labAbsentCount2" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labAbsentCount3" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
