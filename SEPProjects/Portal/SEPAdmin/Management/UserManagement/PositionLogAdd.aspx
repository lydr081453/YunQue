<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionLogAdd.aspx.cs"
    Inherits="SEPAdmin.Management.UserManagement.PositionLogAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd', 'reloadCalendar()');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" colspan="4">
                职务变更历史
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 10%">
                历史部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtDept" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDept" Display="Dynamic" ErrorMessage="请填写历史部门"></asp:RequiredFieldValidator>
           
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                历史职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPosition" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPosition" Display="Dynamic" ErrorMessage="请填写历史职务"></asp:RequiredFieldValidator>
           
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开始日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" />
                 <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBeginDate" Display="Dynamic" ErrorMessage="请填写开始日期"></asp:RequiredFieldValidator>
           
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                结束日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" />
                 <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="请填写结束日期"></asp:RequiredFieldValidator>
           
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" OnClientClick="art.dialog.close();" CssClass="widebuttons"
                    Text=" 取 消 " />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
