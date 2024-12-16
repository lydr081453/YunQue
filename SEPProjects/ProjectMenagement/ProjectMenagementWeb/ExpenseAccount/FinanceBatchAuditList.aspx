<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditList.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditList" Theme="" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/gridStyle2.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }



    </script>

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
                        <td>批 次 号:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                <contenttemplate>
                        <uc1:tabcontainer id="TabContainer1" runat="server" cssclass="AjaxTabStrip" width="98%">
        <uc1:TabPanel ID="TabPanel1" HeaderText="待审批项" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                     <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="GridNoNeed" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  GroupingMode="ConstantGroups"
                                GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"  
                                EditOnClickSelectedItem="false" FooterCssClass="GridFooter"
                                FooterHeight="40" HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="审核列表" PagerInfoClientTemplateId="ClientTemplate3" PagerStyle="Slider"
                                PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                                SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35"
                                ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                                TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11" PageSize="200"  ShowHeader="false"
                                PreExpandOnGroup="true" Width="100%" Height="100%" runat="server" >
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="BatchID" ShowTableHeading="false" TableHeadingCssClass="GridHeader" ShowSelectorCells="false"
                                        RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"  
                                        SortImageHeight="5">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="BatchID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="CreatorID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="批次号" DataField="PurchaseBatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="银行凭证" DataField="BatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="总金额" DataField="TotalPreFee" Align="Center" FormatString="0.00"/>
                                            <ComponentArt:GridColumn HeadingText="批次创建人" DataCellServerTemplateId="createrTemplate" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="创建日期" DataField="CreateDate" Align="Center" FormatString="yyyy-MM-dd" />
                                            <ComponentArt:GridColumn HeadingText="批次中单据数量" DataField="ReturnCount" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="审批" DataCellServerTemplateId="auditTemplate" Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
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
                                    
                                    <ComponentArt:ClientTemplate ID="PostFooterTemplate">
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
                                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                        
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ServerTemplates> 
                                    <ComponentArt:GridServerTemplate ID="createrTemplate">
                                        <Template>
                                            <%#getEmpName(Container.DataItem["CreatorID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="auditTemplate">
                                        <Template>
                                            <%#getUrl(Container.DataItem["BatchID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates> 
                            </ComponentArt:Grid>
                        </td>
                    </tr>    
                </table>
                </ContentTemplate>
        </uc1:TabPanel>
          <uc1:TabPanel ID="TabPanel2" HeaderText="已审批项" runat="server">
            <ContentTemplate>
             <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                     <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="gdAudited" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  GroupingMode="ConstantGroups"
                                GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"  
                                EditOnClickSelectedItem="false" FooterCssClass="GridFooter"
                                FooterHeight="40" HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="已审批项" PagerInfoClientTemplateId="ClientTemplate4" PagerStyle="Slider"
                                PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                                SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35"
                                ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                                TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11" PageSize="200"  ShowHeader="false"
                                PreExpandOnGroup="true" Width="100%" Height="100%" runat="server" >
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="BatchID" ShowTableHeading="false" TableHeadingCssClass="GridHeader" ShowSelectorCells="false"
                                        RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"  
                                        SortImageHeight="5">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="BatchID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="CreatorID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="批次号" DataField="PurchaseBatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="银行凭证" DataField="BatchCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="总金额" DataField="TotalPreFee" Align="Center" FormatString="0.00"/>
                                            <ComponentArt:GridColumn HeadingText="批次创建人" DataCellServerTemplateId="createrTemplate2" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="创建日期" DataField="CreateDate" Align="Center" FormatString="yyyy-MM-dd" />
                                            <ComponentArt:GridColumn HeadingText="批次中单据数量" DataField="ReturnCount" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="查看" DataCellServerTemplateId="AuditTemplate2" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="凭证导出" DataCellServerTemplateId="FinanceTmp" Align="Center" />
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
                                            <%#getUrl2(Container.DataItem["BatchID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                    <ComponentArt:GridServerTemplate ID="FinanceTmp">
                                        <Template>
                                            <%#getFinanceUrl(Container.DataItem["BatchID"].ToString())%>
                                        </Template>
                                    </ComponentArt:GridServerTemplate>
                                </ServerTemplates> 
                            </ComponentArt:Grid>
                        </td>
                    </tr>    
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:tabcontainer>
                    </contenttemplate>
            </td>
        </tr>
    </table>
</asp:Content>
