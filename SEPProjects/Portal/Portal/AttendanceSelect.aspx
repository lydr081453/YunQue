<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceSelect.aspx.cs"
    Inherits="Portal.WebSite.AttendanceSelect" MasterPageFile="Default.Master" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
        function AboutTaskItems() {
            jQuery.blockUI({
                message: $("#aboutTaskItems"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }
        function AboutTaskItems2() {
            jQuery.blockUI({
                message: $("#aboutTaskItems2"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }

        function opennewwindow(flag) {
            if (flag == 1) {
                window.open("<%=WebSiteInfoUrl %>/Attendance/LeaveEdit.aspx", "申请请假单");
            }
            else {
                window.open("<%=WebSiteInfoUrl %>/Overtime/SingleOvertimeEdit.aspx", "申请OT单");
            }
        }

        function addmatter() {

        }
    </script>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td valign="top" class="nav">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="5px"></td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/menu_bg.jpg">
                    <tr>
                        <td width="5">
                            <img src="images/a1_113.gif" width="5" height="32" />
                        </td>
                        <td class="top">考勤管理...
                        </td>
                        <td width="75" valign="bottom">
                            <a href="Default.aspx">
                                <img src="images/renlibtn_11.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3"></td>
                        <td width="75" valign="bottom">
                            <a href="HumanMap.aspx">
                                <img src="images/renlibtn_09.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3"></td>
                        <td width="75" valign="bottom">
                            <a href="AttendanceSelect.aspx">
                                <img src="images/attendance1.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="6"></td>
                        <td width="5">
                            <img src="images/a1_19.gif" width="5" height="32" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="10" cellspacing="0" background="images/renli_24.gif"
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
                                MonthPadding="0" MonthSpacing="0" PopUp="None" MinDate="2009-07-01" OnVisibleDateChanged="cldAttendance_VisibleDateChanged">
                                <Templates>
                                    <ComponentArt:CalendarDayCustomTemplate ID="DefaultTemplate">
                                        <Template>
                                            <div style="float: right; font-size: larger">
                                                <%# DataBindGetDay(Container.DataItem) %>&nbsp;
                                            </div>
                                            <div>
                                                <%# DataBindGetClockIn(Container.DataItem) %>&nbsp;
                                            </div>
                                            <div>
                                                <%# DataBindGetClockOut(Container.DataItem) %>&nbsp;
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <div style="position: relative; top: 20px">
                                                <%# DataBindGetIconsHtml(Container.DataItem) %>
                                            </div>
                                        </Template>
                                    </ComponentArt:CalendarDayCustomTemplate>
                                    <ComponentArt:CalendarDayCustomTemplate ID="OtherMonthDayTemplate">
                                        <Template>
                                        </Template>
                                    </ComponentArt:CalendarDayCustomTemplate>
                                </Templates>
                            </ComponentArt:Calendar>
                            <br />
                            <table width="674" border="0" cellpadding="0" cellspacing="0" bgcolor="#e4e4e4">
                                <tr>
                                    <td width="4" style="background-image: url(images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                        <img src="images/kaoqin_36.jpg" width="4" height="27" />
                                    </td>
                                    <td style="background-image: url(images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="20%" height="27" align="left" style="padding-left: 10px;">
                                                    <strong>考勤统计</strong>（天：D，小时：H）
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="4" style="background-image: url(images/kaoqin_37.jpg); background-repeat: repeat-x;"
                                        align="right">
                                        <img src="images/kaoqin_42.jpg" width="4" height="27" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="padding-bottom: 4px;">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                                            <tr>
                                                <td height="25" align="center" bgcolor="#ececec">迟到
                                                </td>
                                                <td align="center" bgcolor="#ececec">早退
                                                </td>
                                                <td align="center" bgcolor="#ececec">旷工
                                                </td>
                                                <td align="center" bgcolor="#ececec">出差
                                                </td>
                                                <td align="center" bgcolor="#ececec">外出
                                                </td>
                                                <td align="center" bgcolor="#ececec">病假
                                                </td>
                                                <td align="center" bgcolor="#ececec">病假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">事假
                                                </td>
                                                <td align="center" bgcolor="#ececec">事假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">年假
                                                </td>
                                                <td align="center" bgcolor="#ececec">年假(年)
                                                </td>
                                                <td align="center" bgcolor="#ececec">年假(补)
                                                </td>
                                                <td align="center" bgcolor="#ececec">婚假
                                                </td>
                                                <td align="center" bgcolor="#ececec">丧假
                                                </td>
                                                <td align="center" bgcolor="#ececec">产假
                                                </td>
                                                <td align="center" bgcolor="#ececec">产检
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
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <br />
                            <b>图例说明：</b>
                            <table width="295" border="0" cellspacing="3" cellpadding="0">
                                <tr>
                                    <td width="94" height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_chidao.jpg" id="imglate" runat="server" />
                                    </td>
                                    <td width="94" style="background-repeat: no-repeat;">
                                        <img src="../images/big_zaotui.jpg" id="imgleaveearly" runat="server" />
                                    </td>
                                    <td width="94" height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_kuanggong.jpg" id="imgabsent" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_qingjia.jpg" id="imgleave" runat="server" />
                                    </td>
                                    <td height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_chuchai.jpg" id="imgtravel" runat="server" />
                                    </td>
                                    <td style="background-repeat: no-repeat;">
                                        <img src="../images/big_waichu.jpg" id="imgout" runat="server" />
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_jiaban.jpg" id="imgovertime" runat="server" />
                                    </td>
                                    <td height="33" style="background-repeat: no-repeat;">
                                        <img src="../images/big_xiaoxiu.jpg" id="imgofftune" runat="server" />
                                    </td>
                                    <td height="33" style="background-repeat: no-repeat;">
                                        &nbsp;<%--<img src="../images/big_qita.jpg" id="imgother" runat="server" />
                                    </td>
                                </tr>--%>
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
                                        <asp:Button ID="ImageButton2" Text="提交整月考勤"  OnClientClick="if(confirm('您确定要提交整月考勤？提交后不可以回退。')==true){  showLoading();}else{return false;}"
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
            </td>
            <td width="10">&nbsp;
            </td>
            <td width="300" valign="top" class="navright">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="about">
                    <tr>
                        <td height="190" valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title" colspan="2">
                                        <img src="images/a1_30.gif" width="17" height="15" />
                                        我的信息
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="94">
                                                    <table width="90" border="0" cellpadding="1" cellspacing="1" bgcolor="#999999">
                                                        <tr>
                                                            <td bgcolor="#FFFFFF">
                                                                <img id="imgUserIcon" runat="server" width="90" height="90" hspace="0" vspace="0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="174" style="padding-left: 10px;">
                                                    <asp:Label ID="labUserInfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_asset.gif" width="17" height="15" />
                                        资产领用
                                    </td>
                                    <td class="title">
                                        <a href='http://xf.shunyagroup.com/Return/SalaryView.aspx' target="_blank" style="text-decoration: none; color: black;">
                                            <img src="/images/salary.gif" />
                                            薪资查询 </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <asp:Label ID="lblAsset" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="notice" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_48.gif" width="19" height="16" />
                                        公告
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1">
                                        <img src="images/a1_51.gif" width="269" height="2" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="财务部重要提醒2024年12月25日前需协助完成事宜.docx">财务部重要提醒 - 2024年12月25日前需协助完成事宜</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销与借款管理条例.docx">费用申请、报销与借款管理条例</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销细则.docx">费用申请报销细则</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px;">
                                    &nbsp;<br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
