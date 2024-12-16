<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitReceivingAuditList.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.WaitReceivingAuditList" Theme="" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/gridStyle2.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

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
                            关 键 字:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             提交日期:&nbsp;&nbsp;<asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                                        --
                                        <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                        </td>
                        <td align="left">
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            公司选择:&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                                        </asp:DropDownList>
                             
                        </td>
                        <td align="left">
                            <br />
                            <asp:ImageButton ImageUrl="/images/t2_03-07.jpg"  ID="btnSearch" OnClick="btnSearch_Click" Width="56" Height="24" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr class="GridHeader2" >
                        <td style="height:30" class="GridHeaderText">审核列表</td>
                    </tr>
                    <tr>
                        <td class="td">
                            <ComponentArt:Grid ID="GridNoNeed" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  GroupingMode="ConstantGroups"
                                GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"  
                                EditOnClickSelectedItem="false" FooterCssClass="GridFooter"
                                FooterHeight="40" HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                                GroupingNotificationText="审核列表" PagerInfoClientTemplateId="ClientTemplate3" PagerStyle="Numbered"
                                PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                                SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35"
                                ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                                TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11" PageSize="200"  ShowHeader="false"
                                PreExpandOnGroup="true" Width="100%" Height="100%" runat="server" >
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="WorkItemID" ShowTableHeading="false" TableHeadingCssClass="GridHeader" ShowSelectorCells="false"
                                        RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                        HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                        HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                        HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"  GroupHeadingClientTemplateId="GroupByTemplate"
                                        SortImageHeight="5">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="WorkItemID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="WebPage" Visible="false" />
                                            <ComponentArt:GridColumn DataField="confirmFee" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                            <ComponentArt:GridColumn DataField="WorkMonth" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExpenseCommitDeadLine" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                            <ComponentArt:GridColumn HeadingText="选择" DataCellServerTemplateId="checkTemplate" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="审批角色" DataField="ParticipantName" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="项目名称" DataField="ReturnContent" Align="Center" /> 
                                            <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="预计报销金额" DataField="PreFee" Align="Right" FormatString="0.00" />
                                            <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="提交日期" DataField="CommitDate" Align="Center" FormatString="yyyy-MM-dd" />
                                            <ComponentArt:GridColumn HeadingText="业务审批通过日期" DataField="LastAuditPassTime" Align="Center" FormatString="yyyy-MM-dd" />
                                            <ComponentArt:GridColumn HeadingText="单据类型" DataField="ReturnTypeName" Align="Center" />
                                            <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate" Align="Center" />
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
                                            <%#getIsBatchInput(Convert.ToDecimal(Container.DataItem["PreFee"] == DBNull.Value ? 0 : Container.DataItem["PreFee"]), Container.DataItem["WorkItemID"].ToString(), Convert.ToDecimal(Container.DataItem["confirmFee"] == DBNull.Value ? 0 : Container.DataItem["confirmFee"]), Container.DataItem["ReturnType"].ToString(), Container.DataItem["WorkMonth"].ToString())%>
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
                            <br />
                            <asp:ImageButton ID="btnAuditAll" runat="server" OnClientClick="return submitMatter(DataItem);"  ImageUrl="images/apppass.jpg" OnClick="btnAuditAll_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btnExport" runat="server" OnClientClick="return ExportDetail(DataItem);"  ImageUrl="images/090407_48-55.jpg" OnClick="btnExport_Click" />    
                            <input type="hidden" id="hidWorkItemID" value="" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>
