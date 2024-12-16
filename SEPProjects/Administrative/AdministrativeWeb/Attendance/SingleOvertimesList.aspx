<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleOvertimesList.aspx.cs" Inherits="AdministrativeWeb.Attendance.SingleOvertimesList" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <link href="../css/comboboxStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // 删除考勤记录信息
        function DeleteInfo(control, idvalue, flag) {
            if (confirm("您确定要删除吗？")) {
                control.href = "AddedClockInList.aspx?clockinid=" + idvalue + "&flag=" + flag;
                return true;
            }
            return false;
        }
        function GetViewInfo(userid) {
            return "<a href='IntegratedQueryView.aspx?ApplicantID=" + userid + "'><img src=\"../images/dc.gif\" title=\"查看考勤详细信息\"></a>";
        }

        // 全选所有的等待审批单
        function CheckAllItems() {
            var selectAll = document.getElementById("selectAll");
            var gridItem;
            var itemIndex = 0;
            if (selectAll.checked) {
                while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                    gridItem.SetValue('1', true);
                    itemIndex++;
                }
                Grid1.Render();
            }
            else {
                while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                    gridItem.SetValue('1', false);
                    itemIndex++;
                }
                Grid1.Render();
            }
        }

        // 审批通过所选择的考勤事由
        function SendMail() {
            var gridItem;
            var itemIndex = 0;
            var flag = 0;
            while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                if (gridItem.Data[1]) {
                    flag = 1;
                }
                itemIndex++;
            }

            if (flag == 0) {
                alert("请选择要邮件提醒的对象！");
                return false;
            }
            else {
                return confirm("您确认要发送邮件提醒吗？");
            }
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
                            姓名：
                        </td>
                        <td align="left">
                            &nbsp;<asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="202" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            员工编号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="right">
                            状态：
                        </td>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="drpState" runat="Server" Width="205" Height="20" AutoHighlight="false"
                                            AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryCode"
                                            ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                                            TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                            DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="198"
                                            DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con" SelectedIndex="0">
                                            <Items>
                                                <ComponentArt:ComboBoxItem Text="请选择..." Value="0" />
                                                <ComponentArt:ComboBoxItem Text="未提交" Value="1" />
                                                <ComponentArt:ComboBoxItem Text="等待TeamLeader审批" Value="2" />
                                                <ComponentArt:ComboBoxItem Text="等待团队HRAdmin审批" Value="3" />
                                                <ComponentArt:ComboBoxItem Text="等待团队总经理审批" Value="4" />
                                                <ComponentArt:ComboBoxItem Text="等待考勤管理员确认" Value="5" />
                                                <ComponentArt:ComboBoxItem Text="审批通过" Value="6" />
                                                <ComponentArt:ComboBoxItem Text="审批驳回" Value="7" />
                                            </Items>
                                        </ComponentArt:ComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                        <td align="right">
                            时间：
                        </td>
                        <td align="left">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                            ControlType="Picker" PickerCssClass="picker" Enabled="true">
                                        </ComponentArt:Calendar>  至  <ComponentArt:Calendar ID="PickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                            ControlType="Picker" PickerCssClass="picker" Enabled="true">
                                        </ComponentArt:Calendar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            OT单类型：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOvertimeType" runat="server">
                                <asp:ListItem Text=" 全 部 申 请 " Value="0"></asp:ListItem>
                                <asp:ListItem Text=" 事 前 申 请 " Value="-1"></asp:ListItem>
                                <asp:ListItem Text=" 事 后 申 请 " Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <%--<tr>
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
                            职位：
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
                            在职状态：
                        </td>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <ComponentArt:ComboBox ID="cbStatus" runat="Server" Width="204" Height="20" AutoHighlight="false"
                                            AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryCode"
                                            ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                                            TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                            DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                            DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con" SelectedIndex="0">
                                            <Items>
                                                <ComponentArt:ComboBoxItem Text="在职" Value="1" />
                                                <ComponentArt:ComboBoxItem Text="离职" Value="2" />
                                            </Items>
                                        </ComponentArt:ComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>--%>
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
                                AllowColumnResizing="true" 
                                GroupingPageSize="10" 
                                GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" 
                                GroupBySectionCssClass="grp" 
                                GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="OT情况列表" 
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
                                ImagesBaseUrl="../images/gridview2/"
                                PagerImagesFolderUrl="../images/gridview2/pager/" 
                                CallbackCachingEnabled="false"
                                TreeLineImagesFolderUrl="../images/gridview2/lines/" 
                                TreeLineImageWidth="11"
                                TreeLineImageHeight="11" 
                                Width="100%" 
                                Height="100%" 
                                runat="server"
                                ShowSearchBox="false"
                                SearchText="关键字搜索"
                                SearchTextCssClass="SearchText" onitemdatabound="Grid1_ItemDataBound">
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
                                        SelectedRowCssClass="SelectedRow" 
                                        SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" 
                                        SortDescendingImageUrl="desc.gif" 
                                        SortImageWidth="9"
                                        SortImageHeight="5" 
                                        GroupHeadingCssClass="grp-hd">
                                        <Columns>
                                        <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="姓名" DataField="EmployeeName" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="OT起始时间" DataField="BeginTime" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="OT结束时间" DataField="EndTime" Align="Center" />
                                              <ComponentArt:GridColumn HeadingText="部门" DataCellServerTemplateId="ShowUserDepartment" Align="Center" />
                                              <%--<ComponentArt:GridColumn HeadingText="OT日期" DataCellServerTemplateId="ShowUserOvertimes" Align="Center" />--%>
                                            <ComponentArt:GridColumn HeadingText="详细" Width="50" DataCellServerTemplateId="ViewTemplate" Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="ShowUserDepartment">
                                        <Template>
                                            <%# GetUserDepartment(Container.DataItem["UserID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="ViewTemplate">
                                        <Template>
                                          <a target="_blank" href="singleovertimedetails.aspx?overtimeid=<%# Container.DataItem["ID"]%> ">
                                            <img src="../images/Audit.gif" /></a>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                              
                                </ServerTemplates>
                            </ComponentArt:Grid>
                            <input type="checkbox" id="selectAll" onclick="CheckAllItems();"/>全选
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
        <SelectionChanged EventHandler="CalendarFrom1_OnChange" />
      </ClientEvents>
      </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server"
      id="CalendarTo1"
      AllowMultipleSelection="false"
      AllowWeekSelection="false"
      AllowMonthSelection="false"
      ControlType="Calendar"
      PopUp="Custom"
      PopUpExpandControlId="calendar_to_button1"
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
        <SelectionChanged EventHandler="CalendarTo1_OnChange" />
      </ClientEvents>
    </ComponentArt:Calendar>
</asp:Content>
