<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_ClientControl_ClientEdit" Codebehind="ClientEdit.ascx.cs" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="menusection-Packages" colspan="4">
            <asp:Label ID="labHeading" runat="server">添加客户</asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            客户信息
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="oddrow">
            客户中文全称：
        </td>
        <td style="width: 30%" class="oddrow-l">
            <asp:TextBox ID="txtChFullName" runat="server" Width="90%" MaxLength="100"></asp:TextBox><font
                color="red"> *</font>
        </td>
        <td style="width: 20%" class="oddrow">
            客户中文简称：
        </td>
        <td style="width: 30%" class="oddrow-l">
            <asp:TextBox ID="txtChShortName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="oddrow">
            客户英文全称：
        </td>
        <td style="width: 30%" class="oddrow-l">
            <asp:TextBox ID="txtEnFullName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
        </td>
        <td style="width: 20%" class="oddrow">
            客户英文简称：
        </td>
        <td style="width: 30%" class="oddrow-l">
            <asp:TextBox ID="txtEnShortName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            Logo：
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:FileUpload ID="fplLogo" runat="server" Width="90%" unselectable="on" />&nbsp;<asp:Image
                ID="imgLogo" runat="server" Height="24px" Width="24px" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            描述：
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:TextBox ID="txtDes" runat="server" Height="60px" Width="90%" TextMode="MultiLine"
                MaxLength="1024"></asp:TextBox>
        </td>
    </tr>
</table>
