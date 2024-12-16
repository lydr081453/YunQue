<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="VendeeDepartmentTypeRelation.aspx.cs" Inherits="VendeeDepartmentTypeRelation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tableForm" border="0" width="100%"><tr><td  class="heading">部门物料</td></tr>
    <tr><td>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false"  Width="100%" CellPadding="4" OnRowDataBound="gv_RowDataBound">
        <Columns>
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" Visible="False" />
            <asp:BoundField DataField="DepartmentCode" HeaderText="DepartmentCode" Visible="False" />
            <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
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
