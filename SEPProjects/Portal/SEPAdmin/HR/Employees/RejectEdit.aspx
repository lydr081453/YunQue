<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RejectEdit.aspx.cs" Inherits="SEPAdmin.HR.Employees.RejectEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul>
                        <li><a href="#fragment-1"><span>待入职人员信息</span></a></li>
                    </ul>
                    <div id="fragment-1" style="padding: 0px 0px 0px 0px;">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    员工编号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtUserId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    入职日期:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_JoinDate" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    公司邮箱:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtEmail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    登录名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtItCode" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    姓名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labBase_NameCn" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    性别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labBase_Sex" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    身份证号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labIDCard" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    联系电话:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtTel" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    员工类型:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="drpUserType" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    工作地点:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labWorkCity" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    所属公司:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_CompanyName" runat="server" />
                                    <asp:HiddenField ID="hidCompanyId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    部门:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_DepartmentName" runat="server" />
                                    <asp:HiddenField ID="hidDepartmentID" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    组别:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_GroupName" runat="server" />
                                    <asp:HiddenField ID="hidGroupId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    职位:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_JoinJob" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    简历文档
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labResume" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    自己带笔记本:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labOwnedPC" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    社会工龄:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labSeniority" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    &nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                   &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6" style="height: 30px;">
                                    Reject备注:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="100px" Width="100%" runat="server" /><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="请填写Reject备注"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
        <table width="90%">
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click" OnClientClick="return confirm('确定归入Reject人员库？');"
                    Text=" Reject " />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" 返 回 " CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
