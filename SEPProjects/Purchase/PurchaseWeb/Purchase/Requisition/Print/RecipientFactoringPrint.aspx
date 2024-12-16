<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecipientFactoringPrint.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Print.RecipientFactoringPrint" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>收货单</title>
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
            font-size: 12px;
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
            font-size: 12px;
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
            font-size: 12px;
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
            font-size: 14px;
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
    <form id="Form1" runat="server">
    <asp:Repeater ID="repList" runat="server" OnItemDataBound="repList_ItemDataBound">
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
                        <asp:Label ID="lab_supplier_name" runat="server" />&nbsp;
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
                        <asp:Label ID="lab_endusername" runat="server" />&nbsp;
                    </td>
                    <td align="center" height="30" class="f12pxGgrayBold">
                        业务组别
                    </td>
                    <td align="center" width="215" class="f12pxGgray_right">
                        <asp:Label ID="lab_requestor_group" runat="server" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" height="30" class="f12pxGgrayBold">
                        附加收货人
                    </td>
                    <td align="center" class="f12pxGgray_right">
                        <asp:Label ID="lab_AppendUser" runat="server" />&nbsp;
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
                        <asp:Label ID="labRecipientLog" runat="server" />&nbsp;
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
                        <asp:Label ID="lab_moneytype" runat="server" />&nbsp;
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
                <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td align="center" class="f12pxGgray" height="30" style="font-size: 12px;">
                                <%# Eval("Item_No")%>&nbsp;
                            </td>
                            <td class="f12pxGgray_right" align="center" style="font-size: 12px;">
                                <%# Eval("desctiprtion")%>&nbsp;
                            </td>
                            <td align="right" class="f12pxGgray" style="font-size: 12px;">
                                <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.####")%>&nbsp;
                            </td>
                            <td align="center" class="f12pxGgray" style="font-size: 12px;">
                                <%# Eval("quantity")%>&nbsp;
                            </td>
                            <td align="right" class="f12pxGgray_right" style="font-size: 12px;">
                                <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.####")%>&nbsp;
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
                        <asp:Label ID="lablog" runat="server"></asp:Label>&nbsp;
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
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="20" align="center" valign="bottom" class="white_font">
                &nbsp;
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="images/btnbgimg.gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg.gif"
                class="white_font" onclick="javascript:window.close();" style="cursor: pointer">
                关闭
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
