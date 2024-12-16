<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PaymentScheduleDlg.aspx.cs" Inherits="FinanceWeb.Dialogs.PaymentScheduleDlg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function PaymentValid() {
            var msg = "";
            var cur = document.getElementById("<% = txtPaymentFee.ClientID %>").value.replace(/,/g, '');

            if (document.getElementById("<% =txtPaymentFee.ClientID %>").value == "") {
                msg += "请输入回款金额" + "\n";
            }
            if (document.getElementById("<% =txtPaymentFactDate.ClientID %>").value == "") {
                msg += "请输入回款日期" + "\n";
            }
            else if (!testNum(cur)) {
                msg += "付款金额输入错误！" + "\n";
            }
            if (msg == "")
                return true;
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

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                <asp:Label runat="server" ID="lblProjectCode"></asp:Label>付款通知信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">发票抬头：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblInvoiceTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知号码：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPaymentCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知金额：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblAmount"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">财务确认金额：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblBudgetConfirm"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知时间：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知内容：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblContent"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">回款信息
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 15%">回款方式：
            </td>
             <td class="oddrow-l" colspan="3">
                 <asp:DropDownList runat="server" ID="ddlPaymentType" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Selected="True" Text="银行汇款" Value="1"></asp:ListItem>
                     <asp:ListItem Text="承兑汇票" Value="2"></asp:ListItem>
                 </asp:DropDownList>
                 <asp:Label runat="server" ID="lblPaymentTypeDesc" ForeColor="Red" Visible="false">选择“承兑汇票”时，请务必填写汇票到期日。</asp:Label>
             </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">回款日期：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPaymentFactDate" onkeyDown="return false; " Style="cursor: pointer;"
                    runat="server" onclick="setDate(this);" /><font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">汇票到期日：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBillDate" onkeyDown="return false; " Style="cursor: pointer;"
                    runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">预计回款日期：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtEstReturnDate" onkeyDown="return false; " Style="cursor: pointer;"
                    runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">回款金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPaymentFee" runat="server" MaxLength="11" /><font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">回款金额(外币)：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPaymentFactForiegn" runat="server" Width="20%" />
                <asp:DropDownList runat="server" ID="ddlPaymentFactForiegnUnit">
                    <asp:ListItem Text="请选择..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="美元" Value="美元"></asp:ListItem>
                    <asp:ListItem Text="欧元" Value="欧元"></asp:ListItem>
                    <asp:ListItem Text="澳元" Value="澳元"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">汇兑损益（回款）：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtUSDDiffer" runat="server" MaxLength="11" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnSave" Text=" 保存 " class="widebuttons" runat="server" OnClick="btnSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " CssClass="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
    </table>
</asp:Content>
