<%@ Page Language="c#" Inherits="FrameSite.Include.Page.DefaultWorkSpace" Codebehind="DefaultWorkSpace.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>系统首页</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../public/css/style.css" rel="stylesheet">

    <script language="javascript" src="../../public/js/syscomm.js"></script>

</head>
<body style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;
    border: 0px; margen: 0px">
    <form id="frmMain" runat="server">
    <table style="magin: 0px; border: 0px; width: 100%; height: 100%; border-top: 5px solid #F0931A;
        border-bottom: 5px solid #ffb424;" cellpadding="0" cellspacing="0">
        <%--        <tr>
            <td style="background-image:url('/image/tl.jpg');width:10px"></td>
            <td style="background-image:url('/image/tc.jpg');background-repeat:repeat-x;width:90px"></td>
            <td style="background-image:url('/image/tr.jpg');width:7px"></td>
            <td style="width: 5px; filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#F0931A,endcolorstr=#ffb424,gradientType=0)"></td>
        </tr>--%>
        <tr>
            <td style="width: 5px; filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#F0931A,endcolorstr=#ffb424,gradientType=0)">
            </td>
            <td valign="top">
                            <table width="100%" style="height:100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <img src="/images/tableleft.gif" border="0" style="vertical-align:top"  />
                        </td>
                        <td align="right">
                            <img src="/images/tableright.gif" border="0" style="vertical-align:top"  />
                        </td>
                    </tr>
                </table>
                </td></tr>
                <tr><td style="height:100%">
                <table width="100%" border="0" height="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="padding: 20px" colspan="3">
                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<img src="/images/work.gif" border="0" style="vertical-align: bottom"
                                            alt="" />
                                    </td>
                                    <td align="right" valign="bottom">
                                        <asp:Label ID="lbDate" runat="server" Width="30%"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                            ID="lblCaption" runat="server" Width="60%" Style="font-weight: bold; font-size: 12px;
                                            vertical-align: bottom"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: #f8efe7 1px solid; width: 100%;" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </td></tr>
                <tr><td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" valign="bottom">
                    <tr>
                        <td align="left">
                            <img src="/images/tablebleft.gif" border="0" style="vertical-align:bottom"  />
                        </td>
                        <td align="right">
                            <img src="/images/tablebright.gif" border="0"style="vertical-align:bottom"  />
                        </td>
                    </tr>
                </table>
                </td></tr></table>
            </td>
            <td style="width: 5px; filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#F0931A,endcolorstr=#ffb424,gradientType=0)">
            </td>
        </tr>
        <%--				<tr>
					<td colspan="2" style="HEIGHT: 1px">
						&nbsp;
					</td>
				</tr>--%>
        <!--<tr>
					<td style="PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px" valign="top">
						<asp:Panel ID="NotifyContainer" Runat="server">
							<TABLE class="tableView" width="100%">
								<TR>
									<TD class="heading">通知</TD>
								</TR>
								<TR>
									<TD class="oddrowdata-l" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px">
										<DIV id="divNotify" style="WIDTH: 100%%; HEIGHT: 14px" runat="server"></DIV>
									</TD>
								</TR>
							</TABLE>
						</asp:Panel>
					</td>
					<td style="PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;WIDTH:220px;PADDING-TOP:4px"
						valign="top">
						<asp:Panel ID="OtherAppContainer" Runat="server">
							<TABLE class="tableView" width="100%">
								<TR>
									<TD class="heading">单点登录相关站点</TD>
								</TR>
								<TR>
									<TD class="oddrowdata-l" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; WIDTH: 220px; PADDING-TOP: 4px">
										<DIV id="divApp" style="HEIGHT: 14px" runat="server"></DIV>
									</TD>
								</TR>
							</TABLE>
						</asp:Panel>
						<br>
						<asp:Panel ID="HistoryContainer" Runat="server" Visible="False">
							<TABLE class="tableView" width="100%">
								<TR>
									<TD class="heading">最近访问的功能</TD>
								</TR>
								<TR>
									<TD class="oddrowdata-l" style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; WIDTH: 220px; PADDING-TOP: 4px">
										<DIV id="divHistory" runat="server"></DIV>
									</TD>
								</TR>
							</TABLE>
						</asp:Panel>
					</td>
				</tr>-->
    </table>
    </form>
</body>
</html>
