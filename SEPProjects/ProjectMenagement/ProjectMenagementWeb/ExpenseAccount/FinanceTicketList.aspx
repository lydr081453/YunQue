<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="FinanceTicketList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.FinanceTicketList" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table style="width: 100%;">
        <tr>
            <td>
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
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                OnClick="btnSearchAll_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gdBatch" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdBatch_RowDataBound"
                    OnPageIndexChanging="gdBatch_PageIndexChanging" DataKeyNames="BatchID" PageSize="10" EmptyDataText="暂时没有相关记录"
                    PagerSettings-Mode="NumericFirstLast" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="BatchID" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BranchCode" HeaderText="公司代码" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="批次合计" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAmounts"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="返点金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblReAmounts"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款总额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotal"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hylEdit" ImageUrl="/images/edit.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="打印" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hylPrint" ImageUrl="/images/Icon_PrintPo.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
