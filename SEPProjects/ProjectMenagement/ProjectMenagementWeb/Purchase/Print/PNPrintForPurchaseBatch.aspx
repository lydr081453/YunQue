<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PNPrintForPurchaseBatch.aspx.cs"
    Inherits="Purchase_Print_PNPrintForPurchaseBatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>����ȷ��</title>
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
        /* -----------����--------- */.list_title
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
        /* -----------btn_��ҳ--------- */.white_font
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
        /* -----------���뵥�鿴��ʽ��--------- */.title
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
        /* -----------����--------- */.list_title
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
        /* -----------btn_��ҳ--------- */.white_font
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
        /* -----------���뵥�鿴��ʽ��--------- */.title
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
                <span style="color: Black; font-size: 12px;">���κ�:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblPurchaseBatchCode" />&nbsp;
            </td>
            <td align="right" colspan="4">
                <span style="color: Black; font-size: 12px;">����ƾ֤��:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblBatchCode" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="110px" height="20" align="center" class="test_title_left" style="font-size: 12px;">
                ��Ŀ��<br />
                ������ϸ����
            </td>
            <td width="90px" align="center" class="test_title_middle" style="font-size: 12px;">
                ���÷�������
            </td>
            <td width="50px" align="center" class="test_title_middle" style="font-size: 12px;">
                ������
            </td>
            <td width="60px" align="center" class="test_title_middle" style="font-size: 12px;">
                Ա�����
            </td>
            <td width="90px" align="center" class="test_title_middle" style="font-size: 12px;">
                ����������
            </td>
            <td width="80px" align="center" class="test_title_middle" style="font-size: 12px;">
                ������
            </td>
            <td width="75px" align="center" class="test_title_middle" style="font-size: 12px;">
                ������
            </td>
            <td width="75px" align="center" class="title_right" style="font-size: 12px;">
                PN��
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
                С��(����Ӧ�̣�
            </td>
            <td class="f12pxGgray_right" align="right" style="font-size: 12px; background-color: #66ff99;"
                colspan="2">
                <asp:Label ID="lab_TotalPrice" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                ��˾����
            </td>
            <td class="f12pxGgray" align="right" colspan="2" style="font-size: 12px; background-color: #66ff99;">
                <asp:Label ID="labAccountName" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                ����������
            </td>
            <td class="f12pxGgray" align="right" colspan="2" style="font-size: 12px; background-color: #66ff99;">
                <asp:Label ID="labAccountBankName" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" height="20" style="font-size: 12px; font-weight: bold;
                text-align: right; border-left: solid 1px #a5c2a5; border-bottom: solid 1px #a5c2a5">
                �����ʺ�
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
                <a style="cursor: pointer" onclick="javascript:window.print();">��ӡ</a>
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.close();">�ر�</a>
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
                                <strong>��ˮ��<asp:Label ID="labglideNo" runat="server" /><strong><br />
                                    <asp:Label ID="laboldprinfo" runat="server"></asp:Label>
                            </td>
                            <td width="34" align="right" class="f_12px">
                                &nbsp;
                            </td>
                            <td width="280" align="right" class="f_12px">
                                <strong>���뵥��<asp:Label ID="labPrno" runat="server" /></strong><br />
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
                                <strong>����:<asp:Label ID="labLX" runat="server" /></strong><br />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="2" class="test_title">
                                ��Ӧ��
                            </td>
                            <td colspan="2" class="test_title">
                                �ɹ���
                            </td>
                        </tr>
                        <tr>
                            <td width="65" height="20" class="f_12px_bold">
                                ����
                            </td>
                            <td width="259" class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_name" runat="server" />&nbsp;
                            </td>
                            <td width="65" class="f_12px_bold">
                                ����
                            </td>
                            <td width="259" class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_DepartmentName" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                ��ַ
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_address" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                ��ַ
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_Address" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                ��ϵ��
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_linkman" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                ��ϵ��
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_contect_Name" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                ��ϵ�绰
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_phone" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                ��ϵ�绰
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_Telephone" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                ����
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_supplier_fax" runat="server" />&nbsp;
                            </td>
                            <td class="f_12px_bold">
                                �����ʼ�
                            </td>
                            <td class="f_12px" style="font-size: 10px;">
                                <asp:Label ID="lab_buyer_EMail" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" class="f_12px_bold">
                                �����ʼ�
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
                                ��Դ
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
                                �ͻ���
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
                                Ѻ��
                            </td>
                            <td height="20" colspan="6" class="test_title_right" style="font-size: 14px;">
                                <asp:Label runat="server" ID="lblYaJin"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                        <tr>
                            <td height="20" colspan="8" class="test_title_right" style="font-size: 14px;">
                                �ɹ�����
                            </td>
                        </tr>
                        <tr>
                            <td width="40px" height="20" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ���
                            </td>
                            <td width="160px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                �ɹ���Ʒ
                            </td>
                            <td width="140px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                �ջ�ʱ��
                            </td>
                            <td width="60px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��λ
                            </td>
                            <td width="50px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="80px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>С��</strong>
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
                                        ����
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
                                ����<br />
                                ����
                            </td>
                            <td align="center" width="450px" class="f12pxGgray" style="font-size: 12px;">
                                <asp:Label ID="lab_sow" runat="server" />&nbsp;
                            </td>
                            <%--<td rowspan="2" align="center" width="40px" class="f12pxGgray" style=" font-size:12px;"><strong>֧��<br />����</strong></td>
    <td height="20px" align="center" class="f12pxGgray" width="60px" style=" font-size:12px;"><strong>Ԥ����</strong></td>
    <td align="right" class="f12pxGgray" style=" font-size:12px;" width="100px"><asp:Label ID="lab_sow4" runat="server" />&nbsp;</td>--%>
                            <td align="center" width="40px" class="f12pxGgray" style="font-size: 12px;">
                                <strong>�ܼ�</strong>
                            </td>
                            <td align="right" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="lab_moneytype" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <%--<tr>
    <td height="20px" align="center" width="60px" class="f12pxGgray" style=" font-size:12px;"><strong>��������</strong></td>
    <td class="f12pxGgray" style=" font-size:12px;" width="100px"><asp:Label ID="lab_payment_terms" runat="server" />&nbsp;</td>
  </tr>--%>
                        <tr>
                            <td height="20px" width="40px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��ע
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
                                ������Ϣ
                            </td>
                        </tr>
                        <tr>
                            <td width="43" height="20" align="center" class="f12pxGgrayBold">
                                ������
                            </td>
                            <td width="75" align="center" class="f12pxGgrayBold">
                                ��������
                            </td>
                            <td width="105" align="center" class="f12pxGgrayBold">
                                ����
                            </td>
                            <td width="75" align="center" class="f12pxGgrayBold">
                                ҵ�����
                            </td>
                            <td width="43" align="center" class="f12pxGgrayBold">
                                ʹ����
                            </td>
                            <td width="105" align="center" class="f12pxGgrayBold">
                                ����
                            </td>
                            <td width="43" align="center" class="f12pxGgrayBold">
                                �ջ���
                            </td>
                            <td width="105" align="center" class="f12pxGgray_right">
                                <strong>����</strong>
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
                                �����ջ���:
                            </td>
                            <td align="left" colspan="6" height="20px" class="f12pxGgray_right">
                                <asp:Label ID="lab_AppendUser" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="20px" class="f12pxGgrayBold" style="white-space: nowrap">
                                �ջ����������緽ʽ:
                            </td>
                            <td align="center" colspan="6" height="20px" class="f12pxGgray_right">
                                <asp:Label ID="lab_OtherInfo" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="20" colspan="2" align="center" class="f12pxGgray">
                                <strong>��Ŀ��</strong>
                            </td>
                            <td height="20" colspan="4" align="center" class="f12pxGgray">
                                <strong>��Ŀ����</strong>
                            </td>
                            <td colspan="2" align="center" class="f12pxGgray_right">
                                <strong>Ԥ���� </strong>
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
                                ֧������
                            </td>
                        </tr>
                        <tr>
                            <td width="30px" height="20" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ���
                            </td>
                            <td width="55px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��������
                            </td>
                            <td width="80px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����</br>��׼��
                            </td>
                            <td width="45px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="45px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����</br>����
                            </td>
                            <td width="100px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                Ԥ��֧</br>��ʱ��
                            </td>
                            <td width="70px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                Ԥ��֧</br>�����
                            </td>
                            <td width="65px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                Ԥ��֧��</br>�ٷֱ�
                            </td>
                            <td width="65px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ʣ��</br>���
                            </td>
                            <td width="75px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>��ע</strong>
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
                                �ɹ����
                            </td>
                            <td width="180" height="20" class="test_title_right">
                                �ɹ�����
                            </td>
                        </tr>
                        <tr>
                            <td height="40" class="f12pxGgray">
                                <asp:Label ID="labOverrule" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td class="f12pxGgray_right">
                                <strong>����</strong>
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
                                <strong>�ĵ���־ Log: </strong>
                            </td>
                            <td height="10" class="f_12px" style="font-size: 10px; width: 50%">
                                <strong>ҵ�������־ Log: </strong>
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
                                ������Ϣ
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="100" height="30" class="f12pxGgrayBold">
                                ��Ӧ��
                            </td>
                            <td align="center" width="215" class="f12pxGgray">
                                <asp:Label ID="lblRecipientSupplier" runat="server" />&nbsp;
                            </td>
                            <td align="center" width="100" class="f12pxGgrayBold">
                                �������
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labOrderNo" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                ������Ŀ
                            </td>
                            <td align="center" class="f12pxGgray">
                                <asp:Label ID="labproject_name" runat="server" />&nbsp;
                            </td>
                            <td align="center" class="f12pxGgrayBold">
                                ��Ŀ��
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labproject_code" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                ������
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientResponser" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                ҵ�����
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientDept" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                �����ջ���
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="lblRecipientAppend" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                ��������
                            </td>
                            <td align="center" width="215" class="f12pxGgray_right">
                                <asp:Label ID="labproject_descripttion" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                �ջ�����
                            </td>
                            <td align="center" class="f12pxGgray_right">
                                <asp:Label ID="labRecipientType" runat="server" />&nbsp;
                            </td>
                            <td align="center" height="30" class="f12pxGgrayBold">
                                ��������
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
                                �ջ���ϸ
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                �ջ�����
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label runat="server" ID="labRecipientNo"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="215px" height="30" align="center" class="f12pxGgray" height="30" style="font-size: 12px;">
                                <asp:Label ID="labSinglePrice" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td width="100px" height="30" align="center" class="f12pxGgray" style="font-size: 12px;">
                                <strong>����</strong>
                            </td>
                            <td width="215px" height="30" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="labNum" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labDes" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��ע
                            </td>
                            <td width="530px" height="30" colspan="3" align="center" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labNote" runat="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��ʷ�ջ���Ϣ
                            </td>
                            <td width="530px" height="30" align="left" colspan="3" class="f12pxGgray_right" style="font-size: 12px;">
                                <asp:Label ID="labRecipientHist" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��ʷ�ջ�С��
                            </td>
                            <td width="530px" height="30" align="right" colspan="3" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="labHistTotal" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                �����ջ����
                            </td>
                            <td width="530px" height="30" align="right" colspan="3" class="f12pxGgray_right"
                                style="font-size: 12px;">
                                <asp:Label ID="lblRecipientAmount" runat="server" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                Ӧ���ܼ�
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
                                ������ϸ
                            </td>
                        </tr>
                        <tr>
                            <td width="160px" height="30" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��Ʒ
                            </td>
                            <td width="160px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ��ϸ����
                            </td>
                            <td width="110px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="100px" align="center" class="f12pxGgrayBold" style="font-size: 12px;">
                                ����
                            </td>
                            <td width="100px" align="center" class="f12pxGgray_right" style="font-size: 12px;">
                                <strong>С��</strong>
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
                                <strong>�ĵ���־ Log: </strong>
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
