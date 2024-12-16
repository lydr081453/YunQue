<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ReturnAuditList.aspx.cs" Inherits="FinanceWeb.Purchase.ReturnAuditList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<%@ Import Namespace="ESP.Finance.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript">
        function Export(userId, commandName) {
            return FinanceWeb.Purchase.ReturnAuditList.ExportCommand(userId, commandName);
        }
    </script>
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
    <uc1:AuditTab ID="AuditTab" runat="server" TabIndex="4" />
    <br />
    <table width="100%">
        <tr>
            <td colspan="4" class="heading">检索</td>
        </tr>
        <tr>
            <td width="15%" class="oddrow">关键字：</td>
            <td width="35%" class="oddrow-l">
                <asp:TextBox ID="txtKey" runat="server" /></td>
            <td width="15%" class="oddrow">公司代码：</td>
            <td width="35%" class="oddrow-l">
                <asp:TextBox ID="txtBranchCode" runat="server" /></td>

        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
        </tr>
    </table>
    <br />
    <ComponentArt:Grid ID="grComplete" GroupingPageSize="10" GroupingMode="ConstantGroups" AllowTextSelection="true" CallbackCachingEnabled="true" CallbackCacheSize="60" RunningMode="Callback"
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
                    <ComponentArt:GridColumn HeadingText="PN号" DataField="ReturnCode" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="PR号" DataField="PRNO" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="负责人" DataField="RequestEmployeeName" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="预付金额" Align="Center" DataField="PreFee" FormatString="#,##0.00" />
                    <ComponentArt:GridColumn HeadingText="是否发票" Align="Center" DataField="IsInvoice" />
                    <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataField="ReturnStatus" />
                    <ComponentArt:GridColumn HeadingText="帐期类型" Align="Center" DataField="PaymentTypeName" />
                    <ComponentArt:GridColumn HeadingText="供应商名称" Align="Center" DataField="SupplierName" />
                    <ComponentArt:GridColumn HeadingText="附件" Align="Center" DataField="Attach" />
                    <ComponentArt:GridColumn HeadingText="导出" Align="Center" DataField="Export" />
                    <ComponentArt:GridColumn HeadingText="打印预览" Align="Center" DataField="Print" />
                    <ComponentArt:GridColumn HeadingText="审批状态" DataField="AuditStatus" Align="Center" />
                    <ComponentArt:GridColumn HeadingText="审批" DataField="Audit" Align="Center" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
    </ComponentArt:Grid>
</asp:Content>
