<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ProjectTabList.aspx.cs" Inherits="project_ProjectTabList" %>

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

        function abc(obj, status) {
            //只有提交状态的项目可以撤销
            if (status != "1") {
                obj.style.display = "none";
                obj.style.visibility = "hidden";
                
            }
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
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        项目状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Text="请选择" Selected="True" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                                             <asp:ListItem Text="业务审批中" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="风控审批中" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="财务审批中" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="已审批" Value="32"></asp:ListItem>
                                            <asp:ListItem Text="预关闭" Value="33"></asp:ListItem>
                                            <asp:ListItem Text="已关闭" Value="34"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_OnClick" CssClass="widebuttons" />
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
                                   <%-- CallbackPrefix="http://127.0.0.1:11002/Search/ProjectTabList.aspx?Cart_ctl00_ContentPlaceHolder1_GridProject_Callback=yes"--%>
                                        <ComponentArt:Grid ID="GridProject" GroupingPageSize="10" GroupingMode="ConstantGroups" EditOnClickSelectedItem="false"  CallbackCachingEnabled="true"  CallbackCacheSize="60"  RunningMode="Callback"
                                            GroupByTextCssClass="txt" GroupBySectionCssClass="grp" AllowTextSelection="true"  OnItemDataBound="GridProject_ItemDataBound" OnNeedDataSource="GridProject_NeedDataSource"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true"  AutoPostBackOnDelete="true"
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                                            TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                            PreExpandOnGroup="false" Width="100%" Height="100%" runat="server" >
                                            <Levels>
                                                <ComponentArt:GridLevel ShowTableHeading="false" DataKeyField="ProjectID" TableHeadingCssClass="GridHeader" AllowSorting="true"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ProjectID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Status" DataField="Status" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="GroupID" DataField="GroupID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="Step" DataField="Step" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="流水号" DataField="SerialCode"  Align="Center" />
                                                      <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode"  Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目名称" DataField="BusinessDescription" Width="100"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="负责人" DataField="ApplicantEmployeeName" Width="80"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="业务组别" DataField="GroupName" Width="100" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="总金额" DataField="TotalAmount"  Align="Center"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="StatusText"  Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="查看" DataField="View"  Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="审批状态" DataField="ViewAudit" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="变更" DataField="Change" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="证据链" DataField="Contract" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="发票" DataField="ApplyForInvioce" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="历史" DataField="History" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="撤销" DataField="Cancel" Align="Center" DataCellClientTemplateId="CancelT" />                                                       
                                                        <ComponentArt:GridColumn HeadingText="成本" DataField="CBX" Align="Center"/>
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="CancelT">
                                                    <a onclick="return confirm('您确定撤销该项目号吗？');" href="javascript:DeleteRow('## DataItem.ClientId ##');"><img src="/images/Icon_Cancel.gif" onload="abc(this,'## DataItem.GetMember('Status').Value ##');" title="撤销" border="0" /></a> 
                                                </ComponentArt:ClientTemplate>
                                              <%--  <ComponentArt:ClientTemplate ID="CBXT">
                                                    <a href="/CostView/SinglePrjView.aspx?ProjectID=## DataItem.GetMember('ProjectID').Value ##"  target="_blank">
                                                            <img src="../images/dc.gif" border="0px;" title="查看成本"></a>
                                                </ComponentArt:ClientTemplate>--%>
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
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
