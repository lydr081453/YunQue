<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prj.aspx.cs" Inherits="FinanceWeb.CostView.prj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtPrj" runat="server" Height="100" Width="80%"></asp:TextBox>
    <asp:Button runat="server" ID="btnOK" Text=" ok " onclick="btnOK_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
    </asp:GridView>
    </form>
</body>
</html>
