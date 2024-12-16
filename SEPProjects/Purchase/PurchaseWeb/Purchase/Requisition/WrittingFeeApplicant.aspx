<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="Purchase_Requisition_WrittingFeeApplicant"
    CodeBehind="WrittingFeeApplicant.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function DisplayDetail(id, dis, idshow, idnone) {
            document.getElementById(id).style.display = dis;
            document.getElementById(idshow).style.display = "block";
            document.getElementById(idnone).style.display = "none";
        }
        function MediaClick() {
            var mediatype = document.getElementById('<% =ddlOption.ClientID %>').selectedIndex;
            var medianame = document.getElementById('<% = txtMediaSelect.ClientID %>').value;
            var win = window.open('MediaDlg.aspx?<% =ESP.Purchase.Common.RequestName.MediaType %>=' + mediatype + '&<% =ESP.Purchase.Common.RequestName.MediaName %>=' + medianame, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) > 0) {
                return true;
            }
            else {
                return false;
            }
        }


        function ReporterClick() {
            var mid = document.getElementById('<% = hidMediaID.ClientID %>').value;
            if (mid == null || mid == "" || mid == "undefined") {
                alert("����ѡ��ý��." + mid);
                return false;
            }
            var win = window.open('ReporterDlg.aspx?Mid=' + mid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function ShowImage() {
            var ddl = document.getElementById("<%=radioList.ClientID %>").value;
            if (ddl == "1") {
                document.getElementById("<%=lblImgSize.ClientID %>").style.display = "block";
                document.getElementById("<%=txtImgSize.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=lblImgSize.ClientID %>").style.display = "none";
                document.getElementById("<%=txtImgSize.ClientID %>").style.display = "none";
            }

        }

        function checkInfo() {
            var msg = "";
 
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 2)//��ǰ֧��
            {
                if (document.getElementById("<% = txtDateBegin.ClientID%>").value == "")
                    msg += "����д��ǰ֧���Ŀ�ʼʱ��." + "\n";
                if (document.getElementById("<% = txtDateEnd.ClientID%>").value == "")
                    msg += "����д��ǰ֧���Ľ���ʱ��." + "\n";
                var amount = document.getElementById('<%=txtAmountQ.ClientID %>').value;
                amount = amount.replace(',', '');

                if (!testNum(amount)) {
                    msg += "����������" + "\n";
                }

                var preWordLength = document.getElementById('<%=txtPreWordLength.ClientID %>').value;
                preWordLength = preWordLength.replace(',', '');

                if (!testNum(preWordLength)) {
                    msg += "���������������" + "\n";
                }
            }
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 1)//����֧��
            {
                if (document.getElementById("<% = txtTitle.ClientID%>").value == "")
                { msg += "����д����֧���Ŀ�������." + "\n"; }
                if (document.getElementById("<% = txtWordLength.ClientID%>").value == "")
                { msg += "����д����֧��������." + "\n"; }
                else if (document.getElementById("<% = txtWordLength.ClientID%>").value.search(/^[-\+]?\d+(\.\d+)?$/) == -1) {
                    msg += "�����������" + "\n";
                }
                var imageradio = document.getElementById("<% = rdImageList.ClientID%>" + "_0");
                if (imageradio.checked == true) {
                    if (document.getElementById("<% = txtImgSize.ClientID%>").value == "")
                    { msg += "����дͼƬ�ߴ�." + "\n"; }
                    else if (document.getElementById("<% = txtImgSize.ClientID%>").value.search(/^[-\+]?\d+(\.\d+)?$/) == -1) {
                        msg += "ͼƬ�ߴ��������" + "\n";
                    }
                }
                if (document.getElementById("<% = txtReleaseDate.ClientID%>").value == "") {
                    msg += "����д�������ڣ�" + "\n";
                }
                var amount = document.getElementById('<%=txtAmount.ClientID %>').value;
                amount = amount.replace(',', '');
                if (!testNum(amount)) {
                    msg += "����������" + "\n";
                }
                if ((document.getElementById("<% = filSow.ClientID%>").value == "" && document.getElementById("<% = lblFileName.ClientID%>").innerHTML == "") && document.getElementById("<% = txtHref.ClientID%>").value == "") {
                    msg += "���ϴ��������������ӣ�" + "\n";
                }
            }
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 0) {
                msg += "��ѡ��֧����ʽ��" + "\n";
            }
            var mid = document.getElementById('<% = hidMediaID.ClientID %>').value;
            if (mid == null || mid == "" || mid == "undefined") {
                msg += "��ѡ��ý�壡" + "\n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
        }

        function selectPayType(id, text) {

            if (text == "��ǰ֧��") {
                document.getElementById("<% =palH.ClientID %>").style.display = "none";
                document.getElementById("<% =palQ.ClientID %>").style.display = "block";
            }
            else if (text == "����֧��") {
                document.getElementById("<% =palH.ClientID %>").style.display = "block";
                document.getElementById("<% =palQ.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<% =palH.ClientID %>").style.display = "none";
                document.getElementById("<% =palQ.ClientID %>").style.display = "none";
            }
        }

        function showBirthday() {
            var val = document.getElementById("<% =txtIC.ClientID %>").value;
            var birthdayValue = "";
            if (15 == val.length) { //15λ���֤����
                birthdayValue = val.charAt(6) + val.charAt(7);
                if (parseInt(birthdayValue) < 10) {
                    birthdayValue = '20' + birthdayValue;
                }
                else {
                    birthdayValue = '19' + birthdayValue;
                }
                birthdayValue = birthdayValue + '-' + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11);
                return birthdayValue;
            }
            else if (18 == val.length) { //18λ���֤����
                birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11) + '-' + val.charAt(12) + val.charAt(13);

                return birthdayValue;
            }
            else {
                return '1900-01-01';
            }
        }

        function showNomal() {
            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2");
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");

            tr1.style.display = "";
            tr2.style.display = "";
            tr3.style.display = "none";
            tr4.style.display = "none";
        }
        function showImport() {
            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2");
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");

            tr1.style.display = "none";
            tr2.style.display = "none";
            tr3.style.display = "";
            tr4.style.display = "";
        }

    </script>

    <table width="100%" class="tableform">
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow">
                            �����������
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescription"
                                Display="None" ErrorMessage="�����������Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                        </td>
                    </tr>
                    <tr onclick="showNomal();">
                        <td class="heading" colspan="4" style="cursor: hand">
                            �ֶ���ѯģʽ
                        </td>
                    </tr>
                    <tr id="tr1">
                        <td class="oddrow" style="width: 15%;">
                            ý�����ͣ�
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList runat="server" ID="ddlOption">
                                <asp:ListItem Text="��ѡ��.." Value="0"></asp:ListItem>
                                <asp:ListItem Text="ƽ��ý��" Value="1"></asp:ListItem>
                                <asp:ListItem Text="����ý��" Value="2"></asp:ListItem>
                                <asp:ListItem Text="����ý��" Value="3"></asp:ListItem>
                                <asp:ListItem Text="�㲥ý��" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 15%">
                            ý�����ƣ�
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtMediaSelect" runat="server"></asp:TextBox><input type="hidden"
                                id="hidMediaID" runat="server" />
                            <input type="button" value="��ѡ��..." class="widebuttons" onclick="return MediaClick();" /><font
                                color="red">�����ѡ�������ý��</font>
                        </td>
                    </tr>
                    <tr id="tr2">
                        <td class="oddrow" style="width: 15%">
                            ���ߣ�
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtReporterSelect" runat="server"></asp:TextBox><input type="hidden"
                                id="hidReporterID" runat="server" />
                            <input type="button" value="��ѡ��..." class="widebuttons" onclick="return ReporterClick();" /><font
                                color="red">�����ѡ������¼���</font>
                        </td>
                        <td class="oddrow" colspan="2" align="center">
                            <asp:Button ID="btnExport" runat="server" Text="ģ�嵼��ģʽ" OnClick="btnExport_Click"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr onclick="showImport();">
                        <td class="heading" colspan="4" style="cursor: hand">
                            ����/����ģʽ
                        </td>
                    </tr>
                    <tr style="display: none" id="tr3">
                        <td colspan="4" class="oddrow">
                            <a href="WrittingFeeExport.aspx" target="_blank">����������Ϣģ��</a>
                        </td>
                    </tr>
                    <tr style="display: none" id="tr4">
                        <td class="oddrow">
                            ���������Ϣ��
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="fupReportersImport" runat="server" Width="300px" />&nbsp;&nbsp;<asp:Button
                                ID="btnUpload" runat="server" Text=" �ϴ� " CausesValidation="false" OnClick="btnUpload_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="pnlReporter" Style="display: none">
                    <table width="100%" border="0" class="tableform">
                        <tr>
                            <td class="heading" colspan="4">
                                ������Ϣ
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ����ý�壺
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="lblMedia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lblMedia"
                                    Display="None" ErrorMessage="ý������Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                ����������
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="lblReporter" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lblReporter"
                                    Display="None" ErrorMessage="����Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                �����Ա�
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:DropDownList runat="server" ID="ddlSex">
                                    <asp:ListItem Text="��ѡ��.." Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Ů" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="����" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                �����ֻ���
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtReporterPhone" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReporterPhone"
                                    Display="None" ErrorMessage="�����ֻ�Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" colspan="4">
                                ����֧����Ϣ
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                �տ��ˣ�
                            </td>
                            <td colspan="3" class="oddrow-l">
                                <asp:TextBox ID="txtBankAcountName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBankAcountName"
                                    Display="None" ErrorMessage="�տ���Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                �������֤�ţ�
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtIC" runat="server" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIC"
                                    Display="None" ErrorMessage="�������֤��Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                                <br />
                                ���տ�������߲���ͬһ��,����д�տ������֤��
                            </td>
                            <td style="width: 15%" class="oddrow">
                                �տ������ڳ��У�
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCity"
                                    Display="None" ErrorMessage="�տ������ڳ���Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                �տ�����ϵ��ʽ��
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone"
                                    Display="None" ErrorMessage="�տ�����ϵ��ʽΪ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td style="width: 15%" class="oddrow">
                                ������ȫ�ƣ�
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtBank" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBank"
                                    Display="None" ErrorMessage="������Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                �ʺţ�
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAccount"
                                    Display="None" ErrorMessage="�ʺ�Ϊ����"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td style="width: 15%" class="oddrow">
                                �Ƿ���Ҫ˰����
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:RadioButtonList runat="server" ID="radioTax" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ֧����ʽ��
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:DropDownList runat="server" ID="ddlPaymentType">
                                    <asp:ListItem Text="��ѡ��.." Value="-1"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="����֧��" />
                                    <asp:ListItem Value="1" Text="��ǰ֧��" />
                                </asp:DropDownList>
                                <font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                �����壺
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:RadioButtonList runat="server" ID="radioList" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palH" Style="display: none" runat="server">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="4" class="heading">
                                ���������Ϣ (��������д������Ϣ)
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                ������⣺
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                �������ڣ�
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtReleaseDate" onkeyDown="return false; " Style="cursor: hand"
                                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ʵ�ʿ���������
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox runat="server" ID="txtWordLength"></asp:TextBox><font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                ֧����
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                �Ƿ���ͼ��
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:RadioButtonList runat="server" ID="rdImageList" BorderStyle="None" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="��" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="��" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                <asp:Label runat="server" ID="lblImgSize">ͼƬ�ߴ�(ƽ������)��</asp:Label>
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtImgSize" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ���������
                            </td>
                            <td colspan="3" class="oddrow-l">
                                <asp:FileUpload ID="filSow" runat="server" Width="400px" />&nbsp;<asp:Label ID="lblFileName"
                                    runat="server" />&nbsp;
                                <asp:CheckBox ID="chkdownSow" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ������ӣ�
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtHref" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palQ" Style="display: none" runat="server">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="heading">
                                ��ǰ��ע������ʱ�䣺
                            </td>
                        </tr>
                        <tr>
                         <td class="oddrow" style="width: 15%">
                                ����ʱ�䣺
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtDateBegin" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                    onclick="setDate(this);" /><font color="red"> *</font> --
                                <asp:TextBox ID="txtDateEnd" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                    onclick="setDate(this);" /><font color="red"> *</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                ֧����
                            </td>
                            <td class="oddrow-l" style="width: 35%">
                                <asp:TextBox ID="txtAmountQ" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                ����������
                            </td>
                            <td class="oddrow-l" style="width: 35%">
                                <asp:TextBox ID="txtPreWordLength" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <font color="red">�������������Ϸ��Ĳ�ѯ����ѡ��ý��ͼ�����Ϣ</font>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:LinkButton runat="server" ID="btnHid" OnClick="btnHid_onclick"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnHidReturn" OnClick="btnHidReturn_onclick"></asp:LinkButton>
                <input runat="server" id="btnAdd" value=" ���沢��� " type="button" onclick="if(checkInfo() ){if(Page_ClientValidate())this.disabled=true;}else{return false;}"
                    onserverclick="btnAdd_onclick" class="widebuttons" causesvalidation="false" />&nbsp;
                <input runat="server" id="btnNext" value=" ���沢���� " type="button" onclick="if(checkInfo() ){if(Page_ClientValidate())this.disabled=true;}else{return false;}"
                    onserverclick="btnSave_onclick" class="widebuttons" causesvalidation="false" />&nbsp;
                <input id="btnReturnNoSave" runat="server" class="widebuttons" onserverclick="btnReturnNoSave_onclick"
                    type="button" value=" ֱ�ӷ��� " causesvalidation="false" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <input type="hidden" id="hidMediaOrderID" runat="server" />
                <input type="hidden" id="hidOrderID" runat="server" />
                <input type="hidden" id="hidEditPrice" runat="server" />
                <asp:Repeater runat="server" ID="rptParent" OnItemDataBound="rptParent_ItemDataBound">
                    <%--OnItemCommand="rptParent_ItemCommand">--%>
                    <HeaderTemplate>
                        <table class="gridView" cellspacing="0" cellpadding="4" border="0" style="background-color: White;
                            width: 100%; border-collapse: collapse;">
                            <tr class="Gheading" align="center">
                                <th scope="col">
                                    ��Ŀ
                                </th>
                                <th scope="col">
                                    �տ���
                                </th>
                                <th scope="col">
                                    ���֤��
                                </th>
                                <th scope="col">
                                    ֧�����
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="td" align="left" style="background-color: SkyBlue">
                            <td align="center" style="width: 10%;">
                                <%-- CommandName="Expand" CommandArgument='<%#Eval("ReporterID") %>' CommandName="Shrink" CommandArgument='<%#Eval("ReporterID") %>' --%>
                                <asp:HyperLink ID="btnViewChild" runat="server" ImageUrl="/images/Expand.gif" />
                                <asp:HyperLink ID="btnCancelChild" runat="server" ImageUrl="/images/Shrink.gif" />
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("ReceiverName") %>
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("CardNumber")%>
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("totalamount")%>
                            </td>
                        </tr>
                        <tr class="td" align="left">
                            <td align="center" style="width: 10%;">
                            </td>
                            <td colspan="3">
                                <asp:GridView ID="GVChild" runat="server" AutoGenerateColumns="false" DataKeyNames="MeidaOrderID"
                                    OnRowDataBound="GVChild_RowDataBound" OnRowEditing="GVChild_RowEditing" OnRowCommand="GVChild_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="���" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReporterID" HeaderText="ReporterID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MediaID" HeaderText="MediaID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Attachment" HeaderText="Attachment" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="wordLength" HeaderText="wordLength" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ReporterName" HeaderText="����" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="MediaName" HeaderText="����ý��" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Subject" HeaderText="��������" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:TemplateField HeaderText="֧������" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%#Eval("PayType").ToString()=="1"?"����֧��":"��ǰ֧��" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ƽ����" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvgPrice" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="totalamount" HeaderText="֧�����" ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitPrice" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                                        <asp:BoundField DataField="WrittingURL" HeaderText="����" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" />
                                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkDownLoad" runat="server" Text="<img src='/images/ico_04.gif' border='0'>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="�༭" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("MeidaOrderID") %>'
                                                    CommandName="Edit" Text="<img src='/images/edit.gif' title='�༭' border='0'>"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ɾ��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("MeidaOrderID") %>'
                                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='ɾ��' border='0'>"
                                                    OnClientClick="return confirm('��ȷ��ɾ����');" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
