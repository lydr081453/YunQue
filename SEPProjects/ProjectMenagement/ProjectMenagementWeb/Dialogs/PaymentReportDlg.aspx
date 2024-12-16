<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentReportDlg.aspx.cs" MasterPageFile="~/MasterPage.master" Title="付款通知" Inherits="FinanceWeb.Dialogs.PaymentReportDlg" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
      
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
                <asp:Label runat="server" ID="lblProjectCode"></asp:Label>付款通知
            </td>
        </tr>
       
        <tr>
            <td class="oddrow" style="width: 15%">付款通知号码：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPaymentCode" runat="server" Width="30%" /><asp:Button runat="server" id="btnCreate" Text="生成..." OnClick="btnCreate_Click"/>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBudget" runat="server" Width="20%" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">财务确认金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBudgetConfirm" runat="server" Width="20%" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知金额(外币)：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBudgetForiegn" runat="server" Width="20%" />
                <asp:DropDownList runat="server" ID="ddlCurrency">
                     <asp:ListItem Text="请选择..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="美元" Value="美元"></asp:ListItem>
                    <asp:ListItem Text="欧元" Value="欧元"></asp:ListItem>
                    <asp:ListItem Text="澳元" Value="澳元"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知日期：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPaymentDate" onkeyDown="return false; " Style="cursor: pointer;"
                    runat="server" onclick="setDate(this);" Width="20%"/>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">付款通知内容：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtDes" runat="server" Width="70%" />
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">法务负责：
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:CheckBox runat="server"  ID ="chkBad" Checked="false" Text="法务负责"/>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">内部关联公司：
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:CheckBox runat="server"  ID ="chkInner" Checked="false" Text="内部关联公司"/>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">付款通知备注：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="70%" />
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                银行信息
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label ID="lblBankName" runat="server"></asp:Label>
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
                开票信息
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">发票抬头：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceTitle" MaxLength="500" runat="server" Width="40%" />
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">开票方式：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceType" runat="server" Width="20%" />
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">发票号：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceNo" MaxLength="500" runat="server" Width="60%" />
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">开票日期：
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:TextBox ID="txtInvoiceDate" onkeyDown="return false; " Style="cursor:pointer"
                    runat="server" onclick="setDate(this);" Width="20%"/>
            </td>
        </tr>
       
         <tr>
            <td class="oddrow" style="width: 15%">发票金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceAmount" runat="server" Width="20%" />
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">发票领用人：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceReceiver" runat="server" Width="20%" />
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">签收单：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtInvoiceSignIn" MaxLength="500" runat="server" Width="40%" />
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">确认收入状态：
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList><asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td class="heading" colspan="4">
                返点发票信息
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">发票抬头：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateTitle" MaxLength="500" runat="server" Width="40%" />
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">发票号：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateNo" MaxLength="500" runat="server" Width="60%" />
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">开票日期：
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:TextBox ID="txtRebateDate" onkeyDown="return false; " Style="cursor:pointer"
                    runat="server" onclick="setDate(this);" Width="20%"/>
            </td>
        </tr>
       
         <tr>
            <td class="oddrow" style="width: 15%">发票金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateAmount" runat="server" Width="20%" />
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">发票领用人：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateReceiver" runat="server" Width="20%" />
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">签收单：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateSignIn" MaxLength="500" runat="server" Width="40%" />
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">发票类型：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRebateType" runat="server" Width="20%" />
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">确认收入状态：
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:DropDownList runat="server" ID="ddlRebateYear"></asp:DropDownList><asp:DropDownList runat="server" ID="ddlRebateMonth"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnSave" Text=" 保存 " class="widebuttons" runat="server" OnClick="btnSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnClose_Click" OnClientClick="window.close();"/>
            </td>
        </tr>

    </table>

</asp:Content>
