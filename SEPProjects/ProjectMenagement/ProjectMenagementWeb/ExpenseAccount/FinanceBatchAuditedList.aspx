<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditedList.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditedList" Theme="" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/BatchTab.ascx" TagName="BatchTab" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="images/t2_03.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>搜索 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            批 次 号:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            提交日期:&nbsp;&nbsp;<asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);"
                                onkeyDown="return false; "></asp:TextBox>
                            --
                            <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ImageUrl="/images/t2_03-07.jpg" ID="btnSearch" OnClick="btnSearch_Click"
                                Width="56" Height="24" runat="server" ImageAlign="AbsMiddle" BorderColor="Silver"
                                BorderStyle="Solid" />
                        </td>
                    </tr>
                </table>
                <br />
                   <uc1:BatchTab id="BatchTab" runat="server" TabIndex="2" />
                   
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="gdAudited" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"
                                GroupingMode="ConstantGroups" GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"
                                EditOnClickSelectedItem="false" FooterCssClass="GridFooter" FooterHeight="40"
                                HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="已审批项" PagerInfoClientTemplateId="ClientTemplate4" PagerStyle="Slider"
                                PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                                SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35"
                                ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                                TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                PageSize="200" ShowHeader="false" PreExpandOnGroup="true" Width="100%" Height="100%"
                                runat="server">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="BatchID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                        ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                                        DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                                        SortImageHeight="5">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="BatchID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="CreatorID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="批次号" DataField="PurchaseBatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="银行凭证" DataField="BatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="总金额" DataField="TotalPreFee" Align="Center"
                                                FormatString="0.00" />
                                            <ComponentArt:GridColumn HeadingText="批次创建人" DataCellServerTemplateId="createrTemplate2"
                                                Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="创建日期" DataField="CreateDate" Align="Center"
                                                FormatString="yyyy-MM-dd" />
                                            <ComponentArt:GridColumn HeadingText="批次中单据数量" DataField="ReturnCount" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="审批" DataCellServerTemplateId="AuditTemplate2"
                                                Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="PreHeaderTemplate2">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 10px; height: 34px; background-image: url(images/gridview/grid_preheader_01.jpg);">
                                                </td>
                                                <td style="height: 34px; background-image: url(images/gridview/grid_preheader_02.jpg);
                                                    background-repeat: repeat-x;">
                                                    <span style="color: White; font-weight: bold;">列表</span>
                                                </td>
                                                <td style="width: 10px; height: 34px; background-image: url(images/gridview/grid_preheader_03.jpg);">
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="PostFooterTemplate2">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 10px; background-image: url(images/gridview/grid_postfooter_01.jpg);
                                                    background-repeat: repeat-x;">
                                                </td>
                                                <td style="background-image: url(images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 10px; background-image: url(images/gridview/grid_postfooter_03.jpg);
                                                    background-repeat: repeat-x;">
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate4">
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="createrTemplate2">
                                        <Template>
                                            <%#getEmpName(Container.DataItem["CreatorID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="AuditTemplate2">
                                        <Template>
                                            <%#getUrl(Container.DataItem["BatchID"].ToString())%>
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
</asp:Content>
