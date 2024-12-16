<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpMemberEdit.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.EmpMemberEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
                <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">

    </script>
</head>
<body>

    <form id="form1" runat="server">
              <table width="100%" class="tableForm">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    姓名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtMemberName" runat="server" />
                                </td>
                                </tr>
                                <tr>
                                <td class="oddrow" style="width: 10%">
                                    所属公司:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtCompany" runat="server" />
                                </td>
                                </tr>
                                 <tr>
                                <td class="oddrow" style="width: 10%">
                                    成员关系:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtRelation" runat="server" />
                                </td>
                                </tr>
                                <tr>
                                <td class="oddrow" style="width: 10%">
                                    邮箱:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtEmail" runat="server" />
                                </td>
                            </tr>
                             <tr>
                                <td class="oddrow" style="width: 10%">
                                    联系方式:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtPhone" runat="server" />
                                </td>
                            </tr>
                             <tr>
                                <td class="oddrow" style="width: 10%">
                                    年龄:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtAge" runat="server" />
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
        <br /><br /><br /><br /><br />
    </form>
</body>
</html>
