<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ddlDepartments.ascx.cs" Inherits="AdministrativeWeb.UserControls.ddlDepartments" %>
<table style="margin-left:-5px;">
    <tr>
        <td>
            <asp:DropDownList ID="ddlDept1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged" />
            <asp:DropDownList ID="ddlDept2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept2_SelectedIndexChanged" />
            <asp:DropDownList ID="ddlDept3" runat="server" /></td>
    </tr>
</table>
