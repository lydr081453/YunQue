<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReporterEvaluationLog.aspx.cs" Inherits="MediaWeb.Media.ReporterEvaluationLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
<table width="100%" border="1" class="tableForm">
    <tr>
        <td width="20%" class="oddrow">修订人：</td>
        <td width="30%" class="oddrow-l"><asp:Literal ID="litUser" runat="server" /></td>
        <td width="20%" class="oddrow">修订时间：</td>
        <td class="oddrow-l"><asp:Literal ID="litDate" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">评价内容：</td>
        <td colspan="3" class="oddrow-l"><asp:Label ID="labEvaluation" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">修订原因：</td>
        <td colspan="3" class="oddrow-l"><asp:Label ID="labReason" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="4" align="right">
            <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text=" 返回 " OnClick="btnBack_Click" />
            &nbsp;<input type="button" onclick="window.close();" value=" 关闭 " class="widebuttons" />
        </td>
    </tr>
</table>
</asp:Content>
