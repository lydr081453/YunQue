<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceCreateBatch.aspx.cs"
    EnableEventValidation="false" Inherits="FinanceWeb.ExpenseAccount.FinanceCreateBatch"
    Theme="" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/gridStyle2.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function checkedAll() {
            for (var i = 0; i < document.getElementsByName("chkAudit").length; i++) {
                var e = document.getElementsByName("chkAudit")[i];
                e.checked = document.getElementById("chkAll").checked;
            }
        }

        function getUrl(webpage, workitemid, workMonth) {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "'><img src='images/Audit.gif' /></a>";
            if (workMonth != "1.本次处理") {
                return "";
            }
            return sum;
        }

        function createBatch() {
            var hid = document.getElementById("<%= hidWorkItemID.ClientID %>");
            hid.value = "";
            var boxes = document.getElementsByName("chkAudit");
            for (var i = 0; i < boxes.length; i++) {
                var e = boxes[i];
                if (e.checked)
                    hid.value += e.value + ",";
            }


            var batchNo = document.getElementById("<%= txtBatchNo.ClientID %>");
            var nextAuditer = document.getElementById("<%= hidNextAuditor.ClientID %>");
            var branch = document.getElementById("<%=ddlBranch.ClientID %>");
            var branchSelected = branch.options[branch.selectedIndex].value;
            var bank = document.getElementById("<%=ddlBank.ClientID %>");
            var bankSelected = "0";
            if (bank.options.count != 0 && bank.selectedIndex != -1) {
                bankSelected = bank.options[bank.selectedIndex].value;
            }
            var msg = "";
            if (batchNo.value == "") {
                msg += "请填写批次号！\n";
            }
            if (nextAuditer.value == "") {
                msg += "请选择下级审批人！\n";
            }
            if (branchSelected == "0") {
                msg += "请选择公司！\n";
            }
            if (bankSelected == "0") {
                msg += "请选择开户行！\n";
            }
            if (msg == "") {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                return confirm("确认创建批次并审批通过吗？");
            }
            else {
                alert(msg);
                return false;
            }
        }

        function saveBatch() {
            var batchNo = document.getElementById("<%= txtBatchNo.ClientID %>");
            var nextAuditer = document.getElementById("<%= hidNextAuditor.ClientID %>");
            var branch = document.getElementById("<%=ddlBranch.ClientID %>");
            var branchSelected = branch.options[branch.selectedIndex].value;
            var bank = document.getElementById("<%=ddlBank.ClientID %>");
            var bankSelected = "0";
            if (bank.options.count != 0 && bank.selectedIndex != -1) {
                bankSelected = bank.options[bank.selectedIndex].value;
            }
            var msg = "";
            if (batchNo.value == "") {
                msg += "请填写批次号！\n";
            }
            if (nextAuditer.value == "") {
                msg += "请选择下级审批人！\n";
            }
            if (branchSelected == "0") {
                msg += "请选择公司！\n";
            }
            if (bankSelected == "0") {
                msg += "请选择开户行！\n";
            }
            if (msg == "") {
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }

        function auditBatch() {
            return confirm("确认审批通过此批次所有单据吗？");
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
            return groupItem.Grid.Table.GetRow(rows[0]).GetMember("WorkMonth").Value;
        }

        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setnextAuditor(name, sysid) {
            document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
            document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
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
                            <strong>创建批次号</strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                        <td style="width: 15%">
                            批次流水:
                        </td>
                        <td style="width: 35%">
                           <asp:Label runat="server" ID="lblBatchId"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            批次号:
                        </td>
                        <td style="width: 35%">
                           <asp:Label runat="server" ID="lblPurchaseBatchCode"></asp:Label>                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            银行凭证号:
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtBatchNo" runat="server" /><font color="red"> * </font>
                        </td>
                        <td style="width: 15%">
                            下级审批人:
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                                color="red"> * </font>
                            <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
                            <asp:HiddenField ID="hidNextAuditor" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            公司选择:
                        </td>
                        <td style="width: 35%">
                            <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChangeed">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%">
                            选择开户行:
                        </td>
                        <td style="width: 35%">
                            <asp:DropDownList runat="server" ID="ddlBank" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChangeed">
                            </asp:DropDownList>
                            <font color="red">* </font>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            帐号名称:
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblAccountName" runat="server"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            银行帐号:
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblAccount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            银行地址:
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnCreateBatch" runat="server" OnClientClick="return saveBatch();"
                                ImageUrl="images/t2_03-20.jpg" OnClick="btnCreateBatch_Click" />
                            <asp:ImageButton ID="btnAuditBatch" runat="server" OnClientClick="return auditBatch();"
                                ImageUrl="images/submit.jpg" OnClick="btnAuditBatch_Click" />
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="images/return.jpg" OnClick="btnReturn_Click" />
                            <asp:LinkButton ID="btnReLoad" runat="server" OnClick="btnReLoad_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </br>
    <asp:Panel runat="server" ID="panBatch">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
            <tr>
                <td style="width: 65%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="17">
                                            <img src="images/t2_03.jpg" width="21" height="20" />
                                        </td>
                                        <td align="left">
                                            <strong>待选单据 </strong>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            关 键 字:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            单据类型:&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlReturnType" Style="width: auto">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2">
                                             申 请 单:&nbsp;&nbsp;<asp:TextBox ID="txtPNList" Width="500" runat="server" />(用,分隔)
                                        </td>
                                         </tr>
                                    <caption>
                                        <tr><td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:ImageButton ID="btnSearch" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                    Height="24" ImageAlign="AbsMiddle" ImageUrl="/images/t2_03-07.jpg" OnClick="btnSearch_Click"
                                                    Width="56" /> &nbsp;&nbsp;<asp:Button runat="server" ID="btnAddBatchSort" Text=" 按单号顺序创建批次 " OnClick="btnAddBatchSort_Click" />
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                                <br />
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                                    <tr>
                                        <td class="td">
                                            <ComponentArt:Grid ID="GridNoNeed" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"
                                                GroupingMode="ConstantGroups" GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"
                                                EditOnClickSelectedItem="false" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                                GroupingNotificationText="审核列表" ImagesBaseUrl="images/gridview2/" TreeLineImagesFolderUrl="images/gridview2/lines/"
                                                ShowHeader="false" ShowFooter="false" PreExpandOnGroup="true" Width="100%" Height="407"
                                                runat="server" PageSize="20" ManualPaging="true" ScrollBar="Auto" ScrollTopBottomImagesEnabled="true"
                                                ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                <Levels>
                                                    <ComponentArt:GridLevel DataKeyField="WorkItemID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                        ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                                                        DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                                                        GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="5">
                                                        <Columns>
                                                            <%--<ComponentArt:GridColumn HeadingText="审批" DataCellServerTemplateId="auditTemplate" Align="Center" />--%><ComponentArt:GridColumn
                                                                DataField="WorkItemID" Visible="False" />
                                                            <ComponentArt:GridColumn DataField="WebPage" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="confirmFee" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="WorkMonth" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ExpenseCommitDeadLine" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                                            <ComponentArt:GridColumn HeadingText="审批角色" DataField="ParticipantName" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Align="Right" FormatString="0.00" />
                                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="单据类型" DataField="ReturnTypeName" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate"
                                                                Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="编辑" DataCellServerTemplateId="editTemplate"
                                                                Align="Center" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                                <ClientTemplates>
                                                    <ComponentArt:ClientTemplate ID="GroupByTemplate">
                                                        <span>## getTitle(DataItem) ##</span>
                                                    </ComponentArt:ClientTemplate>
                                                </ClientTemplates>
                                                <ServerTemplates>
                                                    <ComponentArt:GridServerTemplate ID="printTemplate">
                                                        <Template>
                                                            <%#getPrintUrl(Container.DataItem["ReturnID"].ToString(),Container.DataItem["ReturnType"].ToString())%>
                                                        </Template>
                                                    </ComponentArt:GridServerTemplate>
                                                    <ComponentArt:GridServerTemplate ID="editTemplate">
                                                        <Template>
                                                            <%#getEditUrl(Container.DataItem["WorkItemID"].ToString())%>
                                                        </Template>
                                                    </ComponentArt:GridServerTemplate>
                                                </ServerTemplates>
                                            </ComponentArt:Grid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnExport" runat="server" OnClientClick="return ExportDetail(DataItem);"
                                                ImageUrl="images/090407_48-55.jpg" OnClick="btnExport_Click" Visible="false" />
                                            <input type="hidden" id="hidWorkItemID" value="" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5%">
                    <asp:Button runat="server" ID="btnAddBatch" Text=" >> " OnClick="btnAddBatch_Click" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="btnRemoveBatch" Text=" << " OnClick="btnRemoveBatch_Click" />
                </td>
                <td style="width: 30%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="17">
                                            <img src="images/t2_03.jpg" width="21" height="20" />
                                        </td>
                                        <td align="left">
                                            <strong>批次中单据</strong>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                                    <tr>
                                        <td class="td">
                                            <ComponentArt:Grid ID="gvInBatch" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"
                                                GroupingMode="ConstantGroups" GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"
                                                EditOnClickSelectedItem="false" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                                GroupingNotificationText="审核列表" ImagesBaseUrl="images/gridview2/" TreeLineImagesFolderUrl="images/gridview2/lines/"
                                                ShowHeader="false" ShowFooter="false" PreExpandOnGroup="true" Width="100%" Height="477"
                                                runat="server" PageSize="20" ManualPaging="true" ScrollBar="Auto" ScrollTopBottomImagesEnabled="true"
                                                ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                <Levels>
                                                    <ComponentArt:GridLevel DataKeyField="WorkItemID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                        ShowSelectorCells="false" RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif"
                                                        DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"
                                                        GroupHeadingClientTemplateId="GroupByTemplate" SortImageHeight="5">
                                                        <Columns>
                                                            <ComponentArt:GridColumn DataField="WorkItemID" Visible="False" />
                                                            <ComponentArt:GridColumn DataField="WebPage" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="confirmFee" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="WorkMonth" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ExpenseCommitDeadLine" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                                            <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" />
                                                            <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Align="Right" FormatString="0.00" />
                                                            <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate2"
                                                                Align="Center" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                                <ClientTemplates>
                                                    <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                                        <span>## getTitle(DataItem) ##</span>
                                                    </ComponentArt:ClientTemplate>
                                                </ClientTemplates>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
