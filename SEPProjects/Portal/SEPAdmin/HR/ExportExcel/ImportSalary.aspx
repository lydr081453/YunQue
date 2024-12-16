<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportSalary.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.ExportExcel.ImportSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

    <div>
         <table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" style="width:10%">工作绩效上传</td>
                        <td class="oddrow-l" ><asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume" runat="server" /></td>
                    </tr>
          </table>
        <table width="90%">
	    <tr>
	        <td>
	            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click" Text=" 导 入 " />
	            &nbsp;&nbsp;&nbsp;
	        </td>
	    </tr>
	</table>
	<table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" style="width:10%">模版下载：</td>
                        <td  ><a href="../ExcelTemplate/ImportSalary.xls" style="color: #6d787d;"  >模版</a></td>
                    </tr>
                    </table>
                    <table width="100%" class="tableForm"> 
                    <tr> 
                    <td class="oddrow" style="width:50%">上传说明：</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">1、请使用本模版导入工资数据</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">2、请不要修改本模版的标题及其位置；</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">3、请确保员工编号和基本工资、标准绩效对应并正确；</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">4、本模版暂时只有员工编号、基本工资、标准绩效为有效字段；</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">5、基本工资、标准绩效的值不能为空，无值请填“0”（零）；</td>                                        
                    </tr>
          </table>
        <table width="90%">
	    <tr>
	        <td>
	            <asp:Button ID="Button1" runat="server" CssClass="widebuttons" OnClick="btnSave_Click" Text=" 导 入 " />
	            &nbsp;&nbsp;&nbsp;
	        </td>
	    </tr>
	</table>
	
    </div>

</asp:Content>
