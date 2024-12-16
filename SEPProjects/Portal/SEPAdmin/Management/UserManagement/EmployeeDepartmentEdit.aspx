<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDepartmentEdit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.Management.UserManagement.EmployeeDepartmentEdit" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#container-1').tabs();
            show();

        });

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }


        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '部门列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function PositionClick() {
            var deptid = document.getElementById("<%= hidGroupId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
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

               // drpPositionsBind(idValues[0]);
            }

            if (idValues.length == 2 && nameValues.length == 2) {

                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = "";
                document.getElementById("<%= hidGroupId.ClientID%>").value = "";

                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[0];

                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[1];

               // drpPositionsBind(idValues[0]);
            }

            if (idValues.length == 3 && nameValues.length == 3) {

                document.getElementById("<%= txtJob_GroupName.ClientID%>").value = nameValues[0];
                document.getElementById("<%= hidGroupId.ClientID%>").value = idValues[0];

                document.getElementById("<%= txtJob_DepartmentName.ClientID%>").value = nameValues[1];
                document.getElementById("<%= hidDepartmentID.ClientID%>").value = idValues[1];

                document.getElementById("<%= txtJob_CompanyName.ClientID%>").value = nameValues[2];
                document.getElementById("<%= hidCompanyId.ClientID%>").value = idValues[2];

               // drpPositionsBind(idValues[0]);
            }

        }          
     </script>
     
     
     <table width="100%" style="min-height:400px;">   
     <tr>
     <td>
     
		<div id="container-1" >
            <ul>
                <li><a href="#fragment-1"><span>部门职务信息</span></a></li>
            </ul>
            <div id="fragment-1" >  
                 <table width="100%" class="tableForm" style="min-height:400px;">                    
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            所属公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtJob_CompanyName" runat="server" />
                            <asp:HiddenField ID="hidCompanyId" runat="server"/>
                            <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtJob_CompanyName" Display="Dynamic" ErrorMessage="请选择公司">请选择公司</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            部门:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtJob_DepartmentName" runat="server" />
                            <asp:HiddenField ID="hidDepartmentID" runat="server"/>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            组别:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtJob_GroupName" runat="server" />
                            <asp:HiddenField ID="hidGroupId" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        
                         <td class="oddrow">
                            入职职位:
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
                    
                 </table>
                 
        </div>
        
        
        <!-- 部门树-->
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
		<input id="hidnowBasePay" type="hidden" runat="server" />
		<input id="hidnowMeritPay" type="hidden" runat="server" />
	</td>
    </tr>
	</table>	
	
	<table width="90%">
	    <tr>
	        <td>
	            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click" Text=" 保 存 " />
	            &nbsp;&nbsp;&nbsp;
	            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click" Text=" 返 回 " CausesValidation="false" />
	        </td>
	    </tr>
	</table>	
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />      
                    <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />    
</asp:Content>

