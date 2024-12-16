<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FieldworkEdit.aspx.cs" EnableEventValidation="false" Inherits="SEPAdmin.HR.Employees.FieldworkEdit" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		

    <script src="/public/js/jquery.js" type="text/javascript"></script>
    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script src="/public/js/dialog.js" type="text/javascript"></script>
    <script type="text/javascript" src="/HR/Employees/js/UserDepartment.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#container-1').tabs();
            show();
        });

        $(document).ready(function() {
        show();
            
            $("#<%=drpJob_JoinJob.ClientID %>").change(function() {
                $("#<%=txtJob_JoinJob.ClientID %>").val($("#<%=drpJob_JoinJob.ClientID %>").val());
        });
    });

        function initItCode(objEmail) {
            var itcode = objEmail.value.split('@');

            if (itcode.length == 2) {
                document.getElementById("<%=txtItCode.ClientID%>").value = itcode[0];
            }
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        
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
        
        function btnClick() {
            dialog("部门列表", "url:post?/HR/Employees/DepartmentsTree.aspx?principal=1", "500px", "500px", "text"); showSelect();
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
     
     
     <table width="100%">   
     <tr>
     <td>     
		<div id="container-1" >
            <ul>
                <li><a href="#fragment-1"><span>基本信息</span></a></li>
                <li><a href="#fragment-3"><span>协议情况</span></a></li>                
            </ul>
            <div id="fragment-1" >  
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l"  style="width: 10%">
                            <asp:CheckBox ID="chkBaseInfoOk" runat="server"  />
                        </td>
                        <td class="oddrow" style="width: 10%" >
                            是否使用公司系统:
                        </td>
                        <td class="oddrow-l"  style="width: 10%" colspan="5">
                            <asp:CheckBox ID="chkIsDeleted" runat="server"  />
                        </td>
                    </tr>                    
                 
                 <tr>
                 <td class="oddrow-l" colspan="6" align="center">
                        <div class="photocontainer">
                            <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg" CssClass="imagePhoto" ToolTip="头像"/>
                        </div>
                        </br>                        
                        <asp:HyperLink ID="linkPhoto" runat="server">点击上传照片</asp:HyperLink>
                  </td>                  
                 </tr>       
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            公司Email:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtEmail" runat="server" onblur="initItCode(this)"/> <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="请填写公司Email">请填写公司Email</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="请输入正确Email地址" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">请输入正确Email地址</asp:RegularExpressionValidator>
                        </td>   
                        <td class="oddrow"  style="width: 10%">
                            登录名:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">                        
                            <asp:TextBox ID="txtItCode" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtItCode" Display="Dynamic" ErrorMessage="请填写登录名">请填写登录名</asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            员工号:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtUserId" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtUserId" Display="Dynamic" ErrorMessage="请填写员工号">请填写员工号</asp:RequiredFieldValidator>
                        </td>
                                             
                    </tr>
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            姓&nbsp;<asp:TextBox ID="txtBase_LastNameCn" runat="server" width="50px"/>&nbsp;名&nbsp;<asp:TextBox ID="txtBase_FitstNameCn" runat="server" width="50px"/>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 15%" >
                            FirstName&nbsp;<asp:TextBox ID="txtBase_FirstNameEn" runat="server"  width="50px"/>&nbsp;LastName&nbsp;<asp:TextBox ID="txtBase_LastNameEn" runat="server"  width="50px"/>
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            性别:
                        </td>
                        <td class="oddrow-l" style="width: 15%" >
                            <asp:DropDownList ID="txtBase_Sex" runat="server">
                                <asp:ListItem Value="0" Text="未知" />
                                <asp:ListItem Value="1" Text="男" />
                                <asp:ListItem Value="2" Text="女" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            出生日期:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtBase_Birthday" runat="server"  onclick="setDate(this);"/>
                        </td>                        
                        <td class="oddrow" >
                            籍贯:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtBase_PlaceOfBirth" runat="server" />
                        </td>
                        <td class="oddrow" >
                            户口所在地:
                        </td>
                        <td class="oddrow-l">
                           <asp:TextBox ID="txtBase_DomicilePlace" runat="server" />                                                   
                        </td>                        
                    </tr>                   
                    <tr>
                        <td class="oddrow">
                            工作特长:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_WorkSpecialty" runat="server" />
                        </td>
                        <td class="oddrow">
                            身份证号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_IdNo" runat="server" />
                        </td>
                        <td class="oddrow">
                            紧急事件联系人:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_EmergencyLinkman" runat="server" />
                        </td>                        
                    </tr>
                    <tr>
                        <td class="oddrow">
                            健康状况:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Health" runat="server" />
                        </td>
                        <td class="oddrow">
                            婚否:
                        </td>
                        <td class="oddrow-l">                          
                            <asp:DropDownList ID="txtBase_Marriage" runat="server">
                                <asp:ListItem Value="0" Text="未知" />
                                <asp:ListItem Value="1" Text="已婚" />
                                <asp:ListItem Value="2" Text="未婚" />
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow">
                            紧急事件联系人电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_EmergencyPhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            IP电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_IPCode" runat="server" />
                        </td>
                        <td class="oddrow">
                            联系电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Phone1" runat="server" />
                        </td>
                        <td class="oddrow">
                            手机:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_MobilePhone" runat="server" />
                        </td>                         
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            通讯地址:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Address1"  runat="server" />
                        </td>
                        <td class="oddrow">
                          邮政编码:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_PostCode" runat="server" />
                        </td>
                        <td class="oddrow">
                            宅电:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_HomePhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            毕业院校:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:TextBox ID="txtBase_FinishSchool" runat="server" />
                        </td>                        
                        <td class="oddrow" style="width: 10%">
                            毕业时间:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:TextBox ID="txtBase_FinishSchoolDate" runat="server"  onclick="setDate(this);"/>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            学历:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:DropDownList ID="txtBase_Education" runat="server">
                                <asp:ListItem Text="高中/中专/中技及以下" Value="高中/中专/中技及以下"></asp:ListItem>
                                <asp:ListItem Text="大专及同等学历" Value="大专及同等学历"></asp:ListItem>
                                <asp:ListItem Text="本科/学士及等同学历" Value="本科/学士及等同学历"></asp:ListItem>
                                <asp:ListItem Text="硕士/研究生及等同学历" Value="硕士/研究生及等同学历"></asp:ListItem>
                                <asp:ListItem Text="博士及以上" Value="博士及以上"></asp:ListItem>
                                <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                            </asp:DropDownList>
                        </td>                        
                        
                    </tr>
                    <tr>
                         <td class="oddrow">
                            专业:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:TextBox ID="txtBase_Speciality" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职日期:
                        </td>
                        <td class="oddrow-l"  >
                            <asp:TextBox ID="txtJob_JoinDate" runat="server"  onclick="setDate(this);"/>
                        </td>                        
                        <td class="oddrow-l" colspan="2">
                          
                        </td>                        
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            所属公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%"> 
                            <asp:TextBox ID="txtJob_CompanyName" runat="server"  /> <font color="red">*</font>
                            <asp:HiddenField ID="hidCompanyId" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtJob_CompanyName" Display="Dynamic" ErrorMessage="请选择所属公司">请选择所属公司</asp:RequiredFieldValidator>
                            <input type="button" id="btndepartment" class="widebuttons" value="选择..." onclick="btnClick();" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            部门:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtJob_DepartmentName" runat="server"  onkeyDown="return false; "/>
                            <asp:HiddenField ID="hidDepartmentID" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            组别:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtJob_GroupName" runat="server"  onkeyDown="return false; "/>
                            <asp:HiddenField ID="hidGroupId" runat="server" />
                        </td>
                    </tr>
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
                    <tr>                      
                        
                        <td class="oddrow">
                            职位:
                        </td>
                        <td class="oddrow-l">
                            <asp:HiddenField ID="txtJob_JoinJob" runat="server" /> 
                            <asp:DropDownList ID="drpJob_JoinJob" runat="server" onclientchange=""/> <font color="red">*</font>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="drpJob_JoinJob" Display="Dynamic" ErrorMessage="请选择职位" 
                                ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                        </td>    
                        <td class="oddrow-l" colspan="2">
                            
                        </td>                       
                                     
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            上级主管姓名:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtJob_DirectorName" runat="server" />
                        </td>
                        <td class="oddrow">
                          上级主管职位:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtJob_DirectorJob" runat="server" />
                        </td>
                         <td class="oddrow">
                            工资所属公司:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_thisYearSalary" runat="server" />
                        </td>
                    </tr>
                 </table>    
                 <table width="100%" class="tableForm">                    
                    
                    </table>
                 <table width="100%" class="tableForm">                             
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
            </div>

            <div id="fragment-3" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l"  style="width: 90%" colspan="5" >
                            <asp:CheckBox ID="chkContractInfoOk" runat="server"  />
                        </td>                        
                    </tr>
                    
                 </table> 
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            协议公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtContract_Company" runat="server" />
                        </td>                       
                        <td class="oddrow" style="width: 10%">
                            协议起始日:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtContract_StartDate" runat="server"  onclick="setDate(this);"/>
                        </td>                   
                    </tr>                     
                    <tr>
                     <td class="oddrow" >
                            固定工资:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtJob_basePay" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtJob_basePay" Display="Dynamic" ErrorMessage="请输入正确固定工资" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确固定工资</asp:RegularExpressionValidator>
                        </td>
                        <td class="oddrow-l" colspan="4"></td>                                     
                    </tr> 
                 </table>
                 <table width="100%" class="tableForm"> 
                        <tr>
                        <td class="oddrow" colspan="8">
                            备注:
                        </td>
                        </tr>
                        <tr>
                        <td class="oddrow-l" colspan="8">
                            <asp:TextBox ID="txtContract_Memo" Height="100px" Width="100%" runat="server" />
                        </td>                                              
                    </tr>
                  </table>
            </div>            
        </div>    
	</td>
    </tr>
	</table>	
	
	<table width="90%">
	    <tr>
	        <td>
	            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click" Text=" 保 存 " />
	            &nbsp;&nbsp;&nbsp;
	            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click" Text=" 返 回 " />
	        </td>
	    </tr>
	</table>	
                 
</asp:Content>
