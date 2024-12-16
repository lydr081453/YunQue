<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="project_SupporterDisplay" CodeBehind="SupporterDisplay.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Project/TopMessage.ascx" tagname="TopMessage" tagprefix="uc1" %>

    <%@ register src="../UserControls/Project/SupporterInfoDisplay.ascx" tagname="SupporterInfoDisplay"
        tagprefix="uc1" %>
    <uc1:TopMessage ID="TopMessage" runat="server" />
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:SupporterInfoDisplay ID="SupporterInfoDisplay" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" runat="server" Text=" 关闭  " CssClass="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
