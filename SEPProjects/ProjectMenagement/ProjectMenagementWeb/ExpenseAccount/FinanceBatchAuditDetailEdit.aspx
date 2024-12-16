<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditDetailEdit.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditDetailEdit" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script language="javascript">


</script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            单据信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:15%">
            PN号:
        </td>
        <td class="oddrow-l"  style="width:85%" colspan="3">
            <asp:Label runat="server" ID="labPNcode" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            申请人:
        </td>
        <td class="oddrow-l"  width="35%">
            <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
        </td>
        <td class="oddrow" width="15%">
            申请日期:
        </td>
        <td class="oddrow-l"  width="35%">
            <asp:Label runat="server" ID="labRequestDate" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" >
            项目号:
        </td>
        <td class="oddrow-l" >
            <asp:Label runat="server" ID="labProjectCode" />
        </td>
        <td class="oddrow">
            项目名称:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="labProjectName" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            成本所属组:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="labGroup" />
        </td>
        <td class="oddrow">
            预计金额:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="labPreFee" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            单据类型:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="labReturnType" />
        </td>
    </tr>
    <%--<tr>
        <td class="oddrow">
            备注:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="labMemo" />
        </td>
    </tr>--%>

</table>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            明细项信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            成本明细:
        </td>
        <td class="oddrow-l"   width="85%" colspan="3">
            <asp:Label ID="labProjectType" runat="server"  />
        </td>
    </tr>
    
    <tr>
        <td class="oddrow" width="15%">
            费用发生日期:
        </td>
        <td class="oddrow-l"  width="35%">
            <asp:Label runat="server" ID="labExpenseDate" />
        </td>
        <td class="oddrow"  width="15%">
            费用类型:
        </td>
        <td class="oddrow-l"  width="35%">
            <asp:Label runat="server" ID="labExpenseType" />
        </td>
    </tr>
    <asp:Panel runat="server" ID="panMealFee">
        <tr>
            <td class="oddrow" >&nbsp;</td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkMealFee1" Text="早餐" />&nbsp;
                <asp:CheckBox runat="server" ID="chkMealFee2" Text="午餐" />&nbsp;
                <asp:CheckBox runat="server" ID="chkMealFee3" Text="晚餐" />
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel runat="server" ID="panPhone">
        <tr>
            <td class="oddrow" >预计报销手机费年月:</td>
            <td class="oddrow-l" colspan="3">
            
                <asp:Label runat="server" ID="labYear" />年 &nbsp;<asp:Label runat="server" ID="labMonth" />月
            </td>
        </tr>
    </asp:Panel>
    <tr>
        <td class="oddrow">
            费用描述:
        </td>
        <td class="oddrow-l" colspan="3">
            <%--<asp:Label runat="server" ID="labExpenseDesc" />--%>
            <asp:TextBox runat="server" ID="txtExpenseDesc" TextMode="MultiLine" Rows="3" Width="40%"  MaxLength="1500" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            单位数量:
        </td>
        <td class="oddrow-l"  >
            <asp:Label runat="server" ID="labExpenseTypeNumber" />
        </td>
        <td class="oddrow">
            金额:
        </td>
        <td class="oddrow-l"  >
            <asp:Label runat="server" ID="labExpenseMoney" />
            <ComponentArt:NumberInput ID="txtExpenseMoney" runat="server" EmptyText="0.00" NumberType="Number"></ComponentArt:NumberInput>元
        </td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4">
            <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
            <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" />
        </td>
    </tr>
</table>


</asp:Content>