<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressBookHistoryDetail.aspx.cs"
    Inherits="SEPAdmin.HR.Employees.AddressBookHistoryDetail" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;" class="tableForm">
                    <tr>
                        <td>
                            <asp:DropDownList AutoPostBack="true" ID="DropDownListDep" runat="server" OnSelectedIndexChanged="DropDownListDep_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;  &nbsp;&nbsp;
                             <asp:Button ID="BackBtn" PostBackUrl="~/HR/Employees/AddressBookHistoryList.aspx" runat="server" Text=" 返回 " CssClass="widebuttons">
                            </asp:Button>
                        </td>
                        <td>
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
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
                                Width="100%" AllowPaging="False" OnRowDataBound="gvE_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Id" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="UserName" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ENName" HeaderText="英文名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Position" HeaderText="职位" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Tel" HeaderText="分机" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center" />
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
