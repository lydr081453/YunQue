<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PRAuthorizationList.aspx.cs" Inherits="PurchaseWeb.Purchase.PRAuthorization.PRAuthorizationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%" border="0" class="tableForm">
    <tr>
        <td class="oddrow" >关键字：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtKey" runat="server" /></td>
        <td class="oddrow">状态：</td>
        <td class="oddrow-l"><asp:RadioButtonList ID="radStatus" runat="server" CssClass="XTable" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Text="全部" Value="-1" />
            <asp:ListItem Text="已关闭" Value="0" />
            <asp:ListItem Text="使用中" Value="1" />
        </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="4" class="oddrow-l">
            <asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="widebuttons" OnClick="btnSearch_Click" />
        </td>
    </tr>
</table>
<br />
<asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" CssClass="widebuttons" />
<asp:GridView ID="gvList" runat="server" AllowPaging="false"  AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="业务人员" DataField="username" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField HeaderText="创建人" DataField="createusername" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
         <asp:BoundField HeaderText="物料类别" DataField="TypeName" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField HeaderText="创建时间" DataField="createdate" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
        <asp:TemplateField HeaderText="状态" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <%# ESP.Purchase.Common.State.PRAuthorizationStatus_Names[int.Parse(Eval("status").ToString())] %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="关闭时间" DataField="closedate"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
         <asp:BoundField HeaderText="备注信息" DataField="Remark" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"/>
        <asp:TemplateField HeaderText="编辑" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <a href="PRAuthorizationEdit.aspx?id=<%# Eval("id") %>"><img src="/images/edit.gif" border="0" /></a>
            </ItemTemplate>
            </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <table width="100" border="0">
            <tr><td>暂无数据</td></tr>
        </table>
    </EmptyDataTemplate>
</asp:GridView>

</asp:Content>
