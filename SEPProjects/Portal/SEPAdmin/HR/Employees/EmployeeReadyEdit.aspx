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
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '�����б�', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function PositionClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: 'ְλ�б�', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
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
                        <li><a href="#fragment-1"><span>����ְ��Ա��Ϣ</span></a></li>
                    </ul>
                    <div id="fragment-1">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">Ա�����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtUserId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">��ְ����:
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    <asp:TextBox ID="txtJob_JoinDate" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtJob_JoinDate"
                                        Display="Dynamic" ErrorMessage="��ѡ����ְ����">��ѡ����ְ����</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">��˾����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtEmail" runat="server" onblur="initItCode(this)" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="����д��˾Email">����д��˾Email</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="��������ȷEmail��ַ" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">��������ȷEmail��ַ</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">��¼��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtItCode" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">�ֻ��ţ�
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
                                <td class="oddrow" style="width: 10%; height: 30px;">������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_LastNameCn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBase_LastNameCn"
                                        Display="Dynamic" ErrorMessage="����д������">����д������</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_FirstNameCn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBase_FirstNameCn"
                                        Display="Dynamic" ErrorMessage="����д������">����д������</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">��˾������:
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
                                        Display="Dynamic" ErrorMessage="����дFirstName">����дFirstName</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">LastName:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_LastNameEn" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBase_LastNameEn"
                                        Display="Dynamic" ErrorMessage="����дLastName">����дLastName</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%; height: 30px;">�Ա�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="txtBase_Sex" runat="server">
                                        <asp:ListItem Value="0" Text="δ֪" />
                                        <asp:ListItem Value="1" Text="��" />
                                        <asp:ListItem Value="2" Text="Ů" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">���֤��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtIDCard" runat="server" /><font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIDCard"
                                        Display="None" ErrorMessage="����д���֤��"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                        Display="None" ErrorMessage="����ȷ��д���֤��" />
                                </td>
                                <td class="oddrow" style="width: 10%">��ϵ�绰:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtTel" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTel"
                                        Display="Dynamic" ErrorMessage="����д��ϵ�绰">����д��ϵ�绰</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">Ա������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpUserType" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">������˾:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_CompanyName" runat="server" />
                                    <font color="red">*</font><%--onkeyDown="return false; "--%>
                                    <asp:HiddenField ID="hidCompanyId" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtJob_CompanyName"
                                        Display="Dynamic" ErrorMessage="��ѡ��������˾">��ѡ��������˾</asp:RequiredFieldValidator>
                                    <input type="button" id="btndepartment" class="widebuttons" value="ѡ��..." onclick="btnClick();" />
                                </td>
                                <td class="oddrow" style="width: 10%">����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_DepartmentName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidDepartmentID" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">���:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_GroupName" runat="server" onkeyDown="return false; " />
                                    <asp:HiddenField ID="hidGroupId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">ְλ:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                                    <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " />
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPosition"
                                        Display="Dynamic" ErrorMessage="��ѡ��ְλ" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    <input type="button" id="btnPosition" class="widebuttons" value="ѡ��..." onclick="PositionClick();" />
                                </td>
                                <td class="oddrow">�����ص�:
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="ddltype" Width="100px" runat="server">
                                        <asp:ListItem Text="��ѡ��" Value="-1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                        <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                        <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                         <asp:ListItem Text="�Ϻ�" Value="�Ϻ�"></asp:ListItem>
                                    </asp:DropDownList>
                                    <font color="red">*</font>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddltype"
                                        Display="Dynamic" ErrorMessage="��ѡ�����ص�" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">������ȡ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpWageMonths" runat="server">
                                        <asp:ListItem Value="12" Text="12����" selected="True"/>
                                        <asp:ListItem Value="13" Text="13����" />

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">Ӧ���ҵ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkExamen" runat="server" />
                                </td>
                                <td class="oddrow">��Ṥ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtSeniority" runat="server" /><%--<font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="����" ControlToValidate="txtSeniority" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="����д����" ControlToValidate="txtSeniority" ValidationExpression="^[1-9]\d*$" Display="Dynamic" />--%>
                                </td>
                                <td class="oddrow">�Լ����ʼǱ�:
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkByoComputer" runat="server" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="oddrow-l" style="width: 20%">
                                    �Ƿ��⼮Ա��<input type="checkbox" id="chkForeign" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    �Ƿ��о�ҵ֤<input type="checkbox" id="chkCertification" runat="server" />
                                </td>
                                <td class="oddrow-l" colspan="4">
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="oddrow">�����ĵ�
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
                            ���ҽ���:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_SelfIntroduction" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            ��ְ����:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_Objective" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            ��������:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_WorkingExperience" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            ��������:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_EducationalBackground" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            ���Լ�����:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_LanguagesAndDialect" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                        </td>
                    </tr>         --%>
                            <tr>
                                <td class="oddrow" colspan="6">��ע:
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
                    Text=" �� �� " />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" �� �� " CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
