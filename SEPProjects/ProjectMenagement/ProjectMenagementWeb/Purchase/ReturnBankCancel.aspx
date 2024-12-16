<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ReturnBankCancel.aspx.cs" Inherits="FinanceWeb.Purchase.ReturnBankCancel" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
    function checkValid() {
        var msg = "";
        if (document.getElementById("<%=txtSupplierBank.ClientID %>").value == "") {
            msg += " - 请输入开户行名称！" + "\n";
        }
        if (document.getElementById("<%=txtSupplierAccount.ClientID %>").value == "") {
                msg += " - 请输入开户行帐号！" + "\n";
            }
        if (msg == "") {
            return true;
        }
        else {
            alert(msg);
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
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtReturnContent" runat="server" Width="500px" TextMode="MultiLine"
                    Height="50px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                供应商信息
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">
                重汇原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSugestion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSupplierName" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSupplierBank" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="txtSupplierBank"  Width="40%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblSupplierAccount" runat="server"></asp:Label><br />
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="40%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款申请
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblFee" runat="server"></asp:Label><br />
            </td>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPayType" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款时间:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblPayDate" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <input type="button" id="btnNo" value="提交" runat="server" class="widebuttons" onclick="if(!checkValid()){ return false;}"
                    onserverclick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
