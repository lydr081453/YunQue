<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Message_MessageList"
    MasterPageFile="~/MasterPage.master" Codebehind="MessageList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                公告列表
            </td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td>
                <li><a href="NewPost.aspx?action=add">添加公告</a></li>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="2">
                公告
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                    OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" DataKeyNames="id"
                    CssClass="tableView" Width="100%" CellPadding="4">
                    <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedRowStyle>
                    <RowStyle Wrap="False" HorizontalAlign="Left" CssClass="evenrowdata" Font-Size="16px">
                    </RowStyle>
                    <HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center" Font-Size="12px">
                    </HeaderStyle>
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="subject" HeaderText="公告主题" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="body" HeaderText="公告内容" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="36%" />
                        <asp:BoundField DataField="creatername" HeaderText="发布人" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="createtime" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="11%" />
                        <asp:BoundField DataField="lasttime" HeaderText="最后修改时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="11%" />
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <a href="MessageView.aspx?action=show&id=<%#Eval("id")%>">
                                    <img src="../../images/dc.gif" border="0px;" alt="查看" title="查看"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <a href="NewPost.aspx?action=edit&id=<%#Eval("id")%>">
                                    <img src="../../images/edit.gif" border="0px;" alt="编辑" title="编辑"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('您确定删除吗？');"
                                    Text="<img src='/images/disable.gif' border='0' />" CommandName="Del" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
