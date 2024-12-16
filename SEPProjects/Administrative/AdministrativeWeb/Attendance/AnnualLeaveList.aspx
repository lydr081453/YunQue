<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AnnualLeaveList.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.AAndRLeaveList" MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <link href="../css/comboboxStyle.css" rel="stylesheet" type="text/css" />

    <script src="/js/DatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Grid1_onItemBeforeInsert(sender, eventArgs) {
            if (!confirm("您确定要添加该条年假信息?"))
                eventArgs.set_cancel(true);
        }

        function Grid1_onItemBeforeUpdate(sender, eventArgs) {
            if (!confirm("您确定要修改该条年假信息?")) {
                eventArgs.set_cancel(true);
            }
        }

        function Grid1_onItemBeforeDelete(sender, eventArgs) {
            if (!confirm("您确定要删除该条年假信息?"))
                eventArgs.set_cancel(true);
        }

        function Grid1_onCallbackError(sender, eventArgs) {
            if (confirm('无效的数据已输入。查看详细信息？')) alert(eventArgs.get_errorMessage());
            Grid1.page(0);
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function editGrid(rowId) {
            Grid1.edit(Grid1.getItemFromClientId(rowId));
        }

        function editRow(rowId) {
            Grid1.EditComplete();
        }

        function insertRow(rowId) {
            Grid1.EditComplete();
        }

        function deleteRow(rowId) {
            Grid1.deleteItem(Grid1.getItemFromClientId(rowId));
        }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="../images/t2_03.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>搜索 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="right">用 户 名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">员工编号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">&nbsp;
                            <%--<asp:ImageButton ImageUrl="../images/cardmanage.jpg" ID="btnAdd" runat="server" OnClick="btnAdd_Click"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;份：
                        </td>
                        <td>
                            <asp:DropDownList ID="drpYear" runat="server">
                            </asp:DropDownList>
                            年
                        </td>
                        <td align="right">职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;位：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPositions" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1" Width="246" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">部门：
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="upnDepartment" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbCompany" runat="Server" Width="100" Height="20" AutoHighlight="false"
                                                    AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryCode"
                                                    ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                                                    TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cbCom_SelectedIndexChanged">
                                                </ComponentArt:ComboBox>
                                            </td>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbDepartment1" runat="Server" Width="100" Height="20"
                                                    AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                    DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                    CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cbDepartment1_SelectedIndexChanged">
                                                </ComponentArt:ComboBox>
                                            </td>
                                            <td>
                                                <ComponentArt:ComboBox ID="cbDepartment2" runat="Server" Width="100" Height="20"
                                                    AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                    DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                    CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                    DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                    DropDownHeight="200" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con">
                                                </ComponentArt:ComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" colspan="3">
                            <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="left">&nbsp;
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="../images/t2_03-14.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>添加年假信息 </strong>
                            &nbsp;&nbsp;&nbsp;&nbsp;年份：<asp:DropDownList ID="ddlYearAdd" runat="server">
                            </asp:DropDownList>
                            年
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="right">用 户 名：
                        </td>
                        <td>
                            <asp:HiddenField runat="server" ID ="hidAddUserId" />
                            <asp:TextBox ID="txtUserNameAdd" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1" Width="100" Height="18"></asp:TextBox>
                        </td>
                        <td align="right">员工编号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodeAdd" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1" Width="100" Height="18"></asp:TextBox>
                             <asp:Button ID="btnAddSearch" runat="server" Text=" 检索 " OnClick="btnAddSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">员工姓名：
                        </td>
                        <td >
                            <asp:Label runat="server" ID="lblUserNameAdd"></asp:Label>
                        </td>
                        <td align="right">年假基数：
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlAnnualType" Width="114">
                                <asp:ListItem Text="请选择.." Value="-1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                          <td align="right">入职日期：
                        </td>
                        <td>
                           <asp:TextBox runat="server" ID="txtJoinDate" Width="100"></asp:TextBox>
                        </td>
                          <td align="right">首次工作日期：
                        </td>
                        <td>
                             <asp:TextBox runat="server" ID="txtWorkBegin" Width="100"></asp:TextBox>
                            <asp:Button runat="server" ID="btnAdd" Text=" 添加 " OnClick="btnAdd_Click" />&nbsp;&nbsp;
                             <asp:Button runat="server" ID="btnBatchAdd" Text=" 批量添加所有员工年假信息 " OnClick="btnBatchAdd_Click" />
                        </td>

                    </tr>
                </table>

            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
        <%--                    <tr>
                        <td align="right">
                            <input type="button" onclick="Grid1.Table.AddRow()" value=" 添 加 " />
                        </td>
                    </tr>--%>
        <tr>
            <td class="td">
                <ComponentArt:Grid ID="Grid1" CallbackCachingEnabled="false" AutoPostBackOnInsert="true"
                    AutoPostBackOnDelete="true" AutoPostBackOnUpdate="true" AutoCallBackOnInsert="true"
                    AutoCallBackOnUpdate="true" AutoCallBackOnDelete="true" EditOnClickSelectedItem="false"
                    CallbackReloadTemplates="false" AllowColumnResizing="true" AllowEditing="true"
                    KeyboardEnabled="false" GroupingPageSize="10" GroupingMode="ConstantGroups" GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="GridHeaderText"
                    GroupingNotificationText="年假统计信息" HeaderHeight="30" HeaderCssClass="GridHeader"
                    GroupingCountHeadingsAsRows="true" DataAreaCssClass="GridData" EnableViewState="true"
                    RunningMode="Client" ShowHeader="true" FooterCssClass="GridFooter" PreExpandOnGroup="true"
                    PagerStyle="Slider" PagerTextCssClass="GridFooterText" PagerButtonWidth="41"
                    PagerButtonHeight="22" PageSize="20" PagerButtonHoverEnabled="false" SliderHeight="20"
                    SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35" ShowSearchBox="true"
                    SearchText="关键字搜索：" SearchTextCssClass="SearchText" ImagesBaseUrl="../images/gridview2/"
                    PagerImagesFolderUrl="../images/gridview2/pager/" TreeLineImagesFolderUrl="../images/gridview2/lines/"
                    TreeLineImageWidth="11" TreeLineImageHeight="11" Width="100%" Height="100%" runat="server"
                    LoadingPanelFadeDuration="1000" LoadingPanelFadeMaximumOpacity="60" LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
                    LoadingPanelPosition="MiddleCenter" OnUpdateCommand="Grid1_UpdateCommand" OnInsertCommand="Grid1_InsertCommand"
                    OnDeleteCommand="Grid1_DeleteCommand" OnNeedRebind="OnNeedRebind" OnNeedDataSource="OnNeedDataSource">
                    <ClientEvents>
                        <CallbackError EventHandler="Grid1_onCallbackError" />
                        <ItemBeforeInsert EventHandler="Grid1_onItemBeforeInsert" />
                        <ItemBeforeUpdate EventHandler="Grid1_onItemBeforeUpdate" />
                        <ItemBeforeDelete EventHandler="Grid1_onItemBeforeDelete" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" ColumnReorderIndicatorImageUrl="reorder.gif"
                            DataCellCssClass="DataCell" HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" EditCommandClientTemplateId="EditCommandTemplate"
                            InsertCommandClientTemplateId="InsertCommandTemplate" SortImageWidth="9" SortImageHeight="5">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                                <ComponentArt:GridColumn AllowEditing="False" HeadingText="姓名" DataField="EmployeeName"
                                    Align="Center" Width="50" />
                                <ComponentArt:GridColumn HeadingText="员工编号" DataField="UserCode" Align="Center" Width="55" />
                                <ComponentArt:GridColumn AllowEditing="False" HeadingText="职位" DataField="DepartmentPositionName"
                                    Align="Center" DataType="System.String" />
                                <ComponentArt:GridColumn AllowEditing="False" HeadingText="部门" DataField="DepartmentName"
                                    Align="Center" DataType="System.String" />
                                <ComponentArt:GridColumn HeadingText="年份" DataField="LeaveYear" Align="Center" DataType="System.String"
                                    Width="55" />
                                <ComponentArt:GridColumn HeadingText="年度年假" DataField="LeaveNumber" DataType="System.String"
                                    Align="Center" />
                                <ComponentArt:GridColumn HeadingText="年度剩余年假" DataField="RemainingNumber" DataType="System.String"
                                    Align="Center" />
                                <ComponentArt:GridColumn AllowSorting="false" HeadingText="编辑" DataCellClientTemplateId="EditTemplate"
                                    EditControlType="EditCommand" Width="150" Align="Center" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                    <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg); background-repeat: repeat-x;">
                                        <span style="color: White; font-weight: bold;">待审批事由列表</span>
                                    </td>
                                    <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg); background-repeat: repeat-x;"></td>
                                    <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;
                                    </td>
                                    <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg); background-repeat: repeat-x;"></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                            <table height="340" width="100%" bgcolor="#e0e0e0">
                                <tr>
                                    <td valign="center" align="center">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td style="font-size: 10px; font-family: Verdana;">Loading...&nbsp;
                                                </td>
                                                <td>
                                                    <img src="../images/spinner.gif" width="16" height="16" border="0">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="EditTemplate">
                            <a href="javascript:editGrid('## DataItem.ClientId ##');">Edit</a> | <a href="javascript:deleteRow('## DataItem.ClientId ##')">Delete</a>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                            <a href="javascript:editRow('## DataItem.ClientId ##');">Update</a> | <a href="javascript:Grid1.EditCancel();">Cancel</a>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="InsertCommandTemplate">
                            <a href="javascript:insertRow('## DataItem ##');">Insert</a> | <a href="javascript:Grid1.EditCancel();">Cancel</a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </td>
        </tr>
        <%-- <tr>
                        <td align="right">
                            <input type="button" onclick="Grid1.Table.AddRow()" value=" 添 加 " />
                        </td>
                    </tr>--%>
    </table>

    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
