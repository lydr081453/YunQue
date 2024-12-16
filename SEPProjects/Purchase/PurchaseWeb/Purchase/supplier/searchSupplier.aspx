<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchSupplier.aspx.cs" Inherits="PurchaseWeb.Purchase.supplier.searchSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<table width="100%" class="tableForm">
        <tr>
            <td colspan="3" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                供应商名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtsupplierName" runat="server" Width="200px" MaxLength="200" />
            </td>
                        <td class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text=" 检索 "  CssClass="widebuttons" />
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
<asp:TemplateField HeaderText="供应商名称" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                >
                <ItemTemplate>
                        <%#Eval("supplier_name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="supplier_area" HeaderText="所属地区" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
            <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%"  />
            <asp:TemplateField HeaderText="联系电话" ItemStyle-Width="10%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labPhone" runat="server" Text='<%#Eval("contact_tel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="supplier_frameNO" HeaderText="框架协议号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
            <asp:TemplateField HeaderText="状态" ItemStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labStatus" runat="server" Text='<%#(ESP.Purchase.Common.State.supplierstatus[int.Parse(Eval("supplier_status").ToString())]) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle ForeColor="Black" Font-Size="12px" Height="30px" />
    </asp:GridView>
    </form>
</body>
</html>
