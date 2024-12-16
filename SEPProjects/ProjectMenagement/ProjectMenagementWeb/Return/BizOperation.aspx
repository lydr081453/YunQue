<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Return_BizOperation" Codebehind="BizOperation.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

    </script>

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
                实际付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label ID="lblFactDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                 <asp:Label ID="lblFactFee" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
             <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
            备注信息:
            </td>
            <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblRemark" runat="server"></asp:Label>
            </td>
        </tr>
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
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Height="60px" Width="80%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPass" runat="server" Text=" 审批通过 " OnClick="btnPass_Click" CssClass="widebuttons" CausesValidation="true" />&nbsp;
                  <asp:Button ID="btnCancel" runat="server" Text=" 审批驳回 " OnClick="btnCancel_Click" CssClass="widebuttons" CausesValidation="true" />&nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false"
                    runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>