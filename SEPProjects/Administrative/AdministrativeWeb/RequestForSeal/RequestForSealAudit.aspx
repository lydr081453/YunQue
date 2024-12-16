<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="RequestForSealAudit.aspx.cs" Inherits="AdministrativeWeb.RequestForSeal.RequestForSealAudit" %>

<%@ Register Src="~/RequestForSeal/Ctl_View.ascx" TagPrefix="uc1" TagName="Ctl_View" %>
<%@ Register Src="~/RequestForSeal/Ctl_AuditLog.ascx" TagPrefix="uc1" TagName="Ctl_AuditLog" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Ctl_View runat="server" id="Ctl_View" />
    <br />
                <table width="100%" border="0" cellspacing="5" cellpadding="0" class="table_list">
                    <tr>
                        <td style="text-align: right" width="15%">审批信息：</td>
                        <td>
                            <asp:TextBox ID="txtAudit" runat="server" TextMode="MultiLine" Width="400px" Height="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height:50px;">
                                            <asp:Button ID="btnYes" runat="server" Text="审批通过" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="审批驳回" OnClick="btnNo_Click" />
                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    </table>

    
</asp:Content>
