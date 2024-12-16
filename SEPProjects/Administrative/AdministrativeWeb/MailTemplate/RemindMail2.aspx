<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemindMail2.aspx.cs" Inherits="AdministrativeWeb.MailTemplate.RemindMail2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="700" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
        <tr>
            <td>
                <asp:Image runat="server" ID="imgs" Width="680" Height="54" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><%=Sender %>提醒您,<%=RemindText %><br />
                    <br />
                </strong>
                <br />
                <div id="divEnable" runat="server">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                        <tr>
                            <td colspan="15" height="25">
                                <asp:Label ID="labTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ececec">迟到</td>
                            <td align="center" bgcolor="#ececec">早退</td>
                            <td align="center" bgcolor="#ececec">旷工</td>
                            <td align="center" bgcolor="#ececec">工作日OT</td>
                            <td align="center" bgcolor="#ececec">节假日OT</td>
                            <td align="center" bgcolor="#ececec">出差</td>
                            <td align="center" bgcolor="#ececec">外出</td>
                            <td align="center" bgcolor="#ececec">病假</td>
                            <td align="center" bgcolor="#ececec">事假</td>
                            <td align="center" bgcolor="#ececec">年假</td>
                            <td align="center" bgcolor="#ececec">婚假</td>
                            <td align="center" bgcolor="#ececec">丧假</td>
                            <td align="center" bgcolor="#ececec">产假</td>
                            <td align="center" bgcolor="#ececec">产检</td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLate" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAbsent" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labHolidayOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEvection" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEgress" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labSickLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAffiairLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAnnualLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMarriageLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labFuneralLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMaternityLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPrenatalCheck" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labIncentive" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
