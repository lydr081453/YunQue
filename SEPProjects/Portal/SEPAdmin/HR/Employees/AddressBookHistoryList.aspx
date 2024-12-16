<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="AddressBookHistoryList.aspx.cs" Inherits="SEPAdmin.HR.Employees.AddressBookHistoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="3">
                            通讯录历史信息检索
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%" align="center">
                            日期
                        </td>
                        <td class="oddrow-l" style="width: 40%">
                            <asp:TextBox ID="CreateDate" runat="server" onkeyDown="return false; " onclick="setDate(this);"></asp:TextBox>
                            -
                            <asp:TextBox ID="EndDate" runat="server" onkeyDown="return false; " onclick="setDate(this);"></asp:TextBox>
                        </td>
                        <td class="oddrow-l">
                            <asp:Button ID="SearchBtn" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="SearchBtn_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
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
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvE" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                Width="100%" AllowPaging="False" OnRowCommand="gvE_RowCommand" 
                                onrowdatabound="gvE_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CreateDate" HeaderText="日期" ItemStyle-HorizontalAlign="Center" />
                                    <%-- <asp:BoundField DataField="Version" HeaderText="版本" ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:BoundField HeaderText="创建人" DataField="CreateId" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="AddressBookHistoryDetail.aspx?id=<%#Eval("Id ") %>">查看</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="导出Excel" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="../ExportExcel/AddressBookExcel.aspx?id=<%#Eval("Id ") %>">导出</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandArgument='<%#Eval("Id ") %>' CommandName="Del" ID="linkbtn"
                                                Text="删除" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
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
