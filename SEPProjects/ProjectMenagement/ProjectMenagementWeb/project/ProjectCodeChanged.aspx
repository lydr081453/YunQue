<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ProjectCodeChanged.aspx.cs" Inherits="project_ProjectCodeChanged" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" type="text/javascript">
        document.onkeydown = function(event_e) {
            if (window.event)
                event_e = window.event;
            var int_keycode = event_e.charCode || event_e.keyCode;
            if (int_keycode == '13')
                document.getElementById("<% =btnReturn.ClientID %>").click();

        }

    </script>

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
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
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
    </table>
            <table class="tableForm" width="100%">
            <tr>
                <td class="oddrow" width="15%">
                    新的项目号:<a name="top_A" />
                </td>
                <td class="oddrow-1" colspan="3">
                    <asp:TextBox runat="server" ID="txtProjectCode" /><font color="red"> * </font>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtProjectCode" Display="Dynamic" ValidationExpression="^[A-Za-z]{1,1}-([A-Za-z&*]{3,3}|[0-9]{3,3})-[A-Za-z*]{1,1}-([0-9]{7,7}[A-Za-z*]{1,1}|[0-9*]{7,7}|[0-9]{4,4}[*]{3,3})$" runat="server" ErrorMessage="请填写正确的项目号"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtProjectCode" ErrorMessage="请填写新的项目号"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        
    <table width="100%">
        <tr>
            <td align="center">
                <input runat="server" id="btnCommit" value=" 变更 " type="button" onclick="if (commitClick()) { showLoading(); } else { return false; }"
                    causesvalidation="false" class="widebuttons" onserverclick="btnCommit_Click" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
    <script>
        function commitClick() {
            if (Page_ClientValidate()) {
                var newCode = document.getElementById("<%=txtProjectCode.ClientID %>").value;
                if (confirm('您确定要将项目号变更为'+newCode+'吗？')) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
