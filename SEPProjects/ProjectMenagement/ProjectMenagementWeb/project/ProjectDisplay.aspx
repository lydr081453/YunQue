<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true"
    Inherits="project_ProjectDisplay" CodeBehind="ProjectDisplay.aspx.cs" %>


<%@ Register Src="/UserControls/Project/CustomerDisplay.ascx" TagName="CustomerDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="PrepareDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMemberDisplay.ascx" TagName="ProjectMemberDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PaymentDisplay.ascx" TagName="PaymentDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectInfoView.ascx" TagName="ProjectInfoView"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectContractCostView.ascx" TagName="ProjectContractCostView"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectSupporterDisplay.ascx" TagName="ProjectSupporterDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

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

    <uc1:TopMessage ID="TopMessage" runat="server" />
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%"
        EnableViewState="false" ActiveTabIndex="0">
        <uc1:TabPanel ID="TabPanel1" HeaderText="项目准备信息" runat="server">
            <ContentTemplate>
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="成员" runat="server">
            <ContentTemplate>
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="合同信息" runat="server">
            <ContentTemplate>
                <uc1:CustomerDisplay ID="CustomerDisplay" runat="server" />
                <uc1:ProjectInfoView ID="ProjectInfoView" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel4" HeaderText="成本明细信息" runat="server">
            <ContentTemplate>
                <uc1:ProjectContractCostView ID="ProjectContractCostView" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel5" HeaderText="支持方信息" runat="server">
            <ContentTemplate>
                <uc1:ProjectSupporterDisplay ID="ProjectSupporterDisplay" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel6" HeaderText="付款通知信息" runat="server">
            <ContentTemplate>
                <uc1:PaymentDisplay ID="PaymentDisplay" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel8" HeaderText="证据链" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">证据链信息</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ContractStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="上传时间" ItemStyle-HorizontalAlign="Center" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField HeaderText="描述" ItemStyle-HorizontalAlign="Center" DataField="Description" />
                                    <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?ContractID=<%# Eval("ContractID") %>'>
                                                <img src="/images/ico_04.gif" border="0" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="上传人" ItemStyle-HorizontalAlign="Center" DataField="CreatorUserName" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel9" HeaderText="发票申请" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">发票申请信息</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvApplyForInvioce" runat="server" AutoGenerateColumns="false" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="流向" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("FlowTo") == null ? "客户" : ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[ (int)Eval("FlowTo")] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="媒体付款主体" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("SupplierId") == null ? "" : Eval("MediaName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="发票金额" ItemStyle-HorizontalAlign="Center" DataField="InviocePrice" DataFormatString="{0:#,##0.00}" />
                                    <asp:BoundField HeaderText="描述" ItemStyle-HorizontalAlign="Center" DataField="Remark" />
                                    <asp:BoundField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" DataField="CreatorUserName" />
                                    <asp:BoundField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ApplyForInvioceStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>

        <uc1:TabPanel ID="TabPanel7" HeaderText="消耗信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 5%">消耗小计:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="lblConsumptionTotal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvConsumption" runat="server" OnRowDataBound="gvConsumption_RowDataBound" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="序号" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="OrderYM" HeaderText="年月" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="PurchaseBatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="JSCode" HeaderText="JSCode" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="XMCode" HeaderText="XMCode" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Media" HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="OrderType" HeaderText="类别" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BatchId" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel10" HeaderText="媒体返点信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 5%">媒体返点小计:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="labRebateRegistrationTotal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvRebateRegistration" runat="server" AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CreditedDate" HeaderText="日期" ItemStyle-Width="5%" />
                                    <asp:BoundField DataField="PurchaseBatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#  Eval("Supplier") == null ? "" : ((ESP.Purchase.Entity.SupplierInfo)Eval("Supplier")).supplier_name %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="RebateAmount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                                           <asp:BoundField DataField="Remark" HeaderText="返点内容" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="AccountingNum" HeaderText="返点核算信息编号" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="SettleType" HeaderText="结算类型" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="Branch" HeaderText="我方主体名称" ItemStyle-HorizontalAlign="Center"
                 />
                                    <asp:BoundField DataField="BatchId" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">银行信息
            </td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">审批记录
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <asp:Label ID="lblLog" runat="server" Font-Size="XX-Small"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">立项原因
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <asp:Label ID="lblReason" runat="server" Font-Size="XX-Small"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" runat="server" Text=" 关闭  " CssClass="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
    </table>
</asp:Content>
