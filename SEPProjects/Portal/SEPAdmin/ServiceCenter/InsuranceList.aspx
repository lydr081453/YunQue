<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceList.aspx.cs"
    Inherits="SEPAdmin.ServiceCenter.InsuranceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="padding: 30px 0 25px 40px">
                <img src="images/staff-center-05.jpg" width="353" height="35" />
            </td>
            <td valign="bottom">
                <img src="images/home.jpg"></img>
                <a target='_parent' href='Default.aspx'>首页</a>&nbsp;&nbsp;&nbsp;
                <img src="images/portal.jpg"></img>
                <a target='_top' href='http://xy.shunyagroup.com'>其他系统</a>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%;">
                <table style="background-color: RGB( 212, 126, 0); width: 100%;">
                    <tr>
                        <td align="center" colspan="7">
                            在星言云汇（按照正常五险一金基数缴纳）
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width: 10%;">
                            缴费基数
                        </td>
                        <td style="width: 10%;">
                            公司支付比例
                        </td>
                        <td style="width: 10%;">
                            公司支付金额
                        </td>
                        <td style="width: 10%;">
                            个人支付比例
                        </td>
                        <td style="width: 10%;">
                            个人支付金额
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="6" style="width: 25%;">
                            五险一金（社保+公积金）
                        </td>
                        <td style="width: 25%;">
                            养老保险
                        </td>
                        <td rowspan="5" style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label17"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label18"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label19"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label20"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            失业保险
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label13"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label14"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label15"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label16"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            工伤保险
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="lblInjuryCPercent"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="lblInjuryCAmount"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="lblInjuryPPercent"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="lblInjuryPAmount"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            生育保险
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label2"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label3"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            医疗保险
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label5"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label6"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label7"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label8"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            合计：
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label9"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label10"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label11"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label12"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            人身意外险
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                            <asp:Label runat="server" ID="Label21"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 10%;">
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 40%;">
                <table style="background-color: RGB( 156, 199, 218); width: 100%;">
                    <tr>
                        <td align="center">
                            其他公司（按照最低标准缴纳）
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 10%;">
                <table style="background-color: #BF9CCA; width: 100%;">
                    <tr>
                        <td align="center">
                            公司缴费
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
