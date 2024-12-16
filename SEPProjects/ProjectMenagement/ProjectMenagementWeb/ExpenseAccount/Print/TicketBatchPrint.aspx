<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketBatchPrint.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.Print.TicketBatchPrint" %>

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
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
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
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
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
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
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
            <td height="80" colspan="2" valign="bottom" style="padding: 0 0 5px 0">
                <img src="/images/xingyan.png" width="126" height="70" />
            </td>
            <td align="center" colspan="2" valign="middle" style="padding: 0 0 5px 0; font-size: 24px;">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span style="color: Black; font-size: 12px;">批次号:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblPN" />&nbsp;
            </td>
            <td align="left">
            </td>
            <td align="left">
                <span style="color: Black; font-size: 12px;">批次创建:</span>
            </td>
            <td align="Left">
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblRequestor" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <span style="color: Black; font-size: 12px;">申请日期:</span>&nbsp;<asp:Label runat="server"
                    Style="color: red; font-size: 12px;" ID="lblAppDate" />&nbsp;
            </td>
            <td align="left">
            </td>
            <td align="left">
                <span style="color: Black; font-size: 12px;">公司代码:</span>
            </td>
            <td align="left">
                <asp:Label runat="server" Style="color: red; font-size: 12px;" ID="lblBranch" />&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                    <asp:Repeater runat="server" ID="repExpense" OnItemDataBound="repExpense_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td align="left" height="20px" width="100px" class="f12pxGgray_left" style="font-size: 12px;">
                                    申请人:
                                </td>
                                <td align="left" height="20px" width="200px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="lblRequestor" runat="server" />&nbsp;
                                </td>
                                <td align="left" width="230px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    申请单号:
                                </td>
                                <td align="left" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                    <asp:Label ID="lblReturnCode" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" height="20px" width="100px" class="f12pxGgray_left" style="font-size: 12px;">
                                    参考单据:
                                </td>
                                <td align="left" height="20px" width="200px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="lblRefNo" runat="server" />&nbsp;
                                </td>
                                <td align="left" width="230px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    参考金额:
                                </td>
                                <td align="left" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                    <asp:Label ID="lblRefAmount" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" height="20px" width="100px" class="f12pxGgray_left" style="font-size: 12px;">
                                    项目号:
                                </td>
                                <td align="left" height="20px" width="200px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="lblProjectCode" runat="server" />&nbsp;
                                </td>
                                <td align="left" width="230px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    申请日期:
                                </td>
                                <td align="left" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                    <asp:Label ID="lblRequestDate" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" height="20px" width="100px" class="f12pxGgray_left" style="font-size: 12px;">
                                    成本所属组:
                                </td>
                                <td align="left" height="20px" width="200px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    <asp:Label ID="lblGroup" runat="server" />&nbsp;
                                </td>
                                <td align="left" width="230px" class="f12pxGgray_middle" style="font-size: 12px;">
                                    申请金额:
                                </td>
                                <td align="left" width="100px" class="f12pxGgray_right" style="font-size: 12px;">
                                    <asp:Label ID="lblTotal" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" height="20px" class="f12pxGgray_one" style="font-size: 12px;">
                                    机票明细:
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="f12pxGgray_one">
                                    <table  width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                                        <asp:Repeater runat="server" ID="repDetail" OnItemDataBound="repDetail_OnItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="100px" height="20" align="left" style="font-size: 12px; color: Black;">
                                                        航班号:<asp:Label ID="labAirNo" runat="server" />
                                                    </td>
                                                    <td width="200px" align="left" style="font-size: 12px; color: Black;">
                                                        出发地:<asp:Label ID="labDFrom" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目的地:<asp:Label
                                                            ID="labTo" runat="server" />
                                                    </td>
                                                    <td width="230px" align="left" style="font-size: 12px; color: Black;">
                                                        出发日期:
                                                        <asp:Label ID="labDate" runat="server" />
                                                    </td>
                                                    <td width="100px" align="left" style="font-size: 12px; color: Black;">
                                                        价格:
                                                        <asp:Label ID="labFee" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100px" height="20" align="left" style="font-size: 12px;">
                                                        登机人:
                                                        <asp:Label ID="labBoarder" runat="server" />
                                                    </td>
                                                    <td width="200px" align="left" style="font-size: 12px;">
                                                        证件号:
                                                        <asp:Label ID="labID" runat="server" />
                                                    </td>
                                                    <td width="230px" align="left" style="font-size: 12px;">
                                                        联系方式:
                                                        <asp:Label ID="lblMobile" runat="server" />
                                                    </td>
                                                    <td width="100px" align="left" style="font-size: 12px; color: Black;">
                                                        类型:
                                                        <asp:Label ID="labType" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="font-size: 12px;">
                                                        备&nbsp;&nbsp;&nbsp;&nbsp;注:<asp:Label ID="labRemark" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="height: 1px;">
                                                        <asp:Label ID="lblLine" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" height="20px" class="f12pxGgray_one" style="font-size: 12px;">
                                    审批意见:
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="630px" colspan="4" class="f12pxGgray_one" style="font-size: 12px;">
                                    <asp:Label ID="lblLog" runat="server" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="630px" colspan="4" class="f12pxGgray_one" style="font-size: 12px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <asp:Literal runat="server" ID="liter"></asp:Literal>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td class="f12pxGgray_one" width="630px" align="left" colspan="4" style="font-size: 12px;">
                            供应商：<asp:Label ID="lblSupplier" runat="server" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="f12pxGgray_one" width="630px" align="left" colspan="4" style="font-size: 12px;">
                            开户行：<asp:Label ID="lblBank" runat="server" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="f12pxGgray_one" width="630px" align="left" colspan="4" style="font-size: 12px;">
                            银行帐号：<asp:Label ID="lblAccount" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="f12pxGgray_one" width="630px" align="left" colspan="4" style="font-size: 12px;">
                            批次合计:<asp:Label ID="lab_TotalPrice" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; 返点金额:
                            <asp:Label ID="lblRetAmount" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="f12pxGgray_one" align="right" colspan="4" style="font-size: 12px; background-color: #66ff99;">
                            付款总额:
                            <asp:Label ID="lblTotalAmount" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20" align="center" valign="bottom" class="white_font">
                &nbsp;
            </td>
            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif"
                class="white_font">
                <a style="cursor: pointer;" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif"
                class="white_font">
                <a style="cursor: pointer;" onclick="javascript:window.close();">关闭</a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
