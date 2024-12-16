<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TicketAuditList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.TicketAuditList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <uc1:AuditTab id="AuditTab" runat="server" TabIndex="8" />
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
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
                            关 键 字:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />
                        </td>
                          <td align="left">
                            <asp:ImageButton ImageUrl="/images/t2_03-07.jpg" ID="btnSearch" OnClick="btnSearch_Click"
                                Width="56" Height="24" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
            <table width="100%" border="1" cellpadding="0" cellspacing="0">
                <tr>
                    <td >
                        <ComponentArt:Grid ID="GridNoNeed" GroupingPageSize="10" GroupingMode="ConstantGroups" EditOnClickSelectedItem="false" CallbackCachingEnabled="true"  CallbackCacheSize="60"  RunningMode="Callback"
                                            GroupByTextCssClass="txt" GroupBySectionCssClass="grp" AllowTextSelection="true"  AutoPostBackOnDelete="true"
                                            GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" EnableViewState="true" 
                                            ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Slider"
                                            PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                            PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                            SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
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
                                        <ComponentArt:GridColumn DataField="WorkItemID" Visible="false" />
                                        <ComponentArt:GridColumn DataField="WebPage" Visible="false" />
                                        <ComponentArt:GridColumn DataField="confirmFee" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                        <ComponentArt:GridColumn DataField="WorkMonth" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ExpenseCommitDeadLine" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                        <ComponentArt:GridColumn HeadingText="审批角色" DataField="ParticipantName" Align="Center" Width="60" />
                                        <ComponentArt:GridColumn HeadingText="申请单号" DataCellServerTemplateId="ReturnCodeTemplate" Align="Center" Width="90" />
                                            <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" Width="115" />
                                            <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center" Width="60" />
                                            <ComponentArt:GridColumn HeadingText="申请金额" DataField="PreFee" Align="Right" FormatString="0.00" Width="80" />
                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" Width="70" />
                                            <ComponentArt:GridColumn HeadingText="提交日期" DataField="CommitDate" Align="Center"
                                                FormatString="yyyy-MM-dd" Width="80" />
                                            <ComponentArt:GridColumn HeadingText="审批" DataCellServerTemplateId="auditTemplate"
                                                Align="Center" Width="25" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                             <ServerTemplates>
                                    <ComponentArt:GridServerTemplate ID="auditTemplate">
                                        <Template>
                                            <%#getUrl(Container.DataItem["WebPage"].ToString(),Container.DataItem["WorkItemID"].ToString(),Container.DataItem["WorkMonth"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                     <ComponentArt:GridServerTemplate ID="ReturnCodeTemplate">
                                        <Template>
                                            <%# getReturnCode(int.Parse(Container.DataItem["ReturnID"].ToString()))%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
</asp:Content>
