<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BankInfo_BankEdit" Codebehind="BankEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function BranchClick() {
            window.__doPostBack = __doPostBack;
            var win = window.open('/Dialogs/BranchDlg.aspx?type=branch', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setBranchInfo(branchId, branchName, branchCode, des) {
            document.getElementById("<%=hidBranchID.ClientID %>").value = branchId;
            document.getElementById("<%=txtBranchName.ClientID %>").value = branchName;
            document.getElementById("<%=txtBankAccountName.ClientID %>").value = branchName; 
            document.getElementById("<%=txtBranchCode.ClientID %>").value = branchCode;
            document.getElementById("<%=labBranchName.ClientID %>").innerHTML = branchName;
            document.getElementById("<%=labBranchCode.ClientID %>").innerHTML = branchCode;
            document.getElementById("<%=labDes.ClientID %>").innerHTML = des;
        }

        function checkBranch() {
            if (document.getElementById("<%=hidBranchID.ClientID %>").value == "") {
                alert(" - 请选择分公司");
                return false;
            } else {
                if (!Page_ClientValidate())
                    return false;
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                分公司信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                公司名称:<asp:HiddenField ID="hidBranchID" runat="server" />
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labBranchName" runat="server" /><font color="red"> * </font><asp:HiddenField ID="txtBranchName"
                    runat="server" />
                &nbsp;<input type="button" causesvalidation="false" id="btnBranch" runat="server"
                    class="widebuttons" onclick="BranchClick();return false;" value="选择" />
            </td>
            <td class="oddrow" style="width: 20%">
                公司代码:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labBranchCode" runat="server" /><asp:HiddenField ID="txtBranchCode"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labDes" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                银行信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                数据库代码:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtDBCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                管理数据库:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtDBManager" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                银行名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBankName" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                    ControlToValidate="txtBankName" ErrorMessage="银行名称为必填" />
            </td>
            <td class="oddrow">
                帐户名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBankAccountName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                帐号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBankAccount" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                    ControlToValidate="txtBankAccount" ErrorMessage="帐号为必填" />
            </td>
            <td class="oddrow">
                地址:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtAddress" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                    ControlToValidate="txtAddress" ErrorMessage="地址为必填" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">银行电话:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPhoneNo" runat="server" />
            </td>
            <td class="oddrow">交换行号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtExchangeNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">银行查询电话:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRequestPhone" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" OnClientClick="return checkBranch();" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
