<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master"
    CodeBehind="RoleBrowse.aspx.cs" Inherits="SEPAdmin.RoleBrowse" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table width="100%">
            <tr>
                  <td class="heading">角色信息</td>
               </tr>
                <tr>
                    <td class="oddrow-l">
                        <li><a href="RoleForm.aspx" style="color:Black">新增角色</a></li>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="height:30px">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="1" style="border-collapse: collapse" width="100%">
                                        <caption class="oddrow-l">
                                            特殊角色</caption>
                                        <tr>
                                            <td class="oddrow">
                                                角色名
                                            </td>
                                            <td class="oddrow">
                                                角色组
                                            </td>
                                            <td class="oddrow">
                                                角色描述
                                            </td>
                                            <td class="oddrow">
                                                权限
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow-l">
                                                所有用户
                                            </td>
                                            <td class="oddrow-l">
                                                所有角色
                                            </td>
                                            <td class="oddrow-l">
                                                任何用户
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:HyperLink runat='server' ID="lnkP1" NavigateUrl="~/Management/PermissionManagement/RolePermissionModify.aspx?id=1&isfr=1" ImageUrl="/images/edit.gif"
                                                    Text="权限" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow-l">
                                                匿名用户
                                            </td>
                                            <td class="oddrow-l">
                                                所有角色
                                            </td>
                                            <td class="oddrow-l">
                                                任务未登录的用户
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:HyperLink runat='server' ID="lnkP2" NavigateUrl="~/Management/PermissionManagement/RolePermissionModify.aspx?id=2&isfr=1" ImageUrl="/images/edit.gif"
                                                    Text="权限" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow-l">
                                                注册用户
                                            </td>
                                            <td class="oddrow-l">
                                                所有角色
                                            </td>
                                            <td class="oddrow-l">
                                                任务已登录用户
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:HyperLink runat='server' ID="lnkP3" NavigateUrl="~/Management/PermissionManagement/RolePermissionModify.aspx?id=3&isfr=1" ImageUrl="/images/edit.gif"
                                                    Text="权限" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:30px">
                                   
                                </td>
                            </tr>
                            <tr>
                    <td class="oddrow-l">
                        普通角色
                    </td>
                         </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="RoleID"
                                        OnRowDataBound="gvView_RowDataBound" Font-Size="12px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="角色ID">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypRoleID" runat="server" Text='<%# Eval("RoleID") %>' NavigateUrl='RoleForm.aspx?id=<%# Eval("RoleID") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="角色名">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="角色组">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("RoleGroupName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="角色描述">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编辑">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblEdit" OnClick="btnEdit_Click" AlternateText="编辑" runat="server" ImageUrl="/images/edit.gif"
                                                        Text="编辑" Enabled='<%# 1 != (int)Eval("RoleID") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblDel" OnClick="btnDel_Click" AlternateText="删除" runat="server"
                                                        ImageUrl="/images/disable.gif" Text="删除" Enabled='<%# 1 != (int)Eval("RoleID") %>'>
                                                    </asp:ImageButton>
                                                    <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="lblDel"
                                                        ConfirmText="您是否要删除此条记录?">
                                                    </act:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="用户">
                                                <ItemTemplate>
                                                    <a href="<%# GetUsersLink((ESP.Framework.Entity.RoleInfo)Container.DataItem) %>">
                                                        <asp:Image ID="imgUsers" ImageUrl="/images/dc.gif" AlternateText="用户" runat="server" Text="用户" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="权限">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblHomePage" OnClick="btnHomePage_Click" AlternateText="权限"
                                                        runat="server" ImageUrl="/images/edit.gif" Text="权限"></asp:ImageButton>
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
