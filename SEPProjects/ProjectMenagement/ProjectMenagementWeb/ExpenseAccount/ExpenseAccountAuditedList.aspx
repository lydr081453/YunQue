<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ExpenseAccountAuditedList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.ExpenseAccountAuditedList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/AuditTab.ascx" TagName="AuditTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/gridStyle2.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
     
    </script>

    <uc1:AuditTab id="AuditTab" runat="server" TabIndex="6" />
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
                            关 键 字:
                        </td>
                        <td>
                            <asp:TextBox ID="txtKey" runat="server" />
                        </td>
                        <td>
                            提交日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                            --
                            <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            公司选择:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                            </asp:DropDownList>
                        </td>
                        <td>
                            报销类型:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlType">
                                <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="常规报销" Value="30"></asp:ListItem>
                                <asp:ListItem Text="第三方报销" Value="35"></asp:ListItem>
                                <asp:ListItem Text="商务卡报销" Value="33"></asp:ListItem>
                                <asp:ListItem Text="现金借款" Value="32"></asp:ListItem>
                                <asp:ListItem Text="支票/电汇付款" Value="31"></asp:ListItem>
                                <asp:ListItem Text="行政报销" Value="37"></asp:ListItem>
                                <asp:ListItem Text="现金冲销" Value="36"></asp:ListItem>
                                <asp:ListItem Text="PR现金借款" Value="34"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ImageButton ImageUrl="/images/t2_03-07.jpg" ID="btnSearch" OnClick="btnSearch_Click"
                                Width="56" Height="24" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="abc" border="0" cellspacing="0" cellpadding="0" style="border-bottom: solid 1px #15428b;">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                    id="Table1">
                    <tr>
                        <td>
                            <asp:LinkButton ID="Tab1" runat="server" Text=" 待审批 " OnClick="tab1_click" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="button_on" runat="server"
                    id="Table2">
                    <tr>
                        <td>
                            <asp:LinkButton ID="Tab2" runat="server" Text=" 已审批 " />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
        <tr>
            <td class="td">
                <ComponentArt:Grid ID="GridHist" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  AllowTextSelection="true" CallbackCachingEnabled="true" CallbackCacheSize="60" RunningMode="Callback"
                    GroupingMode="ConstantGroups" GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"
                    
                    EditOnClickSelectedItem="false" FooterCssClass="GridFooter" FooterHeight="10"
                    HeaderHeight="10" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                    GroupingNotificationText="已审批项列表" PagerStyle="Numbered" PagerTextCssClass="GridFooterText"
                    PagerInfoPosition="BottomLeft" PagerPosition="BottomRight" SliderHeight="20"
                    SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35" ImagesBaseUrl="images/gridview2/"
                    PagerImagesFolderUrl="images/gridview2/pager/" TreeLineImagesFolderUrl="images/gridview2/lines/"
                    TreeLineImageWidth="11" TreeLineImageHeight="11" PageSize="200" ShowHeader="false"
                    PreExpandOnGroup="true" Width="100%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ReturnID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                            DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                            GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="5">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="报销金额" DataField="PreFee" Align="Right" FormatString="0.00" />
                                <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="提交日期" DataField="CommitDate" Align="Center"
                                    FormatString="yyyy-MM-dd" />
                                <ComponentArt:GridColumn HeadingText="审批日期" DataField="LastAuditPassTime" Align="Center"
                                    FormatString="yyyy-MM-dd" />
                                <ComponentArt:GridColumn HeadingText="单据类型" DataField="ReturnTypeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate2"
                                    Align="Center" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="printTemplate2">
                            <Template>
                                <%#getPrintUrl(Container.DataItem["ReturnID"].ToString(),Container.DataItem["ReturnType"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                </ComponentArt:Grid>
            </td>
        </tr>
    </table>
  </asp:Content>
