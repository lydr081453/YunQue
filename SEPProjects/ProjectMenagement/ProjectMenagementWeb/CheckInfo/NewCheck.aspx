<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="CheckInfo_NewCheck" Codebehind="NewCheck.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
    
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="CheckList.aspx">返回支票列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                支票起始号：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtBegin"></asp:TextBox>
                <font color="red">*</font><asp:RequiredFieldValidator ID="rfvtxtBegin" runat="server"
                    ControlToValidate="txtBegin" ErrorMessage="支票起始号必填"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                支票张数：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtCheckCount" MaxLength="4"></asp:TextBox>
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="txtCheckCount" ErrorMessage="支票张数必填"></asp:RequiredFieldValidator><asp:RangeValidator
                        ID="RangeValidator1" runat="server" ErrorMessage="支票张数必须为数字" ControlToValidate="txtCheckCount"
                        MaximumValue="2000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            </td>
        </tr>
        <asp:ValidationSummary runat="server" ID="ValidationSummary" ShowSummary="false"
            ShowMessageBox="true" DisplayMode="bulletList" />
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="  保存 " CausesValidation="true" OnClick="btnSave_Click"
                    CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnReturn" Text="返回" CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
