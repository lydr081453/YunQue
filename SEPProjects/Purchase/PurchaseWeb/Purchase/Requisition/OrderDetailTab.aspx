<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="OrderDetailTab.aspx.cs" Title="PR订单信息" Inherits="OrderDetailTab" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        function openPaymentPreview(ppid) {
            var win = window.open("Print/PaymantPrint.aspx?<%=ESP.Finance.Utility.RequestName.ReturnID %>=" + ppid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/paymentInfo.ascx" tagname="paymentInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/operationAuditLog.ascx" tagname="operationAuditLog"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
        <%@ register src="/UserControls/View/prLogs.ascx" tagname="prLogs"
        tagprefix="uc1" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="uc1" %>
    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <uc1:TabPanel HeaderText="PR信息" runat="server">
            <ContentTemplate>
                <%--          项目号申请单信息        --%>
                <uc1:projectInfo ID="projectInfo" runat="server" />
                <uc1:genericInfo ID="GenericInfo" runat="server" />
                <%--          项目号申请单信息        --%>
                <%-- *********采购物料信息********* --%>
                <uc1:productInfo ID="productInfo" runat="server" />
                <%-- *********采购物料信息********* --%>
                <%-- *********供应商信息********* --%>
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <%-- *********账期信息********* --%>
                <uc1:paymentInfo ID="paymentInfo" runat="server" />
                <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel HeaderText="审核信息" ID="Tab2" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            审核信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            订单编号:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="txtorderid" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            谈判类型:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txttype" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px">
                            比价节约:
                        </td>
                        <td class="oddrow-l" style="width: 300px">
                            <asp:Label ID="txtcontrast" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                                ID="labdownContrast" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 100px">
                            议价节约:
                        </td>
                        <td class="oddrow-l" style="width: 300px">
                            <asp:Label ID="txtconsult" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                                ID="labdownConsult" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px">
                            初审人:
                        </td>
                        <td class="oddrow-l" style="width: 300px">
                            <asp:Label ID="ddlfirst_assessor" runat="server" SkinID="userLabel" />
                        </td>
                        <td class="oddrow" style="width: 100px">
                            事后申请:
                        </td>
                        <td class="oddrow-l" style="width: 300px">
                            <asp:Label ID="labafterwards" runat="server" /><br />
                            <asp:Label ID="labafterwardsReason" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px;">
                            紧急采购:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labEmBuy" runat="server" Width="200px" /><br />
                            <asp:Label ID="labEmBuyReason" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 120px;">
                            客户指定:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labCusAsk" runat="server" Width="200px"></asp:Label><br />
                            <asp:Label ID="labCusName" runat="server" Width="200px"></asp:Label><br />
                            <asp:Label ID="labCusAskYesReason" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 100px;">
                            合同号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labContractNo" runat="server" Width="200px" />
                        </td>
                        <td class="oddrow" style="width: 100px;">
                            其它:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="txtothers" runat="server" Width="200px" />
                        </td>
                    </tr>
                </table>
                <uc1:prLogs ID="prLogs" runat="server" />
                <table width="100%" class="tableForm">
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            最后编辑时间:
                        </td>
                        <td style="width: 35%" class="oddrow-l">
                            <asp:Label ID="lablasttime" runat="server" />
                        </td>
                        <td style="width: 20%" class="oddrow">
                            申请单提交时间:
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:Label ID="labrequisition_committime" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            订单生成时间:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="laborder_committime" runat="server" />
                        </td>
                        <td class="oddrow">
                            订单审批时间:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="laborder_audittime" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palFili" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                分公司审核备注:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labFili" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palOverrulP" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                申请单审核备注:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labOverruleP" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palOverrule" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                申请单审批备注:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labOverrule" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="tabOverrule" runat="server">
                    <table class="tableForm" style="width: 100%;">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                申请单审批备注:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="txtoverrule" TextMode="multiLine" Height="100px" Width="400px" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoverrule"
                                    ErrorMessage="审批备注为必填" Display="None"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table class="tableForm" style="width: 100%" id="tabMedia" runat="server" visible="false">
                    <tr>
                        <td colspan="2" class="heading" style="height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            媒介审批备注:
                        </td>
                        <td class="oddrow-l">
                            <asp:Literal ID="litprMediaRemark" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel HeaderText="收货信息" ID="TabPanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">
                            已收货待确认
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvUnConfirmRecipient" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                OnRowDataBound="gvUnConfirmRecipient_RowDataBound" DataKeyNames="recipientId"
                                PageSize="10000" AllowPaging="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="打印">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="Print/MultiRecipientPrint.aspx?newPrint=true&id=<%# Eval("recipientId") %>"
                                                target="_blank">
                                                <img src="../../images/pri_gr.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# int.Parse(Eval("gid").ToString()).ToString("0000000") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="7%" />
                                    <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="9%" />
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="收货人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("goods_receiver").ToString())) %>');">
                                                <%# Eval("receivername")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附加收货人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("appendReceiver").ToString())) %>');">
                                                <%# Eval("appendReceiverName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="收货方式" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRecipientType"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="11%" />
                                    <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="supplier_name" />
                                     <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="labDown" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%" class="tableForm">
                    <tr>
                        <td class="heading">
                            已完成收货
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound"
                                DataKeyNames="recipientId" PageSize="10000" AllowPaging="True" Width="100%">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="打印">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="Print/MultiRecipientPrint.aspx?newPrint=true&id=<%# Eval("recipientId") %>"
                                                target="_blank">
                                                <img src="../../images/pri_gr.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# int.Parse(Eval("gid").ToString()).ToString("0000000") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="7%">
                                        <HeaderStyle Width="7%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="9%">
                                        <HeaderStyle Width="9%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="收货人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("goods_receiver").ToString())) %>');">
                                                <%# Eval("receivername")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附加收货人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("appendReceiver").ToString())) %>');">
                                                <%# Eval("appendReceiverName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="收货方式" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRecipientType"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="11%">
                                        <ItemStyle HorizontalAlign="Center" Width="11%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="supplier_name">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="labDown" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel HeaderText="付款信息" ID="TabPanel2" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">
                            已完成付款申请
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPayment_RowDataBound"
                                DataKeyNames="ReturnID" PageSize="1000" AllowPaging="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# int.Parse(Eval("PRID").ToString()).ToString("0000000") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="prno" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ReturnCode" HeaderText="付款单号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("RequestorID").ToString())) %>');">
                                                <%# Eval("RequestEmployeeName") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("RequestDate") == null ? "" : DateTime.Parse(Eval("RequestDate").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# GetFormatPrice(decimal.Parse(Eval("PreFee").ToString()))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="实际支付金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# GetFormatPrice(decimal.Parse(Eval("FactFee") ==null ? "0" : Eval("FactFee").ToString()))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="实际支付时间" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("ReturnFactDate") == null ? "" : DateTime.Parse(Eval("ReturnFactDate").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# (Eval("PaymentUserID") == null || Eval("PaymentUserID").ToString() == "") ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("PaymentUserID").ToString())) %>');">
                                                <%# Eval("PaymentEmployeeName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#  ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(Eval("ReturnStatus").ToString()),0,null).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPrint"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <asp:Button ID="btnCancel" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons"
                    OnClick="btn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
