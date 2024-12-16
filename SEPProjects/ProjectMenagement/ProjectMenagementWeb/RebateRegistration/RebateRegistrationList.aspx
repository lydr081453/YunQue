<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RebateRegistrationList.aspx.cs" Inherits="FinanceWeb.RebateRegistration.RebateRegistrationList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/ProjectTab.ascx" TagName="tab" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <table style="width: 100%;">
        <tr>
            <td width="100%" align="center">
                <uc1:tab ID="tab" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">检索
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">关键字:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtKey" runat="server" />
                        </td>
                        <td class="oddrow-l" colspan="2">
                            <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons" OnClick="btnSearchAll_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnImport" runat="server" Text=" 导入返点 " CssClass="widebuttons" OnClick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <table width="100%">
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="GvImport" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvImport_RowDataBound"
                                DataKeyNames="BatchID" PageSize="20" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                OnRowCommand="GvImport_RowCommand" OnPageIndexChanging="GvImport_PageIndexChanging"
                                AllowPaging="true" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BatchID" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="PurchaseBatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="操作日期" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="Amounts" HeaderText="总额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}" HtmlEncode="false"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Creator" HeaderText="创建人" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Description" HeaderText="描述信息" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="16%" />
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <a href="/project/ProjectWorkFlow.aspx?Type=RebateRegistration&FlowID=<%#Eval("BatchID") %>" target="_blank">
                                                <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-Width="7%">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <a href="RebateRegistrationView.aspx?batchId=<%#Eval("BatchID") %>">
                                                <img src="/images/dc.gif" border="0px;" title="查看"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" Text="<img title='删除消耗导入' src='../../images/disable.gif' border='0' />"
                                                OnClientClick="return confirm('您是否确认删除该条记录？');" CausesValidation="false" CommandArgument='<%# Eval("BatchId") %>'
                                                CommandName="Del" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印" ItemStyle-Width="7%">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <a target="_blank" href="RebateRegistrationPrint.aspx?batchId=<%#Eval("BatchID") %>">
                                                <img src="/images/PrintDefault.gif" border="0px;" title="打印"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
