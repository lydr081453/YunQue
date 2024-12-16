<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="InvoiceAppending.aspx.cs" Inherits="FinanceWeb.Purchase.InvoiceAppending" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
        <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
    <uc1:TopMessage ID="TopMessage" runat="server"/>
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
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">
                    预计付款时间:
                </td>
                <td class="oddrow-l" colspan="3">
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
                <td class="oddrow">成本所属组:</td>
                <td class="oddrow-l"><asp:Label ID="labDepartment" runat="server" /></td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">
                    付款申请描述:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label ID="lblPayRemark" runat="server" Width="80%" Height="80px"></asp:Label>
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
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
           <asp:Label runat="server" ID="lblPreDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
 <asp:Label runat="server" ID="lblBankName"></asp:Label>
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
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                网银号/支票号
            </td>
            <td class="oddrow-l" style="width: 35%">
                 <asp:Label runat="server" ID="lblPayCode"></asp:Label>
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
            <td class="heading" colspan="4">
                发票信息
            </td>
        </tr>
        <tr>
        <td class="oddrow" width="15%">
                发票接收人:
            </td>
            <td class="oddrow-l" width="35%">
            <input type="hidden" runat="server" id="hidReceiver" />
                <asp:Label ID="lblReceiver" runat="server"></asp:Label>
            </td>
             <td class="oddrow" width="15%">
                发票号:
            </td>
               <td class="oddrow-l" width="35%">
               <asp:TextBox runat="server" ID="txtInvoiceCode"></asp:TextBox>
               </td>
        </tr>
        <tr>
          <td class="oddrow" width="15%">
                发票补充说明:
            </td>
            <td class="oddrow-l" colspan="3">
<asp:TextBox runat="server" ID="txtInvoiceDesc" TextMode="MultiLine" Height="60" Width="60%"></asp:TextBox>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
              <asp:Button ID="btnOk" Text=" 确定 " CssClass="widebuttons" OnClick="btnOk_Click"
                    runat="server" />&nbsp;
                <asp:Button ID="btnCancel" Text=" 取消 " CssClass="widebuttons" OnClick="btnCancel_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
                
</asp:Content>
