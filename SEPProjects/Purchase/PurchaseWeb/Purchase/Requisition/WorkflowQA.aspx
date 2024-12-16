<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Title="采购流程Q&A" CodeBehind="WorkflowQA.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.WorkflowQA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;" align="center">
        <tr>
            <td class="oddrow" style="font-weight: bolder; font-size: larger" colspan="2">
                1.紧急采购：
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                1)是否为紧急采购:
            </td>
            <td class="oddrow-l">
                <asp:RadioButtonList runat="server" ID="rdEmergency" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="2"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="font-weight: bolder; font-size: larger" colspan="2">
                2.供应商资质：
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                1)协议供应商:
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" ID="rdProxy" AutoPostBack="true" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdProxy_SelectedIndexChanged">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                2)客户指定:
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" AutoPostBack="true" ID="rdOne" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdOne_SelectedIndexChanged">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                3)以上都不是，但曾有较好合作经历或具有较好的行业口碑:
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" AutoPostBack="true" ID="rdCoorperation" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdCoorperation_SelectedIndexChanged">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="font-weight: bolder; font-size: larger" colspan="2">
                3.报价合理性：
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                1)经过比价或经验判断，供应商具有一定价格优势
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" ID="rdAdvance" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                2)已提供正式报价单，包含详细服务/产品描述及<br />
                &nbsp;&nbsp;&nbsp;供应商应履行的责任与义务承诺
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" ID="rdContent" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="font-weight: bolder; font-size: larger" colspan="2">
                4.商务条款风险：
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                1)有无预付条款
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" ID="rdPayment" RepeatDirection="Horizontal">
                    <asp:ListItem Text="无预付" Value="1"></asp:ListItem>
                    <asp:ListItem Text="30%以下预付" Value="2"></asp:ListItem>
                    <asp:ListItem Text="30%以上预付" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                2)双方责任限定是否对等
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger">
                <asp:RadioButtonList runat="server" ID="rdRisk" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" style="width: 40%">
                3)违约赔偿责任是否苛刻
            </td>
            <td class="oddrow-l" style="font-weight: bolder; font-size: larger;">
                <asp:RadioButtonList runat="server" ID="rdPaid" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="3"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnCommit" class="widebuttons" Text=" 提交 " OnClick="btnCommit_Click" />
                &nbsp;&nbsp;<input type="button" value=" 关闭 " class="widebuttons" onclick="window.close();" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="font-size: 12px;">
                 <asp:Label runat="server" ID="lblResultTitle" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblResult" />
            </td>
            <td style="font-size: 12px;color: #a83838;">
                <asp:Label runat="server" ID="lblResult2"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
