<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="AddGift.aspx.cs" EnableEventValidation="false" Inherits="PurchaseWeb.Gift.view.AddGift" %>

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
                礼品编号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtCode" runat="server" MaxLength="300"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                礼品名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtName" runat="server" MaxLength="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                礼品积分:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPorints" runat="server" MaxLength="300"></asp:TextBox>
            </td>
            <td class="oddrow">
                礼品数量:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCount" runat="server" MaxLength="300" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                状态:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Text="待确认" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="活动开始" Value="1"></asp:ListItem>
                    <asp:ListItem Text="活动结束" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtDesc" runat="server" MaxLength="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                附件:
            </td>
            <td class="oddrow-l">
                <input type="file" class="widebuttons" id="btnImage" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td class="oddrow-l" align="center">
                <asp:Button ID="Button1" runat="server" Text="取消" OnClick="btnCancle_Click" CssClass="widebuttons" />
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
