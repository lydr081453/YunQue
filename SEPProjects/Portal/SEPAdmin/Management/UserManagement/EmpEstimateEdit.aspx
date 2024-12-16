<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpEstimateEdit.aspx.cs"
    Inherits="SEPAdmin.Management.UserManagement.EmpEstimateEdit" %>

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
    <table width="100%" class="tableForm" >
        <tr>
            <td class="oddrow" style="width: 15%">
                评估类型
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlEstimate">
                    <asp:ListItem Text="试用评估" Value="试用评估"></asp:ListItem>
                    <asp:ListItem Text="转正评估" Value="转正评估"></asp:ListItem>
                    <asp:ListItem Text="年中评估" Value="年中评估"></asp:ListItem>
                    <asp:ListItem Text="年终评估" Value="年终评估"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">
                生效日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtEstimateDate" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                评估结果
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Text="评估通过" Value="评估通过"></asp:ListItem>
                    <asp:ListItem Text="评估不通过" Value="评估不通过"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                评估说明
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:TextBox ID="txtEstimateDesc" runat="server" TextMode="MultiLine" Width="80%"
                    Height="100" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
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
