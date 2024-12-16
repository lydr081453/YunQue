<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="InvoiceInfo_NewInvoice" Codebehind="NewInvoice.aspx.cs" %>

<%@ Register Src="~/UserControls/Customer/CustomerInfo.ascx" TagName="Customer" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Branch/BranchInfo.ascx" TagName="Branch" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script>
        function ReceiverClick() {
            var username = document.getElementById("<% =Prepareinfo_txtApplicant.ClientID %>").value;
            var sysid = document.getElementById("<% =Prepareinfo_hidApplicantID.ClientID %>").value;
            username = encodeURIComponent ? encodeURIComponent(username) : escape(username);
            var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.SearchType %>=Applicant&UserSysID=' + sysid + '&<% =ESP.Finance.Utility.RequestName.UserName %>=' + username, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <link href="/public/css/css.css" rel="stylesheet" type="text/css" />
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="heading" colspan="4">
                发票信息
            </td>
        </tr>
        <tr>
            <td height="25" align="left" class="oddrow" style="width: 15%">
                发票号
            </td>
            <td height="25" align="left" class="oddrow-l">
                <asp:TextBox ID="txtInvoiceCode" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvInvoiceCode" runat="server" ControlToValidate="txtInvoiceCode"
                    Display="None" ErrorMessage="发票号为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            </td>
            <td class="oddrow">
                接收人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="Prepareinfo_txtApplicant" runat="server" onkeyDown="return false; "
                    Style="cursor: hand" CssClass="inputLabel" Width="200px"></asp:TextBox><input type="button" id="btnApplicant"
                        onclick="return ReceiverClick();" class="widebuttons" value="选择" /><font color="red">*</font>
                <input type="hidden" id="Prepareinfo_hidApplicantID" runat="server" />
                <input type="hidden" id="Prepareinfo_hidApplicantUserID" runat="server" />
                <input type="hidden" id="Prepareinfo_hidApplicantUserCode" runat="server" />
                <input type="hidden" id="Prepareinfo_hidGroupID" runat="server" />
                <asp:TextBox runat="server" ID="Prepareinfo_txtGroup" Style="display: none"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td height="25" class="oddrow" style="width: 15%" align="left">
                发票金额
            </td>
            <td height="25" class="oddrow-l" style="width: 35%" align="left">
                <asp:TextBox ID="txtInvoiceAmounts" runat="server" Width="200px"></asp:TextBox><font
                    color="red">*</font>
                <asp:RequiredFieldValidator ID="rfvInvoiceAmount" runat="server" ControlToValidate="txtInvoiceAmounts"
                    Display="None" ErrorMessage="发票金额为必填"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revInvoiceAmount" runat="server" ControlToValidate="txtInvoiceAmounts"
                    ValidationExpression="([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])"
                    ErrorMessage="发票金额输入有误" Display="None"></asp:RegularExpressionValidator>
            </td>
            <td height="25" class="oddrow" style="width: 15%" align="left">
                美元差额
            </td>
            <td height="25" class="oddrow" style="width: 35%" align="left">
                <asp:TextBox ID="txtUSDDiffer" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="15%" class="oddrow">
                备注
            </td>
            <td height="25" width="*" align="left" colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtRemark" runat="server" Width="90%" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <uc1:Branch runat="server" ID="ProjectInfo" />
    <uc1:Customer runat="server" ID="CustomerInfo" />
    <table class="tableform" width="100%">
        <tr>
            <td height="25" colspan="4">
                <div align="center">
                    <asp:Button ID="btnAdd" runat="server" Text=" 保存 " CssClass="widebuttons"  OnClick="btnAdd_Click"></asp:Button>
                    <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                        CausesValidation="false" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
