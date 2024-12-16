<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPayment.aspx.cs" Inherits="Purchase_Requisition_EditPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script src="../../public/js/jquery-1.2.6.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">

    <script language="javascript">
    $(document).ready(function() {
        $("#<%=txtExpectPaymentPercent.ClientID %>").blur(function() {
                percent = Number(document.getElementById("<%=txtExpectPaymentPercent.ClientID%>").value);
                var totalprice = <%= getTotalPrice() %>;
                document.getElementById("<%=txtExpectPaymentPrice.ClientID%>").value = (totalprice * (percent / 100)).toFixed(4);
        });

        $("#<%=txtExpectPaymentPrice.ClientID %>").blur(function() {
        
                price = Number(document.getElementById("<%=txtExpectPaymentPrice.ClientID%>").value);
                var totalprice = <%= getTotalPrice() %>;
                document.getElementById("<%=txtExpectPaymentPercent.ClientID%>").value = ((price/totalprice)*100).toFixed(2);
        });
    });
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                付款帐期
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                账期类型:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpPeriodType" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="drpPeriodType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 10%" class="oddrow">
                账期基准点:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpPeriodDatumPoint" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">
                账期:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtPeriodDay" runat="server" Width="100px" Text="0" MaxLength="6"></asp:TextBox>天
            </td>
            <td style="width: 10%" class="oddrow">
                日期类型:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpDateType" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%" class="oddrow">
                预计支付时间:
            </td>
            <td style="width: 40%" class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin"></asp:TextBox><font
                    color="red">*</font>
                <img src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtBegin.ClientID %>'), document.getElementById('<%=txtBegin.ClientID %>'), 'yyyy-mm-dd','setEndDate()');" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBegin"
                    Display="None" ErrorMessage="请选择预计支付起始时间"></asp:RequiredFieldValidator>
                <%---<asp:TextBox runat="server" ID="txtEnd" onfocus="javascript:this.blur();"></asp:TextBox>&nbsp;<img
                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtEnd.ClientID %>'), document.getElementById('<%=txtEnd.ClientID %>'), 'yyyy-mm-dd');" />--%>
            </td>
            <td style="width: 10%" class="oddrow">
                预计支付百分比:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtExpectPaymentPercent" runat="server" Width="100px" Text="100"></asp:TextBox>%<font
                    color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExpectPaymentPercent"
                    Display="None" ErrorMessage="请填写预计支付百分比"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtExpectPaymentPercent" Display="None" ErrorMessage="前填写正确百分比"
                    ValidationExpression="^((\d{1,2}(\.\d{1,2})?)|(100(\.0{1,2})?))$"></asp:RegularExpressionValidator>
                &nbsp;
            </td>
            <td class="oddrow" style="width: 10%">
                预计支付金额:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtExpectPaymentPrice" runat="server" Width="100px"></asp:TextBox>元
            </td>
            <input type="hidden" value="0" id="hidTotalPrice" runat="server" />
            <input type="hidden" value="0" id="hitTotalPercent" runat="server" />
            <input type="hidden" value="0" id="hidCurrentPercent" runat="server" />
            <input type="hidden" id="hidPaymentPeriodId" runat="server" />
        </tr>
        <tr runat="server" id="trTax" style="display: none;">
            <td class="oddrow">
                &nbsp;
            </td>
            <td class="oddrow-l" colspan="7">
                <asp:RadioButtonList runat="server" ID="rdList" RepeatDirection="Horizontal">
                    <asp:ListItem Text="税前" Value="1"></asp:ListItem>
                    <asp:ListItem Text="税后" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                    <asp:HiddenField ID="hidTaxTypes" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                备注:
            </td>
            <td style="width: 90%" class="oddrow-l" colspan="7">
                <asp:TextBox ID="txtPeriodRemark" runat="server" Width="90%" MaxLength="1000"></asp:TextBox>
                <br /><asp:Label runat="server" ID="txtMsg" style=" color:Red;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                <asp:Button ID="btnNext" runat="server" Text=" 下一条 " CssClass="widebuttons" OnClick="btnNext_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CausesValidation="false" CssClass="widebuttons"
                    OnClientClick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" />
    </form>
</body>
</html>
