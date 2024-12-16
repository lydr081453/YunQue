<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TicketConfirm.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.TicketConfirm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                报销单信息&nbsp;&nbsp;<asp:Label runat="server" ID="lblReturnCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                申请人:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
            </td>
            <td class="oddrow" width="15%">
                申请日期:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label runat="server" ID="labRequestDate" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                项目号:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblProjectCode"></asp:Label>
            </td>
            <td class="oddrow">
                项目名称:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblProjectDesc"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                成本所属组:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblGroupName"></asp:Label>
            </td>
            <td class="oddrow">
                预计报销总金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请单明细
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false">
                    <Columns>
                        <asp:BoundField DataField="ExpenseDate" HeaderText="发生日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCostDetailName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpenseTypeName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="8%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                审批记录:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="center" colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnAuditConfirm" runat="server" Text=" 确认 " CssClass="widebuttons"
                    OnClick="btnAuditConfirm_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnAuditReturn" runat="server" Text=" 返回 " CssClass="widebuttons"
                    OnClick="btnAuditReturn_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
