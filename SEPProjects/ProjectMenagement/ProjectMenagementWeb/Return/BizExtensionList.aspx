﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Return_BizExtensionList" Codebehind="BizExtensionList.aspx.cs" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<%@ Import Namespace="ESP.Finance.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
    <uc1:AuditTab id="AuditTab" runat="server" TabIndex="5" />
    <br />
            <table width="100%">
        <tr>
            <td colspan="3" class="heading">检索</td>
        </tr>
        <tr>
            <td width="15%" class="oddrow">关键字：</td>
            <td width="35%" class="oddrow-l"><asp:TextBox ID="txtKey" runat="server" /></td>
            <td class="oddrow-l"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
        </tr>
    </table>
    <br />
    <ComponentArt:Grid ID="grComplete" GroupingPageSize="10" GroupingMode="ConstantGroups"
        GroupByTextCssClass="txt" GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt"
        DataAreaCssClass="GridData" EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter"
        PageSize="20" PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
        PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
        SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
        PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
        TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="100%"
        Height="100%" runat="server">
        <Levels>
            <ComponentArt:GridLevel ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                GroupHeadingClientTemplateId="GroupByTemplate" HeadingTextCssClass="HeadingCellText"
                SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell" SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                <Columns>
                    <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="负责人" DataField="Responser" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="付款内容" Align="Center" DataField="PaymentContent" />
                    <ComponentArt:GridColumn HeadingText="预计日期" Align="Center" DataField="PaymentPreDate"
                        FormatString="yyyy-MM-dd" />
                    <ComponentArt:GridColumn HeadingText="预计金额" Align="Center" DataField="PaymentBudget"
                        FormatString="#,##0.00" />
                    <ComponentArt:GridColumn HeadingText="公司代码" Align="Center" DataField="BranchCode" />
                    <ComponentArt:GridColumn HeadingText="付款类型" Align="Center" DataField="PaymentTypeName" />
                    <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataField="PaymentStatusName" />
                    <ComponentArt:GridColumn HeadingText="打印预览" Align="Center" DataField="Print" />
                    <ComponentArt:GridColumn HeadingText="审批状态" Align="Center" DataField="AuditStatus" />
                    <ComponentArt:GridColumn HeadingText="审批" DataField="Audit" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="发票登记" DataField="InvoiceSign" Align="Center" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
    </ComponentArt:Grid>
</asp:Content>