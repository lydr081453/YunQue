<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="project_ProjectAuditedModify" CodeBehind="ProjectAuditedModify.aspx.cs" %>

<%@ Register Src="/UserControls/Project/CustomerInfo.ascx" TagName="CustomerInfo"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PrepareInfo.ascx" TagName="PrepareInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectMember.ascx" TagName="ProjectMember"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectInfo.ascx" TagName="ProjectInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/ProjectContractCost.ascx" TagName="ProjectContractCost"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/PaymentInfo.ascx" TagName="PaymentInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectSupporter.ascx" TagName="ProjectSupporter"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Project/ProjectPercent.ascx" TagName="ProjectPercent"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Project/TopMessage.ascx" TagName="TopMessage" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    
    <script type="text/javascript">
        $().ready(function () {
            var branchid = document.getElementById("<% =hidBranchId.ClientID %>").value;
            $("#<%=ddlBank.ClientID %>").empty();
            project_ProjectAuditedModify.GetBanks(parseInt(branchid), initBank);

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
            }
            else {
            project_ProjectAuditedModify.GetBankModel(id, ChangeBank);
                function ChangeBank(r) {
                    if (r.value != null) {
                        if (r.value[2] != null)
                            document.getElementById("<%=lblAccount.ClientID %>").innerHTML = r.value[2];
                        if (r.value[3] != null)
                            document.getElementById("<%=lblAccountName.ClientID %>").innerHTML = r.value[3]
                        if (r.value[4] != null)
                            document.getElementById("<%=lblBankAddress.ClientID %>").innerHTML = r.value[4];;
                    }
                }
                document.getElementById("<% =hidBankID.ClientID %>").value = id;
            }
        }

    </script>


    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table style="width: 100%">
        <tr>
            <td width="100%" align="left">
                <a name="top_A" /><font color="red">
                    <li>除协议客户的特殊项目外，修改合同总金额、项目成本、支持方将导致该项目重新进入审批流程。</li>
                    <li>进入审批流程后，该项目的一切相关业务活动暂时终止，审批通过后照常进行。</li>
                </font>
            </td>
        </tr>
        <tr>
            <td style="height: 15px"></td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:PrepareInfo ID="PrepareInfo" runat="server" />
                <asp:HiddenField ID="hidOddTotalAmount" runat="server" />
                <asp:HiddenField ID="hidOddCost" runat="server" />
                <asp:HiddenField ID="hidContractStatus" runat="server" />
                <asp:HiddenField ID="hidRechargeAmount" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectMember ID="ProjectMember" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:CustomerInfo ID="CustomerInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectInfo ID="ProjectInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectContractCost ID="ProjectContractCost" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectSupporter ID="ProjectSupporter" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:PaymentInfo ID="PaymentInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px;">
                <uc1:ProjectPercent ID="ProjectPercent" runat="server" />
            </td>
        </tr>
    </table>

    <table class="tableForm" width="100%">
        <tr>
            <td class="oddrow" style="width: 15%">选择开户行:<input type="hidden" runat="server" id="hidBankID" /><input type="hidden" runat="server"
                    id="hidBranchId" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank">
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
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
            <td class="oddrow" style="width: 15%">立项原因:
            </td>
            <td class="oddrow-l" style="width: 85%">
                <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Height="50" Width="60%"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table width="100%">
        <tr>
            <td align="center">
                <%--              <asp:Button ID="Button1" runat="server" Text=" 保存 " OnClick="btnSave_Click" CssClass="widebuttons"
                    CausesValidation="false" />--%>
                <asp:Button ID="btnSave" runat="server" Text=" 提交 " OnClick="btnCommit_Click" CssClass="widebuttons"
                    CausesValidation="false" />&nbsp;
                <asp:Button ID="btnClose" Text=" 返回 " CssClass="widebuttons" OnClick="btnClose_Click"
                    runat="server" CausesValidation="false" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
