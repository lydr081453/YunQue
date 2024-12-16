<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="CloseProject.aspx.cs" Inherits="FinanceWeb.project.CloseProject" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_05.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="CloseProjectList.aspx">返回项目列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:CustomerDisplay ID="CustomerDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectInfoView ID="ProjectInfoView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectContractCostView ID="ProjectContractCostView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectSupporterDisplay ID="ProjectSupporterDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PaymentDisplay ID="PaymentDisplay" runat="server" />
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td class="heading" colspan="4">
                项目申请单列表
            </td>
        </tr>
        <tr id="trNoRecord" runat="server" visible="false">
            <td colspan="4">
                <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                    width: 100%; border-collapse: collapse;">
                    <tr class="Gheading" align="center">
                        <th scope="col">
                            序号
                        </th>
                        <th scope="col">
                            单据号
                        </th>
                        <th scope="col">
                            申请人
                        </th>
                        <th scope="col">
                            单据金额
                        </th>
                        <th scope="col">
                            单据类型
                        </th>
                        <th scope="col">
                            单据状态
                        </th>
                    </tr>
                    <tr class="td" align="left">
                        <td colspan="6" align="center">
                            <span>暂时没有项目申请单信息</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trGrid" runat="server" visible="true">
            <td colspan="4">
                <asp:GridView ID="gvOrder" Width="100%" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="gvOrder_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PrNo" HeaderText="单据号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                                     <asp:BoundField DataField="project_descripttion" HeaderText="内容描述" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="35%" />
                        <asp:BoundField DataField="RequestorName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="单据金额" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAmounts" runat="server"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="moneytype" HeaderText="单据类型" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="单据状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Label ID="lblOrderStatus" runat="server"></asp:Label>                       
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="付款状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                             <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPreClose" runat="server" Text=" 预关闭 " CssClass="widebuttons" OnClientClick="return confirm('你确定要预关闭该项目号吗?该项目号将不能创建采购申请单.');" OnClick="btnPreClose_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="return confirm('你确定要关闭该项目号吗?该项目号将停止使用.');" OnClick="btnClose_Click" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
