<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceProjectList.aspx.cs" Inherits="FinanceWeb.project.InvoiceProjectList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <table id="tableProject" runat="server" width="100%" class="tableForm">
        <tr>
            <td id="tdProject" class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 85%" colspan="3">
                <asp:TextBox ID="txtProjectCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSelect" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" DataKeyNames="ProjectId" EmptyDataText="暂时没有相关的项目号记录"
        OnRowDataBound="gvProject_RowDataBound" OnRowCommand="gvProject_RowCommand" Width="100%"
        CellPadding="4">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                        <asp:Button ID="btnAddProject" OnClientClick="location.href='#tdReturn';" runat="server" Text="选择" CssClass="widebuttons" CommandName="AddProject"
                        CommandArgument='<%# Eval("projectId").ToString() + "#" + (Eval("projectCode").ToString() != "" ? Eval("projectCode") : Eval("SerialCode")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="serialCode" HeaderText="PA号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="projectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="30%" />
            <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-Width="30%" />
            <asp:TemplateField HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:Literal ID="litGroup" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
