<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_PaymentInfo"
    CodeBehind="PaymentInfo.ascx.cs" %>

<script type="text/javascript">
    function PaymentContentClick() {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/PaymentDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl + '&<% =ESP.Finance.Utility.RequestName.Operate %>=' + operate, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        return false;
    }
    function btnRetClick() {
        document.getElementById("<%=btnRet.ClientID%>").click();
    }

    function EditPayment(paymentid) {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/PaymentDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.PaymentID %>=' + paymentid + '&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl + '&<% =ESP.Finance.Utility.RequestName.Operate %>=' + operate, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        return false;
    }
    function EditPaymentDetail(paymentid) {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/PaymentDetailDlg.aspx?PaymentId=' + paymentid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        return false;
    }
    function IsMaxLenPayCycle(maxlength) {
        var strOld = document.getElementById("<% =txtPayCycle.ClientID %>").value;
        if (strOld.length > maxlength) {
            document.getElementById("<% =txtPayCycle.ClientID %>").value = document.getElementById("<% =txtPayCycle.ClientID %>").value.substring(0, maxlength)
        }
    }
    function IsMaxLenCustomerRemark(maxlength) {
        var strOld = document.getElementById("<% =txtCustomerRemark.ClientID %>").value;
        if (strOld.length > maxlength) {
            document.getElementById("<% =txtCustomerRemark.ClientID %>").value = document.getElementById("<% =txtCustomerRemark.ClientID %>").value.substring(0, maxlength)
        }
    }
</script>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading" colspan="4">④ 付款通知&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--<img align="bottom" id="imgPayment" src="/images/a3_07.jpg" style="cursor: hand" onclick="return PaymentContentClick();" alt="添加付款通知信息" />--%>
                    <asp:Button ID="btnAddCost" runat="server" OnClientClick="return PaymentContentClick();"
                        Text="添加付款通知信息" CssClass="widebuttons" />
                    &nbsp;<font color="red">*</font>
                    <asp:LinkButton runat="server" ID="btnRet" OnClick="btnRet_Click" />
                </td>
            </tr>
            <tr id="trNoRecord" runat="server" visible="false">
                <td colspan="4">
                    <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                        <tr class="Gheading" align="center">
                            <th scope="col">序号
                            </th>
                            <th scope="col">付款通知时间
                            </th>
                            <th scope="col">付款通知内容
                            </th>
                            <th scope="col">付款通知金额
                            </th>
                            <th scope="col">备注
                            </th>
                        </tr>
                        <tr class="td" align="left">
                            <td colspan="5" align="center">
                                <span>暂时没有相应的付款通知记录</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trGrid" runat="server" visible="true">
                <td class="oddrow" colspan="4">
                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
                        OnRowCommand="gvPayment_RowCommand" OnRowDataBound="gvPayment_RowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PaymentContent" HeaderText="付款通知内容" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />
                            <asp:TemplateField HeaderText="付款通知时间" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentPreDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentBudget" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                                />
                            <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <img src="/images/edit.gif" alt="编辑" style="cursor: hand" onclick="return EditPayment('<%#Eval("PaymentID") %>');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="明细" ItemStyle-Width="5%">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <img src="/images/edit.gif" alt="明细" style="cursor: hand" onclick="return EditPaymentDetail('<%#Eval("PaymentID") %>');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("PaymentID") %>'
                                        CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                        OnClientClick="return confirm('你确定删除吗？');" />
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
                            <td style="width: 15%; border: 0 0 0 0"></td>
                            <td style="width: 30%; border: 0 0 0 0"></td>
                            <td style="width: 15%; border: 0 0 0 0" align="right">
                                <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                            </td>
                            <td style="width: 25%; border: 0 0 0 0">
                                <asp:Label ID="lblBlance" runat="server" Style="text-align: right" Width="100%" />
                            </td>
                            <td style="width: 15%; border: 0 0 0 0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">预计付款周期:
                </td>
                <td class="oddrow-l" colspan="3" style="width: 85%">
                    <asp:TextBox ID="txtPayCycle" Rows="3" runat="server" onkeyup="return IsMaxLenPayCycle(500)" Width="50%"
                        Columns="100" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">客户特殊要求:
                </td>
                <td class="oddrow-l" colspan="3" style="width: 85%">
                    <asp:TextBox ID="txtCustomerRemark" Rows="3" runat="server" onkeyup="return IsMaxLenCustomerRemark(200)" Width="50%"
                        Columns="100" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">是否需第三方发票:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:RadioButton ID="chkIs3rdInvoice" runat="server" GroupName="3rdInvoice" Text="是" />&nbsp;<asp:RadioButton
                        ID="chkNot3rdInvoice" GroupName="3rdInvoice" runat="server" Text="否" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
