<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovedInfoList.aspx.cs"
    MasterPageFile="~/Default.Master" Inherits="AdministrativeWeb.Attendance.ApprovedInfoList" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function GetAfterInfo(createtime, begintime) {
            if (createtime > begintime) {
                return "<img src=\"../images/gridview2/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\" title=\"事后申请\">";
            }
        }
    </script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="../images/t2_03.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>搜索 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <ComponentArt:Grid id="Grid1"
                                AllowColumnResizing="true"
                                CallbackCachingEnabled="false"
                                CssClass="Grid"
                                DataAreaCssClass="GridData"
                                EnableViewState="true"
                                FooterCssClass="GridFooter"
                                FooterHeight="40"
                                GroupingMode="ConstantRecords"
                                GroupingNotificationTextCssClass="GridHeaderText"
                                GroupByCssClass="GroupByCell"
                                GroupByTextCssClass="GroupByText"
                                GroupBySortImageWidth="10"
                                GroupBySortImageHeight="10"
                                GroupingNotificationText="已审批列表"
                                GroupBySortAscendingImageUrl="group_asc.gif"
                                GroupBySortDescendingImageUrl="group_desc.gif"
                                HeaderHeight="30"
                                Height="100%" 
                                HeaderCssClass="GridHeader"
                                IndentCellWidth="22"
                                ImagesBaseUrl="../images/gridview2/"
                                PreExpandOnGroup="true"
                                PagerStyle="Slider"
                                PagerTextCssClass="GridFooterText"
                                PagerButtonWidth="41"
                                PagerButtonHeight="22"
                                PageSize="20"    
                                PagerImagesFolderUrl="../images/gridview2/pager/"                            
                                RunningMode="Client"
                                runat="server"
                                ShowHeader="true"
                                ShowSearchBox="true"
                                SearchTextCssClass="GridHeaderText"
                                SliderHeight="20"
                                SliderWidth="150"
                                SliderGripWidth="9"
                                SliderPopupOffsetX="35"
                                TreeLineImageWidth="22"
                                TreeLineImageHeight="19"
                                TreeLineImagesFolderUrl="../images/gridview2/lines/"
                                Width="100%">
                                <Levels>
                                  <ComponentArt:GridLevel
                                    RowCssClass="Row"
                                    AlternatingRowCssClass="AlternatingRow"
                                    DataCellCssClass="DataCell"
                                    TableHeadingCssClass="GridHeader"
                                    HeadingRowCssClass="HeadingRow"
                                    HeadingCellCssClass="HeadingCell"
                                    HeadingTextCssClass="HeadingCellText"
                                    ShowSelectorCells="false"
                                    SelectedRowCssClass="SelectedRow"
                                    GroupHeadingCssClass="GroupHeading"
                                    SortAscendingImageUrl="asc.gif"
                                    SortDescendingImageUrl="desc.gif"
                                    SortImageWidth="9"
                                    SortImageHeight="5">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                        <ComponentArt:GridColumn DataField="matId" Visible="false" />
                                        <ComponentArt:GridColumn HeadingText="姓名" DataField="UserName" Align="Center" Width="50" />
                                        <ComponentArt:GridColumn HeadingText="申请时间" DataField="createtime" Align="Center" FormatString="yyyy-MM-dd" DataType="System.String" Width="80" />
                                        <ComponentArt:GridColumn HeadingText="事由开始时间" DataField="begintime" FormatString="yyyy-MM-dd HH:mm" Align="Center" DataType="System.String"/>
                                        <ComponentArt:GridColumn HeadingText="事由结束时间" DataField="endtime" FormatString="yyyy-MM-dd HH:mm" Align="Center" DataType="System.String"/>
                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="projectno" Align="Center" DataType="System.String"/>
                                        <ComponentArt:GridColumn HeadingText="事由内容" DataField="mattercontent" Align="Center" />
                                        <ComponentArt:GridColumn HeadingText="事由状态" DataField="MatterStateName" Align="Center" TextWrap="true" DataType="System.String"/>
                                        <ComponentArt:GridColumn HeadingText="事由类型" DataField="ApproveTypeName" Align="Center" DataType="System.String" Width="50" />
                                        <ComponentArt:GridColumn HeadingText="事后申请" Align="Center" DataCellClientTemplateId="FlagIconTemplate" Width="50" FixedWidth="True" />
                                    </Columns>
                                  </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="FlagIconTemplate">
                                        <span>## GetAfterInfo(DataItem.GetMember('createtime').Value, DataItem.GetMember('begintime').Value) ##</span>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
