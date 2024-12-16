<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Branch_BranchInfo" Codebehind="BranchInfo.ascx.cs" %>
<script type="text/javascript">
    function BranchClick() {
        var win = window.open('/Dialogs/BranchDlg.aspx?<% =ESP.Finance.Utility.RequestName.NotPostBack %>=true', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
</script>
<table width="100%" class="tableForm">
        <tr>
        <td class="heading" colspan="4">
           公司信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:15%"> 
            公司选择:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox runat="server" onkeyDown="return false; "  CssClass="inputLabel" Enabled="true"  ID="txtBranchName"
                Width="40%" /><input type="button" id="btnBranchSelect" class="widebuttons" onclick="return BranchClick();"
                    class="widebuttons" value="搜索" />&nbsp;<font color="red">*</font><asp:RequiredFieldValidator
                        ID="rfvBranch" runat="server" ControlToValidate="txtBranchName" ErrorMessage="公司必填" Display="None"
                        ></asp:RequiredFieldValidator>
            <asp:HiddenField ID="hidBranchID" runat="server" />
            <asp:HiddenField ID="hidBranchCode" runat="server" />
        </td>
    </tr>
</table>
