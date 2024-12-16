<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditTab.ascx.cs" Inherits="FinanceWeb.UserControls.Project.AuditTab" %>

<script type="text/javascript">
    function changeClass(id) {
        id.className = "button_th";
    }
    function changeClass2(id) {
        id.className = "button_over";
    }
</script>

<table width="100%" id="abc" border="0" cellspacing="0" cellpadding="0" style="border-bottom: solid 1px #15428b;">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" class="button_on" runat="server"
                id="Table2">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab2" runat="server" Text=" 项目号 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table3">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab3" runat="server" Text=" 支持方 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table4">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab4" runat="server" Text=" 付款申请 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table5">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab5" runat="server" Text=" 付款通知 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table6">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab6" runat="server" Text=" 报销 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table7">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab7" runat="server" Text=" 证据链 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table8">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab8" runat="server" Text=" 发票申请 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
             <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table9">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab9" runat="server" Text=" 消耗导入 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table10">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab10" runat="server" Text=" 媒体返点 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
             <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table11">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab11" runat="server" Text=" 退款申请 " EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
