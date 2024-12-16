<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddedClockInList.aspx.cs"　
    Inherits="AdministrativeWeb.Attendance.AddedClockInList" MasterPageFile="~/Default.Master" %>
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
                            申请时间：
                        </td>
                        <td>
                            <ComponentArt:Calendar ID="PickerFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerFrom_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            &nbsp;
                            <img id="calendar_from_button" alt="" onclick="ButtonFrom_OnClick(event)" onmouseup="ButtonFrom_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />&nbsp;
                            <ComponentArt:Calendar ID="PickerTo" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="PickerTo_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            &nbsp;
                            <img id="calendar_to_button" alt="" onclick="ButtonTo_OnClick(event)" onmouseup="ButtonTo_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="right">
                                <asp:Button  runat="server" ID ="btnAdd" Text=" 添加 "
                                OnClick="btnAdd_Click"/>
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
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                                runat="server"/>
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
                                GroupingNotificationText="补充的上下班时间" 
                                HeaderHeight="30" 
                                HeaderCssClass="GridHeader"
                                GroupingCountHeadingsAsRows="true" 
                                DataAreaCssClass="GridData" 
                                EnableViewState="true"
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
                                            <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="姓名" Align="Center" DataCellServerTemplateId="ShowUserName"/>
                                            <ComponentArt:GridColumn HeadingText="时间类型" DataField="InOrOut" DataCellServerTemplateId="ShowInOrOut" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="补充打卡时间" DataField="ReadTime" FormatString="yyyy-MM-dd HH:mm" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="备注" DataField="Remark" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="操作人" DataField="OperatorName" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="创建时间" DataField="CreateTime" FormatString="yyyy-MM-dd HH:mm" Align="Center"/>
                                            <ComponentArt:GridColumn HeadingText="删除" DataCellServerTemplateId="DeleteTemplate" Width="50" Align="Center" />
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
                                    <ComponentArt:GridServerTemplate ID="ShowUserName">
                                        <Template>
                                            <%# GetUserName(Container.DataItem["UserCode"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="ShowInOrOut">
                                        <Template>
                                            <%# GetInOrOut(Container.DataItem["InOrOut"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="EditTemplate">
                                        <Template>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="DeleteTemplate">
                                        <Template>
                                            <%# GetDeleteUrl(int.Parse(Container.DataItem["ID"].ToString())) %>
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
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
