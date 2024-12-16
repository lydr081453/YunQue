<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DepartmentsTypeBrowse.aspx.cs" Inherits="SEPAdmin.UserManagement.DepartmentsTypeBrowse" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="oddrow">检索部门类别（输入名字）: </td>
                    <td class="oddrow-l"><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
                    <td class="oddrow-l" width="30%">&nbsp; </td>
                </tr>
                <tr><td style="height:30px"></td></tr>
                <tr>
                    <td colspan="3" class="oddrow-l">
                        <li><a href="DepartmentsTypeForm.aspx" style="color:Black" >新增部门类别</a></li>
                    </td>
                </tr>
                <tr><td style="height:30px"></td></tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td class="oddrow-l">详细信息</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvView_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="部门类型名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("DepartmentTypeName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("DepartmentTypeID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="辅助工作人员/部门ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="状态">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编辑">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblEdit" OnClick="btnEdit_Click" runat="server" Text="编辑" ImageUrl="/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblDel" OnClick="btnDel_Click" runat="server" Text="删除" ImageUrl="/images/disable.gif"></asp:ImageButton>
                                                    <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="lblDel"
                                                        ConfirmText="您是否要删除此条记录?">
                                                    </act:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>