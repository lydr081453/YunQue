<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="css/newstyle.css" rel="stylesheet" />
    <link href="../../css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="../../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/navBarstyle.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.4.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function aSelected(id, link) {

            document.getElementById("MainFrame").src = link;
            $(".selectA").each(function () {
                $(this).removeClass("selectA");
                $(this).addClass("outA");
            });

            $("#a" + id).removeClass("outA");
            $("#a" + id).addClass("selectA");
        }

        function showdiv(id) {
            $(".showSub").each(function () {
                $(this).removeClass("showSub");
                $(this).addClass("hideSub");
            });
            $("#sub" + id).removeClass("hideSub");
            $("#sub" + id).addClass("showSub");

            $(".menu-orag").each(function () {
                $(this).removeClass("menu-orag");
                $(this).addClass("menu-normal");
            });
            $("#parent" + id).removeClass("menu-normal");
            $("#parent" + id).addClass("menu-orag");
        }

        function iFrameHeight() {
            var ifm = document.getElementById("MainFrame");
            var subWeb = document.frames ? document.frames["MainFrame"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = subWeb.body.scrollHeight + 500;

            }
        }

        function Logout() {
            document.getElementById("lbSignOut").click();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="padding: 30px 0 25px 40px">&nbsp;</td>
                <td align="right" valign="top" style="padding-right: 30px;">
                    <table width="280" border="0" cellpadding="10" cellspacing="0" bgcolor="#f1f1f1">
                        <tr>
                            <td align="center"><strong>当前用户：<asp:Label ID="lblCaption" runat="server" /></strong></td>
                            <td><a href="../../PSWChange/PSWChange.aspx" target="modify">
                                <img src="/new_images/icon-key.gif" width="6" height="11" style="margin: 0 4px -2px 0;" />修改密码</a><a href="javascript:void(0);" onclick="Logout();"><img src="/new_images/icon-logout.gif" width="9" height="10" style="margin: 0 4px -1px 20px;" />注销</a><asp:LinkButton ID="lbSignOut" OnClick="ImageButton_Click" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/staff-center-03.jpg" style="background-repeat: repeat-x; background-position: left top;">
            <tr>
                <td background="/new_images/adm-tit.jpg" style="background-position: right top; background-repeat: no-repeat; padding: 0 30px 21px 30px;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="220" height="420" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="42" background="/new_images/staff-center-04_01.jpg" style="background-repeat: no-repeat; background-position: 5px 21px;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td background="/new_images/staff-center-04_02.jpg" style="background-repeat: repeat-y; background-position: 5px top;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" background="/new_images/staff-center-04_01.jpg" style="background-repeat: no-repeat; background-position: 5px -21px; padding-top: 15px;">
                                                        <table width="210" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody id="tb" runat="server"></tbody>
                                                        </table>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 5px;">
                                            <img src="/new_images/staff-center-04_03.jpg" width="213" height="36" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="padding: 0 0 20px 30px;">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="42">
                                                        <table border="0" cellpadding="0" cellspacing="0" id="topmenu">
                                                            <tr>
                                                                <td class="left">
                                                                    <img src="/new_images/icon-home.gif" width="10" height="10" /><a href="../../default.aspx" target="modify">首页</a></td>
                                                                <td class="right">
                                                                    <img src="/new_images/icon-change.gif" width="11" height="8" /><a href='/include/page/ToHome.aspx'>其他系统</a></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />


                                            <iframe frameborder="0" width="100%" height="100%" id="MainFrame" name="modify" scrolling="no" onload="iFrameHeight();" src="<%=WorkSpaceUrl %>"></iframe>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 1px solid #dededd;">
            <tr>
                <td height="38" align="center" background="/new_images/staff-center-08.jpg" style="border-bottom: 5px solid #eeaf70; color: #717172; font-size: 12px;">Copyright ©2016-2024 Xingyan. All rights reserved</td>
            </tr>
        </table>
    </form>
</body>
</html>
