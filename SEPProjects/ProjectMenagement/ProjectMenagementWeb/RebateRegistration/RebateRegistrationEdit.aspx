<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RebateRegistration_RebateRegistrationEdit" Codebehind="RebateRegistrationEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script>
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function MediaClick() {
            var win = window.open('/Dialogs/searchSupplier.aspx?projectId=' + $("#<% =hidProjectId.ClientID %>").val(), null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function setMedia(id, name, rate) {
            $("#<% =hidMediaId.ClientID %>").val(id);
            $("#<% =txtMediaName.ClientID %>").val(name);
        }

        function ProjectClick() {
            var win = window.open('/Dialogs/ProjectList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function setProject(id, name) {
            $("#<% =hidProjectId.ClientID %>").val(id);
            $("#<% =txtProjectCode.ClientID %>").val(name);

            setMedia('', '', '')
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">
                返点登记信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                项目:
            </td>
            <td class="oddrow-l">
                <asp:HiddenField ID="hidProjectId" runat="server" />
                <asp:TextBox runat="server" onfocus="this.blur();" Style="cursor: hand;" ID="txtProjectCode"
                    Width="20%" />&nbsp;<input type="button" id="Button2" class="widebuttons" onclick="return ProjectClick();"
                        class="widebuttons" value="搜索" />
            </td>
            </tr>
        <tr>
            <td class="oddrow">
                媒体:
            </td>
            <td class="oddrow-l">
                <asp:HiddenField ID="hidMediaId" runat="server" />
                <asp:TextBox runat="server" onfocus="this.blur();" Style="cursor: hand;" ID="txtMediaName"
                    Width="20%" />&nbsp;<input type="button" id="Button1" class="widebuttons" onclick="return MediaClick();"
                        class="widebuttons" value="搜索" /><font color="red"> *</font><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMediaName" Display="None" ErrorMessage="媒体为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td class="oddrow">
            返点金额:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRebateAmount" Text="0" Width="10%" runat="server" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRebateAmount" Display="None" ErrorMessage="返点金额为必填项"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtRebateAmount" runat="server" ErrorMessage=" 错误"></asp:CompareValidator>
            </td>
        </tr>
        <tr><td class="oddrow">
            入账日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCreditedDate" onclick="setDate(this);" Width="10%" runat="server" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCreditedDate" Display="None" ErrorMessage="入账日期为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRemark" Width="40%" Height="100px" runat="server" TextMode="MultiLine" />
            </td>
        </tr>
    </table>
    
    <table width="100%" class="XTable">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
