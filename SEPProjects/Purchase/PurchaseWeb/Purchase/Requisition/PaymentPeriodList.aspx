<%@ Page Title="选择帐期" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_PaymentPeriodList" Codebehind="PaymentPeriodList.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                付款账期列表
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvPayment" runat="server" Width="100%" DataKeyNames="id" AutoGenerateColumns="false"
                     OnRowDataBound="gvPayment_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="选择">
                            <ItemTemplate>
                                <input type="radio" id="radPeriod" name="radPeriod" value='<%# Eval("id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期类型"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期基准点"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodDay")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="日期类型"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="预计支付时间"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="预计支付金额"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="预计支付百分比"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("expectPaymentPercent") + "%"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodRemark")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="left">
                <input type="button" value=" 支付 " id="btnCommit" runat="server" class="widebuttons" onclick="if(!check()){alert('- 请选择收货单');return false;} else{ if(confirm('您确定要支付吗？')){this.disabled=true;}else{ return false;}}" onserverclick="btnCommit_Click" />
                &nbsp;<input type="button" value=" 关闭 " class="widebuttons" onclick="window.close();" />
            </td>
        </tr>
    </table>
    <script language="javascript">
        function check() {
            var rads = document.getElementsByName("radPeriod");
            for (var i = 0; i < rads.length; i++) {
                if (rads[i].checked == true) {
                    return true;
                }
            }
            return false;
        }
    </script>
</asp:Content>
