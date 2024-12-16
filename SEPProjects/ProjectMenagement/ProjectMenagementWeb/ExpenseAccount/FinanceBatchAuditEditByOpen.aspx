<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditEditByOpen.aspx.cs" EnableEventValidation="false"
    Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditEditByOpen" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript">

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                单据信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                PN号:
            </td>
            <td class="oddrow-l" style="width: 90%" colspan="3">
                <asp:Label runat="server" ID="labPNcode" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                申请人:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
            </td>
            <td class="oddrow" style="width: 10%">
                申请日期:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="labRequestDate" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                项目号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtproject_code1" runat="server" />
            </td>
            <td class="oddrow">
                项目名称:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtproject_descripttion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                成本所属组:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
            <td class="oddrow">
                预计申请金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
        <asp:Panel runat="server" ID="diffTr">
            <tr>
                <td class="oddrow">
                    原借款单金额:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labReturnFactFee" runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="hylPrint"
                        runat="server" ToolTip="查看明细" Width="90px"></asp:HyperLink>
                </td>
                <td class="oddrow">
                    差额:
                </td>
                <td class="oddrow-l">
                    <asp:Label runat="server" ID="labDifferenceFee" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">
                单据类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="labReturnType" />
            </td>
            <asp:Panel ID="panInvoice" runat="server" Visible="false">
                <td class="oddrow" style="width: 15%">
                    是否有发票:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">未开</asp:ListItem>
                        <asp:ListItem Value="1">已开</asp:ListItem>
                        <asp:ListItem Value="2">无需发票</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </asp:Panel>
        </tr>
        <tr>
            <td class="oddrow">
                历次审批记录:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                审批意见:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="40%" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click"
                    CausesValidation="false" />&nbsp;&nbsp;
                <asp:Button ID="btnUnAudit" runat="server" Text="驳回至申请人" CssClass="widebuttons" OnClick="btnUnAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" />
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
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" OnRowCreated="gvG_RowCreated"
                    EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="项目成本明细" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCostDetailName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpenseTypeName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labRecipient" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所在城市" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCity" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行名称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行账号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankAccountNo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑金额">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="faEdit" runat="server" Text="<img title='编辑金额' src='../../images/edit.gif' border='0' />"
                                    CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="faEdit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ShowSummary="false" />
            </td>
        </tr>
    </table>
</asp:Content>
