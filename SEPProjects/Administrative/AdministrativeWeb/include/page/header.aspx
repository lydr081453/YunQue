<%@ Page Language="c#" Inherits="FrameSite.Web.include.page.Header" CodeBehind="Header.aspx.cs" AutoEventWireup="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <link href="../../css/syshomepage.css" rel="stylesheet" type="text/css" />
</head>
<body class="topbar">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="50%" class="header_left"><img src="../../images/syshomepage/090407_04.jpg" width="138" height="31" hspace="35" /></td>
        <td width="50%" align="right" class="header_right" style="padding-right:20px;">
            <a target='_parent' href='../../SignOut.aspx'>注销</a>&nbsp;&nbsp;
            |&nbsp;&nbsp;<a target='modify' href='../../PSWChange/PSWChange.aspx'>更改密码</a>
        </td>
      </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td class="header_menuLeft" width="15%"><asp:Label ID="lblCaption" runat="server"></asp:Label></td>
        <td align="right" class="header_menuRight" width="85%">
        <img src="../../images/syshomepage/090407_11.jpg" width="10" height="10" hspace="3" /><a target='_parent' href='../../default.aspx'>首页</a>&nbsp;&nbsp;&nbsp;<img src="../../images/syshomepage/090407_11-13.jpg" width="10" height="10" hspace="4" /><a target='_top' href='ToHome.aspx'>其他系统</a></td>
      </tr>
    </table>
</body>
</html>
