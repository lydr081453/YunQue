<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ApproveFlow.aspx.cs" Inherits="PurchaseWeb.WorkFlow.ApproveFlow" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

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
            var faIndex = 0;
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
                } else if (table.rows[i].cells[4].innerHTML == getAuditorTypeName("FASP")) {
                    faIndex = i;
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
            else if (type == "FASP") {
            if (faIndex == 0) { if (faIndex == 0) { if (faIndex == 0) { if (faIndex == 0) { if (faIndex == 0) { if (faIndex == 0) { insertIndex = ysIndex; } else { insertIndex = zjspIndex; } } else { insertIndex = zjzhIndex; } } else { insertIndex = zjfjIndex } } else { insertIndex = zjlspIndex; } } else { insertIndex = zjlzhIndex; } } else { insertIndex = zjlfjIndex; }
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
            } else if (type == "FASP") {
            td4.innerHTML = "FA";
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
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "ZJZH") {
                    tr.cells[3].innerHTML = "10万以下";
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "ZJFJ") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                } else if (type == "ZJLSP") {
                    tr.cells[3].innerHTML = "10万以上";
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "ZJLZH") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                    tr.cells[3].innerHTML = "10万以上";
                } else if (type == "ZJLFJ") {
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);

                } else if (type == "CEO") {
                    tr.cells[3].innerHTML = "10万以上";
                    tr.cells[5].innerHTML = getHtml2(type, trId, userId);
                } else if (type == "FASP") {
                tr.cells[3].innerHTML = "FA";
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

        function getHtml3(type, trId, userId) {
            return "<a href=\"\" onclick=\"openEmployee1('" + type + "','" + trId + "','" + userId + "');return false;\">更改</a>";
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
            } else if (type == "CEO") {
                typeName = "CEO审批";
            } else if (type == "FASP") {
            typeName = "FA审批";
            }
            return typeName;
        }

        //向hidden中添加sysid
        function addUser(type, userId) {
            var hidValue = "";
            var hidClientId = getHidClientId(type);
            hidValue = document.getElementById(hidClientId).value;
            if (type != "ZJZH" && type != "ZJLZH") {
                hidYS += document.getElementById("<%=hidYS.ClientID %>").value;
                hidZJFJ = document.getElementById("<%=hidZJFJ.ClientID %>").value;
                hidZJSP = document.getElementById("<%=hidZJSP.ClientID %>").value;
                hidZJLSP = document.getElementById("<%=hidZJLSP.ClientID %>").value;
                hidZJLFJ = document.getElementById("<%=hidZJLFJ.ClientID %>").value;
                hidFA = document.getElementById("<%=hidFA.ClientID %>").value;
                if (!checkExist(hidValue, userId) && !checkExist(hidYS, userId) && !checkExist(hidZJFJ, userId) && !checkExist(hidZJSP, userId) && !checkExist(hidZJLSP, userId) && !checkExist(hidZJLFJ, userId) && !checkExist(hidFA, userId)) {
                    document.getElementById(hidClientId).value += userId + ",";
                    return true;
                }
            } else {
                if (!checkExist(hidValue, userId)) {
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
                hidFA = document.getElementById("<%=hidFA.ClientID %>").value;
                if (checkExist(hidValue, userId) || checkExist(hidYS, userId) || checkExist(hidZJFJ, userId) || checkExist(hidZJSP, userId) || checkExist(hidZJLSP, userId) || checkExist(hidZJLFJ, userId) || checkExist(hidFA, userId)) {
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
            } else if (type == "CEO") {
                hidClientId = "<%= hidCEO.ClientID %>";
            } else if (type == "FASP") {
            hidClientId = "<%= hidFA.ClientID %>";
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
            var currentUser = '<%= CurrentUserID %>';
            var win = window.open('/Purchase/Requisition/selectOperationAuditor.aspx?<%= ESP.Purchase.Common.RequestName.GeneralID %>=<%= CurrentOrderID%>&curU=' + currentUser + "&type=" + type + '&deptid=<%=DeptID %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function openEmployee1(type, changedTrId, oldId) {
            var currentUser = '<%= CurrentUserID %>';
            var win = window.open('/Purchase/Requisition/selectOperationAuditor.aspx?<%= ESP.Purchase.Common.RequestName.GeneralID %>=<%= CurrentOrderID%>& %>&changedTrId=' + changedTrId + '&oldId=' + oldId + '&type=' + type + '&curU=' + currentUser+'&deptid=<%=DeptID %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function btnCommit_onclick() {

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
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">
                单据类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="请选择.." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="采购申请单" Value="0"></asp:ListItem>
                    <asp:ListItem Text="付款申请单" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="请选择单据类型"
                    ControlToValidate="ddlType" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                流水号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtID"
                    runat="server" ErrorMessage="<br />流水号"></asp:RequiredFieldValidator>&nbsp;
              
            </td>
        </tr>
        <tr>
        <td colspan="4" class="oddrow-l">
          <asp:Button ID="btnSearch" runat="server" CssClass="widebuttons" Text=" 查询 " CausesValidation="true"
                    OnClick="btnSearch_Click" />
        </td>
        </tr>
    </table>
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
                &nbsp;
                <input id="btnCommit" runat="server" causesvalidation="false" class="widebuttons"
                    onclick="this.disabled=true;" type="button" onserverclick="btnCommit_Click"
                    value=" 提交 " />
            </td>
        </tr>
    </table>
</asp:Content>
