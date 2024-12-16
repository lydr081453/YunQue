<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketTab.ascx.cs" Inherits="FinanceWeb.UserControls.Project.TicketTab" %>

<script type="text/javascript">
    function changeClass(id) {
        id.className = "button_th";
    }
    function changeClass2(id) {
        id.className = "button_over";
    }
</script>

<table width="100%" border="0" id="abc" cellspacing="0" cellpadding="0" style="border-bottom: solid 1px #15428b;">
    <tr>
        <td>
            <table id="tabProject" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="button_on">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab1" runat="server" NavigateUrl="/Edit/ProjectTabEdit.aspx?Type=project"
                            Text="项目号" />
                    </td>
                </tr>
            </table>
            <table id="tabSupporter" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab2" runat="server" NavigateUrl="/Edit/SupporterTabEdit.aspx?Type=supporter"
                            Text="支持方" />
                    </td>
                </tr>
            </table>
            <table id="tabReturn" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab3" runat="server" NavigateUrl="/Edit/ReturnTabEdit.aspx?Type=return"
                            Text="付款申请" />
                    </td>
                </tr>
            </table>
            <table id="tabOOP" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab4" runat="server" NavigateUrl="/Edit/OOPTabEdit.aspx?Type=oop"
                            Text="报销申请" />
                    </td>
                </tr>
            </table>
            <table id="tabPayment" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab5" runat="server" NavigateUrl="/Edit/NotifyTabEdit.aspx?Type=payment"
                            Text="付款通知" />
                    </td>
                </tr>
            </table>
            <table id="tabPrDepayment" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab6" runat="server" NavigateUrl="/Edit/PrDePayment.aspx?Type=PrDepayment"
                            Text="PR现金冲销" />
                    </td>
                </tr>
            </table>
            <table id="tabTicket" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="tab7" runat="server" NavigateUrl="/Edit/TicketReceipient.aspx?Type=Ticket"
                            Text="机票申请" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
