<%@ Page Language="C#" AutoEventWireup="true" Inherits="ErrorPage" Codebehind="ErrorPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        function showW() {
            if (document.getElementById("trMsg").style.display == "none") {
                document.getElementById("trMsg").style.display = "block";
                if (document.all)
                    document.getElementById("s1").innerText = " - ";
                else
                    document.getElementById("s1").textContent = " - ";
            }
            else {
                document.getElementById("trMsg").style.display = "none";
                if (document.all)
                    document.getElementById("s1").innerText = " + ";
                else
                    document.getElementById("s1").textContent = " - ";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 20px; font-size: 15px">
                <h2>
                    网站无法显示该网页</h2>
                <br />
                最可能的原因是:
                <li style="font: 15px; color: #666666;">未连接到 Internet。</li>
                <li>数据库发生异常。</li>
                <li>网站发生错误。</li><br />
                <br />
                您可以尝试一下操作
                <li><a href='/Purchase/Default.aspx'>返回采购部系统首页。</a></li>
            </td>
        </tr>
        <tr>
            <td style="height:30px"></td>
        </tr>
        <tr>
            <td style="padding-left: 20px; font-size: 15px" align="left">
                <table width="80%" border="0">
                    <tr>
                        <td><a onclick="showW();" style="color:Black; font-size:15px; cursor:pointer"><span id="s1"> + </span>详细信息</a></td>
                    </tr>
                    <tr id="trMsg" style="display:none;">
                        <td><asp:Literal ID="litMsg" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
