<%@ Page Language="C#" AutoEventWireup="true" Inherits="include_page_ModelTree" EnableViewState="false" Codebehind="ModelTree.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>ModuleBrow</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../public/css/css.css" rel="stylesheet">

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
    background-color: #dedec8; margen: 0px">
    <form runat="server" id="form1">
    <input id="token" type="hidden" runat="server">
    <input id="RoleUrl" type="hidden" runat="server">
    <div style="position: relative; vertical-align: top; overflow: no; height: 100%;
        background: #ffffff">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#f8fafc" height="100%">
            <tr valign="top" style="height: 28px">
                <td background="../../images/blue_inside (5).gif">
                    <img src="../../images/blue_inside (5).gif" width="1" height="28px" />
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
