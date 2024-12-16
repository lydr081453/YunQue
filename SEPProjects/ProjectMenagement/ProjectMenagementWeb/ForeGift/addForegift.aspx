<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="addForegift.aspx.cs" Inherits="ForeGift_addForegift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <asp:HiddenField ID="hidPrId" runat="server" />
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <uc1:TopMessage ID="TopMessage" runat="server" IsEditPage="true" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                Ѻ����Ϣ
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR����:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                ��Ŀ��:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%; ">
                ����������:
            </td>
            <td class="oddrow-l" style="width: 35%; ">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
                <asp:HiddenField ID="hidApplicant" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%; ">
                Ѻ����ˮ:
            </td>
            <td class="oddrow-l" style="width: 35%; ">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ���븶��ʱ��:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                Ѻ��״̬:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%; ">
                ��Ӧ������:
            </td>
            <td class="oddrow-l" colspan="3" style="height: 23px">
                <asp:TextBox runat="server" ID="txtSupplierName" Width="40%" /><font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="��Ӧ������Ϊ����" ControlToValidate="txtSupplierName" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ����������:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierBank" Width="40%"/><font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="����������Ϊ����" ControlToValidate="txtSupplierBank" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                �������ʺ�:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSupplierAccount" Width="40%"/><font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="�������ʺ�Ϊ����" ControlToValidate="txtSupplierAccount" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                Ѻ����:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblForegift" runat="server"></asp:Label><br />
            </td>
            <td class="oddrow" style="width: 15%">
                ���ʽ:<input type="hidden" id="hidPaymentTypeID" runat="server" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow">Ԥ�Ƹ���ʱ��:</td>
            <td class="oddrow-l"><asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                <font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ErrorMessage="��ѡ��Ԥ�Ƹ���ʱ��" ControlToValidate="txtBeginDate" Display="None"></asp:RequiredFieldValidator></td>
            <td class="oddrow">
                Ԥ�ƹ黹ʱ��:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                <font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="��ѡ��Ԥ�ƹ黹ʱ��" ControlToValidate="txtEndDate" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                Ѻ������:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtReturnContent" runat="server" Width="500px" TextMode="MultiLine"
                    Height="50px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <input type="button" id="btnNo" value="����" runat="server" class="widebuttons" onclick=""
                    onserverclick="btnSave_Click" />&nbsp;<input type="button" id="btnSubmit" value="����ҵ�������"
                        runat="server" class="widebuttons" onclick="" onserverclick="btnSetting_Click" />
                <asp:Button ID="btnReturn" Text=" ���� " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false"
                    runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
