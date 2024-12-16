<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequirementDescInfo.ascx.cs"
    Inherits="UserControls_View_RequirementDescInfo" %>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ⑥ 工作需求描述
        </td>
    </tr>
    <tr>
        <td colspan="4" class="oddrow-l">
            <asp:Label ID="txtsow" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            附件:
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:Label ID="labdownSow" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            审批流向:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labrequisitionflow" runat="server"></asp:Label> <asp:RadioButtonList ID="rblrequisitionflow" Visible="false" runat="server" RepeatDirection="horizontal"
                CssClass="XTable">
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
