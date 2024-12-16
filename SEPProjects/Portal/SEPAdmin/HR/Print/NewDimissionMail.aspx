<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewDimissionMail.aspx.cs"
    Inherits="SEPAdmin.HR.Print.NewDimissionMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="900" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
            <tr>
                <td style="background-repeat: repeat-x; padding: 0 10px 20px 10px;">
                    <asp:Label ID="labNextAudit" runat="server" Visible="false" />
                    <asp:Label ID="labMailTip" runat="server" Visible="false" />
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
                        <tr>
                            <td colspan="4" height="30" bgcolor="#FFFFFF">离职申请单
                            <asp:HiddenField ID="hidDimissionFormID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" height="28" bgcolor="#FFFFFF">用户编号:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="labCode" runat="server" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF">所在部门:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtdepartmentName" runat="server" />
                                <asp:HiddenField ID="hiddepartmentId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" height="28" bgcolor="#FFFFFF">姓名:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtuserName" runat="server" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF">所在公司:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtcompanyName" runat="server" />
                                <asp:HiddenField ID="hidcompanyId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">职务:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtPosition" runat="server" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF">所属团队:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtgroupName" runat="server" />
                                <asp:HiddenField ID="hidgroupId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">手机:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtMobilePhone" runat="server" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF">分机:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">私人邮箱:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtEmail" runat="server" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF">入职日期:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label runat="server" ID="txtjoinJobDate" />
                            </td>
                        </tr>
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">公司邮箱:
                            </td>
                            <td height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtComEmail" onkeyDown="return false; " runat="server" />
                            </td>
                            <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                <td height="28" bgcolor="#FFFFFF">期望离职日期:
                                </td>
                                <td height="28" bgcolor="#FFFFFF">
                                    <asp:Label ID="txtdimissionDate2" onkeyDown="return false; " runat="server" />
                                </td>
                            </asp:Panel>
                            <asp:Panel ID="pnlOtherStatus" runat="server" Visible="false">
                                <td style="width: 20%" height="28" bgcolor="#FFFFFF">最后离职日期:
                                </td>
                                <td style="width: 30%" height="28" bgcolor="#FFFFFF">
                                    <asp:Label ID="txtdimissionDate" onkeyDown="return false; " runat="server" />
                                </td>
                            </asp:Panel>
                        </tr>
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">离职原因:
                            </td>
                            <td colspan="3" height="28" bgcolor="#FFFFFF">
                                <asp:Label ID="txtdimissionCause" runat="server" TextMode="MultiLine" Width="90%"
                                    Height="80px" />
                            </td>
                        </tr>
                    </table>
                    <table runat="server" visible="false" id="tbCash" width="900" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
                        <tr>
                            <td>以下现金借款类单据尚未处理，请注意交接。</td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvCashList" runat="server" AutoGenerateColumns="False"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="FormCode" HeaderText="单据编号" />
                            <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                            <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="申请金额" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
