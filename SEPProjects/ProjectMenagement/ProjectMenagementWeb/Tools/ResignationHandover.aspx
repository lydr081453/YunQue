<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResignationHandover.aspx.cs" Inherits="FinanceWeb.Tools.ResignationHandover" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript">
        function ApplicantClick(o) {
            var username = '';
            var sysid = '';
            var dept = '';
            username = encodeURIComponent ? encodeURIComponent(username) : escape(username);
            var win = window.open('/Dialogs/EmployeeList.aspx?showSelectAll=hidden&<% =ESP.Finance.Utility.RequestName.SearchType %>=' + o + '&UserSysID=' + sysid + '&<% =ESP.Finance.Utility.RequestName.UserName %>=' + username + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    </script>
   
    <table class="tableForm" width="100%">
        <tr >
            <td class="heading" colspan="4">离职交接</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%;">离职人：</td>
            <td class="oddrow-l" style="width: 30%;">
                <asp:HiddenField ID="hidUserId" Value="" runat="server" />
                <asp:TextBox ID="txtUser" runat="server" onfocus="this.blur();" /><input type="button"
                    onclick="return ApplicantClick('Monitor');" runat="server" class="widebuttons" value="  选择  " />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="c1" ControlToValidate="txtUser" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="c2" ControlToValidate="txtUser" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 20%;">接收人：</td>
            <td class="oddrow-l" style="width: 30%;">
                <asp:HiddenField ID="hidHead" Value="" runat="server" />
                <asp:TextBox ID="txtProjectHead" runat="server" onfocus="this.blur();" /><input type="button"
                    onclick="return ApplicantClick('ProjectType');" runat="server" class="widebuttons" value="  选择  " />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="c2" ControlToValidate="txtProjectHead" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" CssClass="widebuttons" runat="server" Text="检索数据" ValidationGroup="c1" OnClick="btnSearch_Click" />
                <asp:Button style="margin-left:50px;" ID="btnRun" runat="server" Text="执行更新" CssClass="widebuttons" ValidationGroup="c2" OnClick="btnRun_Click" />
                <asp:Button style="margin-left:50px;" ID="btnReset" runat="server" Text="重置页面" CssClass="widebuttons" OnClick="btnReset_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td><asp:Label ID="lab1" runat="server" /></td>
            <td><asp:Label ID="lab2" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
