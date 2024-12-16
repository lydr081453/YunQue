<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceReFund.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Purchase.FinanceReFund" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
            <td class="oddrow-l"  style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
              <td class="oddrow" style="width: 15%">
                付款单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款起始时间:
            </td>
            <td class="oddrow-l"  colspan="3">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款时间:
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
                  <asp:Label ID="lblPayRemark" runat="server"></asp:Label>
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
                <asp:Label runat="server" ID="lblSupplierBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierAccount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款确认
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:Label runat="server" ID="lblFactFee"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
             <asp:ListItem Value="0">未开</asp:ListItem>
             <asp:ListItem Value="1">已开</asp:ListItem>
             <asp:ListItem Value="2">无须发票</asp:ListItem>
              </asp:RadioButtonList><font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                      <asp:Label runat="server" ID="lblReturnPreDate"></asp:Label>
            </td>
        </tr>
        <tr>
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
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
              <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
            </td>
              <td class="oddrow" style="width: 15%">
               网银号/支票号:
            </td>
            <td class="oddrow-l" style="width: 35%">
             <asp:Label ID="lblPayCode" runat="server"></asp:Label>
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
                退款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRefundFee" runat="server" ></asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" ControlToValidate="txtRefundFee" runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写退款金额!"></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td class="oddrow">
                退款原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator
            ID="RequiredFieldValidator2" ControlToValidate="txtRemark" runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写退款原因!"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
            <asp:Button ID="btnRefund" Text=" 退款 "  runat="server" CssClass="widebuttons" OnClick="btnRefund_Click"/>
                &nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnRefund_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>