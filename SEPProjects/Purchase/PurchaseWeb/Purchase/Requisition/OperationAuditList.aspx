<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_OperationAuditList" CodeBehind="OperationAuditList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>


    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
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
                                        申请人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox MaxLength="200" ID="txtRequestor" runat="server" />
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
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id,moneytype"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                                OnPageIndexChanging="gvG_PageIndexChanging" PagerSettings-Position="TopAndBottom">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="prNo" HeaderText="prNo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypAuditurl" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                <%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("Item_No").ToString() + " Total:" %><%# Eval("moneytype").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请单流向" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labRequisitionflow" Text='<%#Eval("requisitionflow") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypAuditurl1" runat="server" ImageUrl="../../images/audit_icon.gif"
                                                title="审核"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PRType" HeaderText="PRType" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       <%-- <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            业务审核历史
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l">
                            <cc2:NewGridView ID="gvLOG" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvLOG_RowDataBound"
                                PageSize="20" AllowPaging="True" Width="100%" PagerSettings-Position="TopAndBottom">
                                <Columns>
                                    <asp:BoundField HeaderText="申请单号" DataField="prNo" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField HeaderText="订单编号" DataField="orderId" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="项目号" DataField="project_code" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="13%" />
                                    <asp:BoundField HeaderText="供应商" DataField="supplier_name" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# State.requistionOrorder_state[int.Parse(Eval("status").ToString())].ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("auditTime") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# State.operationAudit_statusName[int.Parse(Eval("audtiStatus").ToString())]%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="审核备注" DataField="auditRemark" />
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypView" runat="server" ImageUrl="/images/dc.gif" title="查看"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc2:NewGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
    </table>
</asp:Content>
