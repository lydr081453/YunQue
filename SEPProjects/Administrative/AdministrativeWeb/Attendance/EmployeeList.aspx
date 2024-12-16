<%@ Page Language="C#" MasterPageFile="~/Default.master" Inherits="Purchase_Requisition_EmployeeList"
    Title="选择人员" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EmployeeList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>


    <script type="text/javascript">
    $().ready(function() {
        $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        Purchase_Requisition_EmployeeList.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
        Purchase_Requisition_EmployeeList.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
//            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
            if (r.value != null)
                for (j = 0; j < r.value.length; j++) {
                if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                    document.getElementById("<% =hidGroupName.ClientID %>").value = r.value[j][1];
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

            Purchase_Requisition_EmployeeList.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
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

            Purchase_Requisition_EmployeeList.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
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
            var ddl = document.getElementById("<%=ddltype2.ClientID %>");
            var deptname = "";
            if (ddl.options[ddl.selectedIndex].text != "请选择...") {
                deptname = ddl.options[ddl.selectedIndex].text;
                document.getElementById("<% =hidGroupName.ClientID %>").value = deptname;
            }
        });
    });

    function selectAll(obj) {
        var theTable = obj.parentElement.parentElement.parentElement;
        var i;
        var j = obj.parentElement.cellIndex;

        for (i = 0; i < theTable.rows.length; i++) {
            var objCheckBox = theTable.rows[i].cells[j].firstChild;
            if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
        }
    }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td style="width: 15%" class="oddrow">
                检索关键字：
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtName" runat="server" />&nbsp;
            </td>
            <td class="oddrow" style="width: 15%">
                部门：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
                <asp:HiddenField ID="hidGroupName" Value="" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons"
                    Text=" 检索 " />&nbsp;<asp:Button ID="btnClean" runat="server" CssClass="widebuttons"
                        OnClick="btnClean_Click" Text=" 重新搜索 " />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSubMit1" runat="server" Text=" 确定 " CssClass="widebuttons" OnClick="btnSubMit_Click" />
    <input type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" />
    <asp:GridView ID="gv" runat="server" OnDataBinding="gv_DataBinding" OnRowDataBound="gv_RowDataBound"
        AutoGenerateColumns="false" DataKeyNames="SysUserID" Width="100%" CellPadding="4">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkEmp" runat="server" Checked="false" Text='' />
                </ItemTemplate>
                <HeaderTemplate>
                    &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="sysuserid" HeaderText="sysuserid" />
            <asp:BoundField DataField="userid" HeaderText="userid" />
            <asp:BoundField DataField="usercode" HeaderText="员工帐号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="username" HeaderText="员工姓名" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="userid" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField DataField="telephone" HeaderText="电话" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:TemplateField HeaderText="电话" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("telephone") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="positiondescription" HeaderText="职称" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnSubMit" runat="server" Text=" 确定 " CssClass="widebuttons" OnClick="btnSubMit_Click" />
    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();" />
</asp:Content>
