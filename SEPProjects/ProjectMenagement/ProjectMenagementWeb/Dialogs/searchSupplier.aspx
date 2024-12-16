<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Dialogs_searchSupplier" Title="媒体选择" CodeBehind="searchSupplier.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="3" class="heading">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">媒体名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtsupplierName" runat="server" Width="200px" MaxLength="200" />
            </td>
            <td class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text=" 检索 " CssClass="widebuttons" />
            </td>
            <tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False"
        PageSize="20" AllowPaging="True" OnPageIndexChanging="gvSupplier_PageIndexChanging"
        AllowSorting="true" Width="100%" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderText="选择" HeaderStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Button ID="btnSelect" runat="server" CssClass="widebuttons" Text="选择" OnClick="btnSelect_Click" CommandArgument='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="媒体名称" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#Eval("supplier_name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="supplier_area" HeaderText="所属地区" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
            <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
            <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="CostRate" HeaderText="预估媒体成本比例" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
        </Columns>
        <PagerStyle ForeColor="Black" Font-Size="12px" Height="30px" />
    </asp:GridView>
</asp:Content>
