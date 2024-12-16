<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OMAudit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.IT.OMAudit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">标题:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblTitile"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">申请人:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblCreater"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">申请日期:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblCreateTime"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">运维类型:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblType"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">运维内容:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Height="80" Width="80%" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">审核日志:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">审核信息:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:TextBox runat="server" ID="txtAudit" TextMode="MultiLine" Height="50" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="2">
                <asp:Button runat="server" ID="btnCommit" Text=" 审核通过 " OnClick="btnCommit_Click" />&nbsp;&nbsp;
                 <asp:Button runat="server" ID="btnReject" Text=" 审核驳回 " OnClick="btnReject_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
