<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="PrLinkManEditList.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.PrLinkManEditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">用户信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">联系人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtLinkman"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">手机号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">邮箱:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtMail"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td class="oddrow-l">&nbsp;</td>
            <td class="oddrow-l">
                <asp:Button ID="btnSelect" Text="确定" Font-Size="10" CssClass="widebuttons" runat="server" OnClick="btnSelect_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" Text="返回" Font-Size="10" CssClass="widebuttons" runat="server" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
