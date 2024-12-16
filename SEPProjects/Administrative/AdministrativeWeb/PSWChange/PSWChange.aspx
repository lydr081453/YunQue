<%@ Page Language="C#" AutoEventWireup="true" Inherits="PSWChange_PSWChange" Codebehind="PSWChange.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>更改密码</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../public/css/style.css" rel="stylesheet">
        <script src="../js/syscomm.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<table style="WIDTH: 100%">
				<tr>
					<td class="menusection-Packages" colSpan="2">系统管理 &gt; 更改密码</td>
				</tr>
				<tr>
					<td colspan="2" style="padding:2px;">
						&nbsp;<asp:Label ID="lblMsg" Runat="server" CssClass="message"></asp:Label>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px">
						<table width="100%" class="tableForm" ID="Table2">
							<tr>
								<td class="heading" colspan="3">详细信息</td>
							</tr>
							<tr>
								<td class="oddrow" style="WIDTH:100px">
									旧密码:
								</td>
								<td class="oddrow-l" style="WIDTH:120px">
									<asp:TextBox ID="OldPSW" Runat="server" Width="120" TextMode="Password"></asp:TextBox>
								</td>
								<td class="oddrow-l">
									<asp:RequiredFieldValidator ID="RVOldPSW" Runat="server" ControlToValidate="OldPSW" ErrorMessage="请输入原密码">*</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="oddrow" style="WIDTH:100px">
									新密码:
								</td>
								<td class="oddrow-l" style="WIDTH:120px">
									<asp:TextBox ID="NewPSW" Runat="server" Width="120" TextMode="Password"></asp:TextBox>
								</td>
								<td class="oddrow-l">
									<asp:RequiredFieldValidator ID="RVNewPSW" Runat="server" ControlToValidate="NewPSW" ErrorMessage="请输入新密码">*</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td class="oddrow" style="WIDTH:100px">
									确认密码:
								</td>
								<td class="oddrow-l" style="WIDTH:120px">
									<asp:TextBox ID="ConfirmPSW" Runat="server" Width="120" TextMode="Password"></asp:TextBox>
								</td>
								<td class="oddrow-l">
									<asp:RequiredFieldValidator ID="RVConfirmPSW" Runat="server" ControlToValidate="ConfirmPSW" ErrorMessage="请输入确认密码">*</asp:RequiredFieldValidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Button ID="btnUpdate" CssClass="widebuttons" Text="  提交  " Runat="server" onclick="btnUpdate_Click"></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

