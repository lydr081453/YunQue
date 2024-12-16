<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollatePrint.aspx.cs" Inherits="Purchase_Requisition_Print_CollatePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
</head>
<body>
    <asp:Label runat="server" ID="labText" />
    <br />
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" id="bottomButton" runat="server" >
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
</body>
</html>
