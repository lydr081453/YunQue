<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectContractCost"
    CodeBehind="ProjectContractCost.ascx.cs" %>

<script type="text/javascript">
    function ContractDetailClick() {
        var backurl = window.location.pathname;
        var win = window.open('/Dialogs/CostDetailDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        return false;
    }

    function EditCost(costid, costtype) {
        var backurl = window.location.pathname;
        var win = window.open('/Dialogs/CostDetailDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.ContractCostID %>=' + costid + '&<% =ESP.Finance.Utility.RequestName.CostType %>=' + costtype + '&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        return false;
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading" colspan="4">成本明细信息&nbsp;&nbsp<asp:Button
                    ID="btnAddCost" runat="server" OnClientClick="return ContractDetailClick();"
                    Text=" 添加 " CssClass="widebuttons" />
                    &nbsp;<font color="red">*</font>
                    <asp:LinkButton runat='server' ID="btnRet" OnClick="btnRet_Click" />
                    &nbsp;&nbsp;<a href="ProjectCostView.aspx?ProjectID=<%=Request["ProjectID"] %>" target="_blank" style="text-decoration: underline; color: Blue;">查看已使用成本</a>
                    <a name="top_A" />
                </td>
            </tr>
            <tr id="trNoRecord" runat="server" visible="false">
                <td colspan="4">
                    <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                        <tr class="Gheading" align="center">
                            <th scope="col">序号
                            </th>
                            <th scope="col">成本描述
                            </th>
                            <th scope="col">成本金额
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
                <td class="oddrow" colspan="4">
                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractCostID"
                        OnRowCommand="gvCost_RowCommand" Width="100%" OnRowDataBound="gvCost_RowDataBound">
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
                    <br />
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectExpenseID"
                        OnRowCommand="gvExpense_RowCommand" Width="100%" OnRowDataBound="gvExpense_RowDataBound">
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
                            <td style="width: 10%; border: 0 0 0 0"></td>
                            <td style="width: 40%; border: 0 0 0 0"></td>
                            <td style="width: 25%; border: 0 0 0 0"></td>
                            <td style="width: 25%; border: 0 0 0 0" align="right">
                                <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
