<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="SupSinglePrjView.aspx.cs" Inherits="FinanceWeb.CostView.SupSinglePrjView" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Project/SupporterInfoDisplay.ascx" TagName="supporter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page *//* Default tab */.AjaxTabStrip .ajax__tab_tab
        {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */.AjaxTabStrip .ajax__tab_hover .ajax__tab_tab
        {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */.AjaxTabStrip .ajax__tab_active .ajax__tab_tab
        {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */.AjaxTabStrip .ajax__tab_body
        {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }
        .border
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-bottom-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border2
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border_title_left
        {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_title_right
        {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_datalist
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CC3333;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function getTitle(groupItem, column) {
            var rows = groupItem.Rows;
            var sum = "";
            var row;
            var space = "&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            for (i = 0; i < 1; i++) {
                row = groupItem.Grid.Table.GetRow(rows[i]);
                sum += row.GetMember("TypeName").Value + space + "预算总额: " + row.GetMember("CostPreAmount").Value + space + "已申请总额: " + row.GetMember("TypeTotalAmount").Value;
            }
            return sum;
        }
        function getPNTitle(groupItem, column) {
            var rows = groupItem.Rows;
            var sum = "";
            var row;
            var space = "&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            for (i = 0; i < 1; i++) {
                row = groupItem.Grid.Table.GetRow(rows[i]);
                sum += row.GetMember("PrNo").Value + space + "申请人: " + row.GetMember("Requestor").Value + space + "申请金额: " + row.GetMember("PNTotal").Value;
            }
            return sum;
        }
        $(document).ready(function() {
            var r = $("#<%= raList.ClientID %> input").click(function() {
                var form = $(document.createElement("form"));
                form.appendTo(document.body);
                form.attr("action", document.URL);
                form.attr("method", "POST");
                var hdn = $("<input type='hidden'>").val(this.value).attr("name", "ExpenseGroupBy").appendTo(form);
                form.submit();
                return false;
            });
        });
    </script>

    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <uc1:TabPanel ID="TabPanel1" HeaderText="支持方信息" runat="server">
            <ContentTemplate>
                <uc1:supporter ID="Supporter" runat="server" DontBindOnLoad="True" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="费用申请明细" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <ComponentArt:Grid ID="GridPR" GroupBy=" TypeID desc" GroupingPageSize="10" GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt"
                                DataAreaCssClass="GridData" EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter"
                                PageSize="20" PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="100%"
                                Height="100%" runat="server">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="PrId" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                        RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                        GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="PrId" Visible="false" />
                                             <ComponentArt:GridColumn DataField="PrNo" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="PR流水" DataField="ID" Width="50" Align="Center" />
                                             <ComponentArt:GridColumn HeadingText="PR单号" Width="100" DataCellServerTemplateId="PRTemplate"
                                                Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Width="150" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="申请描述" DataField="Description" Width="150" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="Requestor" Width="50" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="业务组别" DataField="GroupName" Width="100" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="成本项" DataField="TypeID" Width="0" Align="Center"
                                                Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="成本项" DataField="TypeName" Width="0" Align="Center"
                                                Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="CostPreAmount" DataField="CostPreAmount" Width="0"
                                                Align="Right" FormatString="#,##0.00" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="TypeTotalAmount" DataField="TypeTotalAmount"
                                                Width="0" Align="Right" FormatString="#,##0.00" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="占用金额" DataField="OrderTotal" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="PR总额" DataField="AppAmount" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="已付总额" DataField="PaidAmount" Width="80" FormatString="#,##0.00"
                                                Align="Right" />
                                            <ComponentArt:GridColumn HeadingText="未付总额" DataField="UNPaidAmount" Width="80"
                                                FormatString="#,##0.00" Align="Right" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="GroupByTemplate">
                                        <span>## getTitle(DataItem, "TypeID") ##</span>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                 <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="PRTemplate">
                                        <Template>
                                            <%#getUrl(Container.DataItem["PrId"].ToString(), Container.DataItem["PrNo"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates>
                            </ComponentArt:Grid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="OOP报销明细" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" AutoPostBack="true" ID="raList" RepeatDirection="Horizontal">
                                <asp:ListItem Text="按成本项分类" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="按单据分类" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:Grid ID="GridOOP" GroupBy=" TypeID desc" GroupingPageSize="10" GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt"
                                DataAreaCssClass="GridData" EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter"
                                PageSize="20" PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="100%"
                                Height="100%" runat="server">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                        RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                        GroupHeadingClientTemplateId="GroupByTemplate2" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="PrNo" DataField="PrNo" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="单据类型" DataField="ReturnType" Width="100" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="单号" DataField="PrNo" Width="100" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="申请描述" DataField="Description" Width="150" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="Requestor" Width="50" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="业务组别" DataField="GroupName" Width="100" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="成本项" DataField="TypeID" Width="0" Align="Center"
                                                Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="成本项" DataField="TypeName" Width="0" Align="Center"
                                                Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="CostPreAmount" DataField="CostPreAmount" Width="0"
                                                Align="Right" FormatString="#,##0.00" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="PNTotal" DataField="PNTotal" Width="0" FormatString="#,##0.00"
                                                Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="TypeTotalAmount" DataField="TypeTotalAmount"
                                                Width="0" Align="Right" FormatString="#,##0.00" Visible="false" />
                                             <ComponentArt:GridColumn HeadingText="占用金额" DataField="OrderTotal" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="PR总额" DataField="AppAmount" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="已付总额" DataField="PaidAmount" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="未付总额" DataField="UNPaidAmount" Width="80"
                                                FormatString="#,##0.00" Align="Right" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="GroupByTemplate2">
                                        <span>## getTitle(DataItem, "TypeID") ##</span>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="GroupByTemplate3">
                                        <span>## getPNTitle(DataItem, "PrNo") ##</span>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer><br />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnReturn" OnClick="btnReturn_Click" Text="关闭" OnClientClick="window.close();"
                    CssClass="widebuttons" />
            </td>
        </tr>
    </table>
</asp:Content>
