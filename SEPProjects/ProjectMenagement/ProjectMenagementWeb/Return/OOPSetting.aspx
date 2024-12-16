<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OOPSetting.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Return.OOPSetting" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                报销单调整到本次报销
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                报销单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:TextBox runat="server" ID ="txtCode"></asp:TextBox>
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:Button runat="server" ID="btnSetting" Text=" 调整到本次报销 "  OnClick="btnSetting_Click"/>
            </td>
        </tr>
        </table>
</asp:Content>