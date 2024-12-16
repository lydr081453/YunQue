<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="NotifyTabEdit.aspx.cs" Inherits="FinanceWeb.Edit.NotifyTabEdit" %>

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
                <uc1:tab ID="tab" runat="server" TabIndex="4" />
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
                                    <td class="oddrow-l" style="width: 35%" colspan="3">
                                        <asp:TextBox ID="txtKey" runat="server" />
</td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_OnClick"  CssClass="widebuttons" />
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
                                        <ComponentArt:Grid ID="GridProject" GroupingPageSize="10" OnItemDataBound="GridProject_ItemDataBound" CallbackCachingEnabled="true"  CallbackCacheSize="60"  RunningMode="Callback"
                                            GroupingMode="ConstantGroups" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"
                                            AllowTextSelection="true" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                                            EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PageSize="20"
                                            PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                            PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                            SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                            PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                            TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="100%"
                                            Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ProjectID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                <ComponentArt:GridColumn DataField="PaymentID" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="ProjectID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" DataField="Status" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="GroupID" DataField="GroupID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Step" DataField="Step" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="流水号" DataField="PaymentCode" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="负责人" DataField="ApplicantEmployeeName" Width="80"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="付款内容" DataField="PaymentContent" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="预计日期" DataField="PaymentPreDate" Width="80" Align="Center" FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="预计金额" DataField="PaymentBudget" Width="80" Align="Center" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="公司代码" DataField="BranchCode" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="付款类型" DataField="PaymentTypeName" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="PaymentStatusName" Width="40" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="编辑" DataField="Edit" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="延期" DataField="Extension" Width="50" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Width="40" Align="Center" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
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