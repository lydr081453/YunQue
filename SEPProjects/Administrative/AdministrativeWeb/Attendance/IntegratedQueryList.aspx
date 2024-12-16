<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntegratedQueryList.aspx.cs" Inherits="AdministrativeWeb.Attendance.IntegratedQueryList"
    MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <link href="../css/comboboxStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // ɾ�����ڼ�¼��Ϣ
        function DeleteInfo(control, idvalue, flag) {
            if (confirm("��ȷ��Ҫɾ����")) {
                control.href = "AddedClockInList.aspx?clockinid=" + idvalue + "&flag=" + flag;
                return true;
            }
            return false;
        }
        function GetViewInfo(userid) {
            return "<a href='IntegratedQueryView.aspx?ApplicantID=" + userid + "'><img src=\"../images/dc.gif\" title=\"�鿴������ϸ��Ϣ\"></a>";
        }

        // ȫѡ���еĵȴ�������
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

        // ����ͨ����ѡ��Ŀ�������
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
                alert("��ѡ��Ҫ�ʼ����ѵĶ���");
                return false;
            }
            else {
                return confirm("��ȷ��Ҫ�����ʼ�������");
            }
        }
    </script>
    <script src="../js/DatePicker.js" type="text/javascript"></script>

    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="right">������
            </td>
            <td align="left">&nbsp;<asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="202" Height="18"></asp:TextBox>
            </td>
            <td align="right">Ա����ţ�
            </td>
            <td align="left">
                <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">״̬��
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
                                    <ComponentArt:ComboBoxItem Text="��ѡ��..." Value="0" />
                                    <ComponentArt:ComboBoxItem Text="δ�ύ" Value="1" />
                                    <ComponentArt:ComboBoxItem Text="�ȴ�TeamLeader����" Value="2" />
                                    <ComponentArt:ComboBoxItem Text="�ȴ��Ŷ�HRAdmin����" Value="3" />
                                    <ComponentArt:ComboBoxItem Text="�ȴ��Ŷ��ܾ�������" Value="4" />
                                    <ComponentArt:ComboBoxItem Text="�ȴ����ڹ���Աȷ��" Value="5" />
                                    <ComponentArt:ComboBoxItem Text="����ͨ��" Value="6" />
                                    <ComponentArt:ComboBoxItem Text="��������" Value="7" />
                                </Items>
                            </ComponentArt:ComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">ʱ�䣺
            </td>
            <td align="left">
                <asp:DropDownList ID="drpYear" runat="server"></asp:DropDownList>��
                            <asp:DropDownList ID="drpMonth" runat="server"></asp:DropDownList>��
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">���ţ�
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
            <td align="right">ְλ��
            </td>
            <td align="left">
                <asp:TextBox ID="txtPositions" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="246" Height="18"></asp:TextBox>
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">��ְ״̬��
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
                                    <ComponentArt:ComboBoxItem Text="��ְ" Value="1" />
                                    <ComponentArt:ComboBoxItem Text="��ְ" Value="2" />
                                </Items>
                            </ComponentArt:ComboBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">&nbsp;
            </td>
            <td align="left">&nbsp;
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">&nbsp;
            </td>
            <td align="left">&nbsp;
            </td>
            <td align="right">&nbsp;
            </td>
            <td>
                <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                    runat="server" OnClick="btnSearch_Click" />
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">&nbsp;
            </td>
            <td align="left">&nbsp;
            </td>
            <td align="right">&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td align="right">&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <ComponentArt:Grid ID="Grid1"
                    AllowColumnResizing="true"
                    GroupingPageSize="10"
                    GroupingMode="ConstantGroups"
                    GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp"
                    GroupingNotificationTextCssClass="GridHeaderText"
                    GroupingNotificationText="������Ϣ�б�"
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
                    SearchText="�ؼ�������"
                    SearchTextCssClass="SearchText">
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
                                <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="ѡ��" DataField="Deleted" DataType="System.Boolean" ColumnType="CheckBox" AllowEditing="True" Width="30" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="����" DataField="UserName" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="Ա�����" DataField="UserCode" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="����" DataField="Department" Align="Center" Width="100" />
                                <ComponentArt:GridColumn HeadingText="ְλ" DataField="Position" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="ʱ��" DataField="Time" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="״̬" DataField="Content" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="�ύʱ��" DataField="SubmitTime" Align="Center" Width="80"/>
                                <ComponentArt:GridColumn HeadingText="�鿴" Width="50" DataCellClientTemplateId="ViewTemplate" Align="Center" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                    <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg); background-repeat: repeat-x;">
                                        <span style="color: White; font-weight: bold;">�����������б�</span>
                                    </td>
                                    <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg); background-repeat: repeat-x;"></td>
                                    <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;
                                    </td>
                                    <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg); background-repeat: repeat-x;"></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="ViewTemplate">
                            <span>## GetViewInfo(DataItem.GetMember('UserID').Value) ##</span>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="ShowUserName">
                            <Template>
                                <%--<%# GetUserName(Container.DataItem["UserCode"].ToString())%>--%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="ShowCardState">
                            <Template>
                                <%--<%# GetCardState(Container.DataItem["CardState"].ToString())%>--%>
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
        <tr>
            <td>
                <br />
                <input type="checkbox" id="selectAll" onclick="CheckAllItems();" />ȫѡ
                <asp:ImageButton ID="btnSendMail" runat="server" OnClientClick="return SendMail();"
                    ImageUrl="~/images/sendmail.jpg" OnClick="btnSendMail_Click" />
                <input type="hidden" id="hidMatter" value="" runat="server" />
            </td>
        </tr>
    </table>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <ComponentArt:Calendar runat="server"
        ID="CalendarFrom"
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
        ID="CalendarTo"
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
            <SelectionChanged EventHandler="CalendarFrom1_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>
    <ComponentArt:Calendar runat="server"
        ID="CalendarTo1"
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
