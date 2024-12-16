<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditList.aspx.cs" MasterPageFile="~/Default.Master"
    Inherits="AdministrativeWeb.Attendance.AuditList" %>
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
                var type = row.GetMember("Approvetype").Value;
                
                if (type == 2)
                    sum += "请假单";
                else if (type == 3)
                    sum += "OT单";
                else if (type == 4)
                    sum += "出差单";
                else if (type == 5)
                    sum += "调休单";
                else if (type == 6)
                    sum += "其他事由单";
                else if (type == 7)
                    sum += "外出单";
                else if (type == 8)
                    sum += "晚到申请";
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

            var ret = new String();
            ret += dt.getFullYear();
            ret += '-';
            ret += toFixedLengthString(dt.getMonth() + 1, 2);
            ret += '-';
            ret += toFixedLengthString(dt.getDate(), 2);
            return ret;
        }
        function GetAfterInfo(createtime, begintime) {
            if (createtime > begintime) {
                return "<img src=\"../images/gridview/flag_red.gif\" width=\"12\" height=\"12\" border=\"0\" title=\"事后申请\">";
            }
        }

        // 全选所有的等待审批单
        function CheckAllItems() {
            var selectAll = document.getElementById("selectAll");
            var gridItem;
            var itemIndex = 0;
            if (selectAll.checked) {
                while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                    gridItem.SetValue('2', true);
                    itemIndex++;
                }
                Grid1.Render();
            }
            else {
                while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                    gridItem.SetValue('2', false);
                    itemIndex++;
                }
                Grid1.Render();
            }
        }

        // 审批通过所选择的考勤事由
        function AuditMatter() {
            var gridItem;
            var itemIndex = 0;
            var flag = 0;
            while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                if (gridItem.Data[2]) {
                    flag = 1;
                }
                itemIndex++;
            }
            
            if (flag == 0) {
                alert("请选择要审批的事由！");
                return false;
            }
            else {
                return confirm("您确认要审批通过吗？");
            }
        }
        
        // 审批驳回所选择的考勤事由
        function OverruleMatter() {
            var gridItem;
            var itemIndex = 0;
            var flag = 0;
            while (gridItem = Grid1.Table.GetRow(itemIndex)) {
                if (gridItem.Data[2]) {
                    flag = 1;
                }
                itemIndex++;
            }

            if (flag == 0) {
                alert("请选择要驳回的事由！");
                return false;
            }
            else {
                return confirm("您确认要审批驳回吗？");
            }
        }
    </script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
          <td>
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17"><img src="../images/t2_03.jpg" width="21" height="20" /></td>
                        <td align="left"><strong>搜索 </strong>&nbsp;&nbsp;&nbsp;&nbsp;
                            <font color="red" style="font-size: 12px; font-weight: bold;">事由审批注意事项：外出单的审批在“审批中心”的“事由审批”中完成，去除了原有的“外出/其他审批”菜单。
                            </font>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="right">
                            申请人：
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtApp" Width="128px"></asp:TextBox>
                        </td>
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
                            &nbsp;
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
                            <br />
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" 
                                ID="btnSearch" runat="server" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
              <tr>
                <td class="td">
                    <ComponentArt:Grid ID="Grid1" 
                        AllowColumnResizing="true"
                        CallbackCachingEnabled="false"
                        CssClass="Grid"
                        DataAreaCssClass="GridData"
                        EnableViewState="true" 
                        EditOnClickSelectedItem="false"
                        FooterCssClass="GridFooter" 
                        FooterHeight="40"
                        GroupingPageSize="10"
                        GroupingMode="ConstantRecords"
                        GroupingNotificationText="待审批事由列表"
                        GroupingNotificationTextCssClass="GridHeaderText"
                        GroupByCssClass="GroupByCell"
                        GroupByTextCssClass="GroupByText"
                        HeaderHeight="30"                      
                        Height="100%"
                        HeaderCssClass="GridHeader"
                        IndentCellWidth="22"
                        ImagesBaseUrl="../images/gridview2/"
                        PreExpandOnGroup="true"
                        PageSize="20"
                        PagerStyle="Slider" 
                        PagerTextCssClass="GridFooterText"
                        PagerButtonWidth="41"
                        PagerButtonHeight="22"
                        PagerImagesFolderUrl="../images/gridview2/pager/" 
                        RunningMode="Client" 
                        runat="server"
                        ShowHeader="true"
                        ShowSearchBox="false"
                        SearchTextCssClass="GridHeaderText"
                        SliderHeight="20"
                        SliderWidth="150"
                        SliderGripWidth="9"
                        SliderPopupOffsetX="35"
                        TreeLineImagesFolderUrl="../images/gridview2/lines/"
		                TreeLineImageWidth="11"
		                TreeLineImageHeight="11"
                        Width="100%">
                        <Levels>
                            <ComponentArt:GridLevel 
                                DataKeyField="ID" 
                                ShowTableHeading="false"
                                ShowSelectorCells="false"
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
                                    <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="matId" Visible="false" />
                                    <ComponentArt:GridColumn HeadingText="选择" DataField="Deleted" DataType="System.Boolean" ColumnType="CheckBox" AllowEditing="True" Width="30" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="姓名" DataField="UserName" Align="Center" DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="申请时间" DataField="createtime" Align="Center" FormatString="yyyy-MM-dd"  DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="事由开始时间" DataField="begintime" FormatString="yyyy-MM-dd HH:mm" Align="Center"  DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="事由结束时间" DataField="endtime" FormatString="yyyy-MM-dd HH:mm" Align="Center"  DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="事由内容" DataField="mattercontent" Align="Center"  DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="事由状态" DataField="matterstate" Align="Center" DataCellClientTemplateId="MatterStateTemplate" DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="事由类型" DataField="Approvetype" Align="Center" DataCellClientTemplateId="MatterTypeTemplate" DataType="System.String"/>
                                    <ComponentArt:GridColumn HeadingText="审批" DataCellClientTemplateId="auditTemplate" Width="50" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="事后申请" Align="Center" DataCellClientTemplateId="FlagIconTemplate" Width="50" FixedWidth="True" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">待审批事由列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="ShowTemplate">
                                <a style="color: #595959;" href="SingleOvertimeView.aspx?backurl=/Attendance/LeaveList.aspx&leaveid=## DataItem.GetMember('matId').Value ##">
                                    ## DataItem.GetMember('UserName').Value ##</a>
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="TypeTemplate">
                                ## if(DataItem.GetMember('matterstate').Value == 1) "工作日"; else "节假日"; ##
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="StateTemplate">
                                ## if(DataItem.GetMember('matterstate').Value == 1) "未提交"; else if(DataItem.GetMember('matterstate').Value == 2) "等待总监审批"; else if(DataItem.GetMember('matterstate').Value == 3) "审批通过";##
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="GroupByTemplate">
                                <span>## getTitle(DataItem, "Approvetype") ##</span>
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="GroupTextTemplate">
                                <span>## DataItem.GetMember('Approvetype').Value ##</span>
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="MatterTypeTemplate">
                                <span>## if (DataItem.GetMember('Approvetype').Value == 2) "请假单"; 
                                    else if (DataItem.GetMember('Approvetype').Value == 3) "OT单";
                                    else if (DataItem.GetMember('Approvetype').Value == 4) "出差单";
                                    else if (DataItem.GetMember('Approvetype').Value == 5) "调休单";
                                    else if (DataItem.GetMember('Approvetype').Value == 6) "其他事由单";
                                    else if (DataItem.GetMember('Approvetype').Value == 7) "外出单";
                                      else if (DataItem.GetMember('Approvetype').Value == 8) "晚到申请";
                                    else "";##</span>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="MatterStateTemplate">
                                <span>## if (DataItem.GetMember('Approvetype').Value == 3){ if(DataItem.GetMember('matterstate').Value == 1) "未提交"; else if(DataItem.GetMember('matterstate').Value == 2) "等待总监审批"; else if(DataItem.GetMember('matterstate').Value == 3) "审批通过";}
                                    else{if(DataItem.GetMember('matterstate').Value == 1) "未提交"; else if(DataItem.GetMember('matterstate').Value == 2) "审批通过"; else if(DataItem.GetMember('matterstate').Value == 3) "等待总监审批";else if(DataItem.GetMember('matterstate').Value == 4) "等待人力审批";}
                                    ##</span>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="auditTemplate">
                                <a href="AuditEdit.aspx?mattertype=## DataItem.GetMember('Approvetype').Value ##&matterid=## DataItem.GetMember('ID').Value ##&backurl=AuditList.aspx"">
                                    <img src="../images/Audit.gif" /></a>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="FlagIconTemplate">
                                <span>## GetAfterInfo(DataItem.GetMember('createtime').Value, DataItem.GetMember('begintime').Value) ##</span>
                            </ComponentArt:ClientTemplate>
                            
                            <ComponentArt:ClientTemplate ID="checkTemplate">
                                <input type="checkbox" id="chkAudit" name="chkAudit" value="## DataItem.GetMember('ID').Value ##" />
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                    <input type="checkbox" id="selectAll" onclick="CheckAllItems();"/>全选
                </td>
              </tr>
                <tr>
                    <td>
                        <br />
                        <asp:ImageButton ID="btnAudit" runat="server" OnClientClick="return AuditMatter();"
                            OnClick="btnAudit_Click" ImageUrl="~/images/apppass.jpg" />
                        <input type="hidden" id="hidMatter" value="" runat="server" /><input type="hidden"
                            id="hidType" value="" runat="server" />
                        <asp:ImageButton ID="btnOverrule" runat="server" OnClientClick="return OverruleMatter();"
                            OnClick="btnOverrule_Click" ImageUrl="~/images/appOverrule.jpg" />
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