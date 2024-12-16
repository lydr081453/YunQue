<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Employees_DimissionApplyDetail" Codebehind="DimissionApplyDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                离职申请信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="txtjoinJobDate" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                所属团队:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtgroupName" runat="server" />
            </td>
            <td class="oddrow">
                所在部门:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtdepartmentName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">社会保险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsEndowmentEndTime" runat="server" ></asp:Label></td>
            <td class="oddrow" style="width: 20%">公积金结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsPublicReserveFundsEndTime" runat="server" /><%--失业保险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsUnemploymentEndTime" runat="server" />--%></td>
        </tr>
       <%-- <tr>
            <td class="oddrow" style="width: 20%">生育险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsBirthEndTime" runat="server" /></td>        
            <td class="oddrow" style="width: 20%">工伤险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsCompoEndTime" runat="server"  /></td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">医疗保险结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsMedicalEndTime" runat="server"  /></td>
            <td class="oddrow" style="width: 20%">公积金结束时间</td><td class="oddrow-l" style="width: 30%"><asp:Label ID="labsPublicReserveFundsEndTime" runat="server" /></td>
        </tr>--%>
        <tr>
            <td class="oddrow">
                离职日期:
            </td>
            <td class="oddrow-l" colspan="3"><asp:Label ID="txtdimissionDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                离职申请说明:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtdimissionCause" runat="server" />
            </td>
        </tr>
        <%--<tr>
            <td class="oddrow">
                部门总监批示:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtdepartmentMajordomoName" runat="server" />
            </td>
            <td class="oddrow">
                批复日期:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtdepartmentMajordomoDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                团队经理批示:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtgroupManagerName" runat="server" />
            </td>
            <td class="oddrow">
                批复日期:
            </td>
            <td class="oddrow-l"><asp:Label ID="txtgroupManagerDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                人力行政部核准:
            </td>
            <td class="oddrow-l" colspan="3"><asp:Label ID="txthrAuditMemo" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="4" class="oddrow-l">
                <input type="button" runat="server" value=" 返回 " class="widebuttons" id="btnBack" onserverclick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

