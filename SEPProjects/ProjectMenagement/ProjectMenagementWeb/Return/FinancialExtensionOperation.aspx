<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="Return_FinancialExtensionOperation" Codebehind="FinancialExtensionOperation.aspx.cs" %>

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
                客户信息
            </td>
        </tr>
                <tr>
                        <td class="oddrow" style="width: 20%">
                            客户代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtCustomerCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            地址代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddressCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户缩写:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="txtShortEN" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            中文名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameCN1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            英文名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                           <asp:Label ID="txtNameEN1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            地址:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddress1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            发票抬头:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtInvoiceTitle" runat="server" />
                        </td>
                    </tr>
                      <tr>
                    <td class="oddrow" style="width: 20%">
                        联系人姓名:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactName" runat="server" />
                    </td>
                    <td class="oddrow" style="width: 20%">
                        固话:
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:Label ID="txtContactTel" runat="server" />
                    </td>
                </tr>
                <tr>
                  <td class="oddrow">
                        Email:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactEmail" runat="server" />
                    </td>
                    <td class="oddrow">
                        网址
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactWebsite" runat="server" />
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
                备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="txtRemark" Width="80%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan=付款通知审批
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                原付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="txtFactDate" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                延期至:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtExtensionDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr id="trTotal" runat="server">
            <td class="oddrow-l" colspan="4" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 15%; border: 0 0 0 0">
                        </td>
                        <td style="width: 30%; border: 0 0 0 0">
                        </td>
                        <td style="width: 15%; border: 0 0 0 0" align="right">
                            <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 25%; border: 0 0 0 0">
                            <asp:Label ID="lblBlance" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 15%; border: 0 0 0 0">
                        </td>
                    </tr>
                </table>
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
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Height="60px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPass" runat="server" Text=" 审批通过 " 
                    OnClick="btnPass_Click" CssClass="widebuttons" CausesValidation="true" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text=" 审批驳回 " OnClick="btnCancel_Click"
                    CssClass="widebuttons" CausesValidation="true" />&nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
