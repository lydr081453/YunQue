<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountInterview.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountInterview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="/public/css/jquery-ui-new.css">

    <script type="text/javascript" src="/public/js/jquery-3.7.1.js"></script>
    <script src="../../public/js/jquery-ui-new.js"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="/HR/Employees/js/UserDepartment.js"></script>

    <script type="text/javascript" src="/public/js/jquery.ui.datepicker.cn.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript">

        function AuditerClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/EmployeeDlg.aspx?deptid=' + deptid, { title: '审核人列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function KaoqinClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/EmployeeDlg.aspx?iskaoqin=1&deptid=' + deptid, { title: '审核人列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function DimissionClick() {
            art.dialog.open('/HR/Employees/DimissionEmployeeDlg.aspx', { title: '离职人员列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        $(function () {
            // $('#container-1').tabs();
        });

        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '部门列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function PositionClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#" + "<%=txtBirthday.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtJob_JoinDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });

        });

        function onPageSubmit() {
            onSubmit();
            var nameValue = document.getElementById("<%= SelectedModuleName.ClientID%>").value;
            var idValue = document.getElementById("<%= SelectedModuleArr.ClientID%>").value;

            var nameValues = nameValue.split('-');

            var idValues = idValue.split('-');


            if (idValues.length == 1 && nameValues.length == 1 && nameValues[0] != "" && idValues[0] != "") {
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";
            }
            if (idValues.length == 2 && nameValues.length == 2) {
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";
            }
            if (idValues.length == 3 && nameValues.length == 3) {
                document.getElementById("<%= hidGroupId.ClientID%>").value = idValues[0];
            }

        }

    </script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <table width="100%" class="tableForm" style="margin: 20px 0px 20px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">申请人:<asp:HiddenField runat="server" ID="hidGroupId" />
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblCreator"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">申请日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblAppDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">部门:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">职务:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblPosition"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">职级:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblLevel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">工资范围:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>是否AAD以上:
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="chkAAD" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">服务客户:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblCustomer"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">业务新增:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkCreate" Text="立项" Enabled="false" />&nbsp;&nbsp;<asp:CheckBox
                    runat="server" ID="chkUnCreate" Text="未立项" Enabled="false" />
            </td>
        </tr>
        <tr runat="server" id="trReplace1">
            <td class="oddrow" style="width: 10%">被替员工:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblReplaceUser"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trReplace2">
            <td class="oddrow" style="width: 10%">替换原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblReplaceReason"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trReplace3">
            <td class="oddrow" style="width: 10%">离岗日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblDimissionDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>工作职责:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblResponse"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>资格需求:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblRequestment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>备注:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>审批日志:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td>离职员工入职：</td>
            <td colspan="5">
                <asp:HiddenField ID="hidDimissionUserId" runat="server" />
                <asp:HiddenField ID="hidDimissionCode" runat="server" />
                <asp:TextBox ID="txtDimissionUser" runat="server" onkeyDown="return false; " />
                <input type="button" id="Button2" class="widebuttons" value="选择..." onclick="DimissionClick();" />
                <asp:Button runat="server" ID="btnDimission" Text=" 确认并导入该员工数据 " CausesValidation="false" OnClick="btnDimission_Click" />
                <span style="color: red;">从离职库导入数据可以沿用原员工编号</span>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">中文姓:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_LastNameCn" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBase_LastNameCn"
                    Display="Dynamic" ErrorMessage="请填写中文姓">请填写中文姓</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">中文名:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_FirstNameCn" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBase_FirstNameCn"
                    Display="Dynamic" ErrorMessage="请填写中文名">请填写中文名</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">身份证号:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtIDCard" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">个人邮箱:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtPrivateEmail" runat="server" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPrivateEmail"
                    Display="Dynamic" ErrorMessage="请填写员工个人Email">请填写员工个人Email</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">手机:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtMobilePhone" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">应届毕业生:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:CheckBox ID="chkExamen" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">性别:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlGender">
                    <asp:ListItem Text="未知" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="男" Value="1"></asp:ListItem>
                    <asp:ListItem Text="女" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">出生日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBirthday" runat="server" onkeyDown="return false; " />
            </td>
            <td class="oddrow" style="width: 10%">婚否:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlMarry">
                    <asp:ListItem Text="未知" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="已婚" Value="1"></asp:ListItem>
                    <asp:ListItem Text="未婚" Value="2"></asp:ListItem>
                    <asp:ListItem Text="单身" Value="3"></asp:ListItem>
                    <asp:ListItem Text="非单身" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">户口所在地 :
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox runat="server" ID="txtLocation"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 10%">目前住址:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 60%">
                <asp:TextBox runat="server" ID="txtAddress" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">教育背景:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:TextBox runat="server" ID="txtEducation" TextMode="MultiLine" Height="100px"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">工作地点:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server">
                    <asp:ListItem Text="北京" Value="北京" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="重庆" Value="重庆"></asp:ListItem>
                    <asp:ListItem Text="杭州" Value="杭州"></asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddltype"
                    Display="Dynamic" ErrorMessage="请选择工作地点" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>
            <td class="oddrow" style="width: 10%">员工类型:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList ID="drpUserType" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">职位:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                <asp:Label runat="server" ID="lblJoinPosition"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">入职日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtJob_JoinDate" runat="server" onkeyDown="return false;" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtJob_JoinDate"
                    Display="Dynamic" ErrorMessage="请选择入职日期">请选择入职日期</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">基本工资(税前):
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtNowBasePay" runat="server" /><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNowBasePay"
                    Display="Dynamic" ErrorMessage="请正确填写基本工资"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" Type="Currency" MinimumValue="0" MaximumValue="9000000" ControlToValidate="txtNowBasePay" runat="server" ErrorMessage="请正确填写基本工资"></asp:RangeValidator>
            </td>
            <td class="oddrow" style="width: 10%">岗位薪资(税前):
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtNowMeritPay" runat="server" /><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNowMeritPay"
                    Display="Dynamic" ErrorMessage="请正确填写岗位薪资"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator2" Type="Currency" MinimumValue="0" MaximumValue="9000000" ControlToValidate="txtNowMeritPay" runat="server" ErrorMessage="请正确填写岗位薪资"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">考勤绩效(税前):
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtNowAttendance" runat="server" /><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNowAttendance"
                    Display="Dynamic" ErrorMessage="请正确填写考勤绩效"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator3" Type="Currency" MinimumValue="0" MaximumValue="9000000" ControlToValidate="txtNowAttendance" runat="server" ErrorMessage="请正确填写考勤绩效"></asp:RangeValidator>
            </td>
            <td class="oddrow">社会工龄:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtSeniority" runat="server" />
            </td>
            <td class="oddrow-l">考勤审批人
            </td>
            <td class="oddrow-l">
                <asp:HiddenField ID="hidKaoqin" runat="server" />
                <asp:TextBox ID="txtKaoqin" runat="server" onkeyDown="return false; " />
                <font color="red">*</font>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtKaoqin"
                    Display="Dynamic" ErrorMessage="请选择" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                <input type="button" id="Button1" class="widebuttons" value="选择..." onclick="KaoqinClick();" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">模板类型:
            </td>
            <td class="oddrow-l" colspan ="5">
                <asp:DropDownList ID="drpOfferTemplate" runat="server">
                    <asp:ListItem Text="普通模板" Value="1" Selected="True" />
                    <asp:ListItem Text="全薪模板" Value="2" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow">备注:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="80%" runat="server" />
            </td>
        </tr>
        <tr>
            <td>业务团队意见:
            </td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="txtGroup" TextMode="MultiLine" Width="80%" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">HR意见:
            </td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="txtHR" TextMode="MultiLine" Width="80%" Height="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHR"
                    Display="Dynamic" ErrorMessage="请输入HR意见">请输入HR意见</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">仪容/礼貌/精神态度:
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlAppearance">
                    <asp:ListItem Text="极佳" Value="极佳" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="佳" Value="佳"></asp:ListItem>
                    <asp:ListItem Text="平实" Value="平实"></asp:ListItem>
                    <asp:ListItem Text="略差" Value="略差"></asp:ListItem>
                    <asp:ListItem Text="极差" Value="极差"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">领悟、反应及综合素质:
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlQuality">
                    <asp:ListItem Text="特强" Value="特强" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="优秀" Value="优秀"></asp:ListItem>
                    <asp:ListItem Text="平平" Value="平平"></asp:ListItem>
                    <asp:ListItem Text="稍慢" Value="稍慢"></asp:ListItem>
                    <asp:ListItem Text="极劣" Value="极劣"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">对所应聘职位的了解:
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlKnow">
                    <asp:ListItem Text="充分了解" Value="充分了解" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="很了解" Value="很了解"></asp:ListItem>
                    <asp:ListItem Text="尚了解" Value="尚了解"></asp:ListItem>
                    <asp:ListItem Text="部分了解" Value="部分了解"></asp:ListItem>
                    <asp:ListItem Text="极少了解" Value="极少了解"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">所具工作经历与本公司的配合程度 :
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" ID="ddlEqual">
                    <asp:ListItem Text="极配合" Value="极配合" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="配合" Value="配合"></asp:ListItem>
                    <asp:ListItem Text="尚配合" Value="尚配合"></asp:ListItem>
                    <asp:ListItem Text="未尽配合" Value="未尽配合"></asp:ListItem>
                    <asp:ListItem Text="未能配合" Value="未能配合"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">对本公司的了解和前来工作的动机 :
            </td>
            <td colspan="5">
                <asp:DropDownList runat="server" ID="ddlReason">
                    <asp:ListItem Text="很满意" Value="很满意" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="满意" Value="满意"></asp:ListItem>
                    <asp:ListItem Text="普通" Value="普通"></asp:ListItem>
                    <asp:ListItem Text="很少" Value="很少"></asp:ListItem>
                    <asp:ListItem Text="极少" Value="极少"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">4D性格测试:
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txt4D"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 10%">EQ测试:
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtEQ"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button runat="server" ID="btnCommit" Text=" 确认 " OnClientClick=" if(Page_ClientValidate()){showLoading();}else{return false;} " CausesValidation="false" OnClick="btnAudit_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    runat="server" />
                <input id="SelectedModuleArr" type="hidden" value="" runat="server" name="SelectedModuleArr" />
                <input id="SelectedModuleName" type="hidden" value="" runat="server" name="SelectedModuleName" />
                <input id="UpdateModuleArr" type="hidden" value="" runat="server" name="UpdateModuleArr" />
                <input id="UpdateModuleName" type="hidden" value="" runat="server" name="UpdateModuleName" />
                <input id="SelectedBossArr" type="hidden" value="" runat="server" name="SelectedBossArr" />
                <input id="SelectedBossName" type="hidden" value="" runat="server" name="SelectedBossName" />
                <input id="UpdateBossArr" type="hidden" value="" runat="server" name="UpdateBossArr" />
                <input id="UpdateBossName" type="hidden" value="" runat="server" name="UpdateBossName" />
                <input id="hidnowBasePay" type="hidden" runat="server" />
                <input id="hidnowMeritPay" type="hidden" runat="server" />
                <input id="SelectedPositionArr" type="hidden" value="" runat="server" name="SelectedPositionArr" />
                <input id="SelectedPositionName" type="hidden" value="" runat="server" name="SelectedPositionName" />
            </td>
        </tr>
    </table>
</asp:Content>
