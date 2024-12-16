<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeBehind="RecentActity.aspx.cs" Inherits="SEPAdmin.Actity.RecentActity" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table width="40%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 124px">
                培训标题:
            </td>
            <td>
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </td>
            <td>
                培训时间:
            </td>
            <td>
                <asp:Label runat="server" ID="lblTime"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                培训内容:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtContent" runat="server" ReadOnly="true" MaxLength="4000" Height="84px" Width="842px"
                    Columns="76" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                培训讲师:
            </td>
            <td>
                <asp:Label runat="server" ID="lblLecturer"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblId" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnSubmit" Text="抢座" runat="server" onclick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
