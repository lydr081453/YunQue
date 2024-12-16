<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_JobTransferEdit"
    MasterPageFile="~/MasterPage.master" CodeBehind="JobTransferEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="/HR/Employees/js/UserDepartment.js"></script>

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
            document.getElementById("<%= txtJob_NewGroupName.ClientID%>").value = nameValue;
            document.getElementById("<%= hiddepartmentId.ClientID%>").value = idValue;
            if (idValues[0] != "") {
                drpPositionsBind(idValues[0]);
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                员工调动信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                员工姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" /><asp:HiddenField ID="hiduserId"
                    runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                加入公司时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_JoinDate" runat="server" Enabled="false" />
            </td>
            
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                现属业务团队:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:DropDownList ID="ddlnowGroupName" runat="server" Style="width: auto" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                具体调动说明
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                新业务团队:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_NewGroupName" runat="server" Enabled="false" /><input type="button"
                    id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" /><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ErrorMessage="请选择新的业务团队" Display="None" ControlToValidate="txtJob_NewGroupName" /><font
                            color="red"> * </font> --%>
                <asp:HiddenField ID="hiddepartmentId" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                新岗位职责:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:HiddenField ID="hidJob_JoinJob" runat="server" />
                <asp:DropDownList ID="drpJob_JoinJob" runat="server" onclientchange="" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpJob_JoinJob"
                    Display="Dynamic" ErrorMessage="请选择职位" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="oddrow">
                是否负责人
            </td>
            <td class="oddrow-l">
                <input type="checkbox" id="chkManager" runat="server" />
            </td>
            <td class="oddrow">
                是否代理负责人
            </td>
            <td class="oddrow-l">
                <input type="checkbox" id="chkActing" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                调动时间:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_TransferDate" runat="server" onclick="setDate(this);" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ErrorMessage="请选择调动时间" Display="None"
                    ControlToValidate="txtJob_TransferDate" /><font color="red"> * </font>
            </td>
            <td class="oddrow">
                调动地点:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_TransferPlace" runat="server" />
            </td>
        </tr>
         <div id="divSalary" runat="server"> 
        <tr>
            <td class="oddrow">
                原基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowBasePay" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">
                原绩效工资
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowMeritPay" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                新基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewBasePay" runat="server" /><font color="red">* </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写新基本工资"
                    Display="None" ControlToValidate="txtnewBasePay" /><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtnewBasePay"
                        ErrorMessage="请输入正确新基本工资" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新基本工资</asp:RegularExpressionValidator>
            </td>
            <td class="oddrow">
                新绩效工资
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewMeritPay" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写新业务绩效"
                    Display="None" ControlToValidate="txtnewMeritPay" /><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewMeritPay"
                        ErrorMessage="请输入正确新业务绩效" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新绩效工资</asp:RegularExpressionValidator>
            </td>
        </tr>
        </div>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                安排调动（由HR填写）
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                调动原因:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_transferReason" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                调动时限:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_transferTimeLine" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                事宜安排（如需交接）:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_evenPlan" runat="server" />
            </td>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtoperationDate" runat="server" onkeyDown="return false; " onclick="setDate(this);" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="请选择生效日期" Display="None"
                    ControlToValidate="txtoperationDate" /><font color="red"> * </font>
            </td>
        </tr>
    </table>
    <%--<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                调动核准
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                原团队核准意见:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_nowAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                新团队核准意见:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_newAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                人事部:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_hrAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
    </table>--%>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow">
                备注:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <%-- <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClientClick="att();" CausesValidation="false" CssClass="widebuttons" OnClick="btnSave_Click" />   
                    &nbsp;--%><asp:Button ID="btnCommit" runat="server" Text=" 提交 " OnClientClick="att();"
                        CausesValidation="false" CssClass="widebuttons" OnClick="btnCommit_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
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
</asp:Content>
