<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ChangeUser.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.ChangeUser" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="S1" runat="server" />
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
    <link href="/public/css/gridViewStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function EmplyeeClick(type) {
            var oldUserId = document.getElementById("<% =hidOldUser.ClientID %>").value;
            var win = window.open('EmployeeList.aspx?type=' + type + '&oldUserId=' + oldUserId, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

        }

        function checkAll(name1, name2) {
            var all = document.getElementsByName(name1);
            var inputs = document.getElementsByName(name2);
            var aaa;
            for (i = 0; i < inputs.length; i++) {
                inputs[i].checked = all[0].checked;
                aaa += inputs[i].value + ",";
            }
        }
    </script>

    <asp:LinkButton ID="lnkPost" runat="server" OnClick="lnkPost_Click" />
    <asp:LinkButton ID="lnkPost1" runat="server" OnClick="lnkPost1_Click" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" width="15%">
                离职人员:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtOldUser" runat="server" onfocus="javascript:this.blur();" /><asp:HiddenField
                    ID="hidOldUser" runat="server" />
                &nbsp;<asp:Button ID="btnSelect1" CssClass="widebuttons" runat="server" Text="选 择"
                    CausesValidation="false" OnClientClick="EmplyeeClick('changeuser1');return false;" /><asp:RequiredFieldValidator
                        ControlToValidate="txtOldUser" Display="None" ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="请选择离职人员"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                替换人员:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtNewUser" runat="server" onfocus="javascript:this.blur();" /><asp:HiddenField
                    ID="hidNewUser" runat="server" />
                &nbsp;<asp:Button ID="btnSelect2" CssClass="widebuttons" runat="server" Text="选 择"
                    CausesValidation="false" OnClientClick="EmplyeeClick('changeuser2');return false;" /><asp:RequiredFieldValidator
                        ControlToValidate="txtNewUser" Display="None" ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="请选择替换人员"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="100%"
        ActiveTabIndex="0">
        <uc1:TabPanel ID="TabPanel1" HeaderText="申请单" runat="server">
            <ContentTemplate>
                <table width="100%" class="XTable">
                    <tr>
                        <td width="70%">
                            <asp:RadioButtonList ID="radUserType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="radUserType_SelectIndexChanged">
                                <asp:ListItem Text="申请人" Value="requestor" Selected="True" />
                                <asp:ListItem Text="收货人" Value="goods_receiver" />
                                <asp:ListItem Text="附加收货人" Value="appendreceiver" />
                                <asp:ListItem Text="分公司审核人" Value="filiale_auditor" />
                                <asp:ListItem Text="物料审核人" Value="first_assessor" />
                                <asp:ListItem Text="采购总监" Value="purchaseauditor" />
                                <asp:ListItem Text="媒介总监" Value="mediaauditor" />
                                <asp:ListItem Text="AD总监" Value="adauditor" />
                                <asp:ListItem Text="业务审核" Value="auditorId" />
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnChange" runat="server" Text="变更" CssClass="widebuttons" OnClick="btnChange_Click" />
                        </td>
                    </tr>
                </table>
                <ComponentArt:Grid ID="grPR" CallbackCachingEnabled="false" CallbackCacheSize="0"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" AllowColumnResizing="false"
                    GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData" FooterCssClass="GridFooter"
                    PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
                    PagerButtonHeight="26" PagerButtonHoverEnabled="True" SliderHeight="26" SliderWidth="150"
                    SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/"
                    PagerImagesFolderUrl="/images/gridview/pager/" TreeLineImagesFolderUrl="/images/gridview/lines/"
                    TreeLineImageWidth="11" TreeLineImageHeight="11" Width="100%" Height="100%" runat="server"
                    CollapseSlide="None" RecordCount="0">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ID" TableHeadingCssClass="GridHeader" RowCssClass="Row"
                            AllowSorting="false" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkPRT1" DataCellServerTemplateId="chkPRT2"
                                    DataField="ID" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="申请单号" DataField="prNO" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="申请人" DataField="requestorname" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="收货人" DataField="receivername" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="附加收货人" DataField="appendReceiverName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="分公司审核人" DataField="Filiale_AuditName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="物料审核人" DataField="first_assessorname" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="采购总监" DataField="purchaseAuditorName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="媒介审核人" DataField="mediaAuditorName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="AD审核人" DataField="adAuditorName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="业务审核人" DataCellServerTemplateId="operationT"
                                    DataField="auditorId" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="状态" DataField="Status" Align="Center" DataCellServerTemplateId="StatusT" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="StatusT" runat="server">
                            <Template>
                                <%# ESP.Purchase.Common.State.requistionOrorder_state[int.Parse(Container.DataItem["Status"].ToString())]%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="chkPRT2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkPR" name="chkPR" value='<%# Container.DataItem["ID"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="operationT" runat="server">
                            <Template>
                                <%# new ESP.Compatible.Employee(int.Parse(Container.DataItem["auditorId"].ToString())).Name %>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="chkPRT1">
                            <input type="checkbox" id="chkPRall" name="chkPRall" onclick="checkAll('chkPRall','chkPR');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="btnR1" runat="server" CssClass="widebuttons" Text="申请人" OnClick="btnR1_Click" Visible="false" />
                            <asp:Button ID="btnR2" runat="server" CssClass="widebuttons" Text="收货人" OnClick="btnR2_Click" Visible="false" />
                            <asp:Button ID="btnR3" runat="server" CssClass="widebuttons" Text="附加收货人" OnClick="btnR3_Click" Visible="false"/>
                            <asp:Button ID="btnR4" runat="server" CssClass="widebuttons" Text="分公司审核人" OnClick="btnR4_Click" Visible="false" />
                            <asp:Button ID="btnR5" runat="server" CssClass="widebuttons" Text="物料审核人" OnClick="btnR5_Click" Visible="false"/>
                            <asp:Button ID="btnR6" runat="server" CssClass="widebuttons" Text="采购总监人" OnClick="btnR6_Click" Visible="false"/>
                            <asp:Button ID="btnR7" runat="server" CssClass="widebuttons" Text="媒介审核人" OnClick="btnR7_Click" Visible="false"/>
                            <asp:Button ID="btnR8" runat="server" CssClass="widebuttons" Text="AD审核人" OnClick="btnR8_Click" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="付款申请" runat="server">
            <ContentTemplate>
                <ComponentArt:Grid ID="grPN" CallbackCachingEnabled="false" CallbackCacheSize="0"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" DataAreaCssClass="GridData"
                    EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PagerStyle="Slider"
                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                    SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                    Width="98%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="returnID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            AllowSorting="false" HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkPNT1" DataCellServerTemplateId="chkPNT2"
                                    DataField="returnID" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="PN号" DataField="returnCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="申请人" DataField="requestEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="审核人" DataField="auditorEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="状态" DataField="returnStatus" Align="Center"
                                    DataCellServerTemplateId="returnStatusT" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="returnStatusT" runat="server">
                            <Template>
                                <%# ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(Container.DataItem["returnStatus"].ToString()),0,null)%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="chkPNT2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkPN" name="chkPN" value='<%# Container.DataItem["returnID"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="chkPNT1">
                            <input type="checkbox" id="chkPNall" name="chkPNall" onclick="checkAll('chkPNall','chkPN');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="btnN1" runat="server" CssClass="widebuttons" Text="申请人" OnClick="btnN1_Click" />
                            <asp:Button ID="btnN2" runat="server" CssClass="widebuttons" Text="审核人" OnClick="btnN2_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="项目号" runat="server">
            <ContentTemplate>
                <ComponentArt:Grid ID="grPJ" CallbackCachingEnabled="false" CallbackCacheSize="0"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                    EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PagerStyle="Buttons"
                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                    SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                    TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                    PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="projectId" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkPJT1" DataCellServerTemplateId="chkPJT2"
                                    DataField="projectId" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="负责人" DataField="ApplicantEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="审核人" DataField="auditorEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="状态" DataField="status" Align="Center" DataCellServerTemplateId="PJStatusT" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="PJStatusT" runat="server">
                            <Template>
                                <%# ESP.Finance.Utility.State.SetState(int.Parse(Container.DataItem["status"].ToString()))%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="chkPJT2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkPJ" name="chkPJ" value='<%# Container.DataItem["projectId"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="chkPJT1">
                            <input type="checkbox" id="Checkbox1" name="chkPJall" onclick="checkAll('chkPJall','chkPJ');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="btnPJ1" runat="server" CssClass="widebuttons" Text="申请人" OnClick="btnPJ1_Click" />
                            <asp:Button ID="btnPJ2" runat="server" CssClass="widebuttons" Text="审核人" OnClick="btnPJ2_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel4" HeaderText="支持方" runat="server">
            <ContentTemplate>
                <ComponentArt:Grid ID="grSP" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                    EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PagerStyle="Buttons"
                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                    SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                    TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                    PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="SupportID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkSPT1" DataCellServerTemplateId="chkSPT2"
                                    DataField="SupportID" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="负责人" DataField="LeaderEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="审核人" DataField="auditorEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="状态" DataField="status" Align="Center" DataCellServerTemplateId="SPStatusT" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="SPStatusT" runat="server">
                            <Template>
                                <%# ESP.Finance.Utility.State.SetState(int.Parse(Container.DataItem["status"].ToString()))%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="chkSPT2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkSP" name="chkSP" value='<%# Container.DataItem["SupportID"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="chkSPT1">
                            <input type="checkbox" id="Checkbox2" name="chkSPall" onclick="checkAll('chkSPall','chkSP');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="btnSP1" runat="server" CssClass="widebuttons" Text="申请人" OnClick="btnSP1_Click" />
                            <asp:Button ID="btnSP2" runat="server" CssClass="widebuttons" Text="审核人" OnClick="btnSP2_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel5" HeaderText="报销" runat="server" Visible="false">
            <ContentTemplate>
                <ComponentArt:Grid ID="Grid1" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                    EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PagerStyle="Buttons"
                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                    SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                    TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                    PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="SupportID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkSPT1" DataCellServerTemplateId="chkSPT2"
                                    DataField="SupportID" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="负责人" DataField="LeaderEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="审核人" DataField="auditorEmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="状态" DataField="status" Align="Center" DataCellServerTemplateId="SPStatusT" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="GridServerTemplate1" runat="server">
                            <Template>
                                <%# ESP.Finance.Utility.State.SetState(int.Parse(Container.DataItem["status"].ToString()))%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="GridServerTemplate2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkSP" name="chkSP" value='<%# Container.DataItem["SupportID"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            <input type="checkbox" id="Checkbox3" name="chkSPall" onclick="checkAll('chkSPall','chkSP');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="widebuttons" Text="申请人" OnClick="btnSP1_Click" />
                            <asp:Button ID="Button2" runat="server" CssClass="widebuttons" Text="审核人" OnClick="btnSP2_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel6" HeaderText="考勤" runat="server">
            <ContentTemplate>
                <ComponentArt:Grid ID="grKQ" GroupBy="" GroupingPageSize="10" GroupingMode="ConstantGroups"
                    RunningMode="Callback" AllowPaging="false" PageSize="300" GroupByTextCssClass="txt"
                    GroupBySectionCssClass="grp" GroupingNotificationTextCssClass="txt" DataAreaCssClass="GridData"
                    EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter" PagerStyle="Buttons"
                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                    SliderPopupOffsetX="50" ImagesBaseUrl="/images/gridview/" PagerImagesFolderUrl="/images/gridview/pager/"
                    TreeLineImagesFolderUrl="/images/gridview/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                    PreExpandOnGroup="false" Width="98%" Height="100%" runat="server">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                            SortImageHeight="19" GroupHeadingCssClass="grp-hd">
                            <Columns>
                                <ComponentArt:GridColumn HeadingCellClientTemplateId="chkKQT1" DataCellServerTemplateId="chkKQT2"
                                    DataField="ID" Align="Center" AllowHtmlContent="True" AllowSorting="False" />
                                <ComponentArt:GridColumn HeadingText="事由信息" DataField="content" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="申请人" DataField="AppUserName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="审核人" DataField="EmployeeName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="提交时间" DataField="SubmitTime" Align="Center"
                                    FormatString="yyyy-MM-dd mm:hh:ss" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="chkKQT2" runat="server">
                            <Template>
                                <input type="checkbox" id="chkKQ" name="chkKQ" value='<%# Container.DataItem["ID"] %>' />
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="chkKQT1">
                            <input type="checkbox" name="chkKQall" onclick="checkAll('chkKQall','chkKQ');" />选择
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <br />
                <table width="98%" class="XTable">
                    <tr>
                        <td>
                            <asp:Button ID="btnKQ" runat="server" CssClass="widebuttons" Text="审核人" OnClick="btnKQ_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
