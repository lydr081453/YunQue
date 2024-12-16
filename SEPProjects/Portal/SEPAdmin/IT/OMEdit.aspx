<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OMEdit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.IT.OMEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">标题:
            </td>
            <td class="oddrow-l" style="width: 80%">
                <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">运维类型:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">运维内容:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Width="80%" Height="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="2">
                <asp:Button runat="server" ID="btnCommit" Text=" 提交 " OnClick="btnCommit_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
