<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentTree.aspx.cs"
    Inherits="SEPAdmin.Management.ModelManagement.DepartmentTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.treeview.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#tree").treeview();
            $("input.ckbDepartment").click(function() {
                if (!window.__sep_dialogReturnValue)
                    window.__sep_dialogReturnValue = ",";
                var rv = window.__sep_dialogReturnValue;
                var valStr = $(this).val() + ",";
                var searchStr = "," + valStr;
                if ($(this).attr("checked")) {
                    if (rv.indexOf(searchStr) < 0) {
                        rv += valStr;
                    }
                }
                else {
                    var index = rv.indexOf(searchStr);
                    if (index >= 0) {
                        rv = rv.substr(0, index) + rv.substr(index + valStr.length);
                    }
                }
                window.__sep_dialogReturnValue = rv;
            });
        });
    </script>

</head>
<body>
    <ul id="tree" class="filetree">
        <asp:PlaceHolder runat="server" ID="phTree"></asp:PlaceHolder>
    </ul>
</body>
</html>
