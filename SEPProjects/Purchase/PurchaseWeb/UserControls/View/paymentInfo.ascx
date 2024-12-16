<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_View_paymentInfo"
    CodeBehind="paymentInfo.ascx.cs" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            账期设定
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 10%">
            账期类型:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            ⑤ 付款条件信息
        </td>
    </tr>
    <input type="hidden" value="0" id="hidTotalPrice" runat="server" />
    <tr runat="server" id="TrGridView">
        <td colspan="8" class="oddrow-l">
            <asp:GridView ID="gvPayment" runat="server" Width="100%" DataKeyNames="id" AutoGenerateColumns="false"
                OnRowDataBound="gvPayment_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="litNo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="账期类型" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="账期基准点" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="账期" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodDay")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="日期类型" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预计支付时间" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预计支付金额" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预计支付百分比" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("expectPaymentPercent") + "%"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="剩余金额" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Literal ID="LitOverplusPrice" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodRemark")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
