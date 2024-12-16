<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_View_genericInfo" Codebehind="genericInfo.ascx.cs" %>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ② 需求方信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:15%">
            创建时间:
        </td>
        <td class="oddrow-l" style="width:35%">
            <asp:Label ID="txtappdate" runat="server" />
        </td>
        <td class="oddrow" style="width:15%">
            申请人:
        </td>
        <td class="oddrow-l" style="width:35%">
            <asp:Label ID="txtrequeator" runat="server" SkinID="userLabel" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            申请人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtrequestor_info" runat="server" />
        </td>
        <td class="oddrow">
            业务组:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtrequestor_group" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            使用人:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labenduser" runat="server" SkinID="userLabel" />
        </td>
        <td class="oddrow">
            使用人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtenduser_info" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            使用人业务组:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="txtenduser_group" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货人:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labgoods_receiver" runat="server" SkinID="userLabel" />
        </td>
        <td class="oddrow">
            收货人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtreceiver_info" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货地址:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtship_address" runat="server" />
        </td>
        <td class="oddrow">
            收货人业务组:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtReceiverGroup" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货人其他联络方式:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtreceiver_Otherinfo" runat="server" />
        </td>
        <td class="oddrow">
            附加收货人:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtappendReceiver" runat="server" SkinID="userLabel" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">附加收货人联络方式:</td>
        <td class="oddrow-l"><asp:Label ID="txtAppendInfo" runat="server" /></td>
        <td class="oddrow">附加收货人业务组:</td>
        <td class="oddrow-l"><asp:Label ID="txtappendReceiverGroup" runat="server" /></td>
    </tr>
</table>
