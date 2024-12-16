<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_PrepareDisplay"
    CodeBehind="PrepareDisplay.ascx.cs" %>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">① 项目准备信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 10%">确认项目号:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblProjectCode" runat="server">（财务填写）</asp:Label>&nbsp;<asp:Label
                ID="labOldProjectCode" runat="server" ForeColor="Red" />
        </td>
        <td class="oddrow">关联项目号:</td>
        <td class="oddrow-l">
            <asp:Label ID="labRelevanceProjectCode" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 10%">流水号:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblSerialCode" runat="server"></asp:Label>
        </td>
        <td class="oddrow">项目类型：
        </td>
        <td class="oddrow-l" style="width: 40%">
            <asp:Label ID="lblProjectType" runat="server"></asp:Label>
        </td>

    </tr>
    <tr>
        <td class="oddrow">负责人:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
        </td>
        <td class="oddrow">合同状态:</td>
        <td class="oddrow-l">
            <asp:Label ID="lblContactStatus" runat="server"></asp:Label>
        </td>

    </tr>
    <tr>
        <td class="oddrow">创建人:</td>
        <td class="oddrow-l">
            <asp:Label ID="labCreator" runat="server" CssClass="userLabel" /></td>
        <td class="oddrow">项目组别:</td>
        <td class="oddrow-l">
            <asp:Label ID="lblGroup" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">商务负责人:</td>
        <td class="oddrow-l">
            <asp:Label ID="labBusinessPersonName" runat="server" CssClass="userLabel" /></td>
        <td class="oddrow">业务类型:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBizType" runat="server"></asp:Label>
        </td>

    </tr>
    <tr>
        <td class="oddrow">项目名称:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBizDesc" runat="server"></asp:Label>
        </td>
         <td class="oddrow">BD项目号:</td>
        <td class="oddrow-l">
            <asp:Label ID="lblBDProject" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow">品牌:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labBrands" runat="server"></asp:Label>
        </td>
    </tr>
            <tr>
        <td class="oddrow">广告主账户ID:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labAdvertiserID" runat="server" ></asp:Label>
        </td>
    </tr>
        <tr>
        <td class="oddrow">客户项目编号:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labCustomerProjectCode" runat="server"  ></asp:Label>
        </td>
    </tr>
</table>
