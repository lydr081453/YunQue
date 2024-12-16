<%@ Page Language="C#" AutoEventWireup="true" Inherits="Dialogs_GroupDlg" Codebehind="GroupDlg.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
<script language="javascript" src="/public/js/jquery.blockUI.js"></script>
<script language="javascript" src="/public/js/dialog.js"></script>
<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>
<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>
<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
<script src="/public/js/dimensions.js" type="text/javascript"></script>
<script type="text/javascript">
    $().ready(function() {
    $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        Dialogs_GroupDlg.getalist($("#<%=hidtype.ClientID %>").val(), init1);
        function init1(r) {
            if (r.value != null)
                for (k = 0; k < r.value.length; k++) {
                if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                }
                else {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                }
            }

        }
        if ($("#<%=hidtype.ClientID %>").val() == "") {
            $("#<%=ddltype.ClientID %>").val("-1");
        }
        Dialogs_GroupDlg.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
            if (r.value != null)
                for (j = 0; j < r.value.length; j++) {
                if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                }
                else {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                }
            }
        }
        $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());

        $("#<%=ddltype.ClientID %>").change(function() {
            $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();

            Dialogs_GroupDlg.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
            function pop1(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            $("#<%=ddltype2.ClientID %>").append("<option value='-1'>请选择...</option>");
            $("#<%=hidtype1.ClientID %>").val("-1");
            $("#<%=hidtype2.ClientID %>").val("-1");

        });

        $("#<%=ddltype1.ClientID %>").change(function() {
            $("#<%=ddltype2.ClientID %>").empty();

            Dialogs_GroupDlg.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
            function pop2(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
            $("#<%=hidtype2.ClientID %>").val("-1");

        });

        $("#<%=ddltype2.ClientID %>").change(function() {
            $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            document.getElementById("<% =hidGroupID.ClientID %>").value = document.getElementById("<% =hidtype2.ClientID %>");
            document.getElementById("<% =txtGroup.ClientID %>").value = document.getElementById("<% =ddltype2.ClientID %>").options[document.getElementById("<% =ddltype2.ClientID %>").selectedIndex].innerHTML;
            document.getElementById("divDept").style.display = "none";

        });
    });

    function DeptClick() {
        jQuery.blockUI({
            message: document.getElementById("divDept").innerHTML
        });
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
           <asp:TextBox ID="txtGroup" runat="server"></asp:TextBox><input type="button" id="btnGroup" onclick="return DeptClick();" class="widebuttons"
                value="  变更  " /><input type="hidden" id="hidGroupID" runat="server" /><font color="red">*</font>
                
     <div id="divDept" style=" display:none">

            <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
            &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
            &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" /><font color="red">*</font>
            </div>
            <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
            <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
            <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
    </form>
</body>
</html>
