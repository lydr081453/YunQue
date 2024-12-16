<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="AddUserPointRule.aspx.cs" EnableEventValidation="false" Inherits="PurchaseWeb.UserPointRule.view.AddUserPointRule" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                规则名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="textName" runat="server" MaxLength="300" ></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                规则key:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="textKey" runat="server" MaxLength="300" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                积分:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="textScore" runat="server" MaxLength="300"></asp:TextBox>
            </td>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="textDesc" runat="server" MaxLength="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="center">
                <asp:Button ID="Button1" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td class="oddrow-l" align="center">
                <asp:Button ID="Button2" runat="server" Text="取消" OnClick="btnCancle_Click" CssClass="widebuttons"/>
            </td>
        </tr>
    </table>
</asp:Content>








    