<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionCertification.aspx.cs"
    Inherits="SEPAdmin.HR.Dimission.DimissionCertification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>离职证明</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script type="text/javascript" src="/public/js/syscomm.js"></script>

    <script type="text/javascript" src="/public/highslide/highslide-with-html.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />

    <script type="text/javascript">
        hs.graphicsDir = 'public/highslide/graphics/';
        hs.outlineType = 'rounded-white';
        hs.outlineWhileAnimating = true;
    </script>

    <style type="text/css">
        .highslide-html
        {
            background-color: white;
        }
        .highslide-html-blur
        {
        }
        .highslide-html-content
        {
            position: absolute;
            display: none;
        }
        .highslide-loading
        {
            display: block;
            color: black;
            font-size: 8pt;
            font-family: sans-serif;
            font-weight: bold;
            text-decoration: none;
            padding: 2px;
            border: 1px solid black;
            background-color: white;
            padding-left: 22px;
            background-image: url(/public/highslide/graphics/loader.white.gif);
            background-repeat: no-repeat;
            background-position: 3px 1px;
        }
        a.highslide-credits, a.highslide-credits i
        {
            padding: 2px;
            color: silver;
            text-decoration: none;
            font-size: 10px;
        }
        a.highslide-credits:hover, a.highslide-credits:hover i
        {
            color: white;
            background-color: gray;
        }
        /* Styles for the popup */.highslide-wrapper
        {
            background-color: white;
        }
        .highslide-wrapper .highslide-html-content
        {
            width: 400px;
            padding: 5px;
        }
        .highslide-wrapper .highslide-header div
        {
        }
        .highslide-wrapper .highslide-header ul
        {
            margin: 0;
            padding: 0;
            text-align: right;
        }
        .highslide-wrapper .highslide-header ul li
        {
            display: inline;
            padding-left: 1em;
        }
        .highslide-wrapper .highslide-header ul li.highslide-previous, .highslide-wrapper .highslide-header ul li.highslide-next
        {
            display: none;
        }
        .highslide-wrapper .highslide-header a
        {
            font-weight: bold;
            color: gray;
            text-transform: uppercase;
            text-decoration: none;
        }
        .highslide-wrapper .highslide-header a:hover
        {
            color: black;
        }
        .highslide-wrapper .highslide-header .highslide-move a
        {
            cursor: move;
        }
        .highslide-wrapper .highslide-footer
        {
            height: 11px;
        }
        .highslide-wrapper .highslide-footer .highslide-resize
        {
            float: right;
            height: 11px;
            width: 11px;
            background: url(/public/highslide/graphics/resize.gif);
        }
        .highslide-wrapper .highslide-body
        {
        }
        .highslide-move
        {
            cursor: move;
        }
        .highslide-resize
        {
            cursor: nw-resize;
        }
        /* These must be the last of the Highslide rules */.highslide-display-block
        {
            display: block;
        }
        .highslide-display-none
        {
            display: none;
        }
        .tableBorder
        {
            border-collapse: collapse;
            border: solid 1px black;
        }
        .tdBorder
        {
            border-collapse: collapse;
            border: solid 1px black;
        }
        .white_font
        {
            font-size: 12px;
            color: #FFF;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 5px;
            background-repeat: no-repeat;
        }
        .white_font a:link
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_font a:visited
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_font a:hover
        {
            color: #FFF;
            text-decoration: underline;
        }
    </style>
</head>
<body onload="javascript:window.location.href='#top_A';">
    <form id="frmMain" runat="server">
    <table style="width:1000; margin:20px 0 0 60px;" align="center" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td align="center">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top">
                            <img src="../../Images/xingyan.png" alt="xingyan" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="font-weight: bold; font-size: 20px; height: 30px;">
                            <br />
                            离 职 证 明<br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px">
                            <span style="text-decoration: underline; margin: 0 5px 0px 30px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labUserName" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            （身份证号码：<asp:Label ID="labIdCard" runat="server" />）于 
                            <span style="text-decoration: underline;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labJoinDay" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            至<br /><p />
                            <span style="text-decoration: underline;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labLastDay" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            在我公司工作，已正式解除劳动合同关系。<asp:Label ID="labPositionTitle" runat="server" Text ="离职时最终职位为" /><br /><p />
                            <span style="text-decoration: underline;"><asp:Label ID="labPosition" runat="server" /></span>。<br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px; height: 100px" valign="bottom">
                            <span style="margin: 0 0 0 200px;">单位名称：<asp:Label ID="labCompanyName1" runat="server" />
                                <br />
                            </span><span style="margin: 0 0 0 200px;">日期：<asp:Label ID="labCurDay" runat="server" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <hr style="border-top: 1px dashed black; height: 1px" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="font-weight: bold; font-size: 20px">
                            离 职 证 明<br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px">
                            <span style="text-decoration: underline; margin: 0 5px 0px 30px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labUserName2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            （身份证号码：<asp:Label ID="labIdCard2" runat="server" />）于 
                            <span style="text-decoration: underline;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labJoinDay2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                            至<br /><p />
                            <span style="text-decoration: underline;">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labLastDay2" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            在我公司工作，已正式解除劳动合同关系。<asp:Label ID="labPositionTitle2" runat="server" Text ="离职时最终职位为" /><br /><p />
                            <span style="text-decoration: underline;"><asp:Label ID="labPosition2" runat="server" /></span>。<br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px; height: 100px" valign="bottom">
                            <span style="margin: 0 0 0 200px;">单位名称：<asp:Label ID="labCompanyName2" runat="server" />
                                <br />
                            </span><span style="margin: 0 0 0 200px;">日期：
                                <asp:Label ID="labCurDay2" runat="server" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px">
                            <br />
                            <br />
                            本人签收：___________
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 10px; color: Gray" align="center">
                            <br />
                            <br />
                            北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
