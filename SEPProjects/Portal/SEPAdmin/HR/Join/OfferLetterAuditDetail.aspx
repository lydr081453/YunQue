<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterAuditDetail.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.OfferLetterAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul>
                        <li><a href="#fragment-1"><span>Offer Letter信息</span></a></li>
                    </ul>
                    <div id="fragment-1" style="padding: 0px 0px 0px 0px;">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    中文姓:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblBase_LastNameCn" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    中文名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblBase_FirstNameCn" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    身份证号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblIDCard" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    个人邮箱:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblPrivateEmail" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    手机:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblMobilePhone" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    应届毕业生:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblExamen" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    所属公司:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblJob_CompanyName" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    部门:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblJob_DepartmentName" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    组别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblJob_GroupName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    工作地点:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblWorktype" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    员工类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblUserType" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    职位:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblJob_JoinJob" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    入职日期:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblJob_JoinDate" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    基本工资:<br />
                                    (税前)
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblNowBasePay" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    绩效工资:<br />
                                    (税前)
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblNowMeritPay" runat="server" />
                                </td>
                            </tr>
                            <tr>
                            <td class="oddrow" style="width: 10%">
                                    工资职级:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblLevel" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    职级下限
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblLevelLow" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    职级上限
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblLevelHigh" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    模板类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="lblOfferTemplate" runat="server" />
                                </td>
                                <td class="oddrow">
                                    社会工龄:
                                </td>
                                <td class="oddrow-l">
                                    <asp:Label ID="lblSeniority" runat="server" />
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6">
                                    备注:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:Label ID="lblJob_Memo" runat="server" Height="100px" Width="100%" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAudit" runat="server" CssClass="widebuttons" OnClick="btnAudit_Click"
                                        Text=" 审批通过 " />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnBackAudit" runat="server" CssClass="widebuttons" OnClick="btnBackAudit_Click"
                                        Text=" 驳 回 " />&nbsp;&nbsp;&nbsp;
                                    <input id="btnReturn" type="button" class="widebuttons" onclick="javascript:window.location.href='OfferLetterAuditList.aspx'"
                                        value=" 返 回 " />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
