<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SocialSecurityEdit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.SocialSecurity.SocialSecurityEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		
	<table width="100%" class="tableForm">     
	                <tr>
                        <td class="oddrow" style="width: 10%">福利社保开始时间</td>
                        <td class="oddrow-l" style="width: 20%">
                        <asp:DropDownList ID="drpBeginTimeY" runat="server"></asp:DropDownList>年
                        <asp:DropDownList ID="drpBeginTimeM" runat="server"></asp:DropDownList>月
                        </td> 
                        <td class="oddrow" style="width: 10%">福利社保结束时间</td>
                        <td class="oddrow-l" style="width: 20%">
                        <asp:DropDownList ID="drpEndTimeY" runat="server"></asp:DropDownList>年
                        <asp:DropDownList ID="drpEndTimeM" runat="server"></asp:DropDownList>月
                        </td>                          
                        <td class="oddrow" style="width: 10%">所属公司</td>
                        <td class="oddrow-l" style="width: 20%">
                        <asp:DropDownList ID="drpCompany" runat="server"></asp:DropDownList>
                        
                        </td>                                    
                    </tr>              
                    <tr>                     
                        <td class="oddrow">
                            医疗保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMIProportionOfFirms" runat="server"  />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                ControlToValidate="txtMIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确医疗保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            医疗保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtMIProportionOfIndividuals" runat="server"  />%
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
                            <asp:TextBox ID="txtEIProportionOfFirms" runat="server" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                                ControlToValidate="txtEIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确养老保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            养老保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtEIProportionOfIndividuals" runat="server" />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                                ControlToValidate="txtEIProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确养老保险个人比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老保险个人比例</asp:RegularExpressionValidator>
                        </td>                         
                        <td class="oddrow">
                            生育保险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBIProportionOfFirms" runat="server"  />%
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
                            <asp:TextBox ID="txtUIProportionOfFirms" runat="server"  />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                                ControlToValidate="txtUIProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确失业保险公司比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确失业保险公司比例</asp:RegularExpressionValidator>
                        </td> 
                        <td class="oddrow">
                            失业保险个人比例:
                        </td>
                        <td class="oddrow-l" >
                            <asp:TextBox ID="txtUIProportionOfIndividuals" runat="server"  />%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                                ControlToValidate="txtUIProportionOfIndividuals" Display="Dynamic" ErrorMessage="请输入正确失业保险个人比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确失业保险个人比例</asp:RegularExpressionValidator>
                        </td>       
                        <td class="oddrow">
                            工伤险公司比例:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtCIProportionOfFirms" runat="server"/>%
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
                            <asp:TextBox ID="txtPRFProportionOfFirms" runat="server"/>%
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                                ControlToValidate="txtPRFProportionOfFirms" Display="Dynamic" ErrorMessage="请输入正确公积金比例" 
                                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确公积金比例</asp:RegularExpressionValidator>
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
