<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpWorkEdit.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.EmpWorkEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <td class="oddrow" style="width: 15%; height: 30px;">
                工作地点:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtCompany" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtDept" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                职位:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPosition" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtJoinDate" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                工作邮箱:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtEmail" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                工作特长:
            </td>
            <td class="oddrow-l" style="width: 60%">
                <asp:TextBox ID="txtSkill" TextMode="MultiLine" Height="100px" Width="100%"  runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                工作经历:
            </td>
            <td class="oddrow-l" style="width: 60%">
                <asp:TextBox ID="txtExperience" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                服务年限:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtServeYear" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                上级主管:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtDirector" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />
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
