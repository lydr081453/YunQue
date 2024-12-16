<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Experience.ascx.cs" Inherits="MediaWeb.NewMedia.BaseData.skins.Experience" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>



<table style="width: 100%" cellspacing="0">
    <tr>
            <td  colspan="4" style="border:0px">
                &nbsp;</td>
        </tr>
    <tr>
        <td class="heading" colspan="4">
            工作信息：</td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            单位类型：</td>
        <td class="oddrow-l" style="width: 30%">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="fixddl">
                <asp:ListItem Selected="True" Text="媒体" Value="1"></asp:ListItem>
                <asp:ListItem Text="非媒体" Value="2"></asp:ListItem>
            </asp:DropDownList></td>
        <td class="oddrow" style="width: 20%">
            单位名称：</td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>
            <font color="red"> *</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            职位：</td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtDuty" runat="server"></asp:TextBox>
            <font color="red"> *</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            专兼职：</td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlAllTime" runat="server" CssClass="fixddl">
                <asp:ListItem Selected="True" Text="专职" Value="1"></asp:ListItem>
                <asp:ListItem Text="兼职" Value="2"></asp:ListItem>
            </asp:DropDownList></td>
        <td class="oddrow">
            单位所属行业：</td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtUnitNature" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="oddrow">
            工作时间：</td>
        <td class="oddrow-l" colspan="3">
            从 ：<cc2:DatePicker ID="dpWorkBeginTime" runat="server"></cc2:DatePicker>
            至：<cc2:DatePicker ID="dpWorkEndTime" runat="server"></cc2:DatePicker>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            单位描述</td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtUnitDescription" runat="server" TextMode="MultiLine" Height="137px"
                Width="80%"></asp:TextBox></td>
    </tr>
</table>
