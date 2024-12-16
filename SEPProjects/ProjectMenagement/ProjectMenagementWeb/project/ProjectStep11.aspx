<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_ProjectStep11" CodeBehind="ProjectStep11.aspx.cs" %>

<%@ Register Src="/UserControls/Project/ProjectMember.ascx" TagName="ProjectMember"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <table width="100%" border="0" cellpadding="0" runat="server" visible="true" id="tabTitle">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_01.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:Prepare ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectMember ID="ProjectMember" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="  保存 " OnClick="btnSave_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" />&nbsp;
                <asp:Button ID="btnNext" Text="下一步" CssClass="widebuttons" OnClick="btnNext_Click"
                    runat="server" />&nbsp;
                     <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
