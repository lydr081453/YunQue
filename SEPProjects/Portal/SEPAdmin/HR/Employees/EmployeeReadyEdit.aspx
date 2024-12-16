<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="Employees_EmployeeReadyEdit" Title="" EnableEventValidation="false"
    CodeBehind="EmployeeReadyEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
    <script type="text/javascript" src="/public/js/jquery1.12.js"></script>
    <script type="text/javascript" src="/public/js/jquery-ui-new.js"></script>
    <script type="text/javascript" src="/public/js/jquery.ui.datepicker.cn.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript">

        $(function () {
            $('#container-1').tabs();
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#ctl00_ContentPlaceHolder1_txtJob_JoinDate").datepicker();
            show();
        });

        function initItCode(objEmail) {
            var itcode = objEmail.value.split('@');
            if (itcode.length == 2) {
                document.getElementById("<%=txtItCode.ClientID%>").value = itcode[0];
            }
        }

        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '部门列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function PositionClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function onPageSubmit() {
            onSubmit();
            var nameValue = document.getElementById("<%= SelectedModuleName.ClientID%>").value;
            var idValue = document.getElementById("<%= SelectedModuleArr.ClientID%>").value;

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

                drpPositionsBind(idValues[0]);
            }

            if (idValues.length == 2 && nameValues.length == 2) {

                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = "";
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";

                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[0];

                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[1];

                drpPositionsBind(idValues[0]);
            }

            if (idValues.length == 3 && nameValues.length == 3) {

                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidGroupId.ClientID%>").value = idValues[0];

                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[1];

                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[2];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[2];

                drpPositionsBind(idValues[0]);
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
                        <li><a href="#fragment-1"><span>待入职人员信息</span></a></li>
                    </ul>
                    <div id="fragment-1">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">员工编号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtUserId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">入职日期:
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    <asp:TextBox ID="txtJob_JoinDate" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtJob_JoinDate"
                                        Display="Dynamic" ErrorMessage="请选择入职日期">请选择入职日期</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">公司邮箱:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtEmail" runat="server" onblur="initItCode(this)" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="请填写公司Email">请填写公司Email</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="请输入正确Email地址" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">请输入正确Email地址</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">登录名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtItCode" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">分机号：
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtTelPhone"></asp:TextBox>
                                </td>
                                <td class="oddrow-l" style="width: 10%">&nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">中文姓:
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
                                <td class="oddrow" style="width: 10%">公司常用名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtCommonName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">FirstName:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_FirstNameEn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBase_FirstNameEn"
                                        Display="Dynamic" ErrorMessage="请填写FirstName">请填写FirstName</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">LastName:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_LastNameEn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBase_LastNameEn"
                                        Display="Dynamic" ErrorMessage="请填写LastName">请填写LastName</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%; height: 30px;">性别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="txtBase_Sex" runat="server">
                                        <asp:ListItem Value="0" Text="未知" />
                                        <asp:ListItem Value="1" Text="男" />
                                        <asp:ListItem Value="2" Text="女" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">身份证号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtIDCard" runat="server" /><font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIDCard"
                                        Display="None" ErrorMessage="请填写身份证号"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                        Display="None" ErrorMessage="请正确填写身份证号" />
                                </td>
                                <td class="oddrow" style="width: 10%">联系电话:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtTel" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTel"
                                        Display="Dynamic" ErrorMessage="请填写联系电话">请填写联系电话</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">员工类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpUserType" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">所属公司:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_CompanyName" runat="server" />
                                    <font color="red">*</font><%--onkeyDown="return false; "--%>
                                    <asp:HiddenField ID="hidCompanyId" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtJob_CompanyName"
                                        Display="Dynamic" ErrorMessage="请选择所属公司">请选择所属公司</asp:RequiredFieldValidator>
                                    <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                                </td>
                                <td class="oddrow" style="width: 10%">部门:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_DepartmentName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidDepartmentID" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">组别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_GroupName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidGroupId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">职位:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                                    <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPosition"
                                        Display="Dynamic" ErrorMessage="请选择职位" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    <input type="button" id="btnPosition" class="widebuttons" value="选择..." onclick="PositionClick();" />
                                </td>
                                <td class="oddrow">工作地点:
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="ddltype" Width="100px" runat="server">
                                        <asp:ListItem Text="请选择" Value="-1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="北京" Value="北京"></asp:ListItem>
                                        <asp:ListItem Text="重庆" Value="重庆"></asp:ListItem>
                                        <asp:ListItem Text="杭州" Value="杭州"></asp:ListItem>
                                         <asp:ListItem Text="上海" Value="上海"></asp:ListItem>
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddltype"
                                        Display="Dynamic" ErrorMessage="请选择工作地点" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">工资领取月数:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpWageMonths" runat="server">
                                        <asp:ListItem Value="12" Text="12个月" selected="True"/>
                                        <asp:ListItem Value="13" Text="13个月" />

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">应届毕业生:
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkExamen" runat="server" />
                                </td>
                                <td class="oddrow">社会工龄:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtSeniority" runat="server" /><%--<font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="必填" ControlToValidate="txtSeniority" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填写数字" ControlToValidate="txtSeniority" ValidationExpression="^[1-9]\d*$" Display="Dynamic" />--%>
                                </td>
                                <td class="oddrow">自己带笔记本:
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkByoComputer" runat="server" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="oddrow-l" style="width: 20%">
                                    是否外籍员工<input type="checkbox" id="chkForeign" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    是否有就业证<input type="checkbox" id="chkCertification" runat="server" />
                                </td>
                                <td class="oddrow-l" colspan="4">
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="oddrow">简历文档
                                </td>
                                <td class="oddrow-l" colspan="5">
                                    <asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm">
                            <%--<tr>
                        <td class="oddrow" colspan="6">
                            自我介绍:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_SelfIntroduction" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            求职意向:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_Objective" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            工作经验:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_WorkingExperience" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            教育背景:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_EducationalBackground" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            语言及方言:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_LanguagesAndDialect" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr>         --%>
                            <tr>
                                <td class="oddrow" colspan="6">备注:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
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
            </td>
        </tr>
    </table>
    <table width="90%">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" 返 回 " CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
