<%@ Page language="c#" Inherits="FrameSite.Web.include.page.Header" Codebehind="Header.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<link href="../../public/css/css.css" rel="stylesheet">
			<script language="javascript" src="../../public/js/syscomm.js"></script>
			<script language="javascript">
		function ShowNavigator() {
		    if (document.getElementById("Navigator").getAttribute("hidden").trim() == "false") {
	        top.document.getElementById("MainFrame").cols = "0,*";
	        document.getElementById("Navigator").setAttribute("hidden", "true");
	        if (document.all)
	            document.getElementById("Navigator").innerText = "��ʾ����";
			else
			    document.getElementById("Navigator").textContent = "��ʾ����";
		}
		else
		{
		    top.document.getElementById("MainFrame").cols = "10,73";
		    document.getElementById("Navigator").setAttribute("hidden", "false");
		    if(document.all)
		        document.getElementById("Navigator").innerText = "���ص���";
		        else
		            document.getElementById("Navigator").textContent = "���ص���";
		}
		
		
	}
			</script>
			<script language="javascript">
			    function get_time() {
			        var date = new Date();
			        var year = "", month = "", day = "", week = "", hour = "", minute = "", second = "";
			        year = date.getFullYear();
			        month = add_zero(date.getMonth() + 1);
			        day = add_zero(date.getDate());
			        week = date.getDay();
			        switch (date.getDay()) {
			            case 0: val = "������"; break
			            case 1: val = "����һ"; break
			            case 2: val = "���ڶ�"; break
			            case 3: val = "������"; break
			            case 4: val = "������"; break
			            case 5: val = "������"; break
			            case 6: val = "������"; break
			        }
			        hour = add_zero(date.getHours());
			        minute = add_zero(date.getMinutes());
			        second = add_zero(date.getSeconds());
			       
			        if (document.all)
			            showdate.innerText = "" + year + "." + month + "." + day + " / " + +hour + ":" + minute + ":" + second;
			        else
			            showdate.textContent = "" + year + "." + month + "." + day + " / " + +hour + ":" + minute + ":" + second;
			        
			    }
			    function add_zero(temp) {
			        if (temp < 10) return "0" + temp;
			        else return temp;
			    }
			    setInterval("get_time()", 1000);
</script>
	</HEAD>
<body class="topbar">
		<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td background="../../images/top_bg.gif" style="background-repeat:repeat-x;">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="83" align="right" valign="bottom" background="../../images/blue_inside (1).gif" style="background-repeat:no-repeat;padding-right:40px;"><table width="530" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="75%" align="left" style="padding-bottom:20px;">&nbsp;</td>
              <td width="25%" align="right" class="toplink" style="padding-bottom:20px;"><a target='_parent' href='../../SignOut.aspx'>ע��</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target='modify' href='../../PSWChange/PSWChange.aspx'>
									��������</a></td>
            </tr>
            <tr>
              <td align="left" class="toplink" style="padding-bottom:20px;"><span class="toplink" style="padding-bottom:20px;"><a target='_top' href='ToHome.aspx'>
									<strong>����ϵͳ</strong></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a target='_parent' href='../../default.aspx'>
									<strong>�ɹ�ϵͳ��ҳ</strong></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:ShowNavigator();' id="Navigator" hidden="false" style="font-weight:bold">���ص���</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblCaption" runat="server" CssClass="time"></asp:Label></span></td>
              <td align="right" class="time" style="padding-bottom:20px;"><div id="showdate" /></td>
            </tr>
          </table></td>
        </tr>
      </table>
      </td></tr></table>
	</body>
</HTML>
