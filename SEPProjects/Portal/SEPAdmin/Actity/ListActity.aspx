<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeBehind="ListActity.aspx.cs" Inherits="SEPAdmin.Actity.ListActity" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li>
                                <asp:LinkButton ID="link_url" runat="server" Text="新增培训" OnClick="btnLink_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" class="tableForm" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 10%">
                                        培训标题:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtSearch" runat="server" MaxLength="300" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageTop" runat="server">
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        记录数:<asp:Label ID="lblTotalCount" runat="server" />&nbsp;页数:<asp:Label ID="lblPageCount"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvActity" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvGift_RowCommand"
                                OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvGift_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ActityTitle" HeaderText="培训标题" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="ActityContent" HeaderText="培训内容" ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField FooterText="ActityTime" ItemStyle-Width="10%" HeaderText="培训日期"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActityTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Lecturer" HeaderText="讲师" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                    <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<a href='AddActity.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id")%>'>
                                                <img src='/image/audit_icon.gif' border='0px;' title='编辑' />"</a>--%>
                                            <asp:LinkButton ID="linkUpdate" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                CommandName="Update" Text="<img src='/image/audit_icon.gif' title='编辑' border='0'>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                CommandName="Delete" Text="<img src='/image/disable.gif' title='删除' border='0'>"
                                                OnClientClick="return confirm('你确定删除吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        记录数:<asp:Label ID="lblTotalCount2" runat="server" />&nbsp;页数:<asp:Label ID="lblPageCount2"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
