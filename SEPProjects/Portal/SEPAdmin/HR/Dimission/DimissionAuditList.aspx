<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionAuditList.aspx.cs"
    Inherits="DimissionAuditList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">离职检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">员工编号:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtUserCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtDepartments" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">离职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginTime" runat="server" onclick="setDate(this);" />&nbsp --
                &nbsp<asp:TextBox ID="txtEndTime" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
             <td colspan="2" align="right">
                 <asp:Button ID="btnExcel" runat="server" Text=" 导出Excel " CssClass="widebuttons" OnClick="btnExcel_Click" />
            </td>
        </tr>

    </table>

    <br />
    <table width="100%">
        <%--<div id="divPrint" visible="true" runat="server">
            <tr>
                <td align="right">
                    离职时间<asp:DropDownList ID="drpYear" runat="server">
                    </asp:DropDownList>
                    年<asp:DropDownList ID="drpMonth" runat="server">
                    </asp:DropDownList>
                    月&nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" Text=" 发送离职邮件 " CssClass="widebuttons"
                        OnClick="btnSendMail_Click" />
                </td>
            </tr>
        </div>--%>
        <tr>
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
                        <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                            runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="DimissionId"
                    PageSize="20" AllowPaging="True" Width="100%"
                    OnPageIndexChanging="gvList_PageIndexChanging"
                    OnRowDataBound="gvList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="UserCode" HeaderText="离职员工编号" />
                        <asp:BoundField DataField="UserName" HeaderText="离职员工姓名" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="离职员工部门" />
                        <asp:BoundField DataField="LastDay" HeaderText="最后工作日期" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Reason" HeaderText="离职原因" ItemStyle-Width="30%" />
                        <asp:BoundField DataField="Status" HeaderText="状态" />
                        <asp:BoundField DataField="AppDate" HeaderText="提交时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%-- --%>
                                <asp:Label runat="server" ID="lblOperation"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="DimissionFormView.aspx?userid=<%# Eval("UserId") %>" title="查看">
                                    <img src="../../Images/dc.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a target="_blank" href="DimissionAuditStatus.aspx?dimissionId=<%# Eval("DimissionId") %>" title="审批状态">
                                    <img src="../../Images/dc.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
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
</asp:Content>
