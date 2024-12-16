<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierPriceFils.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.SupplierPriceFils" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="gvList" PageSize="20" OnPageIndexChanging="gvList_PageIndexChanging" OnRowCommand="gvList_RowCommand" PagerSettings-Mode="NumericFirstLast"
            AllowPaging="true" runat="server" AutoGenerateColumns="false" Width="100%">
            <Columns>
            <asp:TemplateField HeaderText="文件名" ItemStyle-Width="20%">
                <ItemTemplate>
                    <%# Eval("FileName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注" ItemStyle-Width="65%">
                <ItemTemplate>
                    <%# Eval("Remark")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="文件下载" ItemStyle-Width="15%">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDown" runat="server" CommandName="down" CommandArgument='<%# Eval("id") %>' Text="下载"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            </Columns>
            <PagerStyle Font-Size="20px" ForeColor="Black" />
        </asp:GridView>
    </form>
</body>
</html>
