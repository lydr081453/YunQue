<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Theme="" CodeBehind="ExpenseAccountAuditList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.ExpenseAccountAuditList" %>

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

        function checkedAll() {
            for (var i = 0; i < document.getElementsByName("chkAudit").length; i++) {
                var e = document.getElementsByName("chkAudit")[i];
                e.click();
            }
        }

        function accAdd(arg1, arg2) {
            var r1, r2, m;
            try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
            try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
            m = Math.pow(10, Math.max(r1, r2))
            return (arg1 * m + arg2 * m) / m
        }
        function accSub(arg1, arg2) {
            return accAdd(arg1, -arg2);
        }

        function CalSelected(checked, fee, returnID) {
            var total = 0;
            if (document.getElementById("<%=hidTotal.ClientID %>").value == "")
                total = "0";
            else
                total = document.getElementById("<%=hidTotal.ClientID %>").value;
            if (checked == true) {
                total = accAdd(total, fee);
                document.getElementById("<%=hidTotal.ClientID %>").value = total;
            }
            else {
                total = accSub(total, fee);
                document.getElementById("<%=hidTotal.ClientID %>").value = total;
            }
            document.getElementById("<%=lblTotal.ClientID %>").innerHTML = "Total:" + total;
        }
        function getUrl(webpage, workitemid, workMonth) {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "'><img src='images/Audit.gif' /></a>";
            if (workMonth != "1.本次处理") {
                return "";
            }
            return sum;
        }

        function submitMatter() {
            var hid = document.getElementById("<%= hidWorkItemID.ClientID %>");
            hid.value = "";
            var boxes = document.getElementsByName("chkAudit");
            for (var i = 0; i < boxes.length; i++) {
                var e = boxes[i];
                if (e.checked)
                    hid.value += e.value + ",";
            }
            if (hid.value == "") {
                alert("请选择要审批的单据！");
                return false;
            }
            else {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                return confirm("确认审批通过吗？");
            }
        }

        function ExportDetail() {
            var hid = document.getElementById("<%= hidWorkItemID.ClientID %>");
            hid.value = "";
            var boxes = document.getElementsByName("chkAudit");
            for (var i = 0; i < boxes.length; i++) {
                var e = boxes[i];
                if (e.checked)
                    hid.value += e.value + ",";
            }
            if (hid.value == "") {
                alert("请选择要导出的单据！");
                return false;
            }
            else {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                return true;
            }
        }

        function getTitle(groupItem) {
            var rows = groupItem.Rows;
            //            var sum = 0;
            //            var row;
            //            var count = 0;
            //            var space = "&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            //            for (i = 0; i < rows.length; i++) {
            //                row = groupItem.Grid.Table.GetRow(rows[i]);
            //                sum += row.GetMember(3).Value;
            //                count += row.GetMember(9).Value;
            //            }

            return groupItem.Grid.Table.GetRow(rows[0]).GetMember("WorkMonth").Value;
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
                                <asp:Button  runat="server" ID="btnSearch"  Text=" 检索 " OnClick="btnSearch_Click"/>
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
                <table border="0" cellpadding="0" cellspacing="0" class="button_on" runat="server"
                    id="Table1">
                    <tr>
                        <td>
                            <asp:LinkButton ID="Tab1" runat="server" Text=" 待审批 " />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                    id="Table2">
                    <tr>
                        <td>
                            <asp:LinkButton ID="Tab2" runat="server" Text=" 已审批 " OnClick="tab1_click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                   <%-- CallbackPrefix="http://127.0.0.1:11002/ExpenseAccount.ExpenseAccountAuditedList.aspx?ctl00_ContentPlaceHolder1_GridNoNeed_Callback=yes"--%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
        <tr>
            <td class="td">
                <ComponentArt:Grid ID="GridNoNeed" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  AllowTextSelection="true"

                    GroupingMode="ConstantGroups" GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"
                    EditOnClickSelectedItem="false" FooterCssClass="GridFooter" FooterHeight="40"
                    HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                    GroupingNotificationText="审核列表" PagerInfoClientTemplateId="ClientTemplate3" PagerStyle="Numbered"
                    PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                    SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35" OnItemDataBound="GridNoNeed_ItemDataBound"
                    ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                    TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                    PageSize="200" ShowHeader="false" PreExpandOnGroup="true" Width="100%" Height="100%" 
                    runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="WorkItemID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                            DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow" 
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                            GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="5">
                            <Columns>
                                <ComponentArt:GridColumn DataField="WorkItemID" Visible="false" />
                                <ComponentArt:GridColumn DataField="WebPage" Visible="false" />
                                <ComponentArt:GridColumn DataField="confirmFee" Visible="false" />
                                <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                <ComponentArt:GridColumn DataField="WorkMonth" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExpenseCommitDeadLine" Visible="false" />
                                <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="选择" DataCellServerTemplateId="checkTemplate"
                                    Align="Center" Width="25" />
                                <ComponentArt:GridColumn HeadingText="审批角色" DataField="ParticipantName" Align="Center"
                                    Width="60" />
                                <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center"
                                    Width="90" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center"
                                    Width="115" />
                                <ComponentArt:GridColumn HeadingText="项目名称" DataField="ReturnContent" Align="Center"
                                    Width="60" />
                                <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center"
                                    Width="60" />
                                <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Align="Right" FormatString="0.00"
                                    Width="80" />
                                <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center"
                                    Width="70" />
                                <ComponentArt:GridColumn HeadingText="提交日期" DataField="CommitDate" Align="Center"
                                    FormatString="yyyy-MM-dd" Width="80" />
                                <ComponentArt:GridColumn HeadingText="审批日期" DataField="LastAuditPassTime" Align="Center"
                                    FormatString="yyyy-MM-dd" Width="80" />
                                <ComponentArt:GridColumn HeadingText="单据类型" DataField="ReturnTypeName" Align="Center"
                                    Width="80" />
                                <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate" 
                                    Align="Center" Width="25" />
                                <ComponentArt:GridColumn HeadingText="审批" DataCellServerTemplateId="auditTemplate" 
                                    Align="Center" Width="25" />
                            </Columns>
                        </ComponentArt:GridLevel>
                        <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                            DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                            SortImageHeight="5">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="费用发生日期" DataField="ExpenseDate" FormatString="yyyy-MM-dd"
                                    Align="Center" />
                                <ComponentArt:GridColumn HeadingText="费用明细描述" DataField="ExpenseDesc" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="金额" DataField="ExpenseMoney" Align="Center"
                                    FormatString="#,##0.00" />
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
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <input type="checkbox" id="chkAll" onclick="checkedAll();" />全选
                                    </td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="GroupByTemplate">
                            <span>## getTitle(DataItem) ##</span>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="checkTemplate">
                            <Template>
                                <%#getIsBatchInput(Container.DataItem["ReturnID"].ToString(),Convert.ToDecimal(Container.DataItem["PreFee"] == DBNull.Value ? 0 : Container.DataItem["PreFee"]), Container.DataItem["WorkItemID"].ToString(), Convert.ToDecimal(Container.DataItem["confirmFee"] == DBNull.Value ? 0 : Container.DataItem["confirmFee"]), Container.DataItem["ReturnType"].ToString(), Container.DataItem["WorkMonth"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="auditTemplate">
                            <Template>
                                <%#getUrl(Container.DataItem["WebPage"].ToString(),Container.DataItem["WorkItemID"].ToString(),Container.DataItem["WorkMonth"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="printTemplate">
                            <Template>
                                <%#getPrintUrl(Container.DataItem["ReturnID"].ToString(),Container.DataItem["ReturnType"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                </ComponentArt:Grid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTotal"></asp:Label>
                <input type="hidden" runat="server" id="hidTotal" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:ImageButton ID="btnAuditAll" runat="server" OnClientClick="return submitMatter(DataItem);"
                    ImageUrl="images/apppass.jpg" OnClick="btnAuditAll_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnExport" runat="server" OnClientClick="return ExportDetail(DataItem);"
                    ImageUrl="images/090407_48-55.jpg" OnClick="btnExport_Click" />
                <input type="hidden" id="hidWorkItemID" value="" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>