<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"
    CodeBehind="BatchPurchasePaid.aspx.cs" Inherits="Purchase_BatchPurchasePaid" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
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

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        $().ready(function() {
            $("#<%=ddlBranch.ClientID %>").empty();
            Purchase_BatchPurchasePaid.GetBranchList(initBranch);
            function initBranch(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidBranchID.ClientID %>").val()) {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
                }
            }
        });
        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchID.ClientID %>").value = "";
                document.getElementById("<% =hidBranchName.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchID.ClientID %>").value = id;
                document.getElementById("<% =hidBranchName.ClientID %>").value = text;
            }
        }

        function getTitle(groupItem) {
            var rows = groupItem.Rows;
            var sum = 0;
            var row;
            var count = 0;
            var space = "&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            for (i = 0; i < rows.length; i++) {
                row = groupItem.Grid.Table.GetRow(rows[i]);
                sum += row.GetMember(6).Value;
                count += row.GetMember(12).Value;
            }

            return groupItem.Grid.Table.GetRow(rows[0]).GetMember("CreateYearMonth").Value + space + "总金额:" + sum.toFixed(2) + space + "PN条数:" + count;
        }
    </script>

    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
      <table style="width: 100%">
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
                                        关键字（申请单号、供应商名称）:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        公司选择:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                                        </asp:DropDownList>
                                        <input type="hidden" id="hidBranchID" runat="server" />
                                        <input type="hidden" id="hidBranchName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        提交日期:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                                        --
                                        <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                                    </td>
                                    <td class="oddrow-l" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
                                    </td>
                                </tr>
                            </table>
                            <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="100%">
                                <uc1:TabPanel ID="TabPanel1" HeaderText="采购审批中" runat="server">
                                    <ContentTemplate>
                                        <ComponentArt:Grid ID="grAuditing" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                                            OnItemDataBound="grAuditing_ItemDataBound" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="false"
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Buttons"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn HeadingText="BatchID" DataField="BatchID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="CreatorID" Align="Center" DataField="Creator"
                                                            Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" Align="Center" DataField="Status" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="批次号" DataField="PurchaseBatchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="公司代码" DataField="BranchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="付款总额" Align="Center" DataField="Amounts" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="创建人" Align="Center" DataField="CreatorName" />
                                                        <ComponentArt:GridColumn HeadingText="提交日期" Align="Center" DataField="CreateDate"
                                                            FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataField="StatusName" />
                                                        <ComponentArt:GridColumn HeadingText="批次打印" Align="Center" DataField="BatchPrint" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                        </ComponentArt:Grid>
                                        <table width="100%" class="XTable">
                                            <tr>
                                                <td align="left">
                                                    <b>批次总额：<asp:Literal ID="litPZ2" runat="server" />&nbsp;&nbsp;PN条数：<asp:Literal ID="litPN2"
                                                        runat="server" /></b>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </uc1:TabPanel>
                                <uc1:TabPanel ID="TabPanel2" HeaderText="财务处理中" runat="server">
                                    <ContentTemplate>
                                        <ComponentArt:Grid ID="grFinance" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                                            OnItemDataBound="grFinance_ItemDataBound" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="false"
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Buttons"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn HeadingText="BatchID" DataField="BatchID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="CreatorID" Align="Center" DataField="Creator"
                                                            Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" Align="Center" DataField="Status" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="批次编号" DataField="PurchaseBatchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="公司代码" DataField="BranchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="付款总额" Align="Center" DataField="Amounts" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="创建人" Align="Center" DataField="CreatorName" />
                                                        <ComponentArt:GridColumn HeadingText="提交日期" Align="Center" DataField="CreateDate"
                                                            FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataField="StatusName" />
                                                        <ComponentArt:GridColumn HeadingText="批次打印" Align="Center" DataField="BatchPrint" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                        </ComponentArt:Grid>
                                        <table width="100%" class="XTable">
                                            <tr>
                                                <td align="left">
                                                    <b>批次总额：<asp:Literal ID="litPZ3" runat="server" />&nbsp;&nbsp;PN条数：<asp:Literal ID="litPN3"
                                                        runat="server" /></b>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </uc1:TabPanel>
                             <uc1:TabPanel ID="TabPanel3" HeaderText="已付款" runat="server">
                                    <ContentTemplate>
                                        <ComponentArt:Grid ID="grComplete" GroupBy="CreateYearMonth DESC" GroupingPageSize="10"
                                            OnItemDataBound="grComplete_ItemDataBound" GroupingMode="ConstantGroups" GroupByTextCssClass="txt"
                                            GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                                            EnableViewState="false" ShowHeader="false" FooterCssClass="GridFooter" PageSize="20"
                                            PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                            PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                            SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                            PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                            TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="98%"
                                            Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" HeadingTextCssClass="HeadingCellText"
                                                    SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageWidth="10" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn HeadingText="BatchID" DataField="BatchID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="CreatorID" Align="Center" DataField="Creator"
                                                            Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" Align="Center" DataField="Status" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="批次编号" DataField="PurchaseBatchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="公司代码" DataField="BranchCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="付款总额" Align="Center" DataField="Amounts" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="创建人" Align="Center" DataField="CreatorName" />
                                                        <ComponentArt:GridColumn HeadingText="提交日期" Align="Center" DataField="CreateDate"
                                                            FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataField="StatusName" />
                                                        <ComponentArt:GridColumn HeadingText="批次打印" Align="Center" DataField="BatchPrint" />
                                                        <ComponentArt:GridColumn HeadingText="申请时间" DataField="CreateYearMonth" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="PnCount" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="重汇" Align="Center" DataField="RePayment" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="GroupByTemplate">
                                                    <span>## getTitle(DataItem) ##</span>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                        </ComponentArt:Grid>
                                    </ContentTemplate>
                                </uc1:TabPanel>
                            </uc1:TabContainer>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
