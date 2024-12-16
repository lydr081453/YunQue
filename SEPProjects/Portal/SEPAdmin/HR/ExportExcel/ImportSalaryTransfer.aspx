<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportSalaryTransfer.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.ExportExcel.ImportSalaryTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

    <div>
         <table width="100%" class="tableForm">  
                    <tr>
                        <td class="oddrow" style="width:10%">薪资调整上传</td>
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
                        <td  ><a href="../ExcelTemplate/ImportSalaryTransfer.xls" style="color: #6d787d;"  >模版</a></td>
                    </tr>
                    </table>
                    <table width="100%" class="tableForm">  
                    <tr>
                    <td class="oddrow" style="width:50%">上传说明：</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">1、请使用本模版导入薪资调整数据</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">2、请不要修改本模版的标题及其位置；</td>                    
                    </tr>
                    <tr>
                     <td class="oddrow" style="width:50%">3、请确保数据字段对应并正确；</td>                    
                    </tr>                    
                    <tr> 
                     <td class="oddrow" style="width:50%">4、员工编号和薪资信息的值不能为空，无值请填“0”（零），生效日期必填；</td>                                       
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
