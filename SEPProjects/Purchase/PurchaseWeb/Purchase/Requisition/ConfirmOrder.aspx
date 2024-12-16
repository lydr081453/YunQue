<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_ConfirmOrder" Codebehind="ConfirmOrder.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>ȷ�϶���</title>

<style type="text/css">
body {
	background-image: url(images/body_bg.jpg);
	background-repeat: no-repeat;
	background-position: center top;
	margin: 0px;
	padding: 0px;
}
/* -----------top--------- */
.toplink {
	font-size: 14px;
	color: #000;
}
.toplink a:link {
	color: #000;
	text-decoration: none;
}
.toplink a:visited {
	color: #000;
	text-decoration: none;
}
.toplink a:hover {
	color: #000;
	text-decoration: underline;
}
.time {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size:11px;
	color:#000000;
}
.font_12px_blue {
	font-size:12px;
	color:#7282a9;
	line-height:150%;
}
.font {
	font-size:12px;
	color:#333;
	font-weight:bold;
}
/* -----------left_menu--------- */
.left_menu {
	font-size: 12px;
	color: #666;
	text-decoration: underline;
}
.left_menu a:link {
	color: #666;
	text-decoration: underline;
}
.left_menu a:visited {
	color: #666;
	text-decoration: underline;
}
.left_menu a:hover {
	color: #333;
	text-decoration: underline;
}
/* -----------����--------- */
.list_title {
	background-image:url(images/blue_inside.gif);
	background-repeat:no-repeat;
	font-size:12px;
	color:#7282a9;
	font-weight:bold;
}
.list_title2 {
	font-size:12px;
	color:#7282a9;
	font-weight:bold;
}
.list_nav {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size:12px;
	color:#333;
	padding:10px 0 10px 0;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #eaedf1;
}
.f_16px {
	font-size:16px;
	color:#333;
	font-weight:bold;
}
.f_12px {
	font-family:Arial, Helvetica, sans-serif;
	font-size:12px;
	color:#333;
}
.f_12px_bold {
	font-family:Arial, Helvetica, sans-serif;
	font-size:12px;
	color:#333;
	font-weight:bold;
}
.f_blue {
	font-size: 12px;
	color:#4d568c;
	text-decoration: none;
}
.f_blue a:link {
	color: #4d568c;
	text-decoration: none;
}
.f_blue a:visited {
	color: #4d568c;
	text-decoration: none;
}
.f_blue a:hover {
	color: #4d568c;
	text-decoration: underline;
}
/* -----------btn--------- */
.white_bold_font {
	font-size: 12px;
	color:#FFF;
	font-weight:bold;
	text-decoration: none;
	vertical-align:text-top;
	padding-top:7px;
}
.white_bold_font a:link {
	color: #FFF;
	text-decoration: none;
}
.white_bold_font a:visited {
	color: #FFF;
	text-decoration: none;
}
.white_bold_font a:hover {
	color: #FFF;
	text-decoration: underline;
}
/* -----------btn_��ҳ--------- */
.white_font {
	font-size: 12px;
	color:#FFF;
	text-decoration: none;
	vertical-align:text-top;
	padding-top:5px;
	background-repeat:no-repeat;
}
.white_font a:link {
	color: #FFF;
	text-decoration: none;
}
.white_font a:visited {
	color: #FFF;
	text-decoration: none;
}
.white_font a:hover {
	color: #FFF;
	text-decoration: underline;
}
/* -----------���뵥�鿴��ʽ��--------- */
.title {
	font-size:14px;
	color:#333333;
	font-weight:bold;
	padding:0 0 0 10px;
}
.f_gray_left {
	font-size:12px;
	color:#6e6e6e;
	font-weight:bold;
	padding:0 0 0 10px;
}
.f_gray_right {
	font-size:12px;
	color:#6e6e6e;
	padding:0 0 0 10px;
}
.f_gray_left_withoutPadding {
	font-size:12px;
	color:#6e6e6e;
	font-weight:bold;
}
.f_gray_right_withoutPadding {
	font-size:12px;
	color:#6e6e6e;
}

.input_btn {
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
/* -----------po&pr--------- */
.f_white14_Bold {
	font-size: 14px;
	font-weight: bold;
	color: #FFF;
	padding:0 0 0 3px;
}
.f12pxGgray {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #333;
	padding:0 0 0 3px;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #a5c2a5;
	border-left-width: 1px;
	border-left-style: solid;
	border-left-color: #a5c2a5;
}
.f12pxGgray_right {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #333;
	padding:0 0 0 3px;
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
.f12pxGgrayBold {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: bold;
	color: #333;
	padding:0 0 0 3px;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #a5c2a5;
	border-left-width: 1px;
	border-left-style: solid;
	border-left-color: #a5c2a5;
}
.test_title_left {
	font:Verdana, Arial, Helvetica, sans-serif;
	size:14px;
	color:#a5c2a5;
	font-weight:bold;
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #a5c2a5;
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #a5c2a5;
	border-left-width: 1px;
	border-left-style: solid;
	border-left-color: #a5c2a5;
	padding:0 0 0 3px;
	font-size: 14px;
}
.test_title_right {
	size:14px;
	color:#a5c2a5;
	font-weight:bold;
	border:1px solid #a5c2a5;
	padding:0 0 0 3px;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 14px;
}
.f12pxGgray_right_2 {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #333;
	padding:0 0 0 3px;
	border:1px solid #a5c2a5;
}
.f12pxGgrayBold_2 {
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: bold;
	color: #333;
	padding:0 0 0 3px;
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
.test_title {
	font-size:14px;
	color:#a5c2a5;
	font-weight:bold;
}
</style>

<style type="text/css" media=print>
    .noprint
    {
    	display : none 
    }
</style>
</head>

<body>
<form id="Form1" runat="server">
<div align="center">
    <h1>
<font color="red"><asp:Label ID="labConfirm" runat="server"></asp:Label></font>
    </h1>
</div>
<br />
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="80" valign="bottom" style="padding:0 0 5px 0"><asp:Image  runat="server" id="logoPo" width="183" height="35"  /></td>
    <td align="right" valign="bottom" style="padding:0 0 5px 0"><asp:Image  runat="server" id="logoImg"/></td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="285" height="20" class="f_12px"><strong>������<asp:Label ID="labOrderNo" runat="server" />&nbsp;&nbsp;�����</strong></td>
    <td align="right" class="f_12px"><asp:Image  runat="server" id="logoPo2" width="34" height="37"  /></td>
    <td width="307" align="right" class="f_12px">�˶����ű����¼�ڻ���ķ��˵�����Ʊ�Լ�����ļ���</td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="20" colspan="2" class="test_title">��Ӧ��</td>
    <td colspan="2" class="test_title">�ɹ���</td>
  </tr>
  <tr>
    <td width="65" height="20" class="f_12px_bold">����</td>
    <td width="259" class="f_12px"><asp:Label ID="lab_supplier_name" runat="server"/>&nbsp;</td>
    <td width="65" class="f_12px_bold">����</td>
    <td width="259" class="f_12px"><asp:Label ID="lab_DepartmentName" runat="server"/>&nbsp;</td>
  </tr>
  <tr>
    <td height="20" class="f_12px_bold">��ַ</td>
    <td class="f_12px"><asp:Label ID="lab_supplier_address" runat="server" />&nbsp;</td>
    <td class="f_12px_bold">��ַ</td>
    <td class="f_12px"><asp:Label ID="lab_buyer_Address" runat="server" />&nbsp;</td>
  </tr>
  <tr>
    <td height="20" class="f_12px_bold">��ϵ��</td>
    <td class="f_12px"><asp:Label ID="lab_supplier_linkman" runat="server" />&nbsp;</td>
    <td class="f_12px_bold">��ϵ��</td>
    <td class="f_12px"><asp:Label ID="lab_buyer_contect_Name" runat="server" />&nbsp;</td>
  </tr>
  <tr>
    <td height="20" class="f_12px_bold">��ϵ�绰</td>
    <td class="f_12px"><asp:Label ID="lab_supplier_phone" runat="server" />&nbsp;</td>
    <td class="f_12px_bold">��ϵ�绰</td>
    <td class="f_12px"><asp:Label ID="lab_buyer_Telephone" runat="server" />&nbsp;</td>
  </tr>
  <tr>
    <td height="20" class="f_12px_bold">����</td>
    <td class="f_12px"><asp:Label ID="lab_supplier_fax" runat="server" />&nbsp;</td>
    <td class="f_12px_bold">����</td>
    <td class="f_12px">&nbsp;</td>
  </tr>
  <tr>
    <td height="20" class="f_12px_bold">�����ʼ�</td>
    <td class="f_12px"><asp:Label ID="lab_supplier_email" runat="server"/>&nbsp;</td>
    <td class="f_12px_bold">�����ʼ�</td>
    <td class="f_12px"><asp:Label ID="lab_buyer_EMail" runat="server" />&nbsp;</td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="62" height="20" class="f_12px_bold">�ͻ���</td>
    <td class="f_12px"><asp:Label ID="lab_ship_address" runat="server" />&nbsp;</td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="10"><img src="images/space.gif" width="1" height="1" /></td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="20" colspan="8" class="test_title_right">�ɹ�����</td>
  </tr>
  <tr>
    <td width="40px" height="20" align="center" class="f12pxGgrayBold"  style=" font-size:12px;">���</td>
    <td width="160px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">�ɹ���Ʒ</td>
    <td width="140px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">�ջ�ʱ��</td>
    <td width="60px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</td>
    <td width="50px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</td>
    <td width="50px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">��λ</td>
    <td width="50px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</td>
    <td width="80px" align="center" class="f12pxGgray_right" style=" font-size:12px;"><strong>С��</strong></td>
  </tr>
  
    <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
        <ItemTemplate>
          <tr>
            <td height="20px" align="center" class="f12pxGgray" style=" font-size:12px;"><asp:Label ID="labNum" runat="server" /></td>
            <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# Eval("Item_No")%>&nbsp;</td>
            <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# Eval("intend_receipt_date")%>&nbsp;</td>
            <td align="right" class="f12pxGgray" style=" font-size:12px;"><%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.####")%>&nbsp;</td>
            <td align="center" class="f12pxGgray" style=" font-size:12px;"><asp:Label ID="lab_rep_moneytype" runat="server" />&nbsp;</td>
            <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# Eval("uom")%>&nbsp;</td>
            <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# decimal.Parse(Eval("quantity").ToString()).ToString("#,##0.####")%>&nbsp;</td>
            <td align="right" class="f12pxGgray_right" style=" font-size:12px;"><%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.####")%>&nbsp;</td>
          </tr>
        </ItemTemplate>
    </asp:Repeater>
  <tr>
    <td  width="40px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����<br />����</td>
    <td  align="center" colspan="3" width="450px" class="f12pxGgray" style=" font-size:12px;"><asp:Label ID="lab_sow" runat="server" />&nbsp;</td>
    <td  align="center" width="40px" colspan="2" class="f12pxGgray" style=" font-size:12px;"><strong>�ܼ�</strong></td>
    <td  align="right" width="100px" colspan="2" class="f12pxGgray_right" style=" font-size:12px;"><asp:Label ID="lab_moneytype" runat="server" />&nbsp;</td>
  </tr>
  <tr>
    <td height="20px" width="40px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">��ע</td>
    <td colspan=7" align="center" class="f12pxGgray_right" style=" font-size:12px;"><asp:Label ID="lab_sow3" runat="server" />&nbsp;</td>
  </tr>
</table>

<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="10"><img src="images/space.gif" width="1" height="1" /></td>
  </tr>
</table>
<table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="20" colspan="10" class="test_title_right"  style=" font-size:14px;">֧������</td>
  </tr>
   <tr>
    <td width="30px" height="20" align="center" class="f12pxGgrayBold"  style=" font-size:12px;">���</td>
    <td width="55px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">��������</td>
    <td width="80px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</br>��׼��</td>
    <td width="45px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</td>
    <td width="45px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">����</br>����</td>
    <td width="100px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">Ԥ��֧</br>��ʱ��</td>
    <td width="70px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">Ԥ��֧</br>�����</td>
    <td width="65px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">Ԥ��֧��</br>�ٷֱ�</td>
    <td width="65px" align="center" class="f12pxGgrayBold" style=" font-size:12px;">ʣ��</br>���</td>
    <td width="75px" align="center" class="f12pxGgray_right" style=" font-size:12px;"><strong>��ע</strong></td>
  </tr>   
  <asp:Repeater ID="repPeriod" runat="server" OnItemDataBound="repPeriod_ItemDataBound">
    <ItemTemplate>
      <tr>
        <td height="20px" align="center" class="f12pxGgray" style=" font-size:12px;"><asp:Label ID="labNum" runat="server" /></td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>&nbsp;</td>
        <td align="right" class="f12pxGgray" style=" font-size:12px;"><%# Eval("periodDay")%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><%# Eval("expectPaymentPercent") + "%"%>&nbsp;</td>
        <td align="center" class="f12pxGgray" style=" font-size:12px;"><asp:Literal ID="LitOverplusPrice" runat="server" />&nbsp;</td>
        <td align="right" class="f12pxGgray_right" style=" font-size:12px;"><%# Eval("periodRemark")%>&nbsp;</td>
      </tr>
    </ItemTemplate>
  </asp:Repeater>
</table>

<table width="630" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="10"><img src="images/space.gif" width="1" height="1" /></td>
  </tr>
</table>

<table width="630" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="30" colspan="9" class="test_title_right">��ʷ����</td>
  </tr>
  <tr>
    <td width="45" height="25" align="center" class="f12pxGgrayBold">���</td>
    <td width="120" align="center" class="f12pxGgrayBold">������</td>
    <td width="110" align="center" class="f12pxGgrayBold">�ܶ�</td>
    <td width="60" align="center" class="f12pxGgrayBold">����</td>
    <td width="65" align="center" class="f12pxGgrayBold">��ϵ��</td>
    <td width="125" colspan="2" align="center" class="f12pxGgrayBold">��ϵ���ʼ�</td>
    <td width="90" colspan="2" align="center" class="f12pxGgray_right"></td>
  </tr>
  
    <asp:Repeater ID="repHis" runat="server" OnItemDataBound="repHis_ItemDataBound">
        <ItemTemplate>
              <tr>
                <td height="25" align="center" class="f12pxGgray"><asp:Label ID="labNum" runat="server" />&nbsp;</td>
                <td align="center" class="f12pxGgray"><%# Eval("orderid")%>&nbsp;</td>
                <td align="center" class="f12pxGgray"><%# Eval("totalprice")%>&nbsp;</td>
                <td align="center" class="f12pxGgray"><asp:Label ID="lab_rep_moneytype" runat="server" />&nbsp;</td>
                <td align="right" class="f12pxGgray"><asp:Label ID="lab_buyer_name" runat="server" /> &nbsp;</td>
                <td align="center" colspan="2" class="f12pxGgray"><asp:Label ID="lab_buyer_email" runat="server" />&nbsp;</td>
                <td align="center" colspan="2" class="f12pxGgray_right"><asp:Label ID="lab_statusname" runat="server" />&nbsp;</td>
              </tr>
        </ItemTemplate>
    </asp:Repeater>
    
</table>

</form>
</body>
</html>
