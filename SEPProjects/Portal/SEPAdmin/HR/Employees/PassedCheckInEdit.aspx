<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Employees_PassedCheckInEdit" enableEventValidation="false" Codebehind="PassedCheckInEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script src="/public/js/jquery.js" type="text/javascript"></script>
<script src="/public/js/dialog.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/UserDepartment.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script language="javascript">
        $(document).ready(function() {
            show();

            $("#<%=drpJob_JoinJob.ClientID %>").change(function() {
                $("#<%=hidJob_JoinJob.ClientID %>").val($("#<%=drpJob_JoinJob.ClientID %>").val());
                //alert(document.getElementById("<%=hidJob_JoinJob.ClientID%>").value);
            });
        });
        function drpPositionsBind(depid) {

            ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetPositionsByDepId(depid, popdrp);
            function popdrp(r) {
                $("#<%=drpJob_JoinJob.ClientID %>").empty();
                $("#<%=drpJob_JoinJob.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=drpJob_JoinJob.ClientID %>").append("<option value=\"" + r.value[i].DepartmentPositionID + "\">" + r.value[i].DepartmentPositionName + "</option>");
                }
            }

        }
        function att() {
            if (Page_ClientValidate()) {                
                var Ka = navigator.userAgent.toLowerCase();
                var rt = Ka.indexOf("opera") != -1;
                var r = Ka.indexOf("msie") != -1 && (document.all && !rt);

                if (r) {
                    window.attachEvent("onbeforeunload", function() { $(".widebuttons").val("请等待").attr("disabled", "true"); });
                } else {
                    window.addEventListener('onbeforeunload', function() { $(".widebuttons").val("请等待").attr("disabled", "true"); }, false);
                }
            }
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function btnClick() {
            dialog("部门列表", "url:post?/Management/UserManagement/DepartmentsTree.aspx?principal=1", "500px", "500px", "text"); showSelect();
        }
        function onPageSubmit() {
            onSubmit();
            var nameValue = document.getElementById("<%= SelectedModuleName.ClientID%>").value;
            var idValue = document.getElementById("<%= SelectedModuleArr.ClientID%>").value;
            var idValues = idValue.split('-');
            document.getElementById("<%= txtnewDepartmente.ClientID%>").value = nameValue;
            if (idValues[0] != "") {
                drpPositionsBind(idValues[0]);
            }
         }
    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                转正登记信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" /><asp:HiddenField ID="hiduserId" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtjoinJobDate"  Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                转正前所属部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowDepartment" runat="server"  Enabled="false" />       
            </td>
            <td class="oddrow">
                转正前职位:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowTitle" runat="server"  Enabled="false" /><asp:HiddenField ID="hidnowjob" runat="server" />               
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                转正后所属部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewDepartmente" runat="server" Enabled="false" /><input type="button" id="btndepartment"
                    class="widebuttons" value="选择..." onclick="btnClick();" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ErrorMessage="请选择转正后所属部门" Display="None" ControlToValidate="txtnewDepartmente" /><font
                            color="red"> * </font> <asp:HiddenField ID="hiddepartmentId" runat="server" />                 
            </td>
            <td class="oddrow">
                转正后职位:
            </td>
            <td class="oddrow-l">
                <asp:HiddenField ID="hidJob_JoinJob" runat="server" />
                <asp:DropDownList ID="drpJob_JoinJob" runat="server" onclientchange=""/>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="drpJob_JoinJob" Display="Dynamic" ErrorMessage="请选择职位" 
                    ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="oddrow">是否负责人</td>
            <td class="oddrow-l"><input type="checkbox" id="chkManager" runat="server" /></td>
            <td class="oddrow">是否代理负责人</td>
            <td class="oddrow-l"><input type="checkbox" id="chkActing" runat="server" /></td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtoperationDate" runat="server" onkeyDown="return false; " onclick="setDate(this);" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请选择生效日期" Display="None" ControlToValidate="txtoperationDate" /><font
                        color="red"> * </font>
            </td>
        </tr>
        <div id="divSalary" runat="server">
        <tr>
            <td class="oddrow">
                转正前基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowBasePay"  Enabled="false" runat="server" />
            </td>
            <td class="oddrow">
                转正前绩效工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowMeritPay"  Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                转正后基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewBasePay" MaxLength="10" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请填写调整后月薪" Display="None" ControlToValidate="txtnewBasePay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtnewBasePay"
                    ErrorMessage="请输入正确调整后月薪" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确转正后基本工资</asp:RegularExpressionValidator>
            </td>
            <td class="oddrow">
                转正后绩效工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewMeritPay" MaxLength="10" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请填写调整后绩效工资" Display="None" ControlToValidate="txtnewMeritPay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewMeritPay"
                    ErrorMessage="请输入正确调整后绩效工资" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确转正后绩效工资</asp:RegularExpressionValidator>
            </td>
        </tr>
        </div>
        <tr>
            <td class="oddrow">
                备注:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtmemo" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                    <asp:Button ID="btnCommit" runat="server" Text=" 转正 " OnClientClick="att();" CssClass="widebuttons" OnClick="btnCommit_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
                <input id="SelectedModuleArr" type="hidden" value="" runat="server" NAME="SelectedModuleArr" /> 
        <input id="SelectedModuleName" type="hidden" value="" runat="server" name="SelectedModuleName" />
		<input id="UpdateModuleArr" type="hidden" value="" runat="server" NAME="UpdateModuleArr" />
		<input id="UpdateModuleName" type="hidden" value="" runat="server" name="UpdateModuleName" />
		<input id="SelectedBossArr" type="hidden" value="" runat="server" NAME="SelectedBossArr" />
		<input id="SelectedBossName" type="hidden" value="" runat="server" name="SelectedBossName" />
		<input id="UpdateBossArr" type="hidden" value="" runat="server" NAME="UpdateBossArr" />
		<input id="UpdateBossName" type="hidden" value="" runat="server" name="UpdateBossName" />
</asp:Content>
