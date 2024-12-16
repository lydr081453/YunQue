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
                <asp:TemplateField HeaderText="��ˮ��" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="prno" HeaderText="���뵥��" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="totalprice" HeaderText="�ܽ��" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="project_code" HeaderText="��Ŀ��" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_Name" HeaderText="��Ӧ��" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_linkman" HeaderText="��ϵ��" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_cellphone" HeaderText="�ֻ���" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="supplier_email" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="�ɹ���Ʒ" ItemStyle-HorizontalAlign="Center" Visible="false">
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
