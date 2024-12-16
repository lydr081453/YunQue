<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectTab.ascx.cs" Inherits="UserControls_Project_ProjectTab" %>
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
            <table id="tabProject" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_on">
                <tr>
                    <td><a href="/Search/ProjectTabList.aspx?Type=project">项目号</a></td>
                </tr>
            </table>
            <table id="tabSupporter" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Search/SupporterTabList.aspx?Type=supporter">支持方</a></td>
                </tr>
            </table>
            <table id="tabReturn" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Search/ReturnTabList.aspx?Type=return">付款申请</a></td>
                </tr>
            </table>
            <table id="tabOOP" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Search/OOPTabList.aspx?Type=oop">报销申请</a></td>
                </tr>
            </table>
            <table id="tabPayment" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Search/NotifyTabList.aspx?Type=payment">付款通知</a></td>
                </tr>
            </table>
            <table id="tabConsumption" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Consumption/ConsumptionList.aspx?Type=csm">消耗导入</a></td>
                </tr>
            </table>
            <table id="tabRebateRegistration" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/RebateRegistration/RebateRegistrationList.aspx?Type=rr">媒体返点</a></td>
                </tr>
            </table>
             <table id="tabRefund" runat="server" border="0" cellpadding="0" cellspacing="0" class="button_over">
                <tr>
                    <td><a href="/Search/RefundTabList.aspx?Type=refund">退款申请</a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
