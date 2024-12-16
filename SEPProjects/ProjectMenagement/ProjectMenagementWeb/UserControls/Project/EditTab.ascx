<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditTab.ascx.cs" Inherits="FinanceWeb.UserControls.Project.EditTab" %>

<script type="text/javascript">
    function changeClass(id) {
        id.className = "button_th";
    }
    function changeClass2(id) {
        id.className = "button_over";
    }


    function ShowCount(link, count) {
        var labels = link.children('label');
        if (labels.size() == 2) {
            labels.eq(1).text(count > 0 ? "(" + count + ")" : "").appendTo(link);
            return;
        }
        var text = link.text();
        link.empty();

        var lbl1 = $("<span></span>");
        lbl1.text(text).appendTo(link);
        lbl1.css({ 'float': 'left', 'cursor': 'pointer' });

        var lbl2 = $("<span></span>");
        lbl2.text(count > 0 ? "(" + count + ")" : "").appendTo(link);
        lbl2.css({ 'float': 'left', color: 'red', width: '40px', 'text-align': 'center', 'cursor': 'pointer' });
    }

    $(document).ready(function() {
        var EditTab = FinanceWeb && FinanceWeb.UserControls && FinanceWeb.UserControls.Project && FinanceWeb.UserControls.Project.EditTab;
        if (!EditTab)
            return;

        ShowCount($("#<%= Tab1.ClientID %>"), 0);
        ShowCount($("#<%= Tab2.ClientID %>"), 0);
        ShowCount($("#<%= Tab3.ClientID %>"), 0);
        ShowCount($("#<%= Tab4.ClientID %>"), 0);
        ShowCount($("#<%= Tab5.ClientID %>"), 0);
        ShowCount($("#<%= Tab6.ClientID %>"), 0);
        ShowCount($("#<%= Tab7.ClientID %>"), 0);

        EditTab.GetCounts(function(r) {
            var v = r.value;
            ShowCount($("#<%= Tab1.ClientID %>"), v["project"]);
            ShowCount($("#<%= Tab2.ClientID %>"), v["supporter"]);
            ShowCount($("#<%= Tab3.ClientID %>"), v["return"]);
            ShowCount($("#<%= Tab4.ClientID %>"), v["oop"]);
            ShowCount($("#<%= Tab5.ClientID %>"), v["notify"]);
            ShowCount($("#<%= Tab6.ClientID %>"), v["depayment"]);
            ShowCount($("#<%= Tab7.ClientID %>"), v["refund"]);
        });
    });
   
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
             <table id="tabRefund" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="button_over">
                <tr>
                    <td>
                        <asp:HyperLink ID="Tab7" runat="server" NavigateUrl="/Edit/RefundTabEdit.aspx?Type=Refund"
                            Text="退款申请" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
