<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="BatchRepay.aspx.cs" Inherits="FinanceWeb.Purchase.BatchRepay" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table style="width: 100%">
        <tr>
            <td class="heading" colspan="4">
                批次付款信息
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">
                批次流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
              <asp:Label runat="server" ID="lblBatchId"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                 批次号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPurchaseBatchCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBatchCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款总额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblFee" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentType" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInvoice" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankName" runat="server" />
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
                <asp:Label ID="lblSupplierBankName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSupplierBankAccount" runat="server" />
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
            <td class="oddrow">
                重汇原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                        runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%">
                    <tr>
                        <td class="heading" colspan="4">
                            付款申请列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                                AllowPaging="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                                target="_blank">
                                                <%#Eval("ReturnCode")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPR"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="付款时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStatusName" />
                                            <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                            <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSupplier" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印预览" ItemStyle-Width="8%">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                                Width="4%"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr runat="server" id="trMediaHeader">
                        <td class="heading" colspan="4">
                            媒体稿费详细信息
                        </td>
                    </tr>
                    <tr runat="server" id="trMedia">
                        <td colspan="4">
                            <asp:GridView ID="gvMedia" runat="server" AutoGenerateColumns="False" DataKeyNames="MeidaOrderID"
                                OnRowDataBound="gvMedia_RowDataBound" EmptyDataText="暂时没有媒体记者记录" AllowPaging="false"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="MeidaOrderID" HeaderText="MeidaOrderID" />
                                    <asp:BoundField DataField="PaymentUserID" HeaderText="PaymentUserID" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" />
                                    <asp:BoundField DataField="ReturnCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="MediaName" HeaderText="媒体名称" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="ReporterName" HeaderText="记者名称" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="ReceiverName" HeaderText="收款人" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="CardNumber" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="CityName" HeaderText="所在城市" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BankName" HeaderText="开户行" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BankAccountName" HeaderText="账号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="支付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labTotalAmount" Text='<%# Eval("TotalAmount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="付款人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
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
                                        <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnCancelToRequest" Text=" 退回至申请人 " runat="server" CssClass="widebuttons" OnClick="btnCancelToRequest_Click" />
                &nbsp;
                <asp:Button ID="btnCancelToFinance" Text=" 退回至财务 " runat="server" CssClass="widebuttons"
                    OnClick="btnCancelToFinance_Click" />
                &nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
