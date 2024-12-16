<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TicketReceipient.aspx.cs" Inherits="FinanceWeb.Edit.TicketReceipient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/EditTab.ascx" TagName="tab" TagPrefix="uc1" %>
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
                <uc1:tab ID="tab" runat="server" TabIndex="6" />
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
                                     <td class="oddrow-l" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" />
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
                                        <ComponentArt:Grid ID="GridProject" GroupingPageSize="10" GroupingMode="ConstantGroups" EditOnClickSelectedItem="false" CallbackCachingEnabled="true"  CallbackCacheSize="60"  RunningMode="Callback"
                                            GroupByTextCssClass="txt" GroupBySectionCssClass="grp" AllowTextSelection="true"  AutoPostBackOnDelete="true"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true" 
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider" OnItemDataBound="GridProject_ItemDataBound"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel ShowTableHeading="false" DataKeyField="returnID" TableHeadingCssClass="GridHeader" AllowSorting="true" 
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                    <ComponentArt:GridColumn HeadingText="ReturnID" DataField="ReturnID" Visible="false"/>
                                                        <ComponentArt:GridColumn HeadingText="报销单号" DataField="ReturnCode" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Width="100"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Width="100" Align="Center"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="申请日期" DataField="RequestDate" Width="100" Align="Center"
                                                            FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="StatusText" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Width="40" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="编辑" DataField="Edit" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="删除" DataField="Delete" Align="Center" DataCellClientTemplateId="deleteT" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                             <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="deleteT">
                                                    <a onclick="return confirm('您是否确认删除？');" href="javascript:DeleteRow('## DataItem.ClientId ##')">
                                                        <img src='../../images/Icon_Cancel.gif' border='0' /></a>
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
