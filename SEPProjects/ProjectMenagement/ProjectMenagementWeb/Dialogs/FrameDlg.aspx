<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="Dialogs_FrameDlg" Title="客户框架协议信息" Codebehind="FrameDlg.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
        <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function valid() {
            var msg = "";
            if (document.getElementById("<%=txtFrameDesc.ClientID %>").value == "") {
                msg += "协议描述为必填项." + "\n";
            }
            if (document.getElementById("<%=txtBeginDate.ClientID %>").value == "") {
                msg += "起始日期为必填项." + "\n";
            }
            if (document.getElementById("<%=txtEndDate.ClientID %>").value == "") {
                msg += "结束日期为必填项." + "\n";
            }
            if (document.getElementById("<%=fileupContract.ClientID %>").value == "" && document.getElementById("<%=hidFile.ClientID %>").value == "") {
                msg += "协议附件为必填项." + "\n";
            }

            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
        }
        </script>
    <table style="width: 100%">
    <tr>
    <td class="heading" colspan="4">客户框架协议信息</td>
    </tr>
        <tr>
            <td class="oddrow">
                协议描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtFrameDesc" runat="server" Width="60%"></asp:TextBox>&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrameDesc"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="协议描述为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                起始日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBeginDate"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="起始日期为必填项"></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td class="oddrow" style="width: 15%">
                结束日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndDate"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="结束日期为必填项"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                协议附件:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:FileUpload ID="fileupContract" CssClass="file" runat="server" Width="60%" /><font color="red">*</font>
                <asp:HiddenField ID="hidFile" runat="server" />
                <asp:HyperLink ID="lnkFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                协议备注:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="60%" TextMode="MultiLine" Height="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td class="oddrow" align="center" colspan="4">
        <asp:Button ID="btnSubmit" runat="server" Text=" 确定 " CssClass="widebuttons" 
                 OnClientClick="return valid();" onclick="btnSubmit_Click" />
        <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" 
                onclick="btnClose_Click" />
        </td>
        
        </tr>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
    </table>

</asp:Content>
