<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prLogs.ascx.cs" Inherits="PurchaseWeb.UserControls.View.prLogs" %>
<table width="100%" class="tableForm">
    <tr>
        <td width="15%" class="oddrow">
            操作日志:
        </td>
        <td class="oddrow-l">
            <asp:Repeater ID="repLog" runat="server">
                <ItemTemplate>
                    <%# Eval("DES") %><br />
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
