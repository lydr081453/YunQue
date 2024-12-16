<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_PromotionTransferDetail" MasterPageFile="~/MasterPage.master" Codebehind="PromotionTransferDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >

<table width="100%" class="tableForm">   
        <tr>
             <td class="heading" colspan="6">员工调动信息</td>
        </tr>
        <tr>
            <td class="oddrow"  style="width: 20%">
                员工姓名:
            </td>
            <td class="oddrow-l"  style="width: 30%">                        
                <asp:Label ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow"  style="width: 20%">
                加入公司时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_JoinDate" runat="server" Enabled="false" />
            </td>
         </tr>
         <tr>
            <td class="oddrow">
                现属业务团队:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="ddlnowGroupName"  runat="server" style="width:auto" />
            </td>  
            <td class="oddrow">
            现职位:
            </td>  
            <td class="oddrow-l">
                <asp:Label ID="labnowjob" runat="server"  />
            </td>                     
        </tr> 
        <tr>
            <td class="oddrow">
                最终职位:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="ddlcurrentTitle" runat="server" style="width:auto" />
            </td>  
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtoperationDate" runat="server" onkeyDown="return false; "  />
            </td>                  
        </tr> 
        <div id="divSalary" runat="server"> 
        <tr>
            <td class="oddrow">
                原基本工资:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="txtnowBasePay" runat="server" />
            </td>
            <td class="oddrow">
            原绩效工资               
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnowMeritPay" runat="server" />
            </td> 
            </tr>  
        <tr>
            <td class="oddrow">
                新基本工资:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="txtnewBasePay" runat="server" />
            </td>
            <td class="oddrow">
            新绩效工资              
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnewMeritPay" runat="server" />
            </td> 
            </tr> 
            </div>    
        </table> 
   <table width="100%" class="tableForm">         
        <tr>
         <td class="oddrow">
                备注:
            </td>                      
        </tr>                 
        <tr>
            <td class="oddrow-l" >
                 <asp:Label ID="txtJob_Memo"  Height="100px" Width="100%" runat="server" />
            </td>
        </tr>   
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>            
   </table>

</asp:Content>

