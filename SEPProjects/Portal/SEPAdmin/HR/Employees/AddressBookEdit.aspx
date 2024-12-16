<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressBookEdit.aspx.cs"
    Inherits="SEPAdmin.HR.Employees.AddressBookEdit" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#container-1').tabs();
        });

        function PositionClick() {
            var deptid = document.getElementById("<%= hidDeptId.ClientID%>").value;
            art.dialog.open('/HR/Employees/PositionDlg.aspx?deptid=' + deptid, { title: '职位列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }

    </script>

    <link rel="stylesheet" href="/public/css/jquery.tabs.css" type="text/css" media="print, projection, screen" />

    <table width="100%">
        <tr>
            <td>
                <div id="container-1">
                    <ul style="margin: 0 0 0 3px;">
                        <li><a href="#fragment-1"><span>员工通讯信息</span></a></li>
                    </ul>
                    <div id="fragment-1">
                        <table width="99%" class="tableForm" style="border-color: #CED4E7;">
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    员工编号:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtUserCode" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    中文名:
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    <asp:Label ID="txtCNName" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    英文名:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:Label ID="txtENName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%; height: 30px;">
                                    分机:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtPhone1" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone1"
                                        Display="None" ErrorMessage="请填写分机"></asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 10%">
                                    手机:
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:TextBox ID="txtMobilePhone" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMobilePhone"
                                        Display="Dynamic" ErrorMessage="请填写手机">请填写手机</asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow-l" style="width: 10%">
                                    职位：
                                </td>
                                <td class="oddrow-l" style="width: 20%">
                                    <asp:HiddenField ID="txtJob_JoinJob" runat="server" />
                                    <asp:HiddenField ID="hidDeptId" runat="server" />
                                    <asp:TextBox runat="server" ID="txtPosition" Enabled="false"></asp:TextBox>
                                     <input type="button" id="btnPosition" class="widebuttons" value="选择..." onclick="PositionClick();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 10%">
                                    邮箱：
                                </td>
                                <td class="oddrow-l" colspan="5">
                                    <asp:TextBox ID="txtEmail" runat="server" />
                                    <font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="请填写邮箱">请填写邮箱</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    runat="server" />
            </td>
        </tr>
    </table>
    <table width="90%">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    Text=" 返 回 " CausesValidation="false" />
            </td>
        </tr>
    </table>
    
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
</asp:Content>
