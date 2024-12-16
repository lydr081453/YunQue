<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ReturnTabEdit.aspx.cs" Inherits="FinanceWeb.Edit.ReturnTabEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/EditTab.ascx" TagName="tab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function deleteRow(rowId) {
              GridReturn.deleteItem(GridReturn.getItemFromClientId(rowId));
          }
          function abc(obj, status) {
              //只有未提交状态的项目可以撤销
              if (status != "1") {
                  obj.style.visibility = "hidden";
                  obj.style.display = "none";
              }
          }
    </script>
    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <uc1:tab ID="tab" runat="server" TabIndex="2" />
            </td>
        </tr>
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
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtKey" runat="server" />
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="oddrow">
                                        <ComponentArt:Grid ID="GridReturn" GroupingPageSize="10" GroupingMode="ConstantGroups" EditOnClickSelectedItem="false"　 CallbackCachingEnabled="true"  CallbackCacheSize="60"  RunningMode="Callback"
                                            GroupByTextCssClass="txt" GroupBySectionCssClass="grp" AllowTextSelection="true"  AutoPostBackOnDelete="true"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true"
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel ShowTableHeading="false" DataKeyField="returnID" TableHeadingCssClass="GridHeader" AllowSorting="true"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn HeadingText="ReturnID" DataField="ReturnID"  Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="ReturnStatus" DataField="ReturnStatus"  Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="PR No." DataField="prNo" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="PN No." DataField="ReturnCode" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="负责人" DataField="RequestEmployeeName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" 
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="预付金额" DataField="PreFee" Align="Center" FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="付款日期" DataField="PReBeginDate" Align="Center" FormatString="yyyy-MM-dd" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Align="Center" />
                                                         <ComponentArt:GridColumn HeadingText="状态" DataField="ReturnStatusName" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="附件" DataField="Attach" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="导出" DataField="Export" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="编辑" DataField="Edit" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="撤销" Align="Center" DataField="Cancel" DataCellClientTemplateId="CancelT" />
                                                        <ComponentArt:GridColumn HeadingText="发票" DataField="Invoice" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="重汇" DataField="RePay" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="销账" DataField="Traffic" Width="30" Align="Center" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="CancelT">
                                                    <a onclick="return confirm('您是否确认撤销？');" href="javascript:deleteRow('## DataItem.ClientId ##')"><img  onload="abc(this,'## DataItem.GetMember('ReturnStatus').Value ##');" src='../../images/Icon_Cancel.gif' border='0' /></a>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                        </ComponentArt:Grid>
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
