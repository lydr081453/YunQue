<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="FinanceReporterEdit.aspx.cs" Inherits="FinanceWeb.Purchase.FinanceReporterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>

    <script type="text/javascript">
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

    <uc1:TopMessage ID="TopMessage" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                采购付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款时间:
            </td>
            <td class="oddrow-l"  colspan="3">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                申请付款时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                帐期类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                成本所属组:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblPayRemark" runat="server" Width="80%" Height="80px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                供应商信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierAccount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款确认
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblFactFee"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPreDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblBankName"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                网银号/支票号
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblPayCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                媒体稿费详细信息
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="MeidaOrderID"
                    OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有媒体记者记录"  AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMedia" runat="server" Checked="false" Text='' />
                            </ItemTemplate>
                            <HeaderTemplate>
                                &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />是否付款
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MeidaOrderID" HeaderText="MeidaOrderID" />
                         <asp:BoundField DataField="PaymentUserID" HeaderText="PaymentUserID" />
                          <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" />
                        <asp:BoundField DataField="MediaName" HeaderText="媒体名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReporterName" HeaderText="记者名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReceiverName" HeaderText="收款人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CardNumber" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CityName" HeaderText="所在城市" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BankName" HeaderText="开户行" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BankAccountName" HeaderText="账号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="支付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labTotalAmount" Text='<%# Eval("TotalAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labPaymenter" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trTotal" runat="server">
            <td class="oddrow-l" colspan="4" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                        <td style="width: 20%; border: 0 0 0 0" align="left">
                            <asp:Label ID="lblTotal" runat="server" Style="text-align: left" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnOK" Text=" 确定 " CssClass="widebuttons" runat="server" OnClick="btnOK_Click" />&nbsp;
                <asp:Button ID="btnCancel" Text=" 返回 " CssClass="widebuttons" runat="server" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
