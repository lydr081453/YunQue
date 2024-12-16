<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmExpenseAccountView.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.ConfirmExpenseAccountView" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript">
        function ApplicantClick() {
            var username = document.getElementById("<% =txtApplicant.ClientID %>").value;
            var sysid = document.getElementById("<% =hidApplicantID.ClientID %>").value;
            var dept = document.getElementById("<% =hidGroupID.ClientID %>").value;
            username = encodeURIComponent ? encodeURIComponent(username) : escape(username);
            var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.SearchType %>=NextConfirm&UserSysID=' + sysid + '&<% =ESP.Finance.Utility.RequestName.UserName %>=' + username + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请单信息
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
                预计申请总金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                单据类型:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="labReturnType" />
            </td>
        </tr>
        <%--<tr>
        <td class="oddrow">
            备注:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labMemo" runat="server" />
        </td>
    </tr>--%>
        <tr>
            <td class="oddrow">
                审批记录:
            </td>
            <td class="oddrow-l" colspan="3">
                <%--<asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="60%" Rows="6"></asp:TextBox>--%>
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                确认收货金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <componentart:numberinput id="txtConfirmFee" runat="server" emptytext="0.00" numbertype="Number"></componentart:numberinput>
                <font color="red">* </font>
            </td>
        </tr>
        <tr runat="server">
            <td class="oddrow">
                附加收货人:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtApplicant" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return ApplicantClick();" />
                <input type="hidden" id="hidApplicantID" runat="server" />
                <input type="hidden" id="hidApplicantUserID" runat="server" />
                <input type="hidden" id="hidApplicantUserCode" runat="server" />
                <input type="hidden" id="hidGroupID" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                    ErrorMessage="请选择附加收货人!" ControlToValidate="txtApplicant"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnAudit" runat="server" Text=" 确认 " CssClass="widebuttons" OnClick="btnAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
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
                    OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="项目成本明细" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCostDetailName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpenseTypeName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labRecipient" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所在城市" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCity" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行账号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankAccountNo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
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
