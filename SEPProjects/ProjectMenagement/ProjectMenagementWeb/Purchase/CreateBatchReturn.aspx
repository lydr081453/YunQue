<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" CodeBehind="CreateBatchReturn.aspx.cs" Inherits="FinanceWeb.Purchase.CreateBatchReturn" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
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
                批次付款查询
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" Style="width: auto"
                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChangeed">
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="请选择公司"
                    ControlToValidate="ddlBranch" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                付款方式:<input type="hidden" id="hidPaymentTypeID" runat="server" /><input type="hidden"
                    id="hidPaymentTypeName" runat="server" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChangeed">
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="请选择付款方式"
                    ControlToValidate="ddlPaymentType" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                供应商名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtSupplier" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                提交日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                --
                <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td class="oddrow" style="width: 15%">
                关键字:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
            <td class="oddrow" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 "  CausesValidation="false" CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                编辑付款信息
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
                        runat="server"  ErrorMessage="批次号必填"></asp:RequiredFieldValidator>
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
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtReturnPreDate" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtReturnPreDate"
                        runat="server"  ErrorMessage="付款日期必填"></asp:RequiredFieldValidator>
        
            </td>
            <td class="oddrow" style="width: 15%">
            </td>
            <td class="oddrow-l" style="width: 35%" />
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
            <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%">
                    <tr>
                        <td class="heading" colspan="4">
                            采购付款列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="False"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkReturn" runat="server" Checked="false" Text='' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPR"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                                target="_blank">
                                                <%#Eval("ReturnCode")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("PReEndDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStatusName" />
                                            <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                            <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSupplier" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="供应商帐号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSupplierAccount" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="开户银行" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSupplierBank" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnCreate" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnCreate_Click" />&nbsp;
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

