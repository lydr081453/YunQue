<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRSync.aspx.cs" Inherits="FinanceWeb.project.PRSync" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox runat="server" ID="txtCount"></asp:TextBox>
    <asp:Button runat="server" ID="btnSync" Text="同步PR单状态" OnClick="btnSync_onclick" />
    </div><br />
          <div>
    <asp:TextBox runat="server" ID="txtOOP"></asp:TextBox>
    <asp:Button runat="server" ID="btnAddLog" Text="添加审批日志" OnClick="btnAddLog_Click" />
    </div>
    </form>
</body>
</html>
