<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrafficDetail.ascx.cs"
    Inherits="FinanceWeb.UserControls.Purchase.TrafficDetail" %>
<%@ Register Src="TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>
<uc1:TopMessage ID="TopMessage" runat="server" />
<table class="tableform" width="100%">
    <tr>
        <td colspan="4" class="heading">
            Traffic Fee信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            付款申请描述：
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="txtDesc" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            项目号：
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="txtProjectCode" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            Traffic Fee成本金额：
        </td>
        <td class="oddrow-l">
            项目Traffic Fee总额：<asp:Label ID="labAllTraffic" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            预计付款账期：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtBeginDate" runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="oddrow">成本所属组：</td>
        <td class="oddrow-l"><asp:Label ID="labDepartment" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            预计支付金额：
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="txtPreFee" /> 
        </td>
    </tr>
    <tr>
        <td class="oddrow" width="15%">
            申请类型：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labPaymentType" runat="server" />
        </td>
    </tr>
</table>

