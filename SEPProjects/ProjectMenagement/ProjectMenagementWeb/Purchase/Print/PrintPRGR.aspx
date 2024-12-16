<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPRGR.aspx.cs" Inherits="FinanceWeb.Purchase.Print.PrintPRGR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="labPRGR" runat="server" />
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="20" align="center" valign="bottom" class="white_font">
                &nbsp;
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.close();">关闭</a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
