<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="project_ProjectEdit" CodeBehind="ProjectEdit.aspx.cs" %>

<%@ Register Src="/UserControls/Project/CustomerInfo.ascx" TagName="CustomerInfo"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareInfo.ascx" TagName="PrepareInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMember.ascx" TagName="ProjectMember"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectInfo.ascx" TagName="ProjectInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectContractCost.ascx" TagName="ProjectContractCost"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PaymentInfo.ascx" TagName="PaymentInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectSupporter.ascx" TagName="ProjectSupporter"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectPercent.ascx" TagName="ProjectPercent"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_04.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PrepareInfo ID="PrepareInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectMember ID="ProjectMember" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:CustomerInfo ID="CustomerInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectInfo ID="ProjectInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectContractCost ID="ProjectContractCost" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectSupporter ID="ProjectSupporter" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PaymentInfo ID="PaymentInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectPercent ID="ProjectPercent" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnNext" Text=" 保存 " CssClass="widebuttons" OnClick="btnNext_Click"
                    runat="server"  CausesValidation="false"  />&nbsp;
                <asp:Button ID="btnClose" Text="返回" CssClass="widebuttons" OnClick="btnClose_Click"
                    runat="server" CausesValidation="false" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
