<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsTree.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.DepartmentsTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="width:auto">
    <form id="form1" runat="server">  
    <div id="floatDiv"  runat="server">    
        <table style="WIDTH: 100%;" >
                <tr><td><a id="hr" href="DepartmentsForm.aspx?ParentID=0" runat="server">新增根目录</a></td></tr>
				<tr>
					<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP:0px"
						colspan="2">
						<table id="Table2" width="100%">							
							<tr>							
							    <td valign="top">                                    
                                        <asp:TreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn" ShowLines="true" style="BORDER-RIGHT:0px inset; BORDER-TOP:0px inset; OVERFLOW:auto; BORDER-LEFT:0px inset; WIDTH:100%; BORDER-BOTTOM:0px inset; HEIGHT:100%; BACKGROUND-COLOR:white"
                                            NodeIndent="20">
                                            <ParentNodeStyle Font-Bold="False" />
                                            <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                            <SelectedNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False"  />
                                            <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                                NodeSpacing="1px" VerticalPadding="2px" />
                                        </asp:TreeView>                                                                            
                                  </td>
                               </tr>
						</table>
					</td>
				</tr>				
			</table> 
			</div>    
    </form>
</body>
</html>
