<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCardMail.aspx.cs" Inherits="AdministrativeWeb.MailTemplate.UserCardMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <strong>您好,<br />
                    请操作下列门卡权限：<br />
                </strong>
                <br />
                <div id="divEnable" runat="server" visible="false">
                    启用门卡
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>姓名</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>组别</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>员工编号</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>门禁卡号</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <%=UserName %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CompanyName%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=UserCode %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CardNoInfo %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divUnEnable" runat="server" visible="false">
                    停用门卡
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>姓名</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>组别</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>员工编号</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>门禁卡号</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <%=UserName %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CompanyName%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=UserCode %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CardNoInfo %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divChange" runat="server" visible="false">
                    更换门卡
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>姓名</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>组别</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>员工编号</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>门禁卡号</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <%=UserName %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CompanyName%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=UserCode %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%=CardNoInfo %>
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
