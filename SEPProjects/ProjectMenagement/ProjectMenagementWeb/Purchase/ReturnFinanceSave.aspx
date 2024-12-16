<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ReturnFinanceSave.aspx.cs" Inherits="FinanceWeb.Purchase.ReturnFinanceSave" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

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

    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
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
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
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
                申请付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
           <tr>
            <td class="oddrow" style="width: 15%">
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label runat="server" ID="lblPeriodType"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                网银号/支票号
            </td>
            <td class="oddrow-l" style="width: 35%">
                 <asp:Label runat="server" ID="lblPayCode"></asp:Label>
            </td>
        </tr>
          <tr>
                <td class="oddrow" style="width: 15%">
                    成本所属组:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:Label ID="labDepartment" runat="server" />
                </td>
                <td class="oddrow" style="width: 20%">
                </td>
                <td class="oddrow-l" style="width: 30%">
                </td>
            </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPayRemark" runat="server" Width="80%" Height="80px"></asp:TextBox>
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
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtFactFee" runat="server" MaxLength="21"></asp:TextBox>&nbsp;<font
                    color="red">*</font>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="实际付款金额必填" ControlToValidate="txtFactFee" ValidationGroup="audit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
                <font color="red">*</font>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" Text=" 保存 " runat="server" CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>