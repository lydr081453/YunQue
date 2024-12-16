<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="project_FinancialSupporter" CodeBehind="FinancialSupporter.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Project/TopMessage.ascx" tagname="TopMessage" tagprefix="uc1" %>

    <script language="javascript" type="text/javascript">

        function AuditCheck() {
            var msg = "";
            if (document.getElementById("<%=txtAuditRemark.ClientID %>").value == "") {
                msg += "请填写审批批示!" + "\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("您确定要审批该支持方申请单吗?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>

    <%@ register src="../UserControls/Project/SupporterInfoDisplay.ascx" tagname="SupporterInfoDisplay"
        tagprefix="uc1" %>
    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="FinancialSupporterList.aspx">返回支持方列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:SupporterInfoDisplay ID="SupporterInfoDisplay" runat="server" />
            </td>
        </tr>
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
            <td class="oddrow" width="15%">
                审批批示:
            </td>
            <td class="oddrow-1" colspan="3">
                <asp:TextBox ID="txtAuditRemark" runat="server" TextMode="MultiLine" Height="80px"
                    Width="80%"></asp:TextBox><asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font color="red">*</font>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSubmit" runat="server" Text=" 审批通过 " CssClass="widebuttons" ValidationGroup="audit"
                    OnClientClick="return AuditCheck();" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnReject" runat="server" Text=" 审批驳回 " CssClass="widebuttons" ValidationGroup="audit"
                    OnClientClick="return AuditCheck();" OnClick="btnTerminate_Click" />
                <asp:Button ID="btnEdit" runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
