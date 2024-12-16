<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FactoringOrderPrint.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Print.FactoringOrderPrint" %>


<%@ Import Namespace="ESP.Purchase.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>打印订单</title>
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
        .f12pxGgray_Title_Left_New
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
        }
        .f12pxGgray_Title_New
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #333;
            padding: 0 0 0 3px;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            font-size: 12px;
            font-weight: bold;
        }
        .f12pxGgray_Title_rightNew
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #333;
            padding: 0 0 0 3px;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            font-size: 12px;
            font-weight: bold;
        }
        .f12pxGgray_Total
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
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
            font-size: 12px;
            font-weight: bold;
        }
        .f12pxGgrayNew
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
        }
        .f12pxGgrayNew2
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
        .f12pxGgray_Bottom_left_New
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
        }
        .f12pxGgray_Bottom_right_New
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
        }
        .f12pxGgray_rightNew
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
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
        .test_title_rightNew
        {
            font: Verdana, Arial, Helvetica, sans-serif;
            size: 14px;
            color: #a5c2a5;
            font-weight: bold;
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #a5c2a5;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #a5c2a5;
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #a5c2a5;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #a5c2a5;
            padding: 0 0 0 0px;
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
    <form id="Form1" runat="server">
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="80" valign="bottom" style="padding: 0 0 5px 0">
                <asp:Image runat="server" ID="logoPo" Width="183" Height="35" />
            </td>
            <td align="right" valign="bottom" style="padding: 0 0 5px 0">
                <asp:Image runat="server" ID="logoImg" Width="63" Height="35" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="285" height="20" class="f_12px">
                &nbsp
            </td>
            <td align="right" class="f_12px">
                &nbsp;
            </td>
            <td width="307" align="right" class="f_12px">
                <strong>订单号<asp:Label ID="labOrderNo" runat="server" />&nbsp;&nbsp;</strong>
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
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
                传真
            </td>
            <td class="f_12px" style="font-size: 10px;">
                <asp:Label ID="labbuyerFax" runat="server" />&nbsp;
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
                电子邮件
            </td>
            <td class="f_12px" style="font-size: 10px;">
                <asp:Label ID="lab_buyer_EMail" runat="server" />&nbsp;
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
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" class="f_12px">
                <font style="font-style: italic" color="red">本订单内容受我司订单标准条款管辖</font>
            </td>
        </tr>
        <tr>
            <td height="10">
                <img src="images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="20" width="630px" colspan="6" class="test_title_rightNew" style="font-size: 14px;">
                采购内容
            </td>
        </tr>
        <tr>
            <td width="210px" align="left" class="f12pxGgray_Title_Left_New">
                项目<br />
                货品描述
            </td>
            <td width="140px" align="center" class="f12pxGgray_Title_New">
                收货时间
            </td>
            <td width="80px" align="center" class="f12pxGgray_Title_New">
                货币
            </td>
            <td align="center" class="f12pxGgray_Title_New">
                单位
            </td>
            <td align="center" class="f12pxGgray_Title_New">
                数量
            </td>
            <td align="center" class="f12pxGgray_Title_rightNew">
                单价
            </td>
        </tr>
        <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="left" width="630px" colspan="6" class="f12pxGgrayNew2" style="font-weight: bold;
                        font-size: 12px;">
                        #<asp:Label ID="labNum" runat="server" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="210px" class="f12pxGgrayNew" style="font-size: 12px;">
                        <%# Eval("Item_No")%>&nbsp;
                    </td>
                    <td width="140px" align="center" style="font-size: 12px;">
                        <%# Eval("intend_receipt_date")%>&nbsp;
                    </td>
                    <td width="80px" align="center" style="font-size: 12px;">
                        <asp:Label ID="lab_rep_moneytype" runat="server" />&nbsp;
                    </td>
                    <td align="center" style="font-size: 12px;">
                        <%# Eval("uom")%>&nbsp;
                    </td>
                    <td align="right" style="font-size: 12px;">
                        <%# decimal.Parse(Eval("quantity").ToString()).ToString("#,##0.####")%>&nbsp;
                    </td>
                    <td align="right" class="f12pxGgray_rightNew" style="font-size: 12px;">
                        <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.####")%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="630px" colspan="6" class="f12pxGgrayNew2" style="font-size: 12px;">
                        <%# Eval("desctiprtion")%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" width="630px" class="f12pxGgrayNew2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="630px" colspan="6" class="f12pxGgrayNew2" style="font-size: 12px;">
                        小计:<a style="text-decoration: underline; color: Black;"><%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.####")%></a>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" width="630px" class="f12pxGgrayNew2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="300px" colspan="3" class="f12pxGgray_Bottom_left_New" style="font-size: 12px;">
                    </td>
                    <td align="left" width="330px" colspan="3" class="f12pxGgray_Bottom_left_New" style="font-size: 12px;">
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="630px" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td width="630px" align="right" class="f12pxGgray_Total" style="font-size: 12px;
                color: Black; font-weight: bold">
                <strong>总计:</strong><asp:Label ID="lab_moneytype" runat="server" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="10px">
                <img src="images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="20" colspan="6" width="630px" class="test_title_rightNew" style="font-size: 14px;">
                支付条款
            </td>
        </tr>
        <tr>
            <td width="90px" align="left" class="f12pxGgray_Title_Left_New">
                类型<br />
                帐期描述
            </td>
            <td width="100px" align="center" class="f12pxGgray_Title_New" style="font-size: 12px;">
                基准点
            </td>
            <td width="100px" align="center" class="f12pxGgray_Title_New" style="font-size: 12px;">
                支付时间
            </td>
            <td width="100px" align="center" class="f12pxGgray_Title_New" style="font-size: 12px;">
                账期
            </td>
            <td width="80px" align="center" class="f12pxGgray_Title_New" style="font-size: 12px;">
                支付金额
            </td>
            <td width="80px" align="center" class="f12pxGgray_Title_rightNew" style="font-size: 12px;">
                百分比
            </td>
        </tr>
        <asp:Repeater ID="repPeriod" runat="server" OnItemDataBound="repPeriod_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="left" width="630px" colspan="6" class="f12pxGgrayNew2" style="font-weight: bold;
                        font-size: 12px;">
                        #<asp:Label ID="labNum" runat="server" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="90px" align="left" class="f12pxGgrayNew" style="font-size: 12px;">
                        <%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>&nbsp;
                    </td>
                    <td width="100px" align="center" style="font-size: 12px;">
                        <%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>&nbsp;
                    </td>
                    <td width="100px" align="center" style="font-size: 12px;">
                        <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>&nbsp;
                    </td>
                    <td width="80px" align="center" style="font-size: 12px;">
                        <%# Eval("periodDay")%><%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>&nbsp;
                    </td>
                    <td width="80px" align="center" style="font-size: 12px;">
                        <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>&nbsp;
                    </td>
                    <td width="80px" align="center" class="f12pxGgray_rightNew" style="font-size: 12px;">
                        <%# Eval("expectPaymentPercent") + "%"%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="630px" align="left" colspan="6" class="f12pxGgrayNew2" style="font-size: 12px;">
                        <%# Eval("periodRemark")%>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" width="630px" class="f12pxGgrayNew2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" width="300px" colspan="3" class="f12pxGgray_Bottom_left_New" style="font-size: 12px;">
                    </td>
                    <td align="left" width="330px" colspan="3" class="f12pxGgray_Bottom_left_New" style="font-size: 12px;">
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="10px">
                <img src="images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20px" colspan="8" class="test_title_right">
                申请信息
            </td>
        </tr>
        <tr>
            <td align="left" height="20px" width="100px" colspan="2" class="f12pxGgrayNew" style="font-size: 12px;
                font-weight: bold">
                框架协议号
            </td>
            <td align="left" height="20px" width="215px" colspan="2" style="font-size: 12px;">
                <asp:Label ID="lab_fa_no" runat="server" />&nbsp;
            </td>
            <td align="left" height="20px" width="100px" colspan="2" style="font-size: 12px;
                font-weight: bold">
                申请单号码
            </td>
            <td align="left" height="20px" width="215px" colspan="2" class="f12pxGgray_rightNew"
                style="font-size: 12px;">
                <asp:Label ID="lab_PrNo" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="20px" width="100px" colspan="2" class="f12pxGgrayNew" style="font-size: 12px;
                font-weight: bold">
                项目号
            </td>
            <td align="left" height="20px" width="215px" colspan="2" style="font-size: 12px;">
                <asp:Label ID="lab_project_code" runat="server" />&nbsp;
            </td>
            <td align="left" height="20px" width="100px" colspan="2" style="font-size: 12px;
                font-weight: bold">
                申请日期
            </td>
            <td align="left" height="20px" width="215px" colspan="2" class="f12pxGgray_rightNew"
                style="font-size: 12px;">
                <asp:Label ID="lab_app_date" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="20px" width="100px" colspan="2" class="f12pxGgray_Bottom_left_New"
                style="font-size: 12px; font-weight: bold">
                项目描述
            </td>
            <td align="left" colspan="6" width="530px" class="f12pxGgray_Bottom_right_New" style="font-size: 12px;">
                <asp:Label ID="lab_project_descripttion" runat="server" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="10px">
                <img src="images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20px" colspan="3" class="test_title_right">
                联系信息
            </td>
        </tr>
        <tr>
            <td width="200px" align="left" class="f12pxGgray_Title_Left_New" style="font-size: 12px;">
                姓名
            </td>
            <td width="230px" align="left" class="f12pxGgray_Title_New" style="font-size: 12px;">
                联系电话
            </td>
            <td width="200px" align="left" class="f12pxGgray_Title_rightNew" style="font-size: 12px;">
                部门
            </td>
        </tr>
        <tr>
            <td width="200px" align="left" class="f12pxGgrayNew" style="font-size: 12px;">
                <asp:Label ID="lab_requestorname" runat="server" />(申请人)
            </td>
            <td width="230px" align="left" style="font-size: 12px;">
                <asp:Label ID="lab_requestor_info" runat="server" />&nbsp;&nbsp;
            </td>
            <td width="200px" align="left" style="font-size: 12px;" class="f12pxGgray_rightNew">
                <asp:Label ID="lab_requestor_group" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="200px" align="left" class="f12pxGgrayNew" style="font-size: 12px;">
                <asp:Label ID="lab_endusername" runat="server" />(使用人)
            </td>
            <td width="230px" align="left" style="font-size: 12px;">
                <asp:Label ID="lab_enduser_info" runat="server" />&nbsp;&nbsp;
            </td>
            <td width="200px" align="left" style="font-size: 12px;" class="f12pxGgray_rightNew">
                <asp:Label ID="lab_enduser_group" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="200px" align="left" class="f12pxGgrayNew" style="font-size: 12px;">
                <asp:Label ID="lab_receivername2" runat="server" />(收货人)
            </td>
            <td width="230px" align="left" style="font-size: 12px;">
                <asp:Label ID="lab_receiver_info2" runat="server" />&nbsp;&nbsp;
            </td>
            <td width="200px" align="left" style="font-size: 12px;" class="f12pxGgray_rightNew">
                <asp:Label ID="lab_receiver_group" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td width="200px" align="left" class="f12pxGgray_Bottom_left_New" style="font-size: 12px;">
                采购部已审核
            </td>
            <td width="230px" align="left" style="font-size: 12px; border-bottom-width: 1px;
                border-bottom-style: solid; border-bottom-color: #a5c2a5;">
                &nbsp;
            </td>
            <td width="200px" align="left" style="font-size: 12px;" class="f12pxGgray_Bottom_right_New">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="10">
                <img src="images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td width="630" height="20" class="test_title_right">
                供应商签字确认
            </td>
        </tr>
        <tr>
            <td height="20" class="test_title_right">
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
            <td height="10" class="f_12px" style="font-size: 10px;">
                <span runat="server" id="spanlog" visible="false"><strong>文档日志 Log: </strong>
                    <br />
                </span>
            </td>
        </tr>
        <tr>
            <td height="10" class="f_12px" style="font-size: 10px;">
                <asp:Label ID="lablog" runat="server"></asp:Label>&nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="palBottom" runat="server">
        <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="f_12px">
                    <asp:Image runat="server" ID="logoPo2" Width="34" Height="37" />
                </td>
                <td width="600" align="left" class="f_12px">
                    此订单号必须记录在货物的发运单、发票以及相关文件上
                </td>
            </tr>
        </table>
        <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="5">
                    <div align="center">
                        <asp:HyperLink ID="linkConfirmUrl" runat="server" />
                    </div>
                </td>
            </tr>
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
    </asp:Panel>
    </form>
</body>
</html>

