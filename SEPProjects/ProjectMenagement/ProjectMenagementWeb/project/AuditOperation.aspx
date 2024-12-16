<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_AuditOperation" CodeBehind="AuditOperation.aspx.cs" %>

<%@ Register Src="/UserControls/Project/CustomerDisplay.ascx" TagName="CustomerDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="PrepareDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMemberDisplay.ascx" TagName="ProjectMemberDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PaymentDisplay.ascx" TagName="PaymentDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectInfoView.ascx" TagName="ProjectInfoView"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectContractCostView.ascx" TagName="ProjectContractCostView"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectSupporterDisplay.ascx" TagName="ProjectSupporterDisplay"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

        function ShowHist() {
            var win = window.open('ProjectHist.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function ConfirmAudit() {
            var msg = "";
            //if (!Checking()) {
            //    msg += "您还没有查阅合同附件!" + "\n";
            //}
            if (document.getElementById("<%=txtAuditRemark.ClientID %>").value == "") {
                msg += "请填写审批批示!" + "\n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("您确定要审批该项目号申请单吗?")) {
                    showLoading();
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function confirmReturn() {
            if ( confirm('您确定要驳回该项目号申请单吗?')) {
                showLoading();
                return true;
            } else {
                return false;
            }
        }
    </script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_05.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="AuditList.aspx">返回项目号列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:CustomerDisplay ID="CustomerDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectInfoView ID="ProjectInfoView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectContractCostView ID="ProjectContractCostView" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectSupporterDisplay ID="ProjectSupporterDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PaymentDisplay ID="PaymentDisplay" runat="server" />
            </td>
        </tr>
        <tr>
    </table>
    <table class="tableForm" width="100%">
        <tr>
            <td class="oddrow" width="15%">
                审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtAuditRemark" runat="server" TextMode="MultiLine" Height="80px"
                    Width="70%"></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                        color="red">*</font>
            </td>
        </tr>
    </table>
     <table id="tabReason" runat="server" class="tableForm" width="100%" visible="false">
        <tr>
            <td class="oddrow-l" colspan="2">
               <asp:Label runat="server" ID="lblTip" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                立项原因:
            </td>
            <td class="oddrow-l" style="width: 85%">
                <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Height="50" Width="60%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnConfirm" runat="server" Text=" 审批通过 " CssClass="widebuttons" OnClientClick="return ConfirmAudit();"
                    OnClick="btnConfirm_Click" />
                <asp:Button ID="btnTerminate" runat="server" Text=" 审批驳回 " CssClass="widebuttons"
                    OnClientClick="return confirmReturn();" OnClick="btnTerminate_Click" />
                <asp:Button ID="btnEdit" runat="server" Text=" 编辑 " CssClass="widebuttons" Visible="false" OnClick="btnEdit_Click" />
                <asp:Button ID="btnHist" runat="server" Text=" 项目历史 " CssClass="widebuttons" OnClientClick="return ShowHist();" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回  " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
