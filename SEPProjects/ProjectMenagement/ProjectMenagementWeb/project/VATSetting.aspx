<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="VATSetting.aspx.cs" Inherits="FinanceWeb.project.VATSetting" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                项目增值税状态设置<a name="top_A" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号或流水号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 20%">
                置位状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList ID="ddlContactStatus" runat="server">
                    <asp:ListItem Selected="True" Text="按税改前计算" Value="0"></asp:ListItem>
                    <asp:ListItem Text="按税改后计算" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td class="oddrow-l" colspan="4">
         <asp:Button ID="btnSave" runat="server" Text="  保存 " OnClick="btnSave_Click" CssClass="widebuttons" />&nbsp;
        </td>
        </tr>
    </table>
</asp:Content>
