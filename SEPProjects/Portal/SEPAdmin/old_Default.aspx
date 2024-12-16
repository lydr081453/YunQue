<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="old_Default.aspx.cs" Inherits="SEPAdmin.old_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<title>系统管理</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<frameset rows="85,*" border="0" frameborder="0" framespacing="0" name="frmContainer">
		<frame src="Header.aspx" name="header" noresize scrolling="no">
		<frameset cols="200,*" border="0" frameborder="0" framespacing="0" id="MainFrame" scrolling="no">
			<frame src="Tree.aspx" name="tree" scrolling="yes">
			<frame src="<%=WorkSpaceUrl %>" name="modify" scrolling="yes">
		</frameset>
	</frameset>
</html>
