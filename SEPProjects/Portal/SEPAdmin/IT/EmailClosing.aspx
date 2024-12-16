<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailClosing.aspx.cs" MasterPageFile="~/MasterPage.master"  Inherits="SEPAdmin.IT.EmailClosing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">离职员工:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblUserName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">员工邮箱:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblEmail"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">离职日期:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblLastDay"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">保留日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblKeepDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">所属部门:
            </td>
            <td class="oddrow-l" style="width: 50%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="2">
                  <asp:Button runat="server" ID="btnCommit" Text=" 确认关闭 " OnClick="btnCommit_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
