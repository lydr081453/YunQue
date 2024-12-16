<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="project_FinancialAuditOperation" CodeBehind="FinancialAuditOperation.aspx.cs" %>

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
        function CreateProjectCode() {
            var pid = '<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>';
            var pcode = project_FinancialAuditOperation.CreateProjectCode(pid).value;
            document.getElementById("<%=txtProjecCode.ClientID %>").value = pcode;
        }

        function AuditCheck(checkCode) {
            var msg = "";
            //if (!Checking()) {
            //    msg += "您还没有查阅合同附件!" + "\n";
            //}
            if (document.getElementById("<%=txtAuditRemark.ClientID %>").value == "") {
                msg += "请填写审批批示!" + "\n";
            }
            if (document.getElementById("<%=pnlCode.ClientID %>").style.display == "block" && checkCode == true) {
                if (document.getElementById("<%=txtProjecCode.ClientID %>").value == "") {
                    msg = "请先生成项目号!" + "\n";
                }
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
        function ShowHist() {
            var win = window.open('ProjectHist.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function confirmReturn() {
            if (confirm('您确定要驳回该项目号申请单吗?')) {
                showLoading();
                return true;
            } else {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        $().ready(function() {
            var branchid = document.getElementById("<% =hidBranchId.ClientID %>").value;
            $("#<%=ddlBank.ClientID %>").empty();
            project_FinancialAuditOperation.GetBanks(parseInt(branchid), initBank);

            function initBank(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                    if (r.value[k][0] == $("#<%=hidBankID.ClientID %>").val()) {
                        $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                    }
                    else {
                        $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                    }
                }
            }
        });

        function selectBank(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBankID.ClientID %>").value = "";
                document.getElementById("<%=lblAccount.ClientID %>").innerHTML = "";
                document.getElementById("<%=lblAccountName.ClientID %>").innerHTML = "";
                document.getElementById("<%=lblBankAddress.ClientID %>").innerHTML = "";
            }
            else {
                project_FinancialAuditOperation.GetBankModel(id, ChangeBank);
                function ChangeBank(r) {
                    if (r.value != null) {
                        if (r.value[2] != null)
                            document.getElementById("<%=lblAccount.ClientID %>").innerHTML = r.value[2];
                        if (r.value[3] != null)
                            document.getElementById("<%=lblAccountName.ClientID %>").innerHTML = r.value[3]
                        if (r.value[4] != null)
                            document.getElementById("<%=lblBankAddress.ClientID %>").innerHTML = r.value[4]; ;
                    }
                }
                document.getElementById("<% =hidBankID.ClientID %>").value = id;
            }
        }
        
        function RelevanceProjectCode(obj) {
            PageMethods.SetRelevanceProjectId($(obj).val(),function(val){
                if (val == "error") {
                    alert("关联项目号错误");
                    $(obj).val('');
                    $("#<%= hidRelevanceProjectId.ClientID%>").val('');
                }
                else
                    $("#<%= hidRelevanceProjectId.ClientID%>").val(val);
            });
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
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="FinancialAuditList.aspx">返回项目号列表</asp:HyperLink></li>
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
    </table>
    <asp:Panel ID="pnlCode" runat="server" Style="display: none">
        <table class="tableForm" width="100%">
            <tr>
                <td class="oddrow" width="15%">
                    项目号生成:<a name="top_A" />
                </td>
                <td class="oddrow-l">
                    <asp:TextBox runat="server" ID="txtProjecCode"></asp:TextBox><asp:Button runat="server"
                        ID="btnCreate" CssClass="widebuttons" Text="生成" OnClick="btnCreate_Click" />
                    <asp:Label runat="server" ID="lblBD" ForeColor="Red"></asp:Label>
                </td>
                <td class="oddrow" width="15%">
                    关联项目号：
                </td>
                <td class="oddrow-l" width="35%">
                    <asp:TextBox ID="txtRelevanceProjectCode" runat="server" onchange="RelevanceProjectCode(this);" />
                    <asp:HiddenField ID="hidRelevanceProjectId" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
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
            <td class="oddrow" style="width: 15%">
                选择开户行:<input type="hidden" runat="server" id="hidBankID" /><input type="hidden" runat="server"
                    id="hidBranchId" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank" style="width:250px;">
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">
                帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
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
                    Width="70%"></asp:TextBox><font color="red">*</font>
                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ForeColor="Red" Font-Underline="true" Font-Bold="true" ToolTip="暂时留个Message,以后再操作">留言</asp:LinkButton>
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
            <td class="oddrow-l" style="width:85%">
                <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Height="50" Width="60%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnConfirm" runat="server" Text=" 审批通过 " CssClass="widebuttons" OnClientClick="return AuditCheck(true);"
                    ValidationGroup="audit" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnWaiting" runat="server" Text=" 等待合同 " CssClass="widebuttons" OnClientClick="return AuditCheck(false);"
                    OnClick="btnWaiting_Click" />
                <asp:Button ID="btnTerminate2" runat="server" Text=" 驳回到上一级 " CssClass="widebuttons"
                    OnClientClick="return confirmReturn();" ValidationGroup="audit" OnClick="btnTerminate2_Click" />
                <asp:Button ID="btnTerminate" runat="server" Text=" 审批驳回 " CssClass="widebuttons"
                    OnClientClick="return confirmReturn();" ValidationGroup="audit" OnClick="btnTerminate_Click" />
                <asp:Button ID="btnHist" runat="server" Text=" 项目历史 " CssClass="widebuttons" OnClientClick="return ShowHist();" />
                <asp:Button ID="btnEdit" runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
