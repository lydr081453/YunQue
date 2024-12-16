<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayMaintain.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.HolidayMaintain" MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>    
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarYear.css" rel="stylesheet" type="text/css" />

    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/renli_24.gif" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17"></td>
                        <td align="center" style="font-size: large; font-weight: bold;"><strong>节假日维护 </strong></td>
                    </tr>
                </table>
                <center>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr class="hdr">
                        <td class="hdr-l"></td>
                        <td align="center" class="hdr-m">
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td><asp:LinkButton CssClass="hdr-m-prev" ID="aPrev" runat="server" OnClick="aPrev_Click" ToolTip="去年"></asp:LinkButton></td>
                                    <td class="month"><%=yearvalue %></td>
                                    <td><asp:LinkButton ID="aNext" runat="server" CssClass="hdr-m-next" OnClick="aNext_Click" ToolTip="明年"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                        <td class="hdr-r"></td>
                    </tr>
                    <tr>
                        <td style="background: url(../images/calendar/static_04.jpg) repeat-y;"></td>
                        <td>
                            <ComponentArt:Calendar id="Calendar1"
                              AllowMultipleSelection="true"
                              AllowMonthSelection="false"
                              AllowWeekSelection="false"
                              ShowCalendarTitle="false"
                              ShowMonthTitle="true"
                              DayNameFormat="Short"
                              CalendarTitleType="VisibleRangeText"
                              CalendarTitleDateFormat="MMMM yyyy"
                              CalendarTitleDateRangeSeparatorString=" - "
                              MonthColumns="3"
                              MonthRows="4"
                              runat="server"
                              Width="100%"
                              Height="100%"
                              CalendarCssClass="cal"
                              DayCssClass="day"
                              DayHoverCssClass="day-h"
                              SelectedDayCssClass="day-h"
                              SelectedDayHoverCssClass="day-s-h"
                              OtherMonthDayCssClass="other"
                              OtherMonthDayHoverCssClass="other-h"
                              DayHeaderCssClass="day-hdr"
                              DayHeaderHoverCssClass="day-hdr-h"
                              SelectMonthCssClass="select-month"
                              SelectWeekCssClass="select-week"
                              MonthCssClass="con" 
                              MinDate="1900-01-01"
                              MaxDate="2500-01-01"
                              AutoPostBackOnSelectionChanged="true"
                              AutoPostBackOnVisibleDateChanged="true"
                              onselectionchanged="Calendar1_SelectionChanged">
                            </ComponentArt:Calendar>
                        </td>
                        <td style="width: 1%;background: url(../images/calendar/static_04.jpg) repeat-y; background-position:right"></td>
                    </tr>
                    <tr class="hdf">
                        <td class="hdf-l"></td>
                        <td class="hdf-m"></td>
                        <td class="hdf-r"></td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <span>
                            <br />
                            <b>说明：</b><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.【默认假日】：点击此按钮，将当前年份的日历数据中的节假日按照阳历的周六周日为节假日进行设置，并显示在页面上。<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.【保存】：点击本按钮，将设置后的数据保存。<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.页面显示红色为节假日，黑色为非节假日。<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.原来为红色的数字，点击一次变为黑色，再次点击此数字一次变为红色。<br />
                            </span>                            
                        </td>
                    </tr>
                </table>
                </center>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbDefaultholiday" runat="server" ImageUrl="../images/calendar/kq_50.jpg" 
                    OnClientClick="return confirm('您确定要还原为默认假日？');" onclick="btnDefaultHoliday_Click"/>&nbsp;&nbsp;
                <asp:ImageButton ID="ImageButton1" ImageUrl="../images/kaoqin_save.jpg" 
                    Width="64" Height="29" 
                    OnClientClick="return confirm('您确定要保存？');" runat="server" 
                    onclick="ImageButton1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
