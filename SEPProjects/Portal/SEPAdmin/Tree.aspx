<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="SEPAdmin.Tree"
    EnableViewState="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <title></title>
     <script type="text/javascript">
        function showdiv(divname) {
            if (document.getElementById(divname).style.display == "") {
                document.getElementById(divname).style.display = "none"
            }
            else {
                var eles = document.getElementsByTagName("div");
                for (var i = 0; i < eles.length; i++) {                    
                }   
                document.getElementById(divname).style.display = ""
            }
        }
    </script>
</head>
<body style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;
    margen: 0px">
<%-- <form runat="server" id="form1">
    <input id="token" type="hidden" runat="server" />
    <input id="RoleUrl" type="hidden" runat="server" />
    <div style="position: static; vertical-align: top; padding-left: 20px; overflow: visible;
        height: 100%; background: #ffffff">
        <yyc:SmartTreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn"
            BackColor="#FFFFFF" NodeIndent="10">
            <ParentNodeStyle Font-Bold="False"  />
            <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
            <SelectedNodeStyle BackColor="#FFFFFF" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
            <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                BackColor="#FFFFFF" NodeSpacing="1px" VerticalPadding="2px" />
        </yyc:SmartTreeView>
    </div>
    </form>--%>
   <form runat="server" id="form1">
    <input id="token" type="hidden" runat="server">
    <input id="RoleUrl" type="hidden" runat="server">
    <div style="position: relative; vertical-align: top; overflow:no; height: 100%;
        background: #ffffff">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
            <tr valign="top" style="height: 28px">
                <td background="../../images/sys_theme12_43.gif" width="223" height="47">
                    
                </td>
            </tr>
            <tr>
                <td bgcolor="#f8fafc" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tbody id="tb" runat="server"></tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
