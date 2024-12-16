<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_FinancialAudit" EnableEventValidation="false" CodeBehind="FinancialAudit.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script language="javascript">
        $().ready(function() {

            $("#<%=ddlPaymentType.ClientID %>").empty();
            $("#<%=ddlBank.ClientID %>").empty();
            Purchase_FinancialAudit.GetPayments(initPayment);
            function initPayment(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] + ',' + r.value[k][1] == $("#<%=hidPaymentTypeID.ClientID %>").val()) {
                            $("#<%=ddlPaymentType.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlPaymentType.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
            }
            Purchase_FinancialAudit.GetBanks(parseInt('<%=Request[ESP.Finance.Utility.RequestName.ReturnID] %>'), initBank);
            function initBank(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] + ',' + r.value[k][1] == $("#<%=hidBankID.ClientID %>").val()) {
                            $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
            }
        });


        function selectPaymentType(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidPaymentTypeID.ClientID %>").value = "";
        }
        else {
            Purchase_FinancialAudit.GetPaymentTypeModel(parseInt(id), ChangePaymentType);
            function ChangePaymentType(r) {
                if (r.value != null) {
                    if (r.value[2] != null && r.value[2] == "True") {
                        document.getElementById("<%=hidIsNeedCode.ClientID %>").value = "True";
                            document.getElementById("<%=lblPayCode.ClientID %>").style.display = "block";
                            document.getElementById("<%=txtPayCode.ClientID %>").style.display = "block";
                        }
                        else {
                            document.getElementById("<%=hidIsNeedCode.ClientID %>").value = "False";
                            document.getElementById("<%=lblPayCode.ClientID %>").style.display = "none";
                            document.getElementById("<%=txtPayCode.ClientID %>").style.display = "none";
                        }
                        if (r.value[3] != null && r.value[3] == "True") {
                            document.getElementById("<%=hidIsNeedBank.ClientID %>").value = "True";
                        }
                        else {
                            document.getElementById("<%=hidIsNeedBank.ClientID %>").value = "False";
                        }
                    }
                }
                document.getElementById("<% =hidPaymentTypeID.ClientID %>").value = id + "," + text;
        }
    }

    function selectBank(id, text) {
        if (id == "-1") {
            document.getElementById("<% =hidBankID.ClientID %>").value = "";
            }
            else {
                Purchase_FinancialAudit.GetBankModel(id, ChangeBank);
                function ChangeBank(r) {
                    if (r.value != null) {
                        if (r.value[2] != null)
                            document.getElementById("<%=lblAccount.ClientID %>").innerHTML = r.value[2];
                        if (r.value[3] != null)
                            document.getElementById("<%=lblAccountName.ClientID %>").innerHTML = r.value[3]
                        if (r.value[4] != null)
                            document.getElementById("<%=lblBankAddress.ClientID %>").innerHTML = r.value[4]; ;
                    }
                }
                document.getElementById("<% =hidBankID.ClientID %>").value = id + "," + text;

            }
        }
        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function checkValid() {
            var msg = "";
            var cost = "";
            var inceptfee = 0;
            inceptfee = parseFloat(document.getElementById("<%=lblInceptPrice.ClientID %>").innerHTML.replace(/,/g, ''));
            var radioInvoice0 = document.getElementById("<% = radioInvoice.ClientID%>" + "_0");
            var radioInvoice1 = document.getElementById("<% = radioInvoice.ClientID%>" + "_1");
            var radioInvoice2 = document.getElementById("<% = radioInvoice.ClientID%>" + "_2");
            if (radioInvoice0.checked == false && radioInvoice1.checked == false && radioInvoice2.checked == false) {
                msg += "请输入发票状态！" + "\n";
            }
            if (document.getElementById("<%=txtFactFee.ClientID %>").value == "") {
                msg += "请输入实际付款金额！" + "\n";
            }
            cost = document.getElementById("<%=txtFactFee.ClientID %>").value.replace(/,/g, '');
            if (!testNum(cost)) {
                msg += "实际付款金额输入错误！" + "\n";
            }
            if (cost > inceptfee) {
                msg += "实际付款金额超出申请付款金额！" + "\n";
            }
            if (document.getElementById("<%=hidIsNeedBank.ClientID %>").value == "True") {
                if (document.getElementById("<%= hidBankID.ClientID %>").value == "") {
                    msg += "请选择银行帐号！" + "\n";
                }
            }
            if (document.getElementById("<%=hidPaymentTypeID.ClientID %>").value == "") {
                msg += "请选择付款方式！" + "\n";
            }
            if (document.getElementById("<%=hidIsNeedCode.ClientID %>").value == "True" && document.getElementById("<% =hidPaymentTypeID.ClientID %>").value != "1") {
                if (document.getElementById("<%=txtPayCode.ClientID %>").value == "")
                    msg += "请输入相关网银号或支票号！" + "\n";
            }
            if (document.getElementById("<%=txtRemark.ClientID %>").value == "") {
                msg += "请输入审批批示！" + "\n";
            }

            if (msg == "") {
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }
        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) >= 0) {
                return true;
            }
            else {
                return false;
            }
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

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">采购付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
                <input type="hidden" id="hidPrID" runat="server" />
                <input type="hidden" id="hidProjectID" runat="server" />
                <input type="hidden" id="hidPaymentID" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">预计付款时间:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                <asp:Label runat="server" ID="lblFee" Text="申请付款金额:"></asp:Label>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">申请付款时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">帐期类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">成本所属组:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
        </tr>

        <asp:Panel runat="server" ID="panParent" Visible="false">
            <tr>
                <td class="heading" colspan="4">原单据信息
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">原PR单号:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:Label ID="lblParentPrNo" runat="server"></asp:Label>
                </td>
                <td class="oddrow" style="width: 20%">原PR单金额:
                </td>
                <td class="oddrow-l" style="width: 30%">
                    <asp:Label ID="lblParentPrTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">原单据号:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:Label ID="lblParentCode" runat="server"></asp:Label>
                </td>
                <td class="oddrow" style="width: 20%">原付款金额:
                </td>
                <td class="oddrow-l" style="width: 30%">
                    <asp:Label ID="lblParentAmount" runat="server"></asp:Label>
                </td>
            </tr>
        </asp:Panel>

        <tr>
            <td class="oddrow" style="width: 15%">付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPayRemark" runat="server" Width="80%" Height="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtOtherRemark" runat="server" Width="80%" Height="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">供应商信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblOldSupplierBank" ForeColor="Red"></asp:Label><br />
                <asp:TextBox runat="server" ID="txtSupplierBank" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblOldSupplierAccount" ForeColor="Red"></asp:Label><br />
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">付款确认
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtFactFee" runat="server" MaxLength="21" ReadOnly="true" OnTextChanged="txtFactFee_TextChanged"></asp:TextBox>&nbsp;<font
                    color="red">*</font>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="实际付款金额必填" ControlToValidate="txtFactFee" ValidationGroup="audit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label Style="font-size: 12px; font-weight: bolder;" runat="server" ID="lblTaxDesc"></asp:Label>
                <asp:HiddenField runat="server" ID="hidTaxShow" Value="0" />
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm" runat="server" id="tbFactoring" visible="false">
        <tr>
            <td class="heading" colspan="4">保理信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblFactoringAccount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户银行:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblFactoringAccountNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">银行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblFactoringBank" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtReturnPreDate" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">选择开户行:<input type="hidden" runat="server" id="hidBankID" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank">
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款方式:<input type="hidden" runat="server" id="hidPaymentTypeID" /><input type="hidden"
                runat="server" id="hidIsNeedCode" /><input type="hidden" runat="server" id="hidIsNeedBank" /><input
                    type="hidden" runat="server" id="hidTag" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 15%">
                <asp:Label runat="server" ID="lblPayCode" Text="网银号/支票号:"></asp:Label>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtPayCode"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="trMediaHeader">
            <td class="heading" colspan="4">媒体稿费详细信息
            </td>
        </tr>
        <tr runat="server" id="trMedia">
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="MeidaOrderID"
                    OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有媒体记者记录" AllowPaging="false"
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
                        <asp:BoundField DataField="MediaName" HeaderText="媒体名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReporterName" HeaderText="记者名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReceiverName" HeaderText="收款人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CardNumber" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CityName" HeaderText="所在城市" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BankName" HeaderText="开户行" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="BankAccountName" HeaderText="账号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="支付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labTotalAmount" Text='<%# Eval("TotalAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
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
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 10%; border: 0 0 0 0"></td>
                        <td style="width: 20%; border: 0 0 0 0" align="left">
                            <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSupplierLog" runat="server"></asp:Label><br />
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><asp:LinkButton
                    runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false" ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trNext" runat="server">
            <td class="oddrow">下级审批人:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
                <asp:HiddenField ID="hidNextAuditor" runat="server" />
            </td>
        </tr>
    </table>


    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRAspButton ID="btnSave" Text=" 保存 " runat="server" CssClass="widebuttons"
                    OnClick="btnSave_Click" />
                &nbsp;<input type="button" id="btnPrint" value=" 打印 " class="widebuttons" onclick='window.open("Print/PaymantPrint.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID%>=<%=Request[ESP.Finance.Utility.RequestName.ReturnID]%>    ");' />
                <prc:CheckPRAspButton ID="btnYes" Text="审批通过" ValidationGroup="audit" runat="server"
                    CssClass="widebuttons" OnClick="btnYes_Click" OnClientClick="return checkValid();" />
                &nbsp;<prc:CheckPRAspButton ID="btnNo" Text="驳回至申请人" runat="server" CssClass="widebuttons"
                    OnClick="btnNo_Click" />
                &nbsp;<prc:CheckPRAspButton ID="btnNoFinance" Text="驳回至财务" runat="server" CssClass="widebuttons"
                    OnClick="btnNoFinance_Click" />
                &nbsp;<prc:CheckPRAspButton ID="btnRepay" Text=" 重汇 " runat="server" CssClass="widebuttons"
                    OnClick="btnRepay_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
