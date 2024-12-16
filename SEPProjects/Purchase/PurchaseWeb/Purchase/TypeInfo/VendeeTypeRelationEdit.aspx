<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="VendeeTypeRelationEdit.aspx.cs" Inherits="VendeeTypeRelationEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <table style="height:400px; width:500px;">
    <tr>
        <td>
            <table class="tableForm">
                <tr><td class="heading">可选物料</td></tr>
                <tr><td><asp:ListBox ID="lbox" runat="server" Width="200" Height="300" ></asp:ListBox></td></tr>
            </table>
        </td>
        <td align="center">
            <table>
                <tr><td><asp:Label Id="labdid" runat="server" Visible="false"></asp:Label><asp:Label Id="labuid" runat="server" Visible="false"></asp:Label></td></tr>
                <tr><td>                
                    <asp:Button ID="addType" runat="server" Text=">>" onclick="addType_Click" Width="30px" CssClass="widebuttons" />
                    <br /><br />
                    <asp:Button ID="cancelType" runat="server" Text="<<"  onclick="cancelType_Click" Width="30px" CssClass="widebuttons"/>
                </td></tr>
            </table>
        </td>
        <td>
            <table class="tableForm">
                <tr><td class="heading"><asp:Label Id="labuname" runat="server"></asp:Label></td></tr>
                <tr><td> <asp:ListBox ID="rbox" runat="server"  Width="200" Height="300"></asp:ListBox></td></tr>
            </table>
        </td>
        <td>
            <table class="tableForm">
                <tr><td class="heading"><asp:Label Id="labdname" runat="server"></asp:Label></td></tr>
                <tr><td><asp:ListBox ID="dbox" runat="server"  Width="200" Height="300" Enabled="false"></asp:ListBox></td></tr>
            </table>
        </td>
    </tr>
        <tr><td colspan="4" align="center">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"  CssClass="widebuttons" />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="返回"  CssClass="widebuttons" />
        </td></tr>
    </table>
    </center>
</asp:Content>
