<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.Talents.BackupList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="6">员工检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">应聘职位:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPosition" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">工作地点：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtArea" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">学 历:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEducation" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">工作年限:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtYear" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">关键字:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:TextBox ID="txtKeyword" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnNew" runat="server" Text=" 添加备选人才 " CssClass="widebuttons" OnClick="btnNew_Click" />
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <table width="100%" id="tabTop" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                                <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                跳转到第<asp:DropDownList ID="ddlCurrentPage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>
                                页
                            </asp:Panel>
                        </td>
                        <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                            runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    PageSize="20" AllowPaging="True" EnableViewState="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="NameCN" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DeptShunya" HeaderText="工作地点" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Position" HeaderText="应聘职位" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Mobile" HeaderText="联系方式" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="customer" HeaderText="服务客户" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="工作年限" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblWorkYear"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkEdit" runat="server" ImageUrl="/images/edit.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkPrint" Target="_blank" runat="server" ImageUrl="/images/printno.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="HC申请" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkHC" Target="_blank" runat="server" ImageUrl="/images/audit.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
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
                        <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                            runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </table>
</asp:Content>
