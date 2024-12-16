<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="OOPTabList.aspx.cs" Inherits="FinanceWeb.project.OOPTabList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/ProjectTab.ascx" TagName="tab" TagPrefix="uc1" %>
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
                <uc1:tab ID="tab" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">报销状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlStatus">
                                            <asp:ListItem Text="请选择.." Value="0"></asp:ListItem>
                                            <asp:ListItem Text="驳回" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="业务审核中" Value="2,100,101"></asp:ListItem>
                                             <asp:ListItem Text="业务审核通过" Value="102"></asp:ListItem>
                                            <asp:ListItem Text="财务审核" Value="103,104,110,120"></asp:ListItem>
                                            <asp:ListItem Text="待冲销" Value="136"></asp:ListItem>
                                            <asp:ListItem Text="冲销中" Value="137,138"></asp:ListItem>
                                            <asp:ListItem Text="已冲销" Value="139"></asp:ListItem>
                                            <asp:ListItem Text="已付款" Value="140"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <tr>
                                        <td class="oddrow" style="width: 15%">报销类型:
                                        </td>
                                        <td class="oddrow-l" style="width: 35%">
                                            <asp:DropDownList runat="server" ID="ddlType">
                                                <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="常规报销" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="第三方报销" Value="35"></asp:ListItem>
                                                <asp:ListItem Text="商务卡报销" Value="33"></asp:ListItem>
                                                <asp:ListItem Text="现金借款" Value="32"></asp:ListItem>
                                                <asp:ListItem Text="支票/电汇付款" Value="31"></asp:ListItem>
                                                <asp:ListItem Text="媒体预付申请" Value="37"></asp:ListItem>
                                                <asp:ListItem Text="现金冲销" Value="36"></asp:ListItem>
                                                <asp:ListItem Text="PR现金借款" Value="34"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td class="oddrow">费用所属组:
                                        </td>
                                        <td class="oddrow-l">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                            <asp:DropDownList ID="ddlDepartment1" Style="width: 100px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment1_SelectedIndexChanged" />
                                            <asp:DropDownList ID="ddlDepartment2" runat="server" Style="width: 100px;" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment2_SelectedIndexChanged" />
                                            <asp:DropDownList ID="ddlDepartment3" runat="server" Style="width: 100px;" />
                                                                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                <tr>
                                    <td class="oddrow">项目号:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtProjectCode" runat="server" />
                                    </td>
                                    <td class="oddrow">申请人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtRequestEmployeeName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">申请日期:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtBegin" onclick="setDate(this);" runat="server" />-<asp:TextBox ID="txtEnd" onclick="setDate(this);" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="oddrow-l">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="oddrow-l" style="text-align:left;padding-bottom:5px;font-size:15px;">报销总金额：<asp:Label ID="labTotal" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <ComponentArt:Grid ID="GridProject" GroupingPageSize="10" OnItemDataBound="GridProject_ItemDataBound" CallbackCachingEnabled="true"
                                            CallbackCacheSize="60" RunningMode="Callback" AllowTextSelection="true" DataAreaCssClass="GridData"
                                            GroupingMode="ConstantGroups" GroupByTextCssClass="txt" GroupBySectionCssClass="grp"
                                            EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PageSize="20"
                                            PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                            PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                            SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                            ManualPaging="true" PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                            TreeLineImageWidth="11" TreeLineImageHeight="11" PreExpandOnGroup="false" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ReturnID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn HeadingText="报销单号" DataField="ReturnCode" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Width="120" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Width="200"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Align="Center" Width="100"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Width="100"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="申请日期" DataField="RequestDate" Width="100" Align="Center"
                                                            FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="报销类型" DataField="TypeName" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="StatusText" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="审批状态" DataField="Auditor" Width="50" Align="Center" />
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
            <td style="height: 10px">&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
