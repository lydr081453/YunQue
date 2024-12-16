<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataPermission.aspx.cs"
    Inherits="FinanceWeb.project.DataPermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                项目ID
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtProjectID"></asp:TextBox>
                <asp:Button runat="server" ID="btnProject" Text="Permit One Project" 
                    onclick="btnProject_Click" />
                <asp:Button runat="server" ID="btnProjectAll" Text="Permit All Project" 
                    onclick="btnProjectAll_Click" />
                </td>
        </tr>
        <tr>
            <td>
                支持方ID
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSupporterID"></asp:TextBox>
                <asp:Button runat="server" ID="btnSupport" Text="Permit One Supporter" OnClick="btnSupport_Click" />
                <asp:Button runat="server" ID="btnSupportAll" Text="Permit All Supporter" OnClick="btnSupportAll_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Return ID
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtReturnID"></asp:TextBox>
                <asp:Button runat="server" ID="btnReturn" Text="Permit One Return " OnClick="btnReturn_Click" />
                <asp:Button runat="server" ID="btnReturnAll" Text="Permit All Return" OnClick="btnReturnAll_Click" />
            </td>
        </tr>
                <tr>
            <td>
                PR ID
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPRID"></asp:TextBox>
                <asp:Button runat="server" ID="btnPR" Text="Permit One PR " OnClick="btnPR_Click" />
                <asp:Button runat="server" ID="btnPRAll" Text="Permit All PR" OnClick="btnPRAll_Click" />
            </td>
        </tr>
        <tr>
        <td>
         <asp:TextBox runat="server" ID="txtProjectCode"></asp:TextBox>
                <asp:Button runat="server" ID="btnCost" Text="cost check" OnClick="btnCost_Click" />
        </td>
        </tr>
        <tr>
        <td>
                <asp:Button runat="server" ID="btnUserPoint" Text="userpoint" OnClick="btnUserPoint_Click" />
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
