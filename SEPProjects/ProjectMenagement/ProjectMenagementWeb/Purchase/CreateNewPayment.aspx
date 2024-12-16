<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_CreateNewPayment" EnableEventValidation="false" Codebehind="CreateNewPayment.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Project/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>
    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function openProjectDlg() {
            var win = window.open('/Dialogs/ProjectDlg.aspx?Page=NewPayment&<%=ESP.Finance.Utility.RequestName.ReturnID %>=<%=Request[ESP.Finance.Utility.RequestName.ReturnID] %>&lnkId=<%=lnkDepartment.ClientID %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setProjectValue(projectId, projectCode, deptList) {
            document.getElementById("<%= hidProjectID.ClientID%>").value = projectId;
            document.getElementById("<%= txtProjectCode.ClientID%>").value = projectCode;

            if (deptList != "")
                insertDepartment(deptList);
        }

        function insertDepartment(deptList) {
            deptControl = document.getElementById("<%= ddlDepartment.ClientID %>");
            var depts = deptList.split('#');
            deptControl.options.length = 0;
            for (i = 0; i < depts.length; i++) {
                var option = document.createElement("OPTION");
                option.value = depts[i].split(',')[0];
                option.text = depts[i].split(',')[1];
                deptControl.options.add(option);
            }
            if (deptControl.options.length > 0) {
                document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[0].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[0].text;
            }
        }

        function clearTypes(obj) {
            document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].text;
        }
    </script>
    <asp:LinkButton ID="lnkDepartment" runat="server" OnClick="lnkDepartment_Click" />
    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table class="tableform" width="100%">
        <tr>
            <td colspan="4" class="heading">Traffic Fee����</td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                ��������������<input type="hidden" runat="server" id="hidReturnID" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtDesc" Width="60%"></asp:TextBox>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc"
                    Display="none" ErrorMessage="������������Ϊ������"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                ��Ŀ�ţ�<input type="hidden" runat="server" id="hidProjectID" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtProjectCode" onkeyDown="return false; " Style="cursor: hand"
                    Width="25%"></asp:TextBox><input type="button" id="btnProjectCode" value="ѡ��" class="widebuttons" onclick="openProjectDlg();" />
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProjectCode"
                    Display="none" ErrorMessage="��Ŀ��Ϊ������"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                �ɱ������飺
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" onchange="clearTypes(this);" /><asp:HiddenField ID="hidDeptId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                Traffic Fee�ɱ���
            </td>
            <td class="oddrow-l">
                ��ĿTraffic Fee�ܶ<asp:Label ID="labAllTraffic" runat="server" />&nbsp;ʣ�ࣺ<asp:TextBox ID="txtSYTraffic" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                Ԥ�Ƹ������ڣ�
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />&nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBeginDate"
                    Display="none" ErrorMessage="��ʼ����Ϊ������"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                Ԥ��֧����
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtPreFee"></asp:TextBox>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPreFee"
                    Display="none" ErrorMessage="Ԥ��֧�����Ϊ������"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpresionValidator1" runat="server" ControlToValidate="txtPreFee"
                    ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"
                    ErrorMessage="Ԥ��֧�������������" Display="None"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="CompareValidator1" Display="None" runat="server" ControlToValidate="txtPreFee" ControlToCompare="txtSYTraffic" Operator="LessThanEqual" Type="Currency" ErrorMessage="Ԥ��֧�����ܴ���Traffic Feeʣ����"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                �������ͣ�
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlPaymentType" Enabled="false">
                </asp:DropDownList>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPaymentType"
                    InitialValue="-1" Display="none" ErrorMessage="��������Ϊ������"></asp:RequiredFieldValidator>
            </td>
        </tr>
            <tr>
            <td class="oddrow" width="15%">
                ��ע��Ϣ��<input type="hidden" runat="server" id="Hidden1" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Width="60%" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" CssClass="widebuttons" runat="server" Text=" ���� " OnClick="btnSave_Click"
                    CausesValidation="true" />
                <asp:Button ID="btnSetting" CssClass="widebuttons" runat="server" Text="����ҵ�������"
                    OnClick="btnSetting_Click" CausesValidation="true" />
                <asp:Button ID="btnReturn" CssClass="widebuttons" runat="server" Text=" ���� " OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            ShowMessageBox="true" ShowSummary="false" />
    </table>
</asp:Content>
