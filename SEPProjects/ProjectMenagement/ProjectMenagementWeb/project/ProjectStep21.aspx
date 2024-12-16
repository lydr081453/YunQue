<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_ProjectStep21" CodeBehind="ProjectStep21.aspx.cs" %>

<%@ Register Src="/UserControls/Project/CustomerDisplay.ascx" TagName="CustomerDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="PrepareDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMemberDisplay.ascx" TagName="ProjectMemberDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectInfoView.ascx" TagName="ProjectInfoView"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectContractCostView.ascx" TagName="ProjectContractCostView"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PaymentInfo.ascx" TagName="PaymentInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectSupporterDisplay.ascx" TagName="ProjectSupporterDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectPercent.ascx" TagName="ProjectPercent"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script>
        $(document).ready(function () {
            $(window).scrollTop($(document).height());
        });
    </script>

    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_04.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px"></td>
        </tr>
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:CustomerDisplay ID="CustomerDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectInfoView ID="ProjectInfoView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectContractCostView ID="ProjectContractCostView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectSupporterDisplay ID="ProjectSupporterDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:PaymentInfo ID="PaymentInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectPercent ID="ProjectPercent" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnViewPre" runat="server" Visible="false" CssClass="widebuttons"
        Text="查看上次正常使用信息" />
    <br />
    <table id="tabReason" runat="server" class="tableForm" width="100%" visible="false">
        <tr>
            <td class="oddrow-l" colspan="2">
                <asp:Label runat="server" ID="lblTip" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">立项原因:
            </td>
            <td class="oddrow-l" style="width: 85%">
                <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Height="50" Width="60%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnNext" Text=" 保存 " CssClass="widebuttons" OnClick="btnNext_Click"
                    runat="server" />&nbsp;
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <asp:Button ID="btnSave2" runat="server" Text="设置审核人" OnClick="btnSave2_Click" CssClass="widebuttons" />
                &nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
