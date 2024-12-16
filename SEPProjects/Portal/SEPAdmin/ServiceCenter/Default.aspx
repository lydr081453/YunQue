<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SEPAdmin.ServiceCenter.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="padding: 30px 0 80px 40px">
                <img src="/images/xingyan.png"/>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/staff-center-06.jpg">
        <tr>
            <td height="416" valign="top" style="background-repeat: no-repeat; background-position: top center;">
                <table width="716" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td><img src="images/staff-center-07_01.jpg" width="716" height="219" border="0" usemap="#Map" /></td></tr><tr><td height="197" align="right" valign="top" background="images/staff-center-07_02.jpg" style=" border:0;">
                            <div style="position: relative; left:540px; top: 10px;">
                                <marquee id="tdAlle" runat="server" behavior="scroll" direction="down" height="45"
                                    scrollamount="2" scrolldelay="100" width="100%" onmouseout="this.start()" onmouseover="this.stop()">
                                 &nbsp;
                                </marquee>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 1px solid #dededd;">
        <tr>
            <td height="38" align="center" background="images/staff-center-08.jpg" style="border-bottom: 5px solid #eeaf70;
                color: #717172; font-size: 12px;">
                Copyright ©2016-2024 XingYan. All rights reserved
            </td>
        </tr>
    </table>
    <map name="Map" id="Map">
        <area shape="poly" coords="443,75,522,75,565,147,523,217,443,216,401,149" href="ExitGuid.aspx" />
        <area shape="poly" coords="317,1,396,1,439,73,397,143,317,142,275,75" href="EntryGuid.aspx" />
        <area shape="poly" coords="192,74,271,74,314,146,272,216,192,215,150,148" href="Newcomer.aspx" />
    </map>
</body>
</html>
