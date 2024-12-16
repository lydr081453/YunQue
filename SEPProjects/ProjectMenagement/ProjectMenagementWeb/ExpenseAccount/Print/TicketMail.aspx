<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketMail.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.Print.TicketMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>机票申请单打印</title>
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
        .f12pxGgray_left
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
        .f12pxGgray_one
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
        .f12pxGgray_middle
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
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
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
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
        }
        .test_title_right
        {
            font: Verdana, Arial, Helvetica, sans-serif;
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
        }
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td height="50">
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="80" colspan="4" valign="bottom" style="padding: 0 0 5px 0">
                <asp:Image runat="server" ID="logoImg" Width="126" Height="70" />
            </td>
            <td align="center" colspan="3" valign="middle" style="padding: 0 0 5px 0; font-size: 24px;">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">申请单号:</span>
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblPN" />&nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">提交日期:</span><asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblRequestDate" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">参考单据:</span>
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblRefPn" />
            </td>
            <td>
                &nbsp;
            </td>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">项目号:</span>
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblProjectCode" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">申请人:</span>
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblApplicantUser" />
            </td>
            <td>
                &nbsp;
            </td>
            <td align="left" colspan="3">
                <span style="color: Black; font-size: 12px;">业务组:</span>
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblDepartment" />
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                    <tr>
                        <td width="120px" height="20" align="center" class="test_title_left" style="font-size: 12px;">
                            航班号
                        </td>
                        <td width="100px" align="center" class="test_title_middle" style="font-size: 12px;">
                            From
                        </td>
                        <td width="100px" align="center" class="test_title_middle" style="font-size: 12px;">
                            To
                        </td>
                        <td width="100px" align="center" class="test_title_middle" style="font-size: 12px;">
                            日期
                        </td>
                        <td width="100px" align="center" class="test_title_middle" style="font-size: 12px;">
                            类型
                        </td>
                        <td width="110px" align="center" class="test_title_right" style="font-size: 12px;">
                            价格
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="repExpense" OnItemDataBound="repExpense_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td align="center" height="20px" class="f12pxGgray_left" style="font-size: 12px;">
                                    <asp:Label ID="labAirNo" runat="server" />&nbsp;
                                </td>
                                <td align="center" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="labDFrom" runat="server" />&nbsp;&nbsp;
                                </td>
                                <td align="center" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="labTo" runat="server" />&nbsp;
                                </td>
                                <td align="center" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="labDate" runat="server" />&nbsp;
                                </td>
                                <td align="center" class="f12pxGgray_middle" style="font-size: 12px; font-weight: bold;
                                    font-style: italic">
                                    <asp:Label ID="labType" runat="server" />&nbsp;
                                </td>
                                <td align="right" class="f12pxGgray_right" style="font-size: 12px;">
                                    <asp:Label ID="labFee" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="f12pxGgray_left" align="left" colspan="3" style="font-size: 12px;">
                                    登机人:<asp:Label ID="labBoarder" runat="server" />&nbsp;
                                </td>
                                <td align="left" colspan="3" class="f12pxGgray_right" style="font-size: 12px;">
                                    联系方式:<asp:Label ID="lblMobile" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" class="f12pxGgray_left" style="font-size: 12px;">
                                    证件类型:<asp:Label ID="lblCardType" runat="server" />&nbsp;
                                </td>
                                <td align="left" colspan="3" height="20px" class="f12pxGgray_right" style="font-size: 12px;">
                                    证件号:<asp:Label ID="labID" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="f12pxGgray_one" align="left" colspan="6" style="font-size: 12px;">
                                    备注:
                                    <asp:Label ID="labRemark" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6" class="f12pxGgray_one" style="font-size: 12px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <asp:Literal runat="server" ID="liter"></asp:Literal>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td class="f12pxGgray_one" align="right" colspan="6" style="font-size: 12px; background-color: #66ff99;">
                            合计:
                            <asp:Label ID="lab_TotalPrice" runat="server" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="10" colspan="7">
            </td>
        </tr>
    </table>
    <br />
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" style="font-size: 10px">
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
    </table>
    <br />
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tabSupply">
        <tr>
            <td height="20" align="center" valign="bottom" style="color: Black;">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
