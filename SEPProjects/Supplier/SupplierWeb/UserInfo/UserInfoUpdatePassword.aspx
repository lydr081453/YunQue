<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdatePassword.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfoUpdatePassword" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td width="30%">原始密码：</td>
            <td><asp:TextBox ID="txtOldPassword" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="txtOldPassword" Display="None" ErrorMessage="请填写原始密码"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="30%">新密码：</td>
            <td><asp:TextBox ID="txtNewPassword" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  ControlToValidate="txtNewPassword" Display="None" ErrorMessage="请填写新密码"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="30%">确认新密码：</td>
            <td><asp:TextBox ID="txtNewPasswordConfirm" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="txtNewPasswordConfirm" Display="None" ErrorMessage="请填写确认新密码"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" 
                  ControlToCompare="txtNewPassword" ControlToValidate="txtNewPasswordConfirm" 
                  Display="None" ErrorMessage="新密码和确认密码填写不一致"></asp:CompareValidator></td>
        </tr>
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="保存" onclick="btnUpdate_Click" />
    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" CausesValidation="false" />
      <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
</asp:Content>