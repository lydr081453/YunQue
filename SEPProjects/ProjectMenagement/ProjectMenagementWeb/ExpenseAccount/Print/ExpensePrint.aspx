<%@ Page Language="C#" AutoEventWireup="true" Inherits="ExpenseAccount_Print_ExpensePrint" CodeBehind="ExpensePrint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��ӡ������</title>
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
            font-size: 15px;
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
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #000000;
        }

        .font_12px_blue {
            font-size: 15px;
            color: #7282a9;
            line-height: 150%;
        }

        .font {
            font-size: 15px;
            color: #333;
            font-weight: bold;
        }
        /* -----------left_menu--------- */
        .left_menu {
            font-size: 15px;
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
            background-image: url(images/blue_inside.gif);
            background-repeat: no-repeat;
            font-size: 15px;
            color: #7282a9;
            font-weight: bold;
        }

        .list_title2 {
            font-size: 15px;
            color: #7282a9;
            font-weight: bold;
        }

        .list_nav {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 15px;
            color: #333;
            padding: 10px 0 10px 0;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #eaedf1;
        }

        .f_16px {
            font-size: 16px;
            color: #333;
            font-weight: bold;
        }

        .f_12px {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
        }

        .f_12px_bold {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            font-weight: bold;
        }

        .f_blue {
            font-size: 15px;
            color: #4d568c;
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
            font-size: 15px;
            color: #FFF;
            font-weight: bold;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 7px;
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
            color: #FFF;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 2px;
            background-repeat: no-repeat;
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
            font-size: 15px;
            color: #333333;
            font-weight: bold;
            padding: 0 0 0 10px;
        }

        .f_gray_left {
            font-size: 15px;
            color: #6e6e6e;
            font-weight: bold;
            padding: 0 0 0 10px;
        }

        .f_gray_right {
            font-size: 15px;
            color: #6e6e6e;
            padding: 0 0 0 10px;
        }

        .f_gray_left_withoutPadding {
            font-size: 15px;
            color: #6e6e6e;
            font-weight: bold;
        }

        .f_gray_right_withoutPadding {
            font-size: 15px;
            color: #6e6e6e;
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
            font-size: 15px;
            font-weight: bold;
            color: #FFF;
            padding: 0 0 0 3px;
        }

        .f12pxGgray {
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

        .f12pxGgray_right {
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

        .f12pxGgrayBold {
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

        .test_title_left {
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
            font-size: 15px;
        }

        .test_title_right {
            size: 14px;
            color: #000;
            font-weight: bold;
            border: 1px solid #a5c2a5;
            padding: 0 0 0 3px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 15px;
        }

        .f12pxGgray_right_2 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #333;
            padding: 0 0 0 3px;
            border: 1px solid #a5c2a5;
        }

        .f12pxGgrayBold_2 {
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

        .test_title {
            font-size: 15px;
            color: #000;
            font-weight: bold;
        }

        .widebuttons {
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

    <style type="text/css" media="print">
        .noprint {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td height="50"></td>
            </tr>
        </table>
        <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
                <td colspan="1" height="80" valign="bottom" style="width: 130px; padding: 0 0 5px 0;">
                    <asp:Image runat="server" ID="logoImg" Width="126" Height="70" /></td>
                <td align="center" colspan="7" valign="middle" style="padding: 0 0 5px 0; font-size: 24px;">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label></td>

            </tr>
            <tr>
                <td align="Right" colspan="8" style="padding-top: 15px;">
                    <asp:Label runat="server" Style="color: red; font-size: 15px;" ID="lblRefPn" /></td>
            </tr>
            <tr>
                <td colspan="8">
                    <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr style="height: 20px">
                            <td class="test_title_right" style="font-size: 15px;">PN No.
                            </td>
                            <td class="test_title_right" style="font-size: 15px;">
                                <asp:Label runat="server" Style="color: red; font-size: 15px;" ID="lblPN" />
                            </td>
                            <td class="test_title_right" style="font-size: 15px;">�ύ����
                            </td>
                            <td class="test_title_right" style="font-size: 15px;">
                                <asp:Label runat="server" Style="color: red; font-size: 15px;" ID="lblRequestDate" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="test_title_right" style="font-size: 15px;">��Ŀ��
                            </td>
                            <td class="f12pxGgray_right" style="font-size: 15px;">
                                <asp:Literal ID="litProjectCode" runat="server" />
                            </td>
                            <td class="test_title_right" style="font-size: 15px;">������
                            </td>
                            <td class="f12pxGgray_right" style="font-size: 15px;">
                                <asp:Literal ID="litRec" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="test_title_right" style="font-size: 15px;">��������
                            </td>
                            <td class="f12pxGgray_right" style="font-size: 15px;">
                                <asp:Literal ID="litBank" runat="server" />
                            </td>
                            <td class="test_title_right" style="font-size: 15px;">�����˺�
                            </td>
                            <td class="f12pxGgray_right" style="font-size: 15px;">
                                <asp:Literal ID="litAccount" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td class="test_title_right" style="font-size: 15px;">����������
                            </td>
                            <td colspan="3" class="f12pxGgray_right" style="font-size: 15px;">
                                <asp:Literal ID="litGroup" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />

                </td>
            </tr>

            <tr style="height: 40px;">
                <td align="center" class="test_title_right" style="font-size: 15px;">���÷�������</td>
                <td colspan="6" align="center" class="test_title_right" style="font-size: 15px;">������ϸ����</td>

                <td align="center" class="test_title_right" style="font-size: 15px; width: 110px;">������</td>
            </tr>
            <asp:Repeater runat="server" ID="repExpense" OnItemDataBound="repExpense_OnItemDataBound">
                <ItemTemplate>
                    <tr style="height: 40px;">
                        <td align="center" class="f12pxGgray" style="font-size: 15px;">
                            <asp:Label ID="labReturnFactDate" runat="server" />&nbsp;</td>
                        <td align="left" colspan="6" class="f12pxGgray" style="font-size: 15px;">
                            <asp:Label ID="labReturnContent" runat="server" />&nbsp;</td>
                        <td align="right" class="f12pxGgray_right" style="font-size: 15px;">
                            <asp:Label ID="labPreFee" runat="server" />&nbsp;</td>
                    </tr>
                    <asp:Literal runat="server" ID="liter"></asp:Literal>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater runat="server" ID="repReceiving" OnItemDataBound="repReceiving_OnItemDataBound">
                <ItemTemplate>
                    <tr style="height: 40px;">
                        <td align="center" class="f12pxGgray" style="font-size: 15px;">
                            <asp:Label ID="labReturnFactDate" runat="server" />&nbsp;</td>
                        <td align="left" colspan="6" class="f12pxGgray" style="font-size: 15px;">
                            <asp:Label ID="labReturnContent" runat="server" />&nbsp;</td>

                        <td align="right" class="f12pxGgray_right" style="font-size: 15px;">
                            <asp:Label ID="labPreFee" runat="server" />&nbsp;</td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td class="f12pxGgray">&nbsp;</td>
                <td align="center" height="20" class="f12pxGgrayBold" colspan="6" style="font-size: 15px; background-color: #66ff99;">�ϼ�:</td>
                <td class="f12pxGgray_right" align="right" style="font-size: 15px; background-color: #66ff99;">
                    <asp:Label ID="lab_TotalPrice" runat="server" />&nbsp;</td>
            </tr>
            <tr>
                <td height="10"></td>
            </tr>
        </table>
        <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" align="left" style="font-size: 10px">
                    <asp:Label runat="server" ID="labSuggestion" /></td>
                <td>&nbsp;</td>

            </tr>
        </table>
        <table width="630px" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr class="noprint">
                <td height="20" align="center" valign="bottom" class="white_font">&nbsp;</td>
                <td width="1">
                    <img src="images/space.gif" width="1" height="1" /></td>
                <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif" class="white_font"><a style="cursor: pointer" onclick="javascript:window.print();">��ӡ</a></td>
                <td width="1">
                    <img src="images/space.gif" width="1" height="1" /></td>
                <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg (1).gif" class="white_font"><a style="cursor: pointer" onclick="javascript:window.close();">�ر�</a></td>
            </tr>
        </table>

    </form>
</body>
</html>
