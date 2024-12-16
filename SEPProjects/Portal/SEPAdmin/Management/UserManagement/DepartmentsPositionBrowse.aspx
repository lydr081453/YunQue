<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsPositionBrowse.aspx.cs" MasterPageFile="~/MainMaster.Master"
 Inherits="SEPAdmin.Management.UserManagement.DepartmentsPositionBrowse" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div>        
        <table>
        <tr><td class="heading">职位浏览</td></tr>
        </table>
        <table width="80%">
             <tr>
                <td class="oddrow-l">
                    <li><asp:HyperLink ID="hypDepPost" style="color:Black" runat="server" Text="新增职位"></asp:HyperLink></li>
                </td>
            </tr>
            <tr><td style="height:30px"></td></tr> 
            <tr>
                <td class="oddrow-l">
                    部门公共职位
                </td>
            </tr>                    
            <tr>
                <td>
                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="DepartmentPositionID" OnRowDataBound="gvView_RowDataBound" Font-Size="16px"  >
                        <Columns>
                            <asp:TemplateField HeaderText="职位ID">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypDepID" runat="server" Text='<%# Eval("DepartmentPositionID") %>' NavigateUrl='DepartmentPositionForm.aspx?depposid=<%# Eval("DepartmentPositionID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="职位名" DataField="DepartmentPositionName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="描述" DataField="Description" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lblEdit" OnClick="btnEdit_Click" AlternateText="编辑" runat="server" ImageUrl="/images/edit.gif"
                                        Text="编辑" Enabled='<%# 1 != (int)Eval("DepartmentPositionID") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lblDel" OnClick="btnDel_Click" AlternateText="删除" runat="server" ImageUrl="/images/disable.gif"
                                        Text="删除" Enabled='<%# 1 != (int)Eval("DepartmentPositionID") %>'></asp:ImageButton>
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
    </div>
</asp:Content>

