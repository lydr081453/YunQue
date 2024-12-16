<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetNextAuditer.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.SetNextAuditer" MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script language="javascript">
    function NextUserSelect() {
        var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setnextAuditor(name, sysid) {
        document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
        document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
    }
</script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            审批信息
        </td>
    </tr>
    
    <tr id="trNext" runat="server">
        <td class="oddrow" style="width:20%">
            下级审批人:
        </td>
        <td class="oddrow-l" colspan="3" style="width:80%">
            <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                color="red"> * </font>
            <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
            <asp:HiddenField ID="hidNextAuditor" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择下级审批人!" ControlToValidate="txtNextAuditor" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trBatchNo" runat="server">
        <td class="oddrow" style="width:20%">
            批次号:
        </td>
        <td class="oddrow-l" colspan="3" style="width:80%">
            <asp:TextBox ID="txtBatchNo" runat="server"/><font color="red"> * </font>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写批次号!" ControlToValidate="txtBatchNo" Display="None"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4">
            <asp:Button ID="btnAudit" runat="server" Text=" 确定 " CssClass="widebuttons" OnClick="btnAudit_Click"/>&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
        </td>
    </tr>
</table>
</asp:Content>
