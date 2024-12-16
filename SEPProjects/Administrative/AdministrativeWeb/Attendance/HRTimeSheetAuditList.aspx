<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="HRTimeSheetAuditList.aspx.cs" Inherits="AdministrativeWeb.Attendance.HRTimeSheetAuditList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
<br />
<table width="100%">
    <tr>
                            <td width="15%">
                                关键字：
                            </td>
                            <td><asp:TextBox ID="txtKey" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnSearch" runat="server" class="button-orange" Text="检索" OnClick="btnSearch_Click" /></td>
    </tr>
</table>
<br />
<asp:GridView ID="gvList" Width="100%" runat="server" AutoGenerateColumns="false" 
   OnRowDataBound="gvList_RowDataBound"
 CssClass="topborder-org" HeaderStyle-Height="30px" AllowPaging="true" PageSize="20" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvList_PageIndexChanging">
    <Columns>
        <asp:BoundField HeaderText="员工编号" DataField="UserCode" />
        <asp:TemplateField HeaderText="员工姓名">
            <ItemTemplate>
                <%# Eval("LastNameCN").ToString() + Eval("FirstNameCN").ToString() + "[" + Eval("username") +"]" %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="所属部门" DataField="dpName" />
        <asp:BoundField HeaderText="电话" DataField="Phone1" />
        <asp:BoundField HeaderText="邮箱" DataField="internalEmail" />
        <asp:BoundField HeaderText="天数" DataField="waitCount" />
        <asp:TemplateField HeaderText="详细">
            <ItemTemplate>
                <asp:HyperLink ID="hlyLink" runat="server" Text="查看" /><asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("UserId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <table width="100%">
            <tr>
                <td align="center">暂无符合条件的数据！</td>
            </tr>
        </table>
    </EmptyDataTemplate>
</asp:GridView>
</asp:Content>
