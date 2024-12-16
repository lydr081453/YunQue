<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewApplyForInvioce.aspx.cs" Inherits="FinanceWeb.ApplyForInvioce.NewApplyForInvioce" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tableForm" width="100%">
        <tr>
            <td colspan="2"  class="heading">新增发票申请</td>
        </tr>
        <tr>
            <td class="oddrow">流向：</td>
            <td class="oddrow-l"><asp:DropDownList ID="ddlFlowTo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFlowTo_SelectedIndexChanged" />
                &nbsp;<asp:DropDownList Visible="false" ID="ddlMedia" style="width:250px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMedia_SelectedIndexChanged" /></td>
        </tr>
                <tr>
            <td class="oddrow">发票类型：</td>
            <td class="oddrow-l"><asp:DropDownList ID="ddlInvoiceType" runat="server" /></td>
        </tr>
                       <tr>
            <td class="oddrow">开票单位名称：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtInvoiceTitle" runat="server" Width="250px" /></td>
        </tr>
                        <tr>
            <td class="oddrow">开户银行：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtBankName" runat="server" Width="250px"/></td>
        </tr>
                        <tr>
            <td class="oddrow">账号：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtBankNum" runat="server" Width="250px"/></td>
        </tr>
                        <tr>
            <td class="oddrow">纳税人识别号：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtTIN" runat="server" Width="250px"/></td>
        </tr>
                        <tr>
            <td class="oddrow">开户地址及电话：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtAddressPhone" runat="server" Width="250px"/></td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%;" >发票金额：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtInviocePrice" runat="server" Width="250px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInviocePrice" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Double" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="txtInviocePrice" ErrorMessage="客户发票金额应大于0"></asp:CompareValidator>
            </td>
        </tr>
                <tr>
            <td class="oddrow">描述：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="60%" Height="80px" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" Text="保存并返回" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="保存并下一条" OnClick="btnNext_Click" />&nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text="返回" OnClick="btnBack_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
