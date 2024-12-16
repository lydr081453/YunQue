<%@ Page Title="选择物料类别" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_selectProductTypes" Codebehind="selectProductTypes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <table width="100%">
        <tr><td><input type="button" class="widebuttons" value="关闭" onclick="window.close();" /></td></tr>
        <tr>
            <td>
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
                                                        <input type="checkbox" name="chk" id="chk" value="<%# Eval("typeid")+"-"+Eval("Amounts") %>" />&nbsp;<asp:Literal ID="lab" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px; padding-top: 5px;" valign="top">
                                                        <asp:DataList ID="dg3" runat="server" Width="100%" RepeatColumns="1" ItemStyle-Height="25px"
                                                            ItemStyle-VerticalAlign="Top" OnItemDataBound="dg3_ItemDataBound"
                                                            RepeatDirection="Horizontal">
                                                            <ItemTemplate>
                                                                    <%# Eval("typename") %>
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
            </td>
        </tr>
        <tr><td><asp:Button ID="btnOk" runat="server" Text=" 确认 " OnClientClick="return checkS();" CssClass="widebuttons" OnClick="btnOk_Click" />&nbsp;<input type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" /></td></tr>
    </table>
    <script language="javascript">
        function checkS() {
            var chks = document.getElementsByName("chk");
            for (i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    return true;
                }
            }
            alert(' - 请选择物料类别！');
            return false;
        }
    </script>
</asp:Content>

