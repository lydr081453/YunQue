<%@ Page language="c#" Inherits="_Default" Codebehind="Default.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
<script type="text/javascript">
    $(document).ready(
    function PREntry() {
        var url = null;
        var prentry = "<%=Request[RequestName.MediaPREntry] %>";

        if (typeof (prentry) == "undefined" || prentry == null || prentry == "" || prentry == "undefined") {
            url = null;
        }
        else if (prentry == "<%=(int)MediaOperateType.MediaPublic %>") {
            url = "/Purchase/Requisition/AddRequisitionStep2.aspx?<%=RequestName.ProjectID%>=<% =Request[RequestName.ProjectID]%>&<%=RequestName.MediaPREntry%>=<%=Request[RequestName.MediaPREntry]%>";
        }
        else if (prentry == "<%=(int)MediaOperateType.MediaPrivate %>") {

            url = "/Purchase/Requisition/AddRequisitionStep2.aspx?<%=RequestName.ProjectID%>=<% =Request[RequestName.ProjectID]%>&<%=RequestName.BillType%>=<%=Request[RequestName.BillType]%>&<%=RequestName.BillID%>=<%=Request[RequestName.BillID]%>&<%=RequestName.MediaPREntry%>=<%=Request[RequestName.MediaPREntry]%>";
        }
        else if (prentry == "<%=(int)MediaOperateType.MediaBill %>") {
            url = "/Purchase/Requisition/MediaProduct.aspx?<%=RequestName.ProjectID%>=<% =Request[RequestName.ProjectID]%>&<%=RequestName.BillType%>=<%=Request[RequestName.BillType]%>&<%=RequestName.BillID%>=<%=Request[RequestName.BillID]%>&<%=RequestName.MediaPREntry%>=<%=Request[RequestName.MediaPREntry]%>&<% =RequestName.GeneralID %>=<% =Request[RequestName.GeneralID] %>&<%=RequestName.ProjectCode%>=<% =Request[RequestName.ProjectCode]%>";
        }

        var modifyframe = document.getElementById("FrmModify");
        if (modifyframe != null && url != null) {
            modifyframe.src = url;
        }
    }
    );
</script>
<html>
	<head runat="server">
		<title>采购部系统</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<frameset rows="85,*" border="0" frameborder="0" framespacing="0" name="frmContainer" >
		<frame src="include/page/Header.aspx" name="header" noresize scrolling="no">
		<frameset cols="10,73" border="0" frameborder="0" framespacing="0" id="MainFrame">
			<frame src="include/page/ModelTree.aspx" noresize name="tree" scrolling="yes">
			<frame src="<% = WorkSpaceUrl %>" id="FrmModify" name="modify" scrolling="yes">
		</frameset>
	</frameset>
</html>

