<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="SinglePrjView.aspx.cs" Inherits="FinanceWeb.CostView.SinglePrjView" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page */ /* Default tab */

        .AjaxTabStrip .ajax__tab_tab {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */ .AjaxTabStrip .ajax__tab_hover .ajax__tab_tab {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */ .AjaxTabStrip .ajax__tab_active .ajax__tab_tab {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */ .AjaxTabStrip .ajax__tab_body {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }

        .border {
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

        .border2 {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border_title_left {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_title_right {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_datalist {
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
                sum += row.GetMember("TypeName").Value + space + "预算总额:" + row.GetMember("CostPreAmount").Value + space + "已申请总额:" + row.GetMember("TypeTotalAmount").Value;
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
                sum += row.GetMember("PrNo").Value + space + "申请人:" + row.GetMember("Requestor").Value + space + "申请金额:" + row.GetMember("PNTotal").Value;
            }
            return sum;
        }
        $(document).ready(function () {
            var r = $("#<%= raList.ClientID %> input").click(function () {
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
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%"
        EnableViewState="false" ActiveTabIndex="0">
        <uc1:TabPanel ID="TabPanel1" HeaderText="项目主信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">项目准备信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">确认项目号:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblProjectCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">项目流水:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblSerialCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">负责人:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">合同状态:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblContactStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">相关BD项目号:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblBDProject" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">业务类型:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblBizType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">项目来自合资方：
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblFromJoint" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">项目类型：
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblProjectType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">项目组别:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblGroup" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">项目名称:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="lblBizDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="4">合同信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">公司选择:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="txtBranchName" />
                        </td>

                    </tr>
                    <tr>
                        <td class="oddrow">项目总金额:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="txtTotalAmount" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow">合同税率:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="txtTaxRate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">不含增值税金额:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblTotalNoVAT" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow">附加税（主申请方）:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblTaxFee" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">支持方合计:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblTotalSupporter" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow">附加税（支持方）:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblTaxSupporter" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">合同服务费:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblServiceFee" runat="server"></asp:Label>
                        </td>
                        <td class="oddrow">项目毛利率（%）:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lblProfileRate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">成本合计:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblCostTot"></asp:Label>
                        </td>
                        <td class="oddrow">使用成本:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblUsedCost"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">剩余成本:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblBalanceCost"></asp:Label>
                        </td>
                        <td class="oddrow">媒体返点:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblMediaRebate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">业务起始日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="txtBeginDate" onkeyDown="return false; " Style="cursor: pointer;" runat="server" />
                        </td>
                        <td class="oddrow">已付金额:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblPaid"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">预计结束日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="txtEndDate" onkeyDown="return false; " Style="cursor: pointer;" runat="server" />&nbsp;
                        </td>
                        <td class="oddrow">回款金额:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label runat="server" ID="lblPaymentFee"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="4">成本明细信息
                        </td>
                    </tr>
                    <tr id="trNoRecord" runat="server" visible="false">
                        <td colspan="4">
                            <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                                <tr class="Gheading" align="center">
                                    <th scope="col">序号
                                    </th>
                                    <th scope="col">成本描述
                                    </th>
                                    <th scope="col">成本金额
                                    </th>
                                </tr>
                                <tr class="td" align="left">
                                    <td colspan="3" align="center">
                                        <span>暂时没有相应的成本记录</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trGrid">
                        <td colspan="4">
                            <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractCostID"
                                OnRowDataBound="gvCost_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="45%" />
                                    <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCost" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="使用金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsedCost" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                            <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectExpenseID"
                                Width="100%" OnRowDataBound="gvExpense_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="45%" />
                                    <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpense" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="使用金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsedCost" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr runat="server" id="trTotal">
                        <td class="oddrow-l" colspan="4" align="right">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 5%; border: 0 0 0 0"></td>
                                    <td style="width: 70%; border: 0 0 0 0"></td>
                                    <td style="width: 25%; border: 0 0 0 0" align="right">
                                        <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="4">支持方信息
                        </td>
                    </tr>
                    <tr id="trNoSupporter" runat="server" visible="false">
                        <td colspan="4">
                            <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                                <tr class="Gheading" align="center">
                                    <th scope="col">序号
                                    </th>
                                    <th scope="col">支持方
                                    </th>
                                    <th scope="col">支持方负责人
                                    </th>
                                    <th scope="col">支持方费用
                                    </th>
                                    <th scope="col">不含增值税金额
                                    </th>
                                    <th scope="col">附加税
                                    </th>
                                    <th scope="col">服务类型
                                    </th>
                                    <th scope="col">业务描述
                                    </th>
                                </tr>
                                <tr class="td" align="left">
                                    <td colspan="8" align="center">
                                        <span>暂时没有相应的支持方记录</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trSupporterGrid">
                        <td colspan="4">
                            <asp:GridView ID="gvSupporter" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                                OnRowDataBound="gvSupporter_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="SupportID" HeaderText="支持方ID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="GroupID" HeaderText="GroupID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="支持方" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LeaderUserID" HeaderText="LeaderUserID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("LeaderEmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="支持方费用" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudgetAllocation" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="不含增值税金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudgetNoVAT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附加税" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxVAT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ServiceType" HeaderText="服务类型" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="ServiceDescription" HeaderText="业务描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%" />
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="第三方费用明细" runat="server">
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
                                    <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
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
                                            <ComponentArt:GridColumn HeadingText="已付总额" DataField="PaidAmount" Width="80" Align="Right"
                                                FormatString="#,##0.00" />
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
                            <asp:RadioButtonList runat="server" ID="raList" RepeatDirection="Horizontal">
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
                                            <ComponentArt:GridColumn HeadingText="PNTotal" DataField="PNTotal" Width="0" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="TypeTotalAmount" DataField="TypeTotalAmount"
                                                Width="0" Align="Right" FormatString="#,##0.00" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="申请金额" DataField="AppAmount" Width="100" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="已付金额" DataField="PaidAmount" Width="100" Align="Right"
                                                FormatString="#,##0.00" />
                                            <ComponentArt:GridColumn HeadingText="未付金额" DataField="UNPaidAmount" Width="100"
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
        <uc1:TabPanel ID="TabPanel5" HeaderText="消耗信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 5%">消耗小计:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="lblConsumptionTotal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvConsumption" runat="server" OnRowDataBound="gvConsumption_RowDataBound" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="序号" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="OrderYM" HeaderText="年月" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                      <asp:BoundField DataField="PurchaseBatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="JSCode" HeaderText="JSCode" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="XMCode" HeaderText="XMCode" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Media" HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="OrderType" HeaderText="类别" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BatchId" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel10" HeaderText="媒体返点信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 5%">媒体返点小计:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="labRebateRegistrationTotal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvRebateRegistration" runat="server" AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CreditedDate" HeaderText="日期" ItemStyle-Width="5%" />
                                      <asp:BoundField DataField="PurchaseBatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#  Eval("Supplier") == null ? "" : ((ESP.Purchase.Entity.SupplierInfo)Eval("Supplier")).supplier_name %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="RebateAmount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                                           <asp:BoundField DataField="Remark" HeaderText="返点内容" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="AccountingNum" HeaderText="返点核算信息编号" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="SettleType" HeaderText="结算类型" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="Branch" HeaderText="我方主体名称" ItemStyle-HorizontalAlign="Center"
                 />
                                    <asp:BoundField DataField="BatchId" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel4" HeaderText="退款申请" runat="server">
             <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 5%">退款申请小计:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="lblRefundTotal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvRefund" runat="server" OnRowDataBound="gvRefund_RowDataBound" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="序号" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                      <asp:BoundField DataField="RefundCode" HeaderText="退款单号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="PRNO" HeaderText="采购单号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Remark" HeaderText="退款说明" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Amounts" HeaderText="金额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="15%" />
                                      <asp:TemplateField HeaderText="成本项" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCost" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer><br />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnReturn" OnClientClick="window.close();" Text=" 关闭 "
                    CssClass="widebuttons" />
            </td>
        </tr>
    </table>
</asp:Content>
