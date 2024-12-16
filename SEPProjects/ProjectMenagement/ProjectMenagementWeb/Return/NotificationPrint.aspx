<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Return_NotificationPrint" Codebehind="NotificationPrint.aspx.cs" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
        img
        {
            border: none;
        }
        .nav
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .nav table
        {
            border-top-width: 1px;
            border-left-width: 1px;
            border-top-style: solid;
            border-left-style: solid;
            border-top-color: #999999;
            border-left-color: #999999;
        }
        .nav td
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-right-color: #999999;
            border-bottom-color: #999999;
            padding: 0 0 0 5px;
            color: #333333;
            line-height: 150%;
        }
        .nav em
        {
            font-style: normal;
            font-size: 14px;
            color: #CC6633;
            font-weight: bold;
        }
        .topline
        {
            border-top-width: 2px;
            border-top-style: solid;
            border-top-color: #999999;
        }
    </style>
</head>
<body>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 40px 0 10px 0;">
                    <tr>
                        <td width="100%" align="right">
                            <span style="font-family: Arial Unicode MS; font-size: xx-large">付款通知</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <asp:Label runat="server" ID="labConStatus" />&nbsp;&nbsp;<asp:Label runat="server"
                    ID="lblTel" />&nbsp;&nbsp;<asp:Label runat="server" ID="lblMobile" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                <asp:Label runat="server" ID="lblAddress"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                邮政编码：
                <asp:Label runat="server" ID="lblPostCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                公司名称：
                <asp:Label runat="server" ID="lblCompany"></asp:Label>
            </td>
            <td align="right">
                日期：
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                &nbsp;
            </td>
            <td align="right">
                帐单号：<asp:Label runat="server" ID="lblPaymentCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="1" align="center" cellpadding="1" cellspacing="1">
        <tr>
            <td style="background-color: Gray; font-size: small; font-weight:bold" align="center" colspan="2" width="80%">
                <font color="white">描述</font>
            </td>
            <td style="background-color: Gray; font-size: small;font-weight:bold" align="center" colspan="2" width="20%">
               <font color="white"> 金额</font>
            </td>
        </tr>
        <tr>
            <td width="80%" colspan="2">
                &nbsp;
            </td>
            <td width="20%" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                项目号：<asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
            <td colspan="2" align="right" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                &nbsp;
            </td>
            <td colspan="2" align="right" width="20%">
                人民币
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
            </td>
            <td colspan="2" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                &nbsp;
            </td>
            <td colspan="2" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                <asp:Label ID="lblPaymentDesc" runat="server">&nbsp;</asp:Label>
            </td>
            <td colspan="2" align="right" width="20%">
                <asp:Label ID="lblPaymentAmount" runat="server">&nbsp;</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                &nbsp;
            </td>
            <td colspan="2" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                &nbsp;
            </td>
            <td colspan="2" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                &nbsp;
            </td>
            <td colspan="2" width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%" align="right">
                总额：
            </td>
            <td colspan="2" align="right" width="20%">
                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                备注: 请在收到帐单日即日付款,并将款项用电汇方式付到如下帐号:
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                开户名称：
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                银行帐号：
                <asp:Label ID="lblBankAccount" runat="server"></asp:Label>
            </td>
            <td colspan="2" style="border-bottom:1px dotted #000000" width="20%" align="left">
                &nbsp;
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                银行名称：
                <asp:Label ID="lblBankName" runat="server"></asp:Label>
            </td>
            <td colspan="2" align="left" width="20%">
                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" width="80%">
                银行地址：
                <asp:Label ID="lblBankAddress" runat="server"></asp:Label>
            </td>
            <td colspan="2" align="left" width="20%">
                代表签字：
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="25" align="right">
                &nbsp;
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_11.gif" width="50" height="20" hspace="1" vspace="5"
                        onclick="window.print();" /></a>
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_13.gif" width="50" height="20" vspace="5" onclick="window.close();" /></a>
            </td>
        </tr>
    </table>
</body>
</html>
