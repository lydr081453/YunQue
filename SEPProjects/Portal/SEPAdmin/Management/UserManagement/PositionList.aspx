<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="PositionList.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.PositionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<li><a href="PositionManagement.aspx" style="color:Black" >新增职务</a></li>

<table width="100%">
    <tr>
        <td class="oddrow">名称：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtName" runat="server" /> &nbsp; <asp:Button ID="btnSerach" runat="server" Text="检索" CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
    </tr>
</table>
<br />
<asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvList_PageIndexChanging" Width="80%">
    <Columns>
        <asp:BoundField HeaderText="名称" DataField="PositionName" />
        <asp:BoundField HeaderText="等级" DataField="LevelName" />
                                                    <asp:TemplateField HeaderText="编辑">
                                                <ItemTemplate>
                                                    <a href='PositionManagement.aspx?Id=<%# Eval("Id") %>'><img src="/images/edit.gif" border="0" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblDel" OnClick="btnDel_Click" CommandArgument='<%# Eval("Id") %>' runat="server" OnClientClick="return confirm('您是否要删除此条记录');" Text="删除" ImageUrl="/images/disable.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <table width="100%">
            <tr>
                <td align="center">没有符合条件的数据存在！</td>
            </tr>
        </table>
    </EmptyDataTemplate>
</asp:GridView>
</asp:Content>
