<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionEmployeeDlg.aspx.cs" Inherits="SEPAdmin.HR.Employees.DimissionEmployeeDlg" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function selectEmployee(pid, pname) {
            artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtDimissionUser").value = pname;
            artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidDimissionUserId").value = pid;

            art.dialog.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>用户名
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" />
                </td>
                <td>职位
                </td>
                <td>
                    <asp:TextBox ID="txtPositionName" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="widebuttons" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px">
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" Width="100%"
                        EmptyDataText="没有符合要求的数据">
                        <Columns>
                            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <a id='lblUserId' name='lblUserId' onclick="selectEmployee('<%# Eval("userid") %>','<%# Eval("username") %>');"
                                        style="text-decoration: underline; cursor: pointer;">选择</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="username" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="groupName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DepartmentPositionName" HeaderText="职位" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
