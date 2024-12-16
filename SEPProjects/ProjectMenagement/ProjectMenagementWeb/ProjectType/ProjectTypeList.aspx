<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProjectTypeInfo_ProjectTypeList" CodeBehind="ProjectTypeList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <li><a href="ProjectTypeEdit.aspx">添加项目分类信息</a></li>
    <br />
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">分类名称:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtProjectTypeName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4" align="center">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">项目分类列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectTypeID" OnRowCommand="gvList_RowCommand"
                    PageSize="10" EmptyDataText="暂时没有相关记录"
                    OnPageIndexChanging="gvList_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="分类名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <%# getTypeName(Eval("ParentID"),Eval("ProjectTypeName").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="分类代码" DataField="TypeCode" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" />
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <%# getHeadName(Eval("ProjectHeadId")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="描述" DataField="Description" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" />
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <a href='ProjectTypeEdit.aspx?typeid=<%# Eval("ProjectTypeId")%>'>
                                    <img src="../images/edit.gif" border="0px;" title="编辑"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ProjectTypeID") %>'
                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                    OnClientClick="return confirm('你确定删除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
