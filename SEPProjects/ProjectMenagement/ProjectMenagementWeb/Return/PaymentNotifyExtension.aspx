<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Return_PaymentNotifyExtension" EnableEventValidation="false" CodeBehind="PaymentNotifyExtension.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Project/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function checkremark() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtRemark").value == "") {
                alert("请填写延期付款原因！");
                return false;
            }
            return true;
        }
    </script>

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                项目信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目所属组:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblGroupName" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectType" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                业务类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBizType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                合同状态:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContractStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                公司代码:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                公司名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款周期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblPaymentCircle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                负责人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目负责人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponser" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                负责人电话:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                负责人邮箱:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserEmail" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                负责人手机:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblResponserMobile" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款通知信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款通知内容:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentContent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentPreDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                预计付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款延期至:</td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtExtension" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExtension"
                    ErrorMessage="延期付款日期必填"></asp:RequiredFieldValidator></td>
            <%--<td class="oddrow" style="width: 15%">
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtFactDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" /><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFactDate"
                    ErrorMessage="实际付款日期必填"></asp:RequiredFieldValidator>
            </td>--%>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                延期原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="60px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" />
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 提交 " OnClick="btnSave_Click" CssClass="widebuttons" OnClientClick="return checkremark();"
                    CausesValidation="true" />&nbsp;
                <asp:Button ID="btnNext" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();"
                    CausesValidation="false" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
