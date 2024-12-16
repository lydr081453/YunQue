<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterPersonStep1.aspx.cs" Inherits="SupplierWeb.UserRegister.UserRegisterPersonStep1" %>
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
    <td height="150" background="../Images/reg_03-20.jpg" bgcolor="#5E89FA" style="background-repeat:repeat-x; background-position:bottom left;">
      
      <table width="760" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td height="30"><strong><img src="../Images/reg_10.jpg" width="4" height="7" hspace="5" />您的资料信息，您的信息将直接作为您的评价资料，请填写完整资料信息</strong></td>
        </tr>
        <tr>
          <td height="10"><img src="../Images/reg_14.jpg" width="760" height="2" /></td>
        </tr>
      </table>
      <table width="760" border="0" align="center" cellpadding="0" cellspacing="3">
        <tr>
          <td height="30" align="right">姓名(中文)：</td>
          <td><asp:TextBox ID="txtSupplierName" runat="server" CssClass="input_reg" MaxLength="100"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="txtSupplierName" Display="None" ErrorMessage="公司名称(中文)"></asp:RequiredFieldValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td width="20%" height="30" align="right">姓名(英文)：</td>
          <td width="20%">
            <asp:TextBox ID="txtSupplierNameEn" runat="server" CssClass="input_reg" MaxLength="100"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">地址：</td>
          <td><asp:TextBox ID="txtAddress" runat="server" CssClass="input_reg" MaxLength="100"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                  ControlToValidate="txtAddress" Display="None" ErrorMessage="请填写地址"></asp:RequiredFieldValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">电话：</td>
          <td><asp:TextBox ID="txtTel" runat="server" CssClass="input_reg" MaxLength="25"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                  ControlToValidate="txtTel" Display="None" ErrorMessage="请填写电话"></asp:RequiredFieldValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">手机：</td>
          <td><asp:TextBox ID="txtMobile" runat="server" CssClass="input_reg" MaxLength="11"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="txtMobile" Display="None" ErrorMessage="请填写手机"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                  ControlToValidate="txtMobile" Display="None" ErrorMessage="手机格式有误" 
                  ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">传真：</td>
          <td><asp:TextBox ID="txtFax" runat="server" CssClass="input_reg" MaxLength="25"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">邮箱：</td>
          <td><asp:TextBox ID="txtEmail" runat="server" CssClass="input_reg" MaxLength="25"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                  ControlToValidate="txtEmail" Display="None" ErrorMessage="邮箱格式有误" 
                  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                  ControlToValidate="txtEmail" Display="None" ErrorMessage="请填写EMail邮箱"></asp:RequiredFieldValidator>
            </td>
          <td>（忘记密码时，可凭安全邮箱索取密码。）</td>
        </tr>
        
        <tr>
          <td width="20%" height="30" align="right">MSN：</td>
          <td width="20%">
            <asp:TextBox ID="txtMSN" runat="server" CssClass="input_reg" MaxLength="50"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                  ControlToValidate="txtMSN" Display="None" ErrorMessage="MSN格式有误" 
                  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                  </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">职业：</td>
          <td><asp:TextBox ID="txtservice_content" runat="server" CssClass="input_reg" MaxLength="50"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  ControlToValidate="txtservice_content" Display="None" ErrorMessage="请填写职业"></asp:RequiredFieldValidator>
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
          <td width="20%">&nbsp;</td>
          <td><br />
          <%--<asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="../Images/reg_21.jpg" 
                  width="109" height="44" onclick="btnSubmit_Click" />--%>
                  <asp:Button ID="btnBack" runat="server" width="109" height="44" 
                  onclick="btnBack_Click" Text="上一步" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnSubmit" runat="server" width="109" height="44" 
                  onclick="btnSubmit_Click" Text="下一步"/>
          </td>
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
