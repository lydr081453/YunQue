<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpEducationEdit.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.EmpEducationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
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
              <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    毕业院校:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtSchool" runat="server" />
                                </td>
                                </tr>
                                <tr>
                                <td class="oddrow" style="width: 10%">
                                    就读时间:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);"/>-<asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);"/>
                                </td>
                                </tr>
                                 <tr>
                                <td class="oddrow" style="width: 10%">
                                    学历:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                     <asp:DropDownList ID="txtBase_Education" runat="server">
                                <asp:ListItem Text="高中/中专/中技及以下" Value="高中/中专/中技及以下"></asp:ListItem>
                                <asp:ListItem Text="大专及同等学历" Value="大专及同等学历"></asp:ListItem>
                                <asp:ListItem Text="本科/学士及等同学历" Value="本科/学士及等同学历"></asp:ListItem>
                                <asp:ListItem Text="硕士/研究生及等同学历" Value="硕士/研究生及等同学历"></asp:ListItem>
                                <asp:ListItem Text="博士及以上" Value="博士及以上"></asp:ListItem>
                                <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                            </asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td class="oddrow" style="width: 10%">
                                    专业:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtProfession" runat="server" />
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
