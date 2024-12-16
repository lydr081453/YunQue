<%@ Page Language="C#" AutoEventWireup="true" Inherits="include_page_ModelTree" EnableViewState="false" Codebehind="ModelTree.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>ModuleBrow</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../public/css/style.css" rel="stylesheet">
    <script type="text/javascript">
        function showNav() {
            if (top.MainFrame.cols == "13px,*") {
                document.getElementById("td2").style.width = "100%";
                document.getElementById("td1").style.width = "5px";
                document.getElementById("Nav").src = "/images/showNavL.png";
                top.MainFrame.cols = "16%,*";
            } else {
                document.getElementById("td2").style.width = "0px";
                document.getElementById("Nav").src = "/images/showNavR.png";
                document.getElementById("td1").style.width = "0px";
                top.MainFrame.cols = "13px,*";
            }
        }
    </script>
    
</head>
<body style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;
    margen: 0px; border: 0px">
    <form runat="server">
    <input id="token" type="hidden" runat="server">
    <input id="RoleUrl" type="hidden" runat="server">
    <table width="100%" border="0" style="table-layout: fixed; height: 100%; border-top: 5px solid #F0931A;
        vertical-align: top; border-bottom: 5px solid #ffb424;" cellpadding="0" cellspacing="0">
        <tr>
            <td id="td1" style="width: 5px; filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#F0931A,endcolorstr=#ffb424,gradientType=0)">
            </td>
            <td id="td2" valign="top">
                <table width="100%" style="height: 100%; vertical-align: top;" border="0" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                        <img src="/images/tableleft.gif" border="0" style="vertical-align: top" />
                                    </td>
                                    <td align="right">
                                        <img src="/images/tableright.gif" border="0" style="vertical-align: top" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%" valign="top">
                            <div style="position: relative; vertical-align: top; padding-left: 20px; overflow:auto;height:100%">
                                <yyc:SmartTreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn"
                                    NodeIndent="10">
                                    <ParentNodeStyle Font-Bold="False" />
                                    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                    <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                                        NodeSpacing="1px" VerticalPadding="2px" />
                                </yyc:SmartTreeView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                        <img src="/images/tablebleft.gif" border="0" style="vertical-align: bottom" />
                                    </td>
                                    <td align="right">
                                        <img src="/images/tablebright.gif" border="0" style="vertical-align: bottom" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 13px; filter: progid:DXImageTransform.Microsoft.gradient(startcolorstr=#F0931A,endcolorstr=#ffb424,gradientType=0)"
                align="right">
                <a href="#" onclick="showNav();">
                    <img id="Nav" src="/images/showNavL.png" border="0" /></a>
            </td>
        </tr>
    </table>
    </form>
</body>
