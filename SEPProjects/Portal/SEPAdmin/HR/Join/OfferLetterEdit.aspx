<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterEdit.aspx.cs"
    Inherits="OfferLetterEdit" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        $(function() {
           // $('#container-1').tabs();
        });

        function selectPosition(positionId, positionName) {
           document.getElementById("<%= txtJob_JoinJob.ClientID%>").value = positionId;
           document.getElementById("<%= txtPosition.ClientID%>").value = positionName;
        }
        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '部门列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function PositionClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function AuditerClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/EmployeeDlg.aspx?deptid=' + deptid, { title: '审核人列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function onPageSubmit() {
            onSubmit();
            var nameValue = document.getElementById("<%= SelectedModuleName.ClientID%>").value;
            var idValue = document.getElementById("<%= SelectedModuleArr.ClientID%>").value;

//            var positionName = document.getElementById("<%= SelectedPositionName.ClientID%>").value;
//            var positionId = document.getElementById("<%= SelectedPositionArr.ClientID%>").value;
//            
            var nameValues = nameValue.split('-');
            //alert(nameValues.length);

            var idValues = idValue.split('-');
            //alert(idValues.length);


            
            if (idValues.length == 1 && nameValues.length == 1 && nameValues[0] != "" && idValues[0] != "") {
                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = "";
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";
                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = "";
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = "";
                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[0];
            }
            if (idValues.length == 2 && nameValues.length == 2) {
                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = "";
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";
                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[0];
                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[1];
            }
            if (idValues.length == 3 && nameValues.length == 3) {
                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidGroupId.ClientID%>").value = idValues[0];
                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[1];
                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[2];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[2];
            }

//            document.getElementById("<%= txtJob_JoinJob.ClientID%>").value = positionId;
//            document.getElementById("<%= txtPosition.ClientID%>").value = positionName;
            
        }

        function checkIdcard(idcard) {
            var Errors = new Array(
            "验证通过!",
            "身份证号码位数不对!",
            "身份证号码出生日期超出范围或含有非法字符!",
            "身份证号码校验错误!",
            "身份证地区非法!");
            var area = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }
            var Y, JYM;
            var S, M;
            var idcard_array = new Array();
            idcard_array = idcard.split("");
            //地区检验
            if (area[parseInt(idcard.substr(0, 2))] == null) return Errors[4];
            //身份号码位数及格式检验
            switch (idcard.length) {
                case 15:
                    if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                        ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/; //测试出生日期的合法性
                    } else {
                        ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/; //测试出生日期的合法性
                    }
                    if (ereg.test(idcard)) return Errors[0];
                    else return Errors[2];
                    break;
                case 18:
                    //18位身份号码检测
                    //出生日期的合法性检查 
                    //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
                    //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
                    if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                        ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/; //闰年出生日期的合法性正则表达式
                    } else {
                        ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/; //平年出生日期的合法性正则表达式
                    }
                    if (ereg.test(idcard)) {//测试出生日期的合法性
                        //计算校验位
                        S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
                            + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
                            + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
                            + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
                            + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
                            + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
                            + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
                            + parseInt(idcard_array[7]) * 1
                            + parseInt(idcard_array[8]) * 6
                            + parseInt(idcard_array[9]) * 3;
                        Y = S % 11;
                        M = "F";
                        JYM = "10X98765432";
                        M = JYM.substr(Y, 1); //判断校验位
                        if (M == idcard_array[17]) return true; //检测ID的校验位
                        else return Errors[3];
                    }
                    else return Errors[2];
                    break;
                default:
                    return Errors[1];
                    break;
            }
        }

        function checkCard(sender, args) {
            var msg = checkIdcard(document.getElementById("<%= txtIDCard.ClientID%>").value);
            if (msg == true) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
    </script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <!-- Additional IE/Win specific style sheet (Conditional Comments) -->
    <!--[if lte IE 7]>
        <link rel="stylesheet" href="/public/css/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->
    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul>
                        <li><a href="#fragment-1"><span>Offer Letter信息</span></a></li>
                    </ul>
                    <div id="fragment-1" style="padding: 0px 0px 0px 0px;">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    中文姓:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_LastNameCn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBase_LastNameCn"
                                        Display="Dynamic" ErrorMessage="请填写中文姓">请填写中文姓</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    中文名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_FirstNameCn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBase_FirstNameCn"
                                        Display="Dynamic" ErrorMessage="请填写中文名">请填写中文名</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    身份证号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtIDCard" runat="server" /><font color="red">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    个人邮箱:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtPrivateEmail" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPrivateEmail"
                                        Display="Dynamic" ErrorMessage="请填写员工个人Email">请填写员工个人Email</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPrivateEmail"
                                        Display="Dynamic" ErrorMessage="请输入正确Email地址" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">请输入正确Email地址</asp:RegularExpressionValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    手机:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtMobilePhone" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    应届毕业生:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:CheckBox ID="chkExamen" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    所属公司:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_CompanyName" runat="server" />
                                    <font color="red">*</font>
                                    <asp:HiddenField ID="hidCompanyId" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtJob_CompanyName"
                                        Display="Dynamic" ErrorMessage="请选择所属公司">请选择所属公司</asp:RequiredFieldValidator>
                                    <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    部门:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_DepartmentName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidDepartmentID" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    组别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_GroupName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidGroupId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    工作地点:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddltype"
                                        Display="Dynamic" ErrorMessage="请选择工作地点" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    员工类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpUserType" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    职位:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                                    <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPosition"
                                        Display="Dynamic" ErrorMessage="请选择职位" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    <input type="button" id="btnPosition" class="widebuttons" value="选择..." onclick="PositionClick();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    入职日期:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_JoinDate" runat="server" onkeyDown="return false; " onclick="setDate(this);" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtJob_JoinDate"
                                        Display="Dynamic" ErrorMessage="请选择入职日期">请选择入职日期</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    基本工资:<br />
                                    (税前)
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtNowBasePay" runat="server" /> <font color="red">*</font>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNowBasePay"
                                        Display="Dynamic" ErrorMessage="请填写员工基本工资">请填写员工基本工资</asp:RequiredFieldValidator>
                                   
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    绩效工资:<br />
                                    (税前)
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtNowMeritPay" runat="server" /> <font color="red">*</font>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNowMeritPay"
                                        Display="Dynamic" ErrorMessage="请填写员工绩效工资">请填写员工绩效工资</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    模板类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpOfferTemplate" runat="server">
                                        <asp:ListItem Text="普通模板" Value="1" />
                                        <asp:ListItem Text="总监级以上模板" Value="2" />
                                        <asp:ListItem Text="实习生模板" Value="3" />
                                         <asp:ListItem Text="销售模板" Value="4" />
                                        <asp:ListItem Text="12薪模板" Value="5" />
                                    </asp:DropDownList>
                                </td>
                                <%--<td class="oddrow" style="width: 10%">
                                    自带笔记本:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:CheckBox ID="chkByoComputer" runat="server" />
                                </td>--%>
                                <td class="oddrow">
                                    社会工龄:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtSeniority" runat="server" /><%--<font color="red">*</font>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填" ControlToValidate="txtSeniority" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填写数字" ControlToValidate="txtSeniority" ValidationExpression="^[1-9]\d*$" Display="Dynamic" />--%>
                                </td>
                                <td class="oddrow-l">
                                   考勤审批人
                                </td>
                                <td class="oddrow-l">
                                      <asp:HiddenField ID="hidAuditer" runat="server" />
                                    <asp:TextBox ID="txtAuditer" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtAuditer"
                                        Display="Dynamic" ErrorMessage="请选择" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    <input type="button" id="btnAuditer" class="widebuttons" value="选择..." onclick="AuditerClick();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6">
                                    备注:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                                        Text=" 保 存 " />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSendOffer" runat="server" CssClass="widebuttons" OnClick="btnSendOffer_Click"
                                        Text=" 提 交 " />&nbsp;&nbsp;&nbsp;
                                    <input id="btnReturn" type="button" class="widebuttons" onclick="javascript:window.location.href='OfferLetterList.aspx'"
                                        value=" 返 回 " />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
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
