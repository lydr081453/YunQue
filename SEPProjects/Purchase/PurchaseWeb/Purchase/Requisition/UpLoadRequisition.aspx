<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_UpLoadRequisition" Codebehind="UpLoadRequisition.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm" border="1">
    <tr>
        <td colspan="2" class="heading">导入申请单</td>
    </tr>
    <tr>
        <td style="width:20%" class="oddrow">导入申请单流水号：</td>
        <td class="oddrow-l"><asp:TextBox ID="txtId" runat="server" MaxLength="7"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="请填写流水号" Display="None" ControlToValidate="txtId"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                 ErrorMessage="流水号格式错误" Display="None" ControlToValidate="txtId" ValidationExpression="\d*"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2"  class="oddrow-l"><asp:Button ID="btnUpload" runat="server" Text=" 导入 " CssClass="widebuttons" OnClick="btnUpload_Click" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" />
        </td>
    </tr>
</table>
</asp:Content>