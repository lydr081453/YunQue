<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OverTimeView.ascx.cs"
    Inherits="AdministrativeWeb.UserControls.MatterView.OverTimeView" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript">
    function openOverTimeProject() {
        var win = window.open('OverTimeSelectProjectList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setOverTimeProjectInfo(projectId, projectCode) {
        document.getElementById("<%=hidOverTimeProjectId.ClientID %>").value = projectId;
        document.getElementById("<%=txtOverTimeProjectNo.ClientID %>").value = projectCode;
    }
    
    // 提交校验
    function SubmitCheckOverTime() {
        var projectNo = document.getElementById("<%=txtOverTimeProjectNo.ClientID %>");
        if (projectNo.value == null || projectNo.value == "") {
            alert("请选择项目号信息。");
            return false;
        }
        var desc = document.getElementById("<%=txtDes.ClientID %>");
        if (desc.value == null || desc.value == "") {
            alert("请填写OT内容。");
            return false;
        } 
        return true;
    }
</script>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<script src="../../js/DatePicker.js" type="text/javascript"></script>

<asp:HiddenField ID="singlOverTimeId" runat="server" />
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
                                    <asp:HiddenField ID="hidUserid" runat="server" />
                                    <asp:TextBox ID="txtUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
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
                                    OT类别：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="工作日" Value="1" Enabled="false"></asp:ListItem>
                                        <asp:ListItem Text="节假日" Value="2" Enabled="false"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    OT项目号：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:HiddenField ID="hidOverTimeProjectId" runat="server" />
                                    <asp:TextBox ID="txtOverTimeProjectNo" runat="server" Width="400px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" onkeyDown="return false;"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                    <input type="button" value="选择" onclick="openOverTimeProject();return false;"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工作描述：
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="txtDes" runat="server" Width="500px" Height="40px"
                                        BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    OT时长：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>至</td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="PickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 10px 0 5px 5px; color: #B50000;">
                                    注：周末及节假日OT以半日起算，调休也以半日为调休原则。
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="5" cellpadding="0">
                            <tr>
                                <td width="60">
                                    <asp:LinkButton ID="btnBack" Text="<img src='../../images/t2_03-22.jpg' />" Width="56"
                                        Height="24" runat="server" OnClick="btnBack_Click" CausesValidation="false" />
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
