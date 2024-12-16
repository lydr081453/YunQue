<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="SEPAdmin.Header" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="HEAD1" runat="server">
    <link href="public/css/css.css" rel="stylesheet" />

    <script language="javascript" src="public/js/syscomm.js"></script>

    <script language="javascript">
        function ShowNavigator() {
            if (document.getElementById("Navigator").getAttribute("hidden").trim() == "false") {
                top.document.getElementById("MainFrame").cols = "0,*";
                document.getElementById("Navigator").setAttribute("hidden", "true");
               
                document.getElementById("Navigator").src = "images/sys_theme12_03-05_2.gif";
                
            }
            else {
                top.document.getElementById("MainFrame").cols = "200,*";
                document.getElementById("Navigator").setAttribute("hidden", "false");                
                    document.getElementById("Navigator").src = "images/sys_theme12_03-05.gif";
                
            }


        }
    </script>

</head>
<body>
<form runat="server" id="form1">
<table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/sys_theme12_030.gif" style="background-repeat:repeat-x;">
  <tr>
    <td height="89" background="images/sys_theme12_01.gif" style=" background-repeat:no-repeat;"><table width="610" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="223" align="center"><img src="images/sys_theme12_16.gif" width="126" height="28" /></td>
        <td align="center"><a href="/HR/Default.aspx" target="modify"><img src="images/sys_theme12_03.gif" width="50" height="62" /></a></td>
        <td><img src="images/sys_theme1aaa2_03.gif" width="2" height="62" /></td>
        <td align="center"><a href="javascript:ShowNavigator();" ><img src="images/sys_theme12_03-05.gif" width="55" height="62" id="Navigator" hidden="false" /></a></td>
        <%--<td><img src="images/sys_theme1aaa2_03.gif" width="2" height="62" /></td>
        <td align="center"><a href="#"><img src="images/sys_theme12_03-07.gif" width="55" height="62" /></a></td>--%>
        <td><img src="images/sys_theme1aaa2_03.gif" width="2" height="62" /></td>
        <td align="center"><a target='_top' href='ToHome.aspx'><img src="images/sys_theme12_03-09.gif" width="59" height="62" /></a></td>
      </tr>
    </table>      
    </td>
    <td width="360" align="right" valign="bottom" style=" background-repeat:no-repeat; padding:0 10px 7px 0;"><table width="200" border="0" align="left" cellpadding="0" cellspacing="0" background="images/sys_theme12_20-08.gif">
      <tr>
        <td align="center" background="images/sys_theme12_20.gif" style="background-repeat:no-repeat;"><img src="images/sys_theme12_28.gif" width="15" height="15" hspace="5" />当前用户：<asp:Label ID="lblCaption" runat="server"></asp:Label></td>
        <td width="13" align="right"><img src="images/sys_theme12_20-10.gif" width="13" height="24" /></td>
        </tr>
    </table>    
      <table width="160" border="0" align="left" cellpadding="0" cellspacing="0" background="images/sys_theme12_20-08.gif">
        <tr>
          <td align="center" background="images/sys_theme12_20.gif" style="background-repeat:no-repeat;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="30" align="right"><img src="images/sys_theme12_25.gif" width="16" height="18" /></td>
              <td width="35" align="center"><%--<a target='_parent' href='#' OnClick="ImageButton_Click" runat="server">注销</a>--%><asp:LinkButton ID="lbSignOut" OnClick="ImageButton_Click" Text="注销" runat="server" /></td>
              <td><img src="images/centerline.gif" width="2" height="14" hspace="5" /></td>
              <td width="12"><img src="images/sys_theme12_31.gif" width="12" height="15" hspace="5" /></td>
              <td><a target='modify' href='/PSWChange.aspx'>更改密码</a></td>
            </tr>
          </table></td>
          <td width="13" align="right"><img src="images/sys_theme12_20-10.gif" width="13" height="24" /></td>
        </tr>
      </table></td>
  </tr>
</table>
</form>
</body>
</html>
