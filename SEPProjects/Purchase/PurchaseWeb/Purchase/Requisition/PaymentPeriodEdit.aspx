<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_PaymentPeriodEdit" Codebehind="PaymentPeriodEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" src="../../public/js/DatePicker.js"></script>
<table width="100%" class="tableForm">
    <tr>
        <td colspan="4" class="heading"><asp:Literal ID="litTitle" runat="server" /></td>
    </tr>
    <tr>
        <td style="width:15%" class="oddrow">账期金额:</td>
        <td style="width:35%" class="oddrow-l"><asp:TextBox ID="txtperiodPrice" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="txtperiodPrice" Display="None" ErrorMessage="账期金额为必填" /><font
                                                color="red"> * </font><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtperiodPrice"
                    ErrorMessage="请输入正确账期金额" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator></td>
        <td style="width:15%" class="oddrow">账期时间:</td>
        <td style="width:35%" class="oddrow-l"><asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txtBegin" Display="None" ErrorMessage="起始账期时间为必填" /><font
                                                color="red"> * </font><img src="../../images/dynCalendar.gif"
                    border="0" onclick="popUpCalendar(document.getElementById('<%=txtBegin.ClientID %>'), document.getElementById('<%=txtBegin.ClientID %>'), 'yyyy-mm-dd');" />
                 - <asp:TextBox runat="server" ID="txtEnd" onfocus="javascript:this.blur();"></asp:TextBox>&nbsp;<img
                                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtEnd.ClientID %>'), document.getElementById('<%=txtEnd.ClientID %>'), 'yyyy-mm-dd');" /></td>
    </tr>
    <tr>
        <td class="oddrow">账期备注:</td>
        <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtperiodRemark" runat="server" Height="50px" Width="80%" TextMode="MultiLine" /></td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4"><input runat="server" id="btnSave" value=" 保存  "  type="button" causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />&nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" CausesValidation="false" /></td>
    </tr>
</table>

</asp:Content>

