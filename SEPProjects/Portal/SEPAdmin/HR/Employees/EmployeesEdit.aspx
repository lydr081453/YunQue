<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/HR/Validate/ValidateMaster.Master"
    Inherits="Employees_EmployeesEdit" Title="" CodeBehind="EmployeesEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/UserDepartment.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        $(function() {
            $('#container-1').tabs();
            show();

        });

        $(document).ready(function() {
            show();

        });

        function reloadCalendar() {
            if (document.getElementById("<%=txtContract_StartDate.ClientID%>").value != '') {
                var yyyy = Number(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[0]);
                var mm = Number(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[1]);
                var dd = Number(document.getElementById("<%=txtContract_StartDate.ClientID%>").value.split('-')[2]);
                var mydate = new Date(yyyy, mm + 6, dd);
                document.getElementById("<%=txtContract_ProbationPeriodDeadLine.ClientID%>").value = mydate.getFullYear() + '-' + (mydate.getMonth()) + '-' + mydate.getDate();
                document.getElementById("<%=txtContract_ProbationEndDate.ClientID%>").value = mydate.getFullYear() + '-' + (mydate.getMonth()) + '-' + mydate.getDate();
                //��������õ�����,����
                //var result = (eRDate - sRDate) / (24 * 60 * 60 * 1000);
            }
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd', 'reloadCalendar()');
        }

        function setCosts(pro, base, costs) {
            var pro = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + pro;
            var base = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + base;
            var costs = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + costs;
            var eif = Number(document.getElementById("" + pro + "").value);
            var sbase = Number(document.getElementById("" + base + "").value);
            if (eif != "NaN" && sbase != "NaN") {
                var val = sbase * (eif / 100);
                document.getElementById("" + costs + "").value = ForDight(val, 2);
            }
        }

        function setMICosts() {
            var mii = Number(document.getElementById("<%=txtMIProportionOfIndividuals.ClientID%>").value);
            var mif = Number(document.getElementById("<%=txtMIProportionOfFirms.ClientID%>").value);
            var mbase = Number(document.getElementById("<%=txtInsurance_MedicalInsuranceBase.ClientID%>").value);
            if (mif != "NaN" && mbase != "NaN") {
                var val = mbase * (mif / 100);
                document.getElementById("<%=txtMIFCosts.ClientID%>").value = ForDight(val, 2);
            }
            if (mii != "NaN" && mbase != "NaN") {
                var val2 = mbase * (mii / 100);
                document.getElementById("<%=txtMIICosts.ClientID%>").value = ForDight(val2, 2);
            }
        }
        function setSICosts() {
            var eii = Number(document.getElementById("<%=txtEIProportionOfIndividuals.ClientID%>").value);
            var eif = Number(document.getElementById("<%=txtEIProportionOfFirms.ClientID%>").value);
            var bif = Number(document.getElementById("<%=txtBIProportionOfFirms.ClientID%>").value);
            var uif = Number(document.getElementById("<%=txtUIProportionOfFirms.ClientID%>").value);
            var uii = Number(document.getElementById("<%=txtUIProportionOfIndividuals.ClientID%>").value);
            var cif = Number(document.getElementById("<%=txtCIProportionOfFirms.ClientID%>").value);
            var protec = Number(document.getElementById("<%= hidProtectionLine.ClientID %>").value);

            var sbase = Number(document.getElementById("<%=txtInsurance_SocialInsuranceBase.ClientID%>").value);
            if (eif != "NaN" && sbase != "NaN") {
                var val = sbase * (eif / 100);
                document.getElementById("<%=txtEIFCosts.ClientID%>").value = ForDight(val, 2);
            }
            if (eii != "NaN" && sbase != "NaN") {
                var val2 = sbase * (eii / 100);
                document.getElementById("<%=txtEIICosts.ClientID%>").value = ForDight(val2, 2);
            }
            if (bif != "NaN" && sbase != "NaN" && protec != "NaN") {
                var val3;
                if (sbase < protec)
                    val3 = protec * (bif / 100);
                else
                    val3 = sbase * (bif / 100);
                document.getElementById("<%=txtBIFCosts.ClientID%>").value = ForDight(val3, 2);
            }
            if (uif != "NaN" && sbase != "NaN") {
                var val4 = sbase * (uif / 100);
                document.getElementById("<%=txtUIFCosts.ClientID%>").value = ForDight(val4, 2);
            }
            if (uii != "NaN" && sbase != "NaN") {
                var val5 = sbase * (uii / 100);
                document.getElementById("<%=txtUIICosts.ClientID%>").value = ForDight(val5, 2);
            }
            if (cif != "NaN" && sbase != "NaN") {
                var val6 = sbase * (cif / 100);
                document.getElementById("<%=txtCIFCosts.ClientID%>").value = ForDight(val6, 2);
            }
        }
        //��������
        function ForDight(Dight, How) {
            var Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
            return Dight;
        }

        function changeStatus() {
            var chk = document.getElementById("<%= chkEnd.ClientID %>");
            var chkP = document.getElementById("<%= chkPRF.ClientID %>");
            var endowyear = document.getElementById("<%=  drpEndowmentStarTimeY.ClientID %>");
            var endowmonth = document.getElementById("<%= drpEndowmentStarTimeM.ClientID %>");
            var publicyear = document.getElementById("<%= drpPublicReserveFundsStarTimeY.ClientID %>");
            var publicmonth = document.getElementById("<%= drpPublicReserveFundsStarTimeM.ClientID %>");
            var sib = document.getElementById("<%= txtInsurance_SocialInsuranceBase.ClientID %>");
            var mib = document.getElementById("<%= txtInsurance_MedicalInsuranceBase.ClientID %>");
            var prfb = document.getElementById("<%= txtPublicReserveFunds_Base.ClientID %>");
            var mif = document.getElementById("<%= txtMIProportionOfFirms.ClientID %>");
            var mii = document.getElementById("<%= txtMIProportionOfIndividuals.ClientID %>");
            var mibi = document.getElementById("<%= txtMIBigProportionOfIndividuals.ClientID %>");
            var eif = document.getElementById("<%= txtEIProportionOfFirms.ClientID %>");
            var eii = document.getElementById("<%= txtEIProportionOfIndividuals.ClientID %>");
            var bif = document.getElementById("<%= txtBIProportionOfFirms.ClientID%>");
            var uif = document.getElementById("<%= txtUIProportionOfFirms.ClientID%>");
            var uii = document.getElementById("<%= txtUIProportionOfIndividuals.ClientID%>");
            var cif = document.getElementById("<%= txtCIProportionOfFirms.ClientID%>");
            var mifc = document.getElementById("<%= txtMIFCosts.ClientID%>");
            var miic = document.getElementById("<%= txtMIICosts.ClientID%>");
            var eifc = document.getElementById("<%= txtEIFCosts.ClientID%>");
            var eiic = document.getElementById("<%= txtEIICosts.ClientID%>");
            var bifc = document.getElementById("<%= txtBIFCosts.ClientID%>");
            var uifc = document.getElementById("<%= txtUIFCosts.ClientID%>");
            var uiic = document.getElementById("<%= txtUIICosts.ClientID%>");
            var cifc = document.getElementById("<%= txtCIFCosts.ClientID%>");
            var prff = document.getElementById("<%= txtPRFProportionOfFirms.ClientID%>");
            var prfc = document.getElementById("<%= txtPRFCosts.ClientID%>");

            if (chk.checked) {
                endowyear.disabled = true;
                endowmonth.disabled = true;
                sib.disabled = true;
                mib.disabled = true;
                mif.disabled = true;
                mii.disabled = true;
                mibi.disabled = true;
                eif.disabled = true;
                eii.disabled = true;
                bif.disabled = true;
                uif.disabled = true;
                uii.disabled = true;
                cif.disabled = true;
                mifc.disabled = true;
                miic.disabled = true;
                eifc.disabled = true;
                eiic.disabled = true;
                bifc.disabled = true;
                uifc.disabled = true;
                uiic.disabled = true;
                cifc.disabled = true;
            }
            else {
                endowyear.disabled = false;
                endowmonth.disabled = false;
                sib.disabled = false;
                mib.disabled = false;
                mif.disabled = false;
                mii.disabled = false;
                mibi.disabled = false;
                eif.disabled = false;
                eii.disabled = false;
                bif.disabled = false;
                uif.disabled = false;
                uii.disabled = false;
                cif.disabled = false;
                mifc.disabled = false;
                miic.disabled = false;
                eifc.disabled = false;
                eiic.disabled = false;
                bifc.disabled = false;
                uifc.disabled = false;
                uiic.disabled = false;
                cifc.disabled = false;
            }
            if (chkP.checked) {
                publicyear.disabled = true;
                publicmonth.disabled = true;
                prfb.disabled = true;
                prff.disabled = true;
                prfc.disabled = true;
            }
            else {
                publicyear.disabled = false;
                publicmonth.disabled = false;
                prfb.disabled = false;
                prff.disabled = false;
                prfc.disabled = false;
            }
        }

        function changeChecked() {
            var cmchk = document.getElementById("<%= chkComplementaryMedical.ClientID %>");
            var accchk = document.getElementById("<%= chkAccidentInsurance.ClientID %>");
            var drpCY = document.getElementById("<%=drpComplementaryMedicalY.ClientID %>");
            var drpCM = document.getElementById("<%=drpComplementaryMedicalM.ClientID %>");
            var accy = document.getElementById("<%= drpAccidentInsuranceBeginTimeY.ClientID %>");
            var accm = document.getElementById("<%= drpAccidentInsuranceBeginTimeM.ClientID %>");
            if (cmchk.checked) {
                drpCY.disabled = false;
                drpCM.disabled = false;
            }
            else {
                drpCY.disabled = true;
                drpCM.disabled = true;
            }
            if (accchk.checked) {
                accy.disabled = false;
                accm.disabled = false;
            }
            else {
                accy.disabled = true;
                accm.disabled = true;
            }
        }

        function changeInsurance() {
            try {
                var ins = document.getElementById("<%= drpInsurance_MemoPlace.ClientID %>").options;
                var bic = document.getElementById("<%= txtBIFCosts.ClientID%>");
                var uii = document.getElementById("<%= txtUIICosts.ClientID %>");
                var value;
                for (i = 0; i < ins.length; i++) {
                    if (ins[i].selected)
                        value = ins[i];
                }
                if (value.value == "�⸷���򻧿�" || value.value == "�⸷ũҵ����") {
                    if (value.value == "�⸷ũҵ����") {
                        uii.value = "0";
                        uii.disabled = true;
                    }
                    else {
                        uii.disabled = false;
                        setSICosts();
                    }
                    bic.value = "0";
                    bic.disabled = true;
                }
                else {
                    bic.disabled = false;
                    uii.disabled = false;
                    setSICosts();
                }
            }
            catch (err) { return; }
        }

        function changeInsuranceAddress() {
            try {
                var ins = document.getElementById("<%= drpInsurance_SocialInsuranceAddress.ClientID %>").options;
                var value;
                for (var i = 0; i < ins.length; i++) {
                    if (ins[i].selected)
                        value = ins[i];
                }
                if (value.value == "����") {
                    document.getElementById("<%= txtMIProportionOfFirms.ClientID %>").value = 10;    // ҽ�ƹ�˾
                    document.getElementById("<%= txtMIProportionOfIndividuals.ClientID %>").value = 2;    // ҽ�Ƹ���
                    document.getElementById("<%= txtMIBigProportionOfIndividuals.ClientID %>").value = 3;  // ���ҽ�Ʊ��ո���֧�����
                    document.getElementById("<%= txtEIProportionOfFirms.ClientID %>").value = 20;    // ���Ϲ�˾
                    document.getElementById("<%= txtEIProportionOfIndividuals.ClientID %>").value = 8;   // ���ϸ���
                    document.getElementById("<%= txtUIProportionOfFirms.ClientID %>").value = 1;   // ʧҵ��˾
                    document.getElementById("<%= txtUIProportionOfIndividuals.ClientID %>").value = 0.2;  // ʧҵ����
                    document.getElementById("<%= txtBIProportionOfFirms.ClientID %>").value = 0.8;  // ����
                    document.getElementById("<%= txtCIProportionOfFirms.ClientID %>").value = 0.3;  // ����
                    document.getElementById("<%= txtPRFProportionOfFirms.ClientID %>").value = 12;   // ������
                    setSICosts();
                    setCosts('txtPRFProportionOfFirms', 'txtPublicReserveFunds_Base', 'txtPRFCosts');
                }
                else if (value.value == "�Ϻ�") {
                    document.getElementById("<%= txtMIProportionOfFirms.ClientID %>").value = 12;    // ҽ�ƹ�˾
                    document.getElementById("<%= txtMIProportionOfIndividuals.ClientID %>").value = 2;    // ҽ�Ƹ���
                    document.getElementById("<%= txtMIBigProportionOfIndividuals.ClientID %>").value = 0;  // ���ҽ�Ʊ��ո���֧�����
                    document.getElementById("<%= txtEIProportionOfFirms.ClientID %>").value = 22;    // ���Ϲ�˾
                    document.getElementById("<%= txtEIProportionOfIndividuals.ClientID %>").value = 8;   // ���ϸ���
                    document.getElementById("<%= txtUIProportionOfFirms.ClientID %>").value = 2;   // ʧҵ��˾
                    document.getElementById("<%= txtUIProportionOfIndividuals.ClientID %>").value = 1;  // ʧҵ����
                    document.getElementById("<%= txtBIProportionOfFirms.ClientID %>").value = 0.5;  // ����
                    document.getElementById("<%= txtCIProportionOfFirms.ClientID %>").value = 0.5;  // ����
                    document.getElementById("<%= txtPRFProportionOfFirms.ClientID %>").value = 7;   // ������
                    setSICosts();
                    setCosts('txtPRFProportionOfFirms', 'txtPublicReserveFunds_Base', 'txtPRFCosts');
                }
                else if (value.value == "����") {
                    document.getElementById("<%= txtMIProportionOfFirms.ClientID %>").value = 7;    // ҽ�ƹ�˾
                    document.getElementById("<%= txtMIProportionOfIndividuals.ClientID %>").value = 2;    // ҽ�Ƹ���
                    document.getElementById("<%= txtMIBigProportionOfIndividuals.ClientID %>").value = 9.83;  // ���ҽ�Ʊ��ո���֧�����
                    document.getElementById("<%= txtEIProportionOfFirms.ClientID %>").value = 12;    // ���Ϲ�˾
                    document.getElementById("<%= txtEIProportionOfIndividuals.ClientID %>").value = 8;   // ���ϸ���
                    document.getElementById("<%= txtUIProportionOfFirms.ClientID %>").value = 0.2;   // ʧҵ��˾
                    document.getElementById("<%= txtUIProportionOfIndividuals.ClientID %>").value = 0.1;  // ʧҵ����
                    document.getElementById("<%= txtBIProportionOfFirms.ClientID %>").value = 0.85;  // ����
                    document.getElementById("<%= txtCIProportionOfFirms.ClientID %>").value = 0.4;  // ����
                    document.getElementById("<%= txtPRFProportionOfFirms.ClientID %>").value = 12;   // ������
                    setSICosts();
                    setCosts('txtPRFProportionOfFirms', 'txtPublicReserveFunds_Base', 'txtPRFCosts');
                }
            }
            catch (err) {
                return;
            }
        }

        function changeContractCompany() {
            var ins = document.getElementById("<%= drpContract_Company.ClientID %>").options;
            var value;
            for (var i = 0; i < ins.length; i++) {
                if (ins[i].selected)
                    value = ins[i];
            }
            document.getElementById("<%= txtContract_Company.ClientID %>").value = value.value;
        }

        function changeInsuranceSocialInsuranceCompany() {
            var ins = document.getElementById("<%= drpInsurance_SocialInsuranceCompany.ClientID %>").options;
            var value;
            for (var i = 0; i < ins.length; i++) {
                if (ins[i].selected)
                    value = ins[i];
            }
            document.getElementById("<%= txtInsurance_SocialInsuranceCompany.ClientID %>").value = value.value;
        }
    </script>

    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul>
                        <li><a href="#fragment-1"><span>������Ϣ</span></a></li>
                        <li><a href="#fragment-3"><span>��ͬ���</span></a></li>
                        <li><a href="#fragment-4"><span>���ո���</span></a></li>
                        <li><a href="#fragment-6"><span>�������</span></a></li>
                        <li><a href="#fragment-7"><span>���</span></a></li>
                        <li><a href="#fragment-8"><span>���ʻ���</span></a></li>
                    </ul>
                    <%--������Ϣ--%>
                    <div id="fragment-1" style="padding: 0px 0px 0px 0px;">
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <%--<tr>
                                <td class="oddrow" style="width: 10%">
                                    ��Ϣ�Ƿ���д����:
                                </td>
                                <td class="oddrow-l" colspan="7" style="width: 90%">
                                    <asp:CheckBox ID="chkBaseInfoOk" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="oddrow-l" colspan="6" align="center">
                                    <div class="photocontainer">
                                        <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg"
                                            CssClass="imagePhoto" ToolTip="ͷ��" />
                                    </div>
                                    <%--<asp:FileUpload ID="UploadImage" runat="server" />--%><a href="UpLoadUserPhoto.aspx?userid=<%=Request["userid"]%>">
                                        ����ϴ���Ƭ</a>
                                </td>
                            </tr>
                        </table>
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    Ա�����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <%--<asp:Label ID="txtUserCode" runat="server" />--%>
                                    <asp:TextBox ID="txtUserCode" runat="server" Enabled="false" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��ְ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtJob_JoinDate" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��˾�ʼ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtBase_Email" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��������:
                                </td>
                                <td class="oddrow-l" style="width: 20%;">
                                    ��&nbsp;<asp:TextBox ID="txtBase_LastNameCn" runat="server" Width="50px" />&nbsp;��&nbsp;<asp:TextBox
                                        ID="txtBase_FitstNameCn" runat="server" Width="50px" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    Ӣ������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    FirstName&nbsp;<asp:TextBox ID="txtBase_FirstNameEn" runat="server" Width="50px" />&nbsp;LastName&nbsp;<asp:TextBox
                                        ID="txtBase_LastNameEn" runat="server" Width="50px" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �����ǳ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtCommonName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    �Ա�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:RadioButton ID="radBase_Sex1" runat="server" Text="��" GroupName="Base_Sex" Checked="true" />
                                    <asp:RadioButton ID="radBase_Sex2" runat="server" Text="Ů" GroupName="Base_Sex" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_Birthday" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_PlaceOfBirth" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ���֤����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_IdNo" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBase_IdNo"
                                        Display="None" ErrorMessage="����д���֤��"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkCard"
                                        Display="None" ErrorMessage="����ȷ��д���֤��" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �������ڵ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_DomicilePlace" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ����״��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="txtBase_Marriage" runat="server">
                                        <asp:ListItem Value="2" Text="δ��" />
                                        <asp:ListItem Value="1" Text="�ѻ�����" />
                                        <asp:ListItem Value="4" Text="�ѻ�����" />
                                        <asp:ListItem Value="3" Text="����" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ����״��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_Health" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="99%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��ҵԺУ:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_FinishSchool" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��ҵʱ��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_FinishSchoolDate" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    רҵ:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_Speciality" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ���ѧ��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="txtBase_Education" runat="server">
                                        <asp:ListItem Text="����/��ר/�м�������" Value="����/��ר/�м�������"></asp:ListItem>
                                        <asp:ListItem Text="��ר��ͬ��ѧ��" Value="��ר��ͬ��ѧ��"></asp:ListItem>
                                        <asp:ListItem Text="����/ѧʿ����ͬѧ��" Value="����/ѧʿ����ͬѧ��"></asp:ListItem>
                                        <asp:ListItem Text="˶ʿ/�о�������ͬѧ��" Value="˶ʿ/�о�������ͬѧ��"></asp:ListItem>
                                        <asp:ListItem Text="��ʿ������" Value="��ʿ������"></asp:ListItem>
                                        <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �����س�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_WorkSpecialty" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="99%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <%--<tr>
                                <td class="oddrow-l">
                                    �Ƿ��⼮Ա��<input type="checkbox" id="chkForeign" runat="server" />
                                </td>
                                <td class="oddrow-l">
                                    �Ƿ��о�ҵ֤<input type="checkbox" id="chkCertification" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ������ȡ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpWageMonths" runat="server">
                                        <asp:ListItem Value="13" Text="13����" />
                                        <asp:ListItem Value="12" Text="12����" />
                                    </asp:DropDownList>
                                </td>
                                <td class="oddrow">
                                    ��˾������:
                                </td>
                                <td class="oddrow-l">
                                    
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��ͥ��ϵ�绰:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_HomePhone" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �ֻ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_MobilePhone" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBase_PrivateEmail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    ��ͥͨѶ��ַ:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtBase_Address1" runat="server" />
                                </td>
                                <td class="oddrow">
                                    ��������:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtBase_PostCode" runat="server" />
                                </td>
                                <td class="oddrow">
                                    ������ϵ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtBase_EmergencyLinkman" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    ������ϵ�˵绰:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtBase_EmergencyPhone" runat="server" />
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="oddrow">
                                    �ϼ���������:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtJob_DirectorName" runat="server" />
                                </td>
                                <td class="oddrow">
                                    �ϼ�����ְλ:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtJob_DirectorJob" runat="server" />
                                </td>
                                <td class="oddrow">
                                    ����������˾:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtBase_thisYearSalary" runat="server" />
                                </td>
                            </tr>--%>
                        </table>
                        <table width="99%" class="tableForm">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging"
                                        OnRowCommand="gvList_RowCommand" DataKeyNames="UserID,DepartmentPositionID,DepartmentID"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="CompanyName" HeaderText="��˾" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="GroupName" HeaderText="�Ŷ�" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="DepartmentPositionName" HeaderText="ְ��" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    <table width="100%" id="tabBottom" runat="server">
                                        <tr>
                                            <td width="50%">
                                                <asp:Panel ID="PageBottom" runat="server">
                                                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnFirst_Click" />&nbsp;
                                                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnPrevious_Click" />&nbsp;
                                                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnNext_Click" />&nbsp;
                                                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="ĩҳ" OnClick="btnLast_Click" />&nbsp;
                                                </asp:Panel>
                                            </td>
                                            <td align="right" class="recordTd">
                                                ��¼��:<asp:Label ID="labAllNum" runat="server" />&nbsp;ҳ��:<asp:Label ID="labPageCount"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                        </table>
                        <table width="99%" class="tableForm">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    �����ĵ��ϴ�
                                </td>
                                <td class="oddrow-l">
                                    <asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6">
                                    ��������:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtBase_WorkExperience" TextMode="MultiLine" Height="100px" Width="100%"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6">
                                    �����������Ƿ������صļ�����������¹ʣ���/�У�����ϸ˵��:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtBase_DiseaseInSixMonths" TextMode="MultiLine" Height="100px"
                                        Width="100%" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6">
                                    ��ע:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--��ͬ���--%>
                    <div id="fragment-3" style="padding: 0px 0px 0px 0px;">
                        <%--<table width="100%" class="tableForm">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��Ϣ�Ƿ���д����:
                                </td>
                                <td class="oddrow-l" style="width: 90%" colspan="5">
                                    <asp:CheckBox ID="chkContractInfoOk" runat="server" />
                                </td>
                            </tr>
                        </table>--%>
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��ͬ��˾:
                                </td>
                                <td class="oddrow-l" colspan="5">
                                    <asp:DropDownList ID="drpContract_Company" runat="server" onchange="changeContractCompany();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ��ͬ��˾:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtContract_Company" runat="server" ReadOnly="true" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��ͬ��ʼ��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtContract_StartDate" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    ��ͬ��ֹ��:
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:TextBox ID="txtContract_EndDate" runat="server" onclick="setDate(this);" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    �����ڽ�ֹ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtContract_ProbationPeriodDeadLine" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow">
                                    ת������:
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtContract_ProbationEndDate" runat="server" onclick="setDate(this);" />
                                </td>
                                <td class="oddrow">
                                    ��ǩ����:
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="drpRenewalCount" runat="server">
                                        <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    ��
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    ��ͬǩ�����:
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="drpJContract_ContractInfo" runat="server">
                                        <asp:ListItem Text="����ȡ" Value="����ȡ" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="�ѷ���" Value="�ѷ���"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="oddrow-l" colspan="4">
                                </td>
                            </tr>
                        </table>
                        <table width="99%" class="tableForm">
                            <tr>
                                <td class="oddrow" colspan="8">
                                    ��ע:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="8">
                                    <asp:TextBox ID="txtContract_Memo" Height="100px" Width="100%" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--���ո���--%>
                    <div id="fragment-4" style="padding: 0px 0px 0px 0px;">
                        <%--<table width="100%" class="tableForm">
                            <tr>
                                <td class="oddrow" style="width: 11%">
                                    ��Ϣ�Ƿ���д����:
                                </td>
                                <td style="width: 100%" align="right">
                                    
                                </td>
                            </tr>
                        </table>--%>
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow-l" style="width: 10%">
                                    �籣���ڹ�˾:
                                </td>
                                <td class="oddrow-l" colspan="4">
                                    <asp:DropDownList ID="drpInsurance_SocialInsuranceCompany" runat="server" onchange="changeInsuranceSocialInsuranceCompany();" />
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <input type="checkbox" id="chkEnd" runat="server" onclick="changeStatus();" />û���籣&nbsp;
                                    <input type="checkbox" id="chkPRF" runat="server" onclick="changeStatus();" />û�й�����
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    �籣���ڹ�˾:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtInsurance_SocialInsuranceCompany" runat="server" ReadOnly="true" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �籣�����ص�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpInsurance_SocialInsuranceAddress" runat="server" onchange="changeInsuranceAddress();">
                                        <asp:ListItem Text="����" Value="����" />
                                        <asp:ListItem Text="�Ϻ�" Value="�Ϻ�" />
                                        <asp:ListItem Text="����" Value="����" />
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtInsurance_SocialInsuranceAddress" runat="server" />--%>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �������ڵ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpInsurance_MemoPlace" runat="server" onchange="changeInsurance();">
                                        <asp:ListItem Text="���г��򻧿�" Value="���г��򻧿�" />
                                        <asp:ListItem Text="����ũҵ����" Value="����ũҵ����" />
                                        <asp:ListItem Text="�⸷���򻧿�" Value="�⸷���򻧿�" />
                                        <asp:ListItem Text="�⸷ũҵ����" Value="�⸷ũҵ����" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div id="divBase" runat="server">
                            <table width="99%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                                <tr>
                                    <td class="oddrow" style="width: 10%">
                                        ����/ʧҵ/����/�����ɷѻ���:
                                    </td>
                                    <td class="oddrow-l" style="width: 20%">
                                        <asp:TextBox ID="txtInsurance_SocialInsuranceBase" runat="server" onblur="setSICosts();" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtInsurance_SocialInsuranceBase"
                                            Display="Dynamic" ErrorMessage="��������ȷ����/ʧҵ/����/�����ɷѻ���" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ����/ʧҵ/����/�����ɷѻ���</asp:RegularExpressionValidator>
                                        <input type="hidden" id="hidProtectionLine" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 10%">
                                        ҽ�ƻ���:
                                    </td>
                                    <td class="oddrow-l" style="width: 20%">
                                        <asp:TextBox ID="txtInsurance_MedicalInsuranceBase" runat="server" onblur="setMICosts();" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInsurance_MedicalInsuranceBase"
                                            Display="Dynamic" ErrorMessage="��������ȷҽ�ƻ���" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷҽ�ƻ���</asp:RegularExpressionValidator>
                                    </td>
                                    <td class="oddrow" style="width: 10%">
                                        ��ᱣ�տ�ʼʱ�䣺
                                    </td>
                                    <td class="oddrow-l" style="width: 20%">
                                        <asp:DropDownList ID="drpEndowmentStarTimeY" runat="server">
                                        </asp:DropDownList>
                                        ��<asp:DropDownList ID="drpEndowmentStarTimeM" runat="server">
                                        </asp:DropDownList>
                                        ��
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <table width="100%" cellpadding="0" class="tableForm">
                                            <tr>
                                                <td align="center" colspan="4" style="width: 25%; height: 40px; background-color: #edf1f4;">
                                                    ҽ�Ʊ���
                                                </td>
                                                <td align="center" colspan="4" style="width: 25%; background-color: #edf1f4;">
                                                    ���ϱ���
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ��˾����:<%--ҽ�Ʊ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtMIProportionOfFirms" runat="server" onblur="setCosts('txtMIProportionOfFirms','txtInsurance_MedicalInsuranceBase','txtMIFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtMIProportionOfFirms"
                                                        Display="Dynamic" ErrorMessage="��������ȷҽ�Ʊ��չ�˾����" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷҽ�Ʊ��չ�˾����</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ���˱���:<%--ҽ�Ʊ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtMIProportionOfIndividuals" runat="server" onblur="setCosts('txtMIProportionOfIndividuals','txtInsurance_MedicalInsuranceBase','txtMIICosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtMIProportionOfIndividuals"
                                                        Display="Dynamic" ErrorMessage="��������ȷҽ�Ʊ��ո��˱���" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷҽ�Ʊ��ո��˱���</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ��˾����:<%--���ϱ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtEIProportionOfFirms" runat="server" onblur="setCosts('txtEIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtEIFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                        ControlToValidate="txtEIProportionOfFirms" Display="Dynamic" ErrorMessage="��������ȷ���ϱ��չ�˾����"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ���ϱ��չ�˾����</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ���˱���:<%--���ϱ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtEIProportionOfIndividuals" runat="server" onblur="setCosts('txtEIProportionOfIndividuals','txtInsurance_SocialInsuranceBase','txtEIICosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                        ControlToValidate="txtEIProportionOfIndividuals" Display="Dynamic" ErrorMessage="��������ȷ���ϱ��ո��˱���"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ���ϱ��ո��˱���</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ��˾Ӧ�ɷ���:<%--ҽ�Ʊ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtMIFCosts" runat="server" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ����Ӧ�ɷ���:<%--ҽ�Ʊ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtMIICosts" runat="server" onblur="setMIICosts();" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ��˾Ӧ�ɷ���:<%--���ϱ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtEIFCosts" runat="server" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ����Ӧ�ɷ���:<%--���ϱ���--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtEIICosts" runat="server" />Ԫ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ���ҽ�Ƹ���֧����:
                                                </td>
                                                <td class="oddrow-l" colspan="3">
                                                    <asp:TextBox Width="80px" ID="txtMIBigProportionOfIndividuals" runat="server" />Ԫ
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtMIBigProportionOfIndividuals"
                                                        Display="Dynamic" ErrorMessage="��������ȷҽ�Ʊ��մ��ҽ�Ƹ���֧����" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷҽ�Ʊ��մ��ҽ�Ƹ���֧����</asp:RegularExpressionValidator>
                                                </td>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="width: 24%; height: 40px; background-color: #edf1f4;">
                                                    ʧҵ����
                                                </td>
                                                <td align="center" colspan="2" style="width: 13%; background-color: #edf1f4;">
                                                    ��������
                                                </td>
                                                <td align="center" colspan="2" style="width: 13%; background-color: #edf1f4;">
                                                    ������
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ��˾����:<%--ʧҵ����--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtUIProportionOfFirms" runat="server" onblur="setCosts('txtUIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtUIFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                        ControlToValidate="txtUIProportionOfFirms" Display="Dynamic" ErrorMessage="��������ȷʧҵ���չ�˾����"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷʧҵ���չ�˾����</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ���˱���:<%--ʧҵ����--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtUIProportionOfIndividuals" runat="server" onblur="setCosts('txtUIProportionOfIndividuals','txtInsurance_SocialInsuranceBase','txtUIICosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                        ControlToValidate="txtUIProportionOfIndividuals" Display="Dynamic" ErrorMessage="��������ȷʧҵ���ո��˱���"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷʧҵ���ո��˱���</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ��˾����:<%--��������--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtBIProportionOfFirms" runat="server" onblur="setCosts('txtBIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtBIFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                        ControlToValidate="txtBIProportionOfFirms" Display="Dynamic" ErrorMessage="��������ȷ�������չ�˾����"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ�������չ�˾����</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ��˾����:<%--������--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtCIProportionOfFirms" runat="server" onblur="setCosts('txtCIProportionOfFirms','txtInsurance_SocialInsuranceBase','txtCIFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                        ControlToValidate="txtCIProportionOfFirms" Display="Dynamic" ErrorMessage="��������ȷ�����չ�˾����"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ�����չ�˾����</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ��˾Ӧ�ɷ���:<%--ʧҵ����--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtUIFCosts" runat="server" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ����Ӧ�ɷ���:<%--ʧҵ����--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtUIICosts" runat="server" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ��˾Ӧ�ɷ���:<%--��������--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtBIFCosts" runat="server" />Ԫ
                                                </td>
                                                <td class="oddrow">
                                                    ��˾Ӧ�ɷ���:<%--������--%>
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtCIFCosts" runat="server" />Ԫ
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="width: 24%; height: 40px; background-color: #edf1f4;">
                                                    ������
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" style="height: 35px;">
                                                    ����:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtPublicReserveFunds_Base" runat="server" onblur="setCosts('txtPRFProportionOfFirms','txtPublicReserveFunds_Base','txtPRFCosts');" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPublicReserveFunds_Base"
                                                        Display="Dynamic" ErrorMessage="��������ȷ���������" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ���������</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    ��ʼʱ�䣺
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:DropDownList ID="drpPublicReserveFundsStarTimeY" runat="server" Width="50px">
                                                    </asp:DropDownList>
                                                    ��<asp:DropDownList ID="drpPublicReserveFundsStarTimeM" runat="server" Width="50px">
                                                    </asp:DropDownList>
                                                    ��
                                                </td>
                                                <td class="oddrow">
                                                    ����:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtPRFProportionOfFirms" runat="server" onblur="setCosts('txtPRFProportionOfFirms','txtPublicReserveFunds_Base','txtPRFCosts');" />%
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                        ControlToValidate="txtPRFProportionOfFirms" Display="Dynamic" ErrorMessage="��������ȷ���������"
                                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ���������</asp:RegularExpressionValidator>
                                                </td>
                                                <td class="oddrow">
                                                    Ӧ�ɷ���:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox Width="80px" ID="txtPRFCosts" runat="server" />Ԫ
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="99%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ����ҽ��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:CheckBox ID="chkComplementaryMedical" onclick="changeChecked();" runat="server" />
                                </td>
                                <td class="oddrow">
                                    ����ҽ�ƿ�ʼʱ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="drpComplementaryMedicalY" runat="server">
                                    </asp:DropDownList>
                                    ��
                                    <asp:DropDownList ID="drpComplementaryMedicalM" runat="server">
                                    </asp:DropDownList>
                                    ��
                                    <%--<asp:TextBox ID="txtComplementaryMedical" runat="server" />Ԫ
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                        ControlToValidate="txtComplementaryMedical" Display="Dynamic" ErrorMessage="��������ȷ����ҽ�Ʒ���"
                                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ����ҽ�Ʒ���</asp:RegularExpressionValidator>--%>
                                </td>
                                <td class="oddrow-l" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ���Ᵽ��:
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkAccidentInsurance" onclick="changeChecked();" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 20%">
                                    ���Ᵽ�տ�ʼʱ�䣺
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:DropDownList ID="drpAccidentInsuranceBeginTimeY" runat="server">
                                    </asp:DropDownList>
                                    ��
                                    <asp:DropDownList ID="drpAccidentInsuranceBeginTimeM" runat="server">
                                    </asp:DropDownList>
                                    ��
                                </td>
                                <td class="oddrow-l" colspan="2">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--�������--%>
                    <div id="fragment-6" style="padding: 0px 0px 0px 0px;">
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <%--<tr>
                                <td class="oddrow" style="width: 10%">
                                    ��Ϣ�Ƿ���д����:
                                </td>
                                <td class="oddrow-l" style="width: 90%" colspan="5">
                                    <asp:CheckBox ID="chkArchiveInfoOk" runat="server" />
                                </td>
                            </tr>--%>
                            <%--</table><table width="100%" class="tableForm"> --%>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    �������ڵ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <%--<asp:CheckBox ID="chkArchive_IsArchive" runat="server" />--%>
                                    <asp:DropDownList ID="drpArchive_ArchivePlace" runat="server">
                                        <asp:ListItem Text="�������˲�" Value="�������˲�" />
                                        <asp:ListItem Text="����" Value="����" />
                                        <asp:ListItem Text="�Ϸ��˲��г�" Value="�Ϸ��˲��г�" />
                                    </asp:DropDownList>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtArchive_Code" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    �浵��ʼ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtArchive_ArchiveDate" runat="server" onclick="setDate(this);" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--���/������--%>
                    <div id="fragment-7" style="padding: 0px 0px 0px 0px;">
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    ְλ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpAnnualBase" runat="server">
                                        <asp:ListItem Text="7" Value="7" />
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="0" Value="0" />
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtAnnualBase" runat="server" />--%>
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--���ʻ���--%>
                    <div id="fragment-8" style="padding: 0px 0px 0px 0px;">
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <div id="divSalary" runat="server">
                                <tr>
                                    <td class="oddrow">
                                        ��������:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtJob_basePay" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtJob_basePay"
                                            Display="Dynamic" ErrorMessage="��������ȷ�̶�����" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ�̶�����</asp:RegularExpressionValidator>
                                    </td>
                                    <td class="oddrow">
                                        ��Ч����:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtJob_meritPay" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtJob_meritPay"
                                            Display="Dynamic" ErrorMessage="��������ȷ��׼��Ч" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">��������ȷ��׼��Ч</asp:RegularExpressionValidator>
                                    </td>
                                    <td class="oddrow" style="width: 10%">
                                        ������ȡ����:
                                    </td>
                                    <td class="oddrow-l" style="width: 20%">
                                        <asp:DropDownList ID="drpWageMonths" runat="server">
                                            <asp:ListItem Value="13" Text="13����" />
                                            <asp:ListItem Value="12" Text="12����" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </div>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <table width="90%">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" �� �� " />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" �� �� " />
            </td>
        </tr>
    </table>
</asp:Content>
