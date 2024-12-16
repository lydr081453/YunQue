<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaUnPayment.aspx.cs" Inherits="FinanceWeb.Purchase.Print.MediaUnPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>未付款记者申请单</title>
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
    <form id="Form1" runat="server">
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="80" valign="bottom" style="padding: 0 0 5px 0; font-size: 22px">
                <strong>未付款记者申请单</strong>
            </td>
            <td align="right" valign="bottom" style="padding: 0 0 5px 0">
                <img src="/images/xingyan.png" width="63" height="35" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20" class="f_12px">
            </td>
            <td width="164" align="left" class="f_12px">
                <strong>新PR单号:<asp:Label ID="lblNewPRNo" runat="server" /></strong><br />
                 <strong>新流水号:<asp:Label ID="lblNewPRID" runat="server" /></strong><br />
                 <strong>参考费用:<asp:Label ID="lblNewFee" runat="server" /></strong>
            </td>
            <td width="150" align="right" class="f_12px">
                 <strong>付款单号:<asp:Label ID="lblPN" runat="server" /></strong><br />
                 <strong>申请单号:<asp:Label ID="labPrno" runat="server" /></strong>
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="10">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="20" colspan="4" class="test_title_right">
                基本信息
            </td>
        </tr>
        <tr>
            <td width="157.5" height="20" align="center" class="f12pxGgrayBold">
                申请日期
            </td>
            <td width="157.5" align="center" class="f12pxGgrayBold">
                申请人
            </td>
            <td width="157.5" align="center" class="f12pxGgrayBold">
                申请人组别
            </td>
            <td width="157.5" align="center" class="f12pxGgray_right">
                <strong>联系方式</strong>
            </td>
        </tr>
        <tr>
            <td height="20" align="center" class="f12pxGgray">
                <asp:Label ID="lab_requestorDate" runat="server" />&nbsp;
            </td>
            <td align="center" class="f12pxGgray">
                <asp:Label ID="lab_requestorName" runat="server" />&nbsp;
            </td>
            <td align="center" class="f12pxGgray">
                <asp:Label ID="lab_requestorGroup" runat="server" />&nbsp;
            </td>
            <td align="center" class="f12pxGgray_right">
                <asp:Label ID="lab_requestorContacts" runat="server" />&nbsp;
            </td>
        </tr>
        <tr>
            <td height="20" colspan="2" align="center" class="f12pxGgray">
                <strong>项目号</strong>
            </td>
            <td height="20" align="center" class="f12pxGgray">
                <strong>项目号描述</strong>
            </td>
            <td align="center" class="f12pxGgray_right">
                <strong>费用预算 </strong>
            </td>
        </tr>
        <tr>
            <td height="20" colspan="2" align="center" class="f12pxGgray">
                <asp:Label ID="lab_project_code" runat="server" />&nbsp;
            </td>
            <td height="20" align="center" class="f12pxGgray">
                <asp:Label ID="lab_project_descripttion" runat="server" />&nbsp;
            </td>
            <td align="right" class="f12pxGgray_right">
                <asp:Label ID="lab_buggeted" runat="server" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="5">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="20" colspan="9" class="test_title_right" style="font-size: 14px;">
                稿件费用明细
            </td>
        </tr>
        <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
            <ItemTemplate>
                <asp:Literal ID="litSubTotal" runat="server" />
                <tr>
                    <td width="30" rowspan="5" height="80px" align="center" class="f12pxGgray">
                        <asp:Label ID="labNum" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="20px" align="center" class="f12pxGgray">
                        <strong>媒体名称</strong>
                    </td>
                    <td colspan="3" align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblMediaName" Text='<%#Eval("MediaName")%> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>文章标题</strong>
                    </td>
                    <td colspan="3" align="center" class="f12pxGgray_right">
                        <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Subject") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="20px" align="center" class="f12pxGgray">
                        <strong>收款人姓名</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblReceiptName" Text='<%# Eval("ReporterName","{0}")+"/"+Eval("ReceiverName","{0}")%> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>身份证号</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblIDCard" Text='<%#Eval("CardNumber")   %> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>发稿日期</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblReleaseDate" ></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>费用</strong>
                    </td>
                    <td align="right" class="f12pxGgray_right">
                        <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("TotalAmount")   %> '></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="20px" align="center" class="f12pxGgray">
                        <strong>开户行</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblBank" Text='<%#Eval( "BankName")   %> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>账号</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblBankCount" Text='<%#Eval("BankAccountName")   %> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>所在城市</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblCity" Text='<%#Eval("CityName")   %> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>是否代理发稿</strong>
                    </td>
                    <td align="right" class="f12pxGgray_right">
                        <asp:Label runat="server" ID="lblDelegate" Text='<%#Eval("IsDelegate").ToString()=="True"?"是":"否"   %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="20px" align="center" class="f12pxGgray">
                        <strong>字数</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblWordLength" Text='<%#Eval("WordLength")   %> '></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>单价</strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblUnitPrice"></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong><span style="font-size: 12px; font-style: italic">均价</span></strong>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <asp:Label runat="server" ID="lblAvgPrice"></asp:Label>
                    </td>
                    <td align="center" class="f12pxGgray">
                        <strong>是否配图</strong>
                    </td>
                    <td align="right" class="f12pxGgray_right">
                        <asp:Label runat="server" ID="lblImage" Text='<%# Eval( "IsImage").ToString()=="True"?"是":"否"   %> '></asp:Label>
                    </td>
                </tr>
                <asp:Literal ID="litEndSubTotal" runat="server" />
            </ItemTemplate>
        </asp:Repeater>
        <asp:Literal ID="litTotal" runat="server" />
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="5">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td height="20" colspan="6" width="100%" class="test_title_right">
                审批记录
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="6" height="80" align="left" class="f12pxGgray_right">
                <asp:Label runat="server" ID="lblAuditHist" Width="100%"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="5">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
        </tr>
    </table>
    <table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="20" align="center" valign="bottom" class="white_font">
                &nbsp;
            </td>
            <td width="1">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="/images/btnbgimg.gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="1">
                <img src="/images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="/images/btnbgimg.gif"
                class="white_font" onclick="javascript:window.close();" style="cursor: pointer">
                关闭
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
