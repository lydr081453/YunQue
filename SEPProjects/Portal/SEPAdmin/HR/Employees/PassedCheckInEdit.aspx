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
                $("#<%=drpJob_JoinJob.ClientID %>").append("<option value=\"-1\">��ѡ��...</option>");
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
                    window.attachEvent("onbeforeunload", function() { $(".widebuttons").val("��ȴ�").attr("disabled", "true"); });
                } else {
                    window.addEventListener('onbeforeunload', function() { $(".widebuttons").val("��ȴ�").attr("disabled", "true"); }, false);
                }
            }
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function btnClick() {
            dialog("�����б�", "url:post?/Management/UserManagement/DepartmentsTree.aspx?principal=1", "500px", "500px", "text"); showSelect();
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
                ת���Ǽ���Ϣ
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                ����:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" /><asp:HiddenField ID="hiduserId" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                ��ְ����:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtjoinJobDate"  Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                ת��ǰ��������:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowDepartment" runat="server"  Enabled="false" />       
            </td>
            <td class="oddrow">
                ת��ǰְλ:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowTitle" runat="server"  Enabled="false" /><asp:HiddenField ID="hidnowjob" runat="server" />               
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                ת������������:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewDepartmente" runat="server" Enabled="false" /><input type="button" id="btndepartment"
                    class="widebuttons" value="ѡ��..." onclick="btnClick();" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ErrorMessage="��ѡ��ת������������" Display="None" ControlToValidate="txtnewDepartmente" /><font
                            color="red"> * </font> <asp:HiddenField ID="hiddepartmentId" runat="server" />                 
            </td>
            <td class="oddrow">
                ת����ְλ:
            </td>
            <td class="oddrow-l">
                <asp:HiddenField ID="hidJob_JoinJob" runat="server" />
                <asp:DropDownList ID="drpJob_JoinJob" runat="server" onclientchange=""/>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="drpJob_JoinJob" Display="Dynamic" ErrorMessage="��ѡ��ְλ" 
                    ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="oddrow">�Ƿ�����</td>
            <td class="oddrow-l"><input type="checkbox" id="chkManager" runat="server" /></td>
            <td class="oddrow">�Ƿ��������</td>
            <td class="oddrow-l"><input type="checkbox" id="chkActing" runat="server" /></td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                ��Ч����:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtoperationDate" runat="server" onkeyDown="return false; " onclick="setDate(this);" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="��ѡ����Ч����" Display="None" ControlToValidate="txtoperationDate" /><font
                        color="red"> * </font>
            </td>
        </tr>
        <div id="divSalary" runat="server">
        <tr>
            <td class="oddrow">
                ת��ǰ��������:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowBasePay"  Enabled="false" runat="server" />
            </td>
            <td class="oddrow">
                ת��ǰ��Ч����:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowMeritPay"  Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                ת�����������:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewBasePay" MaxLength="10" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="����д��������н" Display="None" ControlToValidate="txtnewBasePay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtnewBasePay"
                    ErrorMessage="��������ȷ��������н" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷת�����������</asp:RegularExpressionValidator>
            </td>
            <td class="oddrow">
                ת����Ч����:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewMeritPay" MaxLength="10" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="����д������Ч����" Display="None" ControlToValidate="txtnewMeritPay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewMeritPay"
                    ErrorMessage="��������ȷ������Ч����" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷת����Ч����</asp:RegularExpressionValidator>
            </td>
        </tr>
        </div>
        <tr>
            <td class="oddrow">
                ��ע:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtmemo" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                    <asp:Button ID="btnCommit" runat="server" Text=" ת�� " OnClientClick="att();" CssClass="widebuttons" OnClick="btnCommit_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" ���� " CssClass="widebuttons"
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
