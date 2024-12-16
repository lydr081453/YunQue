﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PaymentNotify.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.PaymentNotify" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script language="javascript" type="text/javascript">

        function checkAll() {
            var inputs = document.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox" && inputs[i].name != "selectAll") {
                    var e = inputs[i];
                    e.checked = document.getElementById("selectAll").checked;
                    if (e.checked)
                        document.getElementById('<%= hidItemIds.ClientID %>').value += e.value + ",";
                    else
                        removeIds(e.value);
                }
            }
        }

        function checkOne(item) {
            if (item.checked) {
                document.getElementById('<%= hidItemIds.ClientID %>').value += item.value + ",";
            } else {
                removeIds(item.value);
            }
        }

        function removeIds(itemValue) {
            var itemIds = document.getElementById('<%= hidItemIds.ClientID %>').value.split(",");
            document.getElementById('<%= hidItemIds.ClientID %>').value = "";
            for (var i = 0; i < itemIds.length; i++) {
                if (itemIds[i] != "" && itemIds[i] != itemValue) {
                    document.getElementById('<%= hidItemIds.ClientID %>').value += itemIds[i] + ",";
                }
            }
        }
    </script>
    <asp:HiddenField ID="hidItemIds" runat="server" />
 <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" class="tableForm">
                                            <tr>
                                                <td class="heading" colspan="4">
                                                    检索
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="width: 15%">
                                                    流水号:
                                                </td>
                                                <td class="oddrow-l" style="width: 35%">
                                                    <asp:TextBox ID="txtGlideNo" runat="server" MaxLength="300" />
                                                </td>
                                                <td class="oddrow" style="width: 15%">
                                                    申请单号:
                                                </td>
                                                <td class="oddrow-l" style="width: 35%">
                                                    <asp:TextBox ID="txtPrNo" runat="server" MaxLength="300"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">
                                                    供应商名称:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="300" />
                                                </td>
                                                <td class="oddrow">
                                                    申请人:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox ID="txtRequestor" runat="server" MaxLength="300" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">项目号:</td>
                                                <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtProjectCode" runat="server" MaxLength="20" /></td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="4" align="center">
                                                    <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnSend" runat="server" Text="发送邮件提醒" CssClass="widebuttons" OnClick="btnSend_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" id="tabTop" runat="server">
                                            <tr>
                                                <td width="50%">
                                                    <asp:Panel ID="PageTop" runat="server">
                                                        <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                                        <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                                        <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                                        <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                                                    </asp:Panel>
                                                </td>
                                                <td align="right" class="recordTd">
                                                    记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                            PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                                            OnPageIndexChanging="gvG_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <input type="checkbox" id="selectAll" name="selectAll" onclick="checkAll();" />全选
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <input type="checkbox" id="chkItem" runat="server" name="chkItem" onclick="checkOne(this);" value='<%# Eval("ReturnID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="流水号" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# int.Parse(Eval("PRID").ToString()).ToString("0000000") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="申请单号" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hypView" runat="server" Text='<%# Eval("PRNO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="付款单号" DataField="ReturnCode" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="项目号" DataField="projectCode" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <span class="userLabel" onclick="ShowMsg('<%# Eval("RequestorID").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("RequestorID").ToString())) %>');">
                                                            <%# Eval("requestEmployeeName")%></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="supplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="邮件提醒" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSend" runat="server" CommandArgument='<% # Eval("ReturnID") %>' Font-Underline="true" Text="<img title='发送邮件' src='../../images/Icon_Sendmail.gif' border='0' />"
                                                            CommandName="SendMail" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                            </Columns>
                                            <PagerSettings Visible="false" />
                                        </asp:GridView>
                                        <table width="100%" id="tabBottom" runat="server">
                                            <tr>
                                                <td width="50%">
                                                    <asp:Panel ID="PageBottom" runat="server">
                                                        <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                                        <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                                        <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                                        <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td align="right" class="recordTd">
                                                    记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>