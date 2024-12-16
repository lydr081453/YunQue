<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Customer_AreaInfoEdit" Codebehind="AreaInfoEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" language="javascript" src="/public/js/dialog.js"></script>
    
    <table style="width: 100%">
        <tr>
            <td style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">客户区域信息编辑
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">地区代码:
                        </td>
                        <td class="oddrow-l" style="width: 35%"><asp:TextBox ID="txtCode" runat="server" MaxLength="10" Width="50%" />&nbsp;<font color="red">*</font>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="地区代码"></asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 15%">地区名称:</td>
                        <td class="oddrow-l" style="width: 35%"><asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="50%" />&nbsp;<font color="red">*</font>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="地区名称"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">查询码:</td>
                        <td class="oddrow-l"><asp:TextBox ID="txtSearchCode" runat="server" MaxLength="10" Width="50%" /></td>
                        <td class="oddrow">英文描述:</td>
                        <td class="oddrow-l"><asp:TextBox ID="txtENDescription" runat="server" MaxLength="100" Width="50%" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow">备注</td>
                        <td class="oddrow-l"  colspan="3"><asp:TextBox ID="txtNote" runat="server" Width="70%" Height="60px" TextMode="MultiLine"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click"
                    ValidationGroup="Save" />&nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 "
                        CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                    ShowMessageBox="true" DisplayMode="bulletList" />
            </td>
        </tr>
        <tr>
            <td style="height:10px">&nbsp;</td>
        </tr>
    </table>
</asp:Content>