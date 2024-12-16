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
            <td colspan="4" class="heading">Traffic Fee申请</td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                付款申请描述：<input type="hidden" runat="server" id="hidReturnID" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtDesc" Width="60%"></asp:TextBox>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc"
                    Display="none" ErrorMessage="付款申请描述为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                项目号：<input type="hidden" runat="server" id="hidProjectID" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtProjectCode" onkeyDown="return false; " Style="cursor: hand"
                    Width="25%"></asp:TextBox><input type="button" id="btnProjectCode" value="选择" class="widebuttons" onclick="openProjectDlg();" />
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProjectCode"
                    Display="none" ErrorMessage="项目号为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                成本所属组：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" onchange="clearTypes(this);" /><asp:HiddenField ID="hidDeptId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                Traffic Fee成本金额：
            </td>
            <td class="oddrow-l">
                项目Traffic Fee总额：<asp:Label ID="labAllTraffic" runat="server" />&nbsp;剩余：<asp:TextBox ID="txtSYTraffic" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                预计付款账期：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />&nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBeginDate"
                    Display="none" ErrorMessage="起始日期为必填项"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                预计支付金额：
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtPreFee"></asp:TextBox>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPreFee"
                    Display="none" ErrorMessage="预计支付金额为必填项"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpresionValidator1" runat="server" ControlToValidate="txtPreFee"
                    ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"
                    ErrorMessage="预计支付金额输入有误" Display="None"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="CompareValidator1" Display="None" runat="server" ControlToValidate="txtPreFee" ControlToCompare="txtSYTraffic" Operator="LessThanEqual" Type="Currency" ErrorMessage="预计支付金额不能大于Traffic Fee剩余金额"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                申请类型：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlPaymentType" Enabled="false">
                </asp:DropDownList>
                &nbsp;<font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPaymentType"
                    InitialValue="-1" Display="none" ErrorMessage="申请类型为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
            <tr>
            <td class="oddrow" width="15%">
                备注信息：<input type="hidden" runat="server" id="Hidden1" />
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Width="60%" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" CssClass="widebuttons" runat="server" Text=" 保存 " OnClick="btnSave_Click"
                    CausesValidation="true" />
                <asp:Button ID="btnSetting" CssClass="widebuttons" runat="server" Text="设置业务审核人"
                    OnClick="btnSetting_Click" CausesValidation="true" />
                <asp:Button ID="btnReturn" CssClass="widebuttons" runat="server" Text=" 返回 " OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            ShowMessageBox="true" ShowSummary="false" />
    </table>
</asp:Content>
