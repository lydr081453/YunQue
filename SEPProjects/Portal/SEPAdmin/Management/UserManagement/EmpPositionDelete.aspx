<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpPositionDelete.aspx.cs"
    Inherits="SEPAdmin.Management.UserManagement.EmpPositionDelete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 15%; height: 30px;">
                员工姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblUserName"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%; height: 30px;">
                员工编号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblUserCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                所属部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                职务:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblPosition"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnDelete" runat="server" CssClass="widebuttons" OnClick="btnDelete_Click"
                    Text=" 删 除 " />
                <asp:Button ID="btnCancel" runat="server" OnClientClick="art.dialog.close();" CssClass="widebuttons"
                    Text=" 取 消 " />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    </form>
</body>
</html>
