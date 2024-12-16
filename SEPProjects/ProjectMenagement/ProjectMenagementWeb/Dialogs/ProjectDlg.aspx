<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Dialogs_ProjectDlg" Title="BD项目选择" CodeBehind="ProjectDlg.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                项目查询
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                关键字：
                <asp:TextBox ID="txtCode" runat="server" />
                <asp:Button ID="btnOK" runat="server" CssClass="widebuttons" Text=" 检索 " OnClick="btnOK_Click" />&nbsp;
                <asp:Button ID="btnClean" runat="server" CssClass="widebuttons" Text=" 重新搜索 " OnClick="btnClean_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                    OnRowCommand="gvProject_RowCommand" OnRowDataBound="gvProject_RowDataBound" PageSize="10" EmptyDataText="暂时没有项目信息"
                    AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="选择" CssClass="widebuttons" CommandName="Add"
                                    CommandArgument='<%# Eval("projectId").ToString() == "0" ? Eval("projectCode") : Eval("projectId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="serialCode" HeaderText="PA号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="projectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="12%" />
                        <asp:BoundField DataField="SubmitDate" HeaderText="项目提交时间" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="12%" />
                        <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" />
                        <asp:TemplateField HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Literal ID="litGroup" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle Font-Size="X-Large" ForeColor="SteelBlue" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
