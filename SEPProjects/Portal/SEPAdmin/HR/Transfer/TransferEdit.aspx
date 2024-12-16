<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferEdit.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferEdit" MasterPageFile="~/MasterPage.master" %>

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
            $("#ctl00_ContentPlaceHolder1_txtTransInDate").datepicker();

        });
        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '转出部门', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function btnTransInClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=4', { title: '转入部门', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function TransferPositionInClick() {
            var deptid = document.getElementById("<%= hidTransferGroupIn.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

    </script>
    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 15%">转出公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtJob_CompanyName" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidCompanyId" runat="server" />
                <asp:HiddenField ID="hidHeadCountId" runat="server" />

            </td>
            <td class="oddrow" style="width: 15%">转出部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtJob_DepartmentName" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidDepartmentID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转出组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:TextBox ID="txtJob_GroupName" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidGroupId" runat="server" />
                <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJob_GroupName"
                    Display="Dynamic" ErrorMessage="请选择转出组别">请选择转出组别</asp:RequiredFieldValidator>
                <font color="red">*</font>
            </td>


        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtTransferCompanyIn" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidTransferCompanyIn" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">转入部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtTransferDeptIn" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidTransferDeptIn" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:TextBox ID="txtTransferGroupIn" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidTransferGroupIn" runat="server" />
                <input type="button" id="btnTransferIn" class="widebuttons" value="选择..." onclick="btnTransInClick();" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTransferGroupIn"
                    Display="Dynamic" ErrorMessage="请选择转入组别">请选择转入组别</asp:RequiredFieldValidator>
                <font color="red">*</font>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转入职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPosition"
                    Display="Dynamic" ErrorMessage="请选择转入职务" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                <input type="button" id="btnTransferPositionIn" class="widebuttons" value="选择..." onclick="TransferPositionInClick();" />
                <font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 10%">转入日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtTransInDate" runat="server" onkeyDown="return false; " />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="basicSave"
                    ControlToValidate="txtTransInDate" Display="Dynamic" ErrorMessage="请填写转入日期">请填写转入日期</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">基本工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox runat="server" ID="txtSalaryBase"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="basicSave"
                    ControlToValidate="txtSalaryBase" Display="Dynamic" ErrorMessage="请填写基本工资">请填写基本工资</asp:RequiredFieldValidator>

            </td>
            <td class="oddrow" style="width: 10%">绩效工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox runat="server" ID="txtSalaryPromotion"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="basicSave"
                    ControlToValidate="txtSalaryPromotion" Display="Dynamic" ErrorMessage="请填写绩效工资">请填写绩效工资</asp:RequiredFieldValidator>

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
                    ValidationGroup="basicSave" Text=" 提 交 " />
                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false"
                                Text=" 返 回 " />
            </td>
        </tr>

    </table>
    <asp:ValidationSummary ID="basicSave" ShowSummary="false" runat="server" />

</asp:Content>
