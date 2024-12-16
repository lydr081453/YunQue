<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySqlTest.aspx.cs" Inherits="AdministrativeWeb.MySqlTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="" DataField="" />
            <asp:BoundField HeaderText="" DataField="" />
            <asp:BoundField HeaderText="" DataField="" />
            <asp:BoundField HeaderText="" DataField="" />
            <asp:BoundField HeaderText="" DataField="" />
            <asp:BoundField HeaderText="" DataField="" />
        </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
