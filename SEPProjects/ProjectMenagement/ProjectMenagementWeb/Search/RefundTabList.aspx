<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="RefundTabList.aspx.cs" Inherits="FinanceWeb.Search.RefundTabList" %>


<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/ProjectTab.ascx" TagName="tab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/DatePicker.js"></script>

    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
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
                                    <td class="oddrow" style="width: 15%">付款状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Text="请选择" Selected="True" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="待业务审批" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="待财务预审" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="待财务复审" Value="110"></asp:ListItem>
                                            <asp:ListItem Text="待财务终审" Value="120"></asp:ListItem>
                                            <asp:ListItem Text="待冲销" Value="136"></asp:ListItem>
                                            <asp:ListItem Text="已付款" Value="140"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">费用所属组:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_OnClick"
                                            CssClass="widebuttons" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="oddrow">
                                        <ComponentArt:Grid ID="GridRefund" OnItemDataBound="GridRefund_ItemDataBound" CallbackCachingEnabled="true"
                                            CallbackCacheSize="60" RunningMode="Callback" AllowTextSelection="true" DataAreaCssClass="GridData"
                                            EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PageSize="20"
                                            PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                            PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                            SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                            ManualPaging="true" PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                            TreeLineImageWidth="11" TreeLineImageHeight="11" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="Id" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    AllowGrouping="false" SortImageHeight="19">
                                                    <Columns>
                                                         <ComponentArt:GridColumn HeadingText="Id" DataField="Id"  Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" DataField="Status"  Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="PR No." DataField="PRNO" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="退款单号" DataField="RefundCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="退款金额" DataField="Amounts" Align="Center" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="退款日期" DataField="RefundDate" Align="Center" FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="StatusName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="审批状态" DataField="ViewAudit" Width="30" Align="Center" />
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

    <script language="javascript" type="text/javascript">
        (function () {
            var s = "<% = GridRefund.ClientID %>";
            var func = window["ComponentArt_Init_" + s];

            function modify() {
                var grid = window[s];
                if (!grid.GetProperty)
                    return;

                var property = grid.GetProperty('CallbackPrefix');
                if (property.indexOf("localhost", 0) >= 0) {
                    property = property.replace("localhost", "127.0.0.1");
                    grid.SetProperty('CallbackPrefix', property);
                }
            }

            window["ComponentArt_Init_" + s] = function () {
                func();
                modify();
            }
        })();
    </script>

</asp:Content>
