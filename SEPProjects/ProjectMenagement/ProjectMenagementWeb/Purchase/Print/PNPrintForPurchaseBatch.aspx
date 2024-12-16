<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PNPrintForPurchaseBatch.aspx.cs"
    Inherits="Purchase_Print_PNPrintForPurchaseBatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>付款确认</title>
    <style type="text/css">
        body
        {
            background-image: url(images/body_bg.jpg);
            background-repeat: no-repeat;
            background-position: center top;
            margin: 0px;
            padding: 0px;
        }
        /* -----------top--------- */.toplink
        {
            font-size: 14px;
            color: #000;
        }
        .toplink a:link
        {
            color: #000;
            text-decoration: none;
        }
        .toplink a:visited
        {
            color: #000;
            text-decoration: none;
        }
        .toplink a:hover
        {
            color: #000;
            text-decoration: underline;
        }
        .time
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #000000;
        }
        .font_12px_blue
        {
            font-size: 12px;
            color: #7282a9;
            line-height: 150%;
        }
        .font
        {
            font-size: 12px;
            color: #333;
            font-weight: bold;
        }
        /* -----------left_menu--------- */.left_menu
        {
            font-size: 12px;
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:link
        {
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:visited
        {
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:hover
        {
            color: #333;
            text-decoration: underline;
        }
        /* -----------内容--------- */.list_title
        {
            background-image: url(images/blue_inside.gif);
            background-repeat: no-repeat;
            font-size: 12px;
            color: #7282a9;
            font-weight: bold;
        }
        .list_title2
        {
            font-size: 12px;
            color: #7282a9;
            font-weight: bold;
        }
        .list_nav
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #333;
            padding: 10px 0 10px 0;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #eaedf1;
        }
        .f_16px
        {
            font-size: 16px;
            color: #333;
            font-weight: bold;
        }
        .f_12px
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
        }
        .f_12px_bold
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            font-weight: bold;
        }
        .f_blue
        {
            font-size: 12px;
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:link
        {
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:visited
        {
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:hover
        {
            color: #4d568c;
            text-decoration: underline;
        }
        /* -----------btn--------- */.white_bold_font
        {
            font-size: 12px;
            color: #FFF;
            font-weight: bold;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 7px;
        }
        .white_bold_font a:link
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_bold_font a:visited
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_bold_font a:hover
        {
            color: #FFF;
            text-decoration: underline;
        }
        /* -----------btn_翻页--------- */.white_font
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
        /* -----------申请单查看样式表--------- */.title
        {
            font-size: 14px;
            color: #333333;
            font-weight: bold;
            padding: 0 0 0 10px;
        }
        .f_gray_left
        {
            font-size: 12px;
            color: #6e6e6e;
            font-weight: bold;
            padding: 0 0 0 10px;
        }
        .f_gray_right
        {
            font-size: 12px;
            color: #6e6e6e;
            padding: 0 0 0 10px;
        }
        .f_gray_left_withoutPadding
        {
            font-size: 12px;
            color: #6e6e6e;
            font-weight: bold;
        }
        .f_gray_right_withoutPadding
        {
            font-size: 12px;
            color: #6e6e6e;
        }
        .input_btn
        {
            background-repeat: no-repeat;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: inset;
            border-left-style: none;
            background-color: #90a0cb;
            border-bottom-width: 10px;
            border-bottom-color: #9faac5;
            color: #FFFFFF;
        }
        /* -----------po&pr--------- */.f_white14_Bold
        {
            font-size: 14px;
            font-weight: bold;
            color: #FFF;
            padding: 0 0 0 3px;
        }
        .f12pxGgray_left
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
        }
        .f12pxGgray_bottom
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
        }
        .f12pxGgray_oneline
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
        }
        .f12pxGgray_blank
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
        }
        .f12pxGgray_newright
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
        }
        .f12pxGgrayBold
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
        }
        .test_title_left
        {
            font: Verdana, Arial, Helvetica, sans-serif;
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            padding: 0 0 0 3px;
            font-size: 12px;
        }
        .test_title_middle
        {
            font: Verdana, Arial, Helvetica, sans-serif;
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
            padding: 0 0 0 3px;
            font-size: 12px;
        }
        .title_right
        {
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            padding: 0 0 0 3px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .f12pxGgray_right_2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border: 1px solid #a5c2a5;
        }
        .f12pxGgrayBold_2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
        }
        .test_title
        {
            font-size: 12px;
            color: #000;
            font-weight: bold;
        }
        .widebuttons
        {
            font-size: 8pt;
            height: 19px;
            cursor: hand;
            color: #000;
            background-color: #cdcdbe;
            background-image: url("../../images/btnBack.gif");
            background-repeat: repeat-x;
            border: 1px solid #4f556a;
            border-spacing: 0px;
            letter-spacing: normal;
        }
    </style>
    <style type="text/css">
        body
        {
            background-image: url(images/body_bg.jpg);
            background-repeat: no-repeat;
            background-position: center top;
            margin: 0px;
            padding: 0px;
        }
        /* -----------top--------- */.toplink
        {
            font-size: 14px;
            color: #000;
        }
        .toplink a:link
        {
            color: #000;
            text-decoration: none;
        }
        .toplink a:visited
        {
            color: #000;
            text-decoration: none;
        }
        .toplink a:hover
        {
            color: #000;
            text-decoration: underline;
        }
        .time
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #000000;
        }
        .font_12px_blue
        {
            font-size: 12px;
            color: #7282a9;
            line-height: 150%;
        }
        .font
        {
            font-size: 12px;
            color: #333;
            font-weight: bold;
        }
        /* -----------left_menu--------- */.left_menu
        {
            font-size: 12px;
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:link
        {
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:visited
        {
            color: #666;
            text-decoration: underline;
        }
        .left_menu a:hover
        {
            color: #333;
            text-decoration: underline;
        }
        /* -----------内容--------- */.list_title
        {
            background-image: url(images/blue_inside.gif);
            background-repeat: no-repeat;
            font-size: 12px;
            color: #7282a9;
            font-weight: bold;
        }
        .list_title2
        {
            font-size: 12px;
            color: #7282a9;
            font-weight: bold;
        }
        .list_nav
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #333;
            padding: 10px 0 10px 0;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #eaedf1;
        }
        .f_16px
        {
            font-size: 16px;
            color: #333;
            font-weight: bold;
        }
        .f_12px
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
        }
        .f_12px_bold
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            font-weight: bold;
        }
        .f_blue
        {
            font-size: 12px;
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:link
        {
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:visited
        {
            color: #4d568c;
            text-decoration: none;
        }
        .f_blue a:hover
        {
            color: #4d568c;
            text-decoration: underline;
        }
        /* -----------btn--------- */.white_bold_font
        {
            font-size: 12px;
            color: #FFF;
            font-weight: bold;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 7px;
        }
        .white_bold_font a:link
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_bold_font a:visited
        {
            color: #FFF;
            text-decoration: none;
        }
        .white_bold_font a:hover
        {
            color: #FFF;
            text-decoration: underline;
        }
        /* -----------btn_翻页--------- */.white_font
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
        /* -----------申请单查看样式表--------- */.title
        {
            font-size: 14px;
            color: #333333;
            font-weight: bold;
            padding: 0 0 0 10px;
        }
        .f_gray_left
        {
            font-size: 12px;
            color: #6e6e6e;
            font-weight: bold;
            padding: 0 0 0 10px;
        }
        .f_gray_right
        {
            font-size: 12px;
            color: #6e6e6e;
            padding: 0 0 0 10px;
        }
        .f_gray_left_withoutPadding
        {
            font-size: 12px;
            color: #6e6e6e;
            font-weight: bold;
        }
        .f_gray_right_withoutPadding
        {
            font-size: 12px;
            color: #6e6e6e;
        }
        .input_btn
        {
            background-repeat: no-repeat;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: inset;
            border-left-style: none;
            background-color: #90a0cb;
            border-bottom-width: 10px;
            border-bottom-color: #9faac5;
            color: #FFFFFF;
        }
        /* -----------po&pr--------- */.f_white14_Bold
        {
            font-size: 14px;
            font-weight: bold;
            color: #FFF;
            padding: 0 0 0 3px;
        }
        .f12pxGgray
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
        }
        .f12pxGgray_right
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
        }
        .f12pxGgrayBold
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
        }
        .test_title_left
        {
            font: Verdana, Arial, Helvetica, sans-serif;
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            padding: 0 0 0 3px;
            font-size: 12px;
        }
        .test_title_right
        {
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border: 1px solid #a5c2a5;
            padding: 0 0 0 3px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .f12pxGgray_right_2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border: 1px solid #a5c2a5;
        }
        .f12pxGgrayBold_2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
        }
        .test_title
        {
            font-size: 12px;
            color: #a5c2a5;
            font-weight: bold;
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td colspan="3" height="40" valign="bottom" style="padding: 0 0 5px 0">
                <asp:Image runat="server" ID="logoImg" Width="126" Height="70" />
            </td>
            <td align="center" colspan="5" valign="middle" style="padding: 0 0 5px 0; font-size: 24px;">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <span style="color: Black; font-size: 12px;">批次号:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblPurchaseBatchCode" />&nbsp;
            </td>
            <td align="right" colspan="4">
                <span style="color: Black; font-size: 12px;">银行凭证号:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblBatchCode" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="110px" height="20" align="center" class="test_title_left" style="font-size: 12px;">
                项目号<br />
                费用明细描述
            </td>
            <td width="90px" align="center" class="test_title_middle" style="font-size: 12px;">
                费用发生日期
            </td>
            <td width="50px" align="center" class="test_title_middle" style="font-size: 12px;">
                申请人
            </td>
            <td width="60px" align="center" class="test_title_middle" style="font-size: 12px;">
                员工编号
            </td>
            <td width="90px" align="center" class="test_title_middle" style="font-size: 12px;">
                费用所属组
            </td>
            <td width="80px" align="center" class="test_title_middle" style="font-size: 12px;">
                申请金额
            </td>
            <td width="75px" align="center" class="test_title_middle" style="font-size: 12px;">
                订单号
            </td>
            <td width="75px" align="center" class="title_right" style="font-size: 12px;">
                PN号
            </td>
        </tr>
        <asp:Repeater ID="repList" runat="server" OnItemDataBound="repList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="left" height="20px" class="f12pxGgray_left" style="font-size: 10px;">
                        <asp:Label ID="labProjectCode" runat="server" />&nbsp;
                    </td>
                    <td align="center" style="font-size: 12px;">
                        <asp:Label ID="labReturnFactDate" runat="server" />&nbsp;
                    </td>
                    <td align="center" style="font-size: 12px;">
                        <asp:Label ID="labRequestorUserName" runat="server" />&nbsp;
                    </td>
                    <td align="center" style="font-size: 12px;">
                        <asp:Label ID="labRequestorID" runat="server" />&nbsp;&nbsp;
                    </td>
                    <td align="center" style="font-size: 12px;">
                        <asp:Label ID="labDepartment" runat="server" />&nbsp;
                    </td>
                    <td align="right" style="font-size: 12px;">
                        <asp:Label ID="labPreFee" runat="server" />&nbsp;
                    </td>
                    <td align="right" style="font-size: 12px;">
                        <asp:Label ID="labOrderid" runat="server" />&nbsp;
                    </td>
                    <td align="center" class="f12pxGgray_newright" style="font-size: 12px;">
                        <asp:Label ID="labPNno" runat="server" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8" class="f12pxGgray_oneline" style="font-size: 12px;">
                        <asp:Label ID="labReturnContent" runat="server" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8" class="f12pxGgray_blank" style="font-size: 12px;">
                        &nbsp;
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                小计(按供应商）
            </td>
            <td class="f12pxGgray_right" align="right" style="font-size: 12px; background-color: #66ff99;"
                colspan="2">
                <asp:Label ID="lab_TotalPrice" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                公司名称
            </td>
            <td class="f12pxGgray" align="right" colspan="2" style="font-size: 12px; background-color: #66ff99;">
                <asp:Label ID="labAccountName" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                开户行名称
            </td>
            <td class="f12pxGgray" align="right" colspan="2" style="font-size: 12px; background-color: #66ff99;">
                <asp:Label ID="labAccountBankName" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                银行帐号
            </td>
            <td class="f12pxGgray" align="right" colspan="2" style="font-size: 12px; background-color: #66ff99;">
                <asp:Label ID="labAccountBankNo" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td colspan="6" width="100%" align="left">
                <asp:Label runat="server" ID="lblAuditList" Font-Size="XX-Small"></asp:Label>
            </td>
        </tr>
    </table>
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
    <table>
        <tr>
            <td height="50">
            </td>
        </tr>
    </table>
    <asp:Repeater ID="repPRGR" runat="server" OnItemDataBound="repPRGR_ItemDataBound">
        <ItemTemplate>
            <asp:Repeater ID="repPR" runat="server" OnItemDataBound="repPR_ItemDataBound">
                <ItemTemplate>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="80" valign="bottom" style="padding: 0 0 5px 0">
                                <img src="images/pr_.gif" width="218" height="32" />
                            </td>
                            <td align="right" valign="bottom" style="padding: 0 0 5px 0">
                                <img src="images/xingyan.png" width="63" height="35" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="20" class="f_12px">
                                <strong>流水号<asp:Label ID="labglideNo" runat="server" /><strong><br />
                                    <asp:Label ID="laboldprinfo" runat="server"></asp:Label>
                            </td>
                            <td width="34" align="right" class="f_12px">
                                &nbsp;
                            </td>
                            <td width="280" align="right" class="f_12px">
                                <strong>申请单号<asp:Label ID="labPrno" runat="server" /></strong><br />
                                <strong>
                                    <asp:Label ID="lblPN" runat="server" /><strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="f_12px">
                                &nbsp;
                            </td>
                            <td align="right" class="f_12px">
                                &nbsp;
                            </td>
                            <td align="right" class="f_12px">
                                <strong>流向:<asp:Label ID="labLX" runat="server" /></strong><br />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="2" class="test_title">
                                供应商
                            </td>
                            <td colspan="2" class="test_title">
                                采购方
                            </td>
                        </tr>
                        <tr>
                            <td width="65" height="20" class="f_12px_bold">
                                名称
                            </td>
                            <td width="259" class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_name" runat="server" />&nbsp;
                            </td>
                            <td width="65" class="f_12px_bold">
                                名称
                            </td>
                            <td width="259" class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_DepartmentName" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                地址
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_address" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                地址
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_Address" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                联系人
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_linkman" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                联系人
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_contect_Name" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                联系电话
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_phone" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                联系电话
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_Telephone" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                传真
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_fax" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                电子邮件
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_EMail" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                电子邮件
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_email" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                &nbsp;
                            </td>
                            <td class="f_12px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                来源
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_source" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                &nbsp;
                            </td>
                            <td class="f_12px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="62" height="20" class="f_12px_bold">
                                送货至
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_ship_address" runat="server" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="2" class="test_title_right" style="font-size: 14px;">
                                押金
                            </td>
                            <td height="20" colspan="6" class="test_title_right" style="font-size: 14px;">
                                <asp:Label runat="server" ID="lblYaJin"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="8" class="test_title_right" style="font-size: 14px;">
                                采购内容
                            </td>
                        </tr>
                        <tr>
                            <td width="40px" height="20" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                序号
                            </td>
                            <td width="160px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                采购物品
                            </td>
                            <td width="140px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                收货时间
                            </td>
                            <td width="60px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                单价
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                货币
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                单位
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                数量
                            </td>
                            <td width="80px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>小计</strong>
                            </td>
                        </tr>
                        <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td height="30px" align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <asp:Label ID="labNum" runat="server" />
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("Item_No")%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("intend_receipt_date")%>&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray" style="font-size: 12px;">
                                        <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.00")%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <asp:Label ID="lab_rep_moneytype" runat="server" />&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("uom")%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# decimal.Parse(Eval("quantity").ToString()).ToString("#,##0.00")%>&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray_right" style="font-size: 12px;">
                                        <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.00")%>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" height="20px" class="f12pxGgrayBold" style="font-size: 12px;">
                                        描述
                                    </td>
                                    <td class="f12pxGgray_right" colspan="7" style="font-size: 12px;">
                                        <%# Eval("desctiprtion")%>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="630px" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td width="40px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                工作<br />
                                描述
                            </td>
                            <td align="center" width="450px" class="f12pxGgray" style="font-size: 12px;">
                                <asp:Label ID="lab_sow" runat="server" />&nbsp;
                            </td>
                            <%--<td rowspan="2" align="center" width="40px" class="f12pxGgray" style=" font-size:12px;"><strong>支付<br />条款</strong></td>
    <td height="20px" align="center" class="f12pxGgray" width="60px" style=" font-size:12px;"><strong>预付款</strong></td>
    <td align="right" class="f12pxGgray" style=" font-size:12px;" width="100px"><asp:Label ID="lab_sow4" runat="server" />&nbsp;</td>--%>
                            <td align="center" width="40px" class="f12pxGgray" style="font-size: 12px;">
                                <strong>总计</strong>
                            </td>
                            <td align="right" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="lab_moneytype" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <%--<tr>
    <td height="20px" align="center" width="60px" class="f12pxGgray" style=" font-size:12px;"><strong>其他条款</strong></td>
    <td class="f12pxGgray" style=" font-size:12px;" width="100px"><asp:Label ID="lab_payment_terms" runat="server" />&nbsp;</td>
  </tr>--%>
                        <tr>
                            <td height="20px" width="40px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                备注
                            </td>
                            <td colspan="3" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="lab_sow3" runat="server" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="5">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="8" class="test_title_right">
                                申请信息
                            </td>
                        </tr>
                        <tr>
                            <td width="43" height="20" align="center" class="f12pxGgrayBold">
                                申请人
                            </td>
                            <td width="75" align="center" class="f12pxGgrayBold">
                                申请日期
                            </td>
                            <td width="105" align="center" class="f12pxGgrayBold">
                                联络
                            </td>
                            <td width="75" align="center" class="f12pxGgrayBold">
                                业务组别
                            </td>
                            <td width="43" align="center" class="f12pxGgrayBold">
                                使用人
                            </td>
                            <td width="105" align="center" class="f12pxGgrayBold">
                                联络
                            </td>
                            <td width="43" align="center" class="f12pxGgrayBold">
                                收货人
                            </td>
                            <td width="105" align="center" class="f12pxGgray_right">
                                <strong>联络</strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="center" class="f12pxGgray">
                                <asp:Label ID="lab_requestorname" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_app_date" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_requestor_info" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_requestor_group" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_endusername" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_enduser_info" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="lab_receivername2" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="lab_receiver_info2" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="20px" class="f12pxGgrayBold" style="white-space: nowrap">
                                附加收货人:
                            </td>
                            <td align="left" colspan="6" height="20px" class="f12pxGgray_right">
                                <asp:Label ID="lab_AppendUser" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="20px" class="f12pxGgrayBold" style="white-space: nowrap">
                                收货人其他联络方式:
                            </td>
                            <td align="center" colspan="6" height="20px" class="f12pxGgray_right">
                                <asp:Label ID="lab_OtherInfo" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" colspan="2" align="center" class="f12pxGgray">
                                <strong>项目号</strong>
                            </td>
                            <td height="20" colspan="4" align="center" class="f12pxGgray">
                                <strong>项目描述</strong>
                            </td>
                            <td colspan="2" align="center" class="f12pxGgray_right">
                                <strong>预算金额 </strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" colspan="2" align="center" class="f12pxGgray">
                                <asp:Label ID="lab_project_code" runat="server" />&nbsp;
                            </td>
                            <td height="20" colspan="4" align="center" class="f12pxGgray">
                                <asp:Label ID="lab_project_descripttion" runat="server" />&nbsp;
                            </td>
                            <td colspan="2" align="right" class="f12pxGgray_right">
                                <asp:Label ID="lab_buggeted" runat="server" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="10" class="test_title_right" style="font-size: 14px;">
                                支付条款
                            </td>
                        </tr>
                        <tr>
                            <td width="30px" height="20" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                序号
                            </td>
                            <td width="55px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                账期类型
                            </td>
                            <td width="80px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                账期</br>基准点
                            </td>
                            <td width="45px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                账期
                            </td>
                            <td width="45px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                日期</br>类型
                            </td>
                            <td width="100px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                预计支</br>付时间
                            </td>
                            <td width="70px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                预计支</br>付金额
                            </td>
                            <td width="65px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                预计支付</br>百分比
                            </td>
                            <td width="65px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                剩余</br>金额
                            </td>
                            <td width="75px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>备注</strong>
                            </td>
                        </tr>
                        <asp:Repeater ID="repPeriod" runat="server" OnItemDataBound="repPeriod_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td height="20px" align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <asp:Label ID="labNum" runat="server" />
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# ESP.Purchase.Common.State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# ESP.Purchase.Common.State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("periodDay")%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# ESP.Purchase.Common.State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# (DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")) %>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("expectPaymentPercent") + "%"%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <asp:Literal ID="LitOverplusPrice" runat="server" />&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray_right" style="font-size: 12px;">
                                        <%# Eval("periodRemark")%>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="5">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="5">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td width="450" height="20" class="test_title_left">
                                采购审核
                            </td>
                            <td width="180" height="20" class="test_title_right">
                                采购审批
                            </td>
                        </tr>
                        <tr>
                            <td height="40" class="f12pxGgray">
                                <asp:Label ID="labOverrule" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td class="f12pxGgray_right">
                                <strong>日期</strong>
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10" class="f_12px" style="font-size: 10px; width: 50%">
                                <strong>文档日志 Log: </strong>
                            </td>
                            <td height="10" class="f_12px" style="font-size: 10px; width: 50%">
                                <strong>业务审核日志 Log: </strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="10" class="f_12px" style="font-size: 10px; width: 50%">
                                <asp:Label ID="lablog" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td height="10" class="f_12px" valign="top" style="font-size: 10px; width: 50%">
                                <asp:Label ID="labAuditLog" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="labafter" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="repGR" runat="server" OnItemDataBound="repGR_ItemDataBound">
                <ItemTemplate>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10px">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="80" valign="bottom" style="padding: 0 0 5px 0">
                                <asp:Image runat="server" ID="logoPo" Width="327" Height="35" ImageUrl="images/rec.gif" />
                            </td>
                            <td align="right" valign="bottom" style="padding: 0 0 5px 0">
                                <asp:Image runat="server" ID="logoImg" Width="63" Height="35" ImageUrl="images/xingyan.png" />
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="30px">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="30px" colspan="4" class="test_title_right">
                                基本信息
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="100" height="30" class="f12pxGgrayBold">
                                供应商
                            </td>
                            <td align="center" width="215" class="f12pxGgray">
                                <asp:Label ID="lblRecipientSupplier" runat="server" />&nbsp;
                            </td>
                            <td align="center" width="100" class="f12pxGgrayBold">
                                订单编号
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labOrderNo" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                服务项目
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="labproject_name" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgrayBold">
                                项目号
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labproject_code" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                负责人
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientResponser" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                业务组别
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientDept" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                附加收货人
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientAppend" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                服务内容
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labproject_descripttion" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                收货类型
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="labRecipientType" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                服务日期
                            </td>
                            <td align="left" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labintend_receipt_date" runat="server" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="30px">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="30" colspan="4" class="test_title_right" style="font-size: 14px;">
                                收货明细
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                收货单号
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label runat="server" ID="labRecipientNo"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                单价
                            </td>
                            <td width="215px" height="30" align="center" class="f12pxGgray" height="30" style="font-size: 12px;">
                                <asp:Label ID="labSinglePrice" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td width="100px" height="30" align="center" class="f12pxGgray" style="font-size: 12px;">
                                <strong>数量</strong>
                            </td>
                            <td width="215px" height="30" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="labNum" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                内容
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labDes" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                备注
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labNote" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                历史收货信息
                            </td>
                            <td width="530px" height="30" align="left" colspan="3" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="labRecipientHist" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                历史收货小计
                            </td>
                            <td width="530px" height="30" align="right" colspan="3" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labHistTotal" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                本次收货金额
                            </td>
                            <td width="530px" height="30" align="right" colspan="3" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="lblRecipientAmount" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                应收总计
                            </td>
                            <td width="530px" height="30" align="right" colspan="3" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <strong>
                                    <asp:Label ID="labTotal" runat="server" /></strong>
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="30px">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="30" colspan="5" class="test_title_right" style="font-size: 14px;">
                                订单明细
                            </td>
                        </tr>
                        <tr>
                            <td width="160px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                货品
                            </td>
                            <td width="160px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                详细描述
                            </td>
                            <td width="110px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                单价
                            </td>
                            <td width="100px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                数量
                            </td>
                            <td width="100px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>小计</strong>
                            </td>
                        </tr>
                        <asp:Repeater ID="repRecipientItem" runat="server" OnItemDataBound="repRecipientItem_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="f12pxGgray" height="30" style="font-size: 12px;">
                                        <%# Eval("Item_No")%>&nbsp;
                                    </td>
                                    <td class="f12pxGgray_right" align="center" style="font-size: 12px;">
                                        <%# Eval("desctiprtion")%>&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray" style="font-size: 12px;">
                                        <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.00")%>&nbsp;
                                    </td>
                                    <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                        <%# Eval("quantity")%>&nbsp;
                                    </td>
                                    <td align="right" class="f12pxGgray_right" style="font-size: 12px;">
                                        <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.00")%>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="20px" class="f_12px" style="font-size: 12px;">
                                <asp:Label runat="server" ID="lblLastNotify"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="30px">
                                <img src="images/space.gif" width="1" height="1" />
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="10" class="f_12px" style="font-size: 10px;">
                                <strong>文档日志 Log: </strong>
                            </td>
                        </tr>
                        <tr>
                            <td height="10" class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lblRecipientLog" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblRecipientAfter" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
