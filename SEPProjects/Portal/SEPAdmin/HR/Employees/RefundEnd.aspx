<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefundEnd.aspx.cs"
MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.RefundEnd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
<script language="javascript" src="/public/js/jquery.js"></script>
<script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>
<script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>
<script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script type="text/javascript">

    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }

</script>
<input type="hidden" runat="server" id="hidFlag" />
<table width="100%" class="tableForm">
  <tr>
            <td class="heading" colspan="4">
                <%=flagStr %>笔记本租赁
            </td>
        </tr>
        <tr>
            
            <td class="oddrow" style="width: 10%">
                员工姓名:
            </td>
            <td class="oddrow-l" >
                <asp:Label ID="labUserName" runat="server"></asp:Label>           
            </td>
            <td class="oddrow" style="width: 10%">租赁<%=flagStr %>日期：</td>
            <td class="oddrow-l">          
               <asp:TextBox ID="txtEndTime" onkeyDown="return false; " onclick="setDate(this);"
                    runat="server" />  
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                         ControlToValidate="txtEndTime" Display="None" /><font
                            color="red"> * </font>
            </td>
        </tr>       
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSave" runat="server" Text=" 提 交 " CssClass="widebuttons"  OnClick="btnCommit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text=" 返 回 " CausesValidation="false" CssClass="widebuttons"  OnClick="btnBack_Click" />
            </td>
        </tr>
  </table> 
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
     <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
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
