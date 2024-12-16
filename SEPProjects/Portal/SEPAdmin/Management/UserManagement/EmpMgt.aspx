<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" EnableEventValidation="false"
    CodeBehind="EmpMgt.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.EmpMgt" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

     <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
    <script type="text/javascript" src="/public/js/jquery-3.7.1.js"></script>
    <script src="/public/js/jquery-ui-new.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="/HR/Employees/js/UserDepartment.js"></script>



    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />

    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page */ /* Default tab */

        .AjaxTabStrip .ajax__tab_tab {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */ .AjaxTabStrip .ajax__tab_hover .ajax__tab_tab {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */ .AjaxTabStrip .ajax__tab_active .ajax__tab_tab {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */ .AjaxTabStrip .ajax__tab_body {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }

        .border {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-bottom-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border2 {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border_title_left {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_title_right {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_datalist {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CC3333;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#" + "<%=txtIDValid.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtBase_Birthday.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtBase_CreateDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            }); 
            $("#" + "<%=txtJob_JoinDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtWorkBegin.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtPositionDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtJob_JoinDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtProbation.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtContractBegin.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtSignDate.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#" + "<%=txtContractEnd.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            
           });

        $().ready(function () {

            $("#<%=drpContract_Company.ClientID %>").empty();
            $("#<%=ddlSocialBranch.ClientID %>").empty();

            SEPAdmin.Management.UserManagement.EmpMgt.getBranch(initContractBranch);
            function initContractBranch(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidContactBranch.ClientID %>").val()) {
                            $("#<%=drpContract_Company.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=drpContract_Company.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
            }

            SEPAdmin.Management.UserManagement.EmpMgt.getBranch(initSocialBranch);
            function initSocialBranch(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidSocialBranch.ClientID %>").val()) {
                            $("#<%=ddlSocialBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlSocialBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
            }
        });

        function changeContractCompany(val, text) {
            if (val == "-1") {
                document.getElementById("<% =hidContactBranch.ClientID %>").value = "";
                document.getElementById("<%= txtBranch.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidContactBranch.ClientID %>").value = val;
                document.getElementById("<%= txtBranch.ClientID %>").value = val;

            }
        }


        function changeSocialCompany(val, text) {
            if (val == "-1") {
                document.getElementById("<% =hidSocialBranch.ClientID %>").value = "";
                document.getElementById("<%= txtSocialBranch.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidSocialBranch.ClientID %>").value = val;
                document.getElementById("<%= txtSocialBranch.ClientID %>").value = val;
            }

        }

        function addmember() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/EmpMemberEdit.aspx?userid=' + userid, { title: '家庭成员', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function editmember(memberid) {
            art.dialog.open('/Management/UserManagement/EmpMemberEdit.aspx?memberid=' + memberid, { title: '家庭成员', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function addeducation() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/EmpEducationEdit.aspx?userid=' + userid, { title: '教育情况', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function editeducation(eduid) {
            art.dialog.open('/Management/UserManagement/EmpEducationEdit.aspx?eduid=' + eduid, { title: '教育情况', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function addwork() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/EmpWorkEdit.aspx?userid=' + userid, { title: '工作情况', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function editwork(workid) {
            art.dialog.open('/Management/UserManagement/EmpWorkEdit.aspx?workid=' + workid, { title: '工作情况', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function addPositionLog() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/PositionLogAdd.aspx?userid=' + userid, { title: '职务历史', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function addestimate() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/EmpEstimateEdit.aspx?userid=' + userid, { title: '评估信息', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function editestimate(estimateid) {
            art.dialog.open('/Management/UserManagement/EmpEstimateEdit.aspx?estimateid=' + estimateid, { title: '评估信息', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
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

        function DeletePositionClick(userid, deptid, positionid) {
            art.dialog.open('/Management/UserManagement/EmpPositionDelete.aspx?userId=' + userid + '&deptId=' + deptid + '&positionId=' + positionid, { title: '职务信息', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

        function addContract() {
            var userid = document.getElementById("<%=hidUserId.ClientID %>").value;
            art.dialog.open('/Management/UserManagement/EmpContractEdit.aspx?userid=' + userid, { title: '合同信息', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function editContract(contractid) {
            art.dialog.open('/Management/UserManagement/EmpContractEdit.aspx?contractid=' + contractid, { title: '合同信息', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

    </script>

    <table width="956" class="tableForm">
        <tr>
            <td class="oddrow-l" align="center">
                <div class="photocontainer">
                    <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="/Images/Default_full.jpg" Width="100px" Height="130px"
                        CssClass="imagePhoto" ToolTip="头像" /><br />

                </div>

            </td>
            <td class="oddrow-l" align="left">
                <table width="100%" cellpadding="0" border="0">
                    <tr>
                        <td>姓名:
                            <input type="hidden" runat="server" id="hidUserId" />
                            <asp:Label ID="lblUserName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>编号:<asp:Label ID="lblUserCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>手机:<asp:Label ID="lblMobile" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>部门:<asp:Label ID="lblDept" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" 
         EnableViewState="false" ActiveTabIndex="0">
        <uc1:TabPanel ID="TabPanel1" HeaderText="基础信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 10%">上传照片
                        </td>
                        <td class="oddrow-l" colspan="5">
                            <input name="profile_image" type="file" class="input" />&nbsp;&nbsp;
                            <asp:Button ID="btnSavePhoto" CssClass="enterbtn" Text="保 存" OnClick="btnSavePhoto_Click" runat="server" />&nbsp;<span style="color: gray;">支持.jpg .gif .png的图片，最大可上传2M大小的图片</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">填表日期
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:TextBox ID="txtBase_CreateDate" runat="server"  onkeyDown="return false; "  />
                        </td>
                        <td class="oddrow" style="width: 10%">员工编号:
                        </td>
                        <td class="oddrow-l" colspan="3" style="width: 15%">
                            <asp:Label ID="txtUserCode" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="oddrow" style="width: 10%">中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 15%">姓&nbsp;<asp:TextBox ID="txtBase_LastNameCn" runat="server" Width="50px" />&nbsp;名&nbsp;<asp:TextBox
                            ID="txtBase_FitstNameCn" runat="server" Width="50px" /><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_LastNameCn" Display="Dynamic" ErrorMessage="请填写中文姓名">请填写中文姓名</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">英文姓名:
                        </td>
                        <td class="oddrow-l" colspan="3">FirstName&nbsp;<asp:TextBox ID="txtBase_FirstNameEn" runat="server" Width="50px" />&nbsp;LastName&nbsp;<asp:TextBox
                            ID="txtBase_LastNameEn" runat="server" Width="50px" /><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_FirstNameEn" Display="Dynamic" ErrorMessage="请填写英文姓名">请填写英文姓名</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">性别:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:DropDownList ID="txtBase_Sex" runat="server">
                                <asp:ListItem Value="0" Text="请选择.." />
                                <asp:ListItem Value="1" Text="男" />
                                <asp:ListItem Value="2" Text="女" />
                            </asp:DropDownList>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_Sex" Display="Dynamic" InitialValue="0" ErrorMessage="请填写性别">请填写性别</asp:RequiredFieldValidator>

                        </td>
                        <td class="oddrow" style="width: 10%">出生日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Birthday" runat="server"  onkeyDown="return false; "   />
                        </td>
                        <td class="oddrow" style="width: 10%">民族:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtNation" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtNation" Display="Dynamic" ErrorMessage="请填写民族">请填写民族</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">政治面貌:
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList ID="ddlPolicity" runat="server">
                                <asp:ListItem Selected="True" Value="-1" Text="请选择" />
                                <asp:ListItem Value="中共党员" Text="中共党员" />
                                <asp:ListItem Value="中共预备党员" Text="中共预备党员" />
                                <asp:ListItem Value="中共团员" Text="中共团员" />
                                <asp:ListItem Value="其他党派" Text="其他党派" />
                                <asp:ListItem Value="群众" Text="群众" />
                            </asp:DropDownList><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="ddlPolicity" Display="Dynamic" InitialValue="-1" ErrorMessage="请填写政治面貌">请填写政治面貌</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">籍贯:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_PlaceOfBirth" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_PlaceOfBirth" Display="Dynamic" ErrorMessage="请填写籍贯">请填写籍贯</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">户口所在地:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_DomicilePlace" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_DomicilePlace" Display="Dynamic" ErrorMessage="请填写户口所在地">请填写户口所在地</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">员工性质:
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList runat="server" ID="ddlForeign">
                                <asp:ListItem Selected="True" Text="请选择.." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="正式员工" Value="1"></asp:ListItem>
                                <asp:ListItem Text="派遣员工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="正式员工（外籍人）" Value="3"></asp:ListItem>
                                <asp:ListItem Text="正式员工（港澳台）" Value="4"></asp:ListItem>
                                <asp:ListItem Text="兼职（外籍人）" Value="5"></asp:ListItem>
                                <asp:ListItem Text="兼职（港澳台）" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="ddlForeign" Display="Dynamic" InitialValue="-1" ErrorMessage="请填写员工性质">请填写员工性质</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">婚姻状况:
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList ID="txtBase_Marriage" runat="server">
                                <asp:ListItem Value="0" Text="请选择.." />
                                <asp:ListItem Value="1" Text="已婚" />
                                <asp:ListItem Value="2" Text="未婚" />
                                <asp:ListItem Value="3" Text="已婚有子" />
                                <asp:ListItem Value="4" Text="离异" />
                            </asp:DropDownList>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_Marriage" Display="Dynamic" InitialValue="0" ErrorMessage="请填写婚姻状况">请填写婚姻状况</asp:RequiredFieldValidator>

                        </td>
                        <td class="oddrow" style="width: 10%">最高学历:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtEducation" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtEducation" Display="Dynamic" ErrorMessage="请填写最高学历">请填写最高学历</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">身份证号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_IdNo" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_IdNo" Display="Dynamic" ErrorMessage="请填写身份证号码">请填写身份证号码</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">身份证地址:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtIdAddress" runat="server" Width="65%" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtIdAddress" Display="Dynamic" ErrorMessage="请填写身份证地址">请填写身份证地址</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">身份证有效期:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtIDValid" runat="server" onkeyDown="return false; " />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtIDValid" Display="Dynamic" ErrorMessage="请填写身份证有效期">请填写身份证有效期</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">家庭地址:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtBase_Address1" runat="server" Width="65%" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator113" ValidationGroup="basicSave"
                                runat="server" ControlToValidate="txtBase_Address1" Display="Dynamic" ErrorMessage="请填写家庭地址">请填写家庭地址</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">户口属性:
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList runat="server" ID="ddlHuji">
                                <asp:ListItem Selected="True" Text="请选择.." Value="0"></asp:ListItem>
                                <asp:ListItem Text="本市城镇" Value="本市城镇"></asp:ListItem>
                                <asp:ListItem Text="外埠城镇" Value="外埠城镇"></asp:ListItem>
                                <asp:ListItem Text="本市农业" Value="本市农业"></asp:ListItem>
                                <asp:ListItem Text="外埠农业" Value="外埠农业"></asp:ListItem>
                            </asp:DropDownList>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="basicSave"
                                runat="server" ControlToValidate="ddlHuji" InitialValue="0" Display="Dynamic" ErrorMessage="请填写户口属性">请填写户口属性</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">宅电:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_HomePhone" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">邮政编码:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_PostCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">个人邮箱:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtPrivateEmail" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtPrivateEmail" Display="Dynamic" ErrorMessage="请填写个人邮箱">请填写个人邮箱</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">紧急联系人:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_EmergencyLinkman" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_EmergencyLinkman" Display="Dynamic" ErrorMessage="请填写紧急联系人">请填写紧急联系人</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">联系人电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_EmergencyPhone" runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_EmergencyPhone" Display="Dynamic" ErrorMessage="请填写紧急联系人电话">请填写紧急联系人电话</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">公司邮箱:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Email" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtBase_Email"
                                Display="Dynamic" ValidationGroup="basicSave" ErrorMessage="请输入正确Email地址" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">请输入正确Email地址</asp:RegularExpressionValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">工作地点:
                        </td>
                        <td class="oddrow-l">
                              <asp:DropDownList ID="ddlWorkCity" Width="100px" runat="server">
                                        <asp:ListItem Text="请选择" Value="-1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="北京" Value="北京"></asp:ListItem>
                                        <asp:ListItem Text="重庆" Value="重庆"></asp:ListItem>
                                        <asp:ListItem Text="杭州" Value="杭州"></asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 10%">手机:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_MobilePhone" runat="server" /><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_MobilePhone" Display="Dynamic" ErrorMessage="请填写手机">请填写手机</asp:RequiredFieldValidator>
                        </td>

                    </tr>

                    <tr>
                        <td class="oddrow" style="width: 10%">参加工作日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtWorkBegin" runat="server"  onkeyDown="return false; "  /><font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtWorkBegin" Display="Dynamic" ErrorMessage="请填写参加工作日期">请填写参加工作日期</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">入职日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtJob_JoinDate" runat="server"  onkeyDown="return false; "   /><font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtJob_JoinDate" Display="Dynamic" ErrorMessage="请填写入职日期">请填写入职日期</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">年假基数:
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList runat="server" ID="ddlAnnualType">
                                <asp:ListItem Text="请选择.." Value="-1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                        <td class="oddrow" style="width: 10%">工资卡银行:</td>
                        <td class="oddrow-l" colspan="5">
                            <asp:TextBox runat="server" ID="txtSalaryBank"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtSalaryBank" Display="Dynamic" ErrorMessage="请填写工资卡银行">请填写工资卡银行</asp:RequiredFieldValidator>
                             <span style="color: gray;">请填写完整至分行信息（例：招商银行北京朝外大街支行）</span>
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">工资卡账号:</td>
                        <td class="oddrow-l" colspan="5">
                            <asp:TextBox runat="server" ID="txtSalaryCardNo"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtSalaryCardNo" Display="Dynamic" ErrorMessage="请填写工资卡账号">请填写工资卡账号</asp:RequiredFieldValidator>
                             <span style="color: gray;">请填写银行一类卡</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="oddrow" style="width: 10%">简历文档上传
                        </td>
                        <td class="oddrow-l" colspan="5">
                            <asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume"
                                runat="server" />
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">工作履历:(简述入职前的工作经历概要.格式:时间段、公司名称、职位、客户类型)
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtBase_WorkExperience" TextMode="MultiLine" Height="100px" Width="95%"
                                runat="server" />
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="basicSave"
                                ControlToValidate="txtBase_WorkExperience" Display="Dynamic" ErrorMessage="请填写工作履历">请填写工作履历</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">工作特长:
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtBase_WorkSpecialty" TextMode="MultiLine" Height="100px" Width="95%"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">教育情况:
                            <input type="button" onclick="addeducation();" style="cursor: pointer;" class="widebuttons"
                                value="添加" />
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False"
                                Width="100%" EnableModelValidation="True">
                                <Columns>
                                    <asp:BoundField HeaderText="毕业院校" DataField="School" />
                                    <asp:BoundField HeaderText="就读时间" DataField="BeginDate" />
                                    <asp:BoundField HeaderText="毕业时间" DataField="EndDate" />
                                    <asp:BoundField HeaderText="学历" DataField="Degree" />
                                    <asp:BoundField HeaderText="专业" DataField="Profession" />
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemTemplate>
                                            <a style="cursor: pointer;" onclick="editeducation(<%# Eval("EducationId") %>);">
                                                <img src="/images/edit.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">没有符合条件的数据存在！
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">备注:
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="95%" runat="server" />
                        </td>
                    </tr>
                      <tr>
                         <td class="oddrow-l" colspan="6">
                            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                                ValidationGroup="basicSave" Text=" 保 存 " />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" CausesValidation="false" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                                Text=" 返 回 " />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="basicSave" ShowSummary="False" runat="server" />
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="评估信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <input type="button" onclick="addestimate();" style="cursor: pointer;" class="widebuttons"
                                value=" 添加评估信息 " />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvPingGu" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                PageSize="20" PagerSettings-Mode="NumericFirstLast" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="评估类型" DataField="EstimateType" />
                                    <asp:BoundField HeaderText="评估日期" DataField="EstimateDate" />
                                    <asp:BoundField HeaderText="评估结果" DataField="Result" />
                                    <asp:BoundField HeaderText="评估说明" DataField="Remark" />
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemTemplate>
                                            <a style="cursor: pointer;" onclick="editestimate(<%# Eval("EstimateId") %>);">
                                                <img src="/images/edit.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">没有符合条件的数据存在！
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="部门职位变更" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 15%">所属公司:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtJob_CompanyName" runat="server" Enabled="false" />
                            <asp:HiddenField ID="hidCompanyId" runat="server" />
                            <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJob_CompanyName"
                                Display="Dynamic" ErrorMessage="请选择公司">请选择公司</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 15%">部门:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtJob_DepartmentName" runat="server" Enabled="false" />
                            <asp:HiddenField ID="hidDepartmentID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">组别:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtJob_GroupName" runat="server" Enabled="false" />
                            <asp:HiddenField ID="hidGroupId" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">职位:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                            <asp:TextBox ID="txtPosition" runat="server" onkeyDown="return false; " Enabled="false" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPosition"
                                Display="Dynamic" ErrorMessage="请选择职位" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                            <input type="button" id="btnPosition" class="widebuttons" value="选择..." onclick="PositionClick();" />
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">生效日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtPositionDate" runat="server" /><font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                ControlToValidate="txtPositionDate" Display="Dynamic" ErrorMessage="请填写生效日期">请填写生效日期</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="2">&nbsp;
                        </td>
                        <td class="oddrow-l" colspan="2" style="width: 50%">
                            <asp:Button ID="btnSavePosition" runat="server" CssClass="widebuttons" OnClick="btnSavePosition_Click"
                                Text=" 添加部门职务 " />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">部门考勤审核人
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:HiddenField ID="hidAuditer" runat="server" />
                            <asp:TextBox ID="txtAuditer" runat="server" />
                            <input type="button" id="btnAuditer" class="widebuttons" value="选择..." onclick="AuditerClick();" />
                        </td>
                        <td class="oddrow-l" colspan="2" style="width: 50%">
                            <asp:Button ID="btnKaoqin" runat="server" CssClass="widebuttons" OnClick="btnKaoqin_Click"
                                Text=" 变更考勤审批 " />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="4">当前所属部门列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvPositionList" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                OnRowDataBound="gvPositionList_RowDataBound" OnRowCommand="gvPositionList_RowCommand"
                                DataKeyNames="UserID,DepartmentPositionID,DepartmentID" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CompanyName" HeaderText="公司" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="GroupName" HeaderText="团队" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="DepartmentPositionName" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="LevelName" HeaderText="职级" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a style="cursor: pointer;" onclick="DeletePositionClick(<%# Eval("UserID") %>,<%# Eval("GroupID") %>,<%# Eval("DepartmentPositionID") %>);">
                                                <img src="/images/disable.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="4">部门职位变更记录
                        </td>
                    </tr>
                    <tr>
                        <tr>
                            <td class="oddrow-l" colspan="4">
                                <asp:GridView ID="gvPositionLog" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="20" PagerSettings-Mode="NumericFirstLast" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="部门" DataField="DepartmentName" />
                                        <asp:BoundField HeaderText="职位" DataField="PositionName" />
                                        <asp:BoundField HeaderText="生效日期" DataField="BeginDate" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField HeaderText="截止日期" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="center">没有符合条件的数据存在！
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel5" HeaderText="劳动合同信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" colspan="4">基本信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">入职日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">试用期至:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtProbation" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">合同公司:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:DropDownList ID="drpContract_Company" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">公司代码:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtBranch" runat="server" Enabled="false" />
                            <input type="hidden" id="hidContactBranch" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="4">首次签订合同信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">合同开始日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtContractBegin" runat="server"  />
                        </td>
                        <td class="oddrow" style="width: 15%">合同结束日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtContractEnd" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">合同期限:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtContractYear" Text="3" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">签订日期:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtSignDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:Button ID="btnContractSave" runat="server" CssClass="widebuttons" OnClick="btnContractSave_Click"
                                Text=" 保存合同信息 " />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <input type="button" onclick="addContract();" style="cursor: pointer;" class="widebuttons"
                                value="添加续签信息" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="合同公司" DataField="Branch" />
                                    <asp:BoundField HeaderText="合同起始日" DataField="BeginDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField HeaderText="合同结束日" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemTemplate>
                                            <a style="cursor: pointer;" onclick="editContract(<%# Eval("ContractId") %>);">
                                                <img src="/images/edit.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">没有符合条件的数据存在！
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel7" HeaderText="福利信息" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 15%">福利地:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox runat="server" ID="txtWelfare"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">社保所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 35%">

                            <asp:DropDownList ID="ddlSocialBranch" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">公司缩写:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtSocialBranch" runat="server" Enabled="false" />
                            <input type="hidden" id="hidSocialBranch" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">档案:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:DropDownList runat="server" ID="ddlFile">
                                <asp:ListItem Selected="True" Text="公司存档" Value="公司存档"></asp:ListItem>
                                <asp:ListItem Text="未在公司存档" Value="未在公司存档"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">操作日志:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblWelfareLog"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="2">
                            <asp:Button ID="btnSocialSave" runat="server" CssClass="widebuttons" OnClick="btnSocialSave_Click"
                                Text=" 保存社保信息 " />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
