<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="AdministrativeWeb.Audit.Export" MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:DropDownList ID="drpYear" runat="server">
                </asp:DropDownList>年
                <asp:DropDownList ID="drpMonth" runat="server">
                </asp:DropDownList>月&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="txtUserIds"></asp:TextBox>
            
                <asp:ImageButton ID="btnExport" ImageUrl="../images/export.jpg" ToolTip="导出当月的考勤记录信息"
                    Width="52" Height="29" hspace="10" OnClientClick="return confirm('您确定要导出当月考勤记录？');"
                    OnClick="btnExport_Click" runat="server" />
                <%-- <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导出记录" />--%>
            </td>
        </tr>
    </table>
</asp:Content>