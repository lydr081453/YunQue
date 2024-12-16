<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="SupporterTabList.aspx.cs" Inherits="project_SupporterTabList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/ProjectTab.ascx" TagName="tab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function DeleteRow(rowId) {
            GridProject.deleteItem(GridProject.getItemFromClientId(rowId));
        }

    </script>

    <table style="width: 100%">
     <tr>
            <td width="100%" align="center">
          <uc1:tab ID="tab" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        项目状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Text="请选择" Selected="True" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="已提交" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="已审批" Value="32"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_OnClick" CssClass="widebuttons" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="oddrow">
                                        <ComponentArt:Grid ID="GridProject" GroupingPageSize="10" OnItemDataBound="GridProject_ItemDataBound" 
                                            GroupingMode="ConstantGroups" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"  AllowTextSelection="true"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true" 
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="SupportID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="SupportID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="ProjectID" Visible="false" />
                                                         <ComponentArt:GridColumn HeadingText="Status" DataField="Status" Visible="false" />
                                                         <ComponentArt:GridColumn HeadingText="GroupID" DataField="GroupID" Visible="false" />
                                                         <ComponentArt:GridColumn HeadingText="Step" DataField="Step" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="流水号" DataField="SupporterCode" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Width="150" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="服务类型" DataField="ServiceType" Width="100"
                                                            Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="费用类型" DataField="IncomeType" Width="100"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="负责人" DataField="LeaderEmployeeName" Width="100"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="业务组别" DataField="GroupName" Width="150" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="StatusText" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="查看" DataField="View" Width="50" Align="Center" />
                                                         <ComponentArt:GridColumn HeadingText="审批状态" DataField="ViewAudit" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="变更" DataField="Change" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="历史" DataField="History" Align="Center" Width="50" />
                                                        <ComponentArt:GridColumn HeadingText="成本" DataField="CBX" Align="Center" Width="50" DataCellClientTemplateId="CBXT"/>
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                                                                <ComponentArt:ClientTemplate ID="CBXT">
                                                    <a target="_blank" href="/CostView/SupSinglePrjView.aspx?ProjectID=## DataItem.GetMember('ProjectID').Value ##&SupportID=## DataItem.GetMember('SupportID').Value ##"  target="_blank">
                                                            <img src="../images/dc.gif" border="0px;" title="查看成本"></a>
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
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
