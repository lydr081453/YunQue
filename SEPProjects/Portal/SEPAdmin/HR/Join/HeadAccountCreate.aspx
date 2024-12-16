<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountCreate.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <link href="/public/css/jquery-ui-new.css" rel="stylesheet" />

    <script type="text/javascript" src="/public/js/jquery1.12.js"></script>

    <script type="text/javascript" src="/public/js/jquery-ui-new.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/jquery.ui.datepicker.cn.js"></script>


    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#ctl00_ContentPlaceHolder1_txtDimissionDate").datepicker();
        });

        function btnClick() {
            var deptid = document.getElementById("<%=hidDeptId.ClientID %>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职务列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
    </script>

    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                部门:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                职务:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidDeptId" runat="server" />
                <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                <input type="button" id="btnPosition" class="widebuttons" value=" 选择职务 " onclick="btnClick();" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                职级:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblLevel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                工资范围:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                服务客户:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtCustomer" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                业务新增:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkCreate" Text="立项" />&nbsp;&nbsp;<asp:CheckBox
                    runat="server" ID="chkUnCreate" Text="未立项" />
            </td>
        </tr>
        <tr runat="server" id="trReplace1">
            <td class="oddrow" style="width: 10%">
                被替员工:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblReplaceUser"></asp:Label><asp:HiddenField runat="server" ID="hidPosition"/>
            </td>
        </tr>
        <tr runat="server" id="trReplace2">
            <td class="oddrow" style="width: 10%">
                替换原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtReplaceReason" TextMode="MultiLine" Height="100px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="trReplace3">
            <td class="oddrow" style="width: 10%">
                离岗日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtDimissionDate" runat="server" onkeyDown="return false; "  />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                职级选择:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkAAD" Text="AAD（含）以上，由HR总监最终打印文件" />
            </td>
        </tr>
        <tr>
            <td>
                工作职责:
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtResponse" TextMode="MultiLine" Height="100px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                资格需求:
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtRequestment" TextMode="MultiLine" Height="100px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                备注:
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="100px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button runat="server" ID="btnCommit" Text=" 提交 "  OnClientClick="showLoading(); " OnClick="btnCommit_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
