<%@ Page language="c#" Inherits="FrameSite.Web.include.page.Header" Codebehind="Header.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<%--<link href="../../public/css/style.css" rel="stylesheet">--%>
			<script language="javascript" src="../../public/js/syscomm.js"></script>
			<script language="javascript">
	function ShowNavigator(obj)
	{
		if(document.all("imgs").getAttribute("hidden").trim()=="false")
		{
			top.MainFrame.cols = "1,*";
			document.all("imgs").setAttribute("hidden","true");
			obj.src="/images/head_006.png"; 
			
		}
		else
		{
			top.MainFrame.cols = "16%,*";
			document.all("imgs").setAttribute("hidden","false");
			obj.src="/images/head_06.png"; 
		}
		
		
	}
			</script>
	</HEAD>
<body style="margin:0" >
<table id="__01" width="100%" height="85" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td rowspan="2" align="left">
			<img src="/images/head_01.png" width="12" height="85" border="0"  alt=""></td>
		<td colspan="4" align="left">
			<img src="/images/head_02.png" width="354" height="57" border="0" alt=""></td>
		<td rowspan="2" width="100%"  background="/images/head_03.png"></td>
		<td rowspan="2" align="right">
			<img src="/images/head_04.png" width="356" height="85" border="0" alt=""></td>
	</tr>
	<tr>
		<td align="left">
			<a target='_top' href='<%= PortalUrl%>'><img src="/images/head_05.png" width="87" height="28" border="0" alt=""></a></td>
		<td align="left">
			<img src="/images/head_06.png" id="imgs" width="90" height="28" border="0" onclick="ShowNavigator(this);" hidden="false" alt=""></td>
		<td align="left">
			<a target='_top' href='<%= SignOutUrl %>'><img src="/images/head_07.png" width="87" height="28" border="0" alt=""></a></td>
		<td align="left">
			<a target='modify' href='../../PSWChange/PSWChange.aspx'><img src="/images/head_08.png" width="90" height="28" border="0" alt=""></a></td>
	</tr>
</table>
</body>

</HTML>
