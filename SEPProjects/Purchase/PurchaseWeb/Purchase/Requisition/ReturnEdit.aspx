<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Purchase_Requisition_ReturnEdit" EnableEventValidation="false" Codebehind="ReturnEdit.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">

        $().ready(function() {

            $("#<%=ddlPaymentType.ClientID %>").empty();
            Purchase_Requisition_ReturnEdit.GetPayments(initPayment);
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
        });

        function selectPaymentType(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidPaymentTypeID.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidPaymentTypeID.ClientID %>").value = id + "," + text;
            }
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function checkValid() {
            var msg = "";
            var cost = "";
            if (document.getElementById("<%=ddlPaymentType.ClientID %>").selectedIndex == 0 || document.getElementById("<%=hidPaymentTypeID.ClientID %>").value == "") {
                msg += " - 请选择支付方式！" + "\n";
            }
            if (document.getElementById("<%=txtFee.ClientID %>").value == "") {
                msg += " - 请输入预计付款金额！" + "\n";
            } else {
            cost = document.getElementById("<%=txtFee.ClientID %>").value.replace(/,/g, '');
                if (!testNum(cost)) {
                    msg += " - 预计付款金额输入错误！" + "\n";
                }
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

    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                采购付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
                <input type="hidden" id="hidPrID" runat="server" />
                <input type="hidden" id="hidProjectID" runat="server" />
                <input type="hidden" id="hidPaymentID" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
             <td class="oddrow" style="width: 15%">
                付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
            <tr>
                <td class="oddrow" style="width: 15%">
                    预计付款时间:
                </td>
                <td class="oddrow-l"  colspan="3">
                    <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
                </td>
            </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                申请付款时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                帐期类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:TextBox ID="txtReturnContent" runat="server" Width="500px" TextMode="MultiLine"
                        Height="50px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:TextBox ID="txtRemark" runat="server" Width="500px" TextMode="MultiLine"
                        Height="50px"></asp:TextBox>
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
                <asp:TextBox runat="server" ID="txtSupplierBank"  Width="60%"></asp:TextBox>
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
            <td class="heading" colspan="4">
                付款申请
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtFee" runat="server" OnTextChanged="txtFee_TextChanged"></asp:TextBox><br />
                预计付款金额只能小于或等于申请付款金额
            </td>
            <td class="oddrow" style="width: 15%">
                付款方式:<input type="hidden" id="hidPaymentTypeID" runat="server" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
        </tr>
          <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label style=" font-size:12px; font-weight:bolder;" runat="server" ID="lblTaxDesc"></asp:Label>
                <asp:HiddenField  runat="server" ID="hidTaxShow" Value="0" />
            </td>
        </tr>
       <%-- <tr>
            <td class="oddrow" style="width: 15%">
                预计付款时间:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
               <font color="red">*</font>
            </td>
        </tr>--%>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <input type="button" id="btnNo" value="保存" runat="server" class="widebuttons" onclick="if(!checkValid()){ return false;}"
                    onserverclick="btnSave_Click" />
                &nbsp;<input type="button" id="btnYes" value="设置付款审批人" runat="server" class="widebuttons"
                    onclick="if(!checkValid()){ return false;}" onserverclick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
