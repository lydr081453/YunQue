<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewForeGift.ascx.cs"
    Inherits="UserControls_ForeGift_ViewForeGift" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            押金信息
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
        <td class="oddrow" style="width: 15%;">
            申请人姓名:
        </td>
        <td class="oddrow-l" style="width: 35%;">
            <asp:Label ID="lblApplicant" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%;">
            押金流水:
        </td>
        <td class="oddrow-l" style="width: 35%;">
            <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            申请付款时间:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 20%">
            押金状态:
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%;">
            供应商名称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="lblSupplierName" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            开户行名称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="lblSupplierBank" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            开户行帐号:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="lblSupplierAccount" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            押金金额:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblForegift" runat="server"></asp:Label><br />
        </td>
        <td class="oddrow" style="width: 15%">
            付款方式:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblPaymentType" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            预计付款时间:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            预计归还时间:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblendDate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            押金描述:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblReturnContent" runat="server" />
        </td>
    </tr>
</table>
