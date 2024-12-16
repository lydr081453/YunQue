<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="BatchTicketEdit.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.BatchTicketEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">
        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>

    <asp:HiddenField ID="hidBatchId" runat="server" />
    <table width="100%" border="0" background="/images/allinfo_bg.gif">
        <tr style="height: 30px">
            <td width="50%">
               <font style="font-weight: bold; font-size: 15px"> 创建人:<asp:Label ID="labCreateUser"
                    runat="server" /></font>
            </td>
            <td align="right">
                <font style="font-weight: bold; font-size: 15px">批次号:<asp:Label ID="labPurchaeBatchCode"
                    runat="server" /></font>
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                批次明细列表
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID" OnRowDeleting="gdG_RowDeleting"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                    AllowPaging="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnId" HeaderText="ReturnId" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReturnCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" />
                                       <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出票日期" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# DateTime.Parse(Eval("PReBeginDate").ToString()).ToString("yyyy-MM-dd") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="移除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ReturnID") %>'
                                    CommandName="Delete" Text="<img src='/images/disable.gif' title='移除' border='0'>"
                                    OnClientClick="return confirm('你确定移除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                批次合计:<asp:Label ID="labTotal" runat="server" />
            </td>
        </tr>
         <tr>
            <td align="right">
                返点金额:<asp:TextBox ID="txtRetAmount" runat="server" Text="0.00"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                审批信息
            </td>
        </tr>
        <tr>
            <td width="15%" class="oddrow">
                审批历史:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labAuditLog" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                审批备注:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRemark" runat="server" Width="50%" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark"
                    Display="None" runat="server" ErrorMessage="请填写审批备注"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td align="center">
                <asp:Button ID="btnYes" runat="server" Text=" 提交 " OnClientClick="if(Page_ClientValidate()){return confirm('您确定提交该批次吗？');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text=" 删除 " Visible="false" OnClientClick="return confirm('您确定删除该批次吗？');"
                    CssClass="widebuttons" OnClick="btnNo_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
</asp:Content>
