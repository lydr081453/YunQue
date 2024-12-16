<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpContractEdit.aspx.cs"
    Inherits="SEPAdmin.Management.UserManagement.EmpContractEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>续签合同</title>
</head>

    <script type="text/javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

<body>
    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd', '');
        }

        function changeContractCompany() {
            var ins = document.getElementById("<%= drpContract_Company.ClientID %>").options;
        var value;
        for (var i = 0; i < ins.length; i++) {
            if (ins[i].selected)
                value = ins[i];
        }
        document.getElementById("<%= txtBranch.ClientID %>").value = value.value;
            }
</script>
    <form id="form1" runat="server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" colspan="4">
                续签合同信息
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 10%">
                合同公司:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:DropDownList ID="drpContract_Company" runat="server" onchange="changeContractCompany();" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                公司代码:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBranch" runat="server" />
            </td>
             <td class="oddrow" style="width: 15%">
                续签日期
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtSignDate" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                起始日期
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBegin" runat="server" onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 15%">
                结束日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtEnd" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                  <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />&nbsp;&nbsp; 
                <asp:Button ID="btnCancel" runat="server" OnClientClick="art.dialog.close();" CssClass="widebuttons"
                    Text=" 取 消 " />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
