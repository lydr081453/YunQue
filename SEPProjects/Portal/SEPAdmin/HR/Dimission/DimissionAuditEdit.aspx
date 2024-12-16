<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionAuditEdit.aspx.cs"
    Inherits="DimissionAuditEdit" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
    <script src="/public/js/jquery1.12.js"></script>
    <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.ui.datepicker.cn.js"></script>
    <script language="javascript" type="text/javascript" src="/public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript">

        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#ctl00_ContentPlaceHolder1_txtdimissionDate").datepicker();
            $("#ctl00_ContentPlaceHolder1_txtEmailSaveLastDay").datepicker();
            $("#ctl00_ContentPlaceHolder1_txtTurnAroundDate").datepicker();
            $("#ctl00_ContentPlaceHolder1_txtdimissionDate2").datepicker();

        });


        // ������¼�����ý�����
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
                    "ȡ��": function () { $(this).dialog("close"); },
                    "ȷ��": function () {
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

        // ���������ý�����
        function setSeleteReceiver(obj) {
            obj.__sep_dialog = $("#userList2").dialog({
                modal: true,
                overlay: { opacity: 0.5, background: "black" },
                height: 500, width: 800,
                resizeable: false,
                buttons: {
                    "ȡ��": function () { $(this).dialog("close"); },
                    "ȷ��": function () {
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

        // ȫѡ
        function selectAll(obj) {
            /*
            var theTable = obj.parentNode.parentNode.parentNode;
            var i;
            var j = obj.parentNode.cellIndex;
            for (i = 1; i < theTable.rows.length; i++) {
            alert(theTable.rows[i].cells[j].firstChild);
            var objCheckBox = theTable.rows[i].cells[j].firstChild;
                
            if (objCheckBox != null) {
            objCheckBox.setAttribute("checked", obj.checked);
            //objCheckBox.checked = obj.checked;
            }
            }
            */

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

        function checkReceiver() {
            var inputArr = document.getElementsByTagName("input");
            var flag = 0;
            if (inputArr != null) {
                for (var i = 0; i < inputArr.length; i++) {
                    var obj = inputArr[i];
                    if (obj != null && obj.type == "hidden" && obj.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1) {
                        if (obj.value == null || obj.value == "" || obj.value == "0") {
                            flag = 1;
                        }
                    }
                }
            }

            var dimissionDate = document.getElementById("<%=txtdimissionDate.ClientID %>");
            if (dimissionDate.value == null || dimissionDate.value == "") {
                alert("��������ְ���ڡ�");
                return false;
            }

            if (flag == 1) {
                alert("��Ϊ���е������ý����ˡ�");
                return false;
            }

            var res = document.getElementById("<%= txtRes.ClientID %>");
            if (res.value == null || res.value == "") {
                alert("����д���������");
                return false;
            }
            showLoading();
            return true;
        }

        function changeChecked() {
            var cmchk = document.getElementById("<%= chkComplementaryMedical.ClientID %>");

            var drpCY = document.getElementById("<%=drpAddedMedicalInsY.ClientID %>");
            var drpCM = document.getElementById("<%=drpAddedMedicalInsM.ClientID %>");
            if (cmchk.checked) {
                drpCY.disabled = false;
                drpCM.disabled = false;
            }
            else {
                drpCY.disabled = true;
                drpCM.disabled = true;
            }
        }

        var trindex = 0;
        function addIndemnity(tableid) {
            var obj = document.getElementById(tableid);
            var newtr = obj.insertRow();
            trindex = trindex + 1;

            var newtd1 = newtr.insertCell();
            newtd1.setAttribute("bgColor", '#ffffff');
            var newSelecteHearder = "<input type='text' name='txtIndemnityItem' id='txtIndemnityItem" + trindex + "' style='width: 100%' />"
            newtd1.insertAdjacentHTML("afterBegin", newSelecteHearder);

            var newtd2 = newtr.insertCell();
            newtd2.setAttribute("bgColor", '#ffffff');
            var newSelecteHearder = "<input type='text' name='txtIndemnityAmount' id='txtIndemnityAmount" + trindex + "' style='width: 100%' />";
            newtd2.insertAdjacentHTML("afterBegin", newSelecteHearder);

            var newtd3 = newtr.insertCell();
            newtd3.setAttribute("bgColor", '#ffffff');
            var newSelecteHearder = "<input type='text' name='txtIndemnityDesc' id='txtIndemnityDesc" + trindex + "' style='width: 100%' />";
            newtd3.insertAdjacentHTML("afterBegin", newSelecteHearder);

            var newtd4 = newtr.insertCell();
            newtd4.setAttribute("bgColor", '#ffffff');
            newtd4.setAttribute("align", "center");
            var newSelecteHearder = "<input name='btnDelete' type='button' onclick='deleterow(" + (obj.rows.length - 1) + ", " + tableid + ");' value='ɾ��' />";
            newtd4.insertAdjacentHTML("afterBegin", newSelecteHearder);
        }

        function deleterow(rowNO, tableid) {
            var uploadFileTable = document.getElementById(tableid);
            if (uploadFileTable.rows.length == 1) {
                alert('��������ɾ���� ');
                return false;
            }
            if (confirm("����ɾ����ǰ�������ݣ��Ƿ����?")) {
                uploadFileTable.deleteRow(rowNO);
            }
            indexUploadFile(tableid);
            return true;
        }
        function indexUploadFile(tableid) {
            var uploadFileTable = document.getElementById(tableid);
            var rows = uploadFileTable.rows;
            if (rows != null && rows.length > 1) {
                for (var i = 1; i < rows.length; i++) {
                    rows[i].cells[3].innerHTML = "<input name='btnDelete' type='button' onclick='deleterow(" + i + "," + tableid + ");' value='ɾ��' />";
                }
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">��ְ���뵥
                <asp:HiddenField ID="hidDimissionFormID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">�û����:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCode" runat="server" />
            </td>
            <td class="oddrow">���ڲ���:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdepartmentName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hiddepartmentId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">����:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">���ڹ�˾:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtcompanyName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidcompanyId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">ְ��:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPosition" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">�����Ŷ�:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtgroupName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidgroupId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">�ֻ�:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtMobilePhone" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMobilePhone"
                    ErrorMessage="����д�����ֻ����롣" ForeColor="Red" />
            </td>
            <td class="oddrow">�����ص�:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtWorkCity" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">˽������:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEmail" runat="server" />
            </td>
            <td class="oddrow">��ְ����:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtjoinJobDate" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:Panel ID="pnlDimissionDate1" runat="server">
                    ������ְ����:
                </asp:Panel>
                <asp:Panel ID="pnlDimissionDate2" runat="server">
                    �����ְ����:
                </asp:Panel>
            </td>
            <td class="oddrow-l">
                <div>
                    <asp:TextBox ID="txtdimissionDate2" onkeyDown="return false; "
                        runat="server" />
                </div>
            </td>
            <td class="oddrow" style="width: 20%">&nbsp;
            </td>
            <td class="oddrow-l" style="width: 30%">&nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">��ְԭ��:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtdimissionCause" runat="server" TextMode="MultiLine" Width="90%"
                    Height="80px" ReadOnly="true" />
            </td>
        </tr>
        <!-- ҵ������ -->
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlDirectorAudit" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="heading" colspan="4">��ҵ�񽻽� <font color="red">��Ŀ�����˵Ľ����˱�����SAE��(��)���ϡ�</font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="pnlTop" runat="server">
                                    <input type="button" value="���ý�����" onclick="setSeleteReceiver(this);" />
                                </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labAllNumT" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="gvDetailList" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
                                    Width="100%" OnRowDataBound="gvDetailList_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ѡ��" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckb" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />ȫѡ
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormId" Visible="false" />
                                        <asp:TemplateField HeaderText="���ݱ��">
                                            <ItemTemplate>
                                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                                    title="<%# Eval("FormCode") %>">
                                                    <%# Eval("FormCode") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormType" HeaderText="��������" />
                                        <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" />
                                        <asp:BoundField DataField="Description" HeaderText="��Ŀ������" />
                                        <asp:BoundField DataField="TotalPrice" HeaderText="�ܽ��" />
                                        <asp:TemplateField HeaderText="�����˱��" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverName" runat="server" />
                                                <asp:HiddenField ID="hidReceiverName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�����˲��ű��" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverDepartmentId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverDepartmentId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="���ý�����" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img alt="���ý�����" src="../../images/edit.gif" id="imgSetReceiver" onclick="SetReceiver(this);"
                                                    style="cursor: pointer;" />
                                                <%--<asp:ImageButton ID="ImageButton1" CommandName="Up" runat="server" ImageUrl="../../images/edit.gif"
                                    OnClientClick="return confirm('�Ƿ��������ø��û���');" CommandArgument='<%# Eval("DimissionId") %>' />--%>
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
                                    <input type="button" value="���ý�����" onclick="setSeleteReceiver(this);" />
                                </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labAllNum" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlManageAudit" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="heading" colspan="2">��ҵ�񽻽�
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labManNumberT" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvDetailView" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId"
                                    Width="100%" OnRowDataBound="gvDetailView_RowDataBound">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="ѡ��" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckb" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />ȫѡ
                                            </HeaderTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="FormId" Visible="false" />
                                        <asp:TemplateField HeaderText="���ݱ��">
                                            <ItemTemplate>
                                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                                    title="<%# Eval("FormCode") %>">
                                                    <%# Eval("FormCode") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormType" HeaderText="��������" />
                                        <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" />
                                        <asp:BoundField DataField="Description" HeaderText="��Ŀ������" />
                                        <asp:BoundField DataField="TotalPrice" HeaderText="�ܽ��" />
                                        <asp:TemplateField HeaderText="�����˱��" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverName" runat="server" />
                                                <asp:HiddenField ID="hidReceiverName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="�����˲��ű��" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverDepartmentId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverDepartmentId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����״̬">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverStatus" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="���ý�����" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img alt="���ý�����" src="../../images/edit.gif" runat="server" id="imgSetReceiver" onclick="SetReceiver(this);"
                                                    style="cursor: pointer;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerSettings Visible="false" />
                                </asp:GridView>
                                <input type="hidden" id="Hidden1" value="" runat="server" />
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labManNumberB" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlOOP" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="heading" colspan="2">��������
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labOOP1T" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvOOP1" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="FormId" Visible="false" />
                                        <asp:TemplateField HeaderText="���ݱ��">
                                            <ItemTemplate>
                                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                                    title="<%# Eval("FormCode") %>">
                                                    <%# Eval("FormCode") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormType" HeaderText="��������" />
                                        <asp:BoundField DataField="UserName" HeaderText="������" />
                                        <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" />
                                        <asp:BoundField DataField="Description" HeaderText="��Ŀ������" />
                                        <asp:BoundField DataField="TotalPrice" HeaderText="�ܽ��" />
                                    </Columns>
                                    <PagerSettings Visible="false" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="right" class="recordTd">��¼��:<asp:Label ID="labOOP1B" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAsset" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="heading">�̶��ʲ�����
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvAsset" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <%#(Container.DataItemIndex+1).ToString()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SerialCode" HeaderText="�ʲ����" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="AssetName" HeaderText="�ʲ�����" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Brand" HeaderText="Ʒ��" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Model" HeaderText="�ͺ�" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="ReceiveDate" HeaderText="��������" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                    </Columns>
                                    <PagerSettings Visible="false" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPerformance" runat="server" Visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="oddrow">��Ч:
                            </td>
                            <td class="oddrow-l">�Ƿ�ʵ�ʹ����ս���&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="radIsNormalPerTure" runat="server" Text="��" GroupName="IsNormalPer"
                                    Checked="true" />
                                <asp:RadioButton ID="radIsNormalPerFalse" runat="server" Text="��" GroupName="IsNormalPer" />
                                ��ְ���µļ�Ч���Ϊ��<asp:TextBox ID="txtSumPerformance" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <span style="color: Red; font-weight: bold;">��Ҫ����ȷ����˾������Ϣ���ϣ������Ӱ棩�Ѿ��������</span>
            </td>
        </tr>
        <!-- �Ŷ��������� -->
        <asp:Panel ID="pnlGroupAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">���Ŷ�����
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <font style="font-weight: bolder">&nbsp;&nbsp;����</font>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="oddrow-l">&nbsp;&nbsp;1 / ���¿�����Ϣ��
                </td>
                <td align="right">��������״̬��<asp:Label ID="labAttPreAuditStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                        <tr>
                            <td height="25" align="center" bgcolor="#ececec">�ٵ�
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">������OT
                            </td>
                            <td align="center" bgcolor="#ececec">�ڼ���OT
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����ۼƲ���
                            </td>
                            <td align="center" bgcolor="#ececec">�¼�
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">ɥ��
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">�ʼǱ�����
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLate" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAbsent" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labHolidayOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEvection" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEgress" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labSickLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAddupSickLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAffiairLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAnnualLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMarriageLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labFuneralLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMaternityLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPrenatalCheck" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPCRefund" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="oddrow-l">&nbsp;&nbsp;2 / ���¿�����Ϣ��
                </td>
                <td align="right">��������״̬��<asp:Label ID="labAttCurAuditStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                        <tr>
                            <td height="25" align="center" bgcolor="#ececec">�ٵ�
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">������OT
                            </td>
                            <td align="center" bgcolor="#ececec">�ڼ���OT
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����ۼƲ���
                            </td>
                            <td align="center" bgcolor="#ececec">�¼�
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">���
                            </td>
                            <td align="center" bgcolor="#ececec">ɥ��
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">����
                            </td>
                            <td align="center" bgcolor="#ececec">�ʼǱ�����
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLateCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLeaveEarlyCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAbsentCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labOverTimeCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labHolidayOverTimeCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEvectionCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEgressCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labSickLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAddupSickLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAffiairLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAnnualLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMarriageLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labFuneralLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMaternityLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPrenatalCheckCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPCRefundCur" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / �����&nbsp;<font style="font-weight: bolder"><asp:Label ID="labAnnual"
                    runat="server" /></font>&nbsp;�죻 Ԥ֧<asp:Label ID="labOverDraft"
                        runat="server" />�����
                </td>
            </tr>
            <tr>
                <td colspan="4">��ְԭ��:
                    <asp:DropDownList runat="server" ID="ddlReason">
                        <asp:ListItem Text="��ѡ��..." Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="����ԭ��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��ͬ���ڣ��׷�������" Value="2"></asp:ListItem>
                        <asp:ListItem Text="��ͬ���ڣ��ҷ�������" Value="3"></asp:ListItem>
                        <asp:ListItem Text="��˫��Լ����Ӧ���ҷ���ɵĹ����ﵽԤ���ı�׼����ֹ�Ͷ���ͬ" Value="4"></asp:ListItem>
                        <asp:ListItem Text="��˾����" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;��ע:<asp:TextBox runat="server" ID="txtReason"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <font style="font-weight: bolder">&nbsp;&nbsp;�������̶��ʲ���</font>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;�����������԰칫��Ʒ�嵥����λ��������ǩ���ƹ�Կ�ף�<br />
                    <asp:TextBox ID="txtOfficeSupplies" runat="server" TextMode="MultiLine" Width="90%"
                        Height="80px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="����д�칫��Ʒ�黹˵����"
                        ControlToValidate="txtOfficeSupplies" />
                </td>
            </tr>
            <asp:Panel ID="pnlGroupAddIndemnityAudit" runat="server" Visible="false">
                <tr>
                    <td colspan="4" class="oddrow-l">�����⳥����������ӣ���д��ϸ˵��&nbsp;&nbsp;<input type="button" id="btnAddIndemnityId" value="���"
                        class="widebuttons" onclick="addIndemnity('tabIndemnity');" /><br />
                        <table id="tabIndemnity" width="60%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                            <tr>
                                <td height="25" align="center" bgcolor="#ececec" style="width: 30%">�������
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 30%">���
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 30%">��ע
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 10%">����
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
        </asp:Panel>
        <!-- ����������Դ���� -->
        <asp:Panel ID="pnlHRAudit1" runat="server">
            <tr>
                <td class="heading" colspan="4">�ۼ���������Դ
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / ����ռ�������
                </td>
            </tr>
            <tr>
                <td class="oddrow">�籣�ɷ���:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpSocialInsY" runat="server">
                    </asp:DropDownList>
                    ��
                    <asp:DropDownList ID="drpSocialInsM" runat="server">
                    </asp:DropDownList>
                    ��
                </td>
            </tr>
            <tr>
                <td class="oddrow">ҽ�ƽɷ���:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpMedicalInsY" runat="server">
                    </asp:DropDownList>
                    ��
                    <asp:DropDownList ID="drpMedicalInsM" runat="server">
                    </asp:DropDownList>
                    ��
                </td>
            </tr>
            <tr>
                <td class="oddrow">ס��������ɷ���:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpCapitalReserveY" runat="server">
                    </asp:DropDownList>
                    ��
                    <asp:DropDownList ID="drpCapitalReserveM" runat="server">
                    </asp:DropDownList>
                    ��
                </td>
            </tr>
            <tr>
                <td class="oddrow">����ҽ�ƽɷ���:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:CheckBox ID="chkComplementaryMedical" onclick="changeChecked();" runat="server" />
                    <asp:DropDownList ID="drpAddedMedicalInsY" runat="server">
                    </asp:DropDownList>
                    ��
                    <asp:DropDownList ID="drpAddedMedicalInsM" runat="server">
                    </asp:DropDownList>
                    ��
                </td>
            </tr>
            <tr>
                <td class="oddrow">��ע:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:TextBox ID="txtHRRemark" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
        </asp:Panel>
        <!-- IT������ -->
        <asp:Panel ID="pnlITAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">��IT
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / ���������ʻ���<asp:TextBox ID="txtCompanyEmail" runat="server" Width="220" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete"
                    runat="server" ID="radEmailIsDeleteTrue" Text="ɾ��" Checked="true" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete"
                        runat="server" ID="radEmailIsDeleteFalse" Text="��������" />
                    &nbsp;&nbsp;<asp:TextBox ID="txtEmailSaveLastDay" onkeyDown="return false; "
                        runat="server" Width="220" />
                </td>
            </tr>
            <%--            <tr>
                <td colspan="4" class="oddrow-l">
                    &nbsp;&nbsp;2 / OAϵͳ�ʻ���
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete"
                        runat="server" ID="radAccountIsDeleteTrue" Text="ɾ��" Checked="true" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete"
                        runat="server" ID="radAccountIsDeleteFalse" Text="��������" />
                    &nbsp;&nbsp;<asp:TextBox ID="txtAccountSaveLastDay" onkeyDown="return false; " onclick="setDate(this);"
                        runat="server" Width="220" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;2 / OA�豸/�Թ����Դ����Ա�ţ�<asp:TextBox ID="txtOwnPCCode" runat="server" Width="220" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;��˾���Ա�ţ�<asp:TextBox ID="txtPCCode" runat="server" Width="220" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;��˾����״���������ߣ���<br />
                    <asp:TextBox ID="txtPCUsedDes" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / ����˵����
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <asp:TextBox ID="txtITOther" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;
                </td>
            </tr>
            <asp:Panel ID="pnlITAddIndemnityAudit" runat="server" Visible="false">
                <tr>
                    <td colspan="4" class="oddrow-l">�����⳥����������ӣ���д��ϸ˵��&nbsp;&nbsp;<input type="button" id="Button3" value="���" class="widebuttons"
                        onclick="addIndemnity('tabIndemnity2');" /><br />
                        <table id="tabIndemnity2" width="60%" border="0" cellpadding="2" cellspacing="1"
                            bgcolor="#c1c1c1">
                            <tr>
                                <td height="25" align="center" bgcolor="#ececec" style="width: 30%">�������
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 30%">���
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 30%">��ע
                                </td>
                                <td align="center" bgcolor="#ececec" style="width: 10%">����
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
        </asp:Panel>
        <!-- ���������ܼ����� -->
        <asp:Panel ID="pnlHRDirectorAudit" runat="server" Visible="false">
            <tr>
                <td class="heading" colspan="4">�⳥�����б�
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <asp:GridView ID="gvIndemnity" runat="server" AutoGenerateColumns="False" DataKeyNames="DimissionIndemnityId"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="DimissionIndemnityId" Visible="false" />
                            <asp:BoundField DataField="IndemnityItem" HeaderText="�������" />
                            <asp:BoundField DataField="IndemnityAmount" HeaderText="���" />
                            <asp:BoundField DataField="IndemnityDesc" HeaderText="��ע" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
            <asp:Panel ID="pnlHRDirectorAuditAmount" runat="server" Visible="false">
                <tr>
                    <td class="oddrow">�⳥�ܽ��:
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:TextBox ID="txtTotalIndemnityAmount" runat="server"></asp:TextBox><font color="red">����ȷ���⳥��</font>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditAmount" runat="server" Visible="false">
                <tr>
                    <td class="oddrow-l" colspan="4">���������ܼ�����ͨ����ȷ�ϵ��⳥���Ϊ��<asp:TextBox ReadOnly="true" ID="txtTotalIndemnityAmountFinance"
                        runat="server"></asp:TextBox><br />
                        <asp:Label ID="labMobileInfo" runat="server"></asp:Label>
                        <asp:Label ID="labAppleBook" runat="server"></asp:Label>
                    </td>
                </tr>
            </asp:Panel>
        </asp:Panel>
        <!-- �������� -->
        <asp:Panel ID="pnlFinanceAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">�ݲ���
                </td>
            </tr>
            <asp:Panel ID="pnlFinanceAuditLevel1" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / ���/��Ʊ/������
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:Label ID="labLoan" runat="server" />
                        <asp:Panel ID="pnlLoan" runat="server">
                            <asp:Label ID="labBranchInfo" runat="server" />
                            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="30%">X��<asp:TextBox ID="txtLoanX" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>S��<asp:TextBox ID="txtLoanS" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>K��<asp:TextBox ID="txtLoanK" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">D��<asp:TextBox ID="txtLoanD" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>C��<asp:TextBox ID="txtLoanC" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>Z��<asp:TextBox ID="txtLoanZ" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </asp:Panel>

            <asp:Panel ID="pnlFinanceAuditLevel3" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;2 / ���񿨣�
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtBusinessCard" runat="server" TextMode="MultiLine" Width="90%"
                            Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel4" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / ���ʽ��������
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:RadioButton ID="radSalary1" runat="server" Text="�ۺ�����������ʽ�������" GroupName="SalaryRadioButton" /><br />
                        <asp:RadioButton ID="radSalary2" runat="server" GroupName="SalaryRadioButton" />�貹��<asp:TextBox
                            ID="txtSalary" runat="server" />��ѡ�������ֹ�˾��
                        <asp:DropDownList ID="drpBranchList" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel6" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;4 / ���ʽ��������տ������
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtSalary2" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel5" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;5 / ����˵����
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
        </asp:Panel>
        <!-- ������������ -->
        <asp:Panel runat="server" ID="pnlADAudit">
            <tr>
                <td class="heading" colspan="4">�޼�������
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / �ſ����ţ�<asp:TextBox ID="txtDoorCard" runat="server" />
                </td>
            </tr>
            <%--<tr>
                <td colspan="4" class="oddrow-l">
                    &nbsp;&nbsp;2 / ͼ�����
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <asp:TextBox ID="txtLibraryManage" runat="server" TextMode="MultiLine" Width="90%"
                        Height="80px" />
                </td>
            </tr>--%>
        </asp:Panel>
        <!-- ����������Դ�ڶ������� -->
        <asp:Panel runat="server" ID="pnlHRAudit2">
            <tr>
                <td class="heading" colspan="4">�߼���������Դ
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / ���µ�����
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                    runat="server" ID="radIsHaveArchivesFalse" Text="��" Checked="true" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                        runat="server" ID="radIsHaveArchivesTrue" Text="��" /><br />
                    &nbsp;&nbsp;����<asp:TextBox ID="txtTurnAroundDate" onkeyDown="return false; "
                        runat="server" />ǰ������ϵ�ת������
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">��ѡ�������ֹ�˾��
                    <asp:DropDownList ID="drpBranchList2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">��ְ֤�����Ƿ���ʾְλ��Ϣ��
                    <asp:RadioButton runat="server" ID="radIsShowPositionTrue" GroupName="IsShowPosition"
                        Text="��" Checked="true" />
                    <asp:RadioButton runat="server" ID="radIsShowPositionFalse" GroupName="IsShowPosition"
                        Text="��" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">������־:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labAuditLog" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">�������:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRes" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="����д���������"
                    ControlToValidate="txtRes" />
                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ToolTip="��ʱ����Message,�Ժ��ٲ���"><font style=" text-decoration:underline; color:Blue; font-weight:bold">����</font></asp:LinkButton>
            </td>
        </tr>
        <asp:Panel runat="server" ID="pnlDirLastDay">
            <tr>
                <td class="oddrow">ȷ����ְ����:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:TextBox ID="txtdimissionDate" onkeyDown="return false; "
                        runat="server" />&nbsp;<font style="color: red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdimissionDate"
                        ErrorMessage="����д��������ڡ�" ForeColor="Red" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Panel ID="pnlButtonDirAudit" runat="server">
                    <asp:Button ID="btnPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons" OnClick="btnPass_Click"
                        OnClientClick="return checkReceiver();" />&nbsp;
                    <asp:Button ID="btnOverrule" runat="server" Text=" �������� "  OnClientClick="showLoading();"  CssClass="widebuttons" CausesValidation="false"
                        OnClick="btnOverrule_Click" />&nbsp;
                    <input type="button" id="Button2" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonManAudit" runat="server">
                    <asp:Button ID="btnManAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading(); " 
                        OnClick="btnManAuditPass_Click" />&nbsp;
                    <asp:Button ID="btnManAuditOverrule" runat="server" Text=" �������� " CssClass="widebuttons" CausesValidation="false"  OnClientClick=" showLoading();" 
                        OnClick="btnManAuditOverrule_Click" />&nbsp;
                    <input type="button" id="Button1" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonGroupHRAudit" runat="server">
                    <asp:Button ID="btnGroupHRAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading(); " 
                        OnClick="btnPassGroupHR_Click" />&nbsp;
                    <input type="button" id="Button5" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonHRAudit" runat="server">
                    <asp:Button ID="btnHRAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnPassHR_Click" />&nbsp;
                    <input type="button" id="Button6" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonFinanceAudit" runat="server">
                    <asp:Button ID="btnFinanceAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnPassFinance_Click" />&nbsp;
                    <input type="button" id="Button7" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonITAudit" runat="server">
                    <asp:Button ID="btnITAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnPassIT_Click" />&nbsp;
                    <input type="button" id="Button8" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonADAudit" runat="server">
                    <asp:Button ID="btnADAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnPassAD_Click" />&nbsp;
                    <input type="button" id="Button9" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonHR2Audit" runat="server">
                    <asp:Button ID="btnHR2AuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnPassHR2_Click" />&nbsp;
                    <input type="button" id="Button10" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
                <asp:Panel ID="pnlButtonHRDirAudit" runat="server" Visible="false">
                    <asp:Button ID="btnHRDirAuditPass" runat="server" Text=" ����ͨ�� " CssClass="widebuttons"  OnClientClick=" showLoading();" 
                        OnClick="btnHRDirAuditPass_Click" />&nbsp;
                    <input type="button" id="Button11" class="widebuttons" onclick="window.location = 'DimissionAuditList.aspx';"
                        value=" ���� " />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList1">
            <iframe src="UserList3.aspx?depid=<%=depId %>" id="userListFrame1" height="90%" width="100%"></iframe>
            <input type="hidden" id="test" value="ddddd" class="selected" />
        </div>
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList2">
            <iframe src="UserList3.aspx?depid=<%=depId %>" id="userListFrame2" height="90%" width="100%"></iframe>
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
</asp:Content>
