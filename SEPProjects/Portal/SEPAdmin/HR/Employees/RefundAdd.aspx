<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefundAdd.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.RefundAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="/public/js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>
    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">

        function onPageSubmit() { };
        $(document).ready(function () {

            $("#btnAddUsersDialog1").click(function () {
                if (!document.getElementById("btnAddUsersDialog1").__sep_dialog) {
                    document.getElementById("btnAddUsersDialog1").__sep_dialog = $("#userList1").dialog({
                        modal: true, overlay: { opacity: 0.8, background: "black" },
                        height: 500, width: 800,
                        buttons: {
                            "取消": function () { $(this).dialog("close"); },
                            "确定": function () {
                                var hiddenId = $("#btnAddUsersDialog1").attr("server-hidden-id");
                                var selects = document.getElementById("userListFrame1").contentWindow.__sep_dialogReturnValue;
                                if (selects && selects != "") {
                                    var str = selects.substring(0, selects.indexOf(","));
                                    $("#" + hiddenId).val(str);
                                    document.getElementById("<%=labUserName.ClientID%>").innerHTML =
                                     str.split('-')[1];
                                }
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                document.getElementById("btnAddUsersDialog1").__sep_dialog.dialog("open");
            });
        });

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function val() {
            var hid = document.getElementById("<%= hidUserName.ClientID%>").value;
            var time = document.getElementById("<%= txtBeginTime.ClientID%>").value;
            var msg = "";
            var tr = false;
            if (hid == "") {
                msg += "请选择租赁员工,";
                tr = true;
            }
            if (time == "") {
                msg += "请选择租赁开始时间";
                tr = true;
            }
            if (tr) {
                alert(msg);
                return false;
            }

            if (confirm('您确定要启动员工的笔记本租赁？')) {
                return true;
            }
            else {
                return false;
            }
        }
        function saveVal() {
            var hid = document.getElementById("<%= hidUserName.ClientID%>").value;
    var msg = "";
    var tr = false;
    if (hid == "") {
        msg += "请选择租赁员工,";
        tr = true;
    }
    if (tr) {
        alert(msg);
        return false;
    }
    if (confirm('您确定要保存员工的笔记本租赁记录？')) {
        return true;
    }
    else {
        return false;
    }
}
    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">添加笔记本租赁
            </td>
        </tr>
        <tr>

            <td class="oddrow" style="width: 10%">员工姓名:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labUserName" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" value="选择租赁人" id="btnAddUsersDialog1" class="widebuttons"
                    server-hidden-id="<% = hidUserName.ClientID %>" />
                &nbsp;
            <input type="hidden" id="hidUserName" value="" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">租赁开始日期：</td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginTime" onkeyDown="return false; " onclick="setDate(this);"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">资产名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtProductName"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 10%">资产编号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtProductNo"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">日租费:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtCost"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 10%">资产类型:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlType" Width="100px" runat="server" AutoPostBack="false">
                    <asp:ListItem Text="个人笔记本租赁" Value="1" />
                    <asp:ListItem Text="公共物品" Value="2" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSave" runat="server" Text=" 保 存 " CssClass="widebuttons" OnClientClick="return saveVal();" OnClick="btnCommit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnStart" runat="server" Text=" 启动租赁 " CssClass="widebuttons" OnClientClick="return val();" OnClick="btnStart_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 返 回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>

    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList1">
            <iframe src="UserList2.aspx" id="userListFrame1" height="90%" width="100%"></iframe>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
