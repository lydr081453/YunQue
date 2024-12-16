<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="FinanceTicketAudit.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.FinanceTicketAudit" %>

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
        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setnextAuditor(name, sysid) {
            document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
            document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
        }

    </script>

    <asp:HiddenField ID="hidBatchId" runat="server" />
    <table width="100%" border="0" background="/images/allinfo_bg.gif">
        <tr>
            <td class="oddrow" style="width: 15%">
                批次流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblBatchId"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                <font style="font-weight: bold; font-size: 15px">批次号:</font>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="labPurchaeBatchCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                <font style="font-weight: bold; font-size: 15px">创建人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="labCreateUser" runat="server" /></font>
            </td>
            <td class="oddrow" style="width: 15%">
                <font style="font-weight: bold; font-size: 15px">分公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranch" runat="server" /></font>
            </td>            
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank" Style="width: auto" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlBank_SelectedIndexChangeed">
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="请选择开户行"
                    ControlToValidate="ddlBank" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
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
                批次合计:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                返点金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturn" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款总额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblFactFee" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="请选择付款方式"
                    ControlToValidate="ddlPaymentType" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBatchCode" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtBatchCode"
                    runat="server" ErrorMessage="银行凭证号必填"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
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
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowDeleting="gdG_RowDeleting" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                    EmptyDataText="暂时没有相关记录" AllowPaging="False" Width="100%">
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
                    </Columns>
                </asp:GridView>
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
        <tr id="trNext" runat="server">
            <td class="oddrow">
                下级审批人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
                <asp:HiddenField ID="hidNextAuditor" runat="server" />
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
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnYes_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReject" runat="server" Text=" 驳回至前台 " OnClientClick="if(Page_ClientValidate()){return confirm('您确定驳回至前台吗？');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnReject_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
</asp:Content>
