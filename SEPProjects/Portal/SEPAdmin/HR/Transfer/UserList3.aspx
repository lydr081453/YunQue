<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList3.aspx.cs" Inherits="SEPAdmin.HR.Transfer.UserList3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>  
    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("input.ckbUser").click(function() {
                if (!window.__sep_dialogReturnValue)
                    window.__sep_dialogReturnValue = ",";
                var rv = window.__sep_dialogReturnValue;
                var valStr = $(this).val() + ",";
                var searchStr = "," + valStr;
                if ($(this).attr("checked")) {
                    if (rv.indexOf(searchStr) < 0) {
                        rv = valStr;
                    }
                }
                else {
                    var index = rv.indexOf(searchStr);
                    if (index >= 0) {
                        rv = rv.substr(0, index) + rv.substr(index + valStr.length);
                    }
                }
                window.__sep_dialogReturnValue = rv;
            });
        });
    </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td>用户编号</td>
    <td><asp:TextBox ID="txtUserCode" runat="server" /></td>
    <td>用户名</td>
    <td><asp:TextBox ID="txtUserName" runat="server" /></td>
    <td>职位</td>
    <td><asp:TextBox ID="txtPositionName" runat="server" /></td>
    <td><asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
    </tr>
    </table>
        <table>
            <asp:Repeater runat="server" ID="rptUserList">
                <HeaderTemplate>
                    <tr>
                        <td>
                            &nbsp;
                        </td> 
                        <td>
                            员工编号
                        </td>                       
                        <td>
                            姓名
                        </td>
                        <td>
                            部门
                        </td>
                        <td>
                            职位
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="radio" name="ckbUser" class="ckbUser" id='ckbUser_<%# Eval("UserID")%>' value='<%# Eval("UserID")%>-<%# Eval("Username")%>' />
                        </td>
                        <td>
                            <%# Eval("UserCode")%>
                        </td>
                        <td>
                            <%# Eval("Username")%>
                        </td>                        
                        <td>
                            <%# Eval("DepartmentName")%>
                        </td>
                        <td>
                            <%# Eval("DepartmentPositionName")%>
                        </td>
                    </tr>
                    </columns>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
