<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_PaymentGeneralList" CodeBehind="PaymentGeneralList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>


    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
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
                                        <asp:TextBox ID="txtGlideNo" runat="server" MaxLength="200" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        申请单号:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtPrNo" runat="server" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        订单编号:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtOrderNo" runat="server" MaxLength="200" />
                                    </td>
                                    <td class="oddrow">
                                        初审人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtAudit" runat="server" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        收货人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtgoods_receiver" runat="server" MaxLength="200" />
                                    </td>
                                    <td class="oddrow">
                                        供应商名称:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">分公司审核人:</td>
                                    <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtFilAuditor" MaxLength="50" runat="server" /></td>
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
                                PageSize="20" AllowPaging="True" Width="100%" AllowSorting="true" OnRowDataBound="gvG_RowDataBound"
                                OnPageIndexChanging="gvG_PageIndexChanging">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%">
                                        <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="申请单号" DataField="prNo" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="7%">
                                        <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="订单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <%#Eval("orderid") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%">
                                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="13%">
                                        <ItemStyle HorizontalAlign="Center" Width="13%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="采购总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####") : "￥" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="6%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="分公司审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("Filiale_Auditor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("Filiale_Auditor").ToString())) %>');">
                                                <%# Eval("Filiale_AuditName")%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                <%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="付款申请" ItemStyle-Width="6%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="hyl" ImageUrl="../../images/dc.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="押金" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="hylForeGift" style="cursor:pointer" ImageUrl='<%# (Eval("foregift") == DBNull.Value || decimal.Parse(Eval("foregift")==DBNull.Value ? "0" : Eval("foregift").ToString()) == 0m ) ? "" : "~/images/dc.gif"%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
</asp:Content>
