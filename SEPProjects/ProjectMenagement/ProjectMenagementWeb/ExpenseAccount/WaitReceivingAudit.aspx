<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitReceivingAudit.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.WaitReceivingAudit" MasterPageFile="~/MasterPage.master"%>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script language="javascript">
    function NextUserSelect() {
        var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setnextAuditor(name, sysid) {
        document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
        document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
    }
</script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            单据信息
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
            预计冲销金额:
        </td>
        <td class="oddrow-l" >
            <asp:Label runat="server" ID="labPreFee" />
        </td>
    </tr>
    
    <tr>
        <td class="oddrow">
            单据类型:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="labReturnType" />
        </td>
        <td class="oddrow">
            原借款单金额:
        </td>
        <td class="oddrow-l" >
            <asp:Label ID="labReturnFactFee" runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="hylPrint" runat="server" ToolTip="查看明细" Width="90px"></asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            差额:
        </td>
        <td class="oddrow-l" colspan="3" >
            <asp:Label runat="server" ID="labDifferenceFee" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            历次审批记录:
        </td>
        <td class="oddrow-l" colspan="3">
            <%--<asp:TextBox runat="server" ID="txtHisSuggestion" TextMode="MultiLine" Width="60%" Rows="6" ReadOnly="true"></asp:TextBox>--%>
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
    
    <tr id="trNext" runat="server">
        <td class="oddrow">
            下级审批人:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor:pointer;" /><font
                color="red"> * </font>
            <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
            <asp:HiddenField ID="hidNextAuditor" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4">
            <asp:Button ID="btnAudit" runat="server" Text="审批通过" CssClass="widebuttons" OnClick="btnAudit_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnAuditF" runat="server" Text="审批通过" CssClass="widebuttons" OnClick="btnAuditF_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnUnAudit" runat="server" Text="驳回至申请人" CssClass="widebuttons" OnClick="btnUnAudit_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnUnAuditF" runat="server" Text="驳回至财务第一级" CssClass="widebuttons" OnClick="btnUnAuditF_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnPrint" runat="server" Text=" 打印 " CssClass="widebuttons" CausesValidation="false"
                    OnClick="btnPrint_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false" />
        </td>
    </tr>
</table>


<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            冲销明细
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" OnRowCreated="gvG_RowCreated" 
                EmptyDataText="暂时没有相关记录" 
                AllowPaging="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}"  />
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
                    <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="labRecipient" />
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
                    <asp:TemplateField HeaderText="编辑金额" >
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="faEdit" runat="server" Text="<img title='编辑金额' src='../../images/edit.gif' border='0' />"
                                 CausesValidation="false" CommandArgument='<%# Eval("ID")%>'
                                CommandName="faEdit" />
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
