<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalList.aspx.cs" Inherits="SupplierWeb.ShowDlg.PrincipalList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
	<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        function page_init() {
        var selects = document.getElementsByTagName("input");
        for (var i = 0; i < selects.length; i++) {
            if (selects[i].name == "principallist") {
                if (document.getElementById("hidPrincipalIdP").value == selects[i].id) {
                    selects[i].checked=true;
                }
            }
        }
    }
        
        function setvalue(value1,value2) {
            if (null != document.getElementById("hidPrincipalIdP")) {
                document.getElementById("hidPrincipalIdP").value = value1;
                document.getElementById("txtPrincipal").value = value2;
            }
            hiddiv();
        }
        function hiddiv() {
            var height = 500;
            $("#floatBoxBg").animate({ opacity: "0" }, 1, 'linear', function() { $(this).hide(); });
            $("#floatBox").animate({ top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px" }, 1, 'linear', function() { $(this).hide(); });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="floatDiv2"  runat="server">
            <asp:DataList ID="dlList" runat="server"  OnItemDataBound="dlList_ItemDataBound" RepeatColumns="2" RepeatDirection="Horizontal"   Width="500px" Height="500px" ItemStyle-Height="10px">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="labContent2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
            <asp:HiddenField ID="hidPrincipalId" runat="server" />
    </div>
    </form>
</body>
</html>