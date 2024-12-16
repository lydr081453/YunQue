<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferAudit.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferAudit" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
    <script src="/public/js/jquery1.12.js"></script>
    <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.ui.datepicker.cn.js"></script>
    <script language="javascript" type="text/javascript" src="/public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript">

        function setTransfer(obj)
        {
            
            var theTable = obj.parentNode.parentNode.parentNode;
            var i = obj.parentNode.parentNode.rowIndex;
            var j = obj.parentNode.cellIndex;
            var o = window.document.getElementById("test");
            o.value = i;
            if (obj.checked) {
                var o = window.document.getElementById("test");
                var objCheckBox = theTable.rows[o.value].cells[j - 2].firstChild;

                if (window.navigator.userAgent.indexOf("MSIE") >= 1)
                    objCheckBox.innerText = '转组';
                else
                    objCheckBox.textContent = '转组';

                var allInput = theTable.rows[o.value].cells[j - 2].getElementsByTagName("input");
                var objHid = allInput[0];
                objHid.value = '-1';
            }

        }
        // 单条记录的设置交接人
        function SetReceiver(obj) {
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
                    "取消": function () { $(this).dialog("close"); },
                    "确定": function () {
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
                    "取消": function () { $(this).dialog("close"); },
                    "确定": function () {
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
                    if (o.id != "CheckAll" && o.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1 && o.id.indexOf("chkTransfer") == -1) {
                        o.checked = obj.checked;
                    }
                }
            }
        }

        function checkReceiver() {
            var inputArr = document.getElementsByTagName("input");
            var flag = 0;
            if (inputArr != null) {
                for (var i = 0; i < inputArr.length; i++) {
                    var obj = inputArr[i];
                    if (obj != null && obj.type == "hidden" && obj.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1) {
                        //if (obj.value == null || obj.value == "" || obj.value == "0") {
                        //    flag = 1;
                        //}

                    }
                }
            }

            if (flag == 1) {
                alert("请设置单据交接。");
                return false;
            }

            var res = document.getElementById("<%= txtRemark.ClientID %>");
            if (res.value == null || res.value == "") {
                alert("请填写审批意见。");
                return false;
            }
            return true;
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">转出公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransOutCompany"></asp:Label>

            </td>
            <td class="oddrow" style="width: 15%">转出部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransOutDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转出组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:HiddenField runat="server" ID="hidGroupId" />
                <asp:Label runat="server" ID="lblTransOutGroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInCompany"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">转入部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:Label runat="server" ID="lblTransInGroup"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转入职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInPosition"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">转入日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblTransInDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转出员工:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:HiddenField ID="hidTransUserId" runat="server" />
                <asp:Label runat="server" ID="lblTransUser"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">转出日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblTransOutDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">基本工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalaryBase"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">绩效工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalaryPromotion"></asp:Label>

            </td>
        </tr>

        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>

    </table>

    <table width="100%" class="tableForm" runat="server" id ="tabData">
        <tr>
            <td class="heading" colspan="4">业务交接：<font color="red">项目负责人的交接人必须是SAE级(含)以上。 </font>
                <br />
                备注说明：<font color="gray">设置了交接人，单据将留在原组别并由交接人负责跟进；&nbsp;&nbsp;勾选了转组，单据将跟随申请人转到新组别。<br />
                </font>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlTop" runat="server">
                    <input type="button" value="设置交接人" onclick="setSeleteReceiver(this);" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvDetailList" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
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
                        <asp:TemplateField HeaderText="单据编号">
                            <ItemTemplate>
                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                    title="<%# Eval("FormCode") %>">
                                    <%# Eval("FormCode") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                                <img alt="设置交接人" src="../../images/edit.gif" id="imgSetReceiver" onclick="SetReceiver(this);"
                                    style="cursor: pointer;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否转组" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTransfer" runat="server" onclick="setTransfer(this);" />
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
                    <input type="button" value="设置交接人" onclick="setSeleteReceiver(this);" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">日志:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">审批信息:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="50" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnCommit" runat="server" CssClass="widebuttons" OnClick="btnCommit_Click" OnClientClick="return checkReceiver();"
                    ValidationGroup="basicSave" Text=" 确 认 " />
                &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnAudit" runat="server" CssClass="widebuttons" OnClick="btnAudit_Click"
                    ValidationGroup="basicSave" Text=" 确 认 " />
                &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnTransferConfirm" runat="server" CssClass="widebuttons" OnClick="btnTransferConfirm_Click"
                    ValidationGroup="basicSave" Text=" 确 认 " />
                &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnReject" runat="server" CssClass="widebuttons" OnClick="btnReject_Click"
                    ValidationGroup="basicSave" Text=" 驳 回 " />
                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                                Text=" 返 回 " />
            </td>
        </tr>

    </table>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList1">
            <iframe src="UserList3.aspx?depid=<%=oldGroupId %>" id="userListFrame1" height="90%" width="100%"></iframe>
            <input type="hidden" id="test" value="ddddd" class="selected" />
        </div>
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList2">
            <iframe src="UserList3.aspx?depid=<%=oldGroupId %>" id="userListFrame2" height="90%" width="100%"></iframe>
        </div>
    </div>

</asp:Content>
