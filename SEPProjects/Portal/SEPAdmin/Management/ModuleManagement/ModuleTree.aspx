<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleTree.aspx.cs" Inherits="SEPAdmin.Management.ModuleManagement.ModuleTree"
    MasterPageFile="~/MainMaster.Master" %>

<%@ Register Src="ModuleEdit.ascx" TagName="ModuleEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>--%>
            <table>
                <tr>
                    <td valign="top" style="width: 200px" >
                        <asp:TreeView ID="tvModules" runat="server" ShowLines="true" OnSelectedNodeChanged="tvModules_SelectedNodeChanged">
                            <SelectedNodeStyle BackColor="#333399" ForeColor="White" />
                            <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                BackColor="#FFFFFF" NodeSpacing="1px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </td>
                    <td valign="top">
                        <uc1:ModuleEdit ID="ModuleEdit1" runat="server" Visible="false"/>
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
