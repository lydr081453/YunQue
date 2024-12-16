<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsTree.aspx.cs" Inherits="SEPAdmin.HR.Employees.DepartmentsTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        function selectDept(level1id,level2id,level3id ,level1,level2,level3,principal) {
            art.dialog.data('level1id', level1id);
            art.dialog.data('level2id', level2id);
            art.dialog.data('level3id', level3id);
            art.dialog.data('level1', level1);
            art.dialog.data('level2', level2);
            art.dialog.data('level3', level3);

            if (artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtJob_CompanyName") != null && principal != 4) {
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtJob_CompanyName").value = level1;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtJob_DepartmentName").value = level2;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtJob_GroupName").value = level3;
                //                    
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidCompanyId").value = level1id;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidDepartmentID").value = level2id;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidGroupId").value = level3id;
            }
            else if (artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtTransferCompanyIn") != null && principal==4)
            {
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtTransferCompanyIn").value = level1;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtTransferDeptIn").value = level2;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtTransferGroupIn").value = level3;
                //                    
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidTransferCompanyIn").value = level1id;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidTransferDeptIn").value = level2id;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_hidTransferGroupIn").value = level3id;
            }
            else {
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtJob_CompanyName").value = level1;
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtJob_DepartmentName").value = level2;
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtJob_GroupName").value = level3;
                //
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_hidCompanyId").value = level1id;
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_hidDepartmentID").value = level2id;
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_hidGroupId").value = level3id;

                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtJob_JoinJob").value = "";
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtPosition").value = "";
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_hidAuditer").value = "";
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtAuditer").value = "";      
            }

            art.dialog.close();
        }
    </script>
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
			<br />
			<br />
			<br />
			</div>    
    </form>
</body>
</html>
