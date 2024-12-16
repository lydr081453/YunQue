<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPointEdit.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="PurchaseWeb.UserPointRule.view.UserPointEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        员工信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        姓名:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        邮箱:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        所属部门:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label runat="server" ID="lblDept"></asp:Label>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        当前积分:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label runat="server" ID="lblPoint"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td class="heading" colspan="4">
                            积分调整
                        </td>
                    </tr>
                    <tr>
                     <td class="oddrow" style="width: 15%">
                                        调整积分数:<br />
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                       <asp:TextBox runat="server" ID="txtPoint"></asp:TextBox>
                                       <asp:Button  runat="server" Text=" 确定 " ID="btnPoint" CssClass="widebutton" OnClick="btnPoint_Click"/><font color="red">正数增加积分,负数扣减积分</font>
                                    </td>
                    </tr>
                      <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="4">
                            积分记录
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Panel ID="PageTop" runat="server">
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowDataBound="gvG_RowDataBound"
                                OnPageIndexChanging="gvG_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="UserId" HeaderText="UserId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="UserId" HeaderText="RuleId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="UserId" HeaderText="GiftId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="积分说明" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblType"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Points" HeaderText="积分" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Memo" HeaderText="备注说明" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                </Columns>
                                <PagerSettings Visible="false" />
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
                                        记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
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
