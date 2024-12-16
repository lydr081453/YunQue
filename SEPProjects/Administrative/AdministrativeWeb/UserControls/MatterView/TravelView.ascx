﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TravelView.ascx.cs" Inherits="AdministrativeWeb.UserControls.MatterView.TravelView" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript">
    function openTravelProject() {
        var win = window.open('TravelSelectProjectList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setTravelProjectInfo(projectId, projectCode) {
        document.getElementById("<%=hidTravelProjectId.ClientID %>").value = projectId;
        document.getElementById("<%=txtTravelProjectNo.ClientID %>").value = projectCode;
    }
    
    // 提交校验
    function SubmitCheckTravel() {
        var projectID = document.getElementById("<%=hidTravelProjectId.ClientID %>");
        if (projectID.value == null || projectID.value == "") {
            alert("请选择出差项目号。");
            return false;
        }
        var desc = document.getElementById("<%=txtTravelDes.ClientID %>");
        if (desc.value == null || desc.value == "") {
            alert("请填写出差内容描述。");
            return false;
        } 
        return true;
    }
</script>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<script src="../../js/DatePicker.js" type="text/javascript"></script>

<asp:HiddenField ID="singlTravelId" runat="server" />
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
                        <strong>出差申请单 </strong>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td width="10%">
                                    姓名：
                                </td>
                                <td>
                                    &nbsp;<asp:HiddenField ID="hidTravelUserid" runat="server" />
                                    <asp:TextBox ID="txtTravelUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtTravelUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtTravelTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtTravelGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    &nbsp;&nbsp;<asp:Label ID="labTravelAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    OT项目号：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    &nbsp;<asp:HiddenField ID="hidTravelProjectId" runat="server" />
                                    <asp:TextBox ID="txtTravelProjectNo" runat="server" Width="400px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" onkeyDown="return false;"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                    <input type="button" value="选择" onclick="openTravelProject();return false;"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    内容描述：
                                </td>
                                <td>
                                    &nbsp;&nbsp;<asp:TextBox TextMode="MultiLine" ID="txtTravelDes" runat="server" Width="500px" Height="40px"
                                        BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    出差时长：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="TravelPickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>至</td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="TravelPickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
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
                        </table>
                        <table width="100%" border="0" cellspacing="5" cellpadding="0">
                            <tr>
                                <td width="60">
                                    <asp:LinkButton ID="btnTravelBack" Text="<img src='../../images/t2_03-22.jpg' />" Width="56"
                                        Height="24" runat="server" OnClick="btnTravelBack_Click" CausesValidation="false" />
                                </td>
                                <td>
                                    &nbsp;
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
