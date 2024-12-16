<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="TSUserList.aspx.cs" Inherits="AdministrativeWeb.Attendance.TSUserList" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <script src="../js/jquery-1.4.min.js" type="text/javascript"></script>
<br />
<script language="javascript">
    $(document).ready(function() {
        $("#chkAll").click(function() {
            var chk = $(this).attr("checked");
            $("[name=chkItem]").each(function() {
                $(this).attr("checked",chk);
            });
        });
    });
</script>
<table width="100%">
    <tr>
        <td>时间段：</td>
        <td> <ComponentArt:Calendar id="PickerFrom" SelectedDate="2012-3-5"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker"
                                PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>&nbsp;
                            <img id="calendar_from_button" alt="" onclick="ButtonFrom_OnClick(event);" onmouseup="ButtonFrom_OnMouseUp(event);" class="calendar_button" src="../images/calendar/btn_calendar.gif" />&nbsp;
                            <ComponentArt:Calendar id="PickerTo" SelectedDate="2012-3-11"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker"
                                PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>&nbsp;
                            <img id="calendar_to_button" alt="" onclick="ButtonTo_OnClick(event);" 
                            onmouseup="ButtonTo_OnMouseUp(event);" class="calendar_button" 
                            src="../images/calendar/btn_calendar.gif" /></td>
                            <td>
                                关键字：
                            </td>
                            <td><asp:TextBox ID="txtKey" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnSearch" runat="server" class="button-orange" Text="检索" OnClick="btnSearch_Click" /></td>
    </tr>
</table>
<br />
<asp:GridView ID="gvList" Width="100%" runat="server" AutoGenerateColumns="false" 
   OnRowDataBound="gvList_RowDataBound"
 CssClass="topborder-org" HeaderStyle-Height="30px" AllowPaging="true" PageSize="20" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvList_PageIndexChanging">
    <Columns>
        <asp:TemplateField HeaderText="选择">
            <HeaderTemplate>
                <input type="checkbox" id="chkAll" /> 全选
            </HeaderTemplate>
            <ItemTemplate>
                <input type="checkbox" id="chkItem" name="chkItem" value="<%# Eval("userid") %>" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="员工编号" DataField="UserCode" />
        <asp:TemplateField HeaderText="员工姓名">
            <ItemTemplate>
                <%# Eval("LastNameCN").ToString() + Eval("FirstNameCN").ToString() + "[" + Eval("username") +"]" %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="所属部门" DataField="dpName" />
        <asp:BoundField HeaderText="电话" DataField="Phone1" />
        <asp:BoundField HeaderText="邮箱" DataField="internalEmail" />
        <asp:TemplateField HeaderText="详细">
            <ItemTemplate>
                <asp:HyperLink ID="hylMLink" runat="server" Text="月历" />&nbsp;
                <asp:HyperLink ID="hlyLink" runat="server" Text="周历" /><asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("UserId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<asp:Button ID="btnSendMail" runat="server" Text="邮件提醒" OnClick="btnSendMail_Click" class="button-orange" />

      <ComponentArt:Calendar runat="server"
      id="CalendarFrom"
      AllowMultipleSelection="false"
      AllowWeekSelection="false"
      AllowMonthSelection="false"
      ControlType="Calendar"
      PopUp="Custom"
      PopUpExpandControlId="calendar_from_button"
      CalendarTitleCssClass="title"
      DayHeaderCssClass="dayheader"
      DayCssClass="day"
      DayHoverCssClass="dayhover"
      OtherMonthDayCssClass="othermonthday"
      SelectedDayCssClass="selectedday"
      CalendarCssClass="calendar"
      NextPrevCssClass="nextprev"
      MonthCssClass="month"
      SwapSlide="Linear"
      SwapDuration="300"
      DayNameFormat="Short"
      ImagesBaseUrl="../images/calendar"
      PrevImageUrl="cal_prevMonth.gif"
      NextImageUrl="cal_nextMonth.gif">
      <ClientEvents>
        <SelectionChanged EventHandler="CalendarFrom_OnChange" />
      </ClientEvents>
      </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server"
      id="CalendarTo"
      AllowMultipleSelection="false"
      AllowWeekSelection="false"
      AllowMonthSelection="false"
      ControlType="Calendar"
      PopUp="Custom"
      PopUpExpandControlId="calendar_to_button"
      CalendarTitleCssClass="title"
      DayHeaderCssClass="dayheader"
      DayCssClass="day"
      DayHoverCssClass="dayhover"
      OtherMonthDayCssClass="othermonthday"
      SelectedDayCssClass="selectedday"
      CalendarCssClass="calendar"
      NextPrevCssClass="nextprev"
      MonthCssClass="month"
      SwapSlide="Linear"
      SwapDuration="300"
      DayNameFormat="Short"
      ImagesBaseUrl="../images/calendar"
      PrevImageUrl="cal_prevMonth.gif"
      NextImageUrl="cal_nextMonth.gif">
      <ClientEvents>
        <SelectionChanged EventHandler="CalendarTo_OnChange" />
      </ClientEvents>
    </ComponentArt:Calendar>
</asp:Content>
