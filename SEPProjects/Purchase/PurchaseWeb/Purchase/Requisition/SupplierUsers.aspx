<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierUsers.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.SupplierUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="gvList" 
            AllowPaging="false" runat="server" AutoGenerateColumns="false" Width="100%">
            <Columns>
           <asp:TemplateField HeaderText="姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="英文名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Name_en")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="固定电话" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Phone")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="手机号码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Mobile")%>
                </ItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Eval("Departments")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="职务" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%# Eval("Title")%>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <PagerStyle Font-Size="20px" ForeColor="Black" />
        </asp:GridView>
    </form>
</body>
</html>
