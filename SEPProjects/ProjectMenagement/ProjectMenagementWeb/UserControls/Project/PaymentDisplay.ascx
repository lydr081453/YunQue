<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Project_PaymentDisplay" CodeBehind="PaymentDisplay.ascx.cs" %>


<script type="text/javascript">
    function EditPayment(paymentid) {
        var backurl = window.location.pathname;
        var win = window.open('/Dialogs/PaymentReportDlg.aspx?<% =ESP.Finance.Utility.RequestName.PaymentID %>=' + paymentid + '&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function EditPaymentDetail(paymentid) {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/PaymentDetailDlg.aspx?PaymentId=' + paymentid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function EditPaymentSchedule(paymentid) {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/PaymentScheduleDlg.aspx?PaymentId=' + paymentid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

</script>


<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">④ 付款通知信息
        </td>
    </tr>
    <tr id="trNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">序号</th>
                    <th scope="col">付款通知时间</th>
                    <th scope="col">付款通知内容</th>
                    <th scope="col">付款通知金额</th>
                    <th scope="col">备注</th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="5" align="center"><span>暂时没有相应的付款通知记录</span></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4">
            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
                OnRowDataBound="gvPayment_RowDataBound" OnRowCommand="gvPayment_RowCommand" Width="100%">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="付款通知时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentPreDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PaymentContent" HeaderText="付款通知内容" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentBudget" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="15%" />
                    <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <img src="/images/edit.gif" alt="编辑" style="cursor: pointer" onclick="return EditPayment('<%#Eval("PaymentID") %>');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="明细" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <img src="/images/dc.gif" alt="明细" style="cursor: pointer" onclick="return EditPaymentDetail('<%#Eval("PaymentID") %>');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="回款" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <img src="/images/AuditStatus.gif" alt="回款" style="cursor:pointer;" onclick="return EditPaymentSchedule('<%#Eval("PaymentID") %>');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导出" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                        <ItemTemplate>
                              <a id="aDownLoad" target="_blank" href='ExportPayment.aspx?PaymentID=<%# Eval("PaymentID") %>'>
                                            <img src="/images/Icon_Output.gif" border="0" /></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
        </td>
    </tr>
    <tr id="trTotal" runat="server">
        <td class="oddrow-l" colspan="6" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 15%; border: 0 0 0 0"></td>
                    <td style="width: 40%; border: 0 0 0 0"></td>
                    <td style="width: 15%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                    <td style="width: 30%; border: 0 0 0 0">
                        <asp:Label ID="lblBlance" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">预计付款周期:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblPayCycle" runat="server" Width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">客户特殊要求:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblCustomerRemark" runat="server" Width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">是否需第三方发票:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lbl3rdInvoice" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">预计各月完工百分比
        </td>
    </tr>
    <tr id="trPercentNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">序号
                    </th>
                    <th scope="col">年
                    </th>
                    <th scope="col">月
                    </th>
                    <th scope="col">完工百分比(%)
                    </th>
                    <th scope="col">当月Fee
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center">
                        <span>没有填写预计完工百分比信息</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trPercent" runat="server" visible="true">
        <td colspan="4">
            <asp:GridView ID="gvPercent" Width="100%" runat="server" DataKeyNames="ScheduleID"
                AutoGenerateColumns="false" EmptyDataText="没有填写预计完工百分比信息" OnRowDataBound="gvPercent_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YearValue" HeaderText="年" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:BoundField DataField="monthValue" HeaderText="月" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="完工百分比(%)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("MonthPercent") == null ? "0.00" : Convert.ToDecimal(Eval("MonthPercent")).ToString("0.00") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") == null ? "0.00" : Convert.ToDecimal(Eval("Fee")).ToString("#,##0.00")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trPercentTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 5%; border: 0 0 0 0"></td>
                    <td style="width: 20%; border: 0 0 0 0"></td>
                    <td style="width: 20%; border: 0 0 0 0"></td>
                    <td style="width: 20%; border: 0 0 0 0">
                        <asp:Label ID="lblTotalPercent" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                    <td style="width: 20%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotalFee" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
