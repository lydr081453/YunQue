<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="VendeeDepartmentTypeRelationEdit.aspx.cs" Inherits="VendeeDepartmentTypeRelationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <table style="height:400px; width:500px;">
    <tr>
    <td style="width:200px">
        <table class="tableForm">
            <tr><td class="heading">可选物料</td></tr>
            <tr><td> <asp:ListBox ID="lbox" runat="server" Width="200" Height="300" 
                    onselectedindexchanged="lbox_SelectedIndexChanged"></asp:ListBox></td></tr>
        </table>
    </td>
    <td style="width:100px" align="center">
        <table>
            <tr><td><asp:Label Id="labdid" runat="server" Visible="false"></asp:Label></td></tr>
            <tr><td>
                <asp:Button ID="addType" runat="server" Text=">>" onclick="addType_Click" Width="30px" CssClass="widebuttons" />
                <br /><br />
                <asp:Button ID="cancelType" runat="server" Text="<<" onclick="cancelType_Click" Width="30px" CssClass="widebuttons" />
            </td></tr>
        </table>
    </td>
    <td style="width:200px">
        <table class="tableForm">
            <tr><td class="heading"><asp:Label Id="labdname" runat="server"></asp:Label></td></tr>
            <tr><td>  <asp:ListBox ID="rbox" runat="server"  Width="200" Height="300" 
                    onselectedindexchanged="rbox_SelectedIndexChanged"></asp:ListBox></td></tr>
        </table>
    </td>
    </tr>
        <tr><td colspan="3" align="center">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="widebuttons" Text="保存" />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"  CssClass="widebuttons" Text="返回"/>
        </td></tr>
    </table>
    </center>
    <br />
    <table class="tableForm" border="0" width="100%"><tr><td class="heading">员工物料</td></tr>
    <tr><td>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"  Width="100%" CellPadding="4" OnRowDataBound="gv_RowDataBound">
        <Columns>
            <asp:BoundField DataField="a.UserID" HeaderText="a.UserID" Visible="False" />
            <asp:BoundField DataField="Code" HeaderText="Code" Visible="False" />
<%--            <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:TemplateField HeaderText="员工" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labuname"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="物料">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labtype"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
            
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Literal ID="litView" runat="server" />  
                </ItemTemplate>
           </asp:TemplateField> 
        </Columns>
    </asp:GridView>
    </td></tr></table>
</asp:Content>
