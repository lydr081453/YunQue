<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LateInfoSearch.aspx.cs" Inherits="AdministrativeWeb.Statistic.LateInfoSearch" 
    MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <link href="../../css/comboboxStyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        // 删除考勤记录信息
        function DeleteInfo(control, idvalue, flag) {
            if (confirm("您确定要删除吗？")) {
                control.href = "AddedClockInList.aspx?clockinid=" + idvalue + "&flag=" + flag;
                return true;
            }
            return false;
        }
    </script>
    <script src="../js/DatePicker.js" type="text/javascript"></script>
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
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="right">
                            用 户 名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            员工编号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                            <%--<asp:ImageButton ImageUrl="../images/cardmanage.jpg" ID="btnAdd" runat="server" OnClick="btnAdd_Click"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;份：
                        </td>
                        <td>
                            <asp:DropDownList ID="drpYear" runat="server">
                            </asp:DropDownList>年
                            <asp:DropDownList ID="drpMonth" runat="server">
                            </asp:DropDownList>月
                        </td>
                        <td align="right">
                            职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;位：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPositions" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            部门：
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="upnDepartment" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbCompany" runat="Server" Width="100" Height="20" AutoHighlight="false"
                                                    AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryCode"
                                                    ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                                                    TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cbCom_SelectedIndexChanged">
                                                </ComponentArt:ComboBox>
                                            </td>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbDepartment1" runat="Server" Width="100" Height="20"
                                                    AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                    DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                    CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cbDepartment1_SelectedIndexChanged">
                                                </ComponentArt:ComboBox>
                                            </td>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbDepartment2" runat="Server" Width="100" Height="20"
                                                    AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                    DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                    CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="200" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con">
                                                </ComponentArt:ComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                                runat="server" onclick="btnSearch_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:ImageButton ID="btnExport" ImageUrl="../images/export.jpg" ToolTip="导出考勤统计记录信息"
                    Width="52" Height="29" hspace="10" OnClientClick="return confirm('您确定要导出考勤统计记录信息？');"
                    OnClick="btnExport_Click" runat="server" />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="Grid1" 
                                CallbackCachingEnabled="true" 
                                CallbackCacheSize="50" 
                                AllowColumnResizing="true" 
                                GroupingPageSize="10" 
                                GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" 
                                GroupBySectionCssClass="grp" 
                                GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="迟到统计信息列表" 
                                HeaderHeight="30" 
                                HeaderCssClass="GridHeader"
                                GroupingCountHeadingsAsRows="true" 
                                DataAreaCssClass="GridData" 
                                EnableViewState="true"
                                EditOnClickSelectedItem="false"
                                ShowHeader="true" 
                                FooterCssClass="GridFooter" 
                                PreExpandOnGroup="true" 
                                PagerStyle="Slider"
                                PagerTextCssClass="GridFooterText" 
                                PagerButtonWidth="41" 
                                PagerButtonHeight="22"
                                PageSize="20" 
                                PagerButtonHoverEnabled="false" 
                                SliderHeight="20" 
                                SliderWidth="150"
                                SliderGripWidth="9" 
                                SliderPopupOffsetX="35"
                                ShowSearchBox="true"
                                SearchText="关键字搜索："
                                SearchTextCssClass="SearchText"
                                ImagesBaseUrl="../images/gridview2/"
                                PagerImagesFolderUrl="../images/gridview2/pager/" 
                                TreeLineImagesFolderUrl="../images/gridview2/lines/" 
                                TreeLineImageWidth="11"
                                TreeLineImageHeight="11" 
                                Width="100%" 
                                Height="100%" 
                                runat="server"
                                LoadingPanelFadeDuration="1000"
                                LoadingPanelFadeMaximumOpacity="60"
                                LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
                                LoadingPanelPosition="MiddleCenter">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="UserID" 
                                        ShowTableHeading="false" 
                                        TableHeadingCssClass="GridHeader"
                                        RowCssClass="Row" 
                                        ColumnReorderIndicatorImageUrl="reorder.gif" 
                                        DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" 
                                        HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" 
                                        HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" 
                                        SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" 
                                        SortDescendingImageUrl="desc.gif" 
                                        SortImageWidth="9"
                                        SortImageHeight="5" >
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="姓名" DataField="EmployeeName" Align="Center" Width="50" />
                                            <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center" Width="55" />
                                            <ComponentArt:GridColumn HeadingText="职位" DataField="Position" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="部门" DataField="Department" Align="Center" DataType="System.String" />
                                            <ComponentArt:GridColumn DataField="AttendanceYear" Visible="false" />
                                            <ComponentArt:GridColumn DataField="AttendanceMonth" Visible="false" />                                            
                                            <ComponentArt:GridColumn HeadingText="月份"  DataCellServerTemplateId="AttendanceTimeTemplate" Align="Center" DataType="System.String" Width="55" />
                                            <ComponentArt:GridColumn HeadingText="迟到30分钟内" DataField="LateCount1" DataType="System.String" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="迟到30分钟以上" DataField="LateCount2" DataType="System.String" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="OT次数" DataField="OverTimeCount" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="半天未打卡" DataField="AbsentCount1" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="全天未打卡" DataField="AbsentCount2" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="上/下班打卡记录不全" DataField="AbsentCount3" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="早退" DataField="LeaveEarly" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="提醒" DataCellServerTemplateId="RemindTemplate" Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);">
                                                </td>
                                                <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);
                                                    background-repeat: repeat-x;">
                                                    <span style="color: White; font-weight: bold;">待审批事由列表</span>
                                                </td>
                                                <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);">
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);
                                                    background-repeat: repeat-x;">
                                                </td>
                                                <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);
                                                    background-repeat: repeat-x;">
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                                        <table height="340" width="692" bgcolor="#e0e0e0">
                                            <tr>
                                                <td valign="center" align="center">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td style="font-size: 10px; font-family: Verdana;">
                                                                Loading...&nbsp;
                                                            </td>
                                                            <td>
                                                                <img src="../images/spinner.gif" width="16" height="16" border="0">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="AttendanceTimeTemplate">
                                        <Template>
                                            <%# GetAttendanceTime(Container.DataItem["AttendanceYear"].ToString(), Container.DataItem["AttendanceMonth"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="RemindTemplate">
                                        <Template>
                                            <%# GetRemindUrl(Container.DataItem["UserID"].ToString(), Container.DataItem["AttendanceYear"].ToString(), Container.DataItem["AttendanceMonth"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="EditTemplate">
                                        <Template>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="DeleteTemplate">
                                        <Template>
                                            <%--<%# GetDeleteUrl(int.Parse(Container.DataItem["ID"].ToString())) %>--%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates>
                            </ComponentArt:Grid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <ComponentArt:Calendar runat="server"
      id="CalendarFrom1"
      AllowMultipleSelection="false"
      AllowWeekSelection="false"
      AllowMonthSelection="false"
      ControlType="Calendar"
      PopUp="Custom"
      PopUpExpandControlId="calendar_from_button1"
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
        <SelectionChanged EventHandler="cldOverDateTime_OnChange" />
      </ClientEvents>
    </ComponentArt:Calendar>
</asp:Content>