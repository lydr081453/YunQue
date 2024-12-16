<%@ Page language="c#" Inherits="FrameWork.Web.Public.UserChoice" Codebehind="UserChoice.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD  runat="server">
		<base target="_self">
		<title>�û�ѡ��</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/style.css" rel="stylesheet">
		<script language="javascript" src="../js/syscomm.js"></script>
		<script language="javascript">
		function CheckInput(recordCount)
		{
			if(document.all("QueryStr").value.trim()=="")
			{
				if(confirm("��û��ѡ���κβ������ò�ѯ������ǰ" + recordCount + "�����ݡ��Ƿ������")==false)
				{
					document.all("QueryStr").focus();
					return false;
				}
			}
			
			return true;
		}
		
		// ѡ������û�
		function SelectRow(userCode,userName,positionDescription)
		{
			window.returnValue = "<?xml version='1.0' encoding='gb2312'?><recordset userCode='" + XmlEncode(userCode) + "' userName = '" + XmlEncode(userName) + "' positionDesrciption = '" + XmlEncode(positionDescription) + "' />";
			window.close();
		}

		</script>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<table style="WIDTH: 100%">
				<tr>
					<td class="menusection-Packages" colSpan="2">�û�ѡ��</td>
				</tr>
				<tr>
					<td colspan="2" style="PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px">
						<table width="100%" class="tableView" ID="Table2">
							<tr>
								<td class="heading">�û���Ϣ�б�</td>
							</tr>
							<tr>
								<td class="oddrow-l">�������û���Ż�������:
									<asp:TextBox ID="QueryStr" Runat="Server"></asp:TextBox>&nbsp;
									<asp:Button CssClass="widebuttons" ID="btnQuery" Runat="server" Text="  ����  " onclick="btnQuery_Click"></asp:Button>
								</td>
							</tr>
							<tr>
								<td>
									<asp:datagrid id="DgBrow" runat="server" EnableViewState="False" AutoGenerateColumns="False">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
										<AlternatingItemStyle HorizontalAlign="Left" CssClass="oddrowdata"></AlternatingItemStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="evenrowdata"></ItemStyle>
										<HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="UserCode" HeaderText="���" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="����">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<a href="javascript:SelectRow('<%#DataBinder.Eval(Container.DataItem,"UserCode")%>','<%#DataBinder.Eval(Container.DataItem,"UserName")%>','<%#DataBinder.Eval(Container.DataItem,"PositionDescription")%>');"><%#DataBinder.Eval(Container.DataItem,"UserName")%></a>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="PositionDescription" HeaderText="ְλ����" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
										</Columns>
									</asp:datagrid>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>