<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceSelect.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.AttendanceSelect" MasterPageFile="../Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script src="../js/jquery-1.4.min.js" type="text/javascript"></script>

    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function opennewwindow(flag) {
            if (flag == 1) {
                window.open("LeaveEdit.aspx", "申请请假单");
            }
            else {
                window.open("/Overtime/SingleOvertimeEdit.aspx", "申请OT单");
            }
        }
        
        
    </script>

    <table width="100%" border="0" cellpadding="10" cellspacing="0" background="../images/renli_24.gif"
        style="background-repeat: repeat-x;">
        <tr>
            <td width="674">
                <span style="color: #B50000; font-size: 9pt;">
                    <%=TipsInfo%></span>
                <ComponentArt:Calendar ID="cldAttendance" runat="server" AutoPostBackOnSelectionChanged="false"
                    AutoPostBackOnVisibleDateChanged="true" AllowDaySelection="true" ControlType="Calendar"
                    DayNameFormat="Full" ClientTarget="Auto" CalendarCssClass="calendar" MonthCssClass="month"
                    DayHeaderCssClass="dayheader" CalendarTitleCssClass="CalendarTitle" OtherMonthDayCssClass="othermonthday"
                    SelectedDayCssClass="selectedday" NextPrevCssClass="nextprev" UseServersTodaysDate="true"
                    ImagesBaseUrl="../images/calendar" PrevImageUrl="prev_wht.gif" NextImageUrl="next_wht.gif"
                    MonthPadding="0" MonthSpacing="0" PopUp="None" MinDate="2009-07-01" ReactOnSameSelection="false"
                    RenderSearchEngineStamp="false" OnVisibleDateChanged="cldAttendance_VisibleDateChanged">
                    <Templates>
                        <ComponentArt:CalendarDayCustomTemplate ID="DefaultTemplate" runat="server">
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
                    </Templates>
                </ComponentArt:Calendar>
                <br />
                <asp:UpdatePanel ID="monthstat" runat="server">
                    <ContentTemplate>
                        <table width="674" border="0" cellpadding="0" cellspacing="0" bgcolor="#e4e4e4" runat="server">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td valign="top">
                <br />
                <b>图例说明：</b>
                <table width="295" border="0" cellspacing="3" cellpadding="0">
                    <tr>
                        <td width="94" height="33" style="background-repeat: no-repeat;">
                            <img src="../images/big_chidao.jpg" id="imglate" runat="server" title="迟到" />
                        </td>
                        <td width="94" style="background-repeat: no-repeat;">
                            <img src="../images/big_zaotui.jpg" id="imgleaveearly" runat="server" title="早退" />
                        </td>
                        <td width="94" height="33" style="background-repeat: no-repeat;">
                            <img src="../images/big_kuanggong.jpg" id="imgabsent" runat="server" title="旷工" />
                        </td>
                    </tr>
                    <tr>
                        <td height="33" style="background-repeat: no-repeat;">
                            <img src="../images/big_qingjia.jpg" id="imgleave" runat="server" title="请假" />
                        </td>
                        <td height="33" style="background-repeat: no-repeat;">
                            <img src="../images/big_chuchai.jpg" id="imgtravel" runat="server" title="出差" />
                        </td>
                        <td style="background-repeat: no-repeat;">
                            <img src="../images/big_waichu.jpg" id="imgout" runat="server" title="外出" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" height="50">
                            <span><b>说明：</b><br />
                                1.【添加事由】：给日常考勤添加事由信息。<br />
                                2.【提交审批】：提交当月的考勤统计信息。<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;提交月考勤统计之前必须所有的事由都审批通过。<br />
                                3.<font color="red" style="font-size: 14px; font-weight: bold;">&nbsp;提交离职当月考勤请勾选下面复选框。</font><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDimission" runat="server" Text="当月离职" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddMatters" Text="添加事由" OnClick="btnAddMatters_Click" runat="server" />
                        </td>
                        <td colspan="2">
                            <asp:Button ID="ImageButton2" Text="提交整月考勤" OnClientClick="if(confirm('您确定要提交整月考勤？提交后不可以回退。')==true){  showLoading();}else{return false;}"
                                OnClick="btnSubmit_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="labRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
