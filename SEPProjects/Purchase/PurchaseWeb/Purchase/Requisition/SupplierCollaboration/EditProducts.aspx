<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="PurchaseWeb.Purchase.Requisition.SupplierCollaboration.EditProducts" EnableEventValidation="false" CodeBehind="EditProducts.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>

    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page *//* Default tab */.AjaxTabStrip .ajax__tab_tab
        {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */.AjaxTabStrip .ajax__tab_hover .ajax__tab_tab
        {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */.AjaxTabStrip .ajax__tab_active .ajax__tab_tab
        {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */.AjaxTabStrip .ajax__tab_body
        {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }
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

    <script language="javascript" src="../../../public/js/DatePicker.js"></script>

    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidViewType1" runat="server" />
    <%--已经选中的一级物料类别ID--%>
    <asp:HiddenField ID="hidViewType2" runat="server" />
    <%--已经选中的二级物料类别ID--%>
    <table width="100%"><tr><td>
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
                                                                ItemStyle-VerticalAlign="Top" OnItemDataBound="dg3_ItemDataBound" OnItemCommand="dg3_ItemCommand"
                                                                RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk" CausesValidation="false" CommandArgument='<%# Eval("typeid") %>'
                                                                        CommandName="SET" Font-Underline="true" Font-Size="10px" ForeColor="Black" runat="server" />
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
    </td></tr></table>
</asp:Content>
