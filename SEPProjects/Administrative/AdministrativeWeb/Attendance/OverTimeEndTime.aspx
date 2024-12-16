<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverTimeEndTime.aspx.cs" MasterPageFile="~/Default.Master"Inherits="AdministrativeWeb.Attendance.OverTimeEndTime" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <ComponentArt:Grid ID="Grid1" 
        AllowTextSelection="true"
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
                    <ComponentArt:GridColumn HeadingText="姓名ID" DataField="UserID" Align="Center" Visible="true"/>
                    <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center" Visible="true"/>
                    <ComponentArt:GridColumn HeadingText="姓名" DataField="EmployeeName" Align="Center" Visible="true"/>
                    <ComponentArt:GridColumn HeadingText="申请时间" DataField="AppTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" Visible="true"/>
                    <ComponentArt:GridColumn HeadingText="开始时间" DataField="BeginTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="结束时间" DataField="EndTime" FormatString="yyyy-MM-dd HH:mm" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="下班时间" DataField="CreateTime" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="OT原因" DataField="OverTimeCause" Align="Center"/>
                    <ComponentArt:GridColumn HeadingText="OT项目号" DataField="ProjectNo" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="审批人" DataField="ApproveName" Align="Center" />
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
    </ComponentArt:Grid>
</asp:Content>
