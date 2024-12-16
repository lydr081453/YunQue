<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TaxDetailEdit.aspx.cs" Inherits="FinanceWeb.project.TaxDetailEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="js/iframeTools.js"></script>

    <script language="javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                进项税信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                PN单号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
                <input type="button" value=" 选择单号 " onclick="art.dialog.open('PnTaxSelect.aspx', {title: '选择项目号',width:600, height:400,background: '#BFBFBF',opacity: 0.7,lock:true});" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtFactFee" Enabled="false"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 20%">
                税金:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtTax"></asp:TextBox>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblFactDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                税费发生日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtTaxDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                成本所属组:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
            </td>
             <td class="oddrow" style="width: 15%">
                供应商:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblSupplier" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRInputButton type="button" ID="btnYes" runat="server" value=" 保存 " ValidationGroup="audit"
                    class="widebuttons" OnServerClick="btnYes_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
