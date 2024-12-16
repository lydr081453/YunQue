<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LateSearch.ascx.cs"
    Inherits="AdministrativeWeb.UserControls.IntegratedQuery.LateSearch" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script src="../../js/DatePicker.js" type="text/javascript"></script>

<link href="../../css/comboboxStyle.css" rel="stylesheet" type="text/css" />
<link href="../../css/gridViewStyle.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
<table width="98%" border="0" cellpadding="0" cellspacing="0" background="../../images/renli_24.gif"
    class="table_list">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                <tr>
                    <td width="17">
                        <img src="../../images/t2_03.jpg" width="21" height="20" />
                    </td>
                    <td align="left">
                        <strong>搜索</strong>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                <tr>
                    <td width="15%" align="right">
                        用 户 名：
                    </td>
                    <td width="30%" align="left">
                        <asp:TextBox ID="txtUserName" runat="server" Width="135px" Height="15px"></asp:TextBox>
                    </td>
                    <td width="15%" align="right">
                        部 门：
                    </td>
                    <td colspan="2">
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
                </tr>
                <tr>
                    <td width="15%" align="right">
                        迟到日期：
                    </td>
                    <td width="30%" align="left">
                        <ComponentArt:Calendar ID="PickerFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                            ControlType="Picker" PickerCssClass="picker">
                            <ClientEvents>
                                <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                            </ClientEvents>
                        </ComponentArt:Calendar>
                        <img id="calendar_from_button" alt="" onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)"
                            class="calendar_button" src="../../images/calendar/btn_calendar.gif" />&nbsp;
                        <ComponentArt:Calendar ID="PickerTo" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                            ControlType="Picker" PickerCssClass="picker">
                            <ClientEvents>
                                <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                            </ClientEvents>
                        </ComponentArt:Calendar>
                        <img id="calendar_to_button" alt="" onclick="ButtonTo_OnClick(event)" onmouseup="ButtonTo_OnMouseUp(event)"
                            class="calendar_button" src="../../images/calendar/btn_calendar.gif" />
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <br />
                        <asp:ImageButton ImageUrl="../../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                            <tr>
                                <td class="td">
                                    <ComponentArt:Grid ID="Grid1" CallbackCachingEnabled="true" CallbackCacheSize="50"
                                        Debug="false" PreHeaderClientTemplateId="PreHeaderTemplate" PostFooterClientTemplateId="PostFooterTemplate"
                                        DataAreaCssClass="GridData" EnableViewState="true" RunningMode="Callback" ShowHeader="false"
                                        FooterCssClass="GridFooter" PageSize="15" PagerStyle="Slider" PagerTextCssClass="GridFooterText"
                                        PagerButtonWidth="44" PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26"
                                        SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="50" SliderPopupClientTemplateId="SliderTemplate"
                                        SliderPopupCachedClientTemplateId="CachedSliderTemplate" ImagesBaseUrl="../../images/gridview/"
                                        PagerImagesFolderUrl="../../images/gridview/pager/" LoadingPanelFadeDuration="1000" LoadingPanelFadeMaximumOpacity="60"
                                        LoadingPanelClientTemplateId="LoadingFeedbackTemplate" LoadingPanelPosition="MiddleCenter"
                                        Width="100%" Height="100%" runat="server">
                                        <Levels>
                                            <ComponentArt:GridLevel AllowGrouping="false" DataKeyField="PostId" ShowTableHeading="false"
                                                TableHeadingCssClass="GridHeader" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                                                DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                SortImageHeight="19" TableHeadingClientTemplateId="TableHeadingTemplate">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="PriorityIcon" Align="Center" DataCellClientTemplateId="PriorityIconTemplate"
                                                        HeadingCellCssClass="FirstHeadingCell" DataCellCssClass="FirstDataCell" HeadingImageUrl="icon_priority.gif"
                                                        HeadingImageWidth="9" HeadingImageHeight="14" AllowGrouping="false" Width="12"
                                                        FixedWidth="True" />
                                                    <ComponentArt:GridColumn DataField="EmailIcon" Align="Center" DataCellClientTemplateId="EmailIconIconTemplate"
                                                        HeadingImageUrl="icon_icon.gif" HeadingImageWidth="14" HeadingImageHeight="16"
                                                        AllowGrouping="false" Width="20" FixedWidth="True" />
                                                    <ComponentArt:GridColumn DataField="AttachmentIcon" Align="Center" DataCellClientTemplateId="AttachmentIconTemplate"
                                                        HeadingImageUrl="icon_attachment.gif" HeadingImageWidth="12" HeadingImageHeight="16"
                                                        AllowGrouping="false" Width="12" FixedWidth="True" />
                                                    <ComponentArt:GridColumn DataField="Subject" />
                                                    <ComponentArt:GridColumn DataField="LastPostDate" HeadingText="Received" FormatString="MMM dd yyyy, hh:mm tt" />
                                                    <ComponentArt:GridColumn DataField="StartedBy" Width="80" />
                                                    <ComponentArt:GridColumn DataField="TotalViews" DefaultSortDirection="Descending"
                                                        Width="80" />
                                                    <ComponentArt:GridColumn DataField="FlagIcon" Align="Center" DataCellClientTemplateId="FlagIconTemplate"
                                                        DataCellCssClass="LastDataCell" HeadingImageUrl="icon_flag.gif" HeadingImageWidth="16"
                                                        HeadingImageHeight="14" AllowGrouping="false" Width="20" FixedWidth="True" />
                                                    <ComponentArt:GridColumn DataField="PostId" Visible="false" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 10px; height: 34px; background-image: url(../../images/gridview/grid_preheader_01.jpg);"></td>
                                                        <td style="height: 34px; background-image: url(../../images/gridview/grid_preheader_02.jpg); background-repeat:repeat-x;"><span style="color:White;font-weight:bold;">迟到用户信息列表</span></td>
                                                        <td style="width: 10px; height: 34px; background-image: url(../../images/gridview/grid_preheader_03.jpg);"></td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg); background-repeat:repeat-x;"></td>
                                                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat:repeat-x;">&nbsp;</td>
                                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg); background-repeat:repeat-x;"></td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="TableHeadingTemplate">
                                                Try paging, sorting, column resizing, and column reordering.
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="PriorityIconTemplate">
                                                <img src="../../images/gridview/## DataItem.GetMember('PriorityIcon').Value ##" width="8" height="10"
                                                    border="0">
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="EmailIconIconTemplate">
                                                <img src="../../images/gridview/## DataItem.GetMember('EmailIcon').Value ##" width="20" height="15"
                                                    border="0">
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="AttachmentIconTemplate">
                                                <img src="../../images/gridview/## DataItem.GetMember('AttachmentIcon').Value ##" width="8" height="10"
                                                    border="0">
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="FlagIconTemplate">
                                                <img src="../../images/gridview/## DataItem.GetMember('FlagIcon').Value ##" width="12" height="12"
                                                    border="0">
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
                                                                        <img src="../../images/gridview/spinner.gif" width="16" height="16" border="0">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="SliderTemplate">
                                                <table class="SliderPopup" style="background-color: #ffffff" cellspacing="0" cellpadding="0"
                                                    border="0">
                                                    <tr>
                                                        <td valign="top" style="padding: 5px;" valign="center" align="center">
                                                            <div style="width: 278px; height: 34px; color: #808080;">
                                                                <table height="100%" width="100%">
                                                                    <tr>
                                                                        <td valign="center" align="center">
                                                                            [ data not loaded]
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 14px; background-color: #808080;">
                                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td style="padding-left: 5px; color: white; font-family: verdana; font-size: 10px;">
                                                                        Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## Grid1.PageCount ##</b>
                                                                    </td>
                                                                    <td style="padding-right: 5px; color: white; font-family: verdana; font-size: 10px;"
                                                                        align="right">
                                                                        Message <b>## DataItem.Index + 1 ##</b> of <b>## Grid1.RecordCount ##</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                            <ComponentArt:ClientTemplate ID="CachedSliderTemplate">
                                                <table class="SliderPopup" style="background-color: #ffffff;" cellspacing="0" cellpadding="0"
                                                    border="0">
                                                    <tr>
                                                        <td valign="top" style="padding: 5px;">
                                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td width="25" align="center" valign="top" style="padding-top: 3px;">
                                                                        <img src="../../images/gridview/## DataItem.GetMember('EmailIcon').Value ##" width="20" height="15"><br>
                                                                        <img src="../../images/gridview/## DataItem.GetMember('PriorityIcon').Value ##" width="8" height="10"
                                                                            border="0"><img src="../../images/gridview/## DataItem.GetMember('AttachmentIcon').Value ##" width="8"
                                                                                height="10" border="0">
                                                                    </td>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="2" border="0" style="width: 255px;">
                                                                            <tr>
                                                                                <td style="font-family: verdana; font-size: 11px;">
                                                                                    <div style="overflow: hidden; width: 115px;">
                                                                                        <nobr>## DataItem.GetMember('StartedBy').Value ##</nobr>
                                                                                    </div>
                                                                                </td>
                                                                                <td style="font-family: verdana; font-size: 11px;">
                                                                                    <div style="overflow: hidden; width: 135px;">
                                                                                        <nobr>## DataItem.GetMember('LastPostDate').Text ##</nobr>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="230" colspan="2" style="font-family: verdana; font-size: 11px; font-weight: bold;">
                                                                                                <div style="text-overflow: ellipsis; overflow: hidden; width: 250px;">
                                                                                                    <nobr>## DataItem.GetMember('Subject').Value ##</nobr>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 14px; background-color: #000000;">
                                                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td style="padding-left: 5px; color: white; font-family: verdana; font-size: 10px;">
                                                                        Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## Grid1.PageCount ##</b>
                                                                    </td>
                                                                    <td style="padding-right: 5px; color: white; font-family: verdana; font-size: 10px;"
                                                                        align="right">
                                                                        Message <b>## DataItem.Index + 1 ##</b> of <b>## Grid1.RecordCount ##</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                    </ComponentArt:Grid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<ComponentArt:Calendar runat="server" ID="CalendarFrom" AllowMultipleSelection="false"
    AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
    PopUp="Custom" PopUpExpandControlId="calendar_from_button" CalendarTitleCssClass="title"
    DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
    SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
    MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
    ImagesBaseUrl="../../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
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
    ImagesBaseUrl="../../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
    <ClientEvents>
        <SelectionChanged EventHandler="CalendarTo_OnChange" />
    </ClientEvents>
</ComponentArt:Calendar>
<ComponentArt:Menu ID="Menu1" runat="server" ContextMenu="Custom" SiteMapXmlFile="menu.xml"
    Orientation="Vertical" DefaultGroupExpandOffsetX="4" DefaultGroupExpandOffsetY="-1"
    ExpandDuration="0" CollapseDuration="0" DefaultItemLookId="ItemLook" CssClass="mnu"
    ShadowEnabled="true">
    <ItemLooks>
        <ComponentArt:ItemLook LookId="ItemLook" CssClass="itm" HoverCssClass="itm-h" />
        <ComponentArt:ItemLook LookId="BreakItemLook" CssClass="br" />
    </ItemLooks>
    <ClientTemplates>
        <ComponentArt:ClientTemplate ID="ItemTemplate">
            <div>
                <span class="ico ## DataItem.get_value(); ##"></span><span class="txt">## DataItem.get_text();
                    ##</span>
            </div>
        </ComponentArt:ClientTemplate>
    </ClientTemplates>
</ComponentArt:Menu>
