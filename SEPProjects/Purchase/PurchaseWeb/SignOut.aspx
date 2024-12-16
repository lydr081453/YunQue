<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignOut" Codebehind="SignOut.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD runat="server">
		<title>系统注销</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="public/css/style.css" rel="stylesheet">
		<script language="javascript" src="public/js/syscomm.js"></script>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<input id="hidAction" type="hidden" runat="server" NAME="hidAction">
			<table style="WIDTH: 100%">
				<tr>
					<td class="menusection-Packages" colSpan="2">提示信息</td>
				</tr>
				<tr>
					<td style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px"
						colSpan="2" runat="server" ID="Td1"><br>
						<table class="tableView" style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-BOTTOM-WIDTH: 1px; BORDER-RIGHT-WIDTH: 1px"
							width="100%">
							<tr>
								<td class="heading"><asp:label id="lblCaption" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>正在注销，请稍候...
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="divSSO" runat="server">
			</div>
		</form>
	</body>
</HTML>
