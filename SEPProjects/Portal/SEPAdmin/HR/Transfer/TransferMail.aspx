<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferMail.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                    <asp:Label ID="lblMessage" runat="server" Font-Size="12px" /><br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>姓名</strong>
                            </td>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>编号</strong>
                            </td>
                            <td height="28" align="center" bgcolor="#F4F4F4">
                                <strong>原部门</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>原职务</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>新部门</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>新职务</strong>
                            </td>
                            <td align="center" bgcolor="#F4F4F4">
                                <strong>转组日期</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblName" runat="server" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblCode" runat="server" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblOldDept" runat="server" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblOldPosition" runat="server" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblNewDept" runat="server" />
                            </td>
                             <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblNewPosition" runat="server" />
                            </td>
                             <td align="center" bgcolor="#FFFFFF">
                                <asp:Label ID="lblTransDate" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
