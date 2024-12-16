<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ListUserPointRule.aspx.cs" EnableEventValidation="false" Inherits="PurchaseWeb.UserPointRule.view.ListUserPointRule" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td><li>
                            <asp:LinkButton ID="link_url" runat="server" Text="�������ֹ���" OnClick="btnLink_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" class="tableForm" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="heading" colspan="4">
                                        ����
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        ��������:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="300" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" ���� " CssClass="widebuttons" OnClick="btnSearch_Click">
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
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="ĩҳ" OnClick="btnLast_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        ��¼��:<asp:Label ID="lblTotalCount" runat="server" />&nbsp;ҳ��:<asp:Label ID="lblPageCount"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvUserPointRule" runat="server" AutoGenerateColumns="False" DataKeyNames="RuleId"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvUserPointRule_RowCommand"
                                OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvUserPointRule_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="RuleId" HeaderText="RuleId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="RuleName" HeaderText="��������" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="RuleKey" HeaderText="�������" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="Points" HeaderText="�������" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Description" HeaderText="��������" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50%" />
                                    
                                    <asp:TemplateField HeaderText="�޸�" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href='AddUserPointRule.aspx?id=<%#DataBinder.Eval(Container.DataItem,"RuleId")%>'>
                                                <img src='/images/audit_icon.gif' border='0px;' title='�༭' />"</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ɾ��" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# Eval("RuleId") %>'
                                                CommandName="Del" Text="<img src='/images/disable.gif' title='ɾ��' border='0'>"
                                                OnClientClick="return confirm('��ȷ��ɾ����');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="ĩҳ" OnClick="btnLast_Click" />&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        ��¼��:<asp:Label ID="lblTotalCount2" runat="server" />&nbsp;ҳ��:<asp:Label ID="lblPageCount2"
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

