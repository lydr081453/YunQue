<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierPOList.aspx.cs"
    Inherits="PurchaseWeb.Purchase.Requisition.SupplierPOList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvList" AllowPaging="false" runat="server" AutoGenerateColumns="false" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="prno" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="totalprice" HeaderText="总金额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_Name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_linkman" HeaderText="联系人" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_cellphone" HeaderText="手机号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_email" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle Font-Size="20px" ForeColor="Black" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
