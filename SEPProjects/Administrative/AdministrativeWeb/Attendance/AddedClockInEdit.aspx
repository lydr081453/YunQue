<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddedClockInEdit.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.AddedClockInEdit" MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />

    <table width="100%" border="0" cellspacing="10" cellpadding="0" backgsround="../images/t1_28.jpg"
        style="margin: 10px 0 0 20px;">
        <tr>
            <td>
                员工编号或用户名：<asp:TextBox runat="server" ID="txtCode"></asp:TextBox><asp:Button runat="server" ID="btnSearch" Text=" 检索 " OnClick="btnSearch_Click" CausesValidation="false"/>
            </td>
        </tr>
        <tr>
            <td>员工姓名：<asp:Label ID="labUserName" runat="server"></asp:Label><br />
                <asp:HiddenField ID="hidUserid" runat="server" />
                <asp:HiddenField ID="hidUserCardID" runat="server" />
            </td>
        </tr>
        <tr>
            <td>员工编号：<asp:Label ID="labUserCode" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>员工账号：<asp:Label ID="labUserITCode" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>员工电话：<asp:Label ID="labUserTel" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>所属部门：<asp:Label ID="labUserDept" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="500" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="60">门 卡 号：
                        </td>
                        <td width="">
                            <asp:Label ID="txtCardno" runat="server" Width="120"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <table width="500" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="60">考勤日期：
                        </td>
                        <td width="">
                            <ComponentArt:Calendar ID="txtReadTime" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                            </ComponentArt:Calendar>
                        </td>
                        <td width="60">上班时间：
                        </td>
                        <td>
                            <asp:TextBox ID="txtGoWorkTime" runat="server" Width="60px"></asp:TextBox>
                        </td>
                        <td width="60">下班时间：
                        </td>
                        <td>
                            <asp:TextBox ID="txtOffWorkTime" runat="server" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="500" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="60">备注：
                        </td>
                        <td width="">
                            <asp:TextBox TextMode="MultiLine" Width="60%" Rows="3" ID="txtRemark" runat="server"></asp:TextBox>
                            <font color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="备注信息为必填项" ControlToValidate="txtRemark"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ImageUrl="../images/t2_03-20.jpg" Width="56" ID="btnSave" OnClick="btnSave_Click"
                    runat="server" Height="24" />&nbsp;
                                                    <asp:ImageButton ImageUrl="../images/t2_03-22.jpg" Width="56" ID="btnBack" OnClick="btnBack_Click"
                                                        runat="server" Height="24" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>


</asp:Content>
