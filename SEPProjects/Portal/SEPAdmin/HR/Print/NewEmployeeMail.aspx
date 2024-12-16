<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewEmployeeMail.aspx.cs"
    Inherits="SEPAdmin.HR.Print.NewEmployeeMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        body
        {
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
        <tr>
            <td>
                <asp:Image runat="server" ID="imgs" Width="680" Height="54" />
            </td>
        </tr>
        <tr>
            <td style="background-repeat: repeat-x; padding: 0 10px 20px 10px;">
                <strong>各位同事,<br />
                    星言云汇即将入职员工信息如下：<br />
                </strong>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                    <tr>
                        <td height="28" align="center" bgcolor="#F4F4F4">
                            <strong>姓名</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>性别</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>组别</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>职位</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>工作地点</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>联系电话</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>邮箱</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>入职日期</strong>
                        </td>
                        <td align="center" bgcolor="#F4F4F4">
                            <strong>备注</strong>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptUserList" OnItemDataBound="rptUserList_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td height="28" align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labFullNameCn" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labSex" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labCompanyName" runat="server" />-<asp:Label ID="labDepartmentName"
                                        runat="server" />-<asp:Label ID="labGroupName" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labJoinJob" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labWorkComp" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labTel" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labEmail" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labJoinDate" runat="server" />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <asp:Label ID="labJob_Memo" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableForm2" cellpadding="0" cellspacing="0">
                    <asp:Repeater runat="server" ID="rptAuxList" OnItemDataBound="rptAuxList_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("auxiliaryname")%>:
                                    <asp:Repeater runat="server" ID="rptUserList">
                                        <ItemTemplate>
                                            <%# Eval("FullNameCN")%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td>
                            通讯录：各 BU HR
                        </td>
                    </tr>
                    <tr>
                        <td>
                            文具袋：<asp:Repeater runat="server" ID="rptAdminList">
                                <ItemTemplate>
                                    <%# Eval("LastNameCN")%><%# Eval("FirstNameCN")%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            分机测试：<asp:Repeater runat="server" ID="rptInfoList">
                                <ItemTemplate>
                                    <%# Eval("LastNameCN")%><%# Eval("FirstNameCN")%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <span>请做好准备工作！</span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
