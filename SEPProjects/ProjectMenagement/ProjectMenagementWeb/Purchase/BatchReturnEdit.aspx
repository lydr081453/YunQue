<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" CodeBehind="BatchReturnEdit.aspx.cs" Inherits="FinanceWeb.Purchase.BatchReturnEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx?<%=ESP.Finance.Utility.RequestName.BackUrl %>=BatchReturnEdit.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setnextAuditor(name, sysid) {
            document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
            document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
        }
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
                <asp:TextBox ID="txtBatchCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款总额:
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
                是否有发票:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
                <font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 15%">
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtReturnPreDate" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtReturnPreDate"
                    runat="server" ErrorMessage="付款日期必填"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank" Style="width: auto" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlBank_SelectedIndexChangeed">
                </asp:DropDownList>
                <font color="red">*</font>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="请选择开户行"
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
                <asp:TextBox runat="server" ID="txtSupplierBank" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="60%"></asp:TextBox>
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
        <tr id="trAudit" runat="server">
            <td class="oddrow">
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><asp:LinkButton
                    runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false" ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                            runat="server" ErrorMessage="<br />审批批示必填"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trNext" runat="server">
            <td class="oddrow">
                下级审批人:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
                <asp:HiddenField ID="hidNextAuditor" runat="server" />
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
                                            <asp:Label runat="server" ID="labExpectPaymentPrice"/>
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
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印预览" ItemStyle-Width="8%">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                                Width="4%"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ReturnID") %>'
                                                CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                                CausesValidation="false" OnClientClick="return confirm('你确定删除吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
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
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" CausesValidation="false"
                    OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnAudit" runat="server" Text=" 审批通过 " CssClass="widebuttons" CausesValidation="true"
                    OnClick="btnAudit_Click" />&nbsp;
                <asp:Button ID="btnAudit2" runat="server" Text=" 审批驳回 " CssClass="widebuttons" OnClick="btnAudit2_Click"
                    CausesValidation="false" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnExport_Click"
                    CausesValidation="false" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
