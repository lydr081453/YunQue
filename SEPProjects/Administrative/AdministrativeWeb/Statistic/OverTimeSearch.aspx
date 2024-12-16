<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverTimeSearch.aspx.cs" Inherits="AdministrativeWeb.Statistic.OverTimeSearch" 
    MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
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
                            时&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;间：
                        </td>
                        <td align="left">
                            <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="pckOverDateTime_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_from_button1" alt="" onclick="ButtonFrom1_OnClick(event)" onmouseup="ButtonFrom1_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpState" runat="server" Height="20" Width="95px">
                                <asp:ListItem Text="请选择..." Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="未提交" Value="1"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                                <asp:ListItem Text="审批通过" Value="3"></asp:ListItem>
                            </asp:DropDownList>
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
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="Grid1" 
                                GroupingPageSize="10" 
                                GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" 
                                GroupBySectionCssClass="grp" 
                                GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="OT统计信息列表" 
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
                                CallbackCachingEnabled="false"
                                TreeLineImagesFolderUrl="../images/gridview2/lines/" 
                                TreeLineImageWidth="11"
                                TreeLineImageHeight="11" 
                                Width="100%" 
                                Height="100%" 
                                runat="server">
                                <Levels>
                                    <ComponentArt:GridLevel 
                                        DataKeyField="UserID" 
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
                                        SelectedRowCssClass="SelectedRow" 
                                        ShowSelectorCells="false"
                                        SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" 
                                        SortDescendingImageUrl="desc.gif" 
                                        SortImageWidth="9"
                                        SortImageHeight="5" 
                                        GroupHeadingCssClass="grp-hd">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="姓名" DataField="EmployeeName" Align="Center" Width="50" />
                                            <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center" Width="55" />
                                            <ComponentArt:GridColumn HeadingText="职位" DataField="DepartmentPositionName" Align="Center" Width="50" />
                                            <ComponentArt:GridColumn HeadingText="部门" DataField="Department" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="创建时间" DataField="CreateTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" DataType="System.String" />
                                            <ComponentArt:GridColumn HeadingText="OT开始时间" DataField="BeginTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" DataType="System.String" />
                                            <ComponentArt:GridColumn HeadingText="OT结束时间" DataField="EndTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" DataType="System.String" />
                                            <ComponentArt:GridColumn HeadingText="OT原因" DataField="OverTimeCause" Align="Center" />
                                            <ComponentArt:GridColumn DataField="ApproveState" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="审批状态" DataCellServerTemplateId="ApproveStateTemplateId" Align="Center" DataType="System.String" />
                                            <ComponentArt:GridColumn HeadingText="审批人" DataCellServerTemplateId="ApproveNameTemplateId" DataField="ApproveName" Align="Center" />
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
                                </ClientTemplates>
                                <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="ApproveStateTemplateId">
                                        <Template>
                                            <%# GetApproveState(Container.DataItem["ApproveState"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="ApproveNameTemplateId">
                                        <Template>
                                            <%# GetApproveName(Container.DataItem["ApproveState"].ToString(), Container.DataItem["ApproveName"].ToString())%>
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
    <ComponentArt:Calendar 
        runat="server" 
        ID="CalendarFrom1" 
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