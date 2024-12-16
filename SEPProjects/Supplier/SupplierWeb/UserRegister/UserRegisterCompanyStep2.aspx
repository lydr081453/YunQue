<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterCompanyStep2.aspx.cs" Inherits="SupplierWeb.UserRegister.UserRegisterCompanyStep2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>无标题文档</title>
	<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script src="/public/js/dialog.js" type="text/javascript"></script>
<script type="text/javascript">
    function btnAreaClick() {
        dialog("选择所在地区", "url:post?/ShowDlg/AreaList.aspx", "500px", "500px", "text");
    }
    function btnIndustriesClick() {
        dialog("选择行业", "url:post?/ShowDlg/IndustriesList.aspx", "500px", "500px", "text");
    }
    function btnPrincipalClick() {
        dialog("选择注册资本", "url:post?/ShowDlg/PrincipalList.aspx", "500px", "500px", "text");
    }
    function btnPropertyClick() {
        dialog("选择所有权属性", "url:post?/ShowDlg/PropertyList.aspx", "500px", "500px", "text");
    }
    function btnScaleClick() {
        dialog("选择员工人数", "url:post?/ShowDlg/ScaleList.aspx", "500px", "500px", "text");
    }

        function onPageSubmit() {
        }         
        </script>
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
          <td width="20%" height="30" align="right">所在地区：</td>
          <td width="50%">
            <asp:TextBox ID="txtAreaName" runat="server" CssClass="input_reg"  onfocus="javascript:this.blur();"/>
            <input type="button" id="btnArea" class="widebuttons" value="选择..." onclick="btnAreaClick();" />
            <asp:HiddenField ID="hidAreaIdP" runat="server" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                  ControlToValidate="txtAreaName" Display="None" ErrorMessage="请选择所在地区"></asp:RequiredFieldValidator>
            </td>
          <td width="30%">&nbsp;</td>
        </tr>
        <tr>
          <td  height="30" align="right">行业：</td>
          <td>
            <asp:TextBox ID="txtIndustriesName" runat="server"  CssClass="input_reg" onfocus="javascript:this.blur();"/>
            <input type="button" id="btnIndustries" class="widebuttons" value="选择..." onclick="btnIndustriesClick();" />
            <asp:HiddenField ID="hidIndustriesIdP" runat="server" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                  ControlToValidate="txtIndustriesName" Display="None" ErrorMessage="请选择行业"></asp:RequiredFieldValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">所有权属性：</td>
          <td>
            <asp:TextBox ID="txtProperty" runat="server"  CssClass="input_reg" onfocus="javascript:this.blur();"/>
            <input type="button" id="btnProperty" class="widebuttons" value="选择..." onclick="btnPropertyClick();" />
            <asp:HiddenField ID="hidPropertyIdP" runat="server" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ControlToValidate="txtProperty" Display="None" ErrorMessage="请选择所有权属性"></asp:RequiredFieldValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">注册资本：</td>
          <td>
            <asp:TextBox ID="txtPrincipal" runat="server" CssClass="input_reg"  onfocus="javascript:this.blur();" />
            <input type="button" id="btnPrincipal" class="widebuttons" value="选择..." onclick="btnPrincipalClick();" />
            <asp:HiddenField ID="hidPrincipalIdP" runat="server" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="txtPrincipal" Display="None" ErrorMessage="请选择注册资本"></asp:RequiredFieldValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">成立时间：</td>
          <td>
            <asp:TextBox ID="txtBuilttime" runat="server" CssClass="input_reg" MaxLength="12"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                  ControlToValidate="txtBuilttime" Display="None" ErrorMessage="请填写成立时间"></asp:RequiredFieldValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">员工人数：</td>
          <td>
            <asp:TextBox ID="txtScale" runat="server"  CssClass="input_reg" onfocus="javascript:this.blur();"/>
            <input type="button" id="btnScale" class="widebuttons" value="选择..." onclick="btnScaleClick();" />
            <asp:HiddenField ID="hidScaleIdP" runat="server" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="txtScale" Display="None" ErrorMessage="请选择员工人数"></asp:RequiredFieldValidator>
           </td>
          <td>&nbsp;</td>
        </tr>
        
        <tr>
          <td height="30" align="right">分公司数量：</td>
          <td>
            <asp:TextBox ID="txtFilialeAmount" runat="server" CssClass="input_reg" MaxLength="3"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  ControlToValidate="txtFilialeAmount" Display="None" ErrorMessage="请填写分公司数量"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                  ControlToValidate="txtFilialeAmount" Display="None" ErrorMessage="分公司数量请填写数字" 
                  ValidationExpression="\d*"></asp:RegularExpressionValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">分公司所在地：</td>
          <td>
            <asp:TextBox ID="txtFilialeAdd" runat="server" CssClass="input_reg" MaxLength="200"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                  ControlToValidate="txtFilialeAdd" Display="None" ErrorMessage="请填写分公司所在地"></asp:RequiredFieldValidator>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">服务覆盖地区：</td>
          <td>
                <asp:TextBox ID="txtService_area" runat="server" CssClass="input_reg" MaxLength="200"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                  ControlToValidate="txtService_area" Display="None" ErrorMessage="服务覆盖地区"></asp:RequiredFieldValidator>
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
                  onclick="btnSubmit_Click" Text="下一步" />
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