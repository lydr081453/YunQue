<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterPersonStep2.aspx.cs" Inherits="SupplierWeb.UserRegister.UserRegisterPersonStep2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>无标题文档</title>
	<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script src="/public/js/dialog.js" type="text/javascript"></script>
<script type="text/javascript">
    var W3CDOM = (document.createElement && document.getElementsByTagName);

    function initFileUploads() {
        if (!W3CDOM) return;
        var fakeFileUpload = document.createElement('div');
        fakeFileUpload.className = 'fakefile';
        fakeFileUpload.appendChild(document.createElement('input'));
        var image = document.createElement('img');
        image.src = '../Images/button_select.gif';
        fakeFileUpload.appendChild(image);
        var x = document.getElementsByTagName('input');
        for (var i = 0; i < x.length; i++) {
            if (x[i].type != 'file') continue;
            if (x[i].parentNode.className != 'fileinputs') continue;
            x[i].className = 'file hidden';
            var clone = fakeFileUpload.cloneNode(true);
            x[i].parentNode.appendChild(clone);
            x[i].relatedElement = clone.getElementsByTagName('input')[0];
            x[i].onchange = x[i].onmouseout = function() {
                this.relatedElement.value = this.value;
            }
        }
    }

    function btnTypeClick() {
        dialog("选择服务项目", "url:post?/ShowDlg/TypeList.aspx", "500px", "500px", "text");
    }
    function onPageSubmit() {
    }         

</script>
</head>
<body onload="initFileUploads();">
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
          <td width="20%" height="30" align="right">个人履历：</td>
          <td width="50%">
          
            <div class="fileinputs">
	            <input type="file" class="file" id="fileIntrofile" runat="server" />
	            <div class="fakefile">
		            <%--<input />
		            <img src="../Images/button_select.gif" />--%>
	            </div>
            </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                  ControlToValidate="fileIntrofile" Display="None" ErrorMessage="请上传个人履历"></asp:RequiredFieldValidator>
            </td>
          <td width="30%">&nbsp;</td>
        </tr>
        <tr>
          <td  height="30" align="right">服务项目：</td>
          <td>
                <asp:TextBox ID="txtType" runat="server" CssClass="input_reg"  onfocus="javascript:this.blur();" />
                <input type="button" id="btnType" class="widebuttons" value="选择..." onclick="btnTypeClick();" />
                <asp:HiddenField ID="hidTypeIdP" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="txtType" Display="None" ErrorMessage="请选择服务项目"></asp:RequiredFieldValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">个人作品：</td>
          <td>
                <div class="fileinputs">
	                <input type="file" class="file" id="fileProductfile" runat="server" />
	                <div class="fakefile">
	                </div>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  ControlToValidate="fileProductfile" Display="None" ErrorMessage="请上传个人作品"></asp:RequiredFieldValidator>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td height="30" align="right">个人报价：</td>
          <td>
                <div class="fileinputs">
	                <input type="file" class="file" id="filePricefile" runat="server" />
	                <div class="fakefile">
	                </div>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="filePricefile" Display="None" ErrorMessage="请上传个人报价"></asp:RequiredFieldValidator>
            </td>
          <td></td>
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
                  onclick="btnSubmit_Click" Text="提交"/>
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