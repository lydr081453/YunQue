<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardNoList.aspx.cs" Inherits="AdministrativeWeb.Attendance.CardNoList" 
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
        // ɾ�����ڼ�¼��Ϣ
        function DeleteInfo(control, idvalue, flag) {
            if (confirm("��ȷ��Ҫɾ����")) {
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
                            <strong>���� </strong> <%=CardStoreCount %>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="left">
                            �û�����
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="left">
                            �ſ��ţ�
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCardNo" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:ImageButton ImageUrl="../images/export.jpg" ID="btnExport" runat="server" OnClick="btnExport_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ����ʱ�䣺
                        </td>
                        <td>
                            <ComponentArt:Calendar ID="PickerFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_from_button" alt="" onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                                &nbsp;
                            <ComponentArt:Calendar ID="PickerTo" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_to_button" alt="" onclick="ButtonTo_OnClick(event)" onmouseup="ButtonTo_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                        </td>
                        <td align="left">
                            ͣ��ʱ�䣺
                        </td>
                        <td align="left">
                            <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom1_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_from_button1" alt="" onclick="ButtonFrom1_OnClick(event)" onmouseup="ButtonFrom1_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                                &nbsp;
                            <ComponentArt:Calendar ID="PickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo1_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_to_button1" alt="" onclick="ButtonTo1_OnClick(event)" onmouseup="ButtonTo1_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ״̬��
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpEnable" runat="server" Height="20" Width="95px">
                                <asp:ListItem Text="��ѡ��..." Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="ʹ����" Value="1"></asp:ListItem>
                                <asp:ListItem Text="��ͣ��" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            Ա����ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ���ţ�
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
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="ImageButton1"
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
                                GroupingNotificationText="�ſ���Ϣ�б�" 
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
                                SearchText="�ؼ���������"
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
                                    <ComponentArt:GridLevel DataKeyField="ID" 
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
                                            <ComponentArt:GridColumn DataField="ID" Visible="false"/>
                                            <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="����" DataField="EmployeeName" Align="Center" Width="55"/>
                                            <ComponentArt:GridColumn HeadingText="Ա�����" DataField="UserCode" Align="Center" Width="55"/>
                                            <ComponentArt:GridColumn HeadingText="����" DataField="Department" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="�ſ���" DataField="CardNo" Align="Center" Width="70"/>
                                            <ComponentArt:GridColumn HeadingText="�Ƿ�����" DataField="CardState" Align="Center" DataCellServerTemplateId="ShowCardState" />
                                            <ComponentArt:GridColumn HeadingText="����ʱ��" DataField="CardEnableTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="ͣ��ʱ��" DataField="CardUnEnableTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="����ʱ��" DataField="CreateTime" FormatString="yyyy-MM-dd HH:mm" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="�༭" DataCellServerTemplateId="EditTemplate" Width="30" Align="Right"/>
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
                                                    <span style="color: White; font-weight: bold;">�����������б�</span>
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
                                    <ComponentArt:GridServerTemplate ID="ShowUserName">
                                        <Template>
                                            <%--<%# GetUserName(Container.DataItem["UserCode"].ToString())%>--%></Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="ShowCardState">
                                        <Template>
                                            <%# GetCardState(Container.DataItem["CardState"].ToString())%></Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="EditTemplate">
                                        <Template>
                                            <%# GetEditUrl(Container.DataItem["UserID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="DeleteTemplate">
                                        <Template>
                                            <%--<%# GetDeleteUrl(int.Parse(Container.DataItem["ID"].ToString())) %>--%></Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates>
                            </ComponentArt:Grid>
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
        ImagesBaseUrl="../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
        <ClientEvents>
            <SelectionChanged EventHandler="CalendarFrom_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server" ID="CalendarTo" AllowMultipleSelection="false"
        AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
        PopUp="Custom" PopUpExpandControlId="calendar_to_button" CalendarTitleCssClass="title"
        DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
        SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
        MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
        ImagesBaseUrl="../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
        <ClientEvents>
            <SelectionChanged EventHandler="CalendarTo_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server" ID="CalendarFrom1" AllowMultipleSelection="false"
        AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
        PopUp="Custom" PopUpExpandControlId="calendar_from_button1" CalendarTitleCssClass="title"
        DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
        SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
        MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
        ImagesBaseUrl="../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
        <ClientEvents>
            <SelectionChanged EventHandler="CalendarFrom1_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server" ID="CalendarTo1" AllowMultipleSelection="false"
        AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
        PopUp="Custom" PopUpExpandControlId="calendar_to_button1" CalendarTitleCssClass="title"
        DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
        SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
        MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
        ImagesBaseUrl="../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
        <ClientEvents>
            <SelectionChanged EventHandler="CalendarTo1_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>
</asp:Content>