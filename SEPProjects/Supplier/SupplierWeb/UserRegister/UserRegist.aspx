<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegist.aspx.cs" Inherits="SupplierWeb.UserRegister.UserRegist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>无标题文档</title>
<link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="100">&nbsp;</td>
  </tr>
</table>
<table width="800" border="0" align="center" cellpadding="0" cellspacing="0" style="font-size:12px; color:#FFFFFF;">
  <tr>
    <td valign="top"><img src="../Images/reg1.png" width="64" height="17" hspace="27" /></td>
  </tr>
  <tr>
    <td valign="top" background="../Images/reg_023.jpg" style="background-repeat:repeat-x;"><img src="../Images/reg2.jpg" width="176" height="82" hspace="20" /></td>
  </tr>
  <tr>
    <td height="150" background="../Images/reg_03-20.jpg" bgcolor="#5E89FA" style="background-repeat:repeat-x; background-position:bottom left;"><table width="760" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="30"><strong><img src="../Images/reg_10.jpg" width="4" height="7" hspace="5" />您的用户名和密码，您的用户名和密码将在登陆平台时使用</strong></td>
      </tr>
      <tr>
        <td height="10"><img src="../Images/reg_14.jpg" width="760" height="2" /></td>
      </tr>
    </table>
      <table width="760" border="0" align="center" cellpadding="0" cellspacing="3">
        <tr>
          <td width="20%" height="30" align="right">类别：</td>
          <td width="20%">
            <asp:RadioButtonList ID="radlIsPerson" RepeatDirection="Horizontal" runat="server">
                <asp:ListItem Text="公司" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="个人" Value="1"></asp:ListItem>
            </asp:RadioButtonList></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td width="20%" height="30" align="right">用户名： </td>
          <td width="20%" ><asp:TextBox ID="txtName" runat="server" CssClass="input_reg" MaxLength="25"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  ControlToValidate="txtName" Display="None" ErrorMessage="请填写用户名"></asp:RequiredFieldValidator>
            </td>
          <td>（请用英文小写、汉字、数字、下划线，不能全部是数字，下划线不能在末尾。）</td>
        </tr>
        <tr>
          <td height="30" align="right">密码： </td>
          <td><asp:TextBox ID="txtPassword" runat="server" CssClass="input_reg" MaxLength="25" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="txtPassword" Display="None" ErrorMessage="请填写密码"></asp:RequiredFieldValidator>
            </td>
          <td>（限用英文、数字、半角“.”、“-”、“?”和下划线，区分大小写。）</td>
        </tr>
        <tr>
          <td height="30" align="right">重复密码：</td>
          <td><asp:TextBox ID="txtPasswordConfrim" runat="server" CssClass="input_reg" MaxLength="25" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                  ControlToValidate="txtPasswordConfrim" Display="None" ErrorMessage="请填写验证密码"></asp:RequiredFieldValidator>
              <asp:CompareValidator ID="CompareValidator1" runat="server" 
                  ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfrim" 
                  Display="None" ErrorMessage="密码填写不一致"></asp:CompareValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
      </table>
      <table>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
      </table>
      
      <table width="760" border="0" align="center" cellpadding="0" cellspacing="3">
        <tr>
            <td width="20%" align="right" valign="top">
            服务条款：
            </td>
            <td>
                <div>
                    <span>
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                        待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/待添加/
                    </span>
                </div>
            </td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td><asp:RadioButtonList ID="radFW" runat="server">
                    <asp:ListItem Text="我同意服务条款" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                  Display="None" ErrorMessage="请点击同意服务条款" ControlToValidate="radFW"></asp:RequiredFieldValidator>
            </td>
        </tr>
      </table>
      <table width="760" border="0" align="center" cellpadding="0" cellspacing="3">
        <tr>
          <td width="20%">&nbsp;</td>
          <td><br />
          <%--<asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="../Images/reg_21.jpg" 
                  width="109" height="44" onclick="btnSubmit_Click" />--%>
          <asp:Button ID="btnSubmit" runat="server" width="109" height="44" 
                  onclick="btnSubmit_Click" Text="下一步" />
                  &nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnBack" runat="server" width="109" height="44" 
                  onclick="btnBack_Click" Text="返回" CausesValidation="false" /></td>
        </tr>
      </table>
      <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
      <br />
    <br />
    <br /></td>
  </tr>
</table>
    </form>
</body>
</html>