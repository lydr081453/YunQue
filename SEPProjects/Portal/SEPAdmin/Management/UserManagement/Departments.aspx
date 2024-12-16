<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="SEPAdmin.UserManagement.Departments" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
		<table style="WIDTH:100%;HEIGHT:100%">
			<tr>
			    <td>
		            <frameset cols="200,*" border="0" frameborder="0" framespacing="0" id="treeFrame2">
			            <frame src="DepartmentsTree.aspx" name="tree2" scrolling="yes">
			            <frame src="DepartmentsForm.aspx" name="modify2" scrolling="yes">
		            </frameset>
		        </td>
		        
		    </tr>
		</table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>