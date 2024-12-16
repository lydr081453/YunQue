<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeManager.aspx.cs"
    Inherits="PurchaseWeb.Gift.view.ExchangeManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<title>无标题文档</title>
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 901px;
        }
    </style>
</head>
<body>
    <form runat="server" id="form1">
    <table border="0" align="center" cellpadding="0" cellspacing="0" id="head">
        <tr>
            <td valign="top">
                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 21%">
                            &nbsp;
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(images/jifen-06.jpg);
                                width: 144px; height: 38px; background-repeat: no-repeat; margin: 220px 0 0 10px;
                                text-align: center; color: White;">
                                <tr>
                                    <td style="font-size: 14px; font-family: @宋体; color: White;">
                                        我的积分:<asp:Label ID="lblMyPoints" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="margin-bottom: 20px;">
        <tr>
            <td style="width: 17%">
                &nbsp;
            </td>
            <td>
                <table border="0" align="left" cellpadding="0" cellspacing="0" style="margin-bottom: 20px;">
                    <tr>
                        <asp:DataList ID="rpGift" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <td width="18%">
                                    <table border="0" cellpadding="1" cellspacing="1" bgcolor="#bababa">
                                        <tr>
                                            <td bgcolor="#FFFFFF">
                                                <img id="Img1" src='<%#DataBinder.Eval(Container.DataItem,"Images")%>' height="146"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" bgcolor="#bababa" style="margin-top: 10px;">
                                        <tr>
                                            <td valign="top" bgcolor="#FFFFFF">
                                                <strong>
                                                    <asp:CheckBox ID="cboItem" runat="server" />
                                                    <asp:Label ID="lblModelId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GiftID")%>'
                                                        Visible="false" />
                                                </strong>
                                            </td>
                                            <td bgcolor="#FFFFFF" align="center">
                                                <font style="font-weight: bold; font-size: 16px;"> <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>'></asp:Label></font><br />
                                                 <font style="font-size: 14px;">库存:
                                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Count")%>'></asp:Label>
                                                件<br/>需要积分:
                                                <asp:Label ID="Label23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Points")%>'></asp:Label>
                                                </font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </tr>
                </table>
            </td>
            <td style="width: 15%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 22%">
                &nbsp;
            </td>
            <td align="center" style="border-top: 1px dotted #830000; padding: 20px 0 10px 0;">
                <asp:ImageButton ID="btnExchangeGift" runat="server" OnClick="btnBack_Click" ImageUrl="images/jifen-04.jpg" />
            </td>
            <td style="width: 15%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 22%">
                &nbsp;
            </td>
            <td style="border-bottom: 2px solid #830000; padding: 10px 0 10px 0;">
                <img src="images/jifen-05.jpg" width="90" height="20" />
            </td>
            <td style="width: 15%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="margin-bottom: 20px;">
        <tr>
            <td style="width: 22%">
                &nbsp;
            </td>
            <td>
                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                    <asp:DataList ID="rpExchanged" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                        OnItemDataBound="rpExchanged_DataBound" Width="81%" HorizontalAlign="left">
                        <ItemTemplate>
                            <tr>
                                <td style="border-bottom: 1px dotted #830000; padding: 15px;">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="1" cellspacing="1" bgcolor="#bababa">
                                                    <tr>
                                                        <td bgcolor="#FFFFFF">
                                                            <asp:Image ID="imgGift" runat="server" Width="80" Height="60" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="padding-left: 20px;">
                                                <font style="font-weight: bold; font-size: 16px;">
                                                    <asp:Label ID="lblGiftName" runat="server"></asp:Label></font>
                                                <br />
                                                <font style="font-size: 14px;">库存:
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                    件&nbsp;&nbsp;&nbsp;&nbsp;需要积分:
                                                    <asp:Label ID="lblGiftPoint" runat="server" />
                                                </font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:DataList>
                </table>
            </td>
            <td style="width: 15%">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
