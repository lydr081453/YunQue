<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseAccountDetailView.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.ExpenseAccountDetailView" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/treeStyle.css" rel="stylesheet" type="text/css" />

     <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">单据信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PN号:
            </td>
            <td class="oddrow-l" style="width: 85%" colspan="3">
                <asp:Label runat="server" ID="labPNcode" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">申请人:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
            </td>
            <td class="oddrow" width="15%">申请日期:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label runat="server" ID="labRequestDate" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">项目号:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labProjectCode" />
            </td>
            <td class="oddrow">项目名称:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labProjectName" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">成本所属组:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labGroup" />
            </td>
            <td class="oddrow">预计金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">单据类型:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="labReturnType" />
            </td>
        </tr>
        <%--<tr>
        <td class="oddrow">
            备注:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="labMemo" />
        </td>
    </tr>--%>
    </table>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">明细项信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">成本明细:
            </td>
            <td class="oddrow-l" width="85%" colspan="3">
                <asp:Label ID="labProjectType" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="oddrow" width="15%">费用发生日期:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:TextBox ID="txtExpenseDate" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();"></asp:TextBox>
            </td>
            <td class="oddrow" width="15%">费用类型:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label runat="server" ID="labExpenseType" />
            </td>
        </tr>
        <asp:Panel runat="server" ID="panMealFee">
            <tr>
                <td class="oddrow">&nbsp;</td>
                <td class="oddrow-l" colspan="3">
                    <asp:CheckBox runat="server" ID="chkMealFee1" Text="早餐" />&nbsp;
                <asp:CheckBox runat="server" ID="chkMealFee2" Text="午餐" />&nbsp;
                <asp:CheckBox runat="server" ID="chkMealFee3" Text="晚餐" />
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel runat="server" ID="panPhone">
            <tr>
                <td class="oddrow">预计报销手机费年月:</td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label runat="server" ID="labYear" />年 &nbsp;<asp:Label runat="server" ID="labMonth" />月
                </td>
            </tr>
            <tr>
                <td class="oddrow">手机发票类型:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList runat="server" ID="ddlPhoneInvoice">
                        <asp:ListItem Text="普通发票" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="电子发票" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtInvoiceNo" MaxLength="8"></asp:TextBox>电子发票需输入8位发票号
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">费用描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <%--<asp:Label runat="server" ID="labExpenseDesc" />--%>
                <asp:TextBox runat="server" ID="txtExpenseDesc" TextMode="MultiLine" Rows="3" Width="40%" MaxLength="1500" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">单位数量:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labExpenseTypeNumber" />
            </td>
            <td class="oddrow">金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labExpenseMoney" />
                <asp:TextBox runat="server" ID="txtMoney"></asp:TextBox>
                元
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>


</asp:Content>
