<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PassportWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统登录</title>
    <style type="text/css">
       body
        {
            margin: 0px;
            font-size: 12px;
            background: #e6e6e6;
            font-family: "微软雅黑" , "黑体";
        }
        img
        {
            border: none;
        }
        .float-left
        {
            float: left;
        }
        .float-right
        {
            float: right;
        }
        .clear
        {
            clear: both;
            float: none;
        }
        #logo
        {
            padding: 22px 0 0 30px;
        }
        #main
        {
            background-image: url(images/inside-login_06.jpg);
            background-repeat: no-repeat;
            background-position: center center;
            height: 530px;
        }
        #main .form-box
        {
            margin: 0px auto;
            padding: 135px 0 0 0px;
            width: 230px;
            font-size: 16px;
            color: #fff;
        }
        #main .form-box .item
        {
            padding: 5px;
        }
        #main .form-box .input-usernamekey
        {
            width: 222px;
            height: 28px;
            font-size: 14px;
            color: #7c7c7c;
            font-family: "微软雅黑" , "黑体";
            border: 1px #c3722a solid;
            line-height: 28px;
            padding-left: 5px;
        }
        #main .form-box .securitycode
        {
            height: 28px;
            border: 1px #c3722a solid;
            margin-left: 10px;
        }
        #main .form-box .input-securitycode
        {
            width: 110px;
            height: 28px;
            font-size: 14px;
            color: #7c7c7c;
            font-family: "微软雅黑" , "黑体";
            border: 1px #c3722a solid;
            line-height: 28px;
            padding-left: 5px;
        }
        #main .form-box .btn-div
        {
            padding: 15px 0 0 0;
        }
        #foot
        {
            text-align: right;
            color: #5d5d5d;
            font-size: 12px;
            padding: 0 30px 0 0;
        }
    </style>

    <script src="js/jquery.js" type="text/javascript"></script>

    <script src="js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/dialog1.js"></script>

    <script type="text/javascript">
        function ArticleIsRead(uid, uname, ucode, rurl) {
            $(function() {
                dialog("条款确认", "iframe:ArticleRead.aspx?uid=" + uid + "&uname=" + uname + "&ucode=" + ucode + "&rurl=" + rurl, "800px", "600px", "text");
            });
        }
    </script>

</head>
<body>
    <form id="frmMain" runat="server" defaultfocus="UserCode">
    <div id="logo">
       &nbsp;</div>
    <div id="main">
        <div class="form-box">
            <div class="item">
                <span>用户名</span><asp:RequiredFieldValidator ID="RVUserCode" ForeColor="#ffe68f" Font-Size="12px"
                    runat="server" Display="dynamic" ControlToValidate="UserCode" ErrorMessage="">(必填)</asp:RequiredFieldValidator></div>
            <div>
                <asp:TextBox ID="UserCode" runat="server" MaxLength="20" CssClass="input-usernamekey"></asp:TextBox>
            </div>
            <div class="item">
                <span>密 码</span><asp:RequiredFieldValidator ID="RVPassword" ForeColor="#ffe68f" Font-Size="12px"
                    Display="dynamic" runat="server" ControlToValidate="Password" ErrorMessage="">(必填)</asp:RequiredFieldValidator></div>
            <div>
                <asp:TextBox ID="Password" TextMode="Password" runat="server" MaxLength="20" CssClass="input-usernamekey"></asp:TextBox>
            </div>
            <div class="btn-div">
                <asp:ImageButton ID="btnLogon" runat="server" Width="231" Height="47" ImageUrl="images/btn.jpg"
                    OnClick="LoginButton_Click" />
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div id="foot">
        Copyright ©2018-2024 Xingyan. All rights reserved</div>
    </form>
</body>
</html>
