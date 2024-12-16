<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntegratedQueryView.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.IntegratedQueryView" MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="IntegratedQuery" TagName="LateSearch" Src="~/UserControls/IntegratedQuery/LateSearch.ascx" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list_2">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="200" valign="top" background="../images/t1_28.jpg" style="background-repeat: repeat-x;">
                                <!-- 人力节点树 -->
                                <asp:UpdatePanel ID="upTreeView" runat="server" RenderMode="Block">
                                    <ContentTemplate>
                                        <ComponentArt:TreeView ID="userTreeView" Height="500" Width="220" DragAndDropEnabled="false"
                                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                                            ItemSpacing="0" ImagesBaseUrl="images/" NodeLabelPadding="3" ShowLines="true"
                                            LineImagesFolderUrl="../images/lines/" EnableViewState="true" runat="server">
                                        </ComponentArt:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top">
                                <asp:UpdatePanel ID="upUserInfo" runat="server" RenderMode="Inline" Visible="false">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellspacing="10" cellpadding="0" backgsround="../images/t1_28.jpg"
                                            style="margin: 10px 0 0 20px;">
                                            <tr>
                                                <td>
                                                    <span>
                                                        <%=CurAttendanceUserInfo %></span>
                                                    <ComponentArt:Calendar ID="cldAttendance" runat="server" AutoPostBackOnSelectionChanged="false"
                                                        AutoPostBackOnVisibleDateChanged="true" AllowDaySelection="true" ControlType="Calendar"
                                                        DayNameFormat="Full" ClientTarget="Auto" CalendarCssClass="calendar" MonthCssClass="month"
                                                        DayHeaderCssClass="dayheader" CalendarTitleCssClass="CalendarTitle" OtherMonthDayCssClass="othermonthday"
                                                        SelectedDayCssClass="selectedday" NextPrevCssClass="nextprev" UseServersTodaysDate="true"
                                                        ImagesBaseUrl="../images/calendar" PrevImageUrl="prev_wht.gif" NextImageUrl="next_wht.gif"
                                                        MonthPadding="0" MonthSpacing="0" PopUp="None" MinDate="2009-07-01" OnVisibleDateChanged="cldAttendance_VisibleDateChanged">
                                                        <Templates>
                                                            <ComponentArt:CalendarDayCustomTemplate ID="DefaultTemplate">
                                                                <Template>
                                                                    <div style="float: right; font-size: larger">
                                                                        <%# DataBindGetDay(Container.DataItem) %>&nbsp;</div>
                                                                    <div>
                                                                        <%# DataBindGetClockIn(Container.DataItem) %>&nbsp;</div>
                                                                    <div>
                                                                        <%# DataBindGetClockOut(Container.DataItem) %>&nbsp;</div>
                                                                    <div style="clear: both">
                                                                    </div>
                                                                    <div style="position: relative; top: 20px">
                                                                        <%# DataBindGetIconsHtml(Container.DataItem) %></div>
                                                                </Template>
                                                            </ComponentArt:CalendarDayCustomTemplate>
                                                            <ComponentArt:CalendarDayCustomTemplate ID="OtherMonthDayTemplate">
                                                                <Template>
                                                                </Template>
                                                            </ComponentArt:CalendarDayCustomTemplate>
                                                        </Templates>
                                                    </ComponentArt:Calendar>
                                                    <br />
                                                    <asp:UpdatePanel ID="monthstat" runat="server">
                                                        <ContentTemplate>
                                                            <table id="Table1" width="674" border="0" cellpadding="0" cellspacing="0" bgcolor="#e4e4e4"
                                                                runat="server">
                                                                <tr>
                                                                    <td width="4" style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                                                        <img src="../images/kaoqin_36.jpg" width="4" height="27" />
                                                                    </td>
                                                                    <td style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td width="20%" height="27" align="left" style="padding-left: 10px;">
                                                                                    <strong>考勤统计</strong>（天：D，小时：H）
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td width="4" style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;"
                                                                        align="right">
                                                                        <img src="../images/kaoqin_42.jpg" width="4" height="27" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td style="padding-bottom: 4px;">
                                                                           <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                                            <tr>
                                                <td height="25" align="center" bgcolor="#ececec">
                                                    迟到
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    早退
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    旷工
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    出差
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    外出
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    病假
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    病假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    事假
                                                </td>
                                                 <td align="center" bgcolor="#ececec">
                                                    事假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    年假
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                   年假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    年假(补)
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    婚假
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    丧假
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    产假
                                                </td>
                                                <td align="center" bgcolor="#ececec">
                                                    产检
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td height="25" align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labLate" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labAbsent" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labEvection" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labEgress" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labSickLeave" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="lblSickTotal" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labAffiairLeave" runat="server"></asp:Label>
                                                </td>
                                                 <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labAffairTotal" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labAnnualLeave" runat="server"></asp:Label>
                                                </td>
                                                  <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="lblAnnualTotal" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labAnnualLast" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labMarriageLeave" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labFuneralLeave" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labMaternityLeave" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" bgcolor="#ffffff">
                                                    <asp:Label ID="labPrenatalCheck" runat="server"></asp:Label>
                                                </td>
                                               
                                            </tr>
                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <asp:Label ID="labApproveDesc" runat="server"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="50">
                                <span><b>说明：</b><br />
                                    <font color="red" style="font-size: 14px; font-weight: bold;">&nbsp;提交离职当月考勤请勾选下面复选框。</font><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDimission" runat="server" Text="当月离职" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td  colspan="2" valign="bottom" align="left">
                                <asp:Button ID="ImageButton2" Text="提交整月考勤" OnClientClick="return confirm('您确定要提交整月考勤？');"
                                    OnClick="btnSubmit_Click" runat="server" />&nbsp;&nbsp;
                                <asp:Button ID="btnDelete" Text="撤销整月考勤" OnClientClick="return confirm('您确定要撤销整月考勤？');"
                                    OnClick="btnDelete_Click" runat="server" />&nbsp;&nbsp;
                                <asp:Button ID="btnExport" Text="导出" ToolTip="导出当月的考勤记录信息"
                                    Width="52" Height="29" hspace="10" OnClientClick="return confirm('您确定要导出当月考勤记录？');"
                                    OnClick="btnExport_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
