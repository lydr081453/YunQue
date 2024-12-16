<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="Return_FinancialOperation" Codebehind="FinancialOperation.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">

        function PaymentInvoiceClick() {
            var backurl = window.location.pathname;
            var win = window.open('/Dialogs/PaymentInvoiceDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=ProjectID %>&<% =ESP.Finance.Utility.RequestName.PaymentID %>=<%=Request[ESP.Finance.Utility.RequestName.PaymentID] %>&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        $().ready(function() {

            $("#<%=ddlPaymentType.ClientID %>").empty();
            $("#<%=ddlBank.ClientID %>").empty();
            Return_FinancialOperation.GetPayments(initPayment);
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
            Return_FinancialOperation.GetBanks(parseInt('<%=Request[ESP.Finance.Utility.RequestName.PaymentID] %>'), initBank);
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

        function selectPaymentType(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidPaymentTypeID.ClientID %>").value = "";
            }
            else {
                Return_FinancialOperation.GetPaymentTypeModel(parseInt(id), ChangePaymentType);
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
                Return_FinancialOperation.GetBankModel(id, ChangeBank);
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
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function CheckInput() {
            var msg = "";
            var fee = "";
            var prefee = 0;
            prefee = parseFloat(document.getElementById("<%=lblPaymentAmount.ClientID %>").innerHTML.replace(/,/g, ''));

            if (document.getElementById("<%=txtFactAmount.ClientID %>").value == "") {
                msg += "请输入实际付款金额！" + "\n";
            }
            fee = document.getElementById("<%=txtFactAmount.ClientID %>").value.replace(/,/g, '');
            if (!testNum(fee)) {
                msg += "实际付款金额输入错误！" + "\n";
            }
            if (parseFloat(fee) > prefee) {
                msg += "实际付款金额超出通知付款金额！" + "\n";
            }
            if (document.getElementById("<%= hidBankID.ClientID %>").value == "") {
                msg += "请选择银行帐号！" + "\n";
            }
            if (document.getElementById("<%=hidPaymentTypeID.ClientID %>").value == "") {
                msg += "请选择付款方式！" + "\n";
            }
            if (document.getElementById("<%=hidIsNeedCode.ClientID %>").value == "True") {
                if (document.getElementById("<%=txtPayCode.ClientID %>").value == "")
                    msg += "请输入相关网银号或支票号！" + "\n";
            }

            if (msg == "") {
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                项目信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目所属组:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblGroupName" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectType" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                业务类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBizType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                合同状态:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContractStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                公司代码:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                公司名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款周期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblPaymentCircle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                负责人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目负责人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponser" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                负责人电话:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                负责人邮箱:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserEmail" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                负责人手机:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserMobile" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="heading" colspan="4">
                客户信息
            </td>
        </tr>
                <tr>
                        <td class="oddrow" style="width: 20%">
                            客户代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtCustomerCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            地址代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddressCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户缩写:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="txtShortEN" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            中文名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameCN1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            英文名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                           <asp:Label ID="txtNameEN1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            地址:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddress1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            发票抬头:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtInvoiceTitle" runat="server" />
                        </td>
                    </tr>
                      <tr>
                    <td class="oddrow" style="width: 20%">
                        联系人姓名:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactName" runat="server" />
                    </td>
                    <td class="oddrow" style="width: 20%">
                        固话:
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:Label ID="txtContactTel" runat="server" />
                    </td>
                </tr>
                <tr>
                <tr>
                  <td class="oddrow">
                        Email:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactEmail" runat="server" />
                    </td>
                    <td class="oddrow">
                        网址
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactWebsite" runat="server" />
                    </td>
                  
                </tr>
        <tr>
            <td class="heading" colspan="4">
                付款通知信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款通知内容:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentContent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentPreDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                预计付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" Width="80%" Height="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款通知审批
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtFactDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" /><font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtFactAmount"> </asp:TextBox><font color="red">*</font>
            </td>
        </tr>
        <%--<tr>
            <td class="oddrow" style="width: 15%">
                发票登记:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlInvoice">
                    <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="暂无发票" Value="0"></asp:ListItem>
                    <asp:ListItem Text="发票登记" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
            <td class="oddrow" style="width: 15%">
                <asp:Label runat="server" ID="lblInvoiceTitle">发票号:</asp:Label>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtInvoiceNo"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtCreditCode"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
             <asp:Label runat="server" ID="lblDiffer">美金汇兑差额:</asp:Label>
            </td>
            <td class="oddrow-l" style="width: 35%">
              <asp:TextBox runat="server" ID="txtDiffer"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow" style="width: 15%">
                凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtCreditCode"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                &nbsp;
            </td>
            <td class="oddrow-l" style="width: 35%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:<input type="hidden" runat="server" id="hidBankID" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank">
                </asp:DropDownList><font color="red">*</font>
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
                    runat="server" id="hidIsNeedCode" />
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
            <td colspan="4">
                发票信息:&nbsp;&nbsp;<asp:Button ID="btnAddInvoices" runat="server" OnClientClick="return PaymentInvoiceClick();" Text="添加发票信息" CssClass="widebuttons"></asp:Button>
                <asp:LinkButton runat='server' ID="btnRet" OnClick="btnRet_Click" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                <asp:GridView ID="gvInvoiceDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="InvoiceDetailID"
                    OnRowCommand="gvInvoiceDetail_RowCommand" OnRowDataBound="gvInvoiceDetail_RowDataBound"
                    PageSize="20" OnPageIndexChanging="gvInvoiceDetail_PageIndexChanging" AllowPaging="True"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvoiceNo" HeaderText="发票号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Amounts" HeaderText="付款金额" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="美金汇兑差额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblDiffer" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("InvoiceDetailID") %>'
                                    CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                    OnClientClick="return confirm('你确定删除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>
        <tr id="trTotal" runat="server">
            <td class="oddrow-l" colspan="4" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 15%; border: 0 0 0 0">
                        </td>
                        <td style="width: 30%; border: 0 0 0 0">
                        </td>
                        <td style="width: 15%; border: 0 0 0 0" align="right">
                            <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 25%; border: 0 0 0 0">
                            <asp:Label ID="lblBlance" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 15%; border: 0 0 0 0">
                        </td>
                    </tr>
                </table>
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
            <td class="oddrow" style="width: 15%">
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Height="60px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPass" runat="server" Text=" 审批通过 " OnClientClick="return CheckInput();"
                    OnClick="btnPass_Click" CssClass="widebuttons" CausesValidation="true" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text=" 审批驳回 " OnClick="btnCancel_Click"
                    CssClass="widebuttons" CausesValidation="true" />&nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
