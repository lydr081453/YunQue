<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SupplierWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>欢迎使用供应链平台</title>
</head>
<body class="bodyDP">
    <form id="form1" runat="server">
    <p>&nbsp;</p>
<p>&nbsp;</p>
<table width="860" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="images/a2.gif" width="11" height="6" hspace="50" /></td>
  </tr>
</table>
<table width="860" border="0" align="center" cellpadding="15" cellspacing="0" background="images/g1_28.jpg" bgcolor="#FFFFFF" style="background-repeat:repeat-x; background-position:bottom;">
  <tr>
    <td width="75%" style="padding-bottom:50px;"><table width="606" border="0" cellspacing="10" cellpadding="0">
      <tr>
        <td>&nbsp;</td>
        <td width="1"><img src="images/g1_06.jpg" width="109" height="50" /></td>
        <td width="1"><img src="images/g1_08.jpg" width="109" height="50" /></td>
        <td width="1"><img src="images/g1_10.jpg" width="109" height="50" /></td>
      </tr>
    </table>
      <table width="586" border="0" cellspacing="10" cellpadding="0">
        <tr>
          <td><img src="images/g1_16.jpg" width="586" height="182" /></td>
        </tr>
      </table>
      <table width="606" border="0" cellspacing="10" cellpadding="0">
        <tr>
          <td><strong>有益于网络,有益于世界。</strong><br />
            我们是一个全球化的致力于创建自由、开源产品以及技术的社区，我们致力改进世界各地人们的在线体验。我们之中有来自世界各地的程序员、营销人员、测试员以及广告创意人员，目标就是让网络保持它的公共共享资源特性。<br />
          我们屡获大奖的开源软件产品和技术，拥有四十多种语言，并免费提供给所有人。</td>
        </tr>
      </table></td>
    <td width="25%" valign="top" background="images/g1_26.jpg" style="background-repeat: no-repeat; background-position: bottom left;"><table width="100%" border="0" cellspacing="5" cellpadding="0">
      <tr>
        <td><img src="images/g1_03.jpg" width="115" height="69" /></td>
      </tr>
      <tr>
        <td>用户名邮件地址：</td>
      </tr>
      <tr>
        <td><asp:TextBox ID="txtName" runat="server" CssClass="inputDP"></asp:TextBox></td>
      </tr>
      <tr>
        <td>密码：</td>
      </tr>
      <tr>
        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  CssClass="inputDP"></asp:TextBox></td>
      </tr>
      <tr>
        <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="40%"><input type="checkbox" name="checkbox" id="checkbox" />记住我</td>
            <td width="60%">
            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Images/g1_19.jpg" 
                      width="43" height="20" onclick="btnLogin_Click" /></td>
          </tr>
        </table>          </td>
      </tr>
      <tr>
        <td><a href="UserRegister/UserRegist.aspx">注册</a>　<a href="#">忘记密码</a></td>
      </tr>
    </table>
    </td>
  </tr>
</table>
<table width="860" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center" class="footDP">All content copyright 2007-2009 by Type is Beautiful, unless otherwise noted. <br />
    京ICP备008864664654</td>
  </tr>
</table>
    </form>
</body>
</html>
