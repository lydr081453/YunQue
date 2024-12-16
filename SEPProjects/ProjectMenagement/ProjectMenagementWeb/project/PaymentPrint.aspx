<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPrint.aspx.cs" Inherits="FinanceWeb.project.PaymentPrint" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css" media="print">
        .noprint {
            display: none;
        }
    </style>
    <style type="text/css">
        body {
            margin: 0px;
        }

        img {
            border: none;
        }

        .nav {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

            .nav table {
                border-top-width: 1px;
                border-left-width: 1px;
                border-top-style: solid;
                border-left-style: solid;
                border-top-color: #999999;
                border-left-color: #999999;
            }

            .nav td {
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

            .nav em {
                font-style: normal;
                font-size: 14px;
                color: #CC6633;
                font-weight: bold;
            }

        .topline {
            border-top-width: 2px;
            border-top-style: solid;
            border-top-color: #999999;
        }
    </style>
</head>
<body>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 40px 0 10px 0;">
                    <tr>
                        <td width="50%">
                            <strong style="font-size:larger">付款通知</strong>
                        </td>
                        <td width="50%" align="right">
                            <img src="/images/xingyan.png" width="63" height="35" />
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left" valign="bottom">
                            <asp:Label ID="lblInvoiceTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="left" valign="bottom">人名：<asp:Label ID="lblCustomer" runat="server"></asp:Label>
                        </td>
                        <td width="50%" align="left" valign="bottom">日期：<asp:Label ID="lblPaymentPreDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="left" valign="bottom">地址：<asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                        <td width="50%" align="left" valign="bottom">帐单号：<asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" class="nav" style="padding: 10px 0 10px 0;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="80%" height="25" align="center" style="background-color: gray;">
                            <strong>描述</strong>
                        </td>
                        <td width="20%" align="center" style="background-color: gray;">
                            <strong>金额</strong>
                        </td>

                    </tr>
                    <asp:Literal runat="server" ID="ltPayment"></asp:Literal>
                    <tr>
                        <td colspan="4" align="right">总额：
                            <asp:Label runat="server" ID="lblTotalDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="color:gray;">
                            备注: 请在收到帐单日即日付款,并将款项用电汇方式付到如下帐号:
                        </td>
                    </tr>
                       <tr>
                        <td colspan="4" style="color:gray;">
                           &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">开户名称：<asp:Label runat="server" ID="lblBankTitle"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">开户帐号：
                            <asp:Label runat="server" ID="lblBankAccount"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">银行名称：<asp:Label runat="server" ID="lblBankName"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">银行地址：<asp:Label runat="server" ID="lblBankAddress"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <br /><br />
                            <br /><br /><br />
                            ---------------------------------------------
                            <br />
                            <asp:Label runat="server" ID="lblSign"></asp:Label><br />
                            代表签字
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="25" align="right">&nbsp;
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
