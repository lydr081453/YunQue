<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllJob.aspx.cs" Inherits="Portal.WebSite.AllJob" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
        a
        {
            color: #ed6801;
            text-decoration: underline;
        }
        .font-size-12
        {
            font-size: 12px;
        }
        .grey-bg
        {
            background-image: url('/images/se-bg.jpg');
            background-repeat: repeat-x;
        }
        .portal-box
        {
        }
        .portal-box a
        {
            color: #FFF;
            text-decoration: none;
        }
        .portal-box a:hover
        {
            color: #FFF;
            text-decoration: underline;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Close() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image: url(images/tit-bg.jpg);
        height: 34px;">
        <tr>
            <td>
                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-size: 12px; font-weight: bold;">
                            热点招聘信息
                        </td>
                        <td align="right">
                                <img src="images/ico-close.jpg" alt="关闭页面" style="cursor:pointer" width="20" height="20" border="0" onclick="Close()"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grey-bg">
        <tr>
            <td>
                <p>
                    &nbsp;</p>
                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" background="images/t-repeat.jpg"
                    style="font-size: 12px; color: #fff; font-weight: bold;">
                    <tr>
                        <td width="1">
                            <img src="images/t-left.jpg" width="10" height="32" />
                        </td>
                        <td width="200">
                            职位
                        </td>
                        <td width="400" align="center">
                            工作地点
                        </td>
                        <td align="center">
                            详细内容
                        </td>
                        <td width="1" align="right">
                            <img src="images/t-right.jpg" width="10" height="32" />
                        </td>
                    </tr>
                </table>
                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" class="font-size-12">
                    <tr>
                        <td>
                            <asp:DataList ID="dlJob" runat="server" RepeatColumns="1" RepeatDirection="Vertical"
                                Width="100%" Font-Size="Medium">
                                <ItemTemplate>
                                    <tr align="right" style="background-color: Blue; margin-left: 500px; height: 30px">
                                        <td align="left" width="190px" style="padding: 0 0 0 10px;" bgcolor="#fafafa">
                                            <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"JobName") %>'
                                                Width="190px"></asp:Label>
                                        </td>
                                        <td width="380px" align="center" style="padding: 0 0 0 10px;" bgcolor="#fafafa">
                                            <asp:Label ID="lblWorkingPlace" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WorkingPlace") %>'
                                                Width="380px"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#fafafa">
                                            <asp:Label ID="lblJobId" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"JobId")%>'></asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">查看</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
