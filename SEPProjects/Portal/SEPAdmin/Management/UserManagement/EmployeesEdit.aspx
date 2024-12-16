<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesEdit.aspx.cs" MasterPageFile="~/MainMaster.Master" Inherits="SEPAdmin.Management.UserManagement.EmployeesEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >		
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
        });

        function reloadCalendar() {
            if (document.getElementById("<%=txtContract_StartDate.ClientID%>").value != '') {
                var yyyy = parseInt(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[0]);
                var mm = parseInt(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[1]);
                var dd = parseInt(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[2]);

                var mydate = new Date(yyyy, mm + 6, dd);

                document.getElementById("<%=txtContract_ProbationPeriodDeadLine.ClientID%>").value = mydate.getFullYear() + '-' + (mydate.getMonth()) + '-' + mydate.getDate();

                document.getElementById("<%=txtContract_ProbationEndDate.ClientID%>").value = mydate.getFullYear() + '-' + (mydate.getMonth()) + '-' + mydate.getDate();

            }
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd', 'reloadCalendar()');
        }

        function setCosts(pro, base, costs) {
            var pro = "ctl00_ContentPlaceHolder1_" + pro;
            var base = "ctl00_ContentPlaceHolder1_" + base;
            var costs = "ctl00_ContentPlaceHolder1_" + costs;
            var eif = Number(document.getElementById("" + pro + "").value);
            var sbase = Number(document.getElementById("" + base + "").value);
            if (eif != "NaN" && sbase != "NaN") {
                var val = sbase * (eif / 100);
                document.getElementById("" + costs + "").value = ForDight(val, 2);
            }
        }

        function setMICosts() {
            var mii = Number(document.getElementById("<%=txtMIProportionOfIndividuals.ClientID%>").value);
            var mif = Number(document.getElementById("<%=txtMIProportionOfFirms.ClientID%>").value);
            var mbase = Number(document.getElementById("<%=txtInsurance_MedicalInsuranceBase.ClientID%>").value);
            if (mif != "NaN" && mbase != "NaN") {
                var val = mbase * (mif / 100);
                document.getElementById("<%=txtMIFCosts.ClientID%>").value = ForDight(val, 2);
            }
            if (mii != "NaN" && mbase != "NaN") {
                var val2 = mbase * (mii / 100);
                document.getElementById("<%=txtMIICosts.ClientID%>").value = ForDight(val2, 2);
            }
        }
        function setSICosts() {
            var eii = Number(document.getElementById("<%=txtEIProportionOfIndividuals.ClientID%>").value);
            var eif = Number(document.getElementById("<%=txtEIProportionOfFirms.ClientID%>").value);
            var bif = Number(document.getElementById("<%=txtBIProportionOfFirms.ClientID%>").value);
            var uif = Number(document.getElementById("<%=txtUIProportionOfFirms.ClientID%>").value);
            var uii = Number(document.getElementById("<%=txtUIProportionOfIndividuals.ClientID%>").value);
            var cif = Number(document.getElementById("<%=txtCIProportionOfFirms.ClientID%>").value);

            var sbase = Number(document.getElementById("<%=txtInsurance_SocialInsuranceBase.ClientID%>").value);
            if (eif != "NaN" && sbase != "NaN") {
                var val = sbase * (eif / 100);
                document.getElementById("<%=txtEIFCosts.ClientID%>").value = ForDight(val, 2);
            }
            if (eii != "NaN" && sbase != "NaN") {
                var val2 = sbase * (eii / 100);
                document.getElementById("<%=txtEIICosts.ClientID%>").value = ForDight(val2, 2);
            }
            if (bif != "NaN" && sbase != "NaN") {
                var val3 = sbase * (bif / 100);
                document.getElementById("<%=txtBIFCosts.ClientID%>").value = ForDight(val3, 2);
            }
            if (uif != "NaN" && sbase != "NaN") {
                var val4 = sbase * (uif / 100);
                document.getElementById("<%=txtUIFCosts.ClientID%>").value = ForDight(val4, 2);
            }
            if (uii != "NaN" && sbase != "NaN") {
                var val5 = sbase * (uii / 100);
                document.getElementById("<%=txtUIICosts.ClientID%>").value = ForDight(val5, 2);
            }
            if (cif != "NaN" && sbase != "NaN") {
                var val6 = sbase * (cif / 100);
                document.getElementById("<%=txtCIFCosts.ClientID%>").value = ForDight(val6, 2);
            }
        }
        //四舍五入
        function ForDight(Dight, How) {
            var Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
            return Dight;
        }
        function changeChecked() {
            var cmchk = document.getElementById("<%= chkComplementaryMedical.ClientID %>");
            var accchk = document.getElementById("<%= chkAccidentInsurance.ClientID %>");
            var txtcm = document.getElementById("<%=  txtComplementaryMedical.ClientID%>");
            var accy = document.getElementById("<%= drpAccidentInsuranceBeginTimeY.ClientID %>");
            var accm = document.getElementById("<%= drpAccidentInsuranceBeginTimeM.ClientID %>");
            if (cmchk.checked) {
                txtcm.disabled = false;
            }
            else {
                txtcm.disabled = true;
            }
            if (accchk.checked) {
                accy.disabled = false;
                accm.disabled = false;
            }
            else {
                accy.disabled = true;
                accm.disabled = true;
            }
        }
        function changeInsurance() {
            try {
                var ins = document.getElementById("<%= drpInsurance_MemoPlace.ClientID %>").options;
                var bic = document.getElementById("<%= txtBIFCosts.ClientID%>");
                var uii = document.getElementById("<%= txtUIICosts.ClientID %>");

                var value;

                for (i = 0; i < ins.length; i++) {
                    if (ins[i].selected)
                        value = ins[i];
                }
                if (value.value == "外阜城镇户口" || value.value == "外阜农业户口") {
                    if (value.value == "外阜农业户口") {
                        uii.value = "0";
                        uii.disabled = true;
                    }
                    else {
                        uii.disabled = false;
                        setSICosts();
                    }
                    bic.value = "0";
                    bic.disabled = true;

                }
                else {
                    bic.disabled = false;
                    uii.disabled = false;
                    setSICosts();
                }



            }
            catch (err) { return; }
        }
     </script>
     
     
     <table width="100%">   
     <tr>
     <td>
     
		<div id="container-1" >
            <ul>
                <li><a href="#fragment-1"><span>基本信息</span></a></li>

                <li><a href="#fragment-3"><span>合同情况</span></a></li>
                <li><a href="#fragment-4"><span>保险福利</span></a></li>
                
                <li><a href="#fragment-6"><span>档案情况</span></a></li>
            </ul>
            <div id="fragment-1" >  
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l" colspan="7"  style="width: 90%">
                            <asp:CheckBox ID="chkBaseInfoOk" runat="server"  />
                        </td>
                    </tr>                    
                 
                 <tr>
                 <td class="oddrow-l" colspan="6" align="center">
                        <div class="photocontainer">
                            <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg" CssClass="imagePhoto" ToolTip="头像"/>
                        </div>
                        </br>
                        <a href="UpLoadUserPhoto.aspx?userid=<%=Request["userid"]%>" >点击上传照片</a>
                  </td>                  
                 </tr>       
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            填表日期
                        </td>
                        <td class="oddrow-l"  style="width: 15%">
                            <asp:TextBox ID="txtBase_CreateDate" runat="server"  onclick="setDate(this);"/>
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l" colspan="3"  style="width: 15%">                        
                            <asp:Label ID="txtUserCode" runat="server" />
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
                        <td class="oddrow-l"  >  
                            是否外籍员工<input type="checkbox" id="chkForeign" runat="server" />                       
                        </td>
                        <td class="oddrow-l"  >  
                            是否有就业证<input type="checkbox" id="chkCertification" runat="server" />                       
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            工资领取月数:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:DropDownList ID="drpWageMonths" runat="server">                                
                                
                                <asp:ListItem Value="13" Text="13个月" />
                                <asp:ListItem Value="12" Text="12个月" />
                            </asp:DropDownList>
                        </td>  
                        <td class="oddrow" >
                            公司常用名:
                        </td>
                        <td class="oddrow-l">
                           <asp:TextBox ID="txtCommonName" runat="server" />                                                                               
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
                            <asp:TextBox ID="txtPhone1" Width="30px" runat="server" />-<asp:TextBox ID="txtPhone2" Width="30px" runat="server" />-<asp:TextBox ID="txtPhone3" Width="80px" runat="server" />-<asp:TextBox ID="txtPhone4" Width="50px" runat="server" />
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
                        <td class="oddrow">
                          电子邮件:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBase_Email" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                ControlToValidate="txtBase_Email" Display="Dynamic" ErrorMessage="请输入正确Email地址" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">请输入正确Email地址</asp:RegularExpressionValidator>
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
                    <tr>
                     <td class="oddrow" >
                            户口属性:
                        </td>
                        <td class="oddrow-l" colspan="5">
                            <asp:DropDownList runat="server" ID="ddlHuji">
                    <asp:ListItem Selected="True" Text="请选择.." Value="0"></asp:ListItem>
                    <asp:ListItem Text="本市城镇" Value="本市城镇"></asp:ListItem>
                    <asp:ListItem Text="外埠城镇" Value="外埠城镇"></asp:ListItem>
                    <asp:ListItem Text="本市农业" Value="本市农业"></asp:ListItem>
                    <asp:ListItem Text="外埠农业" Value="外埠农业"></asp:ListItem>
                </asp:DropDownList>
                        </td>
                    </tr>
                 </table>    
                 <table width="100%" class="tableForm">                    
                    
                    </table>
                 <table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" colspan="6">
                            部门职务:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <UL>
					            <LI><asp:Button ID="btnAddDepart" runat="server" CssClass="widebuttons" OnClick="btnAddDepart_Click" Text="添加职务" /></LI>
				            </UL>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                                  OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging" OnRowCommand="gvList_RowCommand" DataKeyNames="UserID,DepartmentPositionID,DepartmentID" Width="100%" >
                                 <Columns>
                                    <asp:BoundField DataField="CompanyName" HeaderText="公司" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="GroupName" HeaderText="团队" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="DepartmentPositionName" HeaderText="职务" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1"  CommandName="Del" runat="server" ImageUrl="../../images/disable.gif" CommandArgument='<%# Eval("UserID") + "," +  Eval("DepartmentPositionID") + "," + Eval("GroupID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>     
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <table width="100%" id="tabBottom" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>     
                </table>
                 <table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" style="width:10%">简历文档上传</td>
                        <td class="oddrow-l" ><asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">
                            工作简历:
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6" >
                            <asp:TextBox ID="txtBase_WorkExperience" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
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
            </div>

            <div id="fragment-3" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l"  style="width: 90%" colspan="5">
                            <asp:CheckBox ID="chkContractInfoOk" runat="server"  />
                        </td>
                    </tr>
                    
                 </table> 
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            合同公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtContract_Company" runat="server" />
                        </td>                       
                        <td class="oddrow" style="width: 10%">
                            合同起始日:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtContract_StartDate" runat="server"  onclick="setDate(this);"/>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            合同终止日:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtContract_EndDate" runat="server"  onclick="setDate(this);"/>
                        </td>                        
                    </tr>
                     <tr>
                        <td class="oddrow">
                            试用期截止日:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtContract_ProbationPeriodDeadLine" runat="server"  onclick="setDate(this);"/>
                        </td>                         
                        <td class="oddrow">
                            转正日期:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtContract_ProbationEndDate" runat="server"  onclick="setDate(this);"/>
                        </td> 
                        <td class="oddrow">
                            续签次数:
                        </td>
                        <td class="oddrow-l" >
                            <asp:DropDownList ID="drpRenewalCount" runat="server">
                                <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                            次
                        </td>                
                    </tr>
                    <tr>
                    <td class="oddrow">
                            合同签订情况:
                        </td>
                        <td class="oddrow-l" >
                            <asp:DropDownList ID="drpJContract_ContractInfo" runat="server">
                                <asp:ListItem Text="已领取" Value="已领取" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已返回" Value="已返回"></asp:ListItem>
                            </asp:DropDownList>
                        </td>  
                        <td class="oddrow-l" colspan="4"></td>
                    </tr>
                    <div id="divSalary" runat="server">
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
                        <td class="oddrow">
                          标准绩效:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtJob_meritPay" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtJob_meritPay" Display="Dynamic" ErrorMessage="请输入正确标准绩效" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确标准绩效</asp:RegularExpressionValidator>
                        </td>
                        <td class="oddrow-l" colspan="2"></td>               
                    </tr> 
                    </div>
                 </table>
                 <table width="100%" class="tableForm"> 
                    <tr>
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
            <div id="fragment-4" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l"  style="width: 90%">
                            <asp:CheckBox ID="chkInsuranceInfoOk" runat="server"  />
                        </td>
                    </tr>
                    
                 </table> 
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            社会保险:
                        </td>
                        <td class="oddrow-l" style="width: 23%">                            
                            <asp:CheckBox ID="chkInsurance_Endowment" runat="server" />
                        </td>
                        <td class="oddrow" >
                            公积金:
                        </td>                        
                        <td class="oddrow-l" >                            
                            <asp:CheckBox ID="chkPublicReserveFunds_IsPublicReserveFunds" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2" >        </td>             
                    </tr>
                    <tr>
                    <td class="oddrow" style="width: 10%">社会保险开始时间</td><td class="oddrow-l" style="width: 20%"><asp:DropDownList ID="drpEndowmentStarTimeY" runat="server"></asp:DropDownList>年<asp:DropDownList ID="drpEndowmentStarTimeM" runat="server"></asp:DropDownList>月</td>
                        <td class="oddrow" style="width: 10%">公积金开始时间</td><td class="oddrow-l" style="width: 20%"><asp:DropDownList ID="drpPublicReserveFundsStarTimeY" runat="server"></asp:DropDownList>年<asp:DropDownList ID="drpPublicReserveFundsStarTimeM" runat="server"></asp:DropDownList>月</td>                       
                        <td class="oddrow-l" colspan="2"></td>                  
                    </tr>                    
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:DropDownList ID="drpInsurance_MemoPlace" runat="server" onchange="changeInsurance();">
                                <asp:ListItem Text="本市城镇户口" Value="本市城镇户口" />
                                <asp:ListItem Text="本市农业户口" Value="本市农业户口" />
                                <asp:ListItem Text="外阜城镇户口" Value="外阜城镇户口" />
                                <asp:ListItem Text="外阜农业户口" Value="外阜农业户口" />
                            </asp:DropDownList>
                        </td>   
                        <td class="oddrow" style="width: 10%">
                            社保所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%" >
                            <asp:TextBox ID="txtInsurance_SocialInsuranceCompany" runat="server" />
                        </td>                         
                        <td class="oddrow" style="width: 10%">
                            社保所属地点:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtInsurance_SocialInsuranceAddress" runat="server" />
                        </td>                                          
                    </tr>
                    <div id="divBase" runat="server">
                    <tr>
                        <td class="oddrow"></td>
                        <td class="oddrow-l"><input type="checkbox" id="chkEnd" runat="server" onclick="changeStatus();" />没有社保</td>
                        <td class="oddrow"></td>
                        <td class="oddrow-l"><input type="checkbox" id="chkPRF" runat="server" onclick="changeStatus();" />没有公积金</td> 
                    </tr> 
                     <tr>                     
                        <td class="oddrow">
                            养老/失业/工伤/生育缴费基数:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtInsurance_SocialInsuranceBase" runat="server" onblur="setSICosts();" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                ControlToValidate="txtInsurance_SocialInsuranceBase" Display="Dynamic" ErrorMessage="请输入正确养老/失业/工伤/生育缴费基数" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老/失业/工伤/生育缴费基数</asp:RegularExpressionValidator>
                                <input type="hidden" id="hidProtectionLine" runat="server" />
                        </td> 
                        <td class="oddrow">
                            医疗基数:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtInsurance_MedicalInsuranceBase" runat="server" onblur="setMICosts();" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtInsurance_MedicalInsuranceBase" Display="Dynamic" ErrorMessage="请输入正确医疗基数" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗基数</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            公积金基数:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtPublicReserveFunds_Base" runat="server" onblur="setCosts('txtPRFProportionOfFirms','txtPublicReserveFunds_Base','txtPRFCosts');" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                                ControlToValidate="txtPublicReserveFunds_Base" Display="Dynamic" ErrorMessage="请输入正确公积金基数" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确公积金基数</asp:RegularExpressionValidator>
                        </td>                                       
                    </tr> 
                    <tr>                     
                        <td class="oddrow">
                            医疗保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMIProportionOfFirms" runat="server" onblur="setCosts('txtMIProportionOfFirms','txtInsurance_MedicalInsuranceBase','txtMIFCosts');"  />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                ControlToValidate="txtMIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确医疗保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            医疗保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtMIProportionOfIndividuals" runat="server" onblur="setCosts('txtMIProportionOfIndividuals','txtInsurance_MedicalInsuranceBase','txtMIICosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                ControlToValidate="txtMIProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确医疗保险个人比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗保险个人比例</asp:RegularExpressionValidator>
                        </td>                         
                        <td class="oddrow">
                            医疗保险大额医疗个人支付额:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMIBigProportionOfIndividuals" runat="server" />元
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                ControlToValidate="txtMIBigProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确医疗保险大额医疗个人支付额" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗保险大额医疗个人支付额</asp:RegularExpressionValidator>
                        </td>                                       
                    </tr>   
                    <tr>                     
                        <td class="oddrow">
                            养老保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtEIProportionOfFirms" runat="server" onblur="setCosts('txtEIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtEIFCosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                                ControlToValidate="txtEIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确养老保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            养老保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtEIProportionOfIndividuals" runat="server" onblur="setCosts('txtEIProportionOfIndividuals','txtInsurance_SocialInsuranceBase','txtEIICosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                                ControlToValidate="txtEIProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确养老保险个人比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老保险个人比例</asp:RegularExpressionValidator>
                        </td>                         
                        <td class="oddrow">
                            生育保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBIProportionOfFirms" runat="server" onblur="setCosts('txtBIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtBIFCosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" 
                                ControlToValidate="txtBIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确生育保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确生育保险公司比例</asp:RegularExpressionValidator>
                        </td>                                       
                    </tr>  
                    <tr>                     
                        <td class="oddrow">
                            失业保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtUIProportionOfFirms" runat="server" onblur="setCosts('txtUIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtUIFCosts');"  />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                                ControlToValidate="txtUIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确失业保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确失业保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            失业保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtUIProportionOfIndividuals" runat="server" onblur="setCosts('txtUIProportionOfIndividuals','txtInsurance_SocialInsuranceBase','txtUIICosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                                ControlToValidate="txtUIProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确失业保险个人比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确失业保险个人比例</asp:RegularExpressionValidator>
                        </td>       
                        <td class="oddrow">
                            工伤险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtCIProportionOfFirms" runat="server" onblur="setCosts('txtCIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtCIFCosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                                ControlToValidate="txtCIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确工伤险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确工伤险公司比例</asp:RegularExpressionValidator>
                        </td>                                                                 
                    </tr>  
                    <tr>   
                                         
                        <td class="oddrow">
                            公积金比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtPRFProportionOfFirms" runat="server" onblur="setCosts('txtPRFProportionOfFirms','txtPublicReserveFunds_Base','txtPRFCosts');" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                                ControlToValidate="txtPRFProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确公积金比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确公积金比例</asp:RegularExpressionValidator>
                        </td>                                       
                        <td class="oddrow-l" colspan="4" >                            
                         </td>                                       
                    </tr> 
                     <tr>                     
                        <td class="oddrow">
                            医疗保险公司应缴费用:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMIFCosts" runat="server" />元                           
                        </td> 
                        <td class="oddrow">
                            医疗保险个人应缴费用:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtMIICosts" runat="server" onblur="setMIICosts();" />元                           
                        </td> 
                        <td class="oddrow">
                            公积金应缴费用:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtPRFCosts" runat="server" />元
                        </td>                                       
                    </tr>   
                    <tr>                     
                        <td class="oddrow">
                            养老保险公司应缴费用:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtEIFCosts" runat="server" />元
                        </td> 
                        <td class="oddrow">
                            养老保险个人应缴费用:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtEIICosts" runat="server" />元
                        </td>                         
                        <td class="oddrow">
                            生育保险公司应缴费用:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBIFCosts" runat="server" />元
                        </td>                                       
                    </tr>  
                    <tr>                     
                        <td class="oddrow">
                            失业保险公司应缴费用:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtUIFCosts" runat="server" />元
                        </td> 
                        <td class="oddrow">
                            失业保险个人应缴费用:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtUIICosts" runat="server" />元
                        </td>
                        <td class="oddrow">
                            工伤险公司应缴费用:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtCIFCosts" runat="server" />元
                        </td>                                        
                    </tr> 
                    </div>                                                    
                </table>
                <table width="100%" class="tableForm">                     
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            补充医疗:
                        </td>
                        <td class="oddrow-l" style="width: 20%">                            
                            <asp:CheckBox ID="chkComplementaryMedical" onclick="changeChecked();" runat="server" />
                        </td> 
                        <td class="oddrow">
                            补充医疗费用:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtComplementaryMedical"  runat="server" />元
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" 
                                ControlToValidate="txtComplementaryMedical" Display="Dynamic" ErrorMessage="请输入正确补充医疗费用" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确补充医疗费用</asp:RegularExpressionValidator>
                        </td>  
                        <td class="oddrow-l" colspan="2"></td>   
                    </tr>
                    <tr>                          
                        <td class="oddrow" style="width: 10%" >
                            意外保险:
                        </td>                        
                        <td class="oddrow-l" >                            
                            <asp:CheckBox ID="chkAccidentInsurance" onclick="changeChecked();" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">意外保险开始时间</td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="drpAccidentInsuranceBeginTimeY" runat="server"></asp:DropDownList>年
                            <asp:DropDownList ID="drpAccidentInsuranceBeginTimeM" runat="server"></asp:DropDownList>月
                        </td>  
                        <td class="oddrow-l" colspan="2"></td>                                       
                    </tr>     
               </table>
                                
            </div>

            <div id="fragment-6" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%" >
                            信息是否填写完整:
                        </td>
                        <td class="oddrow-l"  style="width: 90%" colspan="5">
                            <asp:CheckBox ID="chkArchiveInfoOk" runat="server"  />
                        </td>
                    </tr>
                    
                 <%--</table> 
                 <table width="100%" class="tableForm"> --%>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            在公司挂档:
                        </td>                        
                        <td class="oddrow-l" style="width: 20%">                            
                            <%--<asp:CheckBox ID="chkArchive_IsArchive" runat="server" />--%>
                            <asp:DropDownList ID="drpArchive_ArchivePlace" runat="server">
                                <asp:ListItem Text="北京市人才" Value="北京市人才" />
                                <asp:ListItem Text="中智" Value="中智" />
                            </asp:DropDownList>
                        </td>  
                        <td class="oddrow" style="width: 10%">
                            档案编号:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtArchive_Code" runat="server" />
                        </td>   
                        <td class="oddrow" style="width: 10%">
                            存档日期:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtArchive_ArchiveDate" runat="server"  onclick="setDate(this);"/>
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
