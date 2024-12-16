<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeBehind="ListJob.aspx.cs" Inherits="SEPAdmin.HR.Recruitment.ListJob" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li>
                                <asp:LinkButton ID="link_url" runat="server" Text="新增职位" OnClick="btnLink_Click" /></li>
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
                                        关键字:
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
                            <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" DataKeyNames="JobId"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvGift_RowCommand"
                                OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvGift_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="JobId" HeaderText="JobId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="JobName" HeaderText="岗位名称" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="7%" />
                                    <asp:TemplateField FooterText="WorkingPlace" ItemStyle-Width="5%" HeaderText="工作地点"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkingPlace" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Responsibilities" HeaderText="工作职责 " ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Requirements" HeaderText="职责要求" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SerToCustomer" HeaderText="服务客户" ItemStyle-HorizontalAlign="center" />
                                    <asp:TemplateField FooterText="CreateTime" ItemStyle-Width="7%" HeaderText="创建日期"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActityTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="UpdateTime" ItemStyle-Width="7%" HeaderText="最后修改日期"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdateTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<a href='AddActity.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id")%>'>
                                                <img src='/image/audit_icon.gif' border='0px;' title='编辑' />"</a>--%>
                                            <asp:LinkButton ID="linkUpdate" runat="server" CommandArgument='<%# Eval("JobId") %>'
                                                CommandName="Update" Text="<img src='/images/edit.gif' title='编辑' border='0'>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# Eval("JobId") %>'
                                                CommandName="Delete" Text="<img src='/images/disable.gif' title='删除' border='0'>"
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
