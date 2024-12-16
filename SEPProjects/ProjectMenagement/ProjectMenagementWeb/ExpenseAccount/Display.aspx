<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.Display"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript">


    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                报销信息
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 10%">
                申请单号:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="lblReturncode" />
            </td>
               <td class="oddrow" style="width: 10%">
                单据类型:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="labReturnType" />
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
                预计报销金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
        <tr runat="server" id="trCheckNo1">
            <td class="oddrow">
                开户行:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblBank" runat="server" />
            </td>
            <td class="oddrow">
                帐号名称:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblAccountName" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trCheckNo2">
            <td class="oddrow">
                银行帐号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblAccount" runat="server" />
            </td>
            <td class="oddrow">
                银行地址:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblBankAddress" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trCheckNo">
            <td class="oddrow">
                支票号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblCheckNo" runat="server" />
            </td>
        </tr>
        <tr>
        <td class="oddrow">
            备注:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labMemo" runat="server" />
        </td>
    </tr>
        <tr>
            <td class="oddrow">
                审批意见:
            </td>
            <td class="oddrow-l" colspan="3">
                <%--<asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="60%" Rows="6"></asp:TextBox>--%>
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnReturn" runat="server" Text=" 关闭 " OnClientClick="window.close();"
                    CssClass="widebuttons" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                报销明细
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                    OnRowDataBound="gvG_RowDataBound" OnRowCreated="gvG_RowCreated" EmptyDataText="暂时没有相关记录"
                    AllowPaging="false" Width="100%">
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
                        <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
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
                         <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ShowSummary="false" />
            </td>
        </tr>
    </table>
</asp:Content>
