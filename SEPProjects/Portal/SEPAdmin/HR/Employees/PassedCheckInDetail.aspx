<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Employees_PassedCheckInDetail" Codebehind="PassedCheckInDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                转正登记信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow" style="width: 20%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="txtjoinJobDate"  Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                隶属公司:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtcompany" runat="server"  Enabled="false" />
            </td>
            <td class="oddrow">
                所在部门:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtdepartmentName" runat="server"  Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                所属团队:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtgroupName" runat="server"  Enabled="false" />
            </td>
            <td class="oddrow">
                职位:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labcurrentTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtoperationDate" runat="server" />
            </td>
        </tr>
        <div id="divSalary" runat="server">
        <tr>
            <td class="oddrow">
                转正前基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnowBasePay"  Enabled="false" runat="server" />
            </td>
            <td class="oddrow">
                转正前绩效工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnowMeritPay"  Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                转正后基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnewBasePay" runat="server" />
            </td>
            <td class="oddrow">
                转正后绩效工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnewMeritPay" runat="server" />
            </td>
        </tr>
        </div>
        <tr>
            <td class="oddrow">
                备注:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtmemo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                    <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

