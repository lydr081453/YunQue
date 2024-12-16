<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="SetAuditor.aspx.cs" Inherits="FinanceWeb.Workflows.SetAuditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">

        //预审，总监审批，总监知会，附加审批，总经理审批，总经理知会，附加审批，CEO

        //计算插入行的位置
        function getInsertIndex(type) {
            var table = document.getElementById("<%= tab.ClientID %>");
            var insertIndex = 0;

            var ysIndex = 0;
            var zjspIndex = 0;
            var zjzhIndex = 0;
            var zjfjIndex = 0;
            var zjlspIndex = 0;
            var zjlzhIndex = 0;
            var zjlfjIndex = 0;

            for (i = 1; i < table.rows.length; i++) {
                if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("YS")) {
                    ysIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJSP")) {
                    zjspIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJZH")) {
                    zjzhIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJFJ")) {
                    zjfjIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJLSP")) {
                    zjlspIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJLZH")) {
                    zjlzhIndex = i;
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("ZJLFJ")) {
                    zjlfjIndex = i;
                }
            }
            if (type == "YS") {
                insertIndex = ysIndex;
            } else if (type == "ZJSP") {
                if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; }
            } else if (type == "ZJZH") {
                if (zjzhIndex == 0) { if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; }
            } else if (type == "ZJFJ") {
                if (zjfjIndex == 0) { if (zjzhIndex == 0) { if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; } } else { insertIndex = zjfjIndex; }
            } else if (type == "ZJLSP") {
                if (zjlspIndex == 0) { if (zjfjIndex == 0) { if (zjzhIndex == 0) { if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; } } else { insertIndex = zjfjIndex } } else { insertIndex = zjlspIndex; }
            } else if (type = "ZJLZH") {
                if (zjlzhIndex == 0) { if (zjlspIndex == 0) { if (zjfjIndex == 0) { if (zjzhIndex == 0) { if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; } } else { insertIndex = zjfjIndex } } else { insertIndex = zjlspIndex; } } else { insertIndex = zjlzhIndex; }
            } else if (type == "ZJLFJ") {
                if (zjlfjIndex == 0) { if (zjlzhIndex == 0) { if (zjlspIndex == 0) { if (zjfjIndex == 0) { if (zjzhIndex == 0) { if (zjspIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; } } else { insertIndex = zjfjIndex } } else { insertIndex = zjlspIndex; } } else { insertIndex = zjlzhIndex; } } else { insertIndex = zjlfjIndex; }
            }
            return (insertIndex + 1);
        }

        function addRow1(type, userId, userName, job, operation, changedTrId, oldId) {

            var table = document.getElementById("<%= tab.ClientID %>");

            if (operation == "add") {
                if (!addUser(type, userId)) {//向Hidden中添加用户ID
                    alert('您选择的业务审批人已经存在，不能重复添加！');
                    return;
                }
                tr = table.insertRow(getInsertIndex(type));
                var trId = "";
                tr.className = "td";
                var td1 = document.createElement("td");
                var td2 = document.createElement("td");
                var td3 = document.createElement("td");
                var td4 = document.createElement("td");
                var td5 = document.createElement("td");
                var td6 = document.createElement("td");
                td1.align = "center";
                td2.align = "center";
                td3.align = "center";
                td4.align = "center";
                td5.align = "center";
                td6.align = "center";

                td1.innerHTML = "&nbsp;";
                td2.innerHTML = userName;
                td3.innerHTML = job;
                td4.innerHTML = "&nbsp;";
                trId = tr.id = "tr" + "_" + userId + "_" + type;
                td5.innerHTML = getAuditorTypeName(type);
                if (type == "YS") {
                } else if (type == "ZJSP") {
                    td4.innerHTML = "10万以下";
                } else if (type == "ZJZH") {
                    td4.innerHTML = "10万以下";
                } else if (type == "ZJFJ") {
                } else if (type == "ZJLSP") {
                    td4.innerHTML = "10万以上";
                } else if (type == "ZJLZH") {
                    td4.innerHTML = "10万以上";
                } else if (type == "ZJLFJ") {
                }
                td6.innerHTML = getHtml2(type, trId, userId);
                tr.appendChild(td1);
                tr.appendChild(td2);
                tr.appendChild(td3);
                tr.appendChild(td4);
                tr.appendChild(td5);
                tr.appendChild(td6);
                addUser(type, userId);
            } else if (operation == "change") {
                if (!changedUser(type, userId, oldId)) {
                    alert('您选择的业务审批人已经存在，不能重复添加！');
                    return;
                }
                var tr = null;
                for (i = 0; i < table.rows.length; i++) {
                    if (table.rows[i].id == changedTrId) {
                        tr = table.rows[i];
                    }
                }
                tr.cells[0].innerHTML = "&nbsp;";
                tr.cells[1].innerHTML = userName;
                tr.cells[2].innerHTML = job;
                tr.cells[3].innerHTML = "&nbsp;";

                trId = tr.id = "tr" + "_" + userId + "_" + type;
                tr.cells[4].innerHTML = getAuditorTypeName(type);
                if (type == "YS") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "ZJSP") {
                    tr.cells[3].innerHTML = "10万以下";
                    tr.cells[5].innerHTML = getHtml1(type, trId, userId);
                } else if (type == "ZJZH") {
                    tr.cells[3].innerHTML = "10万以下";
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "ZJFJ") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                } else if (type == "ZJLSP") {
                    tr.cells[3].innerHTML = "10万以上";
                    tr.cells[5].innerHTML = getHtml1(type, trId, userId);
                } else if (type == "ZJLZH") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                    tr.cells[3].innerHTML = "10万以上";
                } else if (type == "ZJLFJ") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                }
            }
            changedSerialNum();
        }

        function getHtml1(type, trId, userId) {
            return "<a href=\"\" onclick=\"openEmployee('" + type + "');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('" + type + "','" + trId + "','" + userId + "');return false;\">更改</a>";
        }

        function getHtml2(type, trId, userId) {
            return "<a href=\"\" onclick=\"openEmployee('" + type + "');return false;\">添加</a>&nbsp;<a href=\"\" onclick=\"openEmployee1('" + type + "','" + trId + "','" + userId + "');return false;\">更改</a>&nbsp;<a href=\"\" onclick=\"removeRow('" + type + "','" + userId + "','" + trId + "');return false;\">删除</a>";
        }

        function getAuditorTypeName(type) {
            var typeName = "";
            if (type == "YS") {
                typeName = "预审";
            } else if (type == "ZJSP") {
                typeName = "总监审批";
            } else if (type == "ZJZH") {
                typeName = "总监知会";
            } else if (type == "ZJFJ") {
                typeName = "总监附加审批";
            } else if (type == "ZJLSP") {
                typeName = "总经理审批";
            } else if (type == "ZJLZH") {
                typeName = "总经理知会";
            } else if (type == "ZJLFJ") {
                typeName = "总经理附加审批";
            }
            return typeName;
        }

        //向hidden中添加sysid
        function addUser(type, userId) {
            var hidValue = "";
            var hidClientId = getHidClientId(type);
            hidValue = document.getElementById(hidClientId).value;
            if (type != "ZJZH" && type != "ZJLZH") {
                hidYS = document.getElementById("<%=hidYS.ClientID %>").value;
                hidZJFJ = document.getElementById("<%=hidZJFJ.ClientID %>").value;
                hidZJSP = document.getElementById("<%=hidZJSP.ClientID %>").value;
                hidZJLSP = document.getElementById("<%=hidZJLSP.ClientID %>").value;
                hidZJLFJ = document.getElementById("<%=hidZJLFJ.ClientID %>").value;
                if (!checkExist(hidValue, userId) && !checkExist(hidYS, userId) && !checkExist(hidZJFJ, userId) && !checkExist(hidZJSP, userId) && !checkExist(hidZJLSP, userId) && !checkExist(hidZJLFJ, userId)) {
                    if (type == "YS")
                        document.getElementById(hidClientId).value = userId + "," + document.getElementById(hidClientId).value;
                    else
                        document.getElementById(hidClientId).value += userId + ",";
                    return true;
                }
            } else {
                if (!checkExist(hidValue, userId)) {
                    if (type == "YS")
                        document.getElementById(hidClientId).value = userId + "," + document.getElementById(hidClientId).value;
                    else
                        document.getElementById(hidClientId).value += userId + ",";
                    return true;
                }
            }
            return false;
        }

        //删除hidden中的sysid
        function removeUser(type, userId) {
            var ids = "";
            var hidClientId = getHidClientId(type);

            ids = document.getElementById(hidClientId).value.split(',');
            document.getElementById(hidClientId).value = "";
            for (i = 0; i < ids.length; i++) {
                if (ids[i] != "") {
                    if (ids[i] != userId) {
                        document.getElementById(hidClientId).value += ids[i] + ",";
                    }
                }
            }
        }

        //变更hidden中的sysid
        function changedUser(type, userId, oldId) {
            var hidClientId = getHidClientId(type);
            var ids = document.getElementById(hidClientId).value.split(',');
            hidValue = document.getElementById(hidClientId).value;
            if (type != "ZJZH" && type != "ZJLZH") {
                hidYS = document.getElementById("<%=hidYS.ClientID %>").value;
                hidZJFJ = document.getElementById("<%=hidZJFJ.ClientID %>").value;
                hidZJSP = document.getElementById("<%=hidZJSP.ClientID %>").value;
                hidZJLSP = document.getElementById("<%=hidZJLSP.ClientID %>").value;
                hidZJLFJ = document.getElementById("<%=hidZJLFJ.ClientID %>").value;
                if (checkExist(hidValue, userId) || checkExist(hidYS, userId) || checkExist(hidZJFJ, userId) || checkExist(hidZJSP, userId) || checkExist(hidZJLSP, userId) || checkExist(hidZJLFJ, userId)) {
                    return false;
                }
            } else {
                if (checkExist(hidValue, userId)) {
                    return false;
                }
            }



            document.getElementById(hidClientId).value = "";
            for (i = 0; i < ids.length; i++) {
                if (ids[i] != "") {
                    if (ids[i] != oldId) {
                        document.getElementById(hidClientId).value += ids[i] + ",";
                    } else {
                        document.getElementById(hidClientId).value += userId + ",";
                    }
                }
            }
            return true;
        }

        //获得操作的hidden的clientID
        function getHidClientId(type) {
            var hidClientId = "";
            if (type == "YS") {
                hidClientId = "<%= hidYS.ClientID%>";
            } else if (type == "ZJSP") {
                hidClientId = "<%= hidZJSP.ClientID %>";
            } else if (type == "ZJZH") {
                hidClientId = "<%= hidZJZH.ClientID %>";
            } else if (type == "ZJFJ") {
                hidClientId = "<%= hidZJFJ.ClientID %>";
            } else if (type == "ZJLSP") {
                hidClientId = "<%= hidZJLSP.ClientID %>";
            } else if (type == "ZJLZH") {
                hidClientId = "<%= hidZJLZH.ClientID %>";
            } else if (type == "ZJLFJ") {
                hidClientId = "<%= hidZJLFJ.ClientID %>";
            }
    return hidClientId;
}

function checkExist(ids, id) {
    if (ids.length > 0) {
        var idsArr = ids.split(",");
        for (i = 0; i < idsArr.length; i++) {
            if (idsArr[i] == id) {
                return true;
            }
        }
    }
    return false;
}

//删除行
function removeRow(type, userId, trId) {
    var targetTable = document.getElementById("<%= tab.ClientID %>")
            for (i = 0; i < targetTable.rows.length; i++) {
                if (targetTable.rows[i].id == trId) {
                    targetTable.deleteRow(i);
                }
            }
            removeUser(type, userId);
            changedSerialNum();
        }

        //变更序号
        function changedSerialNum() {
            var table = document.getElementById("<%= tab.ClientID %>");
            for (i = 1; i < table.rows.length; i++) {
                table.rows[i].cells[0].innerHTML = i;
            }
        }

        function openEmployee(type) {
            var currentUser = '<%= CurrentUser.SysID %>';
            var win = window.open('selectOperationAuditor.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%=Request[ESP.Finance.Utility.RequestName.ReturnID] %>&curU=' + currentUser + "&type=" + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function openEmployee1(type, changedTrId, oldId) {
            var currentUser = '<%= CurrentUser.SysID %>';
            var win = window.open('selectOperationAuditor.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%=Request[ESP.Finance.Utility.RequestName.ReturnID] %>&changedTrId=' + changedTrId + '&oldId=' + oldId + '&type=' + type + '&curU=' + currentUser, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <style type="text/css">
        .Gheading1
        {
            background-image: url("../../images/blue_inside.gif");
            background-repeat: no-repeat;
            height: 25px;
            font-weight: bold;
            font-size: 15px;
        }
    </style>
    <asp:HiddenField ID="hidYS" runat="server" />
    <asp:HiddenField ID="hidZJSP" runat="server" />
    <asp:HiddenField ID="hidZJZH" runat="server" />
    <asp:HiddenField ID="hidZJFJ" runat="server" />
    <asp:HiddenField ID="hidZJLSP" runat="server" />
    <asp:HiddenField ID="hidZJLZH" runat="server" />
    <asp:HiddenField ID="hidZJLFJ" runat="server" />
    <asp:HiddenField ID="hidCEO" runat="server" />
    <asp:HiddenField ID="hidFA" runat="server" />
    <br />
    <table width="100%" class="gridView" id="tab" runat="server">
        <tr class="Gheading1">
            <td align="center" style="width: 5%">
                次序
            </td>
            <td align="center" style="width: 19%">
                审批人
            </td>
            <td align="center" style="width: 19%">
                职位
            </td>
            <td align="center" style="width: 19%">
                审批金额
            </td>
            <td align="center" style="width: 19%">
                审批角色
            </td>
            <td align="center" style="width: 19%">
                操作
            </td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRInputButton runat="server" ID="btnCommit" value=" 提交 " type="button"
                    onclick="showLoading();" CausesValidation="false" class="widebuttons" OnServerClick="btnCommit_Click" />
                &nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
