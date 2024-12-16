<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="OperationAuditList.aspx.cs" Inherits="FinanceWeb.project.OperationAuditList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function getTitle(groupItem) {
            var rows = groupItem.Rows;
            var sum = 0;
            var row;
            var count = 0;
            var space = "&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";

            return groupItem.Grid.Table.GetRow(rows[0]).GetMember("FormType").Value;
        }
    </script>

    <uc1:AuditTab id="AuditTab" runat="server" TabIndex="1" />
    <br />
    <div style="width: 100%; overflow-y: scroll; height: 400">
        <ComponentArt:Grid ID="grComplete" GroupBy="FormType DESC" GroupingPageSize="10"
            GroupingMode="ConstantGroups" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"
            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true"
            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider"
            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
            PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
            <Levels>
                <ComponentArt:GridLevel ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                    GroupHeadingClientTemplateId="GroupByTemplate" HeadingTextCssClass="HeadingCellText"
                    SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell" SortAscendingImageUrl="asc.gif"
                    SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                    <Columns>
                        <ComponentArt:GridColumn HeadingText="单据编号" DataField="FormNumber" Align="Center" />
                        <ComponentArt:GridColumn HeadingText="申请人" DataField="ApplicantName" Align="Center" />
                        <ComponentArt:GridColumn HeadingText="申请日期" DataField="AppliedTime" Align="Center"
                            FormatString="yyyy-MM-dd" />
                        <ComponentArt:GridColumn HeadingText="待审批人" Align="Center" DataField="ApproverName"
                            Visible="false" />
                        <ComponentArt:GridColumn HeadingText="描述" Align="Center" DataField="Description" />
                        <ComponentArt:GridColumn HeadingText="审批" Align="Center" DataField="Url" DataCellServerTemplateId="UrlTmp"/>
                        <ComponentArt:GridColumn HeadingText="FormType" Align="Center" DataField="FormType"
                            Visible="false" />
                            <ComponentArt:GridColumn HeadingText="FormID" Align="Center" DataField="FormID"
                            Visible="false" />
                    </Columns>
                </ComponentArt:GridLevel>
            </Levels>
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="urlT">
                    <a href="..## DataItem.GetMember('Url').Value ##&BackUrl=/project/OperationAuditList.aspx">
                        <img src="/images/Audit.gif" border="0" /></a>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="GroupByTemplate">
                    <span>## getTitle(DataItem) ##</span>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ServerTemplates>
                <ComponentArt:GridServerTemplate ID="UrlTmp">
                    <Template>
                        <%#getUrl(Container.DataItem["FormType"].ToString(), Container.DataItem["FormID"].ToString(), Container.DataItem["Url"].ToString())%>
                    </Template>
                </ComponentArt:GridServerTemplate>
            </ServerTemplates>
        </ComponentArt:Grid>
    </div>
</asp:Content>
