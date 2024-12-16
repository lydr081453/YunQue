<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleOvertimeDetails.aspx.cs" Inherits="AdministrativeWeb.Attendance.SingleOvertimeDetails" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head id="Head1" runat="server">
    <title></title>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<script src="../../js/DatePicker.js" type="text/javascript"></script> 

</head>
<body>
    <form id="form1" runat="server">
<asp:HiddenField ID="singlOverTimeId" runat="server" />
<asp:HiddenField ID="hidLeaveApproveId" runat="server" />

<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="17">
                        <strong>
                            <img src="../../images/090407_12-35.jpg" width="17" height="20" hspace="5" vspace="5" /></strong>
                    </td>
                    <td align="left">
                        <strong>OT申请单 </strong>
                        <span style="color: #B50000; font-size: 9pt;">
                        <b>
                            <asp:Label runat="server" ID="labAfterApprove"></asp:Label>
                        </b>
                        </span>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td width="80px" bgcolor="#F2F2F2">
                                    姓名：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:HiddenField ID="hidUserid" runat="server" />
                                    <asp:Label ID="txtUserName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="80px">
                                    员工编号：
                                </td>
                                <td>
                                    <asp:Label ID="txtUserCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="80px" bgcolor="#F2F2F2">
                                    业务团队：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:Label ID="txtTeam" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="80px">
                                    组别：
                                </td>
                                <td>
                                    <asp:Label ID="txtGroup" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:Label ID="labAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OT项目号：
                                </td>
                                <td>
                                    <asp:HiddenField ID="hidOverTimeProjectId" runat="server" />
                                    <asp:Label ID="txtOverTimeProjectNo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    工作描述：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:Label  ID="txtDes" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OT时长：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>至</td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="PickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="grdMatterDetails" Width="100%" runat="server" AutoGenerateColumns="false" onrowdatabound="grdMatterDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="详细时间" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTimeStart" runat="server"></asp:Label>&nbsp;至&nbsp;
                                                    <asp:Label ID="lblTimeEnd" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="内容" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                <asp:Label ID="txtMatterDetails" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 10px 0 5px 5px; color: #B50000;">
                                    注：周末及节假日OT以半日起算，调休也以半日为调休原则。
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<ComponentArt:Calendar runat="server" ID="CalendarFrom" AllowMultipleSelection="false"
    AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
    PopUp="Custom" PopUpExpandControlId="calendar_from_button" CalendarTitleCssClass="title"
    DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
    SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
    MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
    ImagesBaseUrl="../../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
    <ClientEvents>
        <SelectionChanged EventHandler="CalendarFrom2_OnChange" />
    </ClientEvents>
</ComponentArt:Calendar>
</form>
</body>