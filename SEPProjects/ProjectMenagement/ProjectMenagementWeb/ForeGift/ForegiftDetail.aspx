<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ForegiftDetail.aspx.cs" Inherits="ForeGift_ForegiftDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%@ register src="../UserControls/ForeGift/ViewForeGift.ascx" tagname="ViewForeGift"
        tagprefix="uc1" %>
<uc1:ViewForeGift ID="ViewForeGift" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
