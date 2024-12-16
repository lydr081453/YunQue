<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Purchase_PaymentEdit" CodeBehind="PaymentEdit.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) >= 0) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                采购付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
                <input type="hidden" id="hidPrID" runat="server" />
                <input type="hidden" id="hidProjectID" runat="server" />
                <input type="hidden" id="hidPaymentID" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款时间:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                申请付款时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                帐期类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                成本所属组:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
        </tr>
        <asp:Panel runat="server" ID="panParent" Visible="false">
          <tr>
            <td class="heading" colspan="4">
                原单据信息
            </td>
        </tr>
         <tr>
         <td class="oddrow" style="width: 15%">
                原PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblParentPrNo" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                原PR单金额:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblParentPrTotal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
         <td class="oddrow" style="width: 15%">
                原单据号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblParentCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                原付款金额:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblParentAmount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
         
            <td class="oddrow-l" colspan="4">
                <asp:Label style=" font-size:12px; font-weight:bolder;" ID="lblTaxDesc" runat="server"></asp:Label>
            </td>
        </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPayRemark" runat="server" Width="80%" Height="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtOtherRemark" runat="server" Width="80%" Height="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                供应商信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierBank" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款确认
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
            <td class="oddrow">
                审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><asp:LinkButton
                    runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false" ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRInputButton type="button" ID="btnYes" runat="server" value="审批通过" ValidationGroup="audit"
                    class="widebuttons" OnServerClick="btnYes_Click" />
                &nbsp;<prc:CheckPRInputButton type="button" ID="btnNo" runat="server" value="审批驳回"
                    ValidationGroup="audit" class="widebuttons" OnServerClick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
