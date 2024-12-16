<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ReturnTabList.aspx.cs" Inherits="FinanceWeb.project.ReturnTabList" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/ProjectTab.ascx" TagName="tab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/DatePicker.js"></script>

    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function DeleteRow(rowId) {
            GridProject.deleteItem(GridProject.getItemFromClientId(rowId));
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
                alert("请选择单据！");
                return false;
            }
            else {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                return confirm("确认已经抵扣了吗？");
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
                                    <td class="oddrow" style="width: 15%">单据类型:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlType">
                                            <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="付款申请" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="押金申请" Value="11"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="oddrow">费用所属组:
                                    </td>
                                    <td class="oddrow-l">
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
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtBegin" onclick="setDate(this);" runat="server" />-<asp:TextBox ID="txtEnd" onclick="setDate(this);" runat="server" />
                                    </td>
                                     <td class="oddrow" style="width: 15%">发票状态:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="ddlInvoice" runat="server">
                                            <asp:ListItem Text="请选择" Selected="True" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="未开发票" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="已开发票" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="无需发票" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_OnClick"
                                            CssClass="widebuttons" />&nbsp;&nbsp;
                                        <asp:Button ID="btnDiscount" runat="server" Text=" 抵扣 " OnClientClick="return submitMatter();"
                                            OnClick="btnDiscount_OnClick" />
                                        <input type="hidden" id="hidWorkItemID" value="" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="oddrow-l" style="text-align: left; padding-bottom: 5px; font-size: 15px;">付款申请总额：<asp:Label ID="labTotal" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <ComponentArt:Grid ID="GridReturn" OnItemDataBound="GridReturn_ItemDataBound" CallbackCachingEnabled="true"
                                            CallbackCacheSize="60" RunningMode="Callback" AllowTextSelection="true" DataAreaCssClass="GridData"
                                            EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PageSize="20"
                                            PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                                            PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
                                            SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                                            ManualPaging="true" PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                                            TreeLineImageWidth="11" TreeLineImageHeight="11" Width="100%" Height="100%" runat="server">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="ReturnID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                                    RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                                    HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                                    HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                                    AllowGrouping="false" SortImageHeight="19">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="GroupID" DataField="GroupID" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="FactFee" DataField="FactFee" Visible="false" />
                                                        <ComponentArt:GridColumn HeadingText="选择" DataCellServerTemplateId="checkTemplate"
                                                            DataField="Choice" Align="Center" Width="25" />
                                                        <ComponentArt:GridColumn HeadingText="PN No." DataField="ReturnCode" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="PR No." DataField="PRNO" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="申请人" DataField="RequestEmployeeName" Width="80"
                                                            Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Width="150" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="发票" DataField="IsInvoice" Width="30" Align="Center"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="预付金额" DataField="PreFee" Width="80" Align="Center"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="实际支付" DataField="factFee" Width="80" Align="Center"
                                                            FormatString="#,##0.00" />
                                                        <ComponentArt:GridColumn HeadingText="状态" DataField="ReturnStatus" Width="80" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="供应商" DataField="SupplierName" Width="100" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="附件" DataField="Attach" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="导出" DataField="Export" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="打印" DataField="Print" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="审批状态" DataField="ViewAudit" Width="30" Align="Center" />
                                                        <ComponentArt:GridColumn HeadingText="重汇" DataField="RePay" Width="30" Align="Center" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ServerTemplates>
                                                <ComponentArt:GridServerTemplate ID="checkTemplate">
                                                    <Template>
                                                        <%#getIsDiscount(Container.DataItem["ReturnID"].ToString(), Convert.ToDecimal(Container.DataItem["FactFee"] == DBNull.Value ? 0 : Container.DataItem["FactFee"]))%>
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
            var s = "<% = GridReturn.ClientID %>";
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
