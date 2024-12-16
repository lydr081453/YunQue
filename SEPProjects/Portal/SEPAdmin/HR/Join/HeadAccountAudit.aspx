<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountAudit.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/public/js/jquery1.12.js"></script>

    <table width="100%" class="tableForm" style="margin: 20px 0px 2px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">申请人:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblCreator"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">申请日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblAppDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">部门:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblDept"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">职务:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblPosition"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">职级:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblLevel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">工资范围:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">服务客户:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblCustomer"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">业务新增:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkCreate" Text="立项" Enabled="false" />&nbsp;&nbsp;<asp:CheckBox
                    runat="server" ID="chkUnCreate" Text="未立项" Enabled="false" />
            </td>
        </tr>
        <tr runat="server" id="trReplace1">
            <td class="oddrow" style="width: 10%">被替员工:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblReplaceUser"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trReplace2">
            <td class="oddrow" style="width: 10%">替换原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblReplaceReason"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trReplace3">
            <td class="oddrow" style="width: 10%">离岗日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblDimissionDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">职级选择:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBox runat="server" ID="chkAAD" Text="AAD（含）以上，由HR总监最终打印文件" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">工作职责:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblResponse"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">资格需求:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblRequestment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td colspan="3">
                <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td class="oddrow" style="width: 10%">团队报告附件:
            </td>
            <td colspan="3">
                <asp:FileUpload ID="fileupContract" runat="server" Width="60%" /><asp:HyperLink ID="hypAttach"
                    runat="server" ImageUrl="/images/ico_04.gif" Visible="false"></asp:HyperLink><font color="red">*</font>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">中文姓:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblLastNameCn" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">中文名:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblFirstNameCn" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">身份证号:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblIDCard" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">个人邮箱:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblPrivateEmail" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">手机:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblMobile" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">应届毕业生:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:CheckBox ID="chkExamen" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">工作地点:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblLocation" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">员工类型:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblUserType" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">职位:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblUserPosition" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">入职日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblJoinDate" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                基本工资(税前):
                
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblNowBasePay" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                绩效工资(税前):
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblNowMeritPay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                考勤绩效(税前):
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="lblNowAttendance" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">社会工龄:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblSeniority" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">考勤审批人
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblKaoqin"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">是否AAD以上:
            </td>
            <td colspan="5">
                <asp:CheckBox runat="server" ID="CheckBox1" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:Label runat="server" ID="lblUserRemark"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">业务团队意见:
            </td>
            <td colspan="5">
                <asp:Label runat="server" ID="lblGroupDesc"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">HR意见:
            </td>
            <td colspan="5">
                <asp:Label runat="server" ID="lblHRDesc"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">VP审核人
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:Label runat="server" ID="lblVPAuditor"></asp:Label>
            </td>
        </tr>
    </table>
     <table width="100%" class="tableForm" style="margin: 2px 0px 220px 0px; border-color: #CED4E7;">
      <tr>
            <td class="oddrow" style="width: 11%">审批日志:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 11%">审批意见:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtAudit" TextMode="MultiLine" Width="80%" Height="100px"></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnMessage_Click" CausesValidation="false"
                    ForeColor="Red" Font-Underline="true" Font-Bold="true" ToolTip="暂时留个Message,以后再操作">留言</asp:LinkButton>
            </td>
        </tr>
      <tr>
            <td class="oddrow-l" colspan="2">
                <asp:Button runat="server" ID="btnCommit" Text=" 审批通过 "  OnClientClick="showLoading(); " OnClick="btnAudit_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReject" Text=" 审批驳回 "  OnClientClick="showLoading();" OnClick="btnReject_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
