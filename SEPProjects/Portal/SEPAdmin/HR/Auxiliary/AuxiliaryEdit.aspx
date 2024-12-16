<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuxiliaryEdit.aspx.cs" Inherits="SEPAdmin.HR.Auxiliary.AuxiliaryEdit"  MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                待入职辅助工作信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                辅助工作名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtauxiliaryName" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="辅助工作名称必填" Display="None" ControlToValidate="txtauxiliaryName" /><font
                        color="red"> * </font>
                        <input type="hidden" id="hidId" runat="server" />
            </td>         
        </tr>
        <tr>
            <td class="oddrow">
                工作描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtDescription" runat="server" />
                
            </td>
            
        <tr>
        <tr>
            <td class="oddrow">
                使用公司:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="drpCompany" runat="server">
                </asp:DropDownList>
                
            </td>
            
        <tr>
        <tr>
            <td class="oddrow">
                使用方:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="drpApply" runat="server">
                                <asp:ListItem Value="1" Text="待入职" />                                
                                <asp:ListItem Value="2" Text="离职" />
                                <asp:ListItem Value="3" Text="入职" />
                            </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">                 
                <asp:Button ID="btnCommit" runat="server" Text=" 保 存 " CausesValidation="false" CssClass="widebuttons" OnClick="btnCommit_Click" />                      
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返 回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table> 
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />        
</asp:Content>