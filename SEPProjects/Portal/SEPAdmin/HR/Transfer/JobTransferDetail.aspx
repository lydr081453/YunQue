<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_JobTransferDetail"
    MasterPageFile="~/MasterPage.master" CodeBehind="JobTransferDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                员工调动信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                员工姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow" style="width: 20%">
                加入公司时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_JoinDate" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                现属业务团队:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:Label ID="ddlnowGroupName" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                具体调动说明
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                新业务团队:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_NewGroupName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow" style="width: 20%">
                新岗位职责:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_NewDuty" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                调动时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtJob_TransferDate" runat="server" />
            </td>
            <td class="oddrow">
                调动地点:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtJob_TransferPlace" runat="server" />
            </td>
        </tr>
        <div id="divSalary" runat="server"> 
        <tr>
            <td class="oddrow">
                原基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnowBasePay" runat="server" />
            </td>
            <td class="oddrow">
                原绩效工资
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnowMeritPay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                新基本工资:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnewBasePay" runat="server" />
            </td>
            <td class="oddrow">
                新绩效工资
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtnewMeritPay" runat="server" />
            </td>
        </tr>
        </div>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                安排调动（由HR填写）
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                调动原因:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_transferReason" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                调动时限:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_transferTimeLine" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                事宜安排（如需交接）:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtJob_evenPlan" runat="server" />
            </td>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtoperationDate" runat="server" />
            </td>
        </tr>
    </table>
    <%--<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                调动核准
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                原团队核准意见:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_nowAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                新团队核准意见:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txtJob_newAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                人事部:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtJob_hrAuditNote" runat="server" />
            </td>
            <td class="oddrow-l" colspan="2">
            </td>
        </tr>
    </table>--%>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow">
                备注:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:Label ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
