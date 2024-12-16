<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Score.ascx.cs" Inherits="PurchaseWeb.UserControls.Edit.Score" %>
<table width="100%" class="tableForm">
    <tr>
        <td width="15%" class="oddrow">
            <asp:Label ID="litContentName" runat="server" Style="cursor: pointer" />：<asp:HiddenField
                ID="hidContentId" runat="server" />
        </td>
        <td class="oddrow-l">
            <table width="100%" class="XTable">
                <tr>
                    <td>
                        <asp:RadioButtonList RepeatDirection="Vertical" AutoPostBack="true" CssClass="XTable" 
                            OnSelectedIndexChanged="ddlScore_SelectedIndexChanged" ID="ddlScore" runat="server" />
                        <asp:RequiredFieldValidator ID="CV1" runat="server" ErrorMessage="" ControlToValidate="ddlScore" Display="None" ></asp:RequiredFieldValidator>
                        <asp:Panel ID="P" runat="server" Visible="false">
                            差评描述：<asp:TextBox ID="txtRemark" runat="server" Width="80%" /><asp:RequiredFieldValidator
                                ID="RV1" runat="server" ForeColor="Red" Display="Static" ControlToValidate="txtRemark"
                                Text="*" ErrorMessage="请填写差评描述"></asp:RequiredFieldValidator>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
