<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Return_PaymentNotifyEdit" EnableEventValidation="false" CodeBehind="PaymentNotifyEdit.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Project/TopMessage.ascx" tagname="TopMessage" tagprefix="uc1" %>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        $().ready(function() {

            $("#<%=ddlPaymentType.ClientID %>").empty();
            Return_PaymentNotifyEdit.GetPayments(initPayment);
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
    </script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
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
                预计付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtFactDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFactDate"
                    ErrorMessage="实际付款日期必填"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                预计付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtFactAmount"> </asp:TextBox><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFactAmount"
                    ErrorMessage="实际付款金额必填"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpresionValidator1" runat="server" ControlToValidate="txtFactAmount"
                    ValidationExpression="([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])"
                    ErrorMessage="实际付款金额输入有误" Display="None"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <input type="hidden" runat="server" id="hidPaymentTypeID" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPaymentType"
                    InitialValue="-1" Display="none" ErrorMessage="付款方式为必填项"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 15%">
            </td>
            <td class="oddrow-l" style="width: 35%">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                确认信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="60px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" />
              <tr>
            <td class="heading" colspan="4">
             回款明细
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    OnRowCommand="gvPayment_RowCommand" OnRowDataBound="gvPayment_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymentContent" HeaderText="明细内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="30%" />
                        <asp:TemplateField HeaderText="明细金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="PaymentPreAmount" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="相关附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?FileType=1&ContractID=<%# Eval("Id") %>'>
                                    <img src="/images/ico_04.gif" border="0" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Edit"
                                    Text="<img src='/images/edit.gif' title='编辑' border='0'>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 提交 " OnClick="btnSave_Click" CssClass="widebuttons"
                    CausesValidation="true" />&nbsp;
                <asp:Button ID="btnNext" Text=" 返回 " CssClass="widebuttons" OnClick="btnNext_Click"
                    CausesValidation="false" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
