<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"
    Inherits="Employees_EmployeeReadyJob" Title="" CodeBehind="EmployeeReadyJob.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/UserDepartment.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $().ready(function () {
            $('#container-1').tabs();
            show();

            $("#<%=drpContract_Company.ClientID %>").empty();

            Employees_EmployeeReadyJob.getBranch(initContractBranch);
                 function initContractBranch(r) {
                     if (r.value != null)
                         for (k = 0; k < r.value.length; k++) {
                             if (r.value[k][0] == $("#<%=hidContactBranch.ClientID %>").val()) {
                            $("#<%=drpContract_Company.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=drpContract_Company.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
            }

        });

        function changeContractCompany(val, text) {
            if (val == "-1") {
                document.getElementById("<% =hidContactBranch.ClientID %>").value = "";
                document.getElementById("<%= txtBranch.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidContactBranch.ClientID %>").value = val;
                document.getElementById("<%= txtBranch.ClientID %>").value = val;

            }
        }

    </script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />

    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul>
                        <li><a href="#fragment-1"><span>����ְ��Ա��Ϣ</span></a></li>
                    </ul>
                    <div id="fragment-1" style="padding: 0px 0px 0px 0px;">
                        <table width="100%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">Ա�����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtUserId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">��ְ����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_JoinDate" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">��˾����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtEmail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">��¼��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtItCode" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">�ֻ���
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label runat="server" ID="lblTelPhone"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtTelPhone" Visible="false"></asp:TextBox>
                                    <font color="red">*</font>
                                </td>
                                <td class="oddrow-l" style="width: 10%">&nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labBase_NameCn" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">�Ա�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labBase_Sex" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">���֤��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labIDCard" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">��ϵ�绰:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtTel" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">Ա������:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="drpUserType" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">�����ص�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labWorkCity" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">������˾:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_CompanyName" runat="server" />
                                    <asp:HiddenField ID="hidCompanyId" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_DepartmentName" runat="server" />
                                    <asp:HiddenField ID="hidDepartmentID" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">���:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_GroupName" runat="server" />
                                    <asp:HiddenField ID="hidGroupId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">ְλ:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labJob_JoinJob" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">�����ĵ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labResume" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">�Լ����ʼǱ�:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labOwnedPC" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">��Ṥ��:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="labSeniority" runat="server" />
                                </td>
                                <td class="oddrow-l" style="width: 10%">��ٻ�����
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList runat="server" ID="ddlAnnualType">
                                        <asp:ListItem Text="��ѡ��.." Value="-1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="oddrow-l" style="width: 10%">&nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">��ͬ��˾:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:DropDownList ID="drpContract_Company" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">��˾����:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtBranch" runat="server" Enabled="false" />
                                      <input type="hidden" id="hidContactBranch" runat="server" />
                                </td>
                                 <td class="oddrow-l" style="width: 10%">&nbsp;
                                </td>
                                <td class="oddrow-l" style="width: 20%">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" colspan="6" style="height: 30px;">��ע:
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow-l" colspan="6">
                                    <asp:Label ID="labJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
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
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" ȷ �� " />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" �� �� " CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
