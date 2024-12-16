<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ListGift.aspx.cs" EnableEventValidation="false" Inherits="PurchaseWeb.Gift.view.ListGift" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td><li>
                            <asp:LinkButton ID="link_url" runat="server" Text="新增礼品" OnClick="btnLink_Click" /></li>
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
                                    <td class="oddrow" style="width: 15%">
                                        礼品名称:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="300" />
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
                            <asp:GridView ID="gvGift" runat="server" AutoGenerateColumns="False" DataKeyNames="GiftId"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvGift_RowCommand"
                                OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvGift_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="GiftId" HeaderText="GiftId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Code" HeaderText="礼品编号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Name" HeaderText="礼品名称" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Points" HeaderText="积分" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                    <asp:BoundField DataField="Count" HeaderText="兑换数量" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="30%" />
                                    <asp:TemplateField FooterText="State" HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="CreateTime" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Creator" HeaderText="创建人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreator" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href='AddGift.aspx?id=<%#DataBinder.Eval(Container.DataItem,"GiftId")%>'>
                                                <img src='/images/audit_icon.gif' border='0px;' title='编辑' />"</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# Eval("GiftId") %>'
                                                CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
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
