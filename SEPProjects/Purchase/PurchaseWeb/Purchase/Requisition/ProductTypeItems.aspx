<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="物料类别" Inherits="Purchase_Requisition_ProductTypeItems" Codebehind="ProductTypeItems.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">
        .border
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-bottom-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border2
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border_title_left
        {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_title_right
        {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_datalist
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CC3333;
        }
    </style>
    <script language="javascript">
        function openSuppliers(productTypeId) {
            var type = '<%= Request["type"] %>';
            win = window.open('SupplierListForSearch.aspx?productTypeId=' + productTypeId+'&type='+type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>
    <asp:Button runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();" />
    <br />
    <asp:Repeater ID="rep1" runat="server" OnItemDataBound="rep1_ItemDataBound">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="30">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="5%" height="15">
                                    &nbsp;<a id='<%# Eval("typename") %>' />
                                </td>
                                <td width="15%" rowspan="2" align="center">
                                    <asp:Label ID="lab" runat="server" Font-Size="14px" Font-Bold="true" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td height="15" class="border_title_left">
                                    &nbsp;
                                </td>
                                <td class="border_title_right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="rep2" runat="server" OnItemDataBound="rep2_ItemDataBound" CssClass="border_datalist"
                            Width="100%" ItemStyle-VerticalAlign="Top" RepeatColumns="4" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lab" runat="server" Font-Size="12px" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 40px; padding-top: 5px;" valign="top">
                                            <asp:DataList ID="dg3" runat="server" Width="100%" RepeatColumns="1" ItemStyle-Height="25px"
                                                ItemStyle-VerticalAlign="Top" OnItemDataBound="dg3_ItemDataBound"
                                                RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk" CausesValidation="false" 
                                                         Font-Underline="true" Font-Size="10px" ForeColor="Black" runat="server" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
