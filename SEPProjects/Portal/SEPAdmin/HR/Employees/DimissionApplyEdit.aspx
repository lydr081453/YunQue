<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Employees_DimissionApplyEdit" MasterPageFile="~/MasterPage.master" Codebehind="DimissionApplyEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>
<script src="/public/js/dialog.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/UserDepartment.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script language="javascript">
        $(document).ready(function() {
            
            show();
        });
        function att() {
            if (Page_ClientValidate()){
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
            dialog("部门列表", "url:post?/include/page/DepartmentTree.aspx?principal=1", "500px", "500px", "text"); showSelect(); 
        }
        function onPageSubmit() {
            onSubmit();
            var nameValue = document.getElementById("<%= SelectedModuleName.ClientID%>").value;
            var idValue = document.getElementById("<%= SelectedModuleArr.ClientID%>").value;
            if (nameValue.split('-').length == 3) {
                document.getElementById("<%= txtdepartmentName.ClientID%>").value = nameValue.split('-')[1];
                document.getElementById("<%= txtgroupName.ClientID %>").value = nameValue.split('-')[0];

                document.getElementById("<%= hiddepartmentId.ClientID%>").value = idValue.split('-')[1];
                document.getElementById("<%= hidgroupId.ClientID %>").value = idValue.split('-')[0];
            } else if (nameValue.split('-').length == 2) {
                document.getElementById("<%= txtdepartmentName.ClientID%>").value = nameValue.split('-')[0];
                document.getElementById("<%= txtgroupName.ClientID %>").value = "";

                document.getElementById("<%= hiddepartmentId.ClientID%>").value = idValue.split('-')[0];
                document.getElementById("<%= hidgroupId.ClientID %>").value = "";
            } else {
            document.getElementById("<%= txtdepartmentName.ClientID%>").value = "";
            document.getElementById("<%= txtgroupName.ClientID %>").value = "";

            document.getElementById("<%= hiddepartmentId.ClientID%>").value = "";
            document.getElementById("<%= hidgroupId.ClientID %>").value = "";
            }
        }
        function changeStatus() {
            var chkEnd = document.getElementById("<%= chkEndowment.ClientID %>");
            var chkP = document.getElementById("<%= chkPRF.ClientID %>");
            var endowyear = document.getElementById("<%=  drpEndowmentEndTimeY.ClientID %>");
            var endowmonth = document.getElementById("<%= drpEndowmentEndTimeM.ClientID %>");
            var publicyear = document.getElementById("<%= drpPublicReserveFundsEndTimeY.ClientID %>");
            var publicmonth = document.getElementById("<%= drpPublicReserveFundsEndTimeM.ClientID %>");
            if (chkEnd.checked) {
                endowyear.disabled = true;
                endowmonth.disabled = true;
                
            }
            else {
                endowyear.disabled = false;
                endowmonth.disabled = false;

            }
            if (chkP.checked) {
                publicyear.disabled = true;
                publicmonth.disabled = true;
            }
            else {
                publicyear.disabled = false;
                publicmonth.disabled = false;            
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                离职申请信息
            </td>
        </tr>
        <tr>
        <td class="oddrow" style="width: 20%">
                用户编号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCode" runat="server"  />
            </td> 
            <td class="oddrow-l"><input type="checkbox" id="chkFinish" runat="server" />离职手续已办完</td>
            <td class="oddrow-l" ></td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" />
            </td> 
            <td class="oddrow">
                所在公司:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtcompanyName" runat="server"  Enabled="false" />
                <asp:HiddenField ID="hidcompanyId" runat="server" />
            </td>        
        </tr>
        <tr>
            <td class="oddrow">
                所在部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdepartmentName" runat="server"  Enabled="false" />
                <asp:HiddenField ID="hiddepartmentId" runat="server" />
            </td>
            <td class="oddrow">
                所属团队:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtgroupName" runat="server"  Enabled="false" />
                <asp:HiddenField ID="hidgroupId" runat="server" />
            </td>
        </tr>
        <tr>
        <td class="oddrow" style="width: 20%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtjoinJobDate"  Enabled="false"/>
            </td>
            <td class="oddrow">
                离职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdimissionDate" onkeyDown="return false; " onclick="setDate(this);"
                    runat="server" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ErrorMessage="请选择拟定离职日期" ControlToValidate="txtdimissionDate" Display="None" /><font
                            color="red"> * </font>
            </td>
        </tr>
        <tr>
        <td class="oddrow">
                
            </td>
        <td class="oddrow-l"><input type="checkbox" id="chkEndowment" runat="server" onclick="changeStatus();" />没有社保</td>
        <td class="oddrow">
                
            </td>
        <td class="oddrow-l"><input type="checkbox" id="chkPRF" runat="server" onclick="changeStatus();" />没有公积金</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">社会保险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:DropDownList ID="drpEndowmentEndTimeY" runat="server"></asp:DropDownList>年<asp:DropDownList ID="drpEndowmentEndTimeM" runat="server"></asp:DropDownList>月</td>
            <td class="oddrow" style="width: 20%">公积金结束时间</td><td class="oddrow-l" style="width: 30%"><asp:DropDownList ID="drpPublicReserveFundsEndTimeY" runat="server"></asp:DropDownList>年<asp:DropDownList ID="drpPublicReserveFundsEndTimeM" runat="server"></asp:DropDownList>月</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">意外保险结束时间</td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList ID="drpAccidentInsuranceEndTimeY" runat="server"></asp:DropDownList>年
                <asp:DropDownList ID="drpAccidentInsuranceEndTimeM" runat="server"></asp:DropDownList>月
            </td> 
            <td class="oddrow-l" colspan="2"></td> 
        </tr>
        <tr>
            <td class="oddrow">
                离职申请说明:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtdimissionCause" runat="server" TextMode="MultiLine" Width="90%"
                    Height="80px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">                
                <asp:Button ID="btnCommit" runat="server" Text=" 离职 " OnClientClick="att();" CausesValidation="false" CssClass="widebuttons" OnClick="btnCommit_Click" />                      
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnBack_Click" />
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
