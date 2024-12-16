<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MattersList.aspx.cs" MasterPageFile="~/Default.Master"
    Inherits="AdministrativeWeb.Attendance.MattersList" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function getTitle(groupItem, column) {
            var rows = groupItem.Rows;
            var sum = "";
            var row;
            for (i = 0; i < 1; i++) {
                row = groupItem.Grid.Table.GetRow(rows[i]);
                var type = row.GetMember(column).Value;
                sum = type;
            }
            return sum;
        }
        
        function FormatDateTime(dt) {
            function toFixedLengthString(val, len) {
                var n = Math.pow(10, len);
                var s = new String();
                s += (val + n);
                s = s.substr(s.length - len, len);
                return s;
            }
            var ret = new String()
            ret += dt.getFullYear();
            ret += '-';
            ret += toFixedLengthString(dt.getMonth() + 1, 2);
            ret += '-';
            ret += toFixedLengthString(dt.getDate(), 2);
            return ret;
        }
        
        function CheckAllItems(grid, columnNumber) {
            var gridItem;
            var itemIndex = 0;
            while (gridItem = grid.Table.GetRow(itemIndex)) {
                gridItem.SetValue(columnNumber, true);
                itemIndex++;
            }
            grid.Render();
        }
        
        // 删除考勤记录信息
        function DeleteInfo(control, idvalue, formType) {
            if (confirm("您确定要删除吗？")) {
                control.href = "MattersList.aspx?matterid=" + idvalue + "&flag=1&formType=" + formType;
                return true;
            }
            return false;
        }
        
        // 撤销考勤记录信息
        function CalInfo(control, idvalue, formType) {
            if (confirm("您确定要撤销吗？")) {
                control.href = "MattersList.aspx?matterid=" + idvalue + "&flag=2&formType=" + formType;
                return true;
            }
            return false;
        }

        function GetAfterInfo(createtime, begintime) {
            if (createtime > begintime) {
                return "<img src=\"../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\" title=\"事后申请\">";
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
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>        
                        <td align="right">
                            申请时间：
                        </td>
                        <td>
                            <ComponentArt:Calendar id="PickerFrom"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker"
                                PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>&nbsp;
                            <img id="calendar_from_button" alt="" onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)" class="calendar_button" src="../images/calendar/btn_calendar.gif" />&nbsp;
                            <ComponentArt:Calendar id="PickerTo"
                                runat="server"
                                PickerFormat="Custom"
                                PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker"
                                PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>&nbsp;
                            <img id="calendar_to_button" alt="" onclick="ButtonTo_OnClick(event)" 
                            onmouseup="ButtonTo_OnMouseUp(event)" class="calendar_button" 
                            src="../images/calendar/btn_calendar.gif" />
                        </td>
                        <td align="right">
                            类型：
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="drpFormType">
                                <asp:ListItem Value="-1" Text="...请选择" />
                                <asp:ListItem Value="1" Text="请假" />
                                <asp:ListItem Value="2" Text="OT" />
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td align="right">
                           关键字：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtKey"></asp:TextBox>
                        </td>
                        <td align="right">
                           &nbsp;
                        </td>
                        <td align="left">
                           <br />
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" 
                                ID="btnSearch" runat="server" onclick="btnSearch_Click" />
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
			            GroupingNotificationText="事由信息列表"
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
                        Width="100%" Height="100%"
                        runat="server">
                        <Levels>
                            <ComponentArt:GridLevel 
                                DataKeyField="ID" 
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
                                GroupHeadingClientTemplateId="GroupByTemplate"
                                GroupHeadingCssClass="grp-hd">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ID" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="姓名ID" DataField="UserID" Align="Center" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="表单类型ID" DataField="FormType" Align="Center" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="请假类型ID" DataField="MatterType" Align="Center" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="OT类型ID" DataField="OvertimeType" Align="Center" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="事由状态" DataField="MatterState" Align="Center" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="OT状态" DataField="ApproveState" Align="Center" Visible="false"/>
                                    
                                    <ComponentArt:GridColumn HeadingText="姓名" DataCellServerTemplateId="UserNameTemplate" Align="Center" Width="50"/>
                                    <ComponentArt:GridColumn HeadingText="申请时间" DataField="CreateTime" FormatString="yyyy-MM-dd" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="开始时间" DataField="BeginTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="结束时间" DataField="EndTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="类型"  Align="Center" DataCellServerTemplateId="MatterTypeNameTempate"/>
                                    <ComponentArt:GridColumn HeadingText="内容" DataField="MatterContent" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="状态" Align="Center" DataCellServerTemplateId="StateNameTempate"/>
                                    
                                    <ComponentArt:GridColumn HeadingText="编辑" DataCellServerTemplateId="EditTemplate" Width="50" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="删除" DataCellServerTemplateId="DeleteTemplate" Width="50" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="撤销" DataCellServerTemplateId="CancelTemplate" Width="50" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="事后申请" Align="Center" DataCellClientTemplateId="FlagIconTemplate" Width="50" FixedWidth="True" />
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
                            
                            <ComponentArt:ClientTemplate ID="ShowTemplate">
                                <a style="color: #595959;" href="SingleOvertimeView.aspx?backurl=/Attendance/LeaveList.aspx&leaveid=## DataItem.GetMember('matId').Value ##">
                                    ## DataItem.GetMember('UserID').Value ##</a>
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="PagerInfoTemplate">
                                <input type="checkbox" id="chkAll" name="chkAll" onclick="## CheckAllItems(DataItem, '1') ##" />
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="GroupByTemplate">
                                <span>## getTitle(DataItem, "MatterType") ##</span>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="FlagIconTemplate">
                                <span>## GetAfterInfo(DataItem.GetMember('CreateTime').Value, DataItem.GetMember('BeginTime').Value) ##</span>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        
                        <ServerTemplates>
                            <ComponentArt:GridServerTemplate ID="UserNameTemplate">
                                <Template>
                                    <%#GetUserName(Container.DataItem["UserID"].ToString(), Container.DataItem["ID"].ToString(), Container.DataItem["FormType"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            
                            <ComponentArt:GridServerTemplate ID="MatterTypeNameTempate">
                                <Template>
                                    <%#GetTypeName(Container.DataItem["FormType"].ToString(), int.Parse(Container.DataItem["MatterType"].ToString()), Container.DataItem["OvertimeType"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            
                            <ComponentArt:GridServerTemplate ID="StateNameTempate">
                                <Template>
                                    <%#GetStateName(Container.DataItem["FormType"].ToString(), int.Parse(Container.DataItem["MatterState"].ToString()), int.Parse(Container.DataItem["ApproveState"].ToString()))%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            
                            <ComponentArt:GridServerTemplate ID="EditTemplate">
                                <Template>
                                    <%#GetEditUrl(Container.DataItem["FormType"].ToString(), int.Parse(Container.DataItem["ID"].ToString()), int.Parse(Container.DataItem["MatterType"].ToString()), int.Parse(Container.DataItem["MatterState"].ToString()), int.Parse(Container.DataItem["ApproveState"].ToString()))%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            
                            <ComponentArt:GridServerTemplate ID="DeleteTemplate">
                                <Template>
                                    <%#GetDeleteUrl(Container.DataItem["FormType"].ToString(), int.Parse(Container.DataItem["ID"].ToString()),  int.Parse(Container.DataItem["MatterState"].ToString()), int.Parse(Container.DataItem["ApproveState"].ToString()))%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            
                            <ComponentArt:GridServerTemplate ID="CancelTemplate">
                                <Template>
                                    <%#GetCancelUrl(Container.DataItem["FormType"].ToString(), int.Parse(Container.DataItem["ID"].ToString()), int.Parse(Container.DataItem["MatterState"].ToString()), int.Parse(Container.DataItem["ApproveState"].ToString()))%>
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