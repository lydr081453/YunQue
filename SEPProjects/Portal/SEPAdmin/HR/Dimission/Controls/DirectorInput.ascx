<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectorInput.ascx.cs"
    Inherits="SEPAdmin.HR.Dimission.Controls.DirectorInput" %>

<script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

<script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

<script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

<script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

<script language="javascript" type="text/javascript">
    // 单条记录的设置交接人
    function SetReceiver(obj) {
        //alert(obj.parentElement || obj.parentNode);
        var theTable = obj.parentNode.parentNode.parentNode;
        var i = obj.parentNode.parentNode.rowIndex;
        var j = obj.parentNode.cellIndex;
        var o = window.document.getElementById("test");
        o.value = i;
        obj.__sep_dialog = $("#userList1").dialog({
            modal: true,
            overlay: { opacity: 0.5, background: "black" },
            height: 500, width: 800,
            resizeable: false,
            buttons: {
                "取消": function() { $(this).dialog("close"); },
                "确定": function() {
                    var selects = document.getElementById("userListFrame1").contentWindow.__sep_dialogReturnValue;
                    if (selects && selects != "") {
                        var o = window.document.getElementById("test");
                        //var o = $(this).parent.$("#test");
                        var str = selects.substring(0, selects.indexOf(","));

                        var objCheckBox = theTable.rows[o.value].cells[j - 1].firstChild;
                        if (window.navigator.userAgent.indexOf("MSIE") >= 1)
                            objCheckBox.innerText = str.split('-')[1];
                        else
                            objCheckBox.textContent = str.split('-')[1];

                        var allInput = theTable.rows[o.value].cells[j - 1].getElementsByTagName("input");
                        var objHid = allInput[0];
                        objHid.value = str.split('-')[0];
                    }
                    $(this).dialog("close");
                }
            }
        });
        obj.__sep_dialog.dialog("open");
    }

    // 批量的设置交接人
    function setSeleteReceiver(obj) {
        obj.__sep_dialog = $("#userList2").dialog({
            modal: true,
            overlay: { opacity: 0.5, background: "black" },
            height: 500, width: 800,
            resizeable: false,
            buttons: {
                "取消": function() { $(this).dialog("close"); },
                "确定": function() {
                    var selects = document.getElementById("userListFrame2").contentWindow.__sep_dialogReturnValue;
                    if (selects && selects != "") {
                        var theTable = document.getElementById("<%=gvDetailList.ClientID %>");
                        var str = selects.substring(0, selects.indexOf(","));
                        for (i = 1; i < theTable.rows.length; i++) {
                            var allChk = theTable.rows[i].cells[0].getElementsByTagName("input");
                            var objCheckBox = allChk[0];

                            if (objCheckBox.checked != null && objCheckBox.checked == true) {
                                var objLab = theTable.rows[i].cells[6].firstChild;
                                if (window.navigator.userAgent.indexOf("MSIE") >= 1)
                                    objLab.innerText = str.split('-')[1];
                                else
                                    objLab.textContent = str.split('-')[1];

                                var allInput = theTable.rows[i].cells[6].getElementsByTagName("input");
                                var objHid = allInput[0];
                                objHid.value = str.split('-')[0];
                            }
                        }
                    }
                    $(this).dialog("close");
                }
            }
        });
        obj.__sep_dialog.dialog("open");
    }

    // 全选
    function selectAll(obj) {
        var allobj = document.getElementsByTagName("input");
        var obj = document.getElementById("CheckAll");
        for (var i = 0; i < allobj.length; i++) {
            var o = allobj[i];
            if (o.type == "checkbox") {
                if (o.id != "CheckAll" && o.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1) {
                    o.checked = obj.checked;
                }
            }
        }
    }
</script>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="3">
            <asp:Panel ID="pnlTop" runat="server">
                <input type="button" value="设置交接人" onclick="setSeleteReceiver(this);" /></asp:Panel>
        </td>
        <td align="right" class="recordTd">
            记录数:<asp:Label ID="labAllNumT" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gvDetailList" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId"
                Width="100%" OnRowDataBound="gvDetailList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckb" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FormId" Visible="false" />
                    <asp:BoundField DataField="FormCode" HeaderText="单据编号" />
                    <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                    <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                    <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                    <asp:TemplateField HeaderText="交接人编号" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverId" runat="server" />
                            <asp:HiddenField ID="hidReceiverId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交接人">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverName" runat="server" />
                            <asp:HiddenField ID="hidReceiverName" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交接人部门编号" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverDepartmentId" runat="server" />
                            <asp:HiddenField ID="hidReceiverDepartmentId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="设置交接人" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <img alt="设置交接人" src="../../images/edit.gif" runat="server" id="imgSetReceiver" onclick="SetReceiver(this);"
                                style="cursor: pointer;" />
                            <%--<asp:ImageButton ID="ImageButton1" CommandName="Up" runat="server" ImageUrl="../../images/edit.gif"
                                    OnClientClick="return confirm('是否重新启用该用户？');" CommandArgument='<%# Eval("DimissionId") %>' />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
            <input type="hidden" id="hidUserName" value="" runat="server" />
            <asp:Label ID="labUserName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Panel ID="pnlBottom" runat="server">
                <input type="button" value="设置交接人" onclick="setSeleteReceiver(this);" /></asp:Panel>
        </td>
        <td align="right" class="recordTd">
            记录数:<asp:Label ID="labAllNum" runat="server" />
        </td>
    </tr>
</table>
<div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
    <div id="userList1">
        <iframe src="UserList3.aspx" id="userListFrame1" height="90%" width="100%"></iframe>
        <input type="hidden" id="test" value="ddddd" class="selected" />
    </div>
</div>
<div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
    <div id="userList2">
        <iframe src="UserList3.aspx" id="userListFrame2" height="90%" width="100%"></iframe>
    </div>
</div>
