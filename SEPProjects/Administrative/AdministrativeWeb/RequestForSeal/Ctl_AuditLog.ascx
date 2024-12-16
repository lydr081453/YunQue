<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ctl_AuditLog.ascx.cs" Inherits="AdministrativeWeb.RequestForSeal.Ctl_AuditLog" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="LogTable" runat="server">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td style="font-size: 15px; font-weight: bold;">审批记录</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labLog" runat="server" />
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
    </table>