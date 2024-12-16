<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_ProductLineControl_ProductLineEdit" Codebehind="ProductLineEdit.ascx.cs" %>

    <table width="100%" border="1" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labHeading" runat="server">
                    <asp:Label ID="labClient" runat="server"></asp:Label></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线名称：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtProductLineName" runat="server" Width="80%" MaxLength="50"></asp:TextBox><font
                    color="red"> *</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtProductLineName" Display="None" ErrorMessage="请填写产品线名称"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">所属客户名称：</td>
            <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtCustom" Width="250px" runat="server" Enabled="false" /><font color="red"> * </font><asp:Button ID="btnLink" CausesValidation="false" runat="server" Text="关联客户" CssClass="bigwidebuttons" OnClientClick="openClient();return false;" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtCustom" Display="None" ErrorMessage="请关联客户"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hidCustom" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线图片：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:FileUpload ID="fplTitle" runat="server" Width="80%" Height="24px" unselectable="on" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                描述：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtDes" runat="server" Height="137px" Width="80%" 
                    TextMode="MultiLine" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
    </table>
    
<asp:Panel ID="palClient" runat="server" Style="visibility: hidden; width: 300px; height: 300px">
</asp:Panel>
