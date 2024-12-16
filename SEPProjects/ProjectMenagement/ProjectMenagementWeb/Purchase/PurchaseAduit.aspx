<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="PurchaseAduit.aspx.cs" Inherits="Purchase_PurchaseAduit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
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
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label> &nbsp;&nbsp; <asp:Label runat="server" ID="lblTaxDesc"></asp:Label>
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
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblPayRemark" runat="server" Width="80%" Height="80px"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
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
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">审批信息</td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">审批历史:</td>
            <td class="oddrow-l"><asp:Label ID="labAuditLog" runat="server" /></td>
        </tr>
        <tr>
            <td class="oddrow">审批备注:</td>
            <td class="oddrow-l"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="50px" /></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
            <asp:Button ID="btnYes" runat="server" CssClass="widebuttons" Text="审批通过" OnClientClick="return confirm('您确定审批通过吗？');" OnClick="btnYes_Click" />
            <asp:Button ID="btnNo" runat="server" CssClass="widebuttons" Text="审批驳回" OnClientClick="return confirm('您确定审批驳回吗？');" OnClick="btnNo_Click" />
            <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text=" 返回 " OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
