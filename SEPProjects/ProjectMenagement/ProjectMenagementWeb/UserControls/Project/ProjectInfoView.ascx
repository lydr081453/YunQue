<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectInfoView"
    CodeBehind="ProjectInfoView.ascx.cs" %>

<script type="text/javascript" src="/public/js/DatePicker.js"></script>

<script type="text/javascript">
    var ContractCount = 0;
    function checked() {
        ContractCount = ContractCount + 1;
    }
    function Checking() {
        if (ContractCount >= parseInt('<%=ContractCount %>')) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<asp:HiddenField ID="hidRecharge" runat="server" Value="false" />
<table width="100%" class="TableForm">
    <tr>
        <td class="heading" colspan="4">
            ③ 合同信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            公司选择:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="txtBranchName" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目总金额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtTotalAmount" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            合同税率:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtTaxRate" runat="server"></asp:Label>
        </td>
    </tr>
    <%if (hidRecharge.Value == "true")
              {%>

            <% if (gvMedia.Rows.Count <=1)
               { %>
            <tr>
                <td class="oddrow" style="width: 15%;">媒体付款主体:
                </td>
                <td class="oddrow-l">
                    <asp:HiddenField ID="hidMediaId" runat="server" />
                    <asp:Label runat="server" ID="txtMediaName" /></td>
                <td class="oddrow">预估媒体成本比例：</td>
                <td class="oddrow-l">
                    <asp:Label ID="txtSupplierCostRate" runat="server" /></td>
            </tr>
            <tr>

                <td class="oddrow" style="width: 15%;">充值金额：</td>
                <td class="oddrow-l" style="width: 35%;">
                    <asp:Label ID="txtRechargeAmount" runat="server" />
                                    <td class="oddrow">预估媒体成本：</td>
                <td class="oddrow-l">
                    <asp:Label ID="labSupplierCost" runat="server" /></td>

            </tr>
    <%}else{ %>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvMedia" runat="server" AllowPaging="false" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="媒体付款主体" DataField="MediaName" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="充值金额" DataField="Recharge" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="预估媒体成本比例" DataField="CostRate" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预估媒体成本" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (decimal.Parse(Eval("Recharge").ToString()) * decimal.Parse(Eval("CostRate").ToString())).ToString("#,##0.00") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="起始时间" DataField="BeginDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField HeaderText="结束时间" DataField="EndDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    <%} %>

            <tr>
            <td class="oddrow" style="width: 15%;">预估客户返点比例：</td>
                <td class="oddrow-l">
                    <asp:Label ID="txtCustomerRebateRate" runat="server" /> </td>
                <td class="oddrow">预估客户返点金额：</td>
                <td class="oddrow-l">
                    <asp:Label ID="labCustomerRebateRatePrice" runat="server" /></td>
            </tr>
            <tr>

                <td class="oddrow">应收账款：</td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label ID="labAccountsReceivable" runat="server" /></td>
            </tr>
            <%} %>
    <tr>
        <td class="oddrow">
            不含增值税金额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTotalNoVAT" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            附加税（主申请方）:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTaxFee" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            支持方合计:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTotalSupporter" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            附加税（支持方）:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTaxSupporter" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            合同服务费:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblServiceFee" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            成本合计:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="lblCostTot"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目毛利率（%）:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblProfileRate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            业务起始日期:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server" />
        </td>
        <td class="oddrow" style="width: 20%">
            预计结束日期:
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server" />&nbsp;
        </td>
    </tr>
    <%--    <tr id="trPercent">
        <td class="oddrow">
            预计各月完工百分比
        </td>
        <td id="tdPercent" class="oddrow-l" colspan="3">
            <asp:Label ID="txtPercent" runat="server" Width="100%"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td class="heading">
            申请文件上传
        </td>
    </tr>
    <tr id="trNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        合同描述
                    </th>
<%--                    <th scope="col">
                        合同金额
                    </th>--%>
                    <th scope="col">
                        是否有效
                    </th>
                    <th scope="col">
                        上一版合同
                    </th>
                    <th scope="col">
                        附件
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center">
                        <span>暂时没有上传的合同信息</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGrid" runat="server" visible="true">
        <td colspan="4">
            <asp:GridView ID="gvContracts" Width="100%" runat="server" DataKeyNames="ContractID"
                AutoGenerateColumns="false" OnRowDataBound="gvContracts_RowDataBound" EmptyDataText="还没有上传合同信息">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="30%" HeaderText="合同描述" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDes" runat="server" Text='<%# Eval("Description") %>' ToolTip='<%# Eval("Description") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Width="100px" Text='<%# Eval("TotalAmounts") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblUsable" runat="server" Text="是"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="上一版合同" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOldContract" runat="server" Text="无"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?ContractID=<%# Eval("ContractID") %>'
                                onclick="return checked();">
                                <img src="/images/ico_04.gif" border="0" /></a>
                            <asp:HiddenField ID="hidId" runat="server" Value='<%# Eval("ContractID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
