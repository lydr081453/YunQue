﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_OrderCommitList" Title="Untitled Page" EnableEventValidation="false"
    CodeBehind="OrderCommitList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
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
                                        申请人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtRequestor" runat="server" MaxLength="300" />
                                    </td>
                                    <td class="oddrow">
                                        初审人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtAudit" runat="server" MaxLength="300" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        采购总金额:
                                    </td>
                                    <td class="oddrow-l">
                                        (Min)<asp:TextBox ID="txtTotalMin" runat="server" MaxLength="300"></asp:TextBox>----
                                        <asp:TextBox ID="txtTotalMax" runat="server" MaxLength="300"></asp:TextBox>(Max)
                                    </td>
                                    <td class="oddrow">
                                        申请单流向:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:DropDownList ID="ddlRequisitionflow" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        项目号:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtProjectCode" runat="server" MaxLength="20" />
                                    </td>
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
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id,moneytype"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                                OnPageIndexChanging="gvG_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <a href="OrderDetailTab.aspx?<% =RequestName.GeneralID %>=<%#Eval("id") %>&backUrl=OrderCommitList.aspx">
                                                <%#Eval("prNO") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="分公司审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("Filiale_Auditor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("Filiale_Auditor").ToString())) %>');">
                                                <%# Eval("Filiale_AuditName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                <%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("Item_No").ToString() %>&nbsp;Total:<%# Eval("moneytype").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="流向" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labRequisitionflow" Text='<%#Eval("requisitionflow") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="使用状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# ESP.Purchase.Common.State.PRInUse_Names[(Eval("inUse") == DBNull.Value ? (int)ESP.Purchase.Common.State.PRInUse.Use : int.Parse(Eval("inUse").ToString()))] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <a href="OrderDetailTab.aspx?<% =RequestName.GeneralID %>=<%#Eval("id") %>&backUrl=OrderCommitList.aspx&helpfile=3">
                                                <img src="../../images/dc.gif" border="0px;" title="查看"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="撤销" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnCancel" runat="server" Text="<img title='撤销申请单' src='../../images/Icon_Cancel.gif' border='0' />"
                                                OnClientClick="return confirm('您是否确认撤销？');" CausesValidation="false" CommandArgument='<%# Eval("id") %>'
                                                CommandName="Return" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印" ItemStyle-Width="3%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href='Print/RequisitionPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                target="_blank">
                                                <img title="打印申请单" src="../../images/Icon_PrintPr.gif" border='0' /></a>
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
</asp:Content>
