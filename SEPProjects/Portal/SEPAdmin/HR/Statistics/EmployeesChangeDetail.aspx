<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesChangeDetail.aspx.cs" Inherits="SEPAdmin.HR.Statistics.EmployeesChangeDetail" MasterPageFile="~/HR/Validate/ValidateMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server" >	
    <table width="100%"  class="tableForm">   
     <tr>
     <td>     
		<div id="container-1" >
           <%-- <ul>
                <li><a href="#fragment-1"><span>基本信息</span></a></li>

                <li><a href="#fragment-3"><span>合同情况</span></a></li>
                <li><a href="#fragment-4"><span>五险一金</span></a></li>
                
                <li><a href="#fragment-6"><span>档案情况</span></a></li>
            </ul>--%>
            <div id="fragment-1" >  
                 <table width="100%" class="tableForm">                    
                     <tr>
                         <td class="oddrow-l" colspan="6" align="center">
                                <div class="photocontainer">
                                    <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg" CssClass="imagePhoto" ToolTip="头像"/>
                                </div>                        
                          </td>                  
                     </tr> 
                     <tr>
                        <td class="heading" colspan="5" >
                            基本信息
                        </td>                   
                    </tr>              
                    <tr>
                        <%--<td class="oddrow" style="width: 10%" >
                            填表日期
                        </td>
                        <td class="oddrow-l"  style="width: 15%">
                            <asp:TextBox ID="txtBase_CreateDate" runat="server"  onclick="setDate(this);"/>
                        </td>--%>
                        <td class="oddrow"  style="width: 10%">
                            员工编号:
                        </td>
                        <td class="oddrow-l" colspan="3"  style="width: 15%">                        
                            <asp:Label ID="labUserCode" runat="server" />
                        </td>
                        
                    </tr>             
                    <tr>
                        <td class="oddrow"  style="width: 10%">
                            中文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:Label ID="labFullNameCn" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            英文姓名:
                        </td>
                        <td class="oddrow-l" style="width: 15%" >
                            <asp:Label ID="labFullNameEn" runat="server"/>
                        </td>  
                        <td class="oddrow" >
                            公司常用名:
                        </td>
                        <td class="oddrow-l">
                           <asp:Label ID="labCommonName" runat="server" />                                                                               
                        </td>                       
                        
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            出生日期:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labBase_Birthday" runat="server" />
                        </td>                        
                        <td class="oddrow" >
                            籍贯:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labBase_PlaceOfBirth" runat="server" />
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            性别:
                        </td>
                        <td class="oddrow-l" style="width: 15%" >
                            <asp:Label ID="labBase_Sex" runat="server"/>                            
                        </td>
                                               
                    </tr>
                     <tr>
                     <td class="oddrow" >
                            外籍员工:
                        </td>
                        <td class="oddrow-l"  >  
                            <asp:Label ID="labForeign" runat="server" />                     
                        </td>
                        <td class="oddrow" >
                            就业证:
                        </td>
                        <td class="oddrow-l"  >  
                            <asp:Label ID="labCertification" runat="server" />                    
                        </td>
                        <td class="oddrow"  style="width: 10%">
                            工资领取月数:
                        </td>
                        <td class="oddrow-l" style="width: 20%">                            
                            <asp:Label ID="labWageMonths" runat="server" />
                        </td>  
                                              
                    </tr>
                    <tr>
                        <td class="oddrow">
                            工作特长:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_WorkSpecialty" runat="server" />
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
                            IP电话:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_IPCode" runat="server" />
                        </td>
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
                         
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            通讯地址:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Address1"  runat="server" />
                        </td>
                        <td class="oddrow">
                          邮政编码:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_PostCode" runat="server" />
                        </td>
                        <td class="oddrow">
                            宅电:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_HomePhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            毕业院校:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:Label ID="labBase_FinishSchool" runat="server" />
                        </td>                        
                        <td class="oddrow" style="width: 10%">
                            毕业时间:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:Label ID="labBase_FinishSchoolDate" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">
                            学历:
                        </td>
                        <td class="oddrow-l" style="width: 15%">
                            <asp:Label ID="labBase_Education" runat="server" />                            
                        </td>   
                    </tr>                    
                    <tr>
                         <td class="oddrow">
                            专业:
                        </td>
                        <td class="oddrow-l" style="width: 10%">
                            <asp:Label ID="labBase_Speciality" runat="server" />
                        </td>
                        <td class="oddrow">
                            入职日期:
                        </td>
                        <td class="oddrow-l"  >
                            <asp:Label ID="labJob_JoinDate" runat="server"/>
                        </td> 
                        <td class="oddrow">
                          电子邮件:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBase_Email" runat="server" />                            
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
                            <asp:Label ID="labBase_thisYearSalary" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" >
                            户口所在地:
                        </td>
                        <td class="oddrow-l">
                           <asp:Label ID="labBase_DomicilePlace" runat="server" />                                                                                
                        </td>
                        <td class="oddrow-l" colspan="4" /> 
                    </tr>
                 </table>                
                 <table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" style="width:10%">简历文档</td>
                        <td class="oddrow-l" ><asp:Label ID="labResume" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" colspan="6">
                            工作简历:
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6" >
                            <asp:Label ID="labBase_WorkExperience" Width="100%" runat="server" />
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" colspan="6">
                            备注:
                        </td>
                        </tr>
                    <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:Label ID="labJob_Memo" Width="100%" runat="server" />
                        </td>
                    </tr>               
                </table>
            </div>

            <div id="fragment-3" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="heading" colspan="6" >
                            合同情况
                        </td>                        
                    </tr>                  
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            合同公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labContract_Company" runat="server" />
                        </td>                       
                        <td class="oddrow" style="width: 10%">
                            合同起始日:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labContract_StartDate" runat="server"/>
                        </td>
                        <td class="oddrow" style="width: 10%">
                            合同终止日:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="labContract_EndDate" runat="server" />
                        </td>                        
                    </tr>
                     <tr>
                        <td class="oddrow">
                            试用期截止日:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labContract_ProbationPeriodDeadLine" runat="server" />
                        </td>                         
                        <td class="oddrow">
                            转正日期:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labContract_ProbationEndDate" runat="server" />
                        </td> 
                        <td class="oddrow">
                            续签次数:
                        </td>
                        <td class="oddrow-l" >
                        <asp:Label ID="labRenewalCount" runat="server" />次
                        </td>                
                    </tr>
                    <tr>
                    <td class="oddrow">
                            合同签订情况:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labJContract_ContractInfo" runat="server" />                           
                        </td>
                    <td class="oddrow-l" colspan="4"></td>
                    </tr>
                    <div id="divSalary" runat="server">
                    <tr>
                     <td class="oddrow" >
                            固定工资:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labJob_basePay" runat="server" />                            
                        </td>
                        <td class="oddrow">
                          标准绩效:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labJob_meritPay" runat="server" />                            
                        </td> 
                        <td class="oddrow-l" colspan="2"></td>                                        
                    </tr> 
                    </div>
                 </table>
                 <table width="100%" class="tableForm"><tr>
                        <td class="oddrow" colspan="6">
                            备注:
                        </td>
                        </tr>
                        <tr>
                        <td class="oddrow-l" colspan="6">
                            <asp:Label ID="labContract_Memo" Height="100px" Width="100%" runat="server" />
                        </td> 
                  </table>
            </div>
            <div id="fragment-4" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="heading" colspan="6" >
                            保险福利
                        </td>                        
                    </tr>
                    
                 </table> 
                 <table width="100%" class="tableForm"> 
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            公司上社会保险:
                        </td>
                        <td class="oddrow-l" style="width: 23%">                            
                            <asp:Label ID="labInsurance_Endowment" runat="server" />
                        </td>                        
                        <td class="oddrow" >
                            公司上公积金:
                        </td>                        
                        <td class="oddrow-l" >                            
                            <asp:Label ID="labPublicReserveFunds_IsPublicReserveFunds" runat="server" />
                        </td>   
                        <td class="oddrow-l" colspan="2" >  </td>                  
                    </tr>
                    <tr>
                    <td class="oddrow" >社会保险开始时间</td><td class="oddrow-l" ><asp:Label ID="labEndowmentStarTime" runat="server"  /></td>                    
                    <td class="oddrow" >公积金开始时间</td><td class="oddrow-l" ><asp:Label ID="labPublicReserveFundsStarTime" runat="server"  /></td> 
                    <td class="oddrow-l" colspan="2" >  </td>                   
                    </tr>  
                       <tr>
                    <td class="oddrow" >社会保险结束时间</td><td class="oddrow-l" ><asp:Label ID="labEndowmentEndTime" runat="server"  /></td>                    
                    <td class="oddrow" >公积金结束时间</td><td class="oddrow-l" ><asp:Label ID="labPublicReserveFundsEndTime" runat="server"  /></td> 
                    <td class="oddrow-l" colspan="2" >  </td>                   
                    </tr>                
                     <tr>
                        <td class="oddrow" style="width: 10%">
                            户口所在地:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                             <asp:Label ID="labInsurance_MemoPlace" runat="server"  />                            
                        </td>   
                        <td class="oddrow" style="width: 10%">
                            社保所在公司:
                        </td>
                        <td class="oddrow-l" style="width: 20%" >
                            <asp:Label ID="labInsurance_SocialInsuranceCompany" runat="server" />
                        </td>                         
                        <td class="oddrow" style="width: 10%">
                            社保所属地点:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labInsurance_SocialInsuranceAddress" runat="server" />
                        </td>                                          
                    </tr>
                    <div id="divBase" runat="server">
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
                        <td class="oddrow-l" >
                            <asp:Label ID="labInsurance_MedicalInsuranceBase" runat="server" />
                        </td> 
                        <td class="oddrow">
                            公积金基数:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labPublicReserveFunds_Base" runat="server" />
                        </td>                                       
                    </tr>  
                                        <tr>                     
                        <td class="oddrow">
                            医疗保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labMIProportionOfFirms" runat="server" />%
                        </td> 
                        <td class="oddrow">
                            医疗保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labMIProportionOfIndividuals" runat="server" />%
                        </td>                         
                        <td class="oddrow">
                            医疗保险大额医疗个人支付额:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labMIBigProportionOfIndividuals" runat="server" />元
                        </td>                                       
                    </tr>   
                    <tr>                     
                        <td class="oddrow">
                            养老保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labEIProportionOfFirms" runat="server" />%
                        </td> 
                        <td class="oddrow">
                            养老保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labEIProportionOfIndividuals" runat="server" />%
                        </td>                         
                        <td class="oddrow">
                            生育保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labBIProportionOfFirms" runat="server" />%
                        </td>                                       
                    </tr>  
                    <tr>                     
                        <td class="oddrow">
                            失业保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labUIProportionOfFirms" runat="server" />%
                        </td> 
                        <td class="oddrow">
                            失业保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labUIProportionOfIndividuals" runat="server" />%
                        </td>                         
                        <td class="oddrow">
                            公积金比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labPRFProportionOfFirms" runat="server" />%
                        </td>                                        
                    </tr>  
                    <tr>                     
                        <td class="oddrow">
                            工伤险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labCIProportionOfFirms" runat="server" />%
                        </td> 
                        <td class="oddrow-l" colspan="4" >                            
                        </td>                                        
                    </tr>                    
                    </div>   
                     <tr>
                        <td class="oddrow">
                            补充医疗:
                        </td>
                        <td class="oddrow-l">
                            <asp:Label ID="labComplementaryMedical" runat="server" />
                        </td> 
                        <td class="oddrow">
                            意外保险:
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labAccidentInsurance" runat="server" />
                        </td> 
                        <td class="oddrow-l" colspan="2" >                         
                    </tr>                                              
                </table>
                
            </div>

            <div id="fragment-6" >
                 <table width="100%" class="tableForm">                    
                    <tr>
                        <td class="heading" colspan="6" >
                            档案情况
                        </td>                        
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            在公司挂档:
                        </td>                        
                        <td class="oddrow-l" style="width: 20%">                            
                            <asp:Label ID="labArchive_ArchivePlace" runat="server" />                            
                        </td>  
                        <td class="oddrow" style="width: 10%">
                            档案编号:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_Code" runat="server" />
                        </td>   
                        <td class="oddrow" style="width: 10%">
                            存档日期:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:Label ID="labArchive_ArchiveDate" runat="server"/>
                        </td>                          
                    </tr>
                 </table>
                 
            </div>
        </div>
        
       
	</td>
    </tr>
	</table>
     <table width="100%" class="tableForm">  
            <tr>
                <td class="oddrow" colspan="6">
                    部门职务:
                </td>
            </tr>                    
            <tr>
                <td >
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID,DepartmentPositionID,DepartmentID" Width="100%" >
                         <Columns>
                            <asp:BoundField DataField="DepartmentName" HeaderText="所属团队" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="DepartmentPositionName" HeaderText="职务" ItemStyle-HorizontalAlign="Center"/>                                    
                        </Columns>
                    </asp:GridView>     
                </td>
            </tr>                      
     </table>
     <table width="100%" id="tabTop" runat="server" class="tableForm">
        <tr>
            <td class="oddrow" colspan="6">
                转正情况:
            </td>
        </tr>
        <tr>
            <td> 
            <asp:GridView ID="gvPassList" runat="server" AutoGenerateColumns="False"  DataKeyNames="id" Width="100%" >
                <Columns>                    
                    <asp:BoundField DataField="groupName" HeaderText="所属团队" />
                    <asp:BoundField DataField="departmentName" HeaderText="所在部门" />
                    <asp:TemplateField HeaderText="转正时间">
                        <ItemTemplate>
                            <%# Eval("operationDate").ToString().Split(' ')[0]%>
                        </ItemTemplate>
                    </asp:TemplateField>            
                </Columns>                      
            </asp:GridView>    
            </td>
        </tr>
    </table>
    <div id="divSalary2" runat="server">
    <table width="100%"  id="tableUp" class="tableForm">  
        <tr>
            <td class="oddrow" colspan="6">
                薪资调整情况:
            </td>
        </tr>
        <tr>
          <td >
            <asp:GridView ID="gvSalaryList" runat="server" AutoGenerateColumns="False"  DataKeyNames="id" Width="100%" >
                 <Columns>                    
                    <asp:BoundField DataField="job" HeaderText="职位" />                    
                    <asp:TemplateField HeaderText="调整时间">
                        <ItemTemplate>
                            <%# Eval("operationDate").ToString().Split(' ')[0]%>
                        </ItemTemplate>
                    </asp:TemplateField>          
                </Columns>
            </asp:GridView> 
        </td>
      </tr>
    </table>
    </div>
    <table width="100%"  id="table1" class="tableForm">  
    <tr>
        <td class="oddrow" colspan="6">
            部门调整情况:
        </td>
    </tr>                 
    <tr>
      <td >
        <asp:GridView ID="gvJobList" runat="server" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" >
         <Columns>            
            <asp:BoundField DataField="nowCompanyName" HeaderText="原公司" />
            <asp:BoundField DataField="nowDepartmentName" HeaderText="原部门" />
            <asp:BoundField DataField="nowGroupName" HeaderText="原团队" />
            <asp:BoundField DataField="newCompanyName" HeaderText="新公司" />
            <asp:BoundField DataField="newDepartmentName" HeaderText="新部门" />
            <asp:BoundField DataField="newGroupName" HeaderText="新团队" />
            <asp:TemplateField HeaderText="调整时间">
                <ItemTemplate>
                    <%# Eval("operationDate").ToString().Split(' ')[0]%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>            
        </td>                   
    </tr>         
 </table>
 <table width="100%"  id="table2" class="tableForm"> 
    <tr>
        <td class="oddrow" colspan="6">
            职位调整情况:
        </td>
    </tr>         
    <tr>
       <td >
        <asp:GridView ID="gvPromList" runat="server" AutoGenerateColumns="False"  DataKeyNames="id" Width="100%" >
         <Columns>            
            <asp:BoundField DataField="companyName" HeaderText="公司" />
            <asp:BoundField DataField="departmentName" HeaderText="部门" />
            <asp:BoundField DataField="groupName" HeaderText="团队" />
            <asp:BoundField DataField="currentTitle" HeaderText="原职位" />
            <asp:BoundField DataField="targetTitle" HeaderText="最终职位" /> 
            <asp:TemplateField HeaderText="调整时间">
                <ItemTemplate>
                    <%# Eval("operationDate").ToString().Split(' ')[0]%>
                </ItemTemplate>
            </asp:TemplateField>           
        </Columns>
        </asp:GridView>            
       </td>                   
    </tr>
  </table>
  <table width="100%" id="Table3" runat="server" class="tableForm">
  <tr>
        <td class="oddrow" colspan="6">
            离职情况:
        </td>
    </tr>
     <tr>
       <td>                
        <asp:GridView ID="gvDimList" runat="server" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" >
            <Columns>
                <asp:BoundField DataField="groupName" HeaderText="所属团队" />
                <asp:BoundField DataField="departmentName" HeaderText="所在部门" />
                <asp:TemplateField HeaderText="离职时间">
                    <ItemTemplate>
                        <%# Eval("dimissionDate").ToString().Split(' ')[0]%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>    
       </td>
     </tr>
  </table>
  <table width="100%"  id="table4" class="tableForm"> 
        <tr>
            <td colspan="4" class="oddrow-l">
                <input type="button" runat="server" value=" 返回 " class="widebuttons" id="btnBack" onserverclick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
