<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionMail.aspx.cs" Inherits="SEPAdmin.HR.Print.DimissionMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style  type="text/css">
body{	
	background-color: #ececec;
	font-family:Arial, Helvetica, sans-serif;
	font-size:12px;
	color:#585858;
	line-height:170%;
}

</style>

</head>
<body>
<form id="form1" runat="server">
<table width="900" border="0" align="center" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
  <tr>
    <td><asp:Image  runat="server" id="imgs" width="880" height="54"  /></td>
  </tr>
  <tr>
    <td style="background-repeat:repeat-x; padding:0 10px 20px 10px;">各位同事,<br />
截止到<asp:Label ID="labYear" runat="server" />年<asp:Label ID="labMonth" runat="server" />月，以下人员已确认离职：<br />
      <br />
      <table width="100%" border="1" cellpadding="0" cellspacing="1" bgcolor="#d6d6d6">
        <tr>
          <td height="28" align="center" bgcolor="#F4F4F4"><strong>姓名</strong></td>
          <td align="center" bgcolor="#F4F4F4"><strong>组别</strong></td>
          <td align="center" bgcolor="#F4F4F4"><strong>职位</strong></td>
          <td align="center" bgcolor="#F4F4F4"><strong>离职日期（考勤日）</strong></td>
          <td align="center" bgcolor="#F4F4F4"><strong>医疗缴存至</strong></td>
          <td align="center" bgcolor="#F4F4F4"><strong>养老、失业、工伤、生育缴存至</strong></td>    
          <td align="center" bgcolor="#F4F4F4"><strong>邮箱</strong></td>        
        </tr>    
                    <asp:Repeater runat="server" ID="rptDimission"  OnItemDataBound="rptDimission_ItemDataBound">   
                    <ItemTemplate>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labFullNameCn" runat="server" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labCompany" runat="server" />-<asp:Label ID="labDepartmentName" runat="server" />-<asp:Label ID="labGroupName" runat="server" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labPositions" runat="server" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labDimissionDate" runat="server" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labMIMonth" runat="server"/>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labSIMonth" runat="server" />
                        </td>   
                        <td align="center" bgcolor="#FFFFFF">
                            <asp:Label ID="labMail" runat="server" />
                        </td>                      
                    </tr>                   
                </ItemTemplate>                             
                    
                    </asp:Repeater>
             </table>
    </td>
    </tr>
    <tr><td style="height:20px"></td></tr> 
    </table>
    </form>
</body>
</html>

