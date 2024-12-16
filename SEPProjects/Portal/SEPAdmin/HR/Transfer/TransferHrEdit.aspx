<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferHrEdit.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferHrEdit" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="/public/css/jquery-ui-new.css" />

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>
    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/jquery1.12.js"></script>
    <script type="text/javascript" src="/public/js/jquery-ui-new.js"></script>
    <script type="text/javascript" src="/public/js/jquery.ui.datepicker.cn.js"></script>

    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#ctl00_ContentPlaceHolder1_txtTransOutDate").datepicker();

        });

        function UserClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/EmployeeDlg.aspx?deptid=' + deptid + '&istrans=1', { title: '转出员工', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
    </script>
    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 15%">转出公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label runat="server" ID="lblTransOutCompany"></asp:Label>

            </td>
            <td class="oddrow" style="width: 15%">转出部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransOutDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转出组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:HiddenField  runat="server" ID="hidGroupId"/>
                <asp:Label runat="server" ID="lblTransOutGroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInCompany"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">转入部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label runat="server" ID="lblTransInDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:Label runat="server" ID ="lblTransInGroup"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转入职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInPosition"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">转入日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
              <asp:Label runat="server" ID="lblTransInDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">基本工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalaryBase"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">绩效工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
               <asp:Label runat="server" ID="lblSalaryPromotion"></asp:Label>

            </td>
        </tr>

        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
               <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转出员工:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:HiddenField ID="hidTransUserId" runat="server" />
                <asp:TextBox ID="txtTransUser" runat="server" onkeyDown="return false; " />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTransUser"
                    Display="Dynamic" ErrorMessage="请选择转出员工" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                <input type="button" id="btnTransUser" class="widebuttons" value="选择..." onclick="UserClick();" />
                <font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 10%">转出日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtTransOutDate" runat="server" onkeyDown="return false; " />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="basicSave"
                    ControlToValidate="txtTransOutDate" Display="Dynamic" ErrorMessage="请填写转出日期">请填写转出日期</asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="50" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnCommit" runat="server" CssClass="widebuttons" OnClick="btnCommit_Click"
                    ValidationGroup="basicSave" Text=" 确 认 " />
                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                                Text=" 返 回 " />
            </td>
        </tr>

    </table>
    <asp:ValidationSummary ID="basicSave" ShowSummary="false" runat="server" />

</asp:Content>
