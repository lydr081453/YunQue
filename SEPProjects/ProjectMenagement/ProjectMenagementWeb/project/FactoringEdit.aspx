<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FactoringEdit.aspx.cs" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="FinanceWeb.project.FactoringEdit" %>
 
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
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
                采购申请单号:
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
                申请人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请单号:
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
               <asp:Label runat="server" ID="lblFee" Text="申请付款金额:"></asp:Label>
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
            <td class="oddrow">
                成本所属组:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
        </tr>
        
        <asp:Panel runat="server" ID="panParent" Visible="false">
          <tr>
            <td class="heading" colspan="4">
                原单据信息
            </td>
        </tr>
        <tr>
         <td class="oddrow" style="width: 15%">
                原PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblParentPrNo" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                原PR单金额:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblParentPrTotal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
         <td class="oddrow" style="width: 15%">
                原单据号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblParentCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                原付款金额:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblParentAmount" runat="server"></asp:Label>
            </td>
        </tr>
        </asp:Panel>
        
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPayRemark"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:Label runat="server" ID="lblOtherRemark"></asp:Label>
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
            <td class="oddrow" width="15%">
                审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblSupplierLog" runat="server"></asp:Label><br />
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
          <tr>
            <td class="heading" colspan="4">
                保理付款信息
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                开户名称:
            </td>
            <td class="oddrow-l" >
               <asp:DropDownList runat="server" ID="ddlFactoring" OnSelectedIndexChanged="ddlFactoring_SelectedIndexChanged"></asp:DropDownList>
            </td>
               <td class="oddrow" style="width: 15%">
                付款日期:
            </td>
            <td class="oddrow-l" >
                 <asp:TextBox ID="txtFactorDate" onkeyDown="return false; " Style="cursor: pointer;"
                    runat="server" onclick="setDate(this);" />
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                开户银行:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblFactoringBank"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                开户账号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblFactoringAccount"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRAspButton ID="btnSave" Text=" 转为保理公司付款 " runat="server" CssClass="widebuttons"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
