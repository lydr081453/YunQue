<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_allMessage" CodeBehind="allMessage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" src="/public/js/dialog.js"></script>
    <script language="javascript">
        function show(id) {
            dialog("查看", "url:get?/Purchase/Message/MessageView.aspx?action=show&isback=1&id=" + id, "900px", "400px", "text");
        }
    </script>

    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                采购部新闻
            </td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td style="width: 33%" valign="top">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:PlaceHolder ID="phMessage" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%" valign="top">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:PlaceHolder ID="phMessageSH" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:PlaceHolder ID="phMessageGZ" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <input type="button" class="widebuttons" onclick="window.close();return false;" value=" 关闭 " />
</asp:Content>
