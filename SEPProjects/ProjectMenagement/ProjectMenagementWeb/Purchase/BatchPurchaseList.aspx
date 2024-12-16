<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="BatchPurchaseList.aspx.cs" Inherits="Purchase_BatchPurchaseList"
    EnableEventValidation="false" %>

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
        document.onkeydown = function(evt) {
            if (!evt) evt = event;
            if (evt.keyCode == 13) {
                document.getElementById("<% =btnSearch.ClientID %>").click();
                return false;
            }
        }

        $().ready(function() {
            $("#<%=ddlBranch.ClientID %>").empty();
            Purchase_BatchPurchaseList.GetBranchList(initBranch);
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
                            <ComponentArt:Grid ID="grWait" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                                GroupByTextCssClass="txt" GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt"
                                DataAreaCssClass="GridData" EnableViewState="false" ShowHeader="false" FooterCssClass="GridFooter"
                                PageSize="20" PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
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
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                        GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                        <Columns>
                                            <ComponentArt:GridColumn HeadingText="批次号" DataField="PurchaseBatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="公司代码" DataField="BranchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="付款总额" Align="Center" DataCellServerTemplateId="AmountsT" />
                                            <ComponentArt:GridColumn HeadingText="创建人" Align="Center" DataCellServerTemplateId="RequestorT"
                                                DataField="CreatorID" />
                                            <ComponentArt:GridColumn HeadingText="提交日期" Align="Center" DataField="CreateDate"
                                                FormatString="yyyy-MM-dd hh:ss:mm" />
                                            <ComponentArt:GridColumn HeadingText="付款状态" Align="Center" DataCellServerTemplateId="StatusT"
                                                DataField="Status" />
                                            <ComponentArt:GridColumn HeadingText="批次打印" Align="Center" DataField="BatchID" DataCellServerTemplateId="PrintT" />
                                            <ComponentArt:GridColumn HeadingText="审批" Align="Center" DataCellServerTemplateId="AuditT"
                                                DataField="BatchID" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="AmountsT">
                                        <Template>
                                            <%# ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(((ESP.Finance.Entity.PNBatchInfo)Container.DataItem.DataItem)).ToString("#,##0.00")%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="RequestorT">
                                        <Template>
                                            <%# getUser(int.Parse(Container.DataItem["CreatorID"].ToString()))%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="StatusT">
                                        <Template>
                                            <%# ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(Container.DataItem["Status"].ToString()),0,null) %>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="PrintT">
                                        <Template>
                                            <asp:HyperLink ID="hylPrint" Target="_blank" NavigateUrl='<%# "Print/PNPrintForPurchaseBatch.aspx?" + ESP.Finance.Utility.RequestName.BatchID+"="+Container.DataItem["BatchID"].ToString()  %>'
                                                runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="批次打印" Width="4%"></asp:HyperLink>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="AuditT">
                                        <Template>
                                            <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="审批"
                                                NavigateUrl='<%# "BatchPurchase.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + Container.DataItem["BatchID"].ToString()%>'
                                                Width="5%"></asp:HyperLink>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates>
                            </ComponentArt:Grid>
                            <table width="100%" class="XTable">
                                <tr>
                                    <td align="left">
                                        <b>批次总额：<asp:Literal ID="litPZ1" runat="server" />&nbsp;&nbsp;PN条数：<asp:Literal ID="litPN1"
                                            runat="server" /></b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
