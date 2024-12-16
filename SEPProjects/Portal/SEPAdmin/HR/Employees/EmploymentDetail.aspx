<%@ Page Language="C#" AutoEventWireup="true" Inherits="Employees_EmploymentDetail" MasterPageFile="~/MasterPage.master" Codebehind="EmploymentDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		
	<script src="/public/js/jquery-1.1.3.1.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
            $(function() {
                $('#container-1').tabs(); 
            });
     </script>
     <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen">
        <!-- Additional IE/Win specific style sheet (Conditional Comments) -->
        <!--[if lte IE 7]>
        <link rel="stylesheet" href="/public/css/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->        
		<div id="container-1">
            <ul>
                <li><a href="#fragment-1"><span>基本信息</span></a></li>
                <li><a href="#fragment-2"><span>入职登记表</span></a></li>
                <li><a href="#fragment-3"><span>合同情况</span></a></li>
                <li><a href="#fragment-4"><span>保险情况</span></a></li>
                <li><a href="#fragment-5"><span>公积金情况</span></a></li>
                <li><a href="#fragment-6"><span>档案情况</span></a></li>
            </ul>
            <div id="fragment-1">                
                 <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">
                            <asp:Label ID="labJob_SysId" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_NameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_NameEn" runat="server" />
                        </td>                        
                    </tr>
                    <tr>
                        <td class="oddrow">
                            户口所在地:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_DomicilePlace" runat="server" />
                        </td>                        
                        <td class="oddrow">
                            婚姻状况:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_Marriage" runat="server" />
                        </td>
                        <td class="oddrow">
                            身份证号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_IdNo" runat="server" />
                        </td>                        
                    </tr>
                     <tr>
                        <td class="oddrow">
                            出生日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_Birthday" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职年龄:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_Age" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2">                            
                        </td>                    
                    </tr>
                    <tr>
                        <td class="oddrow">
                            教育程度 (中文):
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_Education" runat="server" />
                        </td>
                        <td class="oddrow">
                            毕业院校:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_FinishSchool" runat="server" />
                        </td>
                        <td class="oddrow">
                            专业:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_Speciality" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            所属公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_CompanyName" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            部门:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_DepartmentName" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            组别:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_GroupName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            职位:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="Label24" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_JoinDate" runat="server" />
                        </td>
                         <td class="oddrow">
                            入职职位:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_JoinJob" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            上级主管姓名:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_DirectorName" runat="server" />
                        </td>
                        <td class="oddrow">
                          上级主管职位:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labJob_DirectorJob" runat="server" />
                        </td>
                         <td class="oddrow">
                            工资所属公司:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="Label1" runat="server" />
                        </td>
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">  
                    <tr>
                     <td class="oddrow" style="width: 10%">
                            固定工资:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_basePay" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                          标准绩效:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labJob_meritPay" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2">                            
                        </td>  
                    </tr>                 
                    <tr>
                        <td class="oddrow" colspan="6">
                            备注:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6" >
                            <asp:Label ID="labJob_Memo" runat="server" />
                        </td>
                    </tr>               
                </table>
            </div>
            <div id="fragment-2">
                <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            编号
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labBase_SysId" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%" >
                            填表日期
                        </td>
                        <td class="oddrow-l"  >
                            <asp:Label ID="labBase_CreateDate" runat="server" />
                        </td>
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            姓名:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labBase_Name" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            出生日期:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labBase_Birthday" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            籍贯:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labBase_PlaceOfBirth" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labBase_DomicilePlace" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            毕业院校:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_FinishSchool" runat="server" />
                        </td>                        
                        <td class="oddrow">
                            毕业时间:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_FinishSchoolDate" runat="server" />
                        </td>
                        <td class="oddrow">
                            学历:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Education" runat="server" />
                        </td>
                        <td class="oddrow">
                            专业:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Speciality" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            入职日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_JoinDate" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职部门:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_01" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职职位:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_JoinJob" runat="server" />
                        </td>
                        <td class="oddrow-l" rowspan="5" colspan="2" align="center">
                        <img src="" id="imgBase_Photo" alt="一寸照片" style="width:3cm;height:4cm" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            工作特长:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_StrongSuit" runat="server" />
                        </td>
                        <td class="oddrow">
                            身份证号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_IdNo" runat="server" />
                        </td>
                        <td class="oddrow">
                            紧急事件联系人:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_EmergencyLinkman" runat="server" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="oddrow">
                            健康状况:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Health" runat="server" />
                        </td>
                        <td class="oddrow">
                            婚否:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Marriage" runat="server" />
                        </td>
                        <td class="oddrow">
                            紧急事件联系人电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_EmergencyPhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            联系电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Phone1" runat="server" />
                        </td>
                        <td class="oddrow">
                            手机:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_MobilePhone" runat="server" />
                        </td>
                         <td class="oddrow">
                            宅电:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_HomePhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            通讯地址:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="labBase_Address1" runat="server" />
                        </td>
                        <td class="oddrow">
                          邮政编码:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_PostCode" runat="server" />
                        </td>
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow">
                            工作简历:
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" >
                            <asp:Label ID="labBase_WorkExperience" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_DiseaseInSixMonths" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            备注:(请填写其他资料：通过的考试、重要培训资格、特长或业余爱好、个人资料变更等)
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" >
                            <asp:Label ID="labBase_Memo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            附件:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="8">
                            <asp:Label ID="Label4" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="fragment-3">
                <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">
                            <asp:Label ID="labContract_SysId" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labContract_NameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labContract_NameEn" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            合同公司:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labContract_Company" runat="server" />
                        </td>  
                        <td class="oddrow" style="width: 10%">
                            社保所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labContract_Social" runat="server" />
                        </td>
                                              
                        <td class="oddrow" style="width: 10%">
                            合同起始日:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labContract_StartDate" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            合同终止日:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labContract_EndDate" runat="server" />
                        </td>                        
                    </tr>
                     <tr>
                        <td class="oddrow">
                            试用期时长:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labContract_ProbationPeriod" runat="server" />
                        </td>
                        <td class="oddrow">
                            试用期截止日:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labContract_ProbationPeriodDeadLine" runat="server" />
                        </td>                         
                        <td class="oddrow">
                            社保所属地点:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="lsbContract_SocialInsuranceAddress" runat="server" />
                        </td>  
                        <td class="oddrow">
                            转正日期:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labContract_ProbationEndDate" runat="server" />
                        </td>                 
                    </tr>
                    <tr>
                        <td class="oddrow">
                            合同文件:
                        </td>                        
                        <td class="oddrow-l" >
                            <asp:Label ID="Label11" runat="server" />
                        </td>
                        <td class="oddrow-l" colspan="6">                            
                        </td>
                        </tr>
                        <tr>
                        <td class="oddrow" colspan="8">
                            备注:
                        </td>
                        </tr>
                        <tr>
                        <td class="oddrow-l" colspan="8">
                            <asp:Label ID="labContract_Memo" runat="server" />
                        </td>                                              
                    </tr>
                    </table>
            </div>
            <div id="fragment-4">
                <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">
                            <asp:Label ID="labInsurance_SysId" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_NameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_NameEn" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            养老保险:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labInsurance_Endowment" runat="server" />
                        </td>  
                        <td class="oddrow" style="width: 10%">
                            失业保险:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labInsurance_Unemployment" runat="server" />
                        </td>
                                              
                        <td class="oddrow" style="width: 10%">
                            生育险:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labInsurance_Birth" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            工伤险:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labInsurance_Compo" runat="server" />
                        </td> 
                        <td class="oddrow" style="width: 10%">
                            医疗保险:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labInsurance_Medical" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_DomicilePlace" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            社保所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_SocialInsuranceCompany" runat="server" />
                        </td>                         
                        <td class="oddrow" style="width: 10%">
                            社保所属地点:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_SocialInsuranceAddress" runat="server" />
                        </td>                                          
                    </tr>
                    <tr>
                        <td class="oddrow">
                            社保编号:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labInsurance_SocialInsuranceCode" runat="server" />
                        </td>
                        <td class="oddrow">
                            医疗编号:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labInsurance_MedicalInsuranceCode" runat="server" />
                        </td>   
                        <td class="oddrow-l" colspan="2">                            
                        </td> 
                        </tr>
                        <tr>                     
                        <td class="oddrow">
                            养老/失业/工伤/生育缴费基数:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labInsurance_SocialInsuranceBase" runat="server" />
                        </td> 
                        <td class="oddrow">
                            医疗基数:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labInsurance_MedicalInsuranceBase" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2">                            
                        </td>                                          
                    </tr>                    
                    </table>
            </div>
            <div id="fragment-5">
                <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_SysId" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_NameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_NameEn" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            公积金:
                        </td>                        
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_IsPublicReserveFunds" runat="server" />
                        </td>  
                        <td class="oddrow-l" style="width: 70%">                            
                        </td>                      
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            公积金所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_Company" runat="server" />
                        </td>                         
                        <td class="oddrow" style="width: 10%">
                            公积金所属地点:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labPublicReserveFunds_Address" runat="server" />
                        </td>                                          
                    </tr>
                    <tr>                     
                        <td class="oddrow">
                            公积金基数:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labPublicReserveFunds_Base" runat="server" />
                        </td> 
                        <td class="oddrow">
                           公积金编号:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labPublicReserveFunds_Code" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2">                            
                        </td>                                          
                    </tr>                    
                    </table>
            </div>
            <div id="fragment-6">
                <table width="100%" class="tableForm">   
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l"  style="width: 20%">
                            <asp:Label ID="labArchive_SysId" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_NameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_NameEn" runat="server" />
                        </td>                        
                    </tr>
                    </table>
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            在公司挂档:
                        </td>                        
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_IsArchive" runat="server" />
                        </td>  
                        <td class="oddrow-l" style="width: 70%">                            
                        </td>                      
                    </tr>
                    </table>
                 <table width="100%" class="tableForm">
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            存档日期:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_ArchiveDate" runat="server" />
                        </td>                                             
                    </tr>
                    <tr>                     
                        <td class="oddrow">
                            档案编号:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labArchive_Code" runat="server" />
                        </td>                         
                        <td class="oddrow-l" colspan="2">                            
                        </td>                                          
                    </tr>                    
                    </table>
            </div>
        </div>
</asp:Content>

