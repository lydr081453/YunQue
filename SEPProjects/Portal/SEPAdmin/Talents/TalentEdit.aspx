<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TalentEdit.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="SEPAdmin.Talents.TalentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                中文名:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:Label runat="server" ID="lblNameCN"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                英文名:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblNameEN"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                职位名称:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:Label runat="server" ID="lblPosition2"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                联系电话:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblMobile"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                电子邮箱:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:Label runat="server" ID="lblEmail"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                学 历:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblEducation"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                工作时间:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:Label runat="server" ID="lblWorkBegin"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                目前薪酬:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                星言职位:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:Label runat="server" ID="lblPosition"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">
                所属部门:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                HR面试意见及反馈:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label runat="server" ID="lblHR"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                团队面试意见及反馈:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label runat="server" ID="lblGroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                简历文档上传
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labResume" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                简历详细信息:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:TextBox runat="server" ID="txtResume" TextMode="MultiLine" Width="80%" Height="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" 返 回 " />
            </td>
        </tr>
    </table>
</asp:Content>
