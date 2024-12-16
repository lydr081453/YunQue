<%@ Page language="c#" Inherits="FrameSite.Web.Public.CalendarSel" Codebehind="CalendarSel.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>����</title>
		<base target="_self">
		<meta http-equiv="Pragma" content="no-cache">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../js/syscomm.js"></script>
		<style>
			a:link{ color: #000000; text-decoration: none; }
			a:visited{ color: #000000; text-decoration: none; }
			a:active{ color: #000000; text-decoration: none; }
			a:hover{ color:#FF9900; text-decoration: underline; }
		</style>
	</HEAD>
	<body topmargin="0" leftmargin="0" scroll="no">
		<form id="frmMain" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
				<TR>
					<TD align="right" bgColor="silver">
						<asp:linkbutton id="m_btnDeduct" runat="server" BorderStyle="None" Font-Bold="True" Text="<<" Font-Names="����"
							Font-Size="8pt" ForeColor="White" Width="12px" Height="14px" BackColor="Silver" onclick="m_btnDeduct_Click"><<</asp:linkbutton>
					</TD>
					<td align="right">
						<asp:dropdownlist id="m_ddlYear" runat="server" Font-Size="9pt" Width="50px" Height="22"
							BackColor="Silver" AutoPostBack="True" onselectedindexchanged="m_ddlYear_SelectedIndexChanged"></asp:dropdownlist>
					</td>
					<td align="right">
						<asp:linkbutton id="m_btnAdd" runat="server" BorderStyle="None" Font-Bold="True" Text=">>" Font-Names="����"
							Font-Size="8pt" ForeColor="White" Width="12px" Height="14px" BackColor="Silver" onclick="m_btnAdd_Click">>></asp:linkbutton>
					</td>
					<td align="right">
						<asp:dropdownlist id="m_ddlMonth" runat="server" Font-Size="9pt" Width="55px" Height="22"
							BackColor="Silver" AutoPostBack="True" onselectedindexchanged="m_ddlMonth_SelectedIndexChanged">
							<asp:ListItem Value="01">һ��</asp:ListItem>
							<asp:ListItem Value="02">����</asp:ListItem>
							<asp:ListItem Value="03">����</asp:ListItem>
							<asp:ListItem Value="04">����</asp:ListItem>
							<asp:ListItem Value="05">����</asp:ListItem>
							<asp:ListItem Value="06" Selected="True">����</asp:ListItem>
							<asp:ListItem Value="07">����</asp:ListItem>
							<asp:ListItem Value="08">����</asp:ListItem>
							<asp:ListItem Value="09">����</asp:ListItem>
							<asp:ListItem Value="10">ʮ��</asp:ListItem>
							<asp:ListItem Value="11">ʮһ��</asp:ListItem>
							<asp:ListItem Value="12">ʮ����</asp:ListItem>
						</asp:dropdownlist></td>
					</TD></TR>
				<TR>
					<TD align="middle" colSpan="4"><FONT face="����"><asp:calendar id="m_cldSelDate" runat="server" BackColor="White" Height="140px" Width="140px"
								PrevMonthText="&amp;lt;&amp;lt;" NextMonthText="&amp;gt;&amp;gt;" CellPadding="1" BorderColor="Silver" ForeColor="#003399" BorderWidth="0px"
								Font-Size="9pt" Font-Names="����" onselectionchanged="m_cldSelDate_SelectionChanged">
								<TodayDayStyle ForeColor="Black" BackColor="White"></TodayDayStyle>
								<DayStyle BackColor="Silver"></DayStyle>
								<NextPrevStyle Font-Size="8pt" ForeColor="White"></NextPrevStyle>
								<DayHeaderStyle Font-Size="9pt" Font-Names="����" Height="1px" ForeColor="White" BorderStyle="None"
									BackColor="Silver"></DayHeaderStyle>
								<SelectedDayStyle Font-Bold="True" ForeColor="Black" BackColor="White"></SelectedDayStyle>
								<TitleStyle Font-Size="9pt" Font-Names="����" Font-Bold="True" BorderWidth="1px" ForeColor="Black"
									BorderStyle="None" BackColor="SteelBlue"></TitleStyle>
								<WeekendDayStyle ForeColor="Red" BackColor="Silver"></WeekendDayStyle>
								<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
							</asp:calendar></FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
