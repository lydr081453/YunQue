<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectContractCostView"
    CodeBehind="ProjectContractCostView.ascx.cs" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            成本明细信息&nbsp;&nbsp;<a href="ProjectCostView.aspx?ProjectID=<%=Request["ProjectID"] %>" target="_blank" style=" text-decoration:underline; color:Blue;">查看已使用成本</a>
        </td>
    </tr>
    <tr id="trNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        成本描述
                    </th>
                    <th scope="col">
                        成本金额
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="3" align="center">
                        <span>暂时没有相应的成本记录</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4" style="width: 100%">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractCostID"
                        OnRowDataBound="gvCost_RowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />
                            <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectExpenseID"
                Width="100%" OnRowDataBound="gvExpense_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="40%" />
                    <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="lblExpense" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
        </td>
    </tr>
    <tr id="trTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 40%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0"></td>
                    <td style="width: 25%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
