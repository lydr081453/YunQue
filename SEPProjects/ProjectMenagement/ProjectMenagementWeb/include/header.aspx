<%@ Page Language="c#" Inherits="FrameSite.Web.include.page.Header" Codebehind="Header.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <link href="/public/css/css.css" rel="stylesheet" />

    <script type="text/javascript" src="/public/js/syscomm.js"></script>

    <script type="text/javascript">
        function get_time() {
            var date = new Date();
            var year = "", month = "", day = "", week = "", hour = "", minute = "", second = "";
            year = date.getFullYear();
            month = add_zero(date.getMonth() + 1);
            day = add_zero(date.getDate());
            week = date.getDay();
            switch (date.getDay()) {
                case 0: val = "星期天"; break
                case 1: val = "星期一"; break
                case 2: val = "星期二"; break
                case 3: val = "星期三"; break
                case 4: val = "星期四"; break
                case 5: val = "星期五"; break
                case 6: val = "星期六"; break
            }
            hour = add_zero(date.getHours());
            minute = add_zero(date.getMinutes());
            second = add_zero(date.getSeconds());

            if (document.all)
               document.getElementById("showdate").innerHTML = "" + year + "." + month + "." + day + " / " + +hour + ":" + minute + ":" + second;
            else
                document.getElementById("showdate").innerHTML = "" + year + "." + month + "." + day + " / " + +hour + ":" + minute + ":" + second;

        }
        function add_zero(temp) {
            if (temp < 10) return "0" + temp;
            else return temp;
        }
        setInterval("get_time()", 1000);

        function changePwd() {
            top.location = "/PSWChange/PSWChange.aspx";
        }
    </script>

</head>
<body class="topbar">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style=" margin:0 0 0 0; padding:0 0 0 0;background-repeat: repeat-x;" background="/images/financial_01.jpg">
        <tr>
            <td height="87" align="left" valign="top">
                <img src="/images/financial_04.jpg" width="247" height="32" style="margin: 24px 0 0 20px;">
            </td>
            <td width="320" valign="top">
                <table width="272" border="0" cellpadding="0" cellspacing="0" class="menu" style="margin-top: 50px;">
                    <tr>
                        <td width="58" valign="bottom">
                            <a target='_top' href='ToHome.aspx' style="color:Gray;">其他系统</a>
                        </td>
                        <td width="21" valign="bottom">
                            <img src="/images/financial_15.jpg" width="21" height="21" />
                        </td>
                        <td width="116" align="center" valign="bottom">
                            <a target='CenterPanel' href='/project/Default.aspx' style="color:Gray;">项目管理系统首页</a>
                        </td>
                        <td width="21" valign="bottom">
                            <img src="/images/financial_15.jpg" width="21" height="21" />
                        </td>
                        <td width="58" align="right" valign="bottom">
                            <a href="#"></a>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="380" align="center" valign="top">
                <table width="350" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="37" align="right" valign="top">
                            <table width="109" border="0" cellpadding="0" cellspacing="0" class="menu" style="margin-top: 14px;">
                                <tr>
                                    <td align="right">
                                        <a href='SignOut.aspx' style="color:Gray;">注销</a> | <a href='#' onclick="return changePwd();" style="color:Gray;">更改密码</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="350" border="0" cellpadding="0" cellspacing="0" background="/images/financial_07-09.jpg"
                                class="date_time" style="background-repeat: repeat-x;">
                                <tr>
                                    <td width="37">
                                        <img src="/images/financial_07.jpg" width="37" height="40" />
                                    </td>
                                    <td valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 17px;">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCaption" ForeColor="White" runat="server" CssClass="time" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="37">
                                        <img src="/images/financial_07-10.jpg" width="37" height="40" />
                                    </td>
                                    <td width="145" valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 16px;">
                                            <tr>
                                                <td align="center">
                                                    <div style="color: White" id="showdate" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="3" align="left">
                                        <img src="/images/financial_07-12.jpg" width="3" height="40" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
