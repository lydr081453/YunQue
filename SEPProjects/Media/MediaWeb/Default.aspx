<%@ Page language="c#" Inherits="FrameSite._Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
<script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
<script type="text/javascript">
    $(document).ready(
    function PREntry() {
        var url = "";
        var prentry = "<%=Request[RequestName.ProjectID] %>";
        var billid = "<%=Request[RequestName.BillID] %>";

        if (prentry == null || prentry=="" || prentry == "undifined") {
            url = '/System/Default.aspx';
        }
        else if (prentry == "0") {
            url = "/Project/ProjectInfo.aspx?ProjectID=0&<% =RequestName.ProjectCode %>=<% =Request[RequestName.ProjectCode] %>&Operate=add";
            }
            else {
                url = "/Bill/WritingFeeBillEdit.aspx?<%=RequestName.ProjectID%>=<% =Request[RequestName.ProjectID]%>&Operate=add";
            }
        var modifyframe = document.getElementById("FrmModify");
        if (modifyframe != null) {
            modifyframe.src = url;
        }
    }
    );
</script>

		<title>系统管理</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<frameset rows="85,*" border="0" frameborder="0" framespacing="0" name="frmContainer">
		<frame src="include/page/Header.aspx" name="header" noresize scrolling="no">
		<frameset cols="16,73" border="0" frameborder="0" framespacing="0" id="MainFrame">
			<frame src="include/page/ModelTree.aspx" noresize name="tree" scrolling="no">
			<frame src="System/Default.aspx" id="FrmModify" name="modify" scrolling="no">
		</frameset>
	</frameset>
</html>
