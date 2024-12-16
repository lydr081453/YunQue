<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchDetailEdit.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.FinanceBatchDetailEdit" MasterPageFile="~/MasterPage.master"%>

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
        <td class="oddrow" style="width:10%">
            申请人:
        </td>
        <td class="oddrow-l"  style="width:40%">
            <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
        </td>
        <td class="oddrow" style="width:10%">
            申请日期:
        </td>
        <td class="oddrow-l"  style="width:40%">
            <asp:Label runat="server" ID="labRequestDate" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" >
            项目号:
        </td>
        <td class="oddrow-l" >
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
        <td class="oddrow-l" >
            <asp:Label ID="labDepartment" runat="server" />
        </td>
        <td class="oddrow">
            预计报销金额:
        </td>
        <td class="oddrow-l" >
            <asp:Label runat="server" ID="labPreFee" />
        </td>
    </tr>
    <asp:Panel  runat="server" id="diffTr">
    <tr>
        <td class="oddrow">
            原借款单金额:
        </td>
        <td class="oddrow-l" >
            <asp:Label ID="labReturnFactFee" runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="hylPrint" runat="server" ToolTip="查看明细" Width="90px"></asp:HyperLink>
        </td>
        <td class="oddrow">
            差额:
        </td>
        <td class="oddrow-l" >
            <asp:Label runat="server" ID="labDifferenceFee" />
        </td>
    </tr>
    </asp:Panel>
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
            历史审批意见:
        </td>
        <td class="oddrow-l" colspan="3">
            <%--<asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="60%" Rows="6"></asp:TextBox>--%>
            <asp:Label runat="server" ID="labSuggestion"  />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            审批意见:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="40%" Rows="3"></asp:TextBox><font color="red"> * </font>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="请填写审批意见!" ControlToValidate="txtSuggestion" Display="None" ErrorMessage="请填写审批意见"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4">
            <asp:Button ID="btnUnAudit" runat="server" Text="驳回至申请人" CssClass="widebuttons" OnClick="btnUnAudit_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnUnAuditF" runat="server" Text="驳回至财务第一级" CssClass="widebuttons" OnClick="btnUnAuditF_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false" />
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
                 OnRowDataBound="gvG_RowDataBound"   OnRowCreated="gvG_RowCreated"
                EmptyDataText="暂时没有相关记录" 
                AllowPaging="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center"
                        DataFormatString="{0:d}"  />
                    <asp:TemplateField HeaderText="项目成本明细" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labCostDetailName" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labExpenseTypeName" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center" >
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
                    <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labRecipient" />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="所在城市" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labCity" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="银行名称" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labBankName" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="银行账号" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labBankAccountNo" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="hylEdit" runat="server"  ToolTip="编辑"
                                Width="4%"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
               <asp:GridView ID="gvRecipient" runat="server" AutoGenerateColumns="False"  OnRowDataBound="gvRecipient_RowDataBound"
                    DataKeyNames="recipientId" Width="100%" >
                    <Columns>
                        <asp:BoundField DataField="recipientId" HeaderText="recipientId" />
                        <asp:TemplateField HeaderText="流水号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# int.Parse(Eval("GID").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="6%" />
                        <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="11%" />
                        <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="11%">
                            <ItemTemplate>
                                <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            SortExpression="supplier_name" />
                    </Columns>
                </asp:GridView>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
        </td>
    </tr>
</table>
</asp:Content>
