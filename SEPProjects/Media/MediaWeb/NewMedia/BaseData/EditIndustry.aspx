<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeBehind="EditIndustry.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.EditIndustry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%;">
    
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labHeading" runat="server">添加记者</asp:Label>
            </td>
        </tr>

        <tr>
            <td class="oddrow" style="width: 20%">
                属性名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox><font color="red"> *</font>
            </td>
            <td class="oddrow" style="width: 20%">
                &nbsp;
            </td>
            <td class="oddrow-l" style="width: 30%">
            &nbsp;
            </td>
        </tr>
        
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="btnOk" runat="server" OnClientClick="return check();" CssClass="widebuttons"
                    Text="保存" OnClick="btnOk_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" /><asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();return false;" />
                <input type="reset" class="widebuttons" value="重置" />
            </td>
        </tr>
    </table>
</asp:Content>
