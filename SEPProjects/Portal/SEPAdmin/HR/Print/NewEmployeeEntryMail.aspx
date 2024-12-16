<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewEmployeeEntryMail.aspx.cs" Inherits="SEPAdmin.HR.Print.NewEmployeeEntryMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        body {
            background-color: #ececec;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #585858;
            line-height: 170%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
            <%--  <tr>
    <td><asp:Image  runat="server" id="imgs" width="680" height="54"  /></td>
  </tr>--%>
            <tr>
                <td style="background-repeat: repeat-x; padding: 0 10px 20px 10px;"><strong>各位同事,<br />
                    星言云汇入职员工信息如下：<br />
                </strong>
                    <br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td height="28" align="center" bgcolor="#F4F4F4"><strong>姓名</strong></td>
                            <td align="center" bgcolor="#F4F4F4"><strong>工作地点</strong></td>
                            <td align="center" bgcolor="#F4F4F4"><strong>组别</strong></td>
                            <td align="center" bgcolor="#F4F4F4"><strong>员工编号</strong></td>
                            <td align="center" bgcolor="#F4F4F4"><strong>门禁卡号</strong></td>
                            <td align="center" bgcolor="#F4F4F4"><strong>手机号</strong></td>

                        </tr>
                        <asp:Repeater runat="server" ID="rptUserList" OnItemDataBound="rptUserList_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td height="28" align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labFullNameCn" runat="server" />
                                    </td>
                                    <td align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labAddress" runat="server" />
                                    </td>
                                    <td align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labCompanyName" runat="server" />-<asp:Label ID="labDepartmentName" runat="server" />-<asp:Label ID="labGroupName" runat="server" />
                                    </td>
                                    <td align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labUserCode" runat="server" />
                                    </td>
                                    <td align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labKeyNo" runat="server" />
                                    </td>
                                    <td align="center" bgcolor="#FFFFFF">
                                        <asp:Label ID="labTel" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>

                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

