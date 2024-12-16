<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectSupporterDisplay"
    CodeBehind="ProjectSupporterDisplay.ascx.cs" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            支持方信息
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
                        支持方
                    </th>
                    <th scope="col">
                        支持方负责人
                    </th>
                    <th scope="col">
                        支持方费用
                    </th>
                    <th scope="col">
                        不含增值税金额
                    </th>
                    <th scope="col">
                        附加税
                    </th>
                    <th scope="col">
                        服务类型
                    </th>
                    <th scope="col">
                        业务描述
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="8" align="center">
                        <span>暂时没有相应的支持方记录</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvSupporter" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                        OnRowDataBound="gvSupporter_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SupportID" HeaderText="支持方ID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GroupID" HeaderText="GroupID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支持方" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LeaderUserID" HeaderText="LeaderUserID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("LeaderEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支持方费用" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetAllocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="不含增值税金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetNoVAT" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="附加税" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaxVAT" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ServiceType" HeaderText="服务类型" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField DataField="ServiceDescription" HeaderText="业务描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 5%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 15%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
