<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="KillTraffic.aspx.cs" Inherits="Purchase_KillTraffic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/Purchase/TrafficDetail.ascx" tagname="TrafficDetail"
        tagprefix="uc1" %>
    <uc1:TrafficDetail ID="TrafficDetail" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                Traffic Fee销账
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                销账金额:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPrice" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPrice"
                    Display="none" ErrorMessage="销账金额为必填项"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpresionValidator1" runat="server" ControlToValidate="txtPrice"
                    ValidationExpression="^(\d{1,3},?)+(\.\d+)?$" ErrorMessage="销账金额输入有误" Display="None"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
           <td class="oddrow" width="15%">
                销账备注:
            </td>
             <td class="oddrow-l" width="85%">
                <asp:TextBox ID="txtRemark" Width="60%" Height="60" runat="server" TextMode="MultiLine"></asp:TextBox>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l" align="center">
                <asp:Button ID="btnSubmit" runat="server" CssClass="widebuttons" Text=" 销账 " OnClick="btnSubmit_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
