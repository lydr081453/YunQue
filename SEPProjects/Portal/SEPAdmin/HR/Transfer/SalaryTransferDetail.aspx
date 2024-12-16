<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_SalaryTransferDetail" MasterPageFile="~/MasterPage.master" Codebehind="SalaryTransferDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
<table width="100%" class="tableForm" id="tableUp"> 
        <tr>
             <td class="heading" colspan="10">薪资调整
             </td>
        </tr>
        <tr>
            <td class="oddrow"  style="width: 20%">中文姓名:</td>
            <td class="oddrow-l"  style="width:30%">
            <asp:Label ID="labuserName" runat="server" Enabled="false" />
            </td>           
            <td class="oddrow" style="width: 20%">工资调整</td>
            <td class="oddrow-l"><asp:Label ID="labPayChange"  runat="server" />
            </td>
            </tr>
            <div id="divSalary" runat="server"> 
        <tr>
            <td class="oddrow" >
                原基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labnowBasePay" runat="server" Enabled="false"/>
            </td>  
            <td class="oddrow">
            原绩效工资               
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labnowMeritPay" runat="server" Enabled="false"/>
            </td>   
            </tr>
        <tr>
             <td class="oddrow" >
                新基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labnewBasePay" runat="server" />
            </td>  
            <td class="oddrow">
            新绩效工资           
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labnewMeritPay" runat="server" />
            </td> 
            </tr>
            </div>
        <tr>
            <td class="oddrow"  style="width: 10%">
                职位:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="labjob" runat="server" Enabled="false"/>
            </td>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laboperationDate" runat="server"  />
            </td>
            </tr>
        <tr>
            <td class="oddrow">
            说明：             
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labmemo"  runat="server" />
            </td>                               
        </tr>        
        <%--<tr>
         <td class="oddrow">
                申报人（总经理）:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labJob_declarer" runat="server" />
            </td>
            <td class="oddrow">
                申报时间:
            </td>
            <td class="oddrow-l"><asp:Label ID="labJob_declarerdate" runat="server"  /></td>
            </tr>
            <tr>
            <td class="oddrow" >
              集团核准（总裁/执行副总裁）:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="labJob_groupApprover" runat="server" />
            </td>  
            <td class="oddrow">
                集团核准时间:
            </td>
            <td class="oddrow-l"><asp:Label ID="labJob_approverdate" runat="server"  /></td>            
        </tr>                 
        <tr>
            <td class="oddrow">
               集团人事部（主管人）:
            </td>           
            <td class="oddrow-l" >
                <asp:Label ID="labJob_groupHr" runat="server" />
            </td>
            <td class="oddrow" >
                集团人事部时间:
            </td>
            <td class="oddrow-l" ><asp:Label ID="labJob_hrdate" runat="server"  /></td>          
        </tr>--%>
        </table>

<asp:Button ID="btnBack" CssClass="widebuttons" OnClick="btnBack_Click" Text=" 返回 " runat="server" />
</asp:Content>
