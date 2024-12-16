<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="financeAudit.aspx.cs" Inherits="ForeGift_financeAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link href="/public/css/buttonLoading.css" rel="stylesheet" />

    <%@ register src="../UserControls/ForeGift/ViewForeGift.ascx" tagname="ViewForeGift"
        tagprefix="uc1" %>
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript">

        $().ready(function() {

            $("#<%=ddlPaymentType.ClientID %>").empty();
            $("#<%=ddlBank.ClientID %>").empty();
            ForeGift_financeAudit.GetPayments(initPayment);
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
            ForeGift_financeAudit.GetBanks(parseInt('<%=Request[ESP.Finance.Utility.RequestName.ReturnID] %>'), initBank);
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
                ForeGift_financeAudit.GetPaymentTypeModel(parseInt(id), ChangePaymentType);
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
                ForeGift_financeAudit.GetBankModel(id, ChangeBank);
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
            if (document.getElementById("<%=hidIsNeedBank.ClientID %>").value == "True") {
                if (document.getElementById("<%= hidBankID.ClientID %>").value == "") {
                    msg += "请选择银行帐号！" + "\n";
                }
            }
            if (document.getElementById("<%=hidPaymentTypeID.ClientID %>").value == "") {
                msg += "请选择付款方式！" + "\n";
            }
            if (document.getElementById("<%=hidIsNeedCode.ClientID %>").value == "True") {
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

    </script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <uc1:ViewForeGift ID="ViewForeGift" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                付款确认
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
                开户行:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierBank" Width="50%"></asp:TextBox>
            </td>
        </tr>
         <tr>
             <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtFactFee" runat="server" MaxLength="21" ReadOnly="true"></asp:TextBox>&nbsp;<font
                    color="red">*</font>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="实际付款金额必填" ControlToValidate="txtFactFee" ValidationGroup="audit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:RadioButtonList runat="server" ID="radioInvoice" CssClass="XTable" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无须发票</asp:ListItem>
                </asp:RadioButtonList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtReturnPreDate" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:<input type="hidden" runat="server" id="hidBankID" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank">
                </asp:DropDownList>
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
                付款方式:<input type="hidden" runat="server" id="hidPaymentTypeID" /><input type="hidden"
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
        <tr>
            <td class="oddrow" width="15%">
                审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                        runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
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
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" Text=" 保存 " runat="server" CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<input type="button" id="btnPrint" value=" 打印 " class="widebuttons" onclick='window.open("Print/PaymantPrint.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID%>=<%=Request[ESP.Finance.Utility.RequestName.ReturnID]%>");' />
                <asp:Button ID="btnYes" Text="审批通过" ValidationGroup="audit" runat="server" CssClass="widebuttons"
                    OnClick="btnYes_Click" OnClientClick="if(checkValid()==true){showLoading();}" />
                &nbsp;<asp:Button ID="btnNo" Text="驳回至申请人" runat="server" CssClass="widebuttons" OnClientClick="showLoading();" 
                    OnClick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnNoFinance" Text="驳回至财务" runat="server" CssClass="widebuttons"  OnClientClick="showLoading();" 
                    OnClick="btnNoFinance_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
